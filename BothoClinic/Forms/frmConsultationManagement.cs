using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BothoClinic.Models;

namespace BothoClinic
{
    public partial class frmConsultationManagement : Form
    {
        private DatabaseHelper dbHelper;
        private DataGridView dgvConsultations;
        private TextBox txtSearch;
        private Label lblTitle;
        private Label lblSearch;
        private Panel pnlTop;

        public frmConsultationManagement()
        {
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            SetupForm();
            LoadConsultations();
        }

        private bool isDataLoaded = false;

        private void SetupForm()
        {
            this.Text = "Consultation Management";
            this.Size = new Size(1200, 800);
            this.MinimumSize = new Size(1000, 600);
            this.BackColor = Color.FromArgb(248, 249, 250);

            // Main Panel - List Only
            pnlTop = new Panel()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None
            };

            // Title
            lblTitle = new Label()
            {
                Text = "All Consultations",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                Location = new Point(20, 20),
                AutoSize = true
            };

            // Search Panel
            var pnlSearch = new Panel()
            {
                Location = new Point(20, 60),
                Size = new Size(700, 40),
                Padding = new Padding(10),
                BackColor = Color.White
            };

            lblSearch = new Label()
            {
                Text = "Search:",
                Location = new Point(10, 10),
                Size = new Size(60, 20),
                Font = new Font("Segoe UI", 10F)
            };

            txtSearch = new TextBox()
            {
                Location = new Point(80, 8),
                Size = new Size(600, 25),
                Font = new Font("Segoe UI", 10F)
            };
            txtSearch.TextChanged += TxtSearch_TextChanged;

            pnlSearch.Controls.Add(lblSearch);
            pnlSearch.Controls.Add(txtSearch);

            // DataGridView for Consultations
            dgvConsultations = new DataGridView()
            {
                Location = new Point(20, 110),
                Size = new Size(1130, 580),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
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
            dgvConsultations.DataBindingComplete += DgvConsultations_DataBindingComplete;

            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(pnlSearch);
            pnlTop.Controls.Add(dgvConsultations);
            pnlTop.AutoScroll = true;
            
            // Add double-click event
            dgvConsultations.CellDoubleClick += DgvConsultations_CellDoubleClick;

            this.Controls.Add(pnlTop);
        }

        private void LoadConsultations()
        {
            try
            {
                string sql = @"
                    SELECT 
                        c.ConsultationId,
                        c.AppointmentId,
                        c.ConsultationDate,
                        c.Temperature,
                        c.BloodPressure,
                        LEFT(c.DiagnosisNotes, 100) as ShortNotes,
                        c.DiagnosisNotes,
                        c.FollowUpNeeded,
                        CASE 
                            WHEN a.PatientId IS NOT NULL THEN sp.FullName
                            ELSE gp.FullName
                        END as PatientName,
                        u.FullName as ProviderName
                    FROM Consultations c
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    LEFT JOIN Patients pt ON a.PatientId = pt.PatientId
                    LEFT JOIN Users sp ON pt.UserId = sp.UserId
                    LEFT JOIN GuestPatients gp ON a.GuestPatientId = gp.GuestPatientId
                    LEFT JOIN Users u ON c.ProviderId = u.UserId
                    ORDER BY c.ConsultationDate DESC";

                var consultations = dbHelper.ExecuteQuery(sql, null);
                
                isDataLoaded = true;
                dgvConsultations.DataSource = consultations;
                dgvConsultations.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading consultations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvConsultations_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvConsultations.Columns.Count > 0)
            {
                if (dgvConsultations.Columns.Contains("ConsultationId"))
                    dgvConsultations.Columns["ConsultationId"].Visible = false;
                if (dgvConsultations.Columns.Contains("AppointmentId"))
                    dgvConsultations.Columns["AppointmentId"].Visible = false;
                if (dgvConsultations.Columns.Contains("DiagnosisNotes"))
                    dgvConsultations.Columns["DiagnosisNotes"].Visible = false;
                
                if (dgvConsultations.Columns.Contains("ConsultationDate"))
                    dgvConsultations.Columns["ConsultationDate"].HeaderText = "Date";
                if (dgvConsultations.Columns.Contains("ShortNotes"))
                    dgvConsultations.Columns["ShortNotes"].HeaderText = "Diagnosis";
                if (dgvConsultations.Columns.Contains("PatientName"))
                    dgvConsultations.Columns["PatientName"].HeaderText = "Patient";
                if (dgvConsultations.Columns.Contains("ProviderName"))
                    dgvConsultations.Columns["ProviderName"].HeaderText = "Provider";
                if (dgvConsultations.Columns.Contains("FollowUpNeeded"))
                    dgvConsultations.Columns["FollowUpNeeded"].HeaderText = "Follow-Up";
                
                // Set widths
                if (dgvConsultations.Columns.Contains("ConsultationDate"))
                    dgvConsultations.Columns["ConsultationDate"].Width = 150;
                if (dgvConsultations.Columns.Contains("Temperature"))
                    dgvConsultations.Columns["Temperature"].Width = 100;
                if (dgvConsultations.Columns.Contains("BloodPressure"))
                    dgvConsultations.Columns["BloodPressure"].Width = 120;
                if (dgvConsultations.Columns.Contains("ShortNotes"))
                    dgvConsultations.Columns["ShortNotes"].Width = 250;
                if (dgvConsultations.Columns.Contains("PatientName"))
                    dgvConsultations.Columns["PatientName"].Width = 150;
                if (dgvConsultations.Columns.Contains("ProviderName"))
                    dgvConsultations.Columns["ProviderName"].Width = 150;
                if (dgvConsultations.Columns.Contains("FollowUpNeeded"))
                    dgvConsultations.Columns["FollowUpNeeded"].Width = 80;
            }
        }

