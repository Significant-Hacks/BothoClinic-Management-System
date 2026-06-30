-- =============================================================================
-- Botho University Campus Clinic - Comprehensive Data Population Script (v2)
-- =============================================================================

-- Use the project database
USE BothoClinicDB;
GO

-- =============================================================================
-- 1. Clear Existing Data
-- Delete from tables in the correct order to avoid foreign key conflicts.
-- =============================================================================
DELETE FROM Prescriptions;
DELETE FROM Consultations;
DELETE FROM Appointments;
DELETE FROM Reminders;
DELETE FROM PasswordResetTokens;
DELETE FROM Patients;
DELETE FROM Users;
DELETE FROM Medications;
DELETE FROM Roles;
GO

-- =============================================================================
-- 2. Reset Identity Columns
-- =============================================================================
DBCC CHECKIDENT ('Roles', RESEED, 0);
DBCC CHECKIDENT ('Users', RESEED, 0);
DBCC CHECKIDENT ('Patients', RESEED, 0);
DBCC CHECKIDENT ('Medications', RESEED, 0);
DBCC CHECKIDENT ('Appointments', RESEED, 0);
DBCC CHECKIDENT ('Consultations', RESEED, 0);
DBCC CHECKIDENT ('Prescriptions', RESEED, 0);
DBCC CHECKIDENT ('Reminders', RESEED, 0);
DBCC CHECKIDENT ('PasswordResetTokens', RESEED, 0);
GO

-- =============================================================================
-- 3. Populate Roles
-- =============================================================================
INSERT INTO Roles (RoleName, Description) VALUES
('Administrator', 'Manages users, settings, and system reports.'),
('Provider', 'Healthcare provider (Doctor/Nurse) who consults with patients.'),
('Student', 'A student of Botho University who is a patient of the clinic.');
GO

-- =============================================================================
-- 4. Populate Medications
-- =============================================================================
INSERT INTO Medications (MedicationName, Description) VALUES
('Paracetamol 500mg', 'For pain and fever relief.'),
('Ibuprofen 200mg', 'Anti-inflammatory and pain relief.'),
('Amoxicillin 250mg', 'Antibiotic for bacterial infections.'),
('Loratadine 10mg', 'Antihistamine for allergies.'),
('Cough Syrup (Generic)', 'For relief of cough symptoms.'),
('Betadine Solution', 'Antiseptic for wound cleaning.'),
('Rehydration Salts', 'For replacing fluids and electrolytes.'),
('Vitamin C 1000mg', 'Immune system support.');
GO

