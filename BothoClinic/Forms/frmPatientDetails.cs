using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BothoClinic.Models;

namespace BothoClinic
{
    public partial class frmPatientDetails : Form
    {
        private DatabaseHelper dbHelper;
        private User _currentUser;
        private int _patientId;
        private bool _isStudent;
        private DataRow _patientData;
        private User currentUser;
        private bool isUpdatingEmergencyFields = false; // Flag to prevent infinite loops

        // Controls
        private Label lblPatientName;
        private Label lblPatientType;
        private TextBox txtPhone;
        private TextBox txtEmail;
        private TextBox txtDOB;
        private TextBox txtGender;
        private TextBox txtBloodType;
        private TextBox txtAddress;
        private TextBox txtEmergencyContact;
        private TextBox txtEmergencyPhone;
        private TextBox txtEmergencyCode;
        private Button btnGenerateEMGCode;
        private DataGridView dgvAppointmentHistory;
        private DataGridView dgvConsultationHistory;
        private Button btnSave;
        private Button btnClose;

        public frmPatientDetails(int patientId, bool isStudent, User user)
        {
            InitializeComponent();
            _patientId = patientId;
            _isStudent = isStudent;
            _currentUser = user;
            currentUser = user; // Also store in this field for compatibility
            dbHelper = new DatabaseHelper();
            SetupForm();
            LoadPatientDetails();
        }

        private void SetupForm()
        {
            this.Text = "Patient Details";
            this.Size = new Size(950, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            // Header Panel
            var headerPanel = new Panel()
            {
                Height = 60,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(52, 152, 219),
                Padding = new Padding(20)
            };

            var headerLabel = new Label()
            {
                Text = "Patient Details",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 18)
            };

            headerPanel.Controls.Add(headerLabel);

            // Main Panel with Scroll
            var mainPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                AutoScroll = true
            };

            int yPos = 20;

