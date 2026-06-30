using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmPrescriptionManagement : Form
    {
        private DatabaseHelper dbHelper;
        private DataGridView dgvPrescriptions;
        private TextBox txtSearch;
        private Label lblTitle;
        private Label lblSearch;
        private Panel pnlTop;
        private bool isDataLoaded = false;

        public frmPrescriptionManagement()
        {
            dbHelper = new DatabaseHelper();
            InitializeComponent();
            SetupForm();
            LoadPrescriptions();
        }

        private void SetupForm()
        {
            this.Text = "Prescription Management";
            this.Size = new Size(1200, 800);
            this.MinimumSize = new Size(1000, 600);
            this.BackColor = Color.FromArgb(248, 249, 250);

            // Main Panel
            pnlTop = new Panel()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None
            };

            // Title
            lblTitle = new Label()
            {
                Text = "All Prescriptions",
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

            // DataGridView for Prescriptions
            dgvPrescriptions = new DataGridView()
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
            dgvPrescriptions.DataBindingComplete += DgvPrescriptions_DataBindingComplete;
            dgvPrescriptions.CellDoubleClick += DgvPrescriptions_CellDoubleClick;

            pnlTop.Controls.Add(lblTitle);
            pnlTop.Controls.Add(pnlSearch);
            pnlTop.Controls.Add(dgvPrescriptions);
            pnlTop.AutoScroll = true;

            this.Controls.Add(pnlTop);
        }

        private void LoadPrescriptions()
        {
            try
            {
                string sql = @"
                    SELECT 
                        p.PrescriptionId,
                        p.ConsultationId,
                        p.Dosage,
                        p.Quantity,
                        LEFT(p.Instructions, 50) as ShortInstructions,
                        p.Instructions,
                        p.DispensedDate,
                        m.MedicationName,
                        CASE 
                            WHEN a.PatientId IS NOT NULL THEN sp.FullName
                            ELSE gp.FullName
                        END as PatientName,
                        u.FullName as PrescribedBy
                    FROM Prescriptions p
                    INNER JOIN Consultations c ON p.ConsultationId = c.ConsultationId
                    INNER JOIN Medications m ON p.MedicationId = m.MedicationId
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    LEFT JOIN Patients pt ON a.PatientId = pt.PatientId
                    LEFT JOIN Users sp ON pt.UserId = sp.UserId
                    LEFT JOIN GuestPatients gp ON a.GuestPatientId = gp.GuestPatientId
                    LEFT JOIN Users u ON c.ProviderId = u.UserId
                    ORDER BY p.DispensedDate DESC";

                var prescriptions = dbHelper.ExecuteQuery(sql, null);
                
                isDataLoaded = true;
                dgvPrescriptions.DataSource = prescriptions;
                dgvPrescriptions.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading prescriptions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DgvPrescriptions_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dgvPrescriptions.Columns.Count > 0)
            {
                if (dgvPrescriptions.Columns.Contains("PrescriptionId"))
                    dgvPrescriptions.Columns["PrescriptionId"].Visible = false;
                if (dgvPrescriptions.Columns.Contains("ConsultationId"))
                    dgvPrescriptions.Columns["ConsultationId"].Visible = false;
                if (dgvPrescriptions.Columns.Contains("Instructions"))
                    dgvPrescriptions.Columns["Instructions"].Visible = false;
                
                if (dgvPrescriptions.Columns.Contains("DispensedDate"))
                    dgvPrescriptions.Columns["DispensedDate"].HeaderText = "Date";
                if (dgvPrescriptions.Columns.Contains("MedicationName"))
                    dgvPrescriptions.Columns["MedicationName"].HeaderText = "Medication";
                if (dgvPrescriptions.Columns.Contains("ShortInstructions"))
                    dgvPrescriptions.Columns["ShortInstructions"].HeaderText = "Instructions";
                if (dgvPrescriptions.Columns.Contains("PatientName"))
                    dgvPrescriptions.Columns["PatientName"].HeaderText = "Patient";
                if (dgvPrescriptions.Columns.Contains("PrescribedBy"))
                    dgvPrescriptions.Columns["PrescribedBy"].HeaderText = "Prescribed By";
                
                // Set widths
                if (dgvPrescriptions.Columns.Contains("DispensedDate"))
                    dgvPrescriptions.Columns["DispensedDate"].Width = 150;
                if (dgvPrescriptions.Columns.Contains("MedicationName"))
                    dgvPrescriptions.Columns["MedicationName"].Width = 200;
                if (dgvPrescriptions.Columns.Contains("Dosage"))
                    dgvPrescriptions.Columns["Dosage"].Width = 100;
                if (dgvPrescriptions.Columns.Contains("Quantity"))
                    dgvPrescriptions.Columns["Quantity"].Width = 80;
                if (dgvPrescriptions.Columns.Contains("ShortInstructions"))
                    dgvPrescriptions.Columns["ShortInstructions"].Width = 250;
                if (dgvPrescriptions.Columns.Contains("PatientName"))
                    dgvPrescriptions.Columns["PatientName"].Width = 150;
                if (dgvPrescriptions.Columns.Contains("PrescribedBy"))
                    dgvPrescriptions.Columns["PrescribedBy"].Width = 150;
            }
        }

        private void DgvPrescriptions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.RowIndex < dgvPrescriptions.RowCount && isDataLoaded)
            {
                try
                {
                    int prescriptionId = Convert.ToInt32(dgvPrescriptions.Rows[e.RowIndex].Cells["PrescriptionId"].Value);
                    
                    // Open Prescription Details popup
                    using (var prescriptionDetailsForm = new frmPrescriptionDetails(prescriptionId))
                    {
                        prescriptionDetailsForm.ShowDialog();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening prescription details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void TxtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtSearch.Text))
                {
                    LoadPrescriptions();
                    return;
                }

                string searchTerm = $"%{txtSearch.Text.Trim()}%";
                string sql = @"
                    SELECT 
                        p.PrescriptionId,
                        p.ConsultationId,
                        p.Dosage,
                        p.Quantity,
                        LEFT(p.Instructions, 50) as ShortInstructions,
                        p.Instructions,
                        p.DispensedDate,
                        m.MedicationName,
                        CASE 
                            WHEN a.PatientId IS NOT NULL THEN sp.FullName
                            ELSE gp.FullName
                        END as PatientName,
                        u.FullName as PrescribedBy
                    FROM Prescriptions p
                    INNER JOIN Consultations c ON p.ConsultationId = c.ConsultationId
                    INNER JOIN Medications m ON p.MedicationId = m.MedicationId
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    LEFT JOIN Patients pt ON a.PatientId = pt.PatientId
                    LEFT JOIN Users sp ON pt.UserId = sp.UserId
                    LEFT JOIN GuestPatients gp ON a.GuestPatientId = gp.GuestPatientId
                    LEFT JOIN Users u ON c.ProviderId = u.UserId
                    WHERE m.MedicationName LIKE @SearchTerm 
                       OR (a.PatientId IS NOT NULL AND sp.FullName LIKE @SearchTerm)
                       OR (a.GuestPatientId IS NOT NULL AND gp.FullName LIKE @SearchTerm)
                       OR p.Instructions LIKE @SearchTerm
                    ORDER BY p.DispensedDate DESC";

                var prescriptions = dbHelper.ExecuteQuery(sql, new { SearchTerm = searchTerm });

                dgvPrescriptions.DataSource = prescriptions;
                dgvPrescriptions.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching prescriptions: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

