using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Drawing.Printing;

namespace BothoClinic
{
    public partial class frmReportDetails : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        private readonly int _patientId;
        private readonly bool _isStudent;
        private Panel mainPanel;
        private Button btnClose;
        private Button btnExportCSV;
        private Button btnExportPDF;

        private DataRow _patientData;
        private DataTable _appointmentsData;
        private DataTable _consultationsData;
        private DataTable _prescriptionsData;

        public frmReportDetails(int patientId, bool isStudent)
        {
            _patientId = patientId;
            _isStudent = isStudent;
            InitializeComponent();
            LoadReport();
        }

        private void InitializeComponent()
        {
            this.Text = "Medical Report";
            this.Size = new Size(1000, 800);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Main panel for scrollable content
            mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20)
            };

            // Button panel at bottom
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(245, 245, 245),
                Padding = new Padding(10)
            };

            btnExportCSV = new Button
            {
                Text = "Export CSV",
                Size = new Size(120, 35),
                Location = new Point(700, 8),
                BackColor = Color.FromArgb(46, 204, 113),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnExportCSV.Click += BtnExportCSV_Click;
            btnExportCSV.FlatAppearance.BorderSize = 0;

            btnExportPDF = new Button
            {
                Text = "Export PDF",
                Size = new Size(120, 35),
                Location = new Point(830, 8),
                BackColor = Color.FromArgb(231, 76, 60),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold)
            };
            btnExportPDF.Click += BtnExportPDF_Click;
            btnExportPDF.FlatAppearance.BorderSize = 0;

            btnClose = new Button
            {
                Text = "Close",
                Size = new Size(120, 35),
                Location = new Point(960, 8),
                BackColor = Color.FromArgb(149, 165, 166),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                DialogResult = DialogResult.Cancel
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Click += (s, e) => this.Close();

            buttonPanel.Controls.Add(btnExportCSV);
            buttonPanel.Controls.Add(btnExportPDF);
            buttonPanel.Controls.Add(btnClose);

            this.Controls.Add(mainPanel);
            this.Controls.Add(buttonPanel);
        }

        private void LoadReport()
        {
            try
            {
                DataTable patient = GetPatientInfo();
                if (patient == null || patient.Rows.Count == 0)
                {
                    Label errorLabel = new Label
                    {
                        Text = "Patient information not found.",
                        Location = new Point(100, 100),
                        Size = new Size(600, 50),
                        Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                        ForeColor = Color.Red
                    };
                    mainPanel.Controls.Add(errorLabel);
                    return;
                }

                int yPos = 20;
                DataRow patientRow = patient.Rows[0];

                // Title
                AddHeader(ref yPos, mainPanel, "MEDICAL REPORT");

                // Provider Information
                AddSection(ref yPos, mainPanel, "PROVIDER INFORMATION", new[] {
                    new[] { "Provider:", UserSession.CurrentUser?.FullName ?? "Dr. Sarah Johnson" }
                });

                // Patient Information
                AddSection(ref yPos, mainPanel, "PATIENT INFORMATION", new[] {
                    new[] { "Name:", patientRow["FullName"]?.ToString() ?? "N/A" },
                    new[] { "Type:", _isStudent ? "Student" : "Guest" },
                    new[] { "ID:", patientRow["StudentId"]?.ToString() ?? "N/A" },
                    new[] { "Contact:", patientRow["ContactPhone"]?.ToString() ?? "N/A" }
                });

                if (_isStudent && patientRow.Table.Columns.Contains("ContactEmail") && !string.IsNullOrEmpty(patientRow["ContactEmail"]?.ToString()))
                {
                    AddField(ref yPos, mainPanel, "Email:", patientRow["ContactEmail"]?.ToString() ?? "N/A");
                }

                AddField(ref yPos, mainPanel, "Report Generated:", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

                // Get and display consultations
                DataTable consultations = GetPatientConsultations(_patientId, _isStudent);
                if (consultations != null && consultations.Rows.Count > 0)
                {
                    AddSection(ref yPos, mainPanel, "CONSULTATION RECORDS", null);
                    
                    foreach (DataRow consult in consultations.Rows)
                    {
                        DateTime consultDate = Convert.ToDateTime(consult["ConsultationDate"]);
                        AddField(ref yPos, mainPanel, "Date:", consultDate.ToString("yyyy-MM-dd HH:mm"));
                        if (consult["Temperature"] != DBNull.Value)
                        {
                            AddField(ref yPos, mainPanel, "Temperature:", consult["Temperature"]?.ToString() + "°C");
                        }
                        if (consult["BloodPressure"] != DBNull.Value)
                        {
                            AddField(ref yPos, mainPanel, "Blood Pressure:", consult["BloodPressure"]?.ToString());
                        }
                        AddField(ref yPos, mainPanel, "Diagnosis:", consult["DiagnosisNotes"]?.ToString() ?? "N/A");
                        AddField(ref yPos, mainPanel, "Follow-Up:", (consult["FollowUpNeeded"].ToString() == "True" ? "Required" : "Not Required"));
                        yPos += 10;
                    }
                }

                // Get and display prescriptions
                DataTable prescriptions = GetPatientPrescriptions(_patientId, _isStudent);
                if (prescriptions != null && prescriptions.Rows.Count > 0)
                {
                    AddSection(ref yPos, mainPanel, "PRESCRIPTION HISTORY", null);
                    
                    foreach (DataRow rx in prescriptions.Rows)
                    {
                        DateTime dispDate = Convert.ToDateTime(rx["DispensedDate"]);
                        AddField(ref yPos, mainPanel, "Dispensed:", dispDate.ToString("yyyy-MM-dd"));
                        AddField(ref yPos, mainPanel, "Medication:", rx["MedicationName"]?.ToString() ?? "N/A");
                        AddField(ref yPos, mainPanel, "Dosage:", rx["Dosage"]?.ToString() ?? "N/A");
                        AddField(ref yPos, mainPanel, "Quantity:", rx["Quantity"]?.ToString() ?? "N/A");
                        AddField(ref yPos, mainPanel, "Instructions:", rx["Instructions"]?.ToString() ?? "N/A");
                        yPos += 10;
                    }
                }

                // Final header
                AddEndHeader(ref yPos, mainPanel, "END OF REPORT");

                // Store data for export
                _patientData = patient.Rows[0];
                _appointmentsData = GetPatientAppointments(_patientId, _isStudent);
                _consultationsData = consultations;
                _prescriptionsData = prescriptions;
            }
            catch (Exception ex)
            {
                Label errorLabel = new Label
                {
                    Text = $"Error loading report: {ex.Message}",
                    Location = new Point(50, 100),
                    Size = new Size(800, 100),
                    Font = new Font("Segoe UI", 11F),
                    ForeColor = Color.Red
                };
                mainPanel.Controls.Add(errorLabel);
            }
        }

        private void AddHeader(ref int yPos, Panel parent, string title)
        {
            Label lblHeader = new Label
            {
                Text = title,
                Location = new Point(0, yPos),
                Size = new Size(900, 40),
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219),
                TextAlign = ContentAlignment.MiddleCenter
            };
            parent.Controls.Add(lblHeader);
            yPos += 50;
        }

        private void AddEndHeader(ref int yPos, Panel parent, string title)
        {
            yPos += 20;
            Label lblEnd = new Label
            {
                Text = title,
                Location = new Point(0, yPos),
                Size = new Size(900, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(149, 165, 166),
                TextAlign = ContentAlignment.MiddleCenter
            };
            parent.Controls.Add(lblEnd);
            yPos += 40;
        }

        private void AddSection(ref int yPos, Panel parent, string sectionTitle, string[][] fields)
        {
            // Section panel with border
            Panel sectionPanel = new Panel
            {
                Location = new Point(0, yPos),
                Size = new Size(900, 0),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                Padding = new Padding(20, 50, 20, 20)
            };

            // Blue header bar
            Panel headerPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(900, 35),
                BackColor = Color.FromArgb(52, 152, 219)
            };
            sectionPanel.Controls.Add(headerPanel);

            Label headerLabel = new Label
            {
                Text = sectionTitle,
                Location = new Point(10, 5),
                Size = new Size(880, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White
            };
            headerPanel.Controls.Add(headerLabel);

            int contentY = 60;

            if (fields != null)
            {
                foreach (var field in fields)
                {
                    AddField(ref contentY, sectionPanel, field[0], field[1]);
                }
            }

            sectionPanel.Height = contentY + 30;
            parent.Controls.Add(sectionPanel);
            yPos += sectionPanel.Height + 20;
        }

        private void AddField(ref int yPos, Panel parent, string label, string value)
        {
            Label lblName = new Label
            {
                Text = label,
                Location = new Point(20, yPos),
                Size = new Size(120, 20),
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };

            // Calculate how much space is available for the value text
            int maxWidth = 700;
            int valueStartX = 150;
            
            // Check if the text fits on one line
            using (Graphics g = parent.CreateGraphics())
            {
                Font valueFont = new Font("Segoe UI", 10F);
                SizeF textSize = g.MeasureString(value, valueFont);
                
                Label lblValue = new Label
                {
                    Text = value,
                    Location = new Point(valueStartX, yPos),
                    Font = valueFont,
                    AutoSize = false
                };

                if (textSize.Width <= maxWidth)
                {
                    // Text fits on one line
                    lblValue.Size = new Size(maxWidth, 20);
                    parent.Controls.Add(lblName);
                    parent.Controls.Add(lblValue);
                    yPos += 25;
                }
                else
                {
                    // Text needs multiple lines - measure and calculate height
                    StringFormat format = new StringFormat(StringFormatFlags.NoClip);
                    format.Trimming = StringTrimming.Word;
                    
                    RectangleF textRect = new RectangleF(0, 0, maxWidth, 1000);
                    SizeF measuredSize = g.MeasureString(value, valueFont, textRect.Size, format);
                    int textHeight = (int)Math.Ceiling(measuredSize.Height);
                    
                    // Text needs multiple lines - enable word wrapping
                    lblValue.Size = new Size(maxWidth, textHeight);
                    
                    parent.Controls.Add(lblName);
                    parent.Controls.Add(lblValue);
                    
                    // Update yPos based on the calculated height
                    yPos += textHeight + 5;
                }
            }
        }

        private DataTable GetPatientInfo()
        {
            if (_isStudent)
            {
                string sql = @"
                    SELECT 
                        pt.PatientId,
                        u.FullName,
                        u.ContactPhone,
                        u.ContactEmail,
                        pt.StudentId
                    FROM Patients pt
                    INNER JOIN Users u ON pt.UserId = u.UserId
                    WHERE pt.PatientId = @PatientId";

                return dbHelper.ExecuteQuery(sql, new { PatientId = _patientId });
            }
            else
            {
                string sql = @"
                    SELECT 
                        gp.GuestPatientId as PatientId,
                        gp.FullName,
                        gp.PhoneNumber as ContactPhone,
                        NULL as ContactEmail,
                        gp.EmergencyPhone as StudentId
                    FROM GuestPatients gp
                    WHERE gp.GuestPatientId = @PatientId";

                return dbHelper.ExecuteQuery(sql, new { PatientId = _patientId });
            }
        }

        private DataTable GetPatientAppointments(int patientId, bool isStudent)
        {
            string sql;
            if (isStudent)
            {
                sql = @"
                    SELECT 
                        a.AppointmentDate,
                        a.TimeSlot,
                        a.Reason,
                        a.Status,
                        provider.FullName as ProviderName
                    FROM Appointments a
                    LEFT JOIN Users provider ON a.ProviderId = provider.UserId
                    WHERE a.PatientId = @PatientId
                    ORDER BY a.AppointmentDate DESC, a.TimeSlot DESC";
            }
            else
            {
                sql = @"
                    SELECT 
                        a.AppointmentDate,
                        a.TimeSlot,
                        a.Reason,
                        a.Status,
                        provider.FullName as ProviderName
                    FROM Appointments a
                    LEFT JOIN Users provider ON a.ProviderId = provider.UserId
                    WHERE a.GuestPatientId = @PatientId
                    ORDER BY a.AppointmentDate DESC, a.TimeSlot DESC";
            }

            return dbHelper.ExecuteQuery(sql, new { PatientId = patientId });
        }

        private DataTable GetPatientConsultations(int patientId, bool isStudent)
        {
            string sql;
            if (isStudent)
            {
                sql = @"
                    SELECT 
                        c.ConsultationDate,
                        c.Temperature,
                        c.BloodPressure,
                        c.DiagnosisNotes,
                        c.FollowUpNeeded
                    FROM Consultations c
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    WHERE a.PatientId = @PatientId
                    ORDER BY c.ConsultationDate DESC";
            }
            else
            {
                sql = @"
                    SELECT 
                        c.ConsultationDate,
                        c.Temperature,
                        c.BloodPressure,
                        c.DiagnosisNotes,
                        c.FollowUpNeeded
                    FROM Consultations c
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    WHERE a.GuestPatientId = @PatientId
                    ORDER BY c.ConsultationDate DESC";
            }

            return dbHelper.ExecuteQuery(sql, new { PatientId = patientId });
        }

        private DataTable GetPatientPrescriptions(int patientId, bool isStudent)
        {
            string sql;
            if (isStudent)
            {
                sql = @"
                    SELECT 
                        pr.DispensedDate,
                        m.MedicationName,
                        pr.Dosage,
                        pr.Quantity,
                        pr.Instructions
                    FROM Prescriptions pr
                    INNER JOIN Medications m ON pr.MedicationId = m.MedicationId
                    INNER JOIN Consultations c ON pr.ConsultationId = c.ConsultationId
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    WHERE a.PatientId = @PatientId
                    ORDER BY pr.DispensedDate DESC";
            }
            else
            {
                sql = @"
                    SELECT 
                        pr.DispensedDate,
                        m.MedicationName,
                        pr.Dosage,
                        pr.Quantity,
                        pr.Instructions
                    FROM Prescriptions pr
                    INNER JOIN Medications m ON pr.MedicationId = m.MedicationId
                    INNER JOIN Consultations c ON pr.ConsultationId = c.ConsultationId
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    WHERE a.GuestPatientId = @PatientId
                    ORDER BY pr.DispensedDate DESC";
            }

            return dbHelper.ExecuteQuery(sql, new { PatientId = patientId });
        }

        private void BtnExportCSV_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                    Title = "Export Report as CSV",
                    FileName = $"MedicalReport_{_patientData["FullName"]}_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    StringBuilder report = new StringBuilder();
                    
                    // Build report text
                    report.AppendLine("═══════════════════════════════════════════════════════════════════");
                    report.AppendLine("                      MEDICAL REPORT");
                    report.AppendLine("═══════════════════════════════════════════════════════════════════");
                    report.AppendLine();
                    report.AppendLine("PATIENT INFORMATION");
                    report.AppendLine("─────────────────────────────────────────────────────────────────────────");
                    report.AppendLine($"Name: {_patientData["FullName"]}");
                    report.AppendLine($"Type: {(_isStudent ? "Student" : "Guest")}");
                    report.AppendLine($"ID: {_patientData["StudentId"]}");
                    report.AppendLine($"Contact: {_patientData["ContactPhone"]}");
                    report.AppendLine($"Report Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                    report.AppendLine();

                    // Add consultations
                    if (_consultationsData != null && _consultationsData.Rows.Count > 0)
                    {
                        report.AppendLine("CONSULTATION RECORDS");
                        report.AppendLine("─────────────────────────────────────────────────────────────────────────");
                        foreach (DataRow consult in _consultationsData.Rows)
                        {
                            DateTime consultDate = Convert.ToDateTime(consult["ConsultationDate"]);
                            report.AppendLine($"Date: {consultDate:yyyy-MM-dd HH:mm}");
                            if (consult["Temperature"] != DBNull.Value)
                            {
                                report.AppendLine($"Temperature: {consult["Temperature"]}°C");
                            }
                            if (consult["BloodPressure"] != DBNull.Value)
                            {
                                report.AppendLine($"Blood Pressure: {consult["BloodPressure"]}");
                            }
                            report.AppendLine($"Diagnosis: {consult["DiagnosisNotes"]}");
                            report.AppendLine($"Follow-Up: {(consult["FollowUpNeeded"].ToString() == "True" ? "Required" : "Not Required")}");
                            report.AppendLine();
                        }
                    }

                    // Add prescriptions
                    if (_prescriptionsData != null && _prescriptionsData.Rows.Count > 0)
                    {
                        report.AppendLine("PRESCRIPTION HISTORY");
                        report.AppendLine("─────────────────────────────────────────────────────────────────────────");
                        foreach (DataRow rx in _prescriptionsData.Rows)
                        {
                            DateTime dispDate = Convert.ToDateTime(rx["DispensedDate"]);
                            report.AppendLine($"Dispensed: {dispDate:yyyy-MM-dd}");
                            report.AppendLine($"Medication: {rx["MedicationName"]}");
                            report.AppendLine($"Dosage: {rx["Dosage"]}");
                            report.AppendLine($"Quantity: {rx["Quantity"]}");
                            report.AppendLine($"Instructions: {rx["Instructions"]}");
                            report.AppendLine();
                        }
                    }

                    report.AppendLine("═══════════════════════════════════════════════════════════════════");
                    report.AppendLine("                   END OF REPORT");
                    report.AppendLine("═══════════════════════════════════════════════════════════════════");

                    File.WriteAllText(saveDialog.FileName, report.ToString());
                    MessageBox.Show("Report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnExportPDF_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveDialog = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf|All files (*.*)|*.*",
                    Title = "Export Report as PDF",
                    FileName = $"MedicalReport_{_patientData["FullName"]}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf"
                };

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    // Create a print document
                    PrintDocument printDocument = new PrintDocument();
                    printDocument.PrintPage += PrintDocument_PrintPage;

                    // Find Microsoft Print to PDF printer
                    string pdfPrinter = null;
                    foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                    {
                        if (printer.Contains("PDF") || printer.Contains("pdf"))
                        {
                            pdfPrinter = printer;
                            break;
                        }
                    }

                    if (string.IsNullOrEmpty(pdfPrinter))
                    {
                        MessageBox.Show("No PDF printer found. Please install 'Microsoft Print to PDF' or another PDF printer.", "PDF Printer Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Set the printer and print to file
                    printDocument.PrinterSettings.PrinterName = pdfPrinter;
                    printDocument.PrinterSettings.PrintToFile = true;
                    printDocument.PrinterSettings.PrintFileName = saveDialog.FileName;

                    // Print without showing dialog
                    printDocument.Print();
                    MessageBox.Show("PDF exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error exporting PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                Graphics g = e.Graphics;
                int yPos = 50;
                float leftMargin = e.MarginBounds.Left;
                float pageWidth = e.MarginBounds.Width;

                // Create fonts
                Font headerFont = new Font("Segoe UI", 20F, FontStyle.Bold);
                Font sectionFont = new Font("Segoe UI", 14F, FontStyle.Bold);
                Font labelFont = new Font("Segoe UI", 11F, FontStyle.Bold);
                Font valueFont = new Font("Segoe UI", 11F);

                // Colors
                Brush headerBrush = new SolidBrush(Color.FromArgb(52, 152, 219));
                Brush sectionBrush = new SolidBrush(Color.FromArgb(52, 152, 219));
                Brush textBrush = new SolidBrush(Color.Black);

                // Title
                StringFormat centerFormat = new StringFormat { Alignment = StringAlignment.Center };
                g.DrawString("MEDICAL REPORT", headerFont, headerBrush, e.MarginBounds.Width / 2, yPos, centerFormat);
                yPos += 40;

                // Provider Information
                yPos += 20;
                Rectangle sectionRect = new Rectangle(50, yPos, e.MarginBounds.Width - 100, 30);
                g.FillRectangle(sectionBrush, sectionRect);
                g.DrawString("PROVIDER INFORMATION", sectionFont, Brushes.White, new PointF(60, yPos + 5));
                yPos += 50;
                g.DrawString("Provider:", labelFont, textBrush, leftMargin, yPos);
                g.DrawString(UserSession.CurrentUser?.FullName ?? "Dr. Sarah Johnson", valueFont, textBrush, leftMargin + 150, yPos);
                yPos += 40;

                // Patient Information
                yPos += 20;
                sectionRect = new Rectangle(50, yPos, e.MarginBounds.Width - 100, 30);
                g.FillRectangle(sectionBrush, sectionRect);
                g.DrawString("PATIENT INFORMATION", sectionFont, Brushes.White, new PointF(60, yPos + 5));
                yPos += 50;
                DrawField(g, "Name:", _patientData["FullName"]?.ToString() ?? "N/A", labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                DrawField(g, "Type:", _isStudent ? "Student" : "Guest", labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                DrawField(g, "ID:", _patientData["StudentId"]?.ToString() ?? "N/A", labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                DrawField(g, "Contact:", _patientData["ContactPhone"]?.ToString() ?? "N/A", labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                DrawField(g, "Report Generated:", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), labelFont, valueFont, ref yPos, leftMargin, pageWidth);

                // Consultations
                if (_consultationsData != null && _consultationsData.Rows.Count > 0)
                {
                    yPos += 20;
                    sectionRect = new Rectangle(50, yPos, e.MarginBounds.Width - 100, 30);
                    g.FillRectangle(sectionBrush, sectionRect);
                    g.DrawString("CONSULTATION RECORDS", sectionFont, Brushes.White, new PointF(60, yPos + 5));
                    yPos += 50;

                    foreach (DataRow consult in _consultationsData.Rows)
                    {
                        DateTime consultDate = Convert.ToDateTime(consult["ConsultationDate"]);
                        DrawField(g, "Date:", consultDate.ToString("yyyy-MM-dd HH:mm"), labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        if (consult["Temperature"] != DBNull.Value)
                        {
                            DrawField(g, "Temperature:", consult["Temperature"]?.ToString() + "°C", labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        }
                        if (consult["BloodPressure"] != DBNull.Value)
                        {
                            DrawField(g, "Blood Pressure:", consult["BloodPressure"]?.ToString(), labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        }
                        string diagnosis = consult["DiagnosisNotes"]?.ToString() ?? "N/A";
                        DrawField(g, "Diagnosis:", diagnosis, labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        DrawField(g, "Follow-Up:", (consult["FollowUpNeeded"].ToString() == "True" ? "Required" : "Not Required"), labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        yPos += 10;
                    }
                }

                // Prescriptions
                if (_prescriptionsData != null && _prescriptionsData.Rows.Count > 0)
                {
                    yPos += 20;
                    sectionRect = new Rectangle(50, yPos, e.MarginBounds.Width - 100, 30);
                    g.FillRectangle(sectionBrush, sectionRect);
                    g.DrawString("PRESCRIPTION HISTORY", sectionFont, Brushes.White, new PointF(60, yPos + 5));
                    yPos += 50;

                    foreach (DataRow rx in _prescriptionsData.Rows)
                    {
                        DateTime dispDate = Convert.ToDateTime(rx["DispensedDate"]);
                        DrawField(g, "Dispensed:", dispDate.ToString("yyyy-MM-dd"), labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        DrawField(g, "Medication:", rx["MedicationName"]?.ToString() ?? "N/A", labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        DrawField(g, "Dosage:", rx["Dosage"]?.ToString() ?? "N/A", labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        DrawField(g, "Quantity:", rx["Quantity"]?.ToString() ?? "N/A", labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        DrawField(g, "Instructions:", rx["Instructions"]?.ToString() ?? "N/A", labelFont, valueFont, ref yPos, leftMargin, pageWidth);
                        yPos += 10;
                    }
                }

                // End of report
                yPos += 30;
                g.DrawString("END OF REPORT", sectionFont, textBrush, e.MarginBounds.Width / 2, yPos, centerFormat);

                e.HasMorePages = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error printing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DrawField(Graphics g, string label, string value, Font labelFont, Font valueFont, ref int yPos, float leftMargin, float pageWidth)
        {
            g.DrawString(label, labelFont, Brushes.Black, leftMargin, yPos);
            
            // Calculate available width for the value text (pageWidth - leftMargin - value start position - right margin)
            float valueStartX = leftMargin + 150;
            float maxWidth = pageWidth - valueStartX - 50; // 50px right margin
            
            // Check if text fits in one line
            SizeF textSize = g.MeasureString(value, valueFont);
            
            if (textSize.Width <= maxWidth)
            {
                // Text fits on one line
                g.DrawString(value, valueFont, Brushes.Black, valueStartX, yPos);
                yPos += 25;
            }
            else
            {
                // Text needs to be wrapped to multiple lines
                StringFormat format = new StringFormat();
                format.Alignment = StringAlignment.Near;
                format.Trimming = StringTrimming.Word;
                
                RectangleF textRect = new RectangleF(valueStartX, yPos, maxWidth, valueFont.Height * 5); // Allow up to 5 lines
                SizeF measuredSize = g.MeasureString(value, valueFont, textRect.Size, format);
                
                RectangleF adjustedRect = new RectangleF(valueStartX, yPos, maxWidth, measuredSize.Height);
                g.DrawString(value, valueFont, Brushes.Black, adjustedRect, format);
                
                // Increase yPos based on actual height used
                yPos += (int)Math.Ceiling(measuredSize.Height) + 5;
            }
        }
    }
}