-- =============================================================================
-- 5. Populate Users
-- Note: Passwords are set to 'Password123' and should be changed on first login.
-- The hash and salt values provided are for 'Password123'.
-- =============================================================================
-- Administrator
INSERT INTO Users (Username, PasswordHash, Salt, FullName, ContactEmail, RoleId, IsActive, MustChangePassword)
VALUES ('admin', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Admin User', 'admin@bothoclinic.ac.bw', 1, 1, 1);

-- Providers
INSERT INTO Users (Username, PasswordHash, Salt, FullName, ContactEmail, RoleId, IsActive, MustChangePassword)
VALUES
('dr_thabo', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Dr. Thabo Moloi', 'thabo.moloi@bothoclinic.ac.bw', 2, 1, 1),
('nurse_anna', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Nurse Anna Phiri', 'anna.phiri@bothoclinic.ac.bw', 2, 1, 1);

-- Students
INSERT INTO Users (Username, PasswordHash, Salt, FullName, ContactEmail, RoleId, IsActive, MustChangePassword)
VALUES
('s2023001', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Lethabo Ndlovu', 's2023001@student.bothouniversity.ac.bw', 3, 1, 1),
('s2023002', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Tshepo Kgosi', 's2023002@student.bothouniversity.ac.bw', 3, 1, 1),
('s2023003', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Boitumelo Molefe', 's2023003@student.bothouniversity.ac.bw', 3, 1, 1),
('s2022015', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Naledi Dube', 's2022015@student.bothouniversity.ac.bw', 3, 1, 1),
('s2021042', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Katlego Moremi', 's2021042@student.bothouniversity.ac.bw', 3, 0, 1); -- Inactive student
GO

-- =============================================================================
-- 6. Populate Patients
-- =============================================================================
INSERT INTO Patients (UserId, StudentId, DOB, Gender, BloodType)
VALUES
((SELECT UserId FROM Users WHERE Username = 's2023001'), 'S2023001', '2003-08-21', 'Female', 'O+'),
((SELECT UserId FROM Users WHERE Username = 's2023002'), 'S2023002', '2002-11-10', 'Male', 'A-'),
((SELECT UserId FROM Users WHERE Username = 's2023003'), 'S2023003', '2004-01-30', 'Female', 'B+'),
((SELECT UserId FROM Users WHERE Username = 's2022015'), 'S2022015', '2001-06-15', 'Female', 'AB+'),
((SELECT UserId FROM Users WHERE Username = 's2021042'), 'S2021042', '2000-03-05', 'Male', 'O-');
GO

-- =============================================================================
-- 7. Populate Appointments, Consultations, Prescriptions, and Reminders
-- =============================================================================

-- Declare variables for IDs
DECLARE @dr_thabo_id INT = (SELECT UserId FROM Users WHERE Username = 'dr_thabo');
DECLARE @nurse_anna_id INT = (SELECT UserId FROM Users WHERE Username = 'nurse_anna');

DECLARE @p_lethabo_id INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2023001');
DECLARE @p_tshepo_id INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2023002');
DECLARE @p_boitumelo_id INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2023003');
DECLARE @p_naledi_id INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2022015');

-- Past Appointments (Completed)
INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
VALUES
(@p_lethabo_id, @dr_thabo_id, DATEADD(day, -30, GETDATE()), '09:30:00', 'Sore throat and headache', 'Completed', DATEADD(day, -32, GETDATE())),
(@p_tshepo_id, @nurse_anna_id, DATEADD(day, -25, GETDATE()), '14:00:00', 'Minor cut on hand', 'Completed', DATEADD(day, -26, GETDATE())),
(@p_boitumelo_id, @dr_thabo_id, DATEADD(day, -15, GETDATE()), '11:00:00', 'Follow-up on allergy medication', 'Completed', DATEADD(day, -20, GETDATE())),
(@p_naledi_id, @dr_thabo_id, DATEADD(day, -10, GETDATE()), '15:30:00', 'Flu-like symptoms', 'Completed', DATEADD(day, -11, GETDATE())),
(@p_lethabo_id, @nurse_anna_id, DATEADD(day, -5, GETDATE()), '10:00:00', 'Check blood pressure', 'Completed', DATEADD(day, -7, GETDATE()));

-- Past Appointments (Cancelled)
INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
VALUES
(@p_tshepo_id, @dr_thabo_id, DATEADD(day, -12, GETDATE()), '16:00:00', 'Sports injury consultation', 'Cancelled', DATEADD(day, -14, GETDATE()));

-- Upcoming Appointments (Booked & Confirmed)
INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
VALUES
(@p_lethabo_id, @dr_thabo_id, DATEADD(day, 2, GETDATE()), '14:00:00', 'General check-up', 'Confirmed', DATEADD(day, -3, GETDATE())),
(@p_tshepo_id, NULL, DATEADD(day, 3, GETDATE()), '10:30:00', 'Feeling unwell, stomach ache', 'Booked', DATEADD(day, -1, GETDATE())),
(@p_naledi_id, @nurse_anna_id, DATEADD(day, 5, GETDATE()), '09:00:00', 'Vaccination query', 'Confirmed', DATEADD(day, -2, GETDATE()));

-- Consultations
INSERT INTO Consultations (AppointmentId, ProviderId, Temperature, BloodPressure, DiagnosisNotes, FollowUpNeeded)
VALUES
((SELECT AppointmentId FROM Appointments WHERE PatientId = @p_lethabo_id AND Reason = 'Sore throat and headache'), @dr_thabo_id, 37.8, '122/81', 'Mild viral infection. Advised rest and hydration.', 0),
((SELECT AppointmentId FROM Appointments WHERE PatientId = @p_tshepo_id AND Reason = 'Minor cut on hand'), @nurse_anna_id, 36.9, '118/78', 'Cleaned and dressed the wound. Advised to keep it dry.', 0),
((SELECT AppointmentId FROM Appointments WHERE PatientId = @p_boitumelo_id AND Reason = 'Follow-up on allergy medication'), @dr_thabo_id, 37.0, '120/80', 'Allergy symptoms have subsided. Continue medication as needed.', 0),
((SELECT AppointmentId FROM Appointments WHERE PatientId = @p_naledi_id AND Reason = 'Flu-like symptoms'), @dr_thabo_id, 38.5, '125/85', 'Diagnosed with seasonal flu. Prescribed rest and medication.', 1),
((SELECT AppointmentId FROM Appointments WHERE PatientId = @p_lethabo_id AND Reason = 'Check blood pressure'), @nurse_anna_id, 37.1, '121/79', 'Blood pressure is normal. No issues found.', 0);

-- Prescriptions
INSERT INTO Prescriptions (ConsultationId, MedicationId, Dosage, Quantity, Instructions)
VALUES
((SELECT c.ConsultationId FROM Consultations c JOIN Appointments a ON c.AppointmentId = a.AppointmentId WHERE a.PatientId = @p_lethabo_id AND a.Reason = 'Sore throat and headache'), (SELECT MedicationId FROM Medications WHERE MedicationName = 'Paracetamol 500mg'), '2 tablets every 6 hours', 20, 'Take with food. Do not exceed 8 tablets in 24 hours.'),
((SELECT c.ConsultationId FROM Consultations c JOIN Appointments a ON c.AppointmentId = a.AppointmentId WHERE a.PatientId = @p_naledi_id AND a.Reason = 'Flu-like symptoms'), (SELECT MedicationId FROM Medications WHERE MedicationName = 'Ibuprofen 200mg'), '1 tablet every 8 hours', 15, 'Take after meals to avoid stomach upset.'),
((SELECT c.ConsultationId FROM Consultations c JOIN Appointments a ON c.AppointmentId = a.AppointmentId WHERE a.PatientId = @p_naledi_id AND a.Reason = 'Flu-like symptoms'), (SELECT MedicationId FROM Medications WHERE MedicationName = 'Cough Syrup (Generic)'), '10ml three times a day', 1, 'Shake well before use.');

-- Reminders
INSERT INTO Reminders (PatientID, Message, ReminderDate, IsRead)
VALUES
(@p_lethabo_id, 'Your next appointment is in 2 days. Please be on time.', DATEADD(day, 2, GETDATE()), 0),
(@p_naledi_id, 'Follow-up appointment for flu check-up is due next week.', DATEADD(day, 7, GETDATE()), 0),
(@p_tshepo_id, 'Remember to drink plenty of water and stay hydrated.', DATEADD(day, -10, GETDATE()), 1),
(@p_boitumelo_id, 'Time for your annual health check-up. Please book an appointment.', DATEADD(day, 30, GETDATE()), 0);
GO

-- =============================================================================
-- 8. Additional Provider Profiles and Today Appointments for Provider Dashboard testing
--    (Non-destructive: adds new table ProviderProfiles if missing and seeds richer data)
-- =============================================================================

-- Create ProviderProfiles table if it does not exist
IF NOT EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderProfiles]') AND type in (N'U'))
BEGIN
    CREATE TABLE ProviderProfiles (
        ProfileId INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL UNIQUE,
        Title VARCHAR(50) NULL,            -- e.g., Dr., Nurse
        Specialty VARCHAR(100) NULL,       -- e.g., General Practitioner
        Department VARCHAR(100) NULL,      -- e.g., Campus Clinic
        Bio NVARCHAR(MAX) NULL,
        FOREIGN KEY (UserId) REFERENCES Users(UserId)
    );
END
GO

-- Clear ProviderProfiles on population runs to keep data consistent
IF EXISTS (SELECT 1 FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProviderProfiles]') AND type in (N'U'))
BEGIN
    DELETE FROM ProviderProfiles;
    DBCC CHECKIDENT ('ProviderProfiles', RESEED, 0);
END
GO

-- Optional: Ensure a Provider user "dr_sarah" exists for prototype alignment
IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'dr_sarah')
BEGIN
    INSERT INTO Users (Username, PasswordHash, Salt, FullName, ContactEmail, RoleId, IsActive, MustChangePassword)
    VALUES ('dr_sarah', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Dr. Sarah Johnson', 'sarah.johnson@bothoclinic.ac.bw', 2, 1, 1);
END
GO

-- Seed ProviderProfiles for all providers
MERGE ProviderProfiles AS target
USING (
    SELECT UserId, 'Dr.' AS Title, 'General Practitioner' AS Specialty, 'Campus Clinic' AS Department, NULL AS Bio
    FROM Users WHERE Username IN ('dr_thabo','dr_sarah')
    UNION ALL
    SELECT UserId, 'Nurse' AS Title, 'General Practice' AS Specialty, 'Campus Clinic' AS Department, NULL AS Bio
    FROM Users WHERE Username = 'nurse_anna'
) AS src
ON target.UserId = src.UserId
WHEN MATCHED THEN UPDATE SET Title = src.Title, Specialty = src.Specialty, Department = src.Department, Bio = src.Bio
WHEN NOT MATCHED THEN INSERT (UserId, Title, Specialty, Department, Bio)
VALUES (src.UserId, src.Title, src.Specialty, src.Department, src.Bio);
GO

-- Ensure a couple of extra student users/patients exist to diversify appointments
IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = 's2023004')
BEGIN
    INSERT INTO Users (Username, PasswordHash, Salt, FullName, ContactEmail, RoleId, IsActive, MustChangePassword)
    VALUES ('s2023004', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Neo Phuthego', 's2023004@student.bothouniversity.ac.bw', 3, 1, 1);
END
IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = 's2023005')
BEGIN
    INSERT INTO Users (Username, PasswordHash, Salt, FullName, ContactEmail, RoleId, IsActive, MustChangePassword)
    VALUES ('s2023005', 'bB5/1b8rO3s2G5d/i43L+L+ssWcpFwHN58wDmXM/MKs=', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE4=', 'Onkarabile Kgosietsile', 's2023005@student.bothouniversity.ac.bw', 3, 1, 1);
END
GO

-- Create Patients for the new student users if missing
IF NOT EXISTS (SELECT 1 FROM Patients WHERE StudentId = 'S2023004')
BEGIN
    INSERT INTO Patients (UserId, StudentId, DOB, Gender, BloodType)
    VALUES ((SELECT UserId FROM Users WHERE Username = 's2023004'), 'S2023004', '2003-04-12', 'Female', 'A+');
END
IF NOT EXISTS (SELECT 1 FROM Patients WHERE StudentId = 'S2023005')
BEGIN
    INSERT INTO Patients (UserId, StudentId, DOB, Gender, BloodType)
    VALUES ((SELECT UserId FROM Users WHERE Username = 's2023005'), 'S2023005', '2002-02-22', 'Male', 'B-');
END
GO

-- Today appointments for providers (mix of statuses for dashboard)
DECLARE @today DATE = CAST(GETDATE() AS DATE);
DECLARE @prov_thabo INT = (SELECT UserId FROM Users WHERE Username = 'dr_thabo');
DECLARE @prov_sarah INT = (SELECT UserId FROM Users WHERE Username = 'dr_sarah');
DECLARE @prov_anna  INT = (SELECT UserId FROM Users WHERE Username = 'nurse_anna');

DECLARE @pat_3001 INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2023001');
DECLARE @pat_3002 INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2023002');
DECLARE @pat_3003 INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2023003');
DECLARE @pat_2015 INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2022015');
DECLARE @pat_3004 INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2023004');
DECLARE @pat_3005 INT = (SELECT PatientId FROM Patients WHERE StudentId = 'S2023005');

-- Insert only if not already inserted today for these times to avoid duplicates on rerun
IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_thabo AND AppointmentDate = @today AND TimeSlot = '08:00:00')
BEGIN
    INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
    VALUES (@pat_3001, @prov_thabo, @today, '08:00:00', 'General Checkup', 'Confirmed', DATEADD(hour, -1, GETDATE()));
END
IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_thabo AND AppointmentDate = @today AND TimeSlot = '09:30:00')
BEGIN
    INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
    VALUES (@pat_3002, @prov_thabo, @today, '09:30:00', 'Follow-up Visit', 'Completed', DATEADD(hour, -2, GETDATE()));
END
IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_thabo AND AppointmentDate = @today AND TimeSlot = '10:30:00')
BEGIN
    INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
    VALUES (@pat_3003, @prov_thabo, @today, '10:30:00', 'Urgent Care', 'Booked', DATEADD(hour, -3, GETDATE()));
END
IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_anna AND AppointmentDate = @today AND TimeSlot = '11:00:00')
BEGIN
    INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
    VALUES (@pat_2015, @prov_anna, @today, '11:00:00', 'Consultation', 'Confirmed', DATEADD(hour, -3, GETDATE()));
END
IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_sarah AND AppointmentDate = @today AND TimeSlot = '14:00:00')
BEGIN
    INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
    VALUES (@pat_3004, @prov_sarah, @today, '14:00:00', 'Vaccination', 'Booked', DATEADD(hour, -4, GETDATE()));
END
IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_sarah AND AppointmentDate = @today AND TimeSlot = '14:30:00')
BEGIN
    INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
    VALUES (@pat_3005, @prov_sarah, @today, '14:30:00', 'General Checkup', 'Cancelled', DATEADD(hour, -5, GETDATE()));
END
GO

-- Helpful view for debugging and data exploration in the app
IF OBJECT_ID('dbo.vAppointmentsWithNames','V') IS NOT NULL
    DROP VIEW dbo.vAppointmentsWithNames;
GO
CREATE VIEW dbo.vAppointmentsWithNames AS
SELECT 
    a.AppointmentId,
    a.AppointmentDate,
    a.TimeSlot,
    a.Status,
    a.Reason,
    a.PatientId,
    p.StudentId,
    u.FullName AS PatientFullName,
    a.ProviderId,
    up.FullName AS ProviderFullName
FROM Appointments a
JOIN Patients p ON a.PatientId = p.PatientId
JOIN Users u ON p.UserId = u.UserId
LEFT JOIN Users up ON a.ProviderId = up.UserId;
GO

-- =============================================================================
-- 9. Clinic Settings: Max Consultation Window
--    Creates ClinicSettings table if missing and seeds MaxConsultationMinutes=30
-- =============================================================================
IF OBJECT_ID('dbo.ClinicSettings','U') IS NULL
BEGIN
    CREATE TABLE dbo.ClinicSettings (
        [Key]   NVARCHAR(100) NOT NULL PRIMARY KEY,
        [Value] NVARCHAR(200) NULL
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.ClinicSettings WHERE [Key] = 'MaxConsultationMinutes')
BEGIN
    INSERT INTO dbo.ClinicSettings([Key],[Value]) VALUES ('MaxConsultationMinutes','30');
END
GO

-- =============================================================================
-- 9b. Roles: Ensure 'Guest Patient' role exists (non-destructive)
IF NOT EXISTS (SELECT 1 FROM dbo.Roles WHERE RoleName = 'Guest Patient')
BEGIN
    INSERT INTO dbo.Roles(RoleName, Description) VALUES ('Guest Patient', 'Non-student walk-in patient role');
END
GO

-- 10. Ensure Today and Tomorrow Appointments exist for Dashboard visibility
--    (Non-destructive inserts guarded by IF NOT EXISTS). Variables scoped per batch.
-- =============================================================================

-- Dr Thabo block (single batch)
DECLARE @today2 DATE = CAST(GETDATE() AS DATE);
DECLARE @tomorrow DATE = DATEADD(DAY, 1, @today2);
DECLARE @prov_thabo2 INT = (SELECT TOP 1 UserId FROM Users WHERE Username = 'dr_thabo');

IF @prov_thabo2 IS NOT NULL AND EXISTS (SELECT 1 FROM Patients)
BEGIN
    DECLARE @pat_t1 INT = (SELECT TOP 1 PatientId FROM Patients ORDER BY PatientId);
    DECLARE @pat_t2 INT = (SELECT TOP 1 PatientId FROM Patients ORDER BY PatientId DESC);

    -- Today for dr_thabo
    IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_thabo2 AND AppointmentDate = @today2 AND TimeSlot = '08:30:00')
        INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
        VALUES (@pat_t1, @prov_thabo2, @today2, '08:30:00', 'General Checkup', 'Confirmed', GETDATE());

    IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_thabo2 AND AppointmentDate = @today2 AND TimeSlot = '10:00:00')
        INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
        VALUES (@pat_t2, @prov_thabo2, @today2, '10:00:00', 'Follow-up Visit', 'Booked', GETDATE());

    -- Tomorrow for dr_thabo
    IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_thabo2 AND AppointmentDate = @tomorrow AND TimeSlot = '09:00:00')
        INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
        VALUES (@pat_t1, @prov_thabo2, @tomorrow, '09:00:00', 'Consultation', 'Booked', GETDATE());
