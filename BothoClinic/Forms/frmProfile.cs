using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;
using BothoClinic.Models;

namespace BothoClinic
{
    public partial class frmProfile : Form
    {
        private readonly User _currentUser;
        // Assuming DatabaseHelper class is defined elsewhere and accessible
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmProfile(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;

            // Wire up the button click event
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            this.Load += new System.EventHandler(this.frmProfile_Load);
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            // FIX: Use the 'Email' property on the User model instead of the non-existent 'ContactEmail'
            txtFullName.Text = _currentUser.FullName;
            txtEmail.Text = _currentUser.Email;
            txtContactPhone.Text = _currentUser.ContactPhone;
            // Optionally, display the Username
            // txtUsername.Text = _currentUser.Username; 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Simple validation check before saving
            if (string.IsNullOrWhiteSpace(txtFullName.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Full Name and Email cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Update profile information (FullName, ContactEmail, ContactPhone)
                // Note: The parameters used here MUST match the column names in the 'Users' table
                string sqlUpdateProfile = "UPDATE Users SET FullName = @FullName, ContactEmail = @ContactEmail, ContactPhone = @ContactPhone WHERE UserID = @UserID";

                dbHelper.ExecuteNonQuery(sqlUpdateProfile, new
                {
                    FullName = txtFullName.Text,
                    // FIX: We use txtEmail.Text (the form input) but pass it to the DB column @ContactEmail
                    ContactEmail = txtEmail.Text,
                    ContactPhone = txtContactPhone.Text,
                    UserID = _currentUser.UserID
                });

                // 2. Change password if new password is provided
                if (!string.IsNullOrWhiteSpace(txtNewPassword.Text))
                {
                    if (txtNewPassword.Text != txtConfirmPassword.Text)
                    {
                        MessageBox.Show("New passwords do not match.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (string.IsNullOrWhiteSpace(txtOldPassword.Text))
                    {
                        MessageBox.Show("Please enter your old password to change it.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Verify old password
                    string sqlVerifyPassword = "SELECT PasswordHash, Salt FROM Users WHERE UserID = @UserID";
                    DataTable dt = dbHelper.ExecuteQuery(sqlVerifyPassword, new { UserID = _currentUser.UserID });

                    if (dt.Rows.Count == 0)
                    {
                        // Should not happen if the user is authenticated, but good for safety
                        MessageBox.Show("Could not retrieve current password hash.", "Security Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    string storedPasswordHash = dt.Rows[0]["PasswordHash"].ToString() ?? string.Empty;
                    string storedSalt = dt.Rows[0]["Salt"].ToString() ?? string.Empty;

                    // Assuming SecurityHelper is accessible
                    if (SecurityHelper.VerifyPasswordHash(txtOldPassword.Text, storedPasswordHash, storedSalt))
                    {
                        // Hash the new password and generate a new salt
                        SecurityHelper.CreatePasswordHash(txtNewPassword.Text, out string newPasswordHash, out string newSalt);

                        string sqlUpdatePassword = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt, MustChangePassword = 0 WHERE UserID = @UserID";

                        dbHelper.ExecuteNonQuery(sqlUpdatePassword, new
                        {
                            PasswordHash = newPasswordHash,
                            Salt = newSalt,
                            UserID = _currentUser.UserID
                        });

                        // Update the current user session object in memory with the new hash/salt
                        _currentUser.PasswordHash = newPasswordHash;
                        _currentUser.PasswordSalt = newSalt;
                        _currentUser.MustChangePassword = false;

                        // Clear the password fields on successful change
                        txtOldPassword.Text = string.Empty;
                        txtNewPassword.Text = string.Empty;
                        txtConfirmPassword.Text = string.Empty;

                    }
                    else
                    {
                        MessageBox.Show("Incorrect old password.", "Password Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Update the current user session object in memory with the new profile data
                _currentUser.FullName = txtFullName.Text;
                _currentUser.Email = txtEmail.Text;
                _currentUser.ContactPhone = txtContactPhone.Text;

                MessageBox.Show("Profile updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating profile: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}