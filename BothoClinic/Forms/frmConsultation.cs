using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using System.Collections.Generic;

namespace BothoClinic
{
    public partial class frmConsultation : Form
    {
        private readonly int _appointmentId;
        private readonly int _providerId;
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        private int _patientId;
        private DataTable _prescriptionsDataTable;

        public frmConsultation(int appointmentId, int providerId)
        {
            InitializeComponent();
            _appointmentId = appointmentId;
            _providerId = providerId;
            _prescriptionsDataTable = new DataTable();
            InitializeConsultationForm();
        }

        private void InitializeConsultationForm()
        {
            try
            {
                // Get Appointment Details
                string sqlAppointment = @"
                    SELECT a.PatientId, p.StudentId, u.FullName as PatientName
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientId = p.PatientId
                    INNER JOIN Users u ON p.UserId = u.UserId
                    WHERE a.AppointmentId = @AppointmentId";
                
                DataTable dtAppointment = dbHelper.ExecuteQuery(sqlAppointment, new { AppointmentId = _appointmentId });

                if (dtAppointment.Rows.Count > 0)
                {
                    _patientId = Convert.ToInt32(dtAppointment.Rows[0]["PatientId"]);
                    lblPatientInfo.Text = $"Patient: {dtAppointment.Rows[0]["PatientName"]} (ID: {dtAppointment.Rows[0]["StudentId"]})";
                }
                else
                {
                    MessageBox.Show("Appointment details not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                // Load Medical History
                LoadMedicalHistory();

                // Load Medications for Prescription Tab
                LoadMedications();

                // Initialize Prescriptions DataTable
                _prescriptionsDataTable = new DataTable();
                _prescriptionsDataTable.Columns.Add("MedicationID", typeof(int));
                _prescriptionsDataTable.Columns.Add("MedicationName", typeof(string));
                _prescriptionsDataTable.Columns.Add("Dosage", typeof(string));
                _prescriptionsDataTable.Columns.Add("Frequency", typeof(string));
                _prescriptionsDataTable.Columns.Add("Duration", typeof(string));
                _prescriptionsDataTable.Columns.Add("Instructions", typeof(string));
                dgvPrescriptions.DataSource = _prescriptionsDataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing consultation: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMedicalHistory()
        {
            try
            {
                string sql = @"
                    SELECT
                        a.AppointmentDate,
                        uProvider.FullName AS Provider,
                        c.DiagnosisNotes,
                        c.Temperature,
                        c.BloodPressure
                    FROM Consultations c
                    INNER JOIN Appointments a ON c.AppointmentId = a.AppointmentId
                    INNER JOIN Users uProvider ON c.ProviderId = uProvider.UserID
                    WHERE a.PatientId = @PatientId
                    ORDER BY a.AppointmentDate DESC";
                
                DataTable dt = dbHelper.ExecuteQuery(sql, new { PatientId = _patientId });
                dgvMedicalHistory.DataSource = dt;

                if (dgvMedicalHistory.Columns.Count > 0)
                {
                    dgvMedicalHistory.Columns["AppointmentDate"].HeaderText = "Date";
                    dgvMedicalHistory.Columns["Provider"].HeaderText = "Provider";
                    dgvMedicalHistory.Columns["Temperature"].HeaderText = "Temp (°C)";
                    dgvMedicalHistory.Columns["BloodPressure"].HeaderText = "BP";
                    dgvMedicalHistory.Columns["DiagnosisNotes"].HeaderText = "Diagnosis";
                    dgvMedicalHistory.AutoResizeColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading medical history: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMedications()
        {
            try
            {
                string sql = "SELECT MedicationId, MedicationName FROM Medications ORDER BY MedicationName";
                DataTable dt = dbHelper.ExecuteQuery(sql);
                cmbMedication.DataSource = dt;
                cmbMedication.DisplayMember = "MedicationName";
                cmbMedication.ValueMember = "MedicationId";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading medications: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddMedication_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbMedication.SelectedValue == null)
                {
                    MessageBox.Show("Please select a medication.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDosage.Text))
                {
                    MessageBox.Show("Please enter dosage.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtFrequency.Text))
                {
                    MessageBox.Show("Please enter frequency.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtDuration.Text))
                {
                    MessageBox.Show("Please enter duration.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow newRow = _prescriptionsDataTable.NewRow();
                newRow["MedicationID"] = Convert.ToInt32(cmbMedication.SelectedValue);
                newRow["MedicationName"] = cmbMedication.Text;
                newRow["Dosage"] = txtDosage.Text;
                newRow["Frequency"] = txtFrequency.Text;
                newRow["Duration"] = txtDuration.Text;
                newRow["Instructions"] = txtInstructions.Text;
                _prescriptionsDataTable.Rows.Add(newRow);

                MessageBox.Show("Medication added to prescription.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields for next entry
                cmbMedication.SelectedIndex = -1;
                txtDosage.Clear();
                txtFrequency.Clear();
                txtDuration.Clear();
                txtInstructions.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding medication: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveConsultation_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(txtDiagnosis.Text))
                {
                    MessageBox.Show("Please enter a diagnosis.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTemperature.Text))
                {
                    MessageBox.Show("Please enter temperature.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtBloodPressure.Text))
                {
                    MessageBox.Show("Please enter blood pressure.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Parse temperature
                if (!decimal.TryParse(txtTemperature.Text, out decimal temperature))
                {
                    MessageBox.Show("Please enter a valid temperature value.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Save Consultation Details
                string sqlConsultation = @"
                    INSERT INTO Consultations (AppointmentId, ProviderId, Temperature, BloodPressure, VitalsJson, DiagnosisNotes, FollowUpNeeded, ConsultationDate)
                    VALUES (@AppointmentId, @ProviderId, @Temperature, @BloodPressure, @VitalsJson, @DiagnosisNotes, @FollowUpNeeded, GETDATE());
                    SELECT SCOPE_IDENTITY();";

                object consultationId = dbHelper.ExecuteScalar(sqlConsultation, new
                {
                    AppointmentId = _appointmentId,
                    ProviderId = _providerId,
                    Temperature = temperature,
                    BloodPressure = txtBloodPressure.Text,
                    VitalsJson = txtVitals.Text,
                    DiagnosisNotes = txtDiagnosis.Text,
                    FollowUpNeeded = chkFollowUp.Checked
                });

                if (consultationId != null && consultationId != DBNull.Value)
                {
                    int newConsultationId = Convert.ToInt32(consultationId);

                    // Save Prescriptions if any
                    if (_prescriptionsDataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in _prescriptionsDataTable.Rows)
                        {
                            string sqlPrescription = @"
                                INSERT INTO Prescriptions (ConsultationId, MedicationId, Dosage, Quantity, Instructions, DispensedDate)
                                VALUES (@ConsultationId, @MedicationId, @Dosage, @Quantity, @Instructions, GETDATE())";

                            dbHelper.ExecuteNonQuery(sqlPrescription, new
                            {
                                ConsultationId = newConsultationId,
                                MedicationId = Convert.ToInt32(row["MedicationID"]),
                                Dosage = row["Dosage"].ToString(),
                                Quantity = 1,
                                Instructions = row["Instructions"].ToString()
                            });
                        }
                    }

                    // Update Appointment Status to Completed
                    string sqlUpdateAppointment = "UPDATE Appointments SET Status = 'Completed' WHERE AppointmentId = @AppointmentId";
                    dbHelper.ExecuteNonQuery(sqlUpdateAppointment, new { AppointmentId = _appointmentId });

                    MessageBox.Show("Consultation saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to save consultation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving consultation: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