END
GO

-- Dr Sarah block (single batch)
DECLARE @today3 DATE = CAST(GETDATE() AS DATE);
DECLARE @tomorrow3 DATE = DATEADD(DAY, 1, @today3);
DECLARE @prov_sarah3 INT = (SELECT TOP 1 UserId FROM Users WHERE Username = 'dr_sarah');

IF @prov_sarah3 IS NOT NULL AND EXISTS (SELECT 1 FROM Patients)
BEGIN
    DECLARE @pat_s1 INT = (SELECT TOP 1 PatientId FROM Patients ORDER BY PatientId);
    DECLARE @pat_s2 INT = (SELECT TOP 1 PatientId FROM Patients ORDER BY PatientId DESC);

    -- Today for dr_sarah
    IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_sarah3 AND AppointmentDate = @today3 AND TimeSlot = '14:00:00')
        INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
        VALUES (@pat_s1, @prov_sarah3, @today3, '14:00:00', 'Vaccination', 'Confirmed', GETDATE());

    -- Tomorrow for dr_sarah
    IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_sarah3 AND AppointmentDate = @tomorrow3 AND TimeSlot = '15:30:00')
        INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
        VALUES (@pat_s2, @prov_sarah3, @tomorrow3, '15:30:00', 'General Checkup', 'Booked', GETDATE());
