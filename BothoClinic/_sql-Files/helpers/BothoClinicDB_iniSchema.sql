-- Botho University Campus Clinic Management System
-- C7-PN1-11 Project: Phase 1 - Database Design and SQL Script
-- Schema designed to comply with 3NF and security requirements.


-- Use project database
USE BothoClinicDB;
GO

-----------------------------------------------------------
-- 1. ROLES Table (Primary Key: RoleId)
-- Holds user roles (Admin, Provider, Student).
-----------------------------------------------------------
CREATE TABLE Roles (
    RoleId INT PRIMARY KEY IDENTITY(1,1),
    RoleName VARCHAR(50) NOT NULL UNIQUE,
    Description VARCHAR(255)
);
GO

-----------------------------------------------------------
-- 2. USERS Table (Security Critical - PK: UserId, FK: RoleId)
-- Includes PasswordHash, Salt, and MustChangePassword for security.
-----------------------------------------------------------
CREATE TABLE Users (
    UserId INT PRIMARY KEY IDENTITY(1,1),
    Username VARCHAR(50) NOT NULL UNIQUE,
    PasswordHash VARCHAR(255) NOT NULL, -- To store hashed password
    Salt VARCHAR(100) NOT NULL,         -- To store the unique salt used for hashing
    FullName VARCHAR(100) NOT NULL,
    ContactEmail VARCHAR(100) UNIQUE,
    ContactPhone VARCHAR(20),
    RoleId INT NOT NULL,
    IsActive BIT DEFAULT 1,
    MustChangePassword BIT DEFAULT 1, -- Forces password change on first successful login
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId)
);
GO

-----------------------------------------------------------
-- 3. PATIENTS Table (FK: UserId)
-- Holds specific patient data, separate from general user login data.
-----------------------------------------------------------
CREATE TABLE Patients (
    PatientId INT PRIMARY KEY IDENTITY(1,1),
    UserId INT NOT NULL UNIQUE, -- Link to the Users table (FK)
    StudentId VARCHAR(20) NOT NULL UNIQUE, -- Botho specific student ID
    DOB DATE NOT NULL,
    Gender VARCHAR(10) NOT NULL,
    BloodType VARCHAR(5),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);
GO

-----------------------------------------------------------
-- 4. MEDICATIONS Table (Optional master table, highly recommended for lookup/collections)
-----------------------------------------------------------
CREATE TABLE Medications (
    MedicationId INT PRIMARY KEY IDENTITY(1,1),
    MedicationName VARCHAR(100) NOT NULL UNIQUE,
    Description VARCHAR(255)
);
GO

-----------------------------------------------------------
-- 5. APPOINTMENTS Table (PK: AppointmentId, FKs: PatientId, ProviderId)
-- Stores scheduled appointments.
-----------------------------------------------------------
CREATE TABLE Appointments (
    AppointmentId INT PRIMARY KEY IDENTITY(1,1),
    PatientId INT NOT NULL,
    ProviderId INT, -- Can be NULL until assigned by a Provider user
    AppointmentDate DATE NOT NULL,
    TimeSlot TIME NOT NULL,
    Reason VARCHAR(255) NOT NULL,
    Status VARCHAR(50) NOT NULL CHECK (Status IN ('Booked', 'Confirmed', 'Cancelled', 'Completed')),
    CreatedDateTime DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (PatientId) REFERENCES Patients(PatientId),
    -- ProviderId FK references a UserId with RoleId=2 (Provider)
    FOREIGN KEY (ProviderId) REFERENCES Users(UserId)
);
GO

-----------------------------------------------------------
-- 6. CONSULTATIONS Table (PK: ConsultationId, FK: AppointmentId, ProviderId)
-- Stores the actual clinical encounter notes.
-----------------------------------------------------------
CREATE TABLE Consultations (
    ConsultationId INT PRIMARY KEY IDENTITY(1,1),
    AppointmentId INT NOT NULL UNIQUE, -- One consultation per appointment
    ProviderId INT NOT NULL,
    ConsultationDate DATETIME DEFAULT GETDATE(),
    Temperature DECIMAL(4,1),
    BloodPressure VARCHAR(10),
    VitalsJson NVARCHAR(MAX), -- JSON for flexible storage of other vitals (e.g., height/weight, SpO2)
    DiagnosisNotes NVARCHAR(MAX) NOT NULL, -- Clinical notes and diagnosis
    FollowUpNeeded BIT DEFAULT 0,
    FOREIGN KEY (AppointmentId) REFERENCES Appointments(AppointmentId),
    FOREIGN KEY (ProviderId) REFERENCES Users(UserId)
);
GO

-----------------------------------------------------------
-- 7. PRESCRIPTIONS Table (FK: ConsultationId, MedicationId)
-- Records dispensed or prescribed medications.
-----------------------------------------------------------
CREATE TABLE Prescriptions (
    PrescriptionId INT PRIMARY KEY IDENTITY(1,1),
    ConsultationId INT NOT NULL,
    MedicationId INT NOT NULL,
    Dosage VARCHAR(50) NOT NULL,
    Quantity INT NOT NULL,
    Instructions NVARCHAR(MAX),
    DispensedDate DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (ConsultationId) REFERENCES Consultations(ConsultationId),
    FOREIGN KEY (MedicationId) REFERENCES Medications(MedicationId)
);
GO