        private void DgvConsultations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvConsultations.RowCount && isDataLoaded)
            {
                try
                {
                    int consultationId = Convert.ToInt32(dgvConsultations.Rows[e.RowIndex].Cells["ConsultationId"].Value);
                    
                    // Open Consultation Details popup
                    using (var consultationDetailsForm = new frmConsultationDetails(consultationId))
                    {
                        consultationDetailsForm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening consultation details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    LoadConsultations();
                    return;
                }

                string searchTerm = $"%{txtSearch.Text.Trim()}%";
                string sql = @"
                    SELECT 
                        c.ConsultationId,
                        c.AppointmentId,
                        c.ConsultationDate,
                        c.Temperature,
                        c.BloodPressure,
                        LEFT(c.DiagnosisNotes, 100) as ShortNotes,
                        c.DiagnosisNotes,
                        c.FollowUpNeeded,
                        CASE 
                            WHEN a.PatientId IS NOT NULL THEN sp.FullName
                            ELSE gp.FullName
                        END as PatientName,
                        u.FullName as ProviderName
                    FROM Consultations c
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    LEFT JOIN Patients pt ON a.PatientId = pt.PatientId
                    LEFT JOIN Users sp ON pt.UserId = sp.UserId
                    LEFT JOIN GuestPatients gp ON a.GuestPatientId = gp.GuestPatientId
                    LEFT JOIN Users u ON c.ProviderId = u.UserId
                    WHERE (a.PatientId IS NOT NULL AND sp.FullName LIKE @SearchTerm)
                       OR (a.GuestPatientId IS NOT NULL AND gp.FullName LIKE @SearchTerm)
                       OR c.DiagnosisNotes LIKE @SearchTerm
                       OR u.FullName LIKE @SearchTerm
                    ORDER BY c.ConsultationDate DESC";

                var consultations = dbHelper.ExecuteQuery(sql, new { SearchTerm = searchTerm });

                dgvConsultations.DataSource = consultations;
                dgvConsultations.Refresh();

                // Adjust column widths
                if (dgvConsultations.Columns.Count > 0)
                {
                    dgvConsultations.Columns["ConsultationId"].Visible = false;
                    dgvConsultations.Columns["AppointmentId"].Visible = false;
                    dgvConsultations.Columns["DiagnosisNotes"].Visible = false;
                    
                    dgvConsultations.Columns["ConsultationDate"].HeaderText = "Date";
                    dgvConsultations.Columns["ShortNotes"].HeaderText = "Diagnosis";
                    dgvConsultations.Columns["PatientName"].HeaderText = "Patient";
                    dgvConsultations.Columns["ProviderName"].HeaderText = "Provider";
                    dgvConsultations.Columns["FollowUpNeeded"].HeaderText = "Follow-Up";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching consultations: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

