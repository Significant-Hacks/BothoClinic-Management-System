    USE BothoClinicDB;
GO

-- Idempotent: add GuestPatientId column, FK, index and check constraint to Appointments
IF OBJECT_ID('dbo.Appointments','U') IS NULL
BEGIN
    PRINT 'ERROR: Appointments table not found. Aborting.';
    RETURN;
END
GO

-- Add column if missing
IF NOT EXISTS (
    SELECT 1 FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Appointments' AND COLUMN_NAME = 'GuestPatientId'
)
BEGIN
    ALTER TABLE dbo.Appointments ADD GuestPatientId INT NULL;
    PRINT 'Added column dbo.Appointments.GuestPatientId';
END
ELSE
    PRINT 'Column dbo.Appointments.GuestPatientId already exists';
GO

-- Add FK if missing (requires GuestPatients to exist)
IF OBJECT_ID('dbo.GuestPatients','U') IS NOT NULL
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM sys.foreign_keys
        WHERE name = 'FK_Appointments_GuestPatients' AND parent_object_id = OBJECT_ID('dbo.Appointments')
    )
    BEGIN
        ALTER TABLE dbo.Appointments
        ADD CONSTRAINT FK_Appointments_GuestPatients FOREIGN KEY (GuestPatientId) REFERENCES dbo.GuestPatients(GuestPatientId);
        PRINT 'Added FK_Appointments_GuestPatients';
    END
    ELSE
        PRINT 'FK_Appointments_GuestPatients already exists';
END
ELSE
BEGIN
    PRINT 'WARNING: dbo.GuestPatients does not exist; FK creation skipped';
END
GO

-- Create index on the new column if missing
IF NOT EXISTS (
    SELECT 1 FROM sys.indexes WHERE name = 'IX_Appointments_GuestPatientId' AND object_id = OBJECT_ID('dbo.Appointments')
)
BEGIN
    CREATE INDEX IX_Appointments_GuestPatientId ON dbo.Appointments(GuestPatientId);
    PRINT 'Created index IX_Appointments_GuestPatientId';
END
ELSE
    PRINT 'Index IX_Appointments_GuestPatientId already exists';
GO

-- Add check constraint enforcing PatientId XOR GuestPatientId if missing
IF NOT EXISTS (
    SELECT 1 FROM sys.check_constraints 
    WHERE name = 'CK_Appointments_PatientType' AND parent_object_id = OBJECT_ID('dbo.Appointments')
)
BEGIN
    ALTER TABLE dbo.Appointments
    ADD CONSTRAINT CK_Appointments_PatientType
    CHECK (
        (PatientId IS NOT NULL AND GuestPatientId IS NULL)
        OR (PatientId IS NULL AND GuestPatientId IS NOT NULL)
    );
    PRINT 'Added check constraint CK_Appointments_PatientType';
END
ELSE
    PRINT 'Check constraint CK_Appointments_PatientType already exists';
GO