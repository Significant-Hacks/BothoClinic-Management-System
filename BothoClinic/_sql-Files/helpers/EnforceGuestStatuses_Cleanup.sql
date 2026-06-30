USE BothoClinicDB;
GO

-- Find guest appointments with disallowed statuses
SELECT AppointmentId, GuestPatientId, Status
FROM dbo.Appointments
WHERE GuestPatientId IS NOT NULL
  AND Status NOT IN ('Scheduled','In Progress','Completed','Cancelled');
GO

-- Optional: update them to 'Scheduled' (run only if you agree)
UPDATE dbo.Appointments
SET Status = 'Scheduled'
WHERE GuestPatientId IS NOT NULL
  AND Status NOT IN ('Scheduled','In Progress','Completed','Cancelled');
GO

-- Verify
SELECT AppointmentId, GuestPatientId, Status
FROM dbo.Appointments
WHERE GuestPatientId IS NOT NULL
ORDER BY AppointmentId DESC;
GO