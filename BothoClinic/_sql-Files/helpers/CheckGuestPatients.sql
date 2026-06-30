-- confirm GuestPatients table exists and Appointments.GuestPatientId column
USE BothoClinicDB;
GO

SELECT OBJECT_ID('dbo.GuestPatients') AS GuestPatientsObjectId;

SELECT TABLE_NAME, COLUMN_NAME
FROM INFORMATION_SCHEMA.COLUMNS
WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Appointments' AND COLUMN_NAME = 'GuestPatientId';

-- Find any DB objects that reference the name (helps find ordering conflicts)
SELECT OBJECT_SCHEMA_NAME(object_id) AS SchemaName, OBJECT_NAME(object_id) AS ObjName, definition
FROM sys.sql_modules
WHERE definition LIKE '%GuestPatientId%';
GO