using System;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmReporting : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmReporting()
        {
            InitializeComponent();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStartDate.Value;
            DateTime endDate = dtpEndDate.Value;

            try
            {
                string sql = @"
                    SELECT
                        a.AppointmentDate,
                        a.TimeSlot,
                        uPatient.FullName AS PatientName,
                        uProvider.FullName AS ProviderName,
                        a.Reason,
                        a.Status,
                        c.DiagnosisNotes,
                        c.FollowUpNeeded
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientId = p.PatientId
                    INNER JOIN Users uPatient ON p.UserId = uPatient.UserID
                    LEFT JOIN Users uProvider ON a.ProviderId = uProvider.UserID
                    LEFT JOIN Consultations c ON a.AppointmentId = c.AppointmentId
                    WHERE a.AppointmentDate >= @StartDate AND a.AppointmentDate <= @EndDate
                    ORDER BY a.AppointmentDate, a.TimeSlot";

                DataTable dt = dbHelper.ExecuteQuery(sql, new { StartDate = startDate, EndDate = endDate });

                if (dt.Rows.Count > 0)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
                    saveFileDialog.Title = "Export Report to CSV";
                    saveFileDialog.FileName = $"ClinicActivityReport_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}.csv";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ToCsv(dt, saveFileDialog.FileName);
                        MessageBox.Show("Report exported successfully!", "Export Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No data found for the selected date range.", "No Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating report: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ToCsv(DataTable dt, string filePath)
        {
            StringBuilder sb = new StringBuilder();

            // Add header row
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                sb.Append(dt.Columns[i].ColumnName);
                if (i < dt.Columns.Count - 1)
                {
                    sb.Append(",");
                }
            }
            sb.AppendLine();

            // Add data rows
            foreach (DataRow row in dt.Rows)
            {
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb.Append(EscapeCsvField(row[i].ToString()));
                    if (i < dt.Columns.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.AppendLine();
            }

            File.WriteAllText(filePath, sb.ToString());
        }

        private string EscapeCsvField(string? field)
        {
            if (field == null) return "";
            // If the field contains a comma or double quote, it must be enclosed in double quotes.
            // Any double quotes within the field must be escaped by doubling them.
            if (field.Contains(",") || field.Contains("\""))
            {
                return "\"" + field.Replace("\"", "\"\"") + "\"";
            }
            return field;
        }
    }
}
