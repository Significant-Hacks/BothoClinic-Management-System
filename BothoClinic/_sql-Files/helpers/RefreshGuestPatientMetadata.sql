USE BothoClinicDB;
GO

SELECT OBJECT_SCHEMA_NAME(object_id) AS SchemaName, OBJECT_NAME(object_id) AS ObjName, TYPE_DESC
FROM sys.objects o
WHERE OBJECT_DEFINITION(o.object_id) LIKE '%GuestPatientId%';

SELECT OBJECT_NAME(object_id) AS ObjName, definition
FROM sys.sql_modules
WHERE definition LIKE '%GuestPatientId%';
GO