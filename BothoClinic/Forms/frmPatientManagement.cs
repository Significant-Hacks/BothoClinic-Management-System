using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BothoClinic.Models;

namespace BothoClinic
{
    public partial class frmPatientManagement : Form
    {
        private DatabaseHelper dbHelper;
        private DataGridView dgvPatients;
        private User _currentUser;
        private Panel pnlPatientDetails;
        private Label lblPatientName;
        private Label lblPatientInfo;
        private TextBox txtSearchPatient;
        private Button btnSearchPatient;
        private ComboBox cmbPatientType;
        private Panel pnlAppointmentHistory;
        private Label lblAppointments;
        private RichTextBox rtbPatientDetails;

        public frmPatientManagement()
        {
            dbHelper = new DatabaseHelper();
            _currentUser = null;
            InitializeComponent();
            SetupForm();
        }

        public void SetCurrentUser(User currentUser)
        {
            _currentUser = currentUser;
        }

        private void SetupForm()
        {
            this.Text = "Patient Management";
            this.Size = new Size(1000, 700);
            this.StartPosition = FormStartPosition.CenterParent;

            // Main Panel with Split Container - Fixed size for proper scrolling
            var splitContainer = new SplitContainer()
            {
                Dock = DockStyle.Fill,
                SplitterDistance = 450, // Adjust to give more space to patient list
                Orientation = Orientation.Horizontal,
                FixedPanel = FixedPanel.Panel2 // Keep patient details fixed height
            };

            // Top Panel - Patient List (no AutoScroll needed, DataGridView handles it)
            var pnlTop = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(0) // Remove padding for full area usage
            };

            // Search Panel
            var pnlSearch = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 80,
                Padding = new Padding(10),
                BackColor = Color.White // Make it stand out
            };

