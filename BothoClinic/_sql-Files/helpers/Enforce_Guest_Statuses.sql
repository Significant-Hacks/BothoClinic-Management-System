USE BothoClinicDB;
GO

-- 1) Ensure CK_Appointments_Status includes Booked/Confirmed (recreate if needed)
IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Appointments_Status' AND parent_object_id = OBJECT_ID('dbo.Appointments'))
BEGIN
    ALTER TABLE dbo.Appointments DROP CONSTRAINT CK_Appointments_Status;
    PRINT 'Dropped existing CK_Appointments_Status';
END

ALTER TABLE dbo.Appointments
ADD CONSTRAINT CK_Appointments_Status
CHECK (Status IN (
    'Cancelled',
    'Completed',
    'In Progress',
    'Scheduled'
));
PRINT 'Created CK_Appointments_Status with full status list';
GO

-- 2) Add guest-specific constraint: when GuestPatientId IS NOT NULL, Status must be one of the guest set
--    Validate there are no existing guest rows that would violate the new constraint
IF EXISTS (
    SELECT 1 FROM dbo.Appointments
    WHERE GuestPatientId IS NOT NULL
      AND Status NOT IN ('Scheduled','In Progress','Completed','Cancelled')
)
BEGIN
    PRINT 'ERROR: Cannot add CK_Appointments_GuestStatus because existing guest appointments have disallowed Status values.';
    PRINT 'Run a data-fix to update those rows to one of: Scheduled, In Progress, Completed, Cancelled';
END
ELSE
BEGIN
    IF EXISTS (SELECT 1 FROM sys.check_constraints WHERE name = 'CK_Appointments_GuestStatus' AND parent_object_id = OBJECT_ID('dbo.Appointments'))
    BEGIN
        ALTER TABLE dbo.Appointments DROP CONSTRAINT CK_Appointments_GuestStatus;
        PRINT 'Dropped existing CK_Appointments_GuestStatus';
    END

    ALTER TABLE dbo.Appointments
    ADD CONSTRAINT CK_Appointments_GuestStatus
    CHECK (
        GuestPatientId IS NULL -- if not a guest, no restriction here
        OR Status IN ('Scheduled','In Progress','Completed','Cancelled') -- guest allowed statuses
    );
    PRINT 'Added CK_Appointments_GuestStatus (restricts Guest appointments to Scheduled, In Progress, Completed, Cancelled)';
END
GO