    USE BothoClinicDB;
    GO

    -- 1) Confirm column exists
    SELECT TABLE_SCHEMA, TABLE_NAME, COLUMN_NAME
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Appointments' AND COLUMN_NAME = 'GuestPatientId';
    GO

    -- 2) Simple runtime test (will error with Msg 207 if column is not visible)
    SELECT TOP 5 GuestPatientId, AppointmentId, PatientId, ProviderId
    FROM dbo.Appointments;
    GO

    -- Insert one sample guest appointment for testing (idempotent)
    DECLARE @GuestId INT = (SELECT TOP 1 GuestPatientId FROM dbo.GuestPatients);
    DECLARE @ProvId  INT = (SELECT TOP 1 UserId FROM dbo.Users WHERE RoleId = 2 AND IsActive = 1);

    IF @GuestId IS NULL OR @ProvId IS NULL
    BEGIN
        PRINT 'Cannot create sample guest appointment: missing guest or provider.';
    END
    ELSE
    BEGIN
        IF NOT EXISTS (
            SELECT 1 FROM dbo.Appointments
            WHERE GuestPatientId = @GuestId
              AND AppointmentDate = CAST(GETDATE() AS DATE)
              AND TimeSlot = '12:00:00'
        )
        BEGIN
            INSERT INTO dbo.Appointments (GuestPatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
            VALUES (@GuestId, @ProvId, CAST(GETDATE() AS DATE), '12:00:00', 'Sample guest appointment', 'Booked', GETDATE());

            PRINT 'Inserted sample guest appointment.';
        END
        ELSE
        BEGIN
            PRINT 'Sample guest appointment already exists.';
        END
    END
    GO

    -- Quick check
    SELECT TOP 10 AppointmentId, GuestPatientId, PatientId, ProviderId, AppointmentDate, TimeSlot, Status
    FROM dbo.Appointments
    ORDER BY AppointmentId DESC;
    GO