            var lblSearch = new Label()
            {
                Text = "Search Patient:",
                Location = new Point(10, 10),
                Size = new Size(100, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            txtSearchPatient = new TextBox()
            {
                Location = new Point(120, 8),
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 10F),
                PlaceholderText = "Enter name, ID, or phone number"
            };
            txtSearchPatient.TextChanged += TxtSearchPatient_TextChanged;

            btnSearchPatient = new Button()
            {
                Text = "Search",
                Location = new Point(380, 8),
                Size = new Size(80, 30),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSearchPatient.Click += BtnSearchPatient_Click;

            var lblFilter = new Label()
            {
                Text = "Filter:",
                Location = new Point(480, 10),
                Size = new Size(50, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            cmbPatientType = new ComboBox()
            {
                Location = new Point(540, 8),
                Size = new Size(120, 30),
                Font = new Font("Segoe UI", 10F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPatientType.Items.AddRange(new string[] { "All Patients", "Students Only", "Guests Only" });
            cmbPatientType.SelectedIndex = 0;
            cmbPatientType.SelectedIndexChanged += CmbPatientType_SelectedIndexChanged;

            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(txtSearchPatient);
            pnlSearch.Controls.Add(btnSearchPatient);
            pnlSearch.Controls.Add(lblFilter);
            pnlSearch.Controls.Add(cmbPatientType);

            // Patient Grid - Dock to fill remaining space
            dgvPatients = new DataGridView()
            {
                Dock = DockStyle.Fill, // Dock to fill the panel
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                ReadOnly = true,
                AllowUserToAddRows = false,
                MultiSelect = false,
                MinimumSize = new Size(0, 200)
            };
            dgvPatients.CellClick += DgvPatients_CellClick;
            dgvPatients.CellDoubleClick += DgvPatients_CellDoubleClick;
            dgvPatients.DataBindingComplete += DgvPatients_DataBindingComplete;
            dgvPatients.SelectionChanged += DgvPatients_SelectionChanged;

            // IMPORTANT: Add in the correct order - DataGridView first (fills area), then search panel (on top)
            pnlTop.Controls.Add(dgvPatients);
            pnlTop.Controls.Add(pnlSearch); // Search panel stays on top

            // Bottom Panel - Patient Details with scrolling
            var pnlBottom = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                AutoScroll = true // Enable scrolling for patient details
            };

            // Patient Details Header
            var lblDetailsHeader = new Label()
            {
                Text = "Patient Details & Medical History",
                Location = new Point(10, 10),
                Size = new Size(400, 30),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            // Patient Info Panel
            pnlPatientDetails = new Panel()
            {
                Location = new Point(10, 50),
                Size = new Size(950, 120),
                BorderStyle = BorderStyle.FixedSingle,
                AutoSize = true
            };

            lblPatientName = new Label()
            {
                Text = "No patient selected",
                Location = new Point(10, 10),
                Size = new Size(400, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            lblPatientInfo = new Label()
            {
                Text = "Select a patient from the list above to view details",
                Location = new Point(10, 40),
                Size = new Size(900, 50),
                Font = new Font("Segoe UI", 10F),
                AutoSize = true
            };

            pnlPatientDetails.Controls.Add(lblPatientName);
            pnlPatientDetails.Controls.Add(lblPatientInfo);

            // Appointment History Panel
            pnlAppointmentHistory = new Panel()
            {
                Location = new Point(10, 160),
                Size = new Size(950, 450),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblAppointments = new Label()
            {
                Text = "Appointment History",
                Location = new Point(10, 10),
                Size = new Size(200, 25),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };

            rtbPatientDetails = new RichTextBox()
            {
                Location = new Point(10, 40),
                Size = new Size(930, 400),
                Font = new Font("Segoe UI", 10F),
                ReadOnly = true,
                ScrollBars = RichTextBoxScrollBars.Vertical
            };

            pnlAppointmentHistory.Controls.Add(lblAppointments);
            pnlAppointmentHistory.Controls.Add(rtbPatientDetails);

            pnlBottom.Controls.Add(lblDetailsHeader);
            pnlBottom.Controls.Add(pnlPatientDetails);
            pnlBottom.Controls.Add(pnlAppointmentHistory);

            splitContainer.Panel1.Controls.Add(pnlTop);
            splitContainer.Panel2.Controls.Add(pnlBottom);

            this.Controls.Add(splitContainer);
            
            // Enable scrolling for the entire form
            this.AutoScroll = true;
            this.AutoScrollMinSize = new Size(0, 1000); // Set minimum scrollable size
            
            // Also make the split container scrollable
            splitContainer.Panel2.AutoScroll = true;

            LoadPatients();
        }

        private void LoadPatients()
        {
            try
            {
                DataTable dt = GetCombinedPatientData();
                dgvPatients.DataSource = dt;

                if (dgvPatients.Columns.Count > 0)
                {
                    dgvPatients.Columns["PatientId"].Visible = false;
                    dgvPatients.Columns["IsStudent"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetCombinedPatientData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PatientId", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("Email", typeof(string));
            dt.Columns.Add("Address", typeof(string));
            dt.Columns.Add("IsStudent", typeof(bool));

            try
            {
                // Load Students
                if (cmbPatientType.SelectedIndex == 0 || cmbPatientType.SelectedIndex == 1)
                {
                    string studentSql = @"
                        SELECT 
                            p.PatientId,
                            u.FullName,
                            u.ContactPhone,
                            u.ContactEmail,
                            p.StudentId,
                            NULL as Address
                        FROM Patients p
                        INNER JOIN Users u ON p.UserId = u.UserId
                        WHERE u.IsActive = 1
                        ORDER BY u.FullName";

                    var studentData = dbHelper.ExecuteQuery(studentSql);

                    foreach (DataRow row in studentData.Rows)
                    {
                        dt.Rows.Add(
                            row["PatientId"],
                            row["FullName"].ToString(),
                            row["ContactPhone"]?.ToString() ?? "",
                            "Student",
                            row["StudentId"].ToString(),
                            row["ContactEmail"]?.ToString() ?? "",
                            "",
                            true
                        );
                    }
                }

                // Load Guests
                if (cmbPatientType.SelectedIndex == 0 || cmbPatientType.SelectedIndex == 2)
                {
                    string guestSql = @"
                        SELECT 
                            GuestPatientId as PatientId,
                            FullName,
                            PhoneNumber as ContactPhone,
                            NULL as ContactEmail,
                            EmergencyPhone as ID,
                            Address
                        FROM GuestPatients
                        WHERE IsActive = 1
                        ORDER BY FullName";

                    var guestData = dbHelper.ExecuteQuery(guestSql);

                    foreach (DataRow row in guestData.Rows)
                    {
                        dt.Rows.Add(
                            row["PatientId"],
                            row["FullName"].ToString(),
                            row["ContactPhone"]?.ToString() ?? "",
                            "Guest",
                            row["ID"]?.ToString() ?? "",
                            "",
                            row["Address"]?.ToString() ?? "",
                            false
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patient data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return dt;
        }

        private void DgvPatients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPatients.RowCount)
            {
                try
                {
                    int patientId = Convert.ToInt32(dgvPatients.Rows[e.RowIndex].Cells["PatientId"].Value);
                    bool isStudent = Convert.ToBoolean(dgvPatients.Rows[e.RowIndex].Cells["IsStudent"].Value);
                    string patientName = dgvPatients.Rows[e.RowIndex].Cells["Name"].Value.ToString();

                    // Open Patient Details popup
                    if (_currentUser != null)
                    {
                        using (var patientDetailsForm = new frmPatientDetails(patientId, isStudent, _currentUser))
                        {
                            patientDetailsForm.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Unable to open patient details. User information not available.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening patient details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DgvPatients_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPatients.RowCount)
            {
                // Ensure the row is selected
                dgvPatients.Rows[e.RowIndex].Selected = true;
                LoadPatientDetails();
            }
        }
        
        private void DgvPatients_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPatients.SelectedRows.Count > 0)
            {
                LoadPatientDetails();
            }
            else
            {
                // Clear details if no row is selected
                ClearPatientDetails();
            }
        }
        
        private void ClearPatientDetails()
        {
            if (lblPatientName != null)
                lblPatientName.Text = "No patient selected";
            if (lblPatientInfo != null)
                lblPatientInfo.Text = "Select a patient from the list above to view details";
            if (rtbPatientDetails != null)
                rtbPatientDetails.Clear();
        }

        private void DgvPatients_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvPatients.Columns.Count > 0)
            {
                dgvPatients.Columns["PatientId"].Visible = false;
                dgvPatients.Columns["IsStudent"].Visible = false;

                if (dgvPatients.Columns.Contains("Name"))
                {
                    dgvPatients.Columns["Name"].HeaderText = "Full Name";
                }
                if (dgvPatients.Columns.Contains("Phone"))
                {
                    dgvPatients.Columns["Phone"].HeaderText = "Phone";
                }
                if (dgvPatients.Columns.Contains("Type"))
                {
                    dgvPatients.Columns["Type"].HeaderText = "Type";
                }
                if (dgvPatients.Columns.Contains("ID"))
                {
                    dgvPatients.Columns["ID"].HeaderText = "ID/Student ID";
                }
            }
        }

        private void LoadPatientDetails()
        {
            if (dgvPatients.SelectedRows.Count == 0)
            {
                ClearPatientDetails();
                return;
            }

            try
            {
                int patientId = Convert.ToInt32(dgvPatients.SelectedRows[0].Cells["PatientId"].Value);
                bool isStudent = Convert.ToBoolean(dgvPatients.SelectedRows[0].Cells["IsStudent"].Value);
                string patientName = dgvPatients.SelectedRows[0].Cells["Name"].Value.ToString();
                string patientType = dgvPatients.SelectedRows[0].Cells["Type"].Value.ToString();

                // Display patient information
                if (lblPatientName != null)
                    lblPatientName.Text = $"{patientName} ({patientType})";
                
                if (lblPatientInfo != null)
                    lblPatientInfo.Text = GetPatientInfo(patientId, isStudent, patientName);

                // Load appointment history
                LoadPatientAppointments(patientId, isStudent);

                // Load consultations and prescriptions
                LoadPatientMedicalHistory(patientId, isStudent);
            }
            catch (Exception ex)
            {
                if (lblPatientName != null)
                    lblPatientName.Text = "Error loading patient";
                if (lblPatientInfo != null)
                    lblPatientInfo.Text = $"Error: {ex.Message}";
            }
        }

        private string GetPatientInfo(int patientId, bool isStudent, string name)
        {
            try
            {
                string sql;
                if (isStudent)
                {
                    sql = @"
                        SELECT 
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

                    var result = dbHelper.ExecuteQuery(sql, new { PatientId = patientId });
                    if (result.Rows.Count > 0)
                    {
                        var row = result.Rows[0];
                        return $"Phone: {row["ContactPhone"]}\nEmail: {row["ContactEmail"]}\nStudent ID: {row["StudentId"]}\nDOB: {row["DOB"]}\nGender: {row["Gender"]}\nBlood Type: {row["BloodType"]}";
                    }
                }
                else
                {
                    sql = @"
                        SELECT 
                            FullName,
                            PhoneNumber,
                            Address,
                            EmergencyContact,
                            EmergencyPhone
                        FROM GuestPatients
                        WHERE GuestPatientId = @PatientId";

                    var result = dbHelper.ExecuteQuery(sql, new { PatientId = patientId });
                    if (result.Rows.Count > 0)
                    {
                        var row = result.Rows[0];
                        return $"Phone: {row["PhoneNumber"]}\nAddress: {row["Address"]}\nEmergency Contact: {row["EmergencyContact"]}\nEmergency Phone: {row["EmergencyPhone"]}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error loading patient info: {ex.Message}";
            }

            return "";
        }

        private void LoadPatientAppointments(int patientId, bool isStudent)
        {
            try
            {
                string sql;
                if (isStudent)
                {
                    sql = @"
                        SELECT 
                            a.AppointmentId,
                            a.AppointmentDate,
                            a.TimeSlot,
                            a.Reason,
                            a.Status,
                            a.CreatedDateTime,
                            u.FullName as ProviderName
                        FROM Appointments a
                        LEFT JOIN Users u ON a.ProviderId = u.UserId
                        WHERE a.PatientId = @PatientId
                        ORDER BY a.AppointmentDate DESC, a.TimeSlot DESC";
                }
                else
                {
                    sql = @"
                        SELECT 
                            a.AppointmentId,
                            a.AppointmentDate,
                            a.TimeSlot,
                            a.Reason,
                            a.Status,
                            a.CreatedDateTime,
                            u.FullName as ProviderName
                        FROM Appointments a
                        LEFT JOIN Users u ON a.ProviderId = u.UserId
                        WHERE a.GuestPatientId = @PatientId
                        ORDER BY a.AppointmentDate DESC, a.TimeSlot DESC";
                }

                var appointments = dbHelper.ExecuteQuery(sql, new { PatientId = patientId });

                rtbPatientDetails.Clear();
                rtbPatientDetails.AppendText("APPOINTMENT HISTORY\n");
                rtbPatientDetails.AppendText("═══════════════════════════════════════════════════════\n\n");

                if (appointments.Rows.Count > 0)
                {
                    foreach (DataRow row in appointments.Rows)
                    {
                        rtbPatientDetails.AppendText($"Date: {Convert.ToDateTime(row["AppointmentDate"]):yyyy-MM-dd}  Time: {row["TimeSlot"]}\n");
                        rtbPatientDetails.AppendText($"Reason: {row["Reason"]}\n");
                        rtbPatientDetails.AppendText($"Status: {row["Status"]}\n");
                        rtbPatientDetails.AppendText($"Provider: {row["ProviderName"]?.ToString() ?? "Unassigned"}\n");
                        rtbPatientDetails.AppendText("───────────────────────────────────────────────────────────\n\n");
                    }
                }
                else
                {
                    rtbPatientDetails.AppendText("No appointments found.\n");
                }
            }
            catch (Exception ex)
            {
                rtbPatientDetails.Text = $"Error loading appointments: {ex.Message}";
            }
        }

        private void LoadPatientMedicalHistory(int patientId, bool isStudent)
        {
            try
            {
                string sql = @"
                    SELECT 
                        c.ConsultationId,
                        c.ConsultationDate,
                        c.DiagnosisNotes,
                        c.Temperature,
                        c.BloodPressure,
                        a.AppointmentDate
                    FROM Consultations c
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    WHERE " + (isStudent ? "a.PatientId = @PatientId" : "a.GuestPatientId = @PatientId") + @"
                    ORDER BY c.ConsultationDate DESC";

                var consultations = dbHelper.ExecuteQuery(sql, new { PatientId = patientId });

                if (consultations.Rows.Count > 0)
                {
                    rtbPatientDetails.AppendText("\n\nCONSULTATIONS\n");
                    rtbPatientDetails.AppendText("═══════════════════════════════════════════════════════\n\n");

                    foreach (DataRow row in consultations.Rows)
                    {
                        rtbPatientDetails.AppendText($"Date: {Convert.ToDateTime(row["ConsultationDate"]):yyyy-MM-dd}\n");
                        rtbPatientDetails.AppendText($"Temperature: {row["Temperature"]}°C  BP: {row["BloodPressure"]}\n");
                        rtbPatientDetails.AppendText($"Notes: {row["DiagnosisNotes"]}\n");
                        rtbPatientDetails.AppendText("───────────────────────────────────────────────────────────\n\n");
                    }
                }
            }
            catch (Exception ex)
            {
                rtbPatientDetails.AppendText($"\n\nError loading consultations: {ex.Message}");
            }
        }

        private void TxtSearchPatient_TextChanged(object sender, EventArgs e)
        {
            // Implement search filtering
            if (dgvPatients.DataSource is DataTable dt)
            {
                DataView dv = new DataView(dt);
                string searchText = txtSearchPatient.Text.Trim();
                
                if (!string.IsNullOrWhiteSpace(searchText))
                {
                    dv.RowFilter = $"Name LIKE '%{searchText}%' OR ID LIKE '%{searchText}%' OR Phone LIKE '%{searchText}%'";
                }
                else
                {
                    dv.RowFilter = "";
                }
                
                dgvPatients.DataSource = dv;
            }
        }

        private void BtnSearchPatient_Click(object sender, EventArgs e)
        {
            TxtSearchPatient_TextChanged(sender, e);
        }

        private void CmbPatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPatients();
        }

    }
}

