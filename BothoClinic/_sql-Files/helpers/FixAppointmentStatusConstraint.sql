USE BothoClinicDB;
GO

-- 1) Check existing Status constraint
SELECT OBJECT_DEFINITION(OBJECT_ID('CK_Appointments_Status')) AS ConstraintDefinition;

-- 2) Show current Status values in use
SELECT DISTINCT Status 
FROM dbo.Appointments 
ORDER BY Status;

-- 3) Fix guest appointment insert with correct Status value
DECLARE @GuestId INT = (SELECT TOP 1 GuestPatientId FROM dbo.GuestPatients);
DECLARE @ProvId  INT = (SELECT TOP 1 UserId FROM dbo.Users WHERE RoleId = 2 AND IsActive = 1);

IF @GuestId IS NULL OR @ProvId IS NULL
BEGIN
    PRINT 'Cannot create test guest appointment: missing guest or provider.';
END
ELSE
BEGIN
    -- Test appointment using allowed Status value
    INSERT INTO dbo.Appointments (
        PatientId,
        GuestPatientId, 
        ProviderId, 
        AppointmentDate, 
        TimeSlot, 
        Reason, 
        Status,           -- Must match allowed values in CK_Appointments_Status
        CreatedDateTime
    )
    VALUES (
        NULL,            -- PatientId NULL for guests
        @GuestId,        -- GuestPatientId
        @ProvId,         -- Provider
        CAST(GETDATE() AS DATE),
        '12:00:00',
        'Test guest appointment with correct status',
        'Confirmed',     -- Using known good Status value
        GETDATE()
    );
    PRINT 'Test guest appointment created with Status = Confirmed';
END
GO

-- Verify latest appointments including Status
SELECT TOP 5 
    AppointmentId,
    CASE 
        WHEN PatientId IS NOT NULL THEN 'Student'
        WHEN GuestPatientId IS NOT NULL THEN 'Guest'
        ELSE 'Unknown'
    END AS AppointmentType,
    Status,
    PatientId,
    GuestPatientId,
    AppointmentDate,
    TimeSlot
FROM dbo.Appointments
ORDER BY AppointmentId DESC;
GO