-----------------------------------------------------------
-- 8. REMINDERS Table (FK: PatientId)
-- Stores health reminders for patients.
-----------------------------------------------------------
CREATE TABLE Reminders (
    ReminderID INT PRIMARY KEY IDENTITY(1,1),
    PatientID INT NOT NULL,
    Message NVARCHAR(500) NOT NULL,
    ReminderDate DATETIME NOT NULL,
    IsRead BIT DEFAULT 0,
    FOREIGN KEY (PatientID) REFERENCES Patients(PatientID)
);
GO

-----------------------------------------------------------
-- 9. INDEXES for Performance (Advanced Implementation)
-----------------------------------------------------------
CREATE UNIQUE INDEX IX_Users_Username ON Users(Username);
CREATE INDEX IX_Appointments_PatientDate ON Appointments(PatientId, AppointmentDate);
CREATE INDEX IX_Appointments_ProviderDate ON Appointments(ProviderId, AppointmentDate);
CREATE INDEX IX_Reminders_PatientID ON Reminders(PatientID);
GO

-----------------------------------------------------------
-- 10. SAMPLE DATA INSERTION
-- NOTE: Passwords are not hashed here. The application must hash them on first run or registration.
-- For demonstration, use a placeholder hash (e.g., 'PLACEHOLDER_HASH') and a real salt.
-----------------------------------------------------------

-- Roles
INSERT INTO Roles (RoleName) VALUES ('Administrator'); -- RoleId 1
INSERT INTO Roles (RoleName) VALUES ('Provider');    -- RoleId 2
INSERT INTO Roles (RoleName) VALUES ('Student');     -- RoleId 3

-- Medications (For Provider lookup)
INSERT INTO Medications (MedicationName) VALUES ('Paracetamol 500mg');
INSERT INTO Medications (MedicationName) VALUES ('Amoxicillin 250mg');
INSERT INTO Medications (MedicationName) VALUES ('Loratadine 10mg');
INSERT INTO Medications (MedicationName) VALUES ('Betadine Solution');

-- Users (The application logic will replace these with real hashes upon registration/setup)
-- User 1: Admin
INSERT INTO Users (Username, PasswordHash, Salt, FullName, ContactEmail, RoleId, MustChangePassword)
VALUES ('admin_user', 'pcJtTfwPGX8ng3Dib43LrL+ssWcpFwHN58wDmXM/MKsBnn8Qjof3BkPUYqfJAqbfdEzOmy29HG0ztXCQqeIn4Q==', 'ecfJCGwLz7ylbOVA0qWSj6AtEPMzPibc5Rz3Y5irtE5ls+xB+mMDoWmIdFxT/ZZYL5pf64Twkw3+br6ZsZ22yQ==', 'Mr. Botho Admin', 'admin@bothoclinic.com', 1, 1);
-- User 2: Provider (Doctor)
INSERT INTO Users (Username, PasswordHash, Salt, FullName, ContactEmail, RoleId, MustChangePassword)
VALUES ('dr_sello', 'Bit8W4oCw9pmQ/v4/FHYlxijUvYMiCavju10azSyaKl0GejbqenpHBlSBAd0pHrMdqoJV9MhSZEhg0Hhy3gpkw==', 'RvfYaOy2r8qyBV0+VTOFa/F7VrekQn84FNpWLRaP4MCEmuhYPzzscVLTObvnaI1JPCIu8jCnzIKKO1X5EFhJXw==', 'Dr. Sello Mokhele', 'sello.mokhele@botho.com', 2, 1);
-- User 3: Student (Patient)
INSERT INTO Users (Username, PasswordHash, Salt, FullName, ContactEmail, RoleId, MustChangePassword)
VALUES ('sbu2023001', 'MeQw2j6+x2yYwtiwW/RYPDNz/dfW1h7fK0snjd3YGEGE2cB+ueTrUro96xg89HOlpnC0LDEOz7PDC1LBKKuSMQ==', 'cfkwWvyVp9ULMXjvXOAXOBdhlOlxMXB49vIYUOtBNbPQh/XIpnGSyXoBkHgsQmW1Jp8RygJfcVOFnZHKTllWTA==', 'Lindiwe Noko', 'lindiwe.noko@bothostudent.com', 3, 1);

-- Patients
INSERT INTO Patients (UserId, StudentId, DOB, Gender)
VALUES (3, 'SBU2023001', '2000-05-15', 'Female');

-- Sample Appointments
INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status)
VALUES (1, 2, '2025-10-18', '09:00:00', 'Routine checkup for sports eligibility.', 'Confirmed');
INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status)
VALUES (1, NULL, '2025-10-19', '11:30:00', 'Persistent cough and mild fever.', 'Booked');

-- Sample Consultation (for the first appointment)
INSERT INTO Consultations (AppointmentId, ProviderId, Temperature, BloodPressure, DiagnosisNotes)
VALUES (1, 2, 36.5, '120/80', 'Patient is fit for sports. Advised on hydration and diet.');

-- Sample Prescription
INSERT INTO Prescriptions (ConsultationId, MedicationId, Dosage, Quantity, Instructions)
VALUES (1, 3, '1 tablet', 10, 'Take at bedtime for mild seasonal allergies.');

-- Sample Reminders
INSERT INTO Reminders (PatientID, Message, ReminderDate, IsRead)
VALUES (1, 'Flu shot due next month.', '2025-11-15', 0);
INSERT INTO Reminders (PatientID, Message, ReminderDate, IsRead)
VALUES (1, 'Annual check-up recommended.', '2025-12-01', 0);
GO
