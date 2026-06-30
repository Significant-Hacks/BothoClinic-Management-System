using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmMedicalHistory : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        private DataGridView dgvMedicalHistory;
        private Label lblTitle;
        private Panel pnlTop;
        private TextBox txtSearch;
        private Label lblSearch;

        public frmMedicalHistory()
        {
            InitializeComponent();
            SetupForm();
            LoadMedicalHistory();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // Form settings
            this.Text = "Medical History";
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
                Text = "Patient Visit Summaries",
                Location = new Point(20, 10),
                Size = new Size(500, 30),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            // Search panel
            Panel pnlSearch = new Panel
            {
                Location = new Point(10, 50),
                Size = new Size(1160, 45),
                Padding = new Padding(10),
                BackColor = Color.White,
                Height = 45
            };

            lblSearch = new Label()
            {
                Text = "Search:",
                Location = new Point(10, 15),
                Size = new Size(60, 20),
                Font = new Font("Segoe UI", 10F)
            };

            txtSearch = new TextBox()
            {
                Location = new Point(80, 12),
                Size = new Size(600, 25),
                Font = new Font("Segoe UI", 10F)
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(txtSearch);

            // DataGridView for Medical History
            dgvMedicalHistory = new DataGridView()
            {
                Location = new Point(20, 105),
                Size = new Size(1140, 1000), // Increased height to make it scrollable
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
                RowHeadersVisible = false,
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle()
                {
                    BackColor = Color.FromArgb(245, 245, 245)
                }
            };

            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(pnlSearch);
            pnlTop.Controls.Add(dgvMedicalHistory);
            
            // Set minimum size for the panel to enable scrolling
            pnlTop.AutoScrollMinSize = new Size(1160, 1200);

            this.Controls.Add(pnlTop);
            this.ResumeLayout(false);
        }

        private void SetupForm()
        {
            // Additional setup if needed
        }

        private void LoadMedicalHistory()
        {
            try
            {
                string sql = @"
                    -- Student Appointments with Consultations
                    SELECT 
                        a.AppointmentDate,
                        CAST(a.AppointmentDate AS VARCHAR(10)) + ' ' + CONVERT(VARCHAR(8), a.TimeSlot, 108) as VisitDateTime,
                        u.FullName as PatientName,
                        'Student' as PatientType,
                        p.StudentId as PatientID,
                        a.Reason as VisitReason,
                        a.Status as VisitStatus,
                        ISNULL(c.DiagnosisNotes, 'No diagnosis recorded') as DiagnosisSummary,
                        ISNULL(provider.FullName, 'Unassigned') as ProviderName,
                        CASE WHEN c.ConsultationId IS NOT NULL THEN 'Yes' ELSE 'No' END as ConsultationDone,
                        CASE WHEN c.FollowUpNeeded = 1 THEN 'Yes' ELSE 'No' END as FollowUpNeeded
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientId = p.PatientId
                    INNER JOIN Users u ON p.UserId = u.UserId
                    LEFT JOIN Consultations c ON a.AppointmentId = c.AppointmentId
                    LEFT JOIN Users provider ON a.ProviderId = provider.UserId
                    
                    UNION ALL
                    
                    -- Guest Appointments with Consultations
                    SELECT 
                        a.AppointmentDate,
                        CAST(a.AppointmentDate AS VARCHAR(10)) + ' ' + CONVERT(VARCHAR(8), a.TimeSlot, 108) as VisitDateTime,
                        gp.FullName as PatientName,
                        'Guest' as PatientType,
                        ISNULL(a.EmergencyCode, CAST(gp.GuestPatientId AS VARCHAR)) as PatientID,
                        a.Reason as VisitReason,
                        a.Status as VisitStatus,
                        ISNULL(c.DiagnosisNotes, 'No diagnosis recorded') as DiagnosisSummary,
                        ISNULL(provider.FullName, 'Unassigned') as ProviderName,
                        CASE WHEN c.ConsultationId IS NOT NULL THEN 'Yes' ELSE 'No' END as ConsultationDone,
                        CASE WHEN c.FollowUpNeeded = 1 THEN 'Yes' ELSE 'No' END as FollowUpNeeded
                    FROM Appointments a
                    INNER JOIN GuestPatients gp ON a.GuestPatientId = gp.GuestPatientId
                    LEFT JOIN Consultations c ON a.AppointmentId = c.AppointmentId
                    LEFT JOIN Users provider ON a.ProviderId = provider.UserId
                    
                    ORDER BY AppointmentDate DESC, VisitDateTime DESC";

                DataTable dt = dbHelper.ExecuteQuery(sql);
                dgvMedicalHistory.DataSource = dt;
                ConfigureColumns();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading medical history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureColumns()
        {
            // Configure column properties
            foreach (DataGridViewColumn column in dgvMedicalHistory.Columns)
            {
                column.ReadOnly = true;
                
                // Hide internal columns if needed
                if (column.Name == "AppointmentDate")
                {
                    column.Visible = false;
                }
            }

            // Set display names
            if (dgvMedicalHistory.Columns.Contains("VisitDateTime"))
                dgvMedicalHistory.Columns["VisitDateTime"].HeaderText = "Visit Date & Time";
            if (dgvMedicalHistory.Columns.Contains("PatientName"))
                dgvMedicalHistory.Columns["PatientName"].HeaderText = "Patient Name";
            if (dgvMedicalHistory.Columns.Contains("PatientType"))
                dgvMedicalHistory.Columns["PatientType"].HeaderText = "Type";
            if (dgvMedicalHistory.Columns.Contains("PatientID"))
                dgvMedicalHistory.Columns["PatientID"].HeaderText = "Patient ID";
            if (dgvMedicalHistory.Columns.Contains("VisitReason"))
                dgvMedicalHistory.Columns["VisitReason"].HeaderText = "Reason";
            if (dgvMedicalHistory.Columns.Contains("VisitStatus"))
                dgvMedicalHistory.Columns["VisitStatus"].HeaderText = "Status";
            if (dgvMedicalHistory.Columns.Contains("DiagnosisSummary"))
                dgvMedicalHistory.Columns["DiagnosisSummary"].HeaderText = "Diagnosis Summary";
            if (dgvMedicalHistory.Columns.Contains("ProviderName"))
                dgvMedicalHistory.Columns["ProviderName"].HeaderText = "Provider";
            if (dgvMedicalHistory.Columns.Contains("ConsultationDone"))
                dgvMedicalHistory.Columns["ConsultationDone"].HeaderText = "Consulted";
            if (dgvMedicalHistory.Columns.Contains("FollowUpNeeded"))
                dgvMedicalHistory.Columns["FollowUpNeeded"].HeaderText = "Follow-Up Needed";
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvMedicalHistory.DataSource is DataTable dt)
                {
                    string filterText = txtSearch.Text;
                    if (string.IsNullOrWhiteSpace(filterText))
                    {
                        dt.DefaultView.RowFilter = "";
                    }
                    else
                    {
                        string filter = $@"PatientName LIKE '%{filterText}%' OR 
                                        PatientID LIKE '%{filterText}%' OR 
                                        VisitReason LIKE '%{filterText}%' OR 
                                        DiagnosisSummary LIKE '%{filterText}%' OR 
                                        ProviderName LIKE '%{filterText}%'";
                        dt.DefaultView.RowFilter = filter;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error filtering data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

