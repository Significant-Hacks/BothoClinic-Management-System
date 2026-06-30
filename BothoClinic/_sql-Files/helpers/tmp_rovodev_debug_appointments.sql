-- Debug query to check what appointments exist
SELECT 
    AppointmentId,
    AppointmentDate,
    TimeSlot,
    Status,
    ProviderId,
    PatientId,
    CreatedDateTime,
    CAST(AppointmentDate AS DATE) as DateOnly,
    CAST(GETDATE() AS DATE) as TodayDate,
    CASE WHEN CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE) THEN 'TODAY' ELSE 'OTHER' END as IsToday
FROM Appointments 
WHERE CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)
ORDER BY AppointmentDate DESC, TimeSlot;

-- Check specifically for unassigned scheduled appointments
SELECT 
    COUNT(*) as UnassignedScheduledCount,
    COUNT(CASE WHEN ProviderId IS NULL THEN 1 END) as NullProviderCount,
    COUNT(CASE WHEN Status = 'Scheduled' THEN 1 END) as ScheduledCount,
    COUNT(CASE WHEN Status = 'Scheduled' AND ProviderId IS NULL THEN 1 END) as UnassignedScheduledToday
FROM Appointments 
WHERE CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE);

-- Check all appointments regardless of date to see what we have
SELECT TOP 10
    AppointmentId,
    AppointmentDate,
    Status,
    ProviderId,
    CASE WHEN ProviderId IS NULL THEN 'UNASSIGNED' ELSE 'ASSIGNED' END as Assignment
FROM Appointments 
ORDER BY AppointmentId DESC;

-- Past Due Appointments list.
SELECT 
    a.AppointmentId,
    a.AppointmentDate,
    a.TimeSlot,
    COALESCE(u.FullName, gp.FullName) AS PatientName,
    COALESCE(p.StudentId, CONCAT('GP', CAST(gp.GuestPatientId AS VARCHAR))) AS StudentId,
    a.Reason
FROM Appointments a
LEFT JOIN Patients p ON a.PatientId = p.PatientId
LEFT JOIN Users u ON p.UserId = u.UserId
LEFT JOIN GuestPatients gp ON a.GuestPatientId = gp.GuestPatientId
WHERE 1=1
AND (
    (CAST(a.AppointmentDate AS DATE) < CAST(GETDATE() AS DATE))
    OR 
    (CAST(a.AppointmentDate AS DATE) = CAST(GETDATE() AS DATE) 
     AND CAST(a.TimeSlot AS TIME) < CAST(GETDATE() AS TIME))
)
ORDER BY a.AppointmentDate, a.TimeSlot