END
GO

-- Nurse Anna block (single batch)
DECLARE @today4 DATE = CAST(GETDATE() AS DATE);
DECLARE @tomorrow4 DATE = DATEADD(DAY, 1, @today4);
DECLARE @prov_anna3 INT = (SELECT TOP 1 UserId FROM Users WHERE Username = 'nurse_anna');

IF @prov_anna3 IS NOT NULL AND EXISTS (SELECT 1 FROM Patients)
BEGIN
    DECLARE @pat_a1 INT = (SELECT TOP 1 PatientId FROM Patients ORDER BY NEWID());

    -- Today for nurse_anna
    IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_anna3 AND AppointmentDate = @today4 AND TimeSlot = '11:45:00')
        INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
        VALUES (@pat_a1, @prov_anna3, @today4, '11:45:00', 'Nurse Check', 'Confirmed', GETDATE());

    -- Tomorrow for nurse_anna
    IF NOT EXISTS (SELECT 1 FROM Appointments WHERE ProviderId = @prov_anna3 AND AppointmentDate = @tomorrow4 AND TimeSlot = '10:15:00')
        INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
        VALUES (@pat_a1, @prov_anna3, @tomorrow4, '10:15:00', 'Follow-up', 'Booked', GETDATE());
END
GO

PRINT 'Database populated with comprehensive sample data.';
GO

