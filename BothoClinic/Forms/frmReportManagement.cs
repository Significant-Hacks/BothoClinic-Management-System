using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace BothoClinic
{
    public partial class frmReportManagement : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        private TextBox txtSearch;
        private Button btnGenerateReport;
        private Label lblTitle;
        private Label lblSearch;
        private Panel pnlTop;
        private Panel pnlSearch;
        private DataGridView dgvPatients;

        public frmReportManagement()
        {
            InitializeComponent();
            SetupForm();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Medical Reports";
            this.Size = new Size(1200, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            
            // Main container
            pnlTop = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true
            };

            // Title
            lblTitle = new Label
            {
                Text = "Generate Medical Reports",
                Location = new Point(20, 10),
                Size = new Size(500, 30),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            // Search panel
            pnlSearch = new Panel
            {
                Location = new Point(10, 50),
                Size = new Size(1160, 45),
                Padding = new Padding(10),
                BackColor = Color.White
            };

            lblSearch = new Label()
            {
                Text = "Search Patient:",
                Location = new Point(10, 15),
                Size = new Size(110, 20),
                Font = new Font("Segoe UI", 10F)
            };

            txtSearch = new TextBox()
            {
                Location = new Point(130, 12),
                Size = new Size(800, 25),
                Font = new Font("Segoe UI", 10F)
            };
            txtSearch.KeyDown += TxtSearch_KeyDown;

            btnGenerateReport = new Button()
            {
                Text = "Generate Report",
                Location = new Point(950, 10),
                Size = new Size(120, 35),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnGenerateReport.Click += BtnGenerateReport_Click;
            btnGenerateReport.FlatAppearance.BorderSize = 0;

            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(txtSearch);
            pnlSearch.Controls.Add(btnGenerateReport);

            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(pnlSearch);

            this.Controls.Add(pnlTop);
            this.ResumeLayout(false);
        }

        private void SetupForm()
        {
            // Add DataGridView for patient list
            dgvPatients = new DataGridView()
            {
                Location = new Point(20, 110),
                Size = new Size(1140, 280),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
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
            dgvPatients.CellDoubleClick += DgvPatients_CellDoubleClick;

            // Add DataGridView to the panel
            pnlTop.Controls.Add(dgvPatients);

            // Load all patients on startup
            LoadAllPatients();
        }

        private void LoadAllPatients()
        {
            try
            {
                DataTable patients = GetAllPatients();
                dgvPatients.DataSource = patients;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading patients: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable GetAllPatients()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("PatientId", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Phone", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("IsStudent", typeof(bool));

            // Load Students
            string studentSql = @"
                SELECT 
                    p.PatientId,
                    u.FullName,
                    u.ContactPhone,
                    p.StudentId
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
                    true
                );
            }

            // Load Guests
            string guestSql = @"
                SELECT 
                    GuestPatientId as PatientId,
                    FullName,
                    PhoneNumber as ContactPhone,
                    EmergencyPhone as ID
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
                    false
                );
            }

            return dt;
        }

        private void DgvPatients_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvPatients.Rows[e.RowIndex];
                int patientId = Convert.ToInt32(row.Cells["PatientId"].Value);
                bool isStudent = Convert.ToBoolean(row.Cells["IsStudent"].Value);
                
                // Open report popup
                frmReportDetails reportDetails = new frmReportDetails(patientId, isStudent);
                reportDetails.ShowDialog();
            }
        }

        private DataTable GetPatientDetails(int patientId, bool isStudent)
        {
            if (isStudent)
            {
                string sql = @"
                    SELECT 
                        'Student' as PatientType,
                        pt.PatientId,
                        u.UserId,
                        u.FullName,
                        u.ContactPhone,
                        u.ContactEmail,
                        pt.StudentId
                    FROM Patients pt
                    INNER JOIN Users u ON pt.UserId = u.UserId
                    WHERE pt.PatientId = @PatientId";

                return dbHelper.ExecuteQuery(sql, new { PatientId = patientId });
            }
            else
            {
                string sql = @"
                    SELECT 
                        'Guest' as PatientType,
                        gp.GuestPatientId as PatientId,
                        NULL as UserId,
                        gp.FullName,
                        gp.PhoneNumber as ContactPhone,
                        NULL as ContactEmail,
                        gp.EmergencyPhone as StudentId
                    FROM GuestPatients gp
                    WHERE gp.GuestPatientId = @PatientId";

                return dbHelper.ExecuteQuery(sql, new { PatientId = patientId });
            }
        }

        private void TxtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                BtnGenerateReport_Click(sender, e);
            }
        }

        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                string searchTerm = txtSearch.Text.Trim();
                
                if (string.IsNullOrEmpty(searchTerm))
                {
                    MessageBox.Show("Please enter a patient name or ID to search.", "Search Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Search for patient
                DataTable patient = SearchPatient(searchTerm);
                
                if (patient == null || patient.Rows.Count == 0)
                {
                    MessageBox.Show("Patient not found. Please check the search term and try again.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get patient details
                int patientId = Convert.ToInt32(patient.Rows[0]["PatientId"]);
                bool isStudent = patient.Rows[0]["PatientType"].ToString() == "Student";

                // Open report popup
                frmReportDetails reportDetails = new frmReportDetails(patientId, isStudent);
                reportDetails.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private DataTable SearchPatient(string searchTerm)
        {
            try
            {
                string sql = @"
                    SELECT 
                        'Student' as PatientType,
                        pt.PatientId,
                        u.UserId,
                        u.FullName,
                        u.ContactPhone,
                        u.ContactEmail,
                        pt.StudentId
                    FROM Patients pt
                    INNER JOIN Users u ON pt.UserId = u.UserId
                    WHERE u.FullName LIKE @Search OR pt.StudentId LIKE @Search OR u.ContactPhone LIKE @Search
                    
                    UNION ALL
                    
                    SELECT 
                        'Guest' as PatientType,
                        gp.GuestPatientId as PatientId,
                        NULL as UserId,
                        gp.FullName,
                        gp.PhoneNumber as ContactPhone,
                        NULL as ContactEmail,
                        gp.EmergencyPhone as StudentId
                    FROM GuestPatients gp
                    WHERE gp.FullName LIKE @Search OR gp.PhoneNumber LIKE @Search OR gp.EmergencyPhone LIKE @Search";

                return dbHelper.ExecuteQuery(sql, new { Search = $"%{searchTerm}%" });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

    }
}

