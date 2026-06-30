using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Text.Json;
using System.Text;
using BothoClinic.Models;

namespace BothoClinic.Forms
{
    public partial class frmAuditLogDetails : Form
    {
        private readonly int _auditLogId;
        private readonly DatabaseHelper _dbHelper;
        private Panel _mainPanel;
        private Button _btnClose;

        public frmAuditLogDetails(int logId, DatabaseHelper dbHelper)
        {
            _auditLogId = logId;
            _dbHelper = dbHelper;
            InitializeComponent();
            LoadAuditLogDetails();
        }

        private void LoadAuditLogDetails()
        {
            try
            {
                string sql = @"
                    SELECT 
                        al.LogId,
                        al.UserId,
                        al.Action,
                        al.TableName,
                        al.RecordId,
                        al.OldValues,
                        al.NewValues,
                        al.IPAddress,
                        al.UserAgent,
                        al.Timestamp AS ActionDate,
                        u.FullName AS UserName,
                        u.Username,
                        al.Details
                    FROM AuditLogs al
                    LEFT JOIN Users u ON al.UserId = u.UserId
                    WHERE al.LogId = @LogId";

                DataTable dt = _dbHelper.ExecuteQuery(sql, new { LogId = _auditLogId });

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Audit log not found.", "Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                    return;
                }

                DataRow row = dt.Rows[0];
                SetupForm();
                PopulateFields(row);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading audit log details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void SetupForm()
        {
            this.Text = "Audit Log Details";
            this.Size = new Size(1200, 900);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new Size(1100, 800);

            // Bottom panel for buttons
            Panel bottomPanel = new Panel
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20, 10, 20, 10)
            };

            // Close button
            _btnClose = new Button
            {
                Text = "Close",
                Size = new Size(100, 35),
                DialogResult = DialogResult.OK,
                Anchor = AnchorStyles.Right,
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            _btnClose.FlatAppearance.BorderSize = 0;
            _btnClose.Click += (s, e) => this.Close();
            
            // Position button properly
            bottomPanel.Resize += (s, e) => {
                _btnClose.Location = new Point(bottomPanel.Width - 130, 12);
            };
            bottomPanel.Controls.Add(_btnClose);
            
            this.Controls.Add(bottomPanel);

            // Main scrollable panel
            _mainPanel = new Panel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(248, 249, 250)
            };
            this.Controls.Add(_mainPanel);
        }

        private void PopulateFields(DataRow row)
        {
            int yPos = 10;

            // Header
            Label lblHeader = new Label
            {
                Text = "Audit Log Details",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(70, 130, 180),
                AutoSize = true,
                Location = new Point(20, yPos)
            };
            _mainPanel.Controls.Add(lblHeader);
            yPos += 60;

            // User Information Section
            Panel userPanel = CreateImprovedSection("USER INFORMATION", yPos, Color.FromArgb(70, 130, 180));
            int sectionYPos = 50;
            AddImprovedField(userPanel, "User:", row["UserName"]?.ToString() ?? "Unknown User", sectionYPos);
            sectionYPos += 35;
            AddImprovedField(userPanel, "Username:", row["Username"]?.ToString() ?? "N/A", sectionYPos);
            yPos += 150;

            // Table Information Section
            Panel tablePanel = CreateImprovedSection("TABLE INFORMATION", yPos, Color.FromArgb(70, 130, 180));
            sectionYPos = 50;
            AddImprovedField(tablePanel, "Table:", row["TableName"]?.ToString() ?? "N/A", sectionYPos);
            sectionYPos += 35;
            AddImprovedField(tablePanel, "Record ID:", row["RecordId"]?.ToString() ?? "N/A", sectionYPos);
            yPos += 150;

            // Change Details Section - Enhanced with better display
            Panel changePanel = CreateImprovedSection("CHANGE DETAILS", yPos, Color.FromArgb(220, 53, 69));
            sectionYPos = 50;
            
            string oldValues = row["OldValues"]?.ToString() ?? "";
            string newValues = row["NewValues"]?.ToString() ?? "";
            
            if (!string.IsNullOrEmpty(oldValues))
            {
                AddImprovedTextArea(changePanel, "Old Values:", oldValues, sectionYPos, 120);
                sectionYPos += 140;
            }
            
            if (!string.IsNullOrEmpty(newValues))
            {
                AddImprovedTextArea(changePanel, "New Values:", newValues, sectionYPos, 120);
                sectionYPos += 140;
            }
            
            yPos += 370;

            // System Information Section - Make it taller to accommodate all fields
            Panel systemPanel = CreateImprovedSection("SYSTEM INFORMATION", yPos, Color.FromArgb(70, 130, 180));
            systemPanel.Height = 150; // Increase height for all three fields
            sectionYPos = 50;
            AddImprovedField(systemPanel, "Date Time:", row["ActionDate"] != DBNull.Value && row["ActionDate"] != null ? 
                Convert.ToDateTime(row["ActionDate"]).ToString("yyyy-MM-dd HH:mm:ss") : "N/A", sectionYPos);
            sectionYPos += 35;
            AddImprovedField(systemPanel, "IP Address:", row["IPAddress"]?.ToString() ?? "N/A", sectionYPos);
            sectionYPos += 35;
            AddImprovedField(systemPanel, "User Agent:", row["UserAgent"]?.ToString() ?? "N/A", sectionYPos);
            
            yPos += 170; // Add proper space after system panel

            // Set proper scroll size to ensure all content is visible with extra padding
            _mainPanel.AutoScrollMinSize = new Size(1100, yPos + 100);
        }