SELECT * FROM  dbo.vAppointmentsWithNames WHERE ProviderId = (SELECT UserId FROM Users WHERE Username = 'dr_thabo') AND AppointmentDate = CAST(GETDATE() AS DATE) ORDER BY TimeSlot;



-- =============================================================================
-- 11. GUEST PATIENTS TABLE CREATION AND SETUP
-- =============================================================================

-- Safe, idempotent GuestPatients + Appointments changes (no TRY/CATCH/THROW)
-- 1) Create GuestPatients table if missing
IF OBJECT_ID('dbo.GuestPatients','U') IS NULL
BEGIN
    CREATE TABLE dbo.GuestPatients (
        GuestPatientId INT IDENTITY(1,1) PRIMARY KEY,
        FullName NVARCHAR(100) NOT NULL,
        PhoneNumber NVARCHAR(15) NOT NULL,
        EmergencyStatus BIT NOT NULL DEFAULT 0,
        EmergencyNumber NVARCHAR(50) NULL,
        GuestIdentifier NVARCHAR(50) NULL,
        EmergencyContact NVARCHAR(100) NULL,
        EmergencyPhone NVARCHAR(15) NULL,
        Address NVARCHAR(200) NULL,
        CreatedBy INT NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
        IsActive BIT NOT NULL DEFAULT 1,
        CONSTRAINT FK_GuestPatients_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES dbo.Users(UserId)
    );
    PRINT 'Created table dbo.GuestPatients';
