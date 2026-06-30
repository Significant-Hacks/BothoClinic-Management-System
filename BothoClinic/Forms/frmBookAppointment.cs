using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Linq;

namespace BothoClinic
{
    public partial class frmBookAppointment : Form
    {
        private readonly int _patientId;
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        public event EventHandler AppointmentBooked;

        public frmBookAppointment(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
            
            // Set minimum date to today
            dtpDate.MinDate = DateTime.Today;
            dtpDate.Value = DateTime.Today;
            
            LoadProviders();
            
            // Set up event handlers for dynamic time slot loading
            dtpDate.ValueChanged += (s, e) => UpdateAvailableTimes();
            cmbProvider.SelectedIndexChanged += (s, e) => UpdateAvailableTimes();
            
            // Load time slots after providers are loaded
            this.Load += (s, e) => UpdateAvailableTimes();
        }

        private void LoadProviders()
        {
            try
            {
                string sql = "SELECT UserID, FullName FROM Users WHERE RoleID = (SELECT RoleID FROM Roles WHERE RoleName = 'Provider')";
                DataTable dt = dbHelper.ExecuteQuery(sql);
                
                // Debug: Check if providers are found
                MessageBox.Show($"Found {dt.Rows.Count} providers", "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                if (dt.Rows.Count > 0)
                {
                    cmbProvider.DataSource = dt;
                    cmbProvider.DisplayMember = "FullName";
                    cmbProvider.ValueMember = "UserID";
                    
                    // Make sure the provider dropdown is visible
                    cmbProvider.Visible = true;
                    lblProvider.Visible = true;
                }
                else
                {
                    // If no providers found, add a dummy provider for testing
                    MessageBox.Show("No providers found in database. Adding test provider.", "Debug Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dt.Rows.Add(1, "Test Provider");
                    cmbProvider.DataSource = dt;
                    cmbProvider.DisplayMember = "FullName";
                    cmbProvider.ValueMember = "UserID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading providers: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                // Add test data if database fails
                DataTable testDt = new DataTable();
                testDt.Columns.Add("UserID", typeof(int));
                testDt.Columns.Add("FullName", typeof(string));
                testDt.Rows.Add(1, "Test Provider");
                cmbProvider.DataSource = testDt;
                cmbProvider.DisplayMember = "FullName";
                cmbProvider.ValueMember = "UserID";
            }
        }

        /// <summary>
        /// Gets a list of available appointment time slots for a given provider and date.
        /// </summary>
        /// <param name="providerId">The ID of the provider (doctor/nurse).</param>
        /// <param name="appointmentDate">The date for which to check availability.</param>
        /// <returns>A list of strings representing available time slots.</returns>
        private List<string> GetAvailableTimeSlots(int providerId, DateTime appointmentDate)
        {
            // Step 1: Define all possible time slots for a workday (8:00 AM to 4:30 PM)
            var allPossibleSlots = new List<string>();
            var startTime = new TimeSpan(8, 0, 0);  // 8:00 AM
            var endTime = new TimeSpan(16, 30, 0); // 4:30 PM is the last slot start time
            var interval = TimeSpan.FromMinutes(30);

            for (var time = startTime; time <= endTime; time += interval)
            {
                // Format to a standard 12-hour format like "8:30 AM"
                allPossibleSlots.Add(DateTime.Today.Add(time).ToString("h:mm tt"));
            }

            // Step 2: Get all the slots that are already booked for that provider on that day
            var bookedSlots = new HashSet<string>();
            try
            {
                string query = @"
                    SELECT 
                        FORMAT(a.TimeSlot, 'h\:mm tt') AS BookedTime
                    FROM Appointments a
                    WHERE a.ProviderId = @ProviderId 
                    AND CAST(a.AppointmentDate AS DATE) = CAST(@AppointmentDate AS DATE)
                    AND Status NOT IN ('Cancelled')";

                var parameters = new { ProviderId = providerId, AppointmentDate = appointmentDate.Date };
                var dt = dbHelper.ExecuteQuery(query, parameters);

                foreach (DataRow row in dt.Rows)
                {
                    if (row["BookedTime"] != DBNull.Value)
                    {
                        // The query formats the time to match our C# format (e.g., "8:30 AM")
                        bookedSlots.Add(row["BookedTime"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching booked slots: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<string>(); // Return an empty list on error
            }

            // Step 3: Subtract the booked slots from all possible slots to find what's available
            var availableSlots = allPossibleSlots.Where(slot => !bookedSlots.Contains(slot)).ToList();

            return availableSlots;
        }

        /// <summary>
        /// Populates the time slot ComboBox based on the selected provider and date.
        /// </summary>
        private void UpdateAvailableTimes()
        {
            // Clear existing items
            cmbTime.DataSource = null;
            cmbTime.Items.Clear();

            // Debug: Always show method is being called
            MessageBox.Show("UpdateAvailableTimes called", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Check if provider is selected and date is valid
            if (cmbProvider.SelectedValue == null)
            {
                // For testing: Add sample time slots even without provider
                MessageBox.Show("No provider selected - adding sample times for testing", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Add some sample time slots for testing
                var sampleTimes = new List<string> { "8:00 AM", "8:30 AM", "9:00 AM", "9:30 AM", "10:00 AM", "10:30 AM", "11:00 AM", "11:30 AM", "12:00 PM", "12:30 PM", "1:00 PM", "1:30 PM", "2:00 PM", "2:30 PM", "3:00 PM", "3:30 PM", "4:00 PM", "4:30 PM" };
                
                foreach (var time in sampleTimes)
                {
                    cmbTime.Items.Add(time);
                }
                cmbTime.Text = "Select a time slot (TEST MODE - No Provider)";
                return;
            }

            if (dtpDate.Value.Date < DateTime.Today)
            {
                cmbTime.Items.Add("Select future date");
                cmbTime.SelectedIndex = 0;
                return;
            }

            try
            {
                // Get the selected provider's ID
                int providerId = Convert.ToInt32(cmbProvider.SelectedValue);
                DateTime selectedDate = dtpDate.Value.Date;

                MessageBox.Show($"Loading slots for Provider ID: {providerId}, Date: {selectedDate:yyyy-MM-dd}", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Fetch the available slots
                var availableSlots = GetAvailableTimeSlots(providerId, selectedDate);

                MessageBox.Show($"Found {availableSlots.Count} available slots", "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Populate the ComboBox
                if (availableSlots.Any())
                {
                    foreach (var slot in availableSlots)
                    {
                        cmbTime.Items.Add(slot);
                    }
                    cmbTime.Text = "Select a time slot";
                }
                else
                {
                    cmbTime.Items.Add("No available slots for this day");
                    cmbTime.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating available times: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cmbTime.Items.Add("Error loading slots");
                cmbTime.SelectedIndex = 0;
            }
        }

        private void LoadAvailableTimeSlots()
        {
            UpdateAvailableTimes();
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            if (cmbTime.SelectedItem == null)
            {
                MessageBox.Show("Please select a time for the appointment.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbProvider.SelectedValue == null)
            {
                MessageBox.Show("Please select a provider.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtReason.Text))
            {
                MessageBox.Show("Please enter a reason for the appointment.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TimeSpan.TryParse(cmbTime.SelectedItem.ToString(), out TimeSpan timeSlot))
            {
                MessageBox.Show("Invalid time format.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql = @"
                    INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status)
                    VALUES (@PatientId, @ProviderId, @AppointmentDate, @TimeSlot, @Reason, @Status)";

                dbHelper.ExecuteNonQuery(sql, new
                {
                    PatientId = _patientId,
                    ProviderId = cmbProvider.SelectedValue,
                    AppointmentDate = dtpDate.Value.Date,
                    TimeSlot = timeSlot,
                    Reason = txtReason.Text,
                    Status = "Scheduled"
                });

                MessageBox.Show("Appointment booked successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AppointmentBooked?.Invoke(this, EventArgs.Empty);
                this.DialogResult = DialogResult.OK; // Signal success to parent form
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error booking appointment: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}