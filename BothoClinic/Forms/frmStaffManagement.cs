using System;
using System.Data;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmStaffManagement : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmStaffManagement()
        {
            InitializeComponent();
            LoadStaffAccounts();
        }

        private void LoadStaffAccounts()
        {
            try
            {
                string sql = @"
                    SELECT 
                        u.UserID,
                        u.Username,
                        u.FullName,
                        u.ContactEmail,
                        u.ContactPhone,
                        r.RoleName,
                        CASE WHEN u.IsActive = 1 THEN 'Active' ELSE 'Inactive' END AS Status,
                        u.IsActive
                    FROM Users u
                    INNER JOIN Roles r ON u.RoleID = r.RoleID
                    WHERE r.RoleName IN ('Provider', 'Admin', 'Staff')
                    ORDER BY u.FullName";

                DataTable dt = dbHelper.ExecuteQuery(sql);
                dgvStaff.DataSource = dt;

                // Hide the IsActive column but keep it for functionality
                if (dgvStaff.Columns["IsActive"] != null)
                    dgvStaff.Columns["IsActive"].Visible = false;

                // Set column headers
                if (dgvStaff.Columns["UserID"] != null) dgvStaff.Columns["UserID"].HeaderText = "ID";
                if (dgvStaff.Columns["FullName"] != null) dgvStaff.Columns["FullName"].HeaderText = "Full Name";
                if (dgvStaff.Columns["ContactEmail"] != null) dgvStaff.Columns["ContactEmail"].HeaderText = "Email";
                if (dgvStaff.Columns["ContactPhone"] != null) dgvStaff.Columns["ContactPhone"].HeaderText = "Phone";
                if (dgvStaff.Columns["RoleName"] != null) dgvStaff.Columns["RoleName"].HeaderText = "Role";

                lblStaffCount.Text = $"Total Staff: {dt.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading staff accounts: {ex.Message}", "Database Error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            // Open User Registration form as specified in question paper
            using (var userRegForm = new frmUserRegistration())
            {
                if (userRegForm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadStaffAccounts(); // Refresh after adding
                }
            }
        }

        private void btnDeactivateAccount_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a staff member to deactivate.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int userId = Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["UserID"].Value);
            string fullName = dgvStaff.SelectedRows[0].Cells["FullName"].Value.ToString();
            bool isActive = Convert.ToBoolean(dgvStaff.SelectedRows[0].Cells["IsActive"].Value);

            string action = isActive ? "deactivate" : "activate";
            string confirmMessage = $"Are you sure you want to {action} {fullName}?";

            if (MessageBox.Show(confirmMessage, $"Confirm {action}", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    string sql = "UPDATE Users SET IsActive = @IsActive WHERE UserID = @UserID";
                    int rowsAffected = dbHelper.ExecuteNonQuery(sql, new { 
                        IsActive = !isActive, 
                        UserID = userId 
                    });

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Staff account {action}d successfully!", "Success", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadStaffAccounts(); // Refresh the list
                    }
                    else
                    {
                        MessageBox.Show($"Failed to {action} staff account.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating staff account: {ex.Message}", "Database Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a staff member to reset password.", "No Selection", 
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int userId = Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["UserID"].Value);
            string fullName = dgvStaff.SelectedRows[0].Cells["FullName"].Value.ToString();

            if (MessageBox.Show($"Reset password for {fullName}?\\nA temporary password will be generated.", 
                "Confirm Password Reset", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Generate temporary password using your existing SecurityHelper
                    string tempPassword = "TempPass123!";
                    string hashedPassword, salt;
                    SecurityHelper.CreatePasswordHash(tempPassword, out hashedPassword, out salt);

                    string sql = "UPDATE Users SET PasswordHash = @PasswordHash, Salt = @Salt, MustChangePassword = 1 WHERE UserID = @UserID";
                    int rowsAffected = dbHelper.ExecuteNonQuery(sql, new { 
                        PasswordHash = hashedPassword, 
                        Salt = salt, 
                        UserID = userId 
                    });

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show($"Password reset successfully!\\n\\nTemporary Password: {tempPassword}\\n\\nUser must change password on next login.", 
                            "Password Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Failed to reset password.", "Error", 
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error resetting password: {ex.Message}", "Database Error", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadStaffAccounts();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (dgvStaff.DataSource is DataTable dt)
            {
                string searchText = txtSearch.Text.Trim();
                if (string.IsNullOrEmpty(searchText))
                {
                    dt.DefaultView.RowFilter = "";
                }
                else
                {
                    dt.DefaultView.RowFilter = $"FullName LIKE '%{searchText}%' OR Username LIKE '%{searchText}%' OR ContactEmail LIKE '%{searchText}%' OR RoleName LIKE '%{searchText}%'";
                }
            }
        }
    }
}