# Botho Clinic Management System

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![C#](https://img.shields.io/badge/C%23-12.0-239120?logo=csharp)](https://learn.microsoft.com/en-us/dotnet/csharp/)
[![WinForms](https://img.shields.io/badge/WinForms-MDI-512BD4)](https://learn.microsoft.com/en-us/dotnet/desktop/winforms/)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-LocalDB-CC2927?logo=microsoft-sql-server)](https://www.microsoft.com/en-us/sql-server/)
[![License](https://img.shields.io/badge/License-MIT-yellow)](LICENSE)

A comprehensive **Clinic Management System** built for Botho University's campus clinic. Developed as a **.NET module project** while pursuing a **BSc (Hons) Degree in Computing — specialising in Software Engineering** (rated **A+**). Uses Windows Forms (MDI) with role-based dashboards, Chart.js-style analytics, and PBKDF2-SHA512 password security.

---

## Technology Stack

| Layer | Technology |
|-------|-----------|
| **Framework** | .NET 8.0 (Windows Forms) |
| **Language** | C# 12.0 |
| **Architecture** | MDI (Multiple Document Interface) |
| **Database** | SQL Server LocalDB (`(LocalDB)\MSSQLLocalDB`) |
| **ORM / Data** | ADO.NET with parameterized queries (`Microsoft.Data.SqlClient`) |
| **Charts** | `WinForms.DataVisualization` (Column, Pie, Doughnut, Line) |
| **Security** | PBKDF2-SHA512 with random 64-byte salt, 350,000 iterations |
| **Audit** | Automatic audit logging on all INSERT/UPDATE/DELETE operations |

---

## Quick Start

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- SQL Server LocalDB (included with Visual Studio or SQL Server Express)

### Setup
```bash
git clone https://github.com/Significant-Hacks/BothoClinic-Management-System.git
cd BothoClinic-Management-System
```

1. Open `BothoClinic/BothoClinic.sln` in Visual Studio 2022+
2. Restore NuGet packages (right-click solution → Restore NuGet Packages)
3. Run `_sql-Files/BothoClinicDB_setupScript.sql` against `(LocalDB)\MSSQLLocalDB` to create the database
4. Set `BothoClinic` as startup project
5. Press **F5** to build and run

### Default Login
| Role | Username | Password |
|------|----------|----------|
| Administrator | admin | admin |
| Provider | provider | provider |
| Student | student | student |

---

## Feature Completeness

| Module | Status | Details |
|--------|--------|---------|
| **Authentication** | ✅ Complete | Login with PBKDF2-SHA512 verification, forgot password, change password on first login, audit logging |
| **Admin Dashboard** | ✅ Complete | KPI cards (patients today, active users, appointments today, emergency cases), Chart.js-style analytics, recent activity feed with filters |
| **Staff/User Management** | ✅ Complete | Full CRUD (add/edit/delete/activate/deactivate), role assignment, password reset, search/filter by role and status |
| **Patient Management** | ✅ Complete | Registered student patients + guest patients, medical history, full details view |
| **Appointment Booking** | ✅ Complete | Book, reschedule, cancel appointments; provider calendar view; appointment row controls; status tracking |
| **Provider Dashboard** | ✅ Complete | Today's appointments, patient lookup, consultation notes entry, schedule view |
| **Consultations** | ✅ Complete | Create/update consultation records, vitals, diagnosis notes, prescriptions |
| **Prescriptions** | ✅ Complete | Medication management linked to consultations, dosage/frequency/duration/instructions |
| **Student Dashboard** | ✅ Complete | View personal appointments, medical history, prescriptions, profile |
| **Reports & Analytics** | ✅ Complete | Monthly trends, status distribution (pie), provider workload, patient type distribution (doughnut), weekly activity patterns (line); CSV export |
| **Audit Logs** | ✅ Complete | Automatic logging of all data changes with before/after values, search/filter by type and action |
| **System Settings** | ✅ Complete | Save/load settings, database backup, connection test |
| **Security** | ✅ Complete | PBKDF2-SHA512 hashing (350K iterations, 64-byte salt), password reset with force-change flag, audit trail |
| **Guest Patients** | ✅ Complete | Walk-in patient registration with emergency contact, separate from registered students |
| **AuthenticationController** | ❌ Stub | `Controllers/AuthenticationController.cs` — empty with `// TODO: Implement authentication logic` (auth logic is in `frmLogin.cs` instead) |
| **ReportController** | ❌ Stub | `Controllers/ReportController.cs` — empty with `// TODO: Implement report logic` (report logic is in `frmAdminDashboard.cs` instead) |

---

## Architecture

### 1. Main Modules & Responsibilities

| Module | Location | Responsibility |
|--------|----------|---------------|
| **MDI Parent** | `Forms/frmMainMDI.cs` | Application shell, login flow, role-based dashboard routing, logout |
| **Login** | `Forms/frmLogin.cs` | Username/password authentication, PBKDF2 verification, forgot-password flow |
| **Admin Dashboard** | `frmAdminDashboard.cs` | Full admin panel — user management, KPIs, charts, reports, settings, audit logs, staff CRUD |
| **Provider Dashboard** | `frmProviderDashboard.cs` | Provider view — today's appointments, consultations, patient lookup |
| **Student Dashboard** | `Forms/frmStudentDashboard.cs` | Student view — personal appointments, medical history, prescriptions |
| **Controllers** | `Controllers/` | 4 classes — Appointment, Authentication, Consultation, Report (business logic layer) |
| **Models** | `Models/` | 5 POCO classes — User, Patient, Appointment, Consultation, Prescription |
| **Data Access** | `DatabaseHelper.cs` | Core ADO.NET utility — parameterized queries, stored procedures, automatic audit logging |
| **Security** | `SecurityHelper.cs` | PBKDF2-SHA512 password hashing with salt |
| **Session** | `UserSession.cs` | Static session state — tracks current user, login/logout audit |
| **AppointmentRowControl** | `AppointmentRowControl.cs` | Custom UserControl for provider appointment calendar rows |
| **Forms** | `Forms/` | 25+ forms — Booking, Reschedule, Consultation, Prescription, Patient Details, Medical History, Reports, Settings, Audit Logs, Profile, etc. |

### 2. Data Flow

```
Application Start
  ↓
Program.cs → Application.Run(new frmMainMDI())
  ↓
frmMainMDI_Shown → ShowLoginForm()
  ↓
frmLogin (modal dialog)
  ├─ AuthenticateUser() → DatabaseHelper.ExecuteQuery() → Users JOIN Roles
  ├─ SecurityHelper.VerifyPasswordHash() → PBKDF2-SHA512
  ├─ Check MustChangePassword → frmChangePassword
  └─ DialogResult.OK → AuthenticatedUser returned
  ↓
frmMainMDI.HandleLoginSuccessful()
  ├─ UserSession.Login(user) → sets static CurrentUser
  └─ ShowDashboard(roleName) → opens role-specific dashboard as MDI child
       ├─ Administrator → frmAdminDashboard
       ├─ Provider → frmProviderDashboard
       └─ Student → frmStudentDashboard
              ↓
       Dashboard loads KPIs / charts / data grids via DatabaseHelper
              ↓
       All CRUD → DatabaseHelper.ExecuteNonQuery()
         └─ Automatic audit logging (before/after capture)
```

**Concrete Example — Admin creates a new user:**
```
Admin clicks "Add User" on Staff tab
  → frmUserRegistration opens (modal)
  → Admin fills form, clicks Save
  → SecurityHelper.CreatePasswordHash("defaultPwd", out hash, out salt)
  → DatabaseHelper.ExecuteNonQuery("INSERT INTO Users ...", {...})
  → DatabaseHelper automatically:
      1. Executes the INSERT
      2. Captures new values from DB
      3. Logs to AuditLogs table (UserId, Action="INSERT", TableName="Users", ...)
  → frmAdminDashboard.LoadUsers() refreshes the DataGridView
```

### 3. Design Patterns Used

| Pattern | Location | Implementation |
|---------|----------|---------------|
| **MDI (Multiple Document Interface)** | `frmMainMDI.cs` | Single parent form container, child dashboards docked to fill |
| **Controller Pattern** | `Controllers/` | Business logic separated from UI — AppointmentController, ConsultationController, etc. |
| **Active Record / Table Data Gateway** | `DatabaseHelper.cs` | Generic ADO.NET helper with parameterized queries, maps DataTable rows to model objects |
| **Singleton** | `UserSession.cs` | Static `CurrentUser` property — single source of truth for logged-in user state |
| **Strategy / Role-based Routing** | `frmMainMDI.ShowDashboard()` | Routes to different dashboard forms based on `RoleName` string |
| **Template Method** | All Forms | Override `Form_Load`, wire events in constructor pattern |
| **Observer / Audit Trail** | `DatabaseHelper.ExecuteNonQuery()` | Automatic before/after capture + audit logging on all data mutations |
| **Value Object** | `Models/` | POCO classes (User, Patient, Appointment, Consultation, Prescription) |
| **Facade** | `DatabaseHelper` | Single class abstracts all SQL Server interaction (connection, queries, commands, audit) |
| **Strategy (Password Hashing)** | `SecurityHelper` | PBKDF2 iterations and algorithm encapsulated in static methods |

### 4. Database Schema

Key tables (inferred from SQL scripts and C# queries):

```
Roles (RoleID, RoleName)
Users (UserID, RoleID, Username, FullName, ContactEmail, ContactPhone, PasswordHash, Salt, IsActive, MustChangePassword)
Patients (PatientID, UserID, StudentID, DOB, Gender, BloodType)
GuestPatients (GuestPatientID, FullName, PhoneNumber, EmergencyContactName, EmergencyContactPhone)
Appointments (AppointmentID, PatientID, GuestPatientID, ProviderID, AppointmentDate, TimeSlot, Status, Reason, CreatedDateTime)
Consultations (ConsultID, ApptID, ProviderID, Vitals, Notes, Diagnosis, Prescription, ConsultationDate, FollowUpNeeded, DiagnosisNotes)
Prescriptions (PrescID, ConsultID, MedID, Dosage, Frequency, Duration, Instructions)
Medications (MedID, Name, DosageForm, Strength)
AuditLogs (LogID, UserID, Action, TableName, RecordID, OldValues, NewValues, Details, IPAddress, UserAgent, Timestamp)
PasswordResetTokens (TokenID, UserID, Token, ExpiresAt, Used)
```

### 5. Role-Based Access

```
Login → UserSession.CurrentUser
           ↓
    ┌──────┼──────┐
    ↓      ↓      ↓
Administrator  Provider  Student
    ↓          ↓         ↓
Full access    Appointments  View own
User CRUD      Consultations Appointments
Reports        Patient lookup Medical History
Settings       Schedule       Prescriptions
Audit Logs     Prescriptions  Profile
Charts/ KPIs   Medical History
```

---

## Design Patterns Used

| Pattern | Where | How |
|---------|-------|-----|
| **MDI (Multiple Document Interface)** | `frmMainMDI` | Single parent form hosting role-specific dashboards as child forms |
| **Controller** | `Controllers/` | Business logic in dedicated classes (Appointment, Consultation) separated from UI |
| **Singleton** | `UserSession` | Static `CurrentUser` with thread-safe property access, login/logout audit |
| **Active Record** | `DatabaseHelper` | Generic ADO.NET wrapper with parameterized SQL, DataTable mapping |
| **Observer / Audit** | `DatabaseHelper.ExecuteNonQuery()` | Automatic before/after value capture + audit log insertion on all writes |
| **Strategy** | `SecurityHelper` | PBKDF2-SHA512 algorithm with configurable iterations encapsulated in static methods |
| **Value Object** | `Models/` | Simple POCO classes with auto-properties for data transfer |
| **Template Method** | WinForms lifecycle | Override `Form_Load`, wire events via `+=` pattern throughout |

---

## Potential Architectural Issues

### Critical
- **SQL Server LocalDB dependency** — Connection string hardcoded to `(LocalDB)\MSSQLLocalDB`; requires LocalDB installed, which is not available on all machines. Should use a configurable connection string.
- **AuthenticationController and ReportController are stubs** — Both exist with `// TODO: Implement` comments. Authentication logic lives in `frmLogin.cs` and reporting logic in `frmAdminDashboard.cs`, which violates the Controller pattern.
- **No connection pooling configuration** — `DatabaseHelper` opens/closes connections per operation; while `using` blocks ensure disposal, high-traffic scenarios may benefit from explicit pooling config.
- **Audit logging could cause recursion** — `LogAuditToDatabase()` calls `ExecuteNonQueryWithoutAudit()` to avoid recursion, but the design is fragile; a misconfigured audit call could cause a stack overflow.

### Moderate
- **Business logic in UI code-behind** — `frmAdminDashboard.cs` contains ~1000+ lines with KPI loading, chart rendering, user CRUD, settings management, audit browsing, and CSV export — all in one file. This should be decomposed into smaller classes.
- **Hardcoded default password** — `"Password123"` in `btnResetPassword_Click` should be randomized or configurable.
- **No input sanitization on search fields** — TextChanged handlers execute SQL queries immediately, which could be exploited if the app is exposed (though LocalDB is local).
- **No async/await** — All database operations are synchronous; UI freezes during long queries.
- **Sample data mixed with real data in SQL** — `UNION ALL` with sample data fallbacks in chart queries make it hard to distinguish real from fake data at a glance.

### Minor
- `AuthenticationController.cs` and `ReportController.cs` are empty stubs with only TODO comments
- `PasswordGenerator.cs` and `PasswordGenerator.exe` at solution root are not part of the main project
- Error handling shows `MessageBox` for all DB errors — better to have a centralized error handler
- `lblKPITitle2.Text = "Active Users"` — KPI titles are hardcoded in code-behind rather than configurable
- No unit tests present

---

## Repository

- **GitHub**: [github.com/Significant-Hacks/BothoClinic-Management-System](https://github.com/Significant-Hacks/BothoClinic-Management-System)
- **Status**: Complete — rated **A+**
- **Academic context**: .NET module project, BSc (Hons) Computing — Software Engineering specialisation
