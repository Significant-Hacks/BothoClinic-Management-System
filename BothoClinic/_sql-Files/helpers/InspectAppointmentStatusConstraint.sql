USE BothoClinicDB;
GO

-- Show constraint definition
SELECT 
    OBJECT_ID('CK_Appointments_Status') AS ConstraintObjectId,
    OBJECT_DEFINITION(OBJECT_ID('CK_Appointments_Status')) AS ConstraintDefinition;
GO

-- Show distinct Status values currently used
SELECT DISTINCT Status
FROM dbo.Appointments
ORDER BY Status;
GO