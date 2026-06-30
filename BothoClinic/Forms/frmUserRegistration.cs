using BothoClinic; // Required for the DatabaseHelper and SecurityHelper classes
using System;
using System.Data;
using System.Windows.Forms;

namespace BothoClinic
{
    /// <summary>
    /// Handles the creation of new user accounts (Admin, Provider, or Student).
    /// Implements all validation and uses SecurityHelper to hash and salt passwords before saving.
    /// </summary>
    public partial class frmUserRegistration : Form
    {
        private readonly DatabaseHelper dbHelper = new DatabaseHelper();

        private readonly bool _providerContext;

        public frmUserRegistration(bool providerContext = false)
        {
            InitializeComponent();
            _providerContext = providerContext;
            LoadRoles();
            this.cmbRole.SelectedIndexChanged += new System.EventHandler(this.cmbRole_SelectedIndexChanged);
            this.AcceptButton = btnRegister;

            // Add KeyDown event handlers to all textboxes
            foreach (Control c in this.Controls)
            {
                if (c is TextBox)
                {
                    c.KeyDown += new KeyEventHandler(txt_KeyDown);
                }
            }
            foreach (Control c in this.pnlStudentFields.Controls)
            {
                if (c is TextBox)
                {
                    c.KeyDown += new KeyEventHandler(txt_KeyDown);
                }
            }
        }

        private void txt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                this.SelectNextControl((Control)sender, true, true, true, true);
            }
        }

        private void LoadRoles()
        {
            try
            {
                string sql = "SELECT RoleID, RoleName FROM Roles ORDER BY RoleName";
                DataTable dt = dbHelper.ExecuteQuery(sql);

                // If launched from Provider context, restrict roles to Guest Patient (or Student if needed)
                if (_providerContext && dt.Columns.Contains("RoleName"))
                {
                    // Prefer 'Student' and a non-student 'Guest Patient' role if present.
                    // If DB does not have Guest/Patient role, fall back to Student only.
                    DataView view = new DataView(dt);
                    if (dt.AsEnumerable().Any(r => string.Equals(r.Field<string>("RoleName"), "Guest Patient", StringComparison.OrdinalIgnoreCase)))
                    {
                        view.RowFilter = "RoleName = 'Guest Patient' OR RoleName = 'Student'";
                    }
                    else
                    {
                        view.RowFilter = "RoleName = 'Student'";
                    }
                    dt = view.ToTable();
                }

                cmbRole.DataSource = dt;
                cmbRole.DisplayMember = "RoleName";
                cmbRole.ValueMember = "RoleID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cmbRole_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbRole.Text == "Student")
            {
                pnlStudentFields.Visible = true;
            }
            else
            {
                pnlStudentFields.Visible = false;
            }
        }

        /// <summary>
        /// Handles the "Register" button click event.
        /// </summary>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Input Validation
            if (!ValidateInputs())
            {
                return;
            }

            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;
            string fullName = txtFullName.Text.Trim();
            string contactPhone = txtContactPhone.Text.Trim();
            int roleID = Convert.ToInt32(cmbRole.SelectedValue);

            // 2. Check for existing username or email to prevent duplicates
            if (IsUserAlreadyRegistered(username, email))
            {
                MessageBox.Show("A user with this Username or Email already exists. Please choose different credentials.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Security: Hash and Salt the password
            SecurityHelper.CreatePasswordHash(password, out string hash, out string salt);

            // 4. Database Insertion
            try
            {
                string sqlInsertUser = @"
                    INSERT INTO Users (Username, FullName, ContactEmail, ContactPhone, PasswordHash, Salt, RoleId, IsActive, MustChangePassword)
                    VALUES (@Username, @FullName, @ContactEmail, @ContactPhone, @PasswordHash, @Salt, @RoleId, @IsActive, @MustChangePassword);
                    SELECT SCOPE_IDENTITY();";

                object? newUserId = dbHelper.ExecuteScalar(sqlInsertUser, new
                {
                    Username = username,
                    FullName = fullName,
                    ContactEmail = email,
                    ContactPhone = contactPhone,
                    PasswordHash = hash,
                    Salt = salt,
                    RoleId = roleID,
                    IsActive = true,
                    MustChangePassword = true
                });

                if (newUserId != null && cmbRole.Text == "Student")
                {
                    string sqlInsertPatient = @"
                        INSERT INTO Patients (UserId, StudentId, DOB, Gender, BloodType)
                        VALUES (@UserId, @StudentId, @DOB, @Gender, @BloodType)";

                    dbHelper.ExecuteNonQuery(sqlInsertPatient, new
                    {
                        UserId = newUserId,
                        StudentId = txtStudentId.Text.Trim(),
                        DOB = dtpDOB.Value,
                        Gender = cmbGender.Text,
                        BloodType = txtBloodType.Text.Trim()
                    });
                }

                MessageBox.Show("Registration successful! You can now log in.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred during registration: {ex.Message}", "Application Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Validates all required user input fields before proceeding with registration.
        /// </summary>
        /// <returns>True if all inputs are valid, False otherwise.</returns>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtPassword.Text) ||
                string.IsNullOrWhiteSpace(txtConfirmPassword.Text) ||
                cmbRole.SelectedValue == null)
            {
                MessageBox.Show("Please fill out all required fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbRole.Text == "Student")
            {
                if (string.IsNullOrWhiteSpace(txtStudentId.Text) ||
                    cmbGender.SelectedItem == null)
                {
                    MessageBox.Show("Please fill out all required student fields.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password and Confirm Password must match.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (txtPassword.Text.Length < 8)
            {
                MessageBox.Show("Password must be at least 8 characters long.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks the database to see if the provided username or email already exists.
        /// </summary>
        private bool IsUserAlreadyRegistered(string username, string email)
        {
            string sql = "SELECT COUNT(*) FROM Users WHERE Username = @Username OR ContactEmail = @Email";
            object? count = dbHelper.ExecuteScalar(sql, new { Username = username, Email = email });

            return Convert.ToInt32(count) > 0;
        }

        /// <summary>
        /// Handles the "Cancel" button click event.
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}