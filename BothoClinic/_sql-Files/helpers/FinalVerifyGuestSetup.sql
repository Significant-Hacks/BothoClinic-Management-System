USE BothoClinicDB;
GO

-- Expect ZERO rows
SELECT AppointmentId, GuestPatientId, Status
FROM dbo.Appointments
WHERE GuestPatientId IS NOT NULL
  AND Status NOT IN ('Scheduled','In Progress','Completed','Cancelled');
GO