-- Create GuestPatients table for non-student patients who cannot login
CREATE TABLE GuestPatients (
    GuestPatientId INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(100) NOT NULL,
    PhoneNumber NVARCHAR(15) NOT NULL,
    EmergencyContact NVARCHAR(100) NULL,
    EmergencyPhone NVARCHAR(15) NULL,
    Address NVARCHAR(200) NULL,
    CreatedBy INT NOT NULL, -- Provider who created this guest patient
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsActive BIT NOT NULL DEFAULT 1,
    
    -- Foreign key to Users table (Provider who created this guest)
    CONSTRAINT FK_GuestPatients_CreatedBy FOREIGN KEY (CreatedBy) REFERENCES Users(UserId)
);

-- Create index for better performance
CREATE INDEX IX_GuestPatients_CreatedBy ON GuestPatients(CreatedBy);
CREATE INDEX IX_GuestPatients_IsActive ON GuestPatients(IsActive);

-- Update Appointments table to support guest patients
-- Add GuestPatientId column to link appointments to guest patients
IF NOT EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('Appointments') AND name = 'GuestPatientId')
BEGIN
    ALTER TABLE Appointments ADD GuestPatientId INT NULL;
    
    -- Add foreign key constraint
    ALTER TABLE Appointments 
    ADD CONSTRAINT FK_Appointments_GuestPatients 
    FOREIGN KEY (GuestPatientId) REFERENCES GuestPatients(GuestPatientId);
    
    -- Create index for performance
    CREATE INDEX IX_Appointments_GuestPatientId ON Appointments(GuestPatientId);
END

-- Add constraint to ensure either PatientId (student) OR GuestPatientId (guest) is provided, but not both
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE name = 'CK_Appointments_PatientType')
BEGIN
    ALTER TABLE Appointments 
    ADD CONSTRAINT CK_Appointments_PatientType 
    CHECK ((PatientId IS NOT NULL AND GuestPatientId IS NULL) OR (PatientId IS NULL AND GuestPatientId IS NOT NULL));
END

-- Insert sample guest patients for testing
INSERT INTO GuestPatients (FullName, PhoneNumber, EmergencyContact, EmergencyPhone, Address, CreatedBy)
VALUES 
    ('John Doe', '+26612345678', 'Jane Doe', '+26612345679', '123 Main Street, Maseru', 2),
    ('Sarah Wilson', '+26687654321', 'Mike Wilson', '+26687654322', '456 Oak Avenue, Maseru', 2),
    ('David Brown', '+26655555555', NULL, NULL, '789 Pine Road, Maseru', 2);