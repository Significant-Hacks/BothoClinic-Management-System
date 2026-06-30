
using System;
using System.Data;
using System.Windows.Forms;

namespace BothoClinic
{
    public partial class frmEditUser : Form
    {
        private readonly int _userId;
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        public frmEditUser(int userId)
        {
            InitializeComponent();
            _userId = userId;
            LoadUserData();
            LoadRoles();
        }

        private void LoadUserData()
        {
            try
            {
                string sql = "SELECT UserID, Username, FullName, ContactEmail, ContactPhone, RoleID, IsActive FROM Users WHERE UserID = @UserID";
                DataTable dt = dbHelper.ExecuteQuery(sql, new { UserID = _userId });

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txtFullName.Text = row["FullName"].ToString();
                    txtContactEmail.Text = row["ContactEmail"].ToString();
                    txtContactPhone.Text = row["ContactPhone"].ToString();
                    chkIsActive.Checked = Convert.ToBoolean(row["IsActive"]);
                    cmbRole.SelectedValue = Convert.ToInt32(row["RoleID"]);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadRoles()
        {
            try
            {
                string sql = "SELECT RoleID, RoleName FROM Roles ORDER BY RoleName";
                DataTable dt = dbHelper.ExecuteQuery(sql);
                cmbRole.DataSource = dt;
                cmbRole.DisplayMember = "RoleName";
                cmbRole.ValueMember = "RoleID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string sql = @"
                    UPDATE Users
                    SET FullName = @FullName,
                        ContactEmail = @ContactEmail,
                        ContactPhone = @ContactPhone,
                        RoleID = @RoleID,
                        IsActive = @IsActive
                    WHERE UserID = @UserID";

                int rowsAffected = dbHelper.ExecuteNonQuery(sql, new
                {
                    FullName = txtFullName.Text,
                    ContactEmail = txtContactEmail.Text,
                    ContactPhone = txtContactPhone.Text,
                    RoleID = Convert.ToInt32(cmbRole.SelectedValue),
                    IsActive = chkIsActive.Checked,
                    UserID = _userId
                });

                if (rowsAffected > 0)
                {
                    MessageBox.Show("User updated successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("User update failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while updating the user: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
