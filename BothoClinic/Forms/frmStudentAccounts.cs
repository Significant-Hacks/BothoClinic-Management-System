using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmStudentAccounts : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmStudentAccounts()
        {
            InitializeComponent();
            this.Load += new System.EventHandler(this.frmStudentAccounts_Load);
            this.dgvStudents.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStudents_CellDoubleClick);
            this.btnAddStudent.Click += new System.EventHandler(this.btnAddStudent_Click);
            this.btnEditStudent.Click += new System.EventHandler(this.btnEditStudent_Click);
            this.btnDeleteStudent.Click += new System.EventHandler(this.btnDeleteStudent_Click);
            this.btnDeactivateStudent.Click += new System.EventHandler(this.btnDeactivateStudent_Click);
            this.btnResetPassword.Click += new System.EventHandler(this.btnResetPassword_Click);
        }

        private void frmStudentAccounts_Load(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                string sql = @"
                    SELECT u.UserID, u.Username, u.FullName, u.ContactEmail, u.ContactPhone, p.StudentId, u.IsActive
                    FROM Users u
                    INNER JOIN Roles r ON u.RoleID = r.RoleID
                    LEFT JOIN Patients p ON u.UserID = p.UserID
                    WHERE r.RoleName = 'Student'";

                DataTable dt = dbHelper.ExecuteQuery(sql);
                dgvStudents.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading student accounts: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddStudent_Click(object sender, EventArgs e)
        {
            frmUserRegistration registrationForm = new frmUserRegistration();
            registrationForm.ShowDialog();
            LoadStudents(); // Refresh the list
        }

        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["UserID"].Value);
                frmEditUser editUserForm = new frmEditUser(userId);
                if (editUserForm.ShowDialog() == DialogResult.OK)
                {
                    LoadStudents(); // Refresh the list if changes were saved
                }
            }
            else
            {
                MessageBox.Show("Please select a student to edit.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["UserID"].Value);
                string username = dgvStudents.SelectedRows[0].Cells["Username"].Value.ToString();

                if (MessageBox.Show($"Are you sure you want to delete student '{username}'?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        string sql = "DELETE FROM Users WHERE UserID = @UserID";
                        int rowsAffected = dbHelper.ExecuteNonQuery(sql, new { UserID = userId });

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Student '{username}' deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStudents(); // Refresh the list
                        }
                        else
                        {
                            MessageBox.Show($"Error deleting student '{username}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the student: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a student to delete.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnDeactivateStudent_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["UserID"].Value);
                string username = dgvStudents.SelectedRows[0].Cells["Username"].Value.ToString();

                if (MessageBox.Show($"Are you sure you want to deactivate student '{username}'?", "Confirm Deactivation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        string sql = "UPDATE Users SET IsActive = 0 WHERE UserID = @UserID";
                        int rowsAffected = dbHelper.ExecuteNonQuery(sql, new { UserID = userId });

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Student '{username}' deactivated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStudents(); // Refresh the list
                        }
                        else
                        {
                            MessageBox.Show($"Error deactivating student '{username}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deactivating the student: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a student to deactivate.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvStudents.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvStudents.SelectedRows[0].Cells["UserID"].Value);
                string username = dgvStudents.SelectedRows[0].Cells["Username"].Value.ToString();

                if (MessageBox.Show($"Are you sure you want to reset password for student '{username}'? A default password will be set, and the student will be forced to change it on next login.", "Confirm Password Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    try
                    {
                        string defaultPassword = "Password123"; // Or generate a random one
                        SecurityHelper.CreatePasswordHash(defaultPassword, out string newPasswordHash, out string newSalt);

                        string sql = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt, MustChangePassword = 1 WHERE UserID = @UserID";
                        int rowsAffected = dbHelper.ExecuteNonQuery(sql, new { PasswordHash = newPasswordHash, Salt = newSalt, UserID = userId });

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"Password for student '{username}' reset successfully. Default password is '{defaultPassword}'. Student must change it on next login.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStudents(); // Refresh the list
                        }
                        else
                        {
                            MessageBox.Show($"Error resetting password for student '{username}'.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while resetting the password: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a student to reset password.", "No Student Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int userId = Convert.ToInt32(dgvStudents.Rows[e.RowIndex].Cells["UserID"].Value);
                frmEditUser editUserForm = new frmEditUser(userId);
                if (editUserForm.ShowDialog() == DialogResult.OK)
                {
                    LoadStudents(); // Refresh the list if changes were saved
                }
            }
        }
    }
}