            // Patient Name and Type
            var lblNameLabel = new Label()
            {
                Text = "Patient Name:",
                Location = new Point(20, yPos),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            lblPatientName = new Label()
            {
                Location = new Point(150, yPos),
                Size = new Size(250, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            lblPatientType = new Label()
            {
                Location = new Point(410, yPos),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.Gray
            };

            yPos += 35;

            // Phone
            var lblPhone = new Label()
            {
                Text = "Phone Number:",
                Location = new Point(20, yPos),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            txtPhone = new TextBox()
            {
                Location = new Point(150, yPos),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 10F),
                Enabled = true // Allow editing
            };

            yPos += 40;

            // Email (for students)
            if (_isStudent)
            {
                var lblEmail = new Label()
                {
                    Text = "Email:",
                    Location = new Point(20, yPos),
                    Size = new Size(120, 25),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };

                txtEmail = new TextBox()
                {
                    Location = new Point(150, yPos),
                    Size = new Size(250, 30),
                    Font = new Font("Segoe UI", 10F),
                    Enabled = true // Allow editing
                };

                mainPanel.Controls.Add(lblEmail);
                mainPanel.Controls.Add(txtEmail);

                yPos += 40;
            }

            // DOB (for students)
            if (_isStudent)
            {
                var lblDOB = new Label()
                {
                    Text = "Date of Birth:",
                    Location = new Point(20, yPos),
                    Size = new Size(120, 25),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };

                txtDOB = new TextBox()
                {
                    Location = new Point(150, yPos),
                    Size = new Size(250, 30),
                    Font = new Font("Segoe UI", 10F),
                    Enabled = false
                };

                mainPanel.Controls.Add(lblDOB);
                mainPanel.Controls.Add(txtDOB);

                yPos += 40;

                // Gender
                var lblGender = new Label()
                {
                    Text = "Gender:",
                    Location = new Point(20, yPos),
                    Size = new Size(120, 25),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };

                txtGender = new TextBox()
                {
                    Location = new Point(150, yPos),
                    Size = new Size(250, 30),
                    Font = new Font("Segoe UI", 10F),
                    Enabled = false
                };

                mainPanel.Controls.Add(lblGender);
                mainPanel.Controls.Add(txtGender);

                yPos += 40;

                // Blood Type
                var lblBloodType = new Label()
                {
                    Text = "Blood Type:",
                    Location = new Point(20, yPos),
                    Size = new Size(120, 25),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };

                txtBloodType = new TextBox()
                {
                    Location = new Point(150, yPos),
                    Size = new Size(100, 30),
                    Font = new Font("Segoe UI", 10F),
                    Enabled = false
                };

                mainPanel.Controls.Add(lblBloodType);
                mainPanel.Controls.Add(txtBloodType);

                yPos += 40;
            }

            // Address (for guests or both)
            var lblAddress = new Label()
            {
                Text = "Address:",
                Location = new Point(20, yPos),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            txtAddress = new TextBox()
            {
                Location = new Point(150, yPos),
                Size = new Size(600, 30),
                Font = new Font("Segoe UI", 10F),
                Enabled = true, // Allow editing
                Visible = !_isStudent // Only visible for guests
            };

            yPos += 40;

            // Emergency Contact (for guests)
            if (!_isStudent)
            {
                var lblEmergencyContact = new Label()
                {
                    Text = "EMG. Contact:",
                    Location = new Point(20, yPos),
                    Size = new Size(120, 25),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };

            txtEmergencyContact = new TextBox()
            {
                Location = new Point(150, yPos),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 10F),
                Enabled = true // Allow editing
            };

                txtEmergencyContact.TextChanged += TxtEmergencyContact_TextChanged;

                mainPanel.Controls.Add(lblEmergencyContact);
                mainPanel.Controls.Add(txtEmergencyContact);

                yPos += 40;

                var lblEmergencyPhone = new Label()
                {
                    Text = "EMG. Number:",
                    Location = new Point(20, yPos),
                    Size = new Size(120, 25),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };

                txtEmergencyPhone = new TextBox()
                {
                    Location = new Point(150, yPos),
                    Size = new Size(250, 30),
                    Font = new Font("Segoe UI", 10F),
                    Enabled = true // Allow editing
                };

                txtEmergencyPhone.TextChanged += TxtEmergencyPhone_TextChanged;

                mainPanel.Controls.Add(lblEmergencyPhone);
                mainPanel.Controls.Add(txtEmergencyPhone);

                yPos += 40;

                // EMG. Code field
                var lblEmergencyCode = new Label()
                {
                    Text = "EMG. Code:",
                    Location = new Point(20, yPos),
                    Size = new Size(120, 25),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold)
                };

                txtEmergencyCode = new TextBox()
                {
                    Location = new Point(150, yPos),
                    Size = new Size(200, 30),
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Enabled = true, // Allow editing
                    ForeColor = Color.DarkBlue
                };

                btnGenerateEMGCode = new Button()
                {
                    Text = "Generate",
                    Location = new Point(360, yPos),
                    Size = new Size(80, 30),
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9F)
                };
                btnGenerateEMGCode.FlatAppearance.BorderSize = 0;
                btnGenerateEMGCode.Click += BtnGenerateEMGCode_Click;

                mainPanel.Controls.Add(lblEmergencyCode);
                mainPanel.Controls.Add(txtEmergencyCode);
                mainPanel.Controls.Add(btnGenerateEMGCode);

                yPos += 40;
            }

            // Appointment History
            var lblAppointmentHeader = new Label()
            {
                Text = "Appointment History",
                Location = new Point(20, yPos),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            yPos += 35;

            dgvAppointmentHistory = new DataGridView()
            {
                Location = new Point(20, yPos),
                Size = new Size(850, 200),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                EnableHeadersVisualStyles = false,
                GridColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                RowHeadersVisible = false
            };

            // Configure columns for Appointment History
            dgvAppointmentHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Date",
                HeaderText = "Date",
                DataPropertyName = "AppointmentDate",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvAppointmentHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Time",
                HeaderText = "Time",
                DataPropertyName = "TimeSlot",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvAppointmentHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Reason",
                HeaderText = "Reason",
                DataPropertyName = "Reason",
                Width = 200,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvAppointmentHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                DataPropertyName = "Status",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvAppointmentHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Provider",
                HeaderText = "Provider",
                DataPropertyName = "ProviderName",
                Width = 150,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            yPos += 210;

            // Consultation History
            var lblConsultationHeader = new Label()
            {
                Text = "Consultation History",
                Location = new Point(20, yPos),
                Size = new Size(300, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            yPos += 35;

            dgvConsultationHistory = new DataGridView()
            {
                Location = new Point(20, yPos),
                Size = new Size(850, 180),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.FromArgb(52, 152, 219),
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Alignment = DataGridViewContentAlignment.MiddleCenter
                },
                EnableHeadersVisualStyles = false,
                GridColor = Color.LightGray,
                BorderStyle = BorderStyle.FixedSingle,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                RowHeadersVisible = false
            };

            // Configure columns for Consultation History
            dgvConsultationHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDate",
                HeaderText = "Date",
                DataPropertyName = "ConsultationDate",
                Width = 110,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvConsultationHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTime",
                HeaderText = "Time",
                DataPropertyName = "ConsultationTime",
                Width = 80,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvConsultationHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colTemperature",
                HeaderText = "Temperature",
                DataPropertyName = "Temperature",
                Width = 100,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvConsultationHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colBloodPressure",
                HeaderText = "Blood Pressure",
                DataPropertyName = "BloodPressure",
                Width = 120,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            dgvConsultationHistory.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colDiagnosisNotes",
                HeaderText = "Diagnosis Notes",
                DataPropertyName = "DiagnosisNotes",
                Width = 300,
                SortMode = DataGridViewColumnSortMode.Automatic
            });

            // Add all controls to main panel
            mainPanel.Controls.Add(lblNameLabel);
            mainPanel.Controls.Add(lblPatientName);
            mainPanel.Controls.Add(lblPatientType);
            mainPanel.Controls.Add(lblPhone);
            mainPanel.Controls.Add(txtPhone);
            mainPanel.Controls.Add(lblAddress);
            mainPanel.Controls.Add(txtAddress);
            mainPanel.Controls.Add(lblAppointmentHeader);
            mainPanel.Controls.Add(dgvAppointmentHistory);
            mainPanel.Controls.Add(lblConsultationHeader);
            mainPanel.Controls.Add(dgvConsultationHistory);

            // Button Panel
            var buttonPanel = new Panel()
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20)
            };

