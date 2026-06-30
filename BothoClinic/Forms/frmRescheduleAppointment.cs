using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BothoClinic.Models;

namespace BothoClinic.Forms
{
    public partial class frmRescheduleAppointment : Form
    {
        private DatabaseHelper dbHelper;
        private User _currentUser;
        private int _appointmentId;

        public frmRescheduleAppointment(int appointmentId, User currentUser)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            _currentUser = currentUser;
            dbHelper = new DatabaseHelper();
            LoadProviders();
            SetDefaults();
        }

        private void LoadProviders()
        {
            try
            {
                // Populate current provider (readonly) and potential transfer targets (other providers)
                string currentProviderQuery = @"
                    SELECT UserId, FullName 
                    FROM Users 
                    WHERE RoleId = (SELECT RoleId FROM Roles WHERE RoleName = 'Provider') 
                      AND IsActive = 1
                      AND UserId = @CurrentProviderId";

                string otherProvidersQuery = @"
                    SELECT UserId, FullName
                    FROM Users
                    WHERE RoleId = (SELECT RoleId FROM Roles WHERE RoleName = 'Provider')
                      AND IsActive = 1
                      AND UserId <> @CurrentProviderId
                    ORDER BY FullName";

                DataTable currentProvider = dbHelper.ExecuteQuery(currentProviderQuery, new { CurrentProviderId = _currentUser.UserID });
                DataTable otherProviders = dbHelper.ExecuteQuery(otherProvidersQuery, new { CurrentProviderId = _currentUser.UserID });
                
                cmbProvider.DisplayMember = "FullName";
                cmbProvider.ValueMember = "UserId";
                cmbProvider.DataSource = currentProvider;
                
                cmbTransferTo.DisplayMember = "FullName";
                cmbTransferTo.ValueMember = "UserId";
                cmbTransferTo.DataSource = otherProviders;
                
                // Select current user as default
                cmbProvider.SelectedValue = _currentUser.UserID;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading providers: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDefaults()
        {
            dtpDate.Value = DateTime.Today.AddDays(1);
            dtpTime.Value = DateTime.Today.AddHours(9); // 9 AM default
        }

        private void btnReschedule_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpDate.Value.Date <= DateTime.Today)
                {
                    MessageBox.Show("Please select a future date.", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedProvider = cmbProvider.SelectedValue;
                if (selectedProvider == null)
                {
                    MessageBox.Show("Please select a provider.", "No Provider Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int? transferTo = null;
                if (cmbTransferTo.SelectedValue != null && int.TryParse(cmbTransferTo.SelectedValue.ToString(), out int tpid))
                {
                    transferTo = tpid;
                }

                // Determine which provider to assign based on transfer selection
                int? finalProviderId = transferTo ?? Convert.ToInt32(selectedProvider);
                
                // Use direct SQL update for reliability
                string updateSql = @"
                    UPDATE Appointments 
                    SET AppointmentDate = @NewDate, 
                        TimeSlot = @NewTime, 
                        ProviderId = @NewProviderId, 
                        Status = 'Scheduled'
                    WHERE AppointmentId = @AppointmentId";
                
                int result = dbHelper.ExecuteNonQuery(updateSql, new
                {
                    AppointmentId = _appointmentId,
                    NewDate = dtpDate.Value.Date,
                    NewTime = dtpTime.Value.TimeOfDay,
                    NewProviderId = finalProviderId
                });

                if (result > 0)
                {
                    MessageBox.Show("Appointment rescheduled successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Debug information
                    string debugInfo = $"Parameters:\nAppointmentId: {_appointmentId}\nNewDate: {dtpDate.Value.Date:yyyy-MM-dd}\nNewTime: {dtpTime.Value.TimeOfDay}\nFinalProviderId: {finalProviderId}\nPerformedBy: {_currentUser.UserID}\nNotes: '{txtNotes.Text.Trim()}'";
                    MessageBox.Show($"Failed to reschedule appointment. No rows were affected.\n\n{debugInfo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error rescheduling appointment: {ex.Message}\n\nDetails:\nAppointmentId: {_appointmentId}\nNewDate: {dtpDate.Value.Date}\nNewTime: {dtpTime.Value.TimeOfDay}\nPerformedBy: {_currentUser.UserID}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}