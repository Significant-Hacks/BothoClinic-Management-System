using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmConsultationDetails : Form
    {
        private DatabaseHelper dbHelper;
        private int _consultationId;

        public frmConsultationDetails(int consultationId)
        {
            InitializeComponent();
            _consultationId = consultationId;
            dbHelper = new DatabaseHelper();
            SetupForm();
            LoadConsultationDetails();
        }

        private void SetupForm()
        {
            this.Text = "Consultation Details";
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
                Text = "Consultation Details",
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

        private void LoadConsultationDetails()
        {
            try
            {
                string sql = @"
                    SELECT 
                        c.ConsultationId,
                        c.ConsultationDate,
                        c.Temperature,
                        c.BloodPressure,
                        c.VitalsJson,
                        c.DiagnosisNotes,
                        c.FollowUpNeeded,
                        a.AppointmentDate,
                        a.TimeSlot,
                        a.Reason as AppointmentReason,
                        a.Status as AppointmentStatus,
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
                        provider.FullName as ProviderName,
                        provider.ContactPhone as ProviderPhone
                    FROM Consultations c
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    LEFT JOIN Patients pt ON a.PatientId = pt.PatientId
                    LEFT JOIN Users u ON pt.UserId = u.UserId
                    LEFT JOIN GuestPatients gp ON a.GuestPatientId = gp.GuestPatientId
                    LEFT JOIN Users provider ON c.ProviderId = provider.UserId
                    WHERE c.ConsultationId = @ConsultationId";

                var result = dbHelper.ExecuteQuery(sql, new { ConsultationId = _consultationId });

                if (result.Rows.Count == 0)
                {
                    MessageBox.Show("Consultation not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                var row = result.Rows[0];
                
                // Create UI with controls - find the main scrollable panel (it's the first one with Dock.Fill)
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
                    Text = "Consultation Details",
                    Location = new Point(15, 10),
                    Size = new Size(400, 30),
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                    ForeColor = Color.FromArgb(52, 152, 219),
                    AutoSize = false
                };
                
                DateTime consultDate = row["ConsultationDate"] != DBNull.Value ? Convert.ToDateTime(row["ConsultationDate"]) : DateTime.Now;
                
                var lblDate = new Label()
                {
                    Text = consultDate.ToString("dd MMM yyyy"),
                    Location = new Point(425, 10),
                    Size = new Size(200, 30),
                    Font = new Font("Segoe UI", 11F, FontStyle.Regular),
                    ForeColor = Color.FromArgb(108, 117, 125),
                    TextAlign = ContentAlignment.MiddleRight,
                    AutoSize = false
                };
                
                var lblTime = new Label()
                {
                    Text = consultDate.ToString("HH:mm"),
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
                    new[] { "Status:", row["AppointmentStatus"]?.ToString() ?? "N/A" }
                });

                // Vitals Section
                string temp = row["Temperature"] != DBNull.Value && row["Temperature"] != null 
                    ? $"{row["Temperature"]}°C" 
                    : "N/A";
                string bp = row["BloodPressure"]?.ToString() ?? "N/A";
                
                AddSection(ref yPos, scrollPanel, "VITALS", new[] {
                    new[] { "Consultation Date:", consultDate.ToString("yyyy-MM-dd HH:mm") },
                    new[] { "Temperature:", temp },
                    new[] { "Blood Pressure:", bp }
                });

                if (row["VitalsJson"] != DBNull.Value && !string.IsNullOrEmpty(row["VitalsJson"]?.ToString()))
                {
                    AddField(ref yPos, scrollPanel, "Additional Vitals:", row["VitalsJson"].ToString());
                }

                // Diagnosis Notes Section - Special handling for long text
                yPos += 20;
                AddClinicalNotesSection(ref yPos, scrollPanel, row["DiagnosisNotes"]?.ToString() ?? "No notes available.");

                // Follow-Up Section with styled panel
                if (row["FollowUpNeeded"] != DBNull.Value && Convert.ToBoolean(row["FollowUpNeeded"]))
                {
                    yPos += 20;
                    var followUpPanel = new Panel()
                    {
                        Location = new Point(20, yPos),
                        Size = new Size(810, 50),
                        BackColor = Color.FromArgb(255, 243, 224),
                        BorderStyle = BorderStyle.FixedSingle,
                        Padding = new Padding(15)
                    };
                    
                    var lblFollowUp = new Label()
                    {
                        Text = "⚠ FOLLOW-UP REQUIRED",
                        Location = new Point(30, 12),
                        Size = new Size(780, 25),
                        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                        ForeColor = Color.FromArgb(230, 126, 34)
                    };
                    followUpPanel.Controls.Add(lblFollowUp);
                    scrollPanel.Controls.Add(followUpPanel);
                    yPos += 60;
                }

                // Provider Section
                AddSection(ref yPos, scrollPanel, "PROVIDER", new[] {
                    new[] { "Name:", row["ProviderName"]?.ToString() ?? "N/A" },
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
                MessageBox.Show($"Error loading consultation details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddSection(ref int yPos, Panel panel, string title, string[][] fields)
        {
            // Section Container with styled border and background
            var sectionPanel = new Panel()
            {
                Location = new Point(0, yPos),
                Size = new Size(850, 40 + fields.Length * 35 + 20), // Header (40) + fields + bottom padding
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(0, 0, 0, 20), // Only bottom padding
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
            int fieldY = 45; // Start below the blue header (40px) + 5px spacing
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

        private void AddField(ref int yPos, Panel panel, string label, string value)
        {
            var lbl = new Label()
            {
                Text = label,
                Location = new Point(20, yPos), // Indented from form margin
                Size = new Size(150, 25),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                AutoSize = false
            };
            panel.Controls.Add(lbl);

            var lblValue = new Label()
            {
                Text = value,
                Location = new Point(180, yPos), // Indented from form margin
                Size = new Size(660, 25), // Adjusted to leave space on right
                Font = new Font("Segoe UI", 10F),
                AutoSize = true
            };
            panel.Controls.Add(lblValue);

            yPos += 30;
        }

        private void AddClinicalNotesSection(ref int yPos, Panel panel, string notes)
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
                Text = "CLINICAL NOTES",
                Location = new Point(20, 10),
                Size = new Size(800, 20),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = false
            };
            headerPanel.Controls.Add(lblSection);
            sectionPanel.Controls.Add(headerPanel);

            // Notes content in a multiline textbox-like panel
            var notesPanel = new Panel()
            {
                Location = new Point(20, 50),
                Size = new Size(810, 80),
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10)
            };

            var lblNotes = new Label()
            {
                Text = notes,
                Location = new Point(10, 10),
                Size = new Size(790, 60),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(50, 50, 50),
                AutoSize = true,
                MaximumSize = new Size(790, 0)
            };
            notesPanel.Controls.Add(lblNotes);
            sectionPanel.Controls.Add(notesPanel);

            // Auto-adjust height based on content
            int textHeight = TextRenderer.MeasureText(notes, new Font("Segoe UI", 10F), new Size(790, 0), TextFormatFlags.WordBreak).Height;
            notesPanel.Height = Math.Min(80, textHeight + 20);
            sectionPanel.Height = notesPanel.Height + 80; // Header (40) + notes + bottom padding

            panel.Controls.Add(sectionPanel);
            yPos += sectionPanel.Height + 20;
        }
    }
}

