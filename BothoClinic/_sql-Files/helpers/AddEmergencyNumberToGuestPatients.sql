-- Check the status constraint to see valid values
USE BothoClinicDB;
GO

-- Check the constraint definition
SELECT 
    cc.name AS ConstraintName,
    cc.definition AS ConstraintDefinition
FROM sys.check_constraints cc
INNER JOIN sys.tables t ON cc.parent_object_id = t.object_id
WHERE t.name = 'Appointments' 
  AND cc.name LIKE '%Status%';

-- Check current status values in the table
SELECT DISTINCT Status, COUNT(*) as Count
FROM Appointments 
GROUP BY Status
ORDER BY Status;

-- Check what appointment 37's status became
SELECT AppointmentId, Status, AppointmentDate, TimeSlot, ProviderId 
FROM Appointments 
WHERE AppointmentId = 37;