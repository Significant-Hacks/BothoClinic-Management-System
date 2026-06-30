using BothoClinic.Models;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmLogin : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();
        public User AuthenticatedUser { get; private set; }

        public frmLogin()
        {
            try
            {
                InitializeComponent();
                this.TopLevel = true;
                this.AcceptButton = btnLogin;
                this.txtUsername.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUsername_KeyDown);
                this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnLogin.PerformClick();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            lblMessage.Text = "";
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ShowMessage("Please enter both username and password.", Color.Red);
                return;
            }

            try
            {
                User user = AuthenticateUser(username, password);
                if (user != null)
                {
                    AuthenticatedUser = user;

                    if (user.MustChangePassword)
                    {
                        frmChangePassword changePasswordForm = new frmChangePassword(user);
                        changePasswordForm.ShowDialog();
                        ShowMessage("Password changed successfully. Please login again.", Color.Green);
                        txtPassword.Clear();
                        return;
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    // Audit failed login attempt
                    try
                    {
                        var auditHelper = new DatabaseHelper();
                        auditHelper.LogAuditToDatabase(
                            userId: 0, // Use 0 for system/failed login events
                            action: "LOGIN_FAILED",
                            tableName: "UserSessions",
                            recordId: "FAILED",
                            oldValues: null,
                            newValues: null,
                            details: $"Failed login attempt for username: '{username}'"
                        );
                    }
                    catch (Exception auditEx)
                    {
                        // Don't let audit logging failure break the login process
                        System.Diagnostics.Debug.WriteLine($"Failed to log failed login audit: {auditEx.Message}");
                    }
                    
                    ShowMessage("Invalid username or password.", Color.Red);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Unexpected error: " + ex.Message, Color.Red);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void linkForgotPassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmForgotPassword forgotPasswordForm = new frmForgotPassword();
            forgotPasswordForm.ShowDialog();
        }

        private void ShowMessage(string message, Color color)
        {
            lblMessage.Text = message;
            lblMessage.ForeColor = color;
        }

        private User AuthenticateUser(string username, string password)
        {
            string sql = @"
                SELECT u.UserID, u.Username, u.FullName, u.ContactEmail, u.ContactPhone, u.PasswordHash, u.Salt, 
                       u.RoleID, r.RoleName, u.IsActive, u.MustChangePassword
                FROM Users u
                JOIN Roles r ON u.RoleID = r.RoleID
                WHERE u.Username = @Username AND u.IsActive = 1";

            DataTable dt = dbHelper.ExecuteQuery(sql, new { Username = username });

            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                string storedHash = row["PasswordHash"].ToString();
                string storedSalt = row["Salt"].ToString();

                if (SecurityHelper.VerifyPasswordHash(password, storedHash, storedSalt))
                {
                    return new User
                    {
                        UserID = Convert.ToInt32(row["UserID"]),
                        RoleID = Convert.ToInt32(row["RoleID"]),
                        Username = row["Username"].ToString(),
                        FullName = row["FullName"].ToString(),
                        Email = row["ContactEmail"].ToString(),
                        ContactPhone = row["ContactPhone"].ToString(),
                        RoleName = row["RoleName"].ToString(),
                        PasswordHash = storedHash,
                        PasswordSalt = storedSalt,
                        IsActive = Convert.ToBoolean(row["IsActive"]),
                        MustChangePassword = Convert.ToBoolean(row["MustChangePassword"])
                    };
                }
            }

            return null;
        }
    }
}