        private Panel CreateImprovedSection(string title, int yPos, Color headerColor)
        {
            // Adjust height based on section type
            int sectionHeight = 130;
            if (title == "CHANGE DETAILS")
                sectionHeight = 350;
            else if (title == "SYSTEM INFORMATION")
                sectionHeight = 150; // Make system info section taller
            
            Panel sectionPanel = new Panel
            {
                Location = new Point(20, yPos),
                Size = new Size(1050, sectionHeight),
                BackColor = Color.White
            };

            // Add subtle border
            sectionPanel.Paint += (s, e) =>
            {
                ControlPaint.DrawBorder(e.Graphics, sectionPanel.ClientRectangle,
                    Color.FromArgb(200, 200, 200), ButtonBorderStyle.Solid);
            };

            Panel headerPanel = new Panel
            {
                Location = new Point(0, 0),
                Size = new Size(1050, 40),
                BackColor = headerColor
            };

            Label headerLabel = new Label
            {
                Text = title,
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(15, 10),
                AutoSize = true
            };

            headerPanel.Controls.Add(headerLabel);
            sectionPanel.Controls.Add(headerPanel);
            _mainPanel.Controls.Add(sectionPanel);
            return sectionPanel;
        }

        private void AddImprovedField(Panel parentPanel, string label, string value, int yPos)
        {
            Label lblField = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(20, yPos),
                Size = new Size(120, 25),
                ForeColor = Color.FromArgb(70, 130, 180)
            };

            Label lblValue = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(150, yPos),
                Size = new Size(850, 25),
                ForeColor = Color.FromArgb(33, 37, 41),
                AutoEllipsis = true
            };

            parentPanel.Controls.Add(lblField);
            parentPanel.Controls.Add(lblValue);
        }

        private void AddImprovedTextArea(Panel parentPanel, string label, string value, int yPos, int height)
        {
            Label lblField = new Label
            {
                Text = label,
                Font = new Font("Segoe UI", 9F, FontStyle.Bold),
                Location = new Point(20, yPos),
                Size = new Size(120, 25),
                ForeColor = Color.FromArgb(70, 130, 180)
            };

            // Use TextBox for scrollable JSON display
            TextBox txtValue = new TextBox
            {
                Text = FormatJsonForDisplay(value),
                Font = new Font("Consolas", 8.5F),
                Location = new Point(150, yPos),
                Size = new Size(850, height),
                Multiline = true,
                ScrollBars = ScrollBars.Both,
                ReadOnly = true,
                BackColor = Color.FromArgb(248, 249, 250),
                BorderStyle = BorderStyle.FixedSingle,
                WordWrap = false
            };

            parentPanel.Controls.Add(lblField);
            parentPanel.Controls.Add(txtValue);
        }

        private string FormatJsonForDisplay(string jsonString)
        {
            if (string.IsNullOrEmpty(jsonString))
                return "";

            try
            {
                // Try to parse and format as JSON
                using (var document = JsonDocument.Parse(jsonString))
                {
                    return JsonSerializer.Serialize(document, new JsonSerializerOptions 
                    { 
                        WriteIndented = true 
                    });
                }
            }
            catch
            {
                // If not valid JSON, return as-is with some basic formatting
                return jsonString.Replace(",", ",\n").Replace("{", "{\n").Replace("}", "\n}");
            }
        }

    }
}

