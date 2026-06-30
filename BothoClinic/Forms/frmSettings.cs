using System;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmSettings : Form
    {
        private readonly int _userId;

        public frmSettings(int userId)
        {
            InitializeComponent();
            _userId = userId;
        }

        private void frmSettings_Load(object sender, EventArgs e)
        {
            // Load user-specific settings here
            // For now, these are placeholders
            chkEmailNotifications.Checked = true;
            chkSMSNotifications.Checked = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Save changes to settings here
            MessageBox.Show("Settings saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
    }
}
