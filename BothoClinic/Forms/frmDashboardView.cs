using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BothoClinic.Models;

namespace BothoClinic.Forms
{
    public partial class frmDashboardView : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        private readonly User _currentUser;

        public frmDashboardView(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Dock = DockStyle.Fill;
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            LoadStatisticsCards();
            LoadTodaysSchedule();
        }

        private void LoadStatisticsCards()
        {
            try
            {
                // Load real stats from database
                string query = @"
                    SELECT 
                        COUNT(*) as TotalPatients,
                        SUM(CASE WHEN Status = 'Completed' THEN 1 ELSE 0 END) as Completed,
                        SUM(CASE WHEN Status = 'In Progress' THEN 1 ELSE 0 END) as InProgress,
                        SUM(CASE WHEN Status IN ('Scheduled', 'Booked') THEN 1 ELSE 0 END) as Pending
                    FROM Appointments a
                    WHERE a.ProviderId = @ProviderId 
                    AND CAST(a.AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)";

                var result = dbHelper.ExecuteQuery(query, new { ProviderId = _currentUser.UserID });
                
                if (result.Rows.Count > 0)
                {
                    var row = result.Rows[0];
                    lblStatPatientsValue.Text = row["TotalPatients"].ToString();
                    lblStatCompletedValue.Text = row["Completed"].ToString();
                    lblStatInProgressValue.Text = row["InProgress"].ToString();
                    lblStatPendingValue.Text = row["Pending"].ToString();
                }
                else
                {
                    // Default values if no data
                    lblStatPatientsValue.Text = "0";
                    lblStatCompletedValue.Text = "0";
                    lblStatInProgressValue.Text = "0";
                    lblStatPendingValue.Text = "0";
                }
            }
            catch (Exception)
            {
                // Fallback values on error
                lblStatPatientsValue.Text = "0";
                lblStatCompletedValue.Text = "0";
                lblStatInProgressValue.Text = "0";
                lblStatPendingValue.Text = "0";
            }
        }

        private void LoadTodaysSchedule()
        {
            try
            {
                // Clear existing content and ensure proper layout order
                pnlScheduleCard.Controls.Clear();
                
                // Add header first (docked to top)
                pnlScheduleCard.Controls.Add(pnlScheduleHeader);
                
                // Create FlowLayoutPanel exactly like the Appointments tab with proper anchoring
                var flowAppointments = new FlowLayoutPanel
                {
                    Name = "flowTodaysAppointments",
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                    FlowDirection = FlowDirection.TopDown,
                    WrapContents = false,
                    AutoScroll = true,
                    BackColor = Color.White,
                    Padding = new Padding(10),
                    Margin = new Padding(0),
                    Location = new Point(20, 80), // Start below header
                    Size = new Size(810, 340) // Fixed height with proper bottom clearance
                };

                // Add the FlowLayoutPanel AFTER the header so it fills remaining space
                pnlScheduleCard.Controls.Add(flowAppointments);
                
                // Force the header to stay at top and flow to fill remaining space
                pnlScheduleHeader.BringToFront();
                
                // Use the exact same method as Appointments tab but filter for today only
                LoadAppointmentsForToday(flowAppointments);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error setting up today's schedule: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAppointmentsForToday(FlowLayoutPanel targetPanel)
        {
            if (targetPanel == null) return;
            targetPanel.SuspendLayout();
            try
            {
                // Use the exact same query structure as Appointments tab
                string sql = @"
                    SELECT 
                        a.AppointmentId AS ApptID,
                        a.PatientId,
                        a.ProviderId,
                        (CAST(a.AppointmentDate AS datetime) + CAST(a.TimeSlot AS datetime)) AS ApptDateTime,
                        a.Status,
                        a.Reason,
                        u.FullName AS PatientFullName,
                        p.StudentId
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientId = p.PatientId
                    INNER JOIN Users u ON p.UserId = u.UserId
                    WHERE a.ProviderId = @ProviderId AND a.AppointmentDate = CAST(@Date AS DATE)
                    ORDER BY a.AppointmentDate, a.TimeSlot";

                var dt = dbHelper.ExecuteQuery(sql, new { ProviderId = _currentUser.UserID, Date = DateTime.Today });

                targetPanel.Controls.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    DateTime apptTime = Convert.ToDateTime(row["ApptDateTime"]);
                    string displayStatus = MapStatusForPrototype(row["Status"]?.ToString());
                    string patientName = row["PatientFullName"]?.ToString() ?? "Unknown";
                    string studentId = row["StudentId"]?.ToString() ?? "";

                    string initials = "";
                    if (!string.IsNullOrWhiteSpace(patientName))
                    {
                        var parts = patientName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        if (parts.Length > 0) initials += parts[0][0];
                        if (parts.Length > 1) initials += parts[^1][0];
                    }
                    if (string.IsNullOrWhiteSpace(initials)) initials = "?";

                    var item = new AppointmentRowControl
                    {
                        PatientName = patientName,
                        PatientId = studentId,
                        AvatarText = initials.ToUpperInvariant(),
                        Time = apptTime.ToString("h:mm tt"),
                        Reason = row["Reason"]?.ToString() ?? "",
                        Status = displayStatus,
                        Tag = row["ApptID"] // Store appointment ID for click handling
                    };

                    // Make clickable like the original
                    item.Cursor = Cursors.Hand;
                    item.Click += AppointmentCard_Click;

                    targetPanel.Controls.Add(item);
                }

                // Update the title with count
                lblTodaysScheduleTitle.Text = $"Today's Schedule ({dt.Rows.Count} appointments)";

                // If no appointments, show a message
                if (dt.Rows.Count == 0)
                {
                    var noAppointmentsLabel = new Label
                    {
                        Text = "No appointments scheduled for today",
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular),
                        ForeColor = Color.Gray,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Fill,
                        AutoSize = false,
                        Height = 100
                    };
                    targetPanel.Controls.Add(noAppointmentsLabel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load today's appointments: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblTodaysScheduleTitle.Text = "Today's Schedule (0 appointments)";
            }
            finally
            {
                targetPanel.ResumeLayout();
            }
        }

        private string MapStatusForPrototype(string? status)
        {
            if (string.IsNullOrWhiteSpace(status)) return "Scheduled";
            // Map DB statuses to prototype labels - same as Appointments tab
            switch (status.Trim().ToLowerInvariant())
            {
                case "booked":
                case "confirmed":
                case "scheduled":
                    return "Scheduled";
                case "in progress":
                case "inprogress":
                case "active":
                    return "In Progress";
                case "completed":
                case "finished":
                case "done":
                    return "Completed";
                case "cancelled":
                case "canceled":
                    return "Cancelled";
                default:
                    return "Scheduled"; // Default to Scheduled for unknown statuses
            }
        }

        private void AppointmentCard_Click(object sender, EventArgs e)
        {
            try
            {
                var appointmentCard = sender as AppointmentRowControl;
                var appointmentId = appointmentCard.Tag;
                
                // Get detailed appointment information from database
                ShowAppointmentDetails(Convert.ToInt32(appointmentId));
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error viewing appointment details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowAppointmentDetails(int appointmentId)
        {
            try
            {
                // Get full appointment details from database
                string query = @"
                    SELECT 
                        a.AppointmentId,
                        a.AppointmentDate,
                        a.TimeSlot,
                        a.Status,
                        a.Reason,
                        a.Notes,
                        u.FullName AS PatientName,
                        p.StudentId,
                        p.DateOfBirth,
                        p.Gender,
                        p.ContactNumber,
                        p.EmergencyContact,
                        provider.FullName AS ProviderName
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientId = p.PatientId
                    INNER JOIN Users u ON p.UserId = u.UserId
                    INNER JOIN Users provider ON a.ProviderId = provider.UserId
                    WHERE a.AppointmentId = @AppointmentId";

                var dt = dbHelper.ExecuteQuery(query, new { AppointmentId = appointmentId });

                if (dt.Rows.Count > 0)
                {
                    var row = dt.Rows[0];
                    
                    // Format appointment date and time
                    DateTime appointmentDate = Convert.ToDateTime(row["AppointmentDate"]);
                    TimeSpan timeSlot = (TimeSpan)row["TimeSlot"];
                    DateTime appointmentDateTime = appointmentDate.Add(timeSlot);
                    
                    // Build detailed information string
                    string details = $"APPOINTMENT DETAILS\n" +
                                   $"===================================\n\n" +
                                   $"📅 Date & Time: {appointmentDateTime:dddd, MMMM dd, yyyy 'at' h:mm tt}\n" +
                                   $"🆔 Appointment ID: {row["AppointmentId"]}\n" +
                                   $"📋 Status: {row["Status"]}\n" +
                                   $"💡 Reason: {row["Reason"]}\n\n" +
                                   $"PATIENT INFORMATION\n" +
                                   $"===================================\n" +
                                   $"👤 Name: {row["PatientName"]}\n" +
                                   $"🆔 Student ID: {row["StudentId"]}\n" +
                                   $"🎂 Date of Birth: {Convert.ToDateTime(row["DateOfBirth"]):MMMM dd, yyyy}\n" +
                                   $"⚧ Gender: {row["Gender"]}\n" +
                                   $"📞 Contact: {row["ContactNumber"]}\n" +
                                   $"🚨 Emergency Contact: {row["EmergencyContact"]}\n\n" +
                                   $"PROVIDER INFORMATION\n" +                                   
                                   $"===================================\n" +
                                   $"👨‍⚕️ Healthcare Provider: {row["ProviderName"]}\n";

                    // Add notes if available
                    if (!string.IsNullOrWhiteSpace(row["Notes"]?.ToString()))
                    {
                        details += $"\n📝 Notes: {row["Notes"]}\n";
                    }

                    // Add action prompt
                    details += $"\n===================================\n" +
                              $"Click OK to continue, or consider opening\n" +
                              $"the consultation form for this appointment.";

                    // Show detailed information
                    MessageBox.Show(details, $"Appointment Details - {row["PatientName"]}", 
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Appointment details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error retrieving appointment details: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public void RefreshData()
        {
            LoadDashboardData();
        }
    }
}