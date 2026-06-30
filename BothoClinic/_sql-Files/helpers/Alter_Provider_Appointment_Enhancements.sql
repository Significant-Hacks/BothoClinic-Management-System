-- ============================================================================
-- Provider Claiming, Reschedule, and Status Normalization Enhancements (Corrected)
-- Safe to run multiple times (idempotent checks used where applicable).
-- Order fixed to avoid CHECK constraint violations.
-- ============================================================================

USE BothoClinicDB;
GO

/*
Steps:
 1) Drop existing Status CHECK constraint on Appointments (name-agnostic)
 2) Normalize existing rows to unified statuses
 3) Recreate CHECK constraint with: Scheduled, In Progress, Completed, Cancelled
 4) Add claim/reschedule lineage columns if missing
 5) Add foreign keys for new columns if missing
 6) Create AppointmentProviderHistory table if missing
 7) Helpful indexes
 8) View vAppointmentsWithNames (upsert)
 9) Stored procedures: usp_Appointment_Claim, usp_Appointment_Reschedule, usp_Appointment_UpdateStatus
*/

-- 1) Drop the existing Status check constraint (name-agnostic)
DECLARE @chkName sysname;
SELECT @chkName = kc.name
FROM sys.check_constraints kc
JOIN sys.tables t ON kc.parent_object_id = t.object_id
JOIN sys.columns c ON c.object_id = t.object_id AND c.column_id = kc.parent_column_id
WHERE t.name = 'Appointments' AND c.name = 'Status';

IF @chkName IS NOT NULL
BEGIN
    DECLARE @sql NVARCHAR(MAX) = N'ALTER TABLE dbo.Appointments DROP CONSTRAINT ' + QUOTENAME(@chkName) + N';';
    EXEC sp_executesql @sql;
END
GO

-- 2) Normalize existing Status values to the unified set
UPDATE dbo.Appointments SET Status = 'Scheduled' WHERE Status IN ('Booked','Confirmed');
-- (Optionally coerce any stray values)
UPDATE dbo.Appointments SET Status = 'Scheduled' WHERE Status NOT IN ('Scheduled','In Progress','Completed','Cancelled');
GO

-- 3) Recreate the Status CHECK with the new allowed values
IF NOT EXISTS (
    SELECT 1 FROM sys.check_constraints 
    WHERE parent_object_id = OBJECT_ID('dbo.Appointments') AND name = 'CK_Appointments_Status')
BEGIN
    ALTER TABLE dbo.Appointments WITH CHECK ADD CONSTRAINT CK_Appointments_Status
    CHECK (Status IN ('Scheduled','In Progress','Completed','Cancelled'));
END
GO

-- 4) Add provider-claim/reschedule lineage columns if missing
IF COL_LENGTH('dbo.Appointments','ClaimedByProviderId') IS NULL
    ALTER TABLE dbo.Appointments ADD ClaimedByProviderId INT NULL;
IF COL_LENGTH('dbo.Appointments','ClaimedAt') IS NULL
    ALTER TABLE dbo.Appointments ADD ClaimedAt DATETIME NULL;
IF COL_LENGTH('dbo.Appointments','RescheduledFromAppointmentId') IS NULL
    ALTER TABLE dbo.Appointments ADD RescheduledFromAppointmentId INT NULL;
GO

-- 5) Add FKs for the new columns if not present
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Appointments_ClaimedByProvider_Users')
BEGIN
    ALTER TABLE dbo.Appointments
    ADD CONSTRAINT FK_Appointments_ClaimedByProvider_Users
    FOREIGN KEY (ClaimedByProviderId) REFERENCES dbo.Users(UserId);
END

IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Appointments_RescheduledFrom_Appointment')
BEGIN
    ALTER TABLE dbo.Appointments
    ADD CONSTRAINT FK_Appointments_RescheduledFrom_Appointment
    FOREIGN KEY (RescheduledFromAppointmentId) REFERENCES dbo.Appointments(AppointmentId);
END
GO

-- 6) History table to audit provider assignment changes
IF NOT EXISTS (SELECT 1 FROM sys.tables WHERE name = 'AppointmentProviderHistory' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.AppointmentProviderHistory
    (
        Id                INT IDENTITY(1,1) PRIMARY KEY,
        AppointmentId     INT NOT NULL,
        ProviderId        INT NULL,
        Action            VARCHAR(20) NOT NULL, -- Assigned | Reassigned | Unassigned | Claimed
        PerformedByUserId INT NOT NULL,
        Timestamp         DATETIME NOT NULL DEFAULT(GETDATE()),
        Notes             NVARCHAR(500) NULL,
        CONSTRAINT FK_APH_Appointment FOREIGN KEY (AppointmentId) REFERENCES dbo.Appointments(AppointmentId),
        CONSTRAINT FK_APH_Provider    FOREIGN KEY (ProviderId)    REFERENCES dbo.Users(UserId),
        CONSTRAINT FK_APH_ByUser      FOREIGN KEY (PerformedByUserId) REFERENCES dbo.Users(UserId)
    );
END
GO

-- 7) Helpful indexes
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Appointments_StatusDate')
    CREATE INDEX IX_Appointments_StatusDate ON dbo.Appointments(Status, AppointmentDate);
IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Appointments_ClaimedAt')
    CREATE INDEX IX_Appointments_ClaimedAt ON dbo.Appointments(ClaimedAt) INCLUDE (Status, ProviderId);
GO

-- 8) Utility view: appointments with patient and provider names (upsert)
IF OBJECT_ID('dbo.vAppointmentsWithNames','V') IS NULL
    EXEC('CREATE VIEW dbo.vAppointmentsWithNames AS SELECT 1 AS Dummy');
GO

ALTER VIEW dbo.vAppointmentsWithNames
AS
SELECT 
    a.AppointmentId,
    a.AppointmentDate,
    a.TimeSlot,
    a.Reason,
    a.Status,
    a.PatientId,
    pu.FullName AS PatientName,
    p.StudentId,
    a.ProviderId,
    pr.FullName AS ProviderName,
    a.ClaimedByProviderId,
    a.ClaimedAt,
    a.RescheduledFromAppointmentId,
    a.CreatedDateTime
FROM dbo.Appointments a
JOIN dbo.Patients p ON a.PatientId = p.PatientId
JOIN dbo.Users pu ON p.UserId = pu.UserId
LEFT JOIN dbo.Users pr ON a.ProviderId = pr.UserId;
GO

-- 9) Stored procedures
-- 9.1 Claim/Start
IF OBJECT_ID('dbo.usp_Appointment_Claim','P') IS NULL
    EXEC('CREATE PROCEDURE dbo.usp_Appointment_Claim AS SELECT 1');
GO

ALTER PROCEDURE dbo.usp_Appointment_Claim
    @AppointmentId INT,
    @ProviderId    INT,
    @PerformedBy   INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Appointments
    SET 
        ProviderId = COALESCE(ProviderId, @ProviderId),
        ClaimedByProviderId = COALESCE(ClaimedByProviderId, @ProviderId),
        ClaimedAt = COALESCE(ClaimedAt, GETDATE()),
        Status = CASE WHEN Status = 'Scheduled' THEN 'In Progress' ELSE Status END
    WHERE AppointmentId = @AppointmentId
      AND (
            (Status = 'Scheduled') OR
            (Status = 'In Progress' AND (ProviderId = @ProviderId OR ProviderId IS NULL))
          );

    IF @@ROWCOUNT = 0
    BEGIN
        RAISERROR('Appointment cannot be claimed (already owned or not in a claimable state).', 16, 1);
        RETURN;
    END

    INSERT INTO dbo.AppointmentProviderHistory(AppointmentId, ProviderId, Action, PerformedByUserId, Notes)
    VALUES(@AppointmentId, @ProviderId, 'Claimed', @PerformedBy, 'Provider claimed or started the appointment');
END
GO

-- 9.2 Reschedule (update date/time and optionally provider)
IF OBJECT_ID('dbo.usp_Appointment_Reschedule','P') IS NULL
    EXEC('CREATE PROCEDURE dbo.usp_Appointment_Reschedule AS SELECT 1');
GO

ALTER PROCEDURE dbo.usp_Appointment_Reschedule
    @AppointmentId INT,
    @NewDate       DATE,
    @NewTime       TIME,
    @NewProviderId INT = NULL,
    @PerformedBy   INT,
    @Notes         NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Appointments
    SET AppointmentDate = @NewDate,
        TimeSlot = @NewTime,
        ProviderId = COALESCE(@NewProviderId, ProviderId),
        Status = 'Scheduled'
    WHERE AppointmentId = @AppointmentId;

    INSERT INTO dbo.AppointmentProviderHistory(AppointmentId, ProviderId, Action, PerformedByUserId, Notes)
    VALUES(@AppointmentId, @NewProviderId, 'Assigned', @PerformedBy, COALESCE(@Notes, 'Rescheduled'));
END
GO

-- 9.3 Update Status (with validation)
IF OBJECT_ID('dbo.usp_Appointment_UpdateStatus','P') IS NULL
    EXEC('CREATE PROCEDURE dbo.usp_Appointment_UpdateStatus AS SELECT 1');
GO

ALTER PROCEDURE dbo.usp_Appointment_UpdateStatus
    @AppointmentId INT,
    @NewStatus     VARCHAR(50),
    @PerformedBy   INT,
    @Notes         NVARCHAR(500) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @NewStatus NOT IN ('Scheduled','In Progress','Completed','Cancelled')
    BEGIN
        RAISERROR('Invalid status value.',16,1);
        RETURN;
    END

    UPDATE dbo.Appointments SET Status = @NewStatus WHERE AppointmentId = @AppointmentId;

    INSERT INTO dbo.AppointmentProviderHistory(AppointmentId, ProviderId, Action, PerformedByUserId, Notes)
    SELECT @AppointmentId, ProviderId, 'Reassigned', @PerformedBy, COALESCE(@Notes, 'Status update to ' + @NewStatus)
    FROM dbo.Appointments WHERE AppointmentId = @AppointmentId;
END
GO

-- End of script
