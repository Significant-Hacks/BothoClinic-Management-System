using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmNotifications : Form
    {
        private readonly int _userId;
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmNotifications(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void frmNotifications_Load(object sender, EventArgs e)
        {
            try
            {
                string sql = @"
                    SELECT r.Message, r.ReminderDate, r.IsRead
                    FROM Reminders r
                    INNER JOIN Patients p ON r.PatientID = p.PatientID
                    WHERE p.UserID = @UserID
                    ORDER BY r.ReminderDate DESC";

                DataTable dt = dbHelper.ExecuteQuery(sql, new { UserID = _userId });
                dgvNotifications.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading notifications: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
