using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmPrescriptionDetails : Form
    {
        private DatabaseHelper dbHelper;
        private int _prescriptionId;

        public frmPrescriptionDetails(int prescriptionId)
        {
            InitializeComponent();
            _prescriptionId = prescriptionId;
            dbHelper = new DatabaseHelper();
            SetupForm();
            LoadPrescriptionDetails();
        }

        private void SetupForm()
        {
            this.Text = "Prescription Details";
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
                Text = "Prescription Details",
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

            this.Controls.Add(mainPanel);
            this.Controls.Add(headerPanel);
        }

        private void LoadPrescriptionDetails()
        {
            try
            {
                string sql = @"
                    SELECT 
                        p.PrescriptionId,
                        p.ConsultationId,
                        p.Dosage,
                        p.Quantity,
                        p.Instructions,
                        p.DispensedDate,
                        m.MedicationName,
                        m.Description as MedicationDescription,
                        CASE 
                            WHEN a.PatientId IS NOT NULL THEN u.FullName
                            ELSE gp.FullName
                        END as PatientName,
                        CASE 
                            WHEN a.PatientId IS NOT NULL THEN u.ContactPhone
                            ELSE gp.PhoneNumber
                        END as PatientPhone,
                        CASE
                            WHEN a.PatientId IS NOT NULL THEN 'Student'
                            ELSE 'Guest'
                        END as PatientType,
                        c.DiagnosisNotes,
                        provider.FullName as PrescribedBy,
                        provider.ContactPhone as ProviderPhone,
                        a.AppointmentDate,
                        a.TimeSlot,
                        a.Reason as AppointmentReason
                    FROM Prescriptions p
                    INNER JOIN Consultations c ON p.ConsultationId = c.ConsultationId
                    INNER JOIN Medications m ON p.MedicationId = m.MedicationId
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    LEFT JOIN Patients pt ON a.PatientId = pt.PatientId
                    LEFT JOIN Users u ON pt.UserId = u.UserId
                    LEFT JOIN GuestPatients gp ON a.GuestPatientId = gp.GuestPatientId
                    LEFT JOIN Users provider ON c.ProviderId = provider.UserId
                    WHERE p.PrescriptionId = @PrescriptionId";

                var result = dbHelper.ExecuteQuery(sql, new { PrescriptionId = _prescriptionId });

                if (result.Rows.Count == 0)
                {
                    MessageBox.Show("Prescription not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                var row = result.Rows[0];
                
                // Create UI with controls - find the main scrollable panel
                Panel scrollPanel = null;
                foreach (Control ctrl in this.Controls)
                {
                    if (ctrl is Panel && ctrl.Dock == DockStyle.Fill)
                    {
                        scrollPanel = ctrl as Panel;
                        break;
                    }
                }
                
                if (scrollPanel == null) return;
                scrollPanel.Controls.Clear();
                
                int yPos = 20;
                
                DateTime dispensedDate = row["DispensedDate"] != DBNull.Value ? Convert.ToDateTime(row["DispensedDate"]) : DateTime.Now;
                
                // Header with date badge
                var headerPanel = new Panel()
                {
                    Location = new Point(20, yPos),
                    Size = new Size(810, 50),
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle
                };
                
                var lblHeader = new Label()
                {
                    Text = "Prescription Details",
                    Location = new Point(15, 10),
                    Size = new Size(400, 30),
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(52, 152, 219),
                    AutoSize = false
                };
                
                var lblDate = new Label()
                {
                    Text = dispensedDate.ToString("dd MMM yyyy"),
                    Location = new Point(425, 10),
                    Size = new Size(200, 30),
                    Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    TextAlign = ContentAlignment.MiddleRight,
                    AutoSize = false
                };
                
                var lblTime = new Label()
                {
                    Text = dispensedDate.ToString("HH:mm"),
                    Location = new Point(630, 10),
                    Size = new Size(100, 30),
                    Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    TextAlign = ContentAlignment.MiddleLeft,
                    AutoSize = false
                };
                
                headerPanel.Controls.Add(lblHeader);
                headerPanel.Controls.Add(lblDate);
                headerPanel.Controls.Add(lblTime);
                scrollPanel.Controls.Add(headerPanel);
                yPos += 60;

                // Patient Information Section
                AddSection(ref yPos, scrollPanel, "PATIENT INFORMATION", new[] {
                    new[] { "Patient Name:", row["PatientName"]?.ToString() ?? "N/A" },
                    new[] { "Contact:", row["PatientPhone"]?.ToString() ?? "N/A" },
                    new[] { "Patient Type:", row["PatientType"]?.ToString() ?? "N/A" }
                });

                // Appointment Details Section
                DateTime apptDate = row["AppointmentDate"] != DBNull.Value ? Convert.ToDateTime(row["AppointmentDate"]) : DateTime.Now;
                
                AddSection(ref yPos, scrollPanel, "APPOINTMENT DETAILS", new[] {
                    new[] { "Date:", apptDate.ToString("yyyy-MM-dd") },
                    new[] { "Time:", row["TimeSlot"]?.ToString() ?? "N/A" },
                    new[] { "Reason:", row["AppointmentReason"]?.ToString() ?? "N/A" },
                    new[] { "Diagnosis:", row["DiagnosisNotes"]?.ToString() ?? "N/A" }
                });

                // Prescription Section
                AddSection(ref yPos, scrollPanel, "PRESCRIPTION DETAILS", new[] {
                    new[] { "Medication:", row["MedicationName"]?.ToString() ?? "N/A" },
                    new[] { "Description:", row["MedicationDescription"]?.ToString() ?? "N/A" },
                    new[] { "Dosage:", row["Dosage"]?.ToString() ?? "N/A" },
                    new[] { "Quantity:", row["Quantity"]?.ToString() ?? "N/A" }
                });

                // Instructions Section
                yPos += 20;
                AddInstructionsSection(ref yPos, scrollPanel, row["Instructions"]?.ToString() ?? "No special instructions.");

                // Prescribed By Section
                AddSection(ref yPos, scrollPanel, "PRESCRIBED BY", new[] {
                    new[] { "Name:", row["PrescribedBy"]?.ToString() ?? "N/A" },
                    new[] { "Contact:", row["ProviderPhone"]?.ToString() ?? "N/A" }
                });

                // Button Panel at bottom
                var buttonPanel = new Panel()
                {
                    Height = 60,
                    Dock = DockStyle.Bottom,
                    BackColor = Color.FromArgb(248, 249, 250),
                    Padding = new Padding(20)
                };

                var btnClose = new Button()
                {
                    Text = "Close",
                    Size = new Size(100, 35),
                    Location = new Point(800, 12),
                    BackColor = Color.FromArgb(108, 117, 125),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 10F)
                };
                btnClose.FlatAppearance.BorderSize = 0;
                btnClose.Click += (s, e) => this.Close();
                
                buttonPanel.Controls.Add(btnClose);
                this.Controls.Add(buttonPanel);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading prescription details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddSection(ref int yPos, Panel panel, string title, string[][] fields)
        {
            // Section Container with styled border and background
            var sectionPanel = new Panel()
            {
                Location = new Point(0, yPos),
                Size = new Size(850, 40 + fields.Length * 35 + 20),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(0, 0, 0, 20),
                BackColor = Color.FromArgb(250, 252, 255)
            };

            // Section Header with colored background - flush with border
            var headerPanel = new Panel()
            {
                Location = new Point(0, 0),
                Size = new Size(850, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                Padding = new Padding(20, 10, 0, 0)
            };

            var lblSection = new Label()
            {
                Text = title,
                Location = new Point(20, 10),
                Size = new Size(800, 20),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false
            };
            headerPanel.Controls.Add(lblSection);
            sectionPanel.Controls.Add(headerPanel);

            // Fields with alternating row colors
            int fieldY = 45;
            bool isAlternate = false;
            
            foreach (var field in fields)
            {
                if (field.Length == 2)
                {
                    // Field container with alternating background
                    var fieldContainer = new Panel()
                    {
                        Location = new Point(20, fieldY),
                        Size = new Size(810, 30),
                        BackColor = isAlternate ? Color.FromArgb(245, 248, 250) : Color.White
                    };

                    var lbl = new Label()
                    {
                        Text = field[0],
                        Location = new Point(10, 5),
                        Size = new Size(160, 20),
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                        AutoSize = false,
                        ForeColor = Color.FromArgb(70, 70, 70)
                    };
                    fieldContainer.Controls.Add(lbl);

                    var lblValue = new Label()
                    {
                        Text = field[1],
                        Location = new Point(180, 5),
                        Size = new Size(620, 20),
                        Font = new Font("Segoe UI", 10F),
                        AutoSize = true,
                        ForeColor = Color.FromArgb(50, 50, 50)
                    };
                    fieldContainer.Controls.Add(lblValue);
                    
                    sectionPanel.Controls.Add(fieldContainer);
                    fieldY += 35;
                    isAlternate = !isAlternate;
                }
            }

            sectionPanel.Height = fieldY + 10;
            panel.Controls.Add(sectionPanel);
            yPos += sectionPanel.Height + 20;
        }

        private void AddInstructionsSection(ref int yPos, Panel panel, string instructions)
        {
            // Section Container
            var sectionPanel = new Panel()
            {
                Location = new Point(0, yPos),
                Size = new Size(850, 150),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(0, 0, 0, 20),
                BackColor = Color.FromArgb(250, 252, 255)
            };

            // Section Header - flush with border
            var headerPanel = new Panel()
            {
                Location = new Point(0, 0),
                Size = new Size(850, 40),
                BackColor = Color.FromArgb(52, 152, 219),
                Padding = new Padding(20, 10, 0, 0)
            };

            var lblSection = new Label()
            {
                Text = "INSTRUCTIONS",
                Location = new Point(20, 10),
                Size = new Size(800, 20),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false
            };
            headerPanel.Controls.Add(lblSection);
            sectionPanel.Controls.Add(headerPanel);

            // Instructions content in a multiline panel
            var instructionsPanel = new Panel()
            {
                Location = new Point(20, 50),
                Size = new Size(810, 80),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            var lblInstructions = new Label()
            {
                Text = instructions,
                Location = new Point(10, 10),
                Size = new Size(790, 60),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(50, 50, 50),
                AutoSize = true,
                MaximumSize = new Size(790, 0)
            };
            instructionsPanel.Controls.Add(lblInstructions);
            sectionPanel.Controls.Add(instructionsPanel);

            // Auto-adjust height based on content
            int textHeight = TextRenderer.MeasureText(instructions, new Font("Segoe UI", 10F), new Size(790, 0), TextFormatFlags.WordBreak).Height;
            instructionsPanel.Height = Math.Min(80, textHeight + 20);
            sectionPanel.Height = instructionsPanel.Height + 80;

            panel.Controls.Add(sectionPanel);
            yPos += sectionPanel.Height + 20;
        }
    }
}