            btnSave = new Button()
            {
                Text = "Save Changes",
                Size = new Size(120, 35),
                Location = new Point(650, 12),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Enabled = true
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            btnSave.Visible = true; // Show save button

            btnClose = new Button()
            {
                Text = "Close",
                Size = new Size(80, 35),
                Location = new Point(780, 12),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F)
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnClose);

            this.Controls.Add(mainPanel);
            this.Controls.Add(headerPanel);
            this.Controls.Add(buttonPanel);
        }

        private void LoadPatientDetails()
        {
            try
            {
                string sql;
                if (_isStudent)
                {
                    sql = @"
                        SELECT 
                            u.UserId,
                            u.FullName,
                            u.ContactPhone,
                            u.ContactEmail,
                            p.StudentId,
                            p.DOB,
                            p.Gender,
                            p.BloodType
                        FROM Patients p
                        INNER JOIN Users u ON p.UserId = u.UserId
                        WHERE p.PatientId = @PatientId";
                }
                else
                {
                    sql = @"
                        SELECT 
                            gp.GuestPatientId,
                            gp.FullName,
                            gp.PhoneNumber as ContactPhone,
                            gp.EmergencyContact,
                            gp.EmergencyPhone,
                            gp.Address,
                            (SELECT TOP 1 EmergencyCode 
                             FROM Appointments 
                             WHERE GuestPatientId = gp.GuestPatientId 
                               AND EmergencyCode IS NOT NULL 
                             ORDER BY AppointmentDate DESC, AppointmentId DESC) as EmergencyCode
                        FROM GuestPatients gp
                        WHERE gp.GuestPatientId = @PatientId";
                }

                var result = dbHelper.ExecuteQuery(sql, new { PatientId = _patientId });
                
                if (result.Rows.Count > 0)
                {
                    _patientData = result.Rows[0];
                    PopulateFields();
                    LoadPatientHistory();
                }
                else
                {
                    MessageBox.Show("Patient not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PopulateFields()
        {
            if (_patientData == null) return;

            lblPatientName.Text = _patientData["FullName"].ToString();
            lblPatientType.Text = _isStudent ? "Student" : "Guest";
            lblPatientType.ForeColor = _isStudent ? Color.Green : Color.Orange;

            txtPhone.Text = _patientData["ContactPhone"]?.ToString() ?? "";

            if (_isStudent)
            {
                txtEmail.Text = _patientData["ContactEmail"]?.ToString() ?? "";
                
                if (_patientData["DOB"] != DBNull.Value)
                    txtDOB.Text = Convert.ToDateTime(_patientData["DOB"]).ToString("yyyy-MM-dd");
                
                txtGender.Text = _patientData["Gender"]?.ToString() ?? "";
                txtBloodType.Text = _patientData["BloodType"]?.ToString() ?? "";
            }
            else
            {
                txtAddress.Text = _patientData["Address"]?.ToString() ?? "";
                txtEmergencyContact.Text = _patientData["EmergencyContact"]?.ToString() ?? "";
                txtEmergencyPhone.Text = _patientData["EmergencyPhone"]?.ToString() ?? "";
                
                // Set Emergency Code if available
                if (_patientData.Table.Columns.Contains("EmergencyCode") && _patientData["EmergencyCode"] != DBNull.Value && _patientData["EmergencyCode"] != null && !string.IsNullOrEmpty(_patientData["EmergencyCode"].ToString()))
                {
                    txtEmergencyCode.Text = _patientData["EmergencyCode"]?.ToString() ?? "";
                    txtEmergencyCode.ForeColor = Color.DarkBlue;
                }
                else
                {
                    txtEmergencyCode.Text = "No emergency code assigned";
                    txtEmergencyCode.ForeColor = Color.Gray;
                }
                
                txtAddress.Visible = true;
            }
        }

        private void LoadPatientHistory()
        {
            try
            {
                // Load appointments
                string appointmentSql;
                if (_isStudent)
                {
                    appointmentSql = @"
                        SELECT 
                            a.AppointmentId,
                            a.AppointmentDate,
                            a.TimeSlot,
                            a.Reason,
                            a.Status,
                            u.FullName as ProviderName
                        FROM Appointments a
                        LEFT JOIN Users u ON a.ProviderId = u.UserId
                        WHERE a.PatientId = @PatientId
                        ORDER BY a.AppointmentDate DESC, a.TimeSlot DESC";
                }
                else
                {
                    appointmentSql = @"
                        SELECT 
                            a.AppointmentId,
                            a.AppointmentDate,
                            a.TimeSlot,
                            a.Reason,
                            a.Status,
                            u.FullName as ProviderName
                        FROM Appointments a
                        LEFT JOIN Users u ON a.ProviderId = u.UserId
                        WHERE a.GuestPatientId = @PatientId
                        ORDER BY a.AppointmentDate DESC, a.TimeSlot DESC";
                }

                var appointments = dbHelper.ExecuteQuery(appointmentSql, new { PatientId = _patientId });

                if (appointments.Rows.Count > 0)
                {
                    // Create a DataTable for the appointments
                    DataTable appointmentTable = new DataTable();
                    
                    // Format the data for display
                    appointmentTable.Columns.Add("AppointmentDate", typeof(string));
                    appointmentTable.Columns.Add("TimeSlot", typeof(string));
                    appointmentTable.Columns.Add("Reason", typeof(string));
                    appointmentTable.Columns.Add("Status", typeof(string));
                    appointmentTable.Columns.Add("ProviderName", typeof(string));

                    foreach (DataRow row in appointments.Rows)
                    {
                        DataRow newRow = appointmentTable.NewRow();
                        newRow["AppointmentDate"] = Convert.ToDateTime(row["AppointmentDate"]).ToString("yyyy-MM-dd");
                        newRow["TimeSlot"] = row["TimeSlot"].ToString();
                        newRow["Reason"] = row["Reason"]?.ToString() ?? "";
                        newRow["Status"] = row["Status"]?.ToString() ?? "";
                        newRow["ProviderName"] = row["ProviderName"]?.ToString() ?? "Unassigned";
                        appointmentTable.Rows.Add(newRow);
                    }

                    dgvAppointmentHistory.DataSource = appointmentTable;
                    dgvAppointmentHistory.Refresh();
                }
                else
                {
                    dgvAppointmentHistory.DataSource = null;
                    dgvAppointmentHistory.Rows.Clear();
                }

                // Load consultations
                string consultationSql = @"
                    SELECT 
                        c.ConsultationDate,
                        c.Temperature,
                        c.BloodPressure,
                        c.DiagnosisNotes
                    FROM Consultations c
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    WHERE " + (_isStudent ? "a.PatientId = @PatientId" : "a.GuestPatientId = @PatientId") + @"
                    ORDER BY c.ConsultationDate DESC";

                var consultations = dbHelper.ExecuteQuery(consultationSql, new { PatientId = _patientId });

                if (consultations.Rows.Count > 0)
                {
                    // Create a DataTable for the consultations
                    DataTable consultationTable = new DataTable();
                    
                    // Format the data for display
                    consultationTable.Columns.Add("ConsultationDate", typeof(string));
                    consultationTable.Columns.Add("ConsultationTime", typeof(string));
                    consultationTable.Columns.Add("Temperature", typeof(string));
                    consultationTable.Columns.Add("BloodPressure", typeof(string));
                    consultationTable.Columns.Add("DiagnosisNotes", typeof(string));

                    foreach (DataRow row in consultations.Rows)
                    {
                        DataRow newRow = consultationTable.NewRow();
                        DateTime consultDate = Convert.ToDateTime(row["ConsultationDate"]);
                        newRow["ConsultationDate"] = consultDate.ToString("yyyy-MM-dd");
                        newRow["ConsultationTime"] = consultDate.ToString("HH:mm");
                        newRow["Temperature"] = row["Temperature"]?.ToString() ?? "N/A";
                        newRow["BloodPressure"] = row["BloodPressure"]?.ToString() ?? "N/A";
                        newRow["DiagnosisNotes"] = row["DiagnosisNotes"]?.ToString() ?? "";
                        consultationTable.Rows.Add(newRow);
                    }

                    dgvConsultationHistory.DataSource = consultationTable;
                    dgvConsultationHistory.Refresh();
                }
                else
                {
                    dgvConsultationHistory.DataSource = null;
                    dgvConsultationHistory.Rows.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isStudent)
                {
                    SaveStudentData();
                }
                else
                {
                    SaveGuestData();
                }
                
                MessageBox.Show("Patient details updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Reload the patient details
                LoadPatientDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving patient details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveStudentData()
        {
            // Get old values for audit logging
            string getOldValuesSql = "SELECT ContactEmail, ContactPhone FROM Users WHERE UserId = @UserId";
            var oldData = dbHelper.ExecuteQuery(getOldValuesSql, new { UserId = _patientData["UserId"] });
            
            string oldEmail = oldData.Rows.Count > 0 ? oldData.Rows[0]["ContactEmail"]?.ToString() ?? "" : "";
            string oldPhone = oldData.Rows.Count > 0 ? oldData.Rows[0]["ContactPhone"]?.ToString() ?? "" : "";

            // Update user's phone and email
            string updateUserSql = @"
                UPDATE Users 
                SET ContactPhone = @Phone, 
                    ContactEmail = @Email
                WHERE UserId = @UserId";
            dbHelper.ExecuteNonQuery(updateUserSql, new 
            { 
                Phone = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                UserId = _patientData["UserId"] 
            });

            // Log the audit trail
            string oldValues = $"{{\"Email\":\"{oldEmail}\",\"Phone\":\"{oldPhone}\"}}";
            string newValues = $"{{\"Email\":\"{txtEmail.Text.Trim()}\",\"Phone\":\"{txtPhone.Text.Trim()}\"}}";
            
            dbHelper.LogAudit(
                userId: _currentUser?.UserID ?? 0,
                action: "UPDATE",
                tableName: "Users",
                recordId: _patientData["UserId"].ToString(),
                oldValues: oldValues,
                newValues: newValues,
                details: $"Provider updated patient profile information"
            );
        }

        private void SaveGuestData()
        {
            // Get old values for audit logging
            string getOldValuesSql = @"
                SELECT PhoneNumber, EmergencyContact, EmergencyPhone, Address 
                FROM GuestPatients 
                WHERE GuestPatientId = @GuestPatientId";
            var oldData = dbHelper.ExecuteQuery(getOldValuesSql, new { GuestPatientId = _patientId });
            
            string oldPhone = oldData.Rows.Count > 0 ? oldData.Rows[0]["PhoneNumber"]?.ToString() ?? "" : "";
            string oldEmergencyContact = oldData.Rows.Count > 0 ? oldData.Rows[0]["EmergencyContact"]?.ToString() ?? "" : "";
            string oldEmergencyPhone = oldData.Rows.Count > 0 ? oldData.Rows[0]["EmergencyPhone"]?.ToString() ?? "" : "";
            string oldAddress = oldData.Rows.Count > 0 ? oldData.Rows[0]["Address"]?.ToString() ?? "" : "";

            // Update guest patient data
            string updateGuestSql = @"
                UPDATE GuestPatients 
                SET PhoneNumber = @PhoneNumber,
                    EmergencyContact = @EmergencyContact,
                    EmergencyPhone = @EmergencyPhone,
                    Address = @Address
                WHERE GuestPatientId = @GuestPatientId";
            
            dbHelper.ExecuteNonQuery(updateGuestSql, new
            {
                PhoneNumber = txtPhone.Text.Trim(),
                EmergencyContact = txtEmergencyContact.Text.Trim(),
                EmergencyPhone = txtEmergencyPhone.Text.Trim(),
                Address = txtAddress.Text.Trim(),
                GuestPatientId = _patientId
            });

            // Log the audit trail
            string oldValues = $"{{\"PhoneNumber\":\"{oldPhone}\",\"EmergencyContact\":\"{oldEmergencyContact}\",\"EmergencyPhone\":\"{oldEmergencyPhone}\",\"Address\":\"{oldAddress}\"}}";
            string newValues = $"{{\"PhoneNumber\":\"{txtPhone.Text.Trim()}\",\"EmergencyContact\":\"{txtEmergencyContact.Text.Trim()}\",\"EmergencyPhone\":\"{txtEmergencyPhone.Text.Trim()}\",\"Address\":\"{txtAddress.Text.Trim()}\"}}";
            
            dbHelper.LogAudit(
                userId: _currentUser?.UserID ?? 0,
                action: "UPDATE",
                tableName: "GuestPatients",
                recordId: _patientId.ToString(),
                oldValues: oldValues,
                newValues: newValues,
                details: $"Provider updated guest patient information"
            );

            // If EMG Code was updated and is not empty, save it to the most recent appointment
            if (!string.IsNullOrWhiteSpace(txtEmergencyCode.Text) && 
                txtEmergencyCode.Text != "No emergency code assigned")
            {
                // First check if there's any appointment for this guest
                string checkAppointmentSql = @"
                    SELECT COUNT(*) 
                    FROM Appointments 
                    WHERE GuestPatientId = @GuestPatientId";

                var appointmentCount = dbHelper.ExecuteScalar(checkAppointmentSql, new { GuestPatientId = _patientId });
                int count = appointmentCount != null ? Convert.ToInt32(appointmentCount) : 0;

                if (count > 0)
                {
                    // Update the most recent appointment with the EMG Code
                    string updateEMGCodeSql = @"
                        UPDATE TOP (1) Appointments
                        SET EmergencyCode = @EmergencyCode
                        WHERE GuestPatientId = @GuestPatientId";
                    
                    dbHelper.ExecuteNonQuery(updateEMGCodeSql, new
                    {
                        EmergencyCode = txtEmergencyCode.Text.Trim(),
                        GuestPatientId = _patientId
                    });
                }
                else
                {
                    // Create a new appointment record for this guest to store the EMG Code
                    string insertAppointmentSql = @"
                        INSERT INTO Appointments 
                        (GuestPatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime, EmergencyCode)
                        VALUES 
                        (@GuestPatientId, NULL, GETDATE(), CONVERT(TIME, GETDATE()), 'Emergency Record Entry', 'Completed', GETDATE(), @EmergencyCode)";
                    
                    dbHelper.ExecuteNonQuery(insertAppointmentSql, new
                    {
                        GuestPatientId = _patientId,
                        EmergencyCode = txtEmergencyCode.Text.Trim()
                    });
                }
            }
        }

        private void BtnGenerateEMGCode_Click(object sender, EventArgs e)
        {
            try
            {
                if (_isStudent)
                {
                    MessageBox.Show("EMG Code generation is only available for guest patients.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Get guest name
                string guestName = lblPatientName.Text;
                
                // Get the first emergency appointment date for this guest
                string firstEmergencySql = @"
                    SELECT TOP 1 AppointmentDate, CreatedDateTime
                    FROM Appointments
                    WHERE GuestPatientId = @GuestPatientId
                      AND EmergencyCode IS NOT NULL
                    ORDER BY AppointmentDate ASC, AppointmentId ASC";

                var firstEmergencyResult = dbHelper.ExecuteQuery(firstEmergencySql, new { GuestPatientId = _patientId });
                
                DateTime referenceDate;
                if (firstEmergencyResult.Rows.Count > 0)
                {
                    // Use the first emergency appointment date
                    referenceDate = Convert.ToDateTime(firstEmergencyResult.Rows[0]["AppointmentDate"]);
                }
                else
                {
                    // If no emergency exists, use current date
                    referenceDate = DateTime.Now;
                }

                // Get initials from guest name
                string[] nameParts = guestName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string initials = "";
                foreach (string part in nameParts)
                {
                    if (!string.IsNullOrEmpty(part))
                    {
                        initials += part[0].ToString().ToUpper();
                    }
                }
                if (initials.Length > 3) initials = initials.Substring(0, 3);

                // Count total emergency codes for this guest to get sequence number
                string countSql = @"
                    SELECT COUNT(*)
                    FROM Appointments
                    WHERE GuestPatientId = @GuestPatientId
                      AND EmergencyCode IS NOT NULL";

                var countResult = dbHelper.ExecuteScalar(countSql, new { GuestPatientId = _patientId });
                int sequenceNumber = (countResult != null ? Convert.ToInt32(countResult) : 0) + 1;

                // Generate the code: EMG{INITIALS}{SEQUENCE:000}{DATE:YYYYMMDD}
                string dateString = referenceDate.ToString("yyyyMMdd");
                string emergencyCode = $"EMG{initials}{sequenceNumber:D3}{dateString}";

                // Set the emergency code in the field
                txtEmergencyCode.Text = emergencyCode;
                txtEmergencyCode.ForeColor = Color.DarkBlue;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating EMG code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtEmergencyContact_TextChanged(object sender, EventArgs e)
        {
            // Prevent infinite loop
            if (isUpdatingEmergencyFields) return;

            // Auto-fill EMG. Number with the same value
            if (txtEmergencyContact != null && txtEmergencyPhone != null)
            {
                isUpdatingEmergencyFields = true;
                txtEmergencyPhone.Text = txtEmergencyContact.Text;
                isUpdatingEmergencyFields = false;
            }
        }

        private void TxtEmergencyPhone_TextChanged(object sender, EventArgs e)
        {
            // Prevent infinite loop
            if (isUpdatingEmergencyFields) return;

            // Auto-fill EMG. Contact with the same value
            if (txtEmergencyContact != null && txtEmergencyPhone != null)
            {
                isUpdatingEmergencyFields = true;
                txtEmergencyContact.Text = txtEmergencyPhone.Text;
                isUpdatingEmergencyFields = false;
            }
        }
    }
}

