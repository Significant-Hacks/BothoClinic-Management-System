using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using BothoClinic.Models;

namespace BothoClinic
{
    public partial class frmStudentDashboard : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        private readonly User _currentUser;
        private int _patientId;

        public frmStudentDashboard(User currentUser)
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error in InitializeComponent: " + ex.ToString());
            }
            _currentUser = currentUser;
            this.Text = $"Student Dashboard - Welcome, {_currentUser.FullName}";
            
            // Initialize profile info in sidebar
            InitializeStudentProfile();
            
            // Wire up event handlers
            this.btnNavDashboard.Click += new System.EventHandler(this.btnNavDashboard_Click);
            this.btnNavBookAppointment.Click += new System.EventHandler(this.btnNavBookAppointment_Click);
            this.btnNavAppointmentHistory.Click += new System.EventHandler(this.btnNavAppointmentHistory_Click);
            this.btnNavProfile.Click += new System.EventHandler(this.btnNavProfile_Click);
            this.btnNavNotifications.Click += new System.EventHandler(this.btnNavNotifications_Click);
            this.btnNavSettings.Click += new System.EventHandler(this.btnNavSettings_Click);
            this.btnNavLogout.Click += new System.EventHandler(this.btnNavLogout_Click);
            
            // Book Appointment Tab Events
            this.btnBookAppointment.Click += new System.EventHandler(this.btnBookAppointment_Click);
            this.btnCancelBooking.Click += new System.EventHandler(this.btnCancelBooking_Click);
            this.dtpAppointmentDate.ValueChanged += new System.EventHandler(this.dtpAppointmentDate_ValueChanged);
            
            // Appointment History Events
            this.btnRefreshHistory.Click += new System.EventHandler(this.btnRefreshHistory_Click);
            
            // Profile Events
            this.btnEditProfile.Click += new System.EventHandler(this.btnEditProfile_Click);
            this.btnChangePassword.Click += new System.EventHandler(this.btnChangePassword_Click);
            this.btnSaveProfile.Click += new System.EventHandler(this.btnSaveProfile_Click);
            
            // Notifications Events
            this.btnMarkAsRead.Click += new System.EventHandler(this.btnMarkAsRead_Click);
            this.btnRefreshNotifications.Click += new System.EventHandler(this.btnRefreshNotifications_Click);
            
            // Settings Events
            this.btnSaveSettings.Click += new System.EventHandler(this.btnSaveSettings_Click);
        }

        private void InitializeStudentProfile()
        {
            try
            {
                // Generate initials from name
                string initials = "";
                if (!string.IsNullOrWhiteSpace(_currentUser.FullName))
                {
                    string[] parts = _currentUser.FullName.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length > 0) initials += parts[0][0];
                    if (parts.Length > 1) initials += parts[parts.Length - 1][0];
                }
                initials = initials.ToUpperInvariant();

                if (lblAvatar != null) lblAvatar.Text = string.IsNullOrEmpty(initials) ? "S" : initials;

                // Name
                string displayName = string.IsNullOrWhiteSpace(_currentUser.FullName)
                    ? $"{_currentUser.FirstName} {_currentUser.LastName}".Trim()
                    : _currentUser.FullName;
                if (string.IsNullOrWhiteSpace(displayName)) displayName = _currentUser.Username;
                if (lblStudentName != null) lblStudentName.Text = displayName;

                // Role
                if (lblStudentRole != null) lblStudentRole.Text = "Student";
            }
            catch (Exception ex)
            {
                // If profile initialization fails, silently continue
                System.Diagnostics.Debug.WriteLine($"Error initializing student profile: {ex.Message}");
            }
        }

        private void FrmStudentDashboard_Load(object sender, EventArgs e)
        {
            try
            {
                GetPatientId();
                InitializeData();
                LoadDashboardData();
                LoadAppointmentHistory();
                LoadProfileData();
                LoadNotifications();
                LoadSettings();
                InitializeTimeSlots(); // Initialize time slots on form load
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetPatientId()
        {
            try
            {
                string sql = "SELECT PatientId FROM Patients WHERE UserId = @UserId";
                object result = dbHelper.ExecuteScalar(sql, new { UserId = _currentUser.UserID });
                if (result != null)
                {
                    _patientId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting patient ID: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void InitializeData()
        {
            LoadDashboardData();
        }

        // ==================== DASHBOARD TAB ====================
        private void LoadDashboardData()
        {
            LoadNextAppointment();
            LoadHealthReminders();
        }

        private void LoadNextAppointment()
        {
            try
            {
                string sql = @"
                    SELECT TOP 1 a.AppointmentId, a.AppointmentDate, a.TimeSlot, u.FullName as ProviderName, a.Reason, a.Status
                    FROM Appointments a
                    LEFT JOIN Users u ON a.ProviderId = u.UserID
                    WHERE a.PatientId = @PatientId AND a.AppointmentDate >= CAST(GETDATE() AS DATE)
                    ORDER BY a.AppointmentDate, a.TimeSlot";
                
                DataTable dt = dbHelper.ExecuteQuery(sql, new { PatientId = _patientId });

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    lblNextApptDateValue.Text = Convert.ToDateTime(row["AppointmentDate"]).ToShortDateString();
                    lblNextApptTimeValue.Text = row["TimeSlot"].ToString();
                    lblNextApptProviderValue.Text = row["ProviderName"] != DBNull.Value ? row["ProviderName"].ToString() : "Not Assigned";
                    lblNextApptReasonValue.Text = row["Reason"].ToString();
                    lblNextApptStatusValue.Text = row["Status"].ToString();
                    
                    // Color code status
                    switch (row["Status"].ToString())
                    {
                        case "Completed":
                            lblNextApptStatusValue.ForeColor = System.Drawing.Color.Green;
                            break;
                        case "Cancelled":
                            lblNextApptStatusValue.ForeColor = System.Drawing.Color.Red;
                            break;
                        case "Scheduled":
                            lblNextApptStatusValue.ForeColor = System.Drawing.Color.Orange;
                            break;
                        case "In Progress":
                            lblNextApptStatusValue.ForeColor = System.Drawing.Color.Blue;
                            break;
                        default:
                            lblNextApptStatusValue.ForeColor = System.Drawing.Color.Black;
                            break;
                    }
                }
                else
                {
                    lblNextApptDateValue.Text = "No upcoming appointments";
                    lblNextApptTimeValue.Text = "N/A";
                    lblNextApptProviderValue.Text = "N/A";
                    lblNextApptReasonValue.Text = "N/A";
                    lblNextApptStatusValue.Text = "N/A";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading next appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHealthReminders()
        {
            try
            {
                string sql = @"
                    SELECT ReminderID, Message, ReminderDate, IsRead
                    FROM Reminders
                    WHERE PatientID = @PatientId
                    ORDER BY ReminderDate DESC";

                DataTable dt = dbHelper.ExecuteQuery(sql, new { PatientId = _patientId });

                lbReminders.Items.Clear();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string reminder = row["Message"].ToString();
                        if (Convert.ToBoolean(row["IsRead"]) == false)
                        {
                            reminder = "● " + reminder; // Bullet for unread
                        }
                        lbReminders.Items.Add(reminder);
                    }
                }
                else
                {
                    lbReminders.Items.Add("No reminders at this time.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading health reminders: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== BOOK APPOINTMENT TAB ====================
        private void btnBookAppointment_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate inputs
                if (dtpAppointmentDate.Value.Date < DateTime.Now.Date)
                {
                    MessageBox.Show("Please select a future date.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbTimeSlot.SelectedItem == null)
                {
                    MessageBox.Show("Please select a time slot.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtAppointmentReason.Text))
                {
                    MessageBox.Show("Please enter a reason for the appointment.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Parse the selected time slot (format: "9:00 AM", "9:30 AM", etc.)
                string selectedTime = cmbTimeSlot.SelectedItem.ToString();
                if (selectedTime == "No available slots for this date")
                {
                    MessageBox.Show("No available time slots for the selected date. Please choose a different date.", "No Slots Available", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                if (!DateTime.TryParse(selectedTime, out DateTime timeDateTime))
                {
                    MessageBox.Show("Invalid time format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                
                TimeSpan timeSlot = timeDateTime.TimeOfDay;

                // Create unassigned appointment (no auto-assignment)
                string sql = @"
                    INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime)
                    VALUES (@PatientId, @ProviderId, @AppointmentDate, @TimeSlot, @Reason, @Status, GETDATE())";

                int rowsAffected = dbHelper.ExecuteNonQuery(sql, new
                {
                    PatientId = _patientId,
                    ProviderId = (object)DBNull.Value, // KEY CHANGE: NULL instead of auto-assignment
                    AppointmentDate = dtpAppointmentDate.Value.Date,
                    TimeSlot = timeSlot,
                    Reason = txtAppointmentReason.Text,
                    Status = "Scheduled" // Status: "Scheduled" = waiting for provider assignment
                });

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Appointment booked successfully! A provider will be assigned soon.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearBookingForm();
                    LoadDashboardData(); // Refresh dashboard to show new appointment
                    LoadAppointmentHistory(); // Refresh to show the new appointment
                }
                else
                {
                    MessageBox.Show("Failed to book appointment.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error booking appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            ClearBookingForm();
        }

        private void dtpAppointmentDate_ValueChanged(object sender, EventArgs e)
        {
            // Refresh time slots when date changes
            InitializeTimeSlots();
        }

        private void ClearBookingForm()
        {
            dtpAppointmentDate.Value = DateTime.Now.AddDays(1);
            cmbTimeSlot.SelectedIndex = -1;
            txtAppointmentReason.Clear();
        }

        // ==================== APPOINTMENT HISTORY TAB ====================
        private void LoadAppointmentHistory()
        {
            try
            {
                string sql = @"
                    SELECT 
                        a.AppointmentId,
                        a.AppointmentDate,
                        a.TimeSlot,
                        ISNULL(u.FullName, 'Not Assigned') as ProviderName,
                        a.Reason,
                        a.Status,
                        ISNULL(c.DiagnosisNotes, 'No notes') as Diagnosis
                    FROM Appointments a
                    LEFT JOIN Users u ON a.ProviderId = u.UserID
                    LEFT JOIN Consultations c ON a.AppointmentId = c.AppointmentId
                    WHERE a.PatientId = @PatientId
                    ORDER BY a.AppointmentDate DESC";

                DataTable dt = dbHelper.ExecuteQuery(sql, new { PatientId = _patientId });
                dgvAppointmentHistory.DataSource = dt;
                
                // Format columns
                if (dgvAppointmentHistory.Columns.Count > 0)
                {
                    dgvAppointmentHistory.Columns["AppointmentId"].Visible = false;
                    dgvAppointmentHistory.Columns["AppointmentDate"].HeaderText = "Date";
                    dgvAppointmentHistory.Columns["TimeSlot"].HeaderText = "Time";
                    dgvAppointmentHistory.Columns["ProviderName"].HeaderText = "Provider";
                    dgvAppointmentHistory.Columns["Reason"].HeaderText = "Reason";
                    dgvAppointmentHistory.Columns["Status"].HeaderText = "Status";
                    dgvAppointmentHistory.Columns["Diagnosis"].HeaderText = "Diagnosis";
                    
                    dgvAppointmentHistory.AutoResizeColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointment history: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefreshHistory_Click(object sender, EventArgs e)
        {
            LoadAppointmentHistory();
            MessageBox.Show("Appointment history refreshed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==================== PROFILE TAB ====================
        private void LoadProfileData()
        {
            try
            {
                string sql = @"
                    SELECT u.FullName, u.ContactEmail, u.ContactPhone, p.StudentId, p.DOB, p.Gender, p.BloodType
                    FROM Users u
                    LEFT JOIN Patients p ON u.UserID = p.UserId
                    WHERE u.UserID = @UserId";

                DataTable dt = dbHelper.ExecuteQuery(sql, new { UserId = _currentUser.UserID });

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtProfileFullName.Text = row["FullName"].ToString();
                    txtProfileEmail.Text = row["ContactEmail"].ToString();
                    txtProfilePhone.Text = row["ContactPhone"].ToString();
                    txtStudentId.Text = row["StudentId"] != DBNull.Value ? row["StudentId"].ToString() : "";
                    txtDOB.Text = row["DOB"] != DBNull.Value ? Convert.ToDateTime(row["DOB"]).ToShortDateString() : "";
                    txtGender.Text = row["Gender"] != DBNull.Value ? row["Gender"].ToString() : "";
                    txtBloodType.Text = row["BloodType"] != DBNull.Value ? row["BloodType"].ToString() : "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading profile: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnEditProfile_Click(object sender, EventArgs e)
        {
            txtProfileEmail.ReadOnly = false;
            txtProfilePhone.ReadOnly = false;
            btnSaveProfile.Enabled = true;
            MessageBox.Show("Profile is now editable. Update your email and phone, then click Save.", "Edit Mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSaveProfile_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtProfileEmail.Text))
                {
                    MessageBox.Show("Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtProfilePhone.Text))
                {
                    MessageBox.Show("Phone cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get old values for audit logging
                string getOldValuesSql = "SELECT ContactEmail, ContactPhone FROM Users WHERE UserID = @UserId";
                System.Data.DataTable oldData = dbHelper.ExecuteQuery(getOldValuesSql, new { UserId = _currentUser.UserID });
                
                string oldEmail = oldData.Rows.Count > 0 ? oldData.Rows[0]["ContactEmail"]?.ToString() ?? "" : "";
                string oldPhone = oldData.Rows.Count > 0 ? oldData.Rows[0]["ContactPhone"]?.ToString() ?? "" : "";

                string sql = @"
                    UPDATE Users 
                    SET ContactEmail = @Email, ContactPhone = @Phone
                    WHERE UserID = @UserId";

                int rowsAffected = dbHelper.ExecuteNonQuery(sql, new
                {
                    Email = txtProfileEmail.Text,
                    Phone = txtProfilePhone.Text,
                    UserId = _currentUser.UserID
                });

                if (rowsAffected > 0)
                {
                    // Log the audit trail
                    string oldValues = $"{{\"Email\":\"{oldEmail}\",\"Phone\":\"{oldPhone}\"}}";
                    string newValues = $"{{\"Email\":\"{txtProfileEmail.Text}\",\"Phone\":\"{txtProfilePhone.Text}\"}}";
                    
                    dbHelper.LogAudit(
                        userId: _currentUser.UserID,
                        action: "UPDATE",
                        tableName: "Users",
                        recordId: _currentUser.UserID.ToString(),
                        oldValues: oldValues,
                        newValues: newValues,
                        details: "Student updated their profile information"
                    );

                    MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtProfileEmail.ReadOnly = true;
                    txtProfilePhone.ReadOnly = true;
                    btnSaveProfile.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Failed to update profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving profile: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                string newPassword = PromptForPassword("Enter new password:");
                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show("Password change cancelled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (newPassword.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SecurityHelper.CreatePasswordHash(newPassword, out string hash, out string salt);

                string sql = "UPDATE Users SET PasswordHash = @Hash, PasswordSalt = @Salt WHERE UserID = @UserId";
                int rowsAffected = dbHelper.ExecuteNonQuery(sql, new
                {
                    Hash = hash,
                    Salt = salt,
                    UserId = _currentUser.UserID
                });

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Password changed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Failed to change password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string PromptForPassword(string prompt)
        {
            Form promptForm = new Form()
            {
                Text = "Password",
                Width = 300,
                Height = 150,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label label = new Label() { Left = 20, Top = 20, Text = prompt, Width = 250 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 250, PasswordChar = '*' };
            Button okButton = new Button() { Text = "OK", Left = 120, Width = 100, Top = 80, DialogResult = DialogResult.OK };
            Button cancelButton = new Button() { Text = "Cancel", Left = 220, Width = 50, Top = 80, DialogResult = DialogResult.Cancel };

            promptForm.Controls.Add(label);
            promptForm.Controls.Add(textBox);
            promptForm.Controls.Add(okButton);
            promptForm.Controls.Add(cancelButton);
            promptForm.AcceptButton = okButton;
            promptForm.CancelButton = cancelButton;

            return promptForm.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }

        // ==================== NOTIFICATIONS TAB ====================
        private void LoadNotifications()
        {
            try
            {
                string sql = @"
                    SELECT ReminderID, Message, ReminderDate, IsRead
                    FROM Reminders
                    WHERE PatientID = @PatientId
                    ORDER BY ReminderDate DESC";

                DataTable dt = dbHelper.ExecuteQuery(sql, new { PatientId = _patientId });
                dgvNotifications.DataSource = dt;

                if (dgvNotifications.Columns.Count > 0)
                {
                    dgvNotifications.Columns["ReminderID"].Visible = false;
                    dgvNotifications.Columns["Message"].HeaderText = "Message";
                    dgvNotifications.Columns["ReminderDate"].HeaderText = "Date";
                    dgvNotifications.Columns["IsRead"].HeaderText = "Read";
                    dgvNotifications.AutoResizeColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notifications: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMarkAsRead_Click(object sender, EventArgs e)
        {
            if (dgvNotifications.SelectedRows.Count > 0)
            {
                try
                {
                    int reminderId = Convert.ToInt32(dgvNotifications.SelectedRows[0].Cells["ReminderID"].Value);
                    string sql = "UPDATE Reminders SET IsRead = 1 WHERE ReminderID = @ReminderId";
                    dbHelper.ExecuteNonQuery(sql, new { ReminderId = reminderId });
                    LoadNotifications();
                    MessageBox.Show("Notification marked as read.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error marking notification: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a notification.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnRefreshNotifications_Click(object sender, EventArgs e)
        {
            LoadNotifications();
            MessageBox.Show("Notifications refreshed.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==================== SETTINGS TAB ====================
        private void LoadSettings()
        {
            try
            {
                chkEmailNotifications.Checked = true;
                chkSMSNotifications.Checked = false;
                chkAppNotifications.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Settings saved successfully!\n\n" +
                    $"Email Notifications: {(chkEmailNotifications.Checked ? "Enabled" : "Disabled")}\n" +
                    $"SMS Notifications: {(chkSMSNotifications.Checked ? "Enabled" : "Disabled")}\n" +
                    $"In-App Notifications: {(chkAppNotifications.Checked ? "Enabled" : "Disabled")}", 
                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving settings: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== NAVIGATION ====================
        private void btnNavDashboard_Click(object sender, EventArgs e)
        {
            SwitchTab(tpDashboard);
            LoadDashboardData();
        }

        private void btnNavBookAppointment_Click(object sender, EventArgs e)
        {
            SwitchTab(tpBookAppointment);
            InitializeTimeSlots();
        }

        private void btnNavAppointmentHistory_Click(object sender, EventArgs e)
        {
            SwitchTab(tpAppointmentHistory);
            LoadAppointmentHistory();
        }

        private void btnNavProfile_Click(object sender, EventArgs e)
        {
            SwitchTab(tpProfile);
            LoadProfileData();
        }

        private void btnNavNotifications_Click(object sender, EventArgs e)
        {
            SwitchTab(tpNotifications);
            LoadNotifications();
        }

        private void btnNavSettings_Click(object sender, EventArgs e)
        {
            SwitchTab(tpSettings);
            LoadSettings();
        }

        private void SwitchTab(TabPage tabPage)
        {
            tabControl.SelectedTab = tabPage;
        }

        private void btnNavLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to logout?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                ((frmMainMDI)this.MdiParent).Logout();
            }
        }

        // ==================== HELPER METHODS ====================
        private void InitializeTimeSlots()
        {
            try
            {
                cmbTimeSlot.Items.Clear();
                
                // Get selected date (default to tomorrow if not set)
                DateTime selectedDate = dtpAppointmentDate?.Value ?? DateTime.Now.AddDays(1);
                
                // Generate available time slots based on actual availability
                LoadAvailableTimeSlots(selectedDate);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing time slots: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Fallback to basic time slots if there's an error
                LoadBasicTimeSlots();
            }
        }

        private void LoadAvailableTimeSlots(DateTime selectedDate)
        {
            try
            {
                // Check if ANY time slot has space (not provider-specific)
                string sql = @"
                    SELECT TimeSlot, COUNT(*) as AppointmentCount
                    FROM Appointments 
                    WHERE AppointmentDate = @AppointmentDate 
                    AND Status IN ('Scheduled', 'In Progress')
                    GROUP BY TimeSlot
                    HAVING COUNT(*) < @MaxAppointmentsPerSlot";
                
                DataTable occupiedSlots = dbHelper.ExecuteQuery(sql, new { 
                    AppointmentDate = selectedDate.Date,
                    MaxAppointmentsPerSlot = 3 // Allow multiple appointments per time slot
                });
                
                // Generate available slots (9:00 AM to 5:00 PM, every 30 minutes)
                var availableSlots = new List<string>();
                for (int hour = 9; hour < 17; hour++)
                {
                    for (int minute = 0; minute < 60; minute += 30)
                    {
                        TimeSpan timeSlot = new TimeSpan(hour, minute, 0);
                        
                        // Check if this slot has space
                        bool hasSpace = true;
                        foreach (DataRow row in occupiedSlots.Rows)
                        {
                            if (TimeSpan.TryParse(row["TimeSlot"].ToString(), out TimeSpan existingSlot) && 
                                existingSlot == timeSlot)
                            {
                                int count = Convert.ToInt32(row["AppointmentCount"]);
                                if (count >= 3) // Max 3 appointments per slot
                                {
                                    hasSpace = false;
                                    break;
                                }
                            }
                        }
                        
                        if (hasSpace)
                        {
                            DateTime time = DateTime.Today.Add(timeSlot);
                            availableSlots.Add(time.ToString("h:mm tt"));
                        }
                    }
                }
                
                if (availableSlots.Count > 0)
                {
                    cmbTimeSlot.Items.AddRange(availableSlots.ToArray());
                }
                else
                {
                    cmbTimeSlot.Items.Add("No available slots for this date");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading available time slots: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadBasicTimeSlots();
            }
        }

        private void LoadBasicTimeSlots()
        {
            // Fallback method with basic time slots
            cmbTimeSlot.Items.Clear();
            for (int i = 9; i < 17; i++)
            {
                DateTime time = DateTime.Today.AddHours(i);
                cmbTimeSlot.Items.Add(time.ToString("h:mm tt"));
                
                time = DateTime.Today.AddHours(i).AddMinutes(30);
                cmbTimeSlot.Items.Add(time.ToString("h:mm tt"));
            }
        }

    }
}