END
ELSE
    PRINT 'dbo.GuestPatients already exists';
GO

-- 2) Create indexes on GuestPatients if missing
IF OBJECT_ID('dbo.GuestPatients','U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_GuestPatients_CreatedBy' AND object_id = OBJECT_ID('dbo.GuestPatients'))
    BEGIN
        CREATE INDEX IX_GuestPatients_CreatedBy ON dbo.GuestPatients(CreatedBy);
        PRINT 'Created index IX_GuestPatients_CreatedBy';
    END
    ELSE PRINT 'IX_GuestPatients_CreatedBy exists';

    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_GuestPatients_IsActive' AND object_id = OBJECT_ID('dbo.GuestPatients'))
    BEGIN
        CREATE INDEX IX_GuestPatients_IsActive ON dbo.GuestPatients(IsActive);
        PRINT 'Created index IX_GuestPatients_IsActive';
    END
    ELSE PRINT 'IX_GuestPatients_IsActive exists';
END
GO

-- 3) Add GuestPatientId column to Appointments if missing
IF OBJECT_ID('dbo.Appointments','U') IS NULL
BEGIN
    PRINT 'ERROR: dbo.Appointments does not exist - cannot alter. Please verify schema.';
END
ELSE
BEGIN
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
END
GO

-- 4) Add FK Appointments -> GuestPatients if missing (requires GuestPatients to exist)
IF OBJECT_ID('dbo.Appointments','U') IS NOT NULL AND OBJECT_ID('dbo.GuestPatients','U') IS NOT NULL
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
    PRINT 'FK creation skipped because Appointments or GuestPatients table is missing';
