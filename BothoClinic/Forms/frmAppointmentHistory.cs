using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmAppointmentHistory : Form
    {
        private readonly int _patientId;
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmAppointmentHistory(int patientId)
        {
            InitializeComponent();
            _patientId = patientId;
        }

        private void frmAppointmentHistory_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = @"
                    SELECT a.AppointmentDate, u.FullName AS Provider, a.Reason, c.DiagnosisNotes AS Diagnosis
                    FROM Appointments a
                    LEFT JOIN Users u ON a.ProviderId = u.UserId
                    LEFT JOIN Consultations c ON a.AppointmentId = c.AppointmentId
                    WHERE a.PatientId = @PatientId
                    ORDER BY a.AppointmentDate DESC";

                DataTable dt = dbHelper.ExecuteQuery(sql, new { PatientId = _patientId });
                dgvAppointmentHistory.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading appointment history: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
