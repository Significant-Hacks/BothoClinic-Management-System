using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmAuditLogs : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmAuditLogs()
        {
            InitializeComponent();
        }

        private void frmAuditLogs_Load(object sender, EventArgs e)
        {
            try
            {
                // This assumes an AuditLogs table exists with columns like LogId, Timestamp, UserId, Action, Details
                string sql = @"
                    SELECT LogId, Timestamp, u.Username, Action, Details
                    FROM AuditLogs al
                    INNER JOIN Users u ON al.UserId = u.UserID
                    ORDER BY Timestamp DESC";

                DataTable dt = dbHelper.ExecuteQuery(sql);
                dgvAuditLogs.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading audit logs: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