GO

-- 5) Create index on Appointments.GuestPatientId if missing
IF OBJECT_ID('dbo.Appointments','U') IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM sys.indexes WHERE name = 'IX_Appointments_GuestPatientId' AND object_id = OBJECT_ID('dbo.Appointments'))
    BEGIN
        CREATE INDEX IX_Appointments_GuestPatientId ON dbo.Appointments(GuestPatientId);
        PRINT 'Created index IX_Appointments_GuestPatientId';
    END
    ELSE PRINT 'IX_Appointments_GuestPatientId exists';
END
GO

-- 6) Add check constraint to ensure either PatientId or GuestPatientId is set (xor) if missing
IF OBJECT_ID('dbo.Appointments','U') IS NOT NULL
BEGIN
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
        PRINT 'Added CK_Appointments_PatientType';
    END
    ELSE PRINT 'CK_Appointments_PatientType exists';
END
GO

-- 7) Insert minimal sample guest patients only if none exist and an active provider exists
IF OBJECT_ID('dbo.GuestPatients','U') IS NOT NULL
BEGIN
    DECLARE @provId INT = (SELECT TOP 1 UserId FROM dbo.Users WHERE RoleId = 2 AND IsActive = 1);
    IF @provId IS NOT NULL AND NOT EXISTS (SELECT 1 FROM dbo.GuestPatients)
    BEGIN   
        INSERT INTO dbo.GuestPatients (FullName, PhoneNumber, EmergencyStatus, GuestIdentifier, EmergencyContact, EmergencyPhone, Address, CreatedBy)
        VALUES
            ('John Doe', '+26612345678', 0, 'JD001', 'Jane Doe', '+26612345679', '123 Main Street, Maseru', @provId),
            ('Sarah Wilson', '+26687654321', 1, 'SW002', 'Mike Wilson', '+26687654322', '456 Oak Avenue, Maseru', @provId),
            ('David Brown', '+26655555555', 0, 'DB003', NULL, NULL, '789 Pine Road, Maseru', @provId);
        PRINT 'Inserted sample guest patients';
    END
    ELSE IF @provId IS NULL
        PRINT 'No active provider found; sample guest insert skipped';
    ELSE
        PRINT 'Guest sample data already present';
END
GO

PRINT 'Guest patient schema and seed block completed. If you still see Msg 207, close/reopen query windows or refresh schema and re-run the population batch.';
GO

-- ==================
-- FIX 1:  Add the EmergencyCode column
-- ==================

-- Use the project database
USE BothoClinicDB;
GO

-- Add EmergencyCode column to Appointments table
ALTER TABLE Appointments
ADD EmergencyCode NVARCHAR(50) NULL;

-- Create index for performance
CREATE INDEX IX_Appointments_EmergencyCode 
ON Appointments(EmergencyCode);
