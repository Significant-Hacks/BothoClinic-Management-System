using System;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using BothoClinic.Models;

namespace BothoClinic
{
    public partial class frmChangePassword : Form
    {
        private readonly User _currentUser;
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmChangePassword(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            this.AcceptButton = btnChangePassword;
            this.txtNewPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNewPassword_KeyDown);
            this.txtConfirmPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtConfirmPassword_KeyDown);
        }

        private void txtNewPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtConfirmPassword_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnChangePassword.PerformClick();
            }
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (txtNewPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("New passwords do not match.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SecurityHelper.CreatePasswordHash(txtNewPassword.Text, out string newPasswordHash, out string newSalt);
                string sqlUpdatePassword = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt, MustChangePassword = 0 WHERE UserID = @UserID";
                dbHelper.ExecuteNonQuery(sqlUpdatePassword, new
                {
                    PasswordHash = newPasswordHash,
                    Salt = newSalt,
                    UserID = _currentUser.UserID
                });

                MessageBox.Show("Password changed successfully. Please log in again.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}