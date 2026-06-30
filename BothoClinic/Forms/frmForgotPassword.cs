using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmForgotPassword : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmForgotPassword()
        {
            InitializeComponent();
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            string identifier = txtEmail.Text.Trim();

            if (string.IsNullOrWhiteSpace(identifier))
            {
                MessageBox.Show("Please enter your email or username.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string sql;
                if (rbEmail.Checked)
                {
                    sql = "SELECT UserID FROM Users WHERE ContactEmail = @Identifier";
                }
                else
                {
                    sql = "SELECT UserID FROM Users WHERE Username = @Identifier";
                }

                object userId = dbHelper.ExecuteScalar(sql, new { Identifier = identifier });

                if (userId != null)
                {
                    string defaultPassword = "Password123!";
                    SecurityHelper.CreatePasswordHash(defaultPassword, out string newPasswordHash, out string newSalt);

                    string updateSql = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt, MustChangePassword = 1 WHERE UserID = @UserID";
                    dbHelper.ExecuteNonQuery(updateSql, new { PasswordHash = newPasswordHash, Salt = newSalt, UserID = userId });

                    MessageBox.Show("Your password has been reset to \"Password123!\". You will be required to change it on your next login.", "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No user found with that email or username.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}