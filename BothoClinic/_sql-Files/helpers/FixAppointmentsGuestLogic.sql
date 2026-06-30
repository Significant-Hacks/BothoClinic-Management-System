USE BothoClinicDB;
GO

-- 1) Drop existing constraints that reference PatientId
IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Appointments_Patients')
    ALTER TABLE dbo.Appointments DROP CONSTRAINT FK_Appointments_Patients;

IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Appointments_PatientType')
    ALTER TABLE dbo.Appointments DROP CONSTRAINT CK_Appointments_PatientType;

-- 2) Modify PatientId to allow NULL
ALTER TABLE dbo.Appointments ALTER COLUMN PatientId INT NULL;
PRINT 'Made PatientId nullable';

-- 3) Re-add foreign key and check constraint
ALTER TABLE dbo.Appointments
ADD CONSTRAINT FK_Appointments_Patients 
FOREIGN KEY (PatientId) REFERENCES dbo.Patients(PatientId);
PRINT 'Re-added FK_Appointments_Patients';

ALTER TABLE dbo.Appointments
ADD CONSTRAINT CK_Appointments_PatientType
CHECK (
    (PatientId IS NOT NULL AND GuestPatientId IS NULL)
    OR (PatientId IS NULL AND GuestPatientId IS NOT NULL)
);
PRINT 'Re-added CK_Appointments_PatientType';

-- 4) Test guest appointment insert
DECLARE @GuestId INT = (SELECT TOP 1 GuestPatientId FROM dbo.GuestPatients);
DECLARE @ProvId  INT = (SELECT TOP 1 UserId FROM dbo.Users WHERE RoleId = 2 AND IsActive = 1);

IF @GuestId IS NULL OR @ProvId IS NULL
BEGIN
    PRINT 'Cannot create test guest appointment: missing guest or provider.';
END
ELSE
BEGIN
    -- Test appointment at noon today
    INSERT INTO dbo.Appointments (
        PatientId,
        GuestPatientId, 
        ProviderId, 
        AppointmentDate, 
        TimeSlot, 
        Reason, 
        Status, 
        CreatedDateTime
    )
    VALUES (
        NULL,           -- PatientId must be NULL for guests
        @GuestId,       -- GuestPatientId from existing guest
        @ProvId,        -- Provider
        CAST(GETDATE() AS DATE),
        '12:00:00',
        'Test guest appointment after schema fix',
        'Booked',
        GETDATE()
    );
    PRINT 'Test guest appointment created successfully.';
END
GO

-- Verify appointment types
SELECT TOP 5 
    AppointmentId,
    CASE 
        WHEN PatientId IS NOT NULL THEN 'Student'
        WHEN GuestPatientId IS NOT NULL THEN 'Guest'
        ELSE 'Unknown'
    END AS AppointmentType,
    PatientId,
    GuestPatientId,
    Status,
    AppointmentDate,
    TimeSlot
FROM dbo.Appointments
ORDER BY AppointmentId DESC;
GO