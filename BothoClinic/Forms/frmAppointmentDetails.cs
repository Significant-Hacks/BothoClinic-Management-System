using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using BothoClinic.Models;

namespace BothoClinic.Forms
{
    public partial class frmAppointmentDetails : Form
    {
        private DatabaseHelper dbHelper;
        private User _currentUser;
        private int _appointmentId;
        private DataRow _appointmentData;

        public frmAppointmentDetails(int appointmentId, User currentUser)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            _currentUser = currentUser;
            dbHelper = new DatabaseHelper();
            LoadAppointmentDetails();
        }

        private void LoadAppointmentDetails()
        {
            try
            {
                string sql = @"
                    SELECT 
                        a.AppointmentId,
                        a.AppointmentDate,
                        a.TimeSlot,
                        a.Reason,
                        a.Status,
                        a.PatientId,
                        a.GuestPatientId,
                        a.ProviderId,
                        a.ClaimedByProviderId,
                        a.ClaimedAt,
                        a.CreatedDateTime,
                        COALESCE(u.FullName, gp.FullName) AS PatientName,
                        COALESCE(u.ContactEmail, '') AS PatientEmail,
                        COALESCE(u.ContactPhone, gp.PhoneNumber) AS PatientPhone,
                        COALESCE(p.StudentId, CONCAT('GP', CAST(gp.GuestPatientId AS VARCHAR))) AS StudentId,
                        COALESCE(p.DOB, '') AS DOB,
                        COALESCE(p.Gender, '') AS Gender,
                        COALESCE(p.BloodType, '') AS BloodType,
                        pr.FullName AS ProviderName,
                        CASE WHEN a.GuestPatientId IS NOT NULL THEN 'Guest' ELSE 'Student' END AS PatientType
                    FROM Appointments a
                    LEFT JOIN Patients pt ON a.PatientId = pt.PatientId
                    LEFT JOIN Users u ON pt.UserId = u.UserId
                    LEFT JOIN GuestPatients gp ON a.GuestPatientId = gp.GuestPatientId
                    LEFT JOIN Users pr ON a.ProviderId = pr.UserId
                    LEFT JOIN Patients p ON a.PatientId = p.PatientId
                    WHERE a.AppointmentId = @AppointmentId";

                DataTable result = dbHelper.ExecuteQuery(sql, new { AppointmentId = _appointmentId });
                
                if (result.Rows.Count > 0)
                {
                    _appointmentData = result.Rows[0];
                    PopulateFields();
                    SetupButtons();
                }
                else
                {
                    MessageBox.Show("Appointment not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void PopulateFields()
        {
            // Appointment Info
            lblAppointmentId.Text = _appointmentData["AppointmentId"].ToString();
            lblAppointmentDate.Text = Convert.ToDateTime(_appointmentData["AppointmentDate"]).ToString("MMM dd, yyyy");
            lblTimeSlot.Text = _appointmentData["TimeSlot"].ToString();
            txtReason.Text = _appointmentData["Reason"].ToString();
            lblStatus.Text = _appointmentData["Status"].ToString();
            
            // Patient Info
            lblPatientName.Text = _appointmentData["PatientName"].ToString();
            lblStudentId.Text = _appointmentData["StudentId"].ToString();
            lblPatientEmail.Text = _appointmentData["PatientEmail"]?.ToString() ?? "N/A";
            lblPatientPhone.Text = _appointmentData["PatientPhone"]?.ToString() ?? "N/A";
            lblDOB.Text = _appointmentData["DOB"] != DBNull.Value ? Convert.ToDateTime(_appointmentData["DOB"]).ToString("MMM dd, yyyy") : "N/A";
            lblGender.Text = _appointmentData["Gender"]?.ToString() ?? "N/A";
            lblBloodType.Text = _appointmentData["BloodType"]?.ToString() ?? "N/A";
            
            // Provider Info
            string providerName = _appointmentData["ProviderName"]?.ToString();
            lblCurrentProvider.Text = string.IsNullOrEmpty(providerName) ? "Unassigned" : providerName;
            
            // Timestamps
            lblCreatedDate.Text = Convert.ToDateTime(_appointmentData["CreatedDateTime"]).ToString("MMM dd, yyyy HH:mm");
            lblClaimedDate.Text = _appointmentData["ClaimedAt"] != DBNull.Value ? 
                Convert.ToDateTime(_appointmentData["ClaimedAt"]).ToString("MMM dd, yyyy HH:mm") : "Not claimed";
        }

        private void SetupButtons()
        {
            string status = _appointmentData["Status"].ToString();
            object providerId = _appointmentData["ProviderId"];
            bool isMyAppointment = providerId != DBNull.Value && Convert.ToInt32(providerId) == _currentUser.UserID;
            bool isUnassigned = providerId == DBNull.Value;

            // Reset all buttons
            btnAssignToMe.Visible = false;
            btnStartConsultation.Visible = false;
            btnCompleteAppointment.Visible = false;
            btnReschedule.Visible = false;
            btnCancelAppointment.Visible = false;
            btnViewConsultation.Visible = false;

            switch (status)
            {
                case "Scheduled":
                    if (isUnassigned)
                    {
                        btnAssignToMe.Visible = true;
                    }
                    else if (isMyAppointment)
                    {
                        btnStartConsultation.Visible = true;
                        btnReschedule.Visible = true;
                        btnCancelAppointment.Visible = true;
                    }
                    else
                    {
                        btnReschedule.Visible = true;
                        btnCancelAppointment.Visible = true;
                    }
                    break;

                case "In Progress":
                    if (isMyAppointment)
                    {
                        btnCompleteAppointment.Visible = true;
                        btnReschedule.Visible = true;
                        btnCancelAppointment.Visible = true;
                    }
                    else
                    {
                        btnReschedule.Visible = true;
                        btnCancelAppointment.Visible = true;
                    }
                    break;

                case "Completed":
                    btnViewConsultation.Visible = true;
                    break;

                case "Cancelled":
                    btnReschedule.Visible = true; // Allow rescheduling cancelled appointments
                    break;
            }

            // Always allow reschedule and cancel for providers (admin capabilities)
            if (!btnReschedule.Visible) btnReschedule.Visible = true;
            if (!btnCancelAppointment.Visible && status != "Cancelled") btnCancelAppointment.Visible = true;
        }

        private void btnAssignToMe_Click(object sender, EventArgs e)
        {
            try
            {
                var result = dbHelper.ExecuteProcNonQuery("usp_Appointment_Claim", new
                {
                    AppointmentId = _appointmentId,
                    ProviderId = _currentUser.UserID,
                    PerformedBy = _currentUser.UserID
                });

                if (result > 0)
                {
                    MessageBox.Show("Appointment assigned to you successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadAppointmentDetails(); // Refresh
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error assigning appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStartConsultation_Click(object sender, EventArgs e)
        {
            try
            {
                // Determine appointment time window
                DateTime apptDate = Convert.ToDateTime(_appointmentData["AppointmentDate"]);
                TimeSpan timeSlot = TimeSpan.Parse(_appointmentData["TimeSlot"].ToString());
                DateTime startTime = apptDate.Date + timeSlot;
                int maxMinutes = GetMaxConsultationWindowMinutes();
                DateTime endTime = startTime.AddMinutes(maxMinutes);

                DateTime now = DateTime.Now;
                bool withinWindow = now >= startTime && now <= endTime;

                if (!withinWindow)
                {
                    string msg;
                    if (now < startTime)
                    {
                        msg = $"This appointment is scheduled for {startTime:MMM dd, yyyy h:mm tt}.\n\nStarting now will be treated as an EMERGENCY consultation.\nDo you want to proceed?";
                    }
                    else
                    {
                        msg = $"This appointment window ended at {endTime:MMM dd, yyyy h:mm tt} (max {maxMinutes} minutes).\n\nStarting now will be treated as an EMERGENCY consultation.\nDo you want to proceed?";
                    }

                    var confirm = MessageBox.Show(msg, "Confirm Emergency Start", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (confirm != DialogResult.Yes)
                    {
                        return; // Do not start
                    }
                }

                // Claim/Start consultation (stored proc may also set status to In Progress)
                var result = dbHelper.ExecuteProcNonQuery("usp_Appointment_Claim", new
                {
                    AppointmentId = _appointmentId,
                    ProviderId = _currentUser.UserID,
                    PerformedBy = _currentUser.UserID
                });

                if (result > 0)
                {
                    MessageBox.Show("Consultation started! Opening consultation form...", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    using (var consultationForm = new frmConsultation(_appointmentId, _currentUser.UserID))
                    {
                        consultationForm.ShowDialog();
                    }

                    LoadAppointmentDetails(); // Refresh after consultation
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting consultation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetMaxConsultationWindowMinutes()
        {
            // Default to 30 minutes if no admin setting exists or i forget to set this in the settings of the admin panel
            try
            {
                // Try to read from an optional ClinicSettings table if present
                string sql = @"SELECT TOP 1 TRY_CAST(Value AS INT) AS Minutes
                                 FROM ClinicSettings
                                 WHERE [Key] = 'MaxConsultationMinutes'";
                var dt = dbHelper.ExecuteQuery(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (int.TryParse(dt.Rows[0]["Minutes"]?.ToString(), out int minutes) && minutes > 0)
                    {
                        return minutes;
                    }
                }
            }
            catch
            {
                // ignore; fall back to default below
            }
            return 30;
        }

        private void btnCompleteAppointment_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Mark this appointment as completed?", "Confirm Completion", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var result = dbHelper.ExecuteProcNonQuery("usp_Appointment_UpdateStatus", new
                    {
                        AppointmentId = _appointmentId,
                        NewStatus = "Completed",
                        PerformedBy = _currentUser.UserID,
                        Notes = "Marked as completed by provider"
                    });

                    if (result > 0)
                    {
                        MessageBox.Show("Appointment marked as completed!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAppointmentDetails(); // Refresh
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error completing appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnReschedule_Click(object sender, EventArgs e)
        {
            using (var rescheduleForm = new frmRescheduleAppointment(_appointmentId, _currentUser))
            {
                if (rescheduleForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // If a transfer target was chosen, set status to 'Transfer' to reflect on dashboards
                        var cmb = rescheduleForm.Controls.Find("cmbTransferTo", true).FirstOrDefault() as ComboBox;
                        int? transferToProviderId = null;
                        if (cmb != null && cmb.SelectedValue != null && int.TryParse(cmb.SelectedValue.ToString(), out int tpid))
                        {
                            transferToProviderId = tpid;
                        }
                        if (transferToProviderId.HasValue)
                        {
                            string sql = "UPDATE Appointments SET Status = 'Scheduled' WHERE AppointmentId = @AppointmentId";
                            dbHelper.ExecuteNonQuery(sql, new { AppointmentId = _appointmentId });
                        }
                    }
                    catch { }

                    LoadAppointmentDetails(); // Refresh
                }
            }
        }

        private void btnCancelAppointment_Click(object sender, EventArgs e)
        {
            string currentStatus = _appointmentData["Status"].ToString();
            
            // Prevent canceling completed or in-progress appointments
            if (currentStatus == "Completed" || currentStatus == "In Progress")
            {
                MessageBox.Show($"Cannot cancel appointment that is {currentStatus}.\n\nOnly scheduled appointments can be cancelled.", 
                    "Cannot Cancel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure you want to cancel this appointment?", "Confirm Cancellation", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var result = dbHelper.ExecuteProcNonQuery("usp_Appointment_UpdateStatus", new
                    {
                        AppointmentId = _appointmentId,
                        NewStatus = "Cancelled",
                        PerformedBy = _currentUser.UserID,
                        Notes = "Cancelled by provider"
                    });

                    if (result > 0)
                    {
                        MessageBox.Show("Appointment cancelled!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAppointmentDetails(); // Refresh
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error cancelling appointment: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnViewConsultation_Click(object sender, EventArgs e)
        {
            try
            {
                using (var consultationForm = new frmConsultation(_appointmentId, _currentUser.UserID))
                {
                    consultationForm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening consultation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}