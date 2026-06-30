namespace BothoClinic
{
    partial class frmUserRegistration
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

                #region Windows Form Designer generated code

        

                /// <summary>

                /// Required method for Designer support - do not modify

                /// the contents of this method with the code editor.

                /// </summary>

                private void InitializeComponent()

                {

                    this.components = new System.ComponentModel.Container();

                    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;

                    this.ClientSize = new System.Drawing.Size(600, 700);

                    this.Text = "New User Registration";

                    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;

                    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

                    this.MaximizeBox = false;

                    this.MinimizeBox = false;

        

                    // Title

                    this.lblTitle = new Label();

                    this.lblTitle.Text = "User Registration";

                    this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(44, 62, 80);

                    this.lblTitle.Size = new System.Drawing.Size(600, 45);

                    this.lblTitle.Location = new System.Drawing.Point(0, 20);

                    this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        

                    // Subtitle

                    this.lblSubtitle = new Label();

                    this.lblSubtitle.Text = "Create a new user account for the Botho University Clinic";

                    this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(127, 140, 141);

                    this.lblSubtitle.Size = new System.Drawing.Size(600, 32);

                    this.lblSubtitle.Location = new System.Drawing.Point(0, 70);

                    this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

        

                    // Full Name

                    this.lblFullName = new Label();

                    this.lblFullName.Text = "Full Name";

                    this.lblFullName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblFullName.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblFullName.Location = new System.Drawing.Point(40, 120);

                    this.lblFullName.AutoSize = true;

        

                    this.txtFullName = new TextBox();

                    this.txtFullName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.txtFullName.Size = new System.Drawing.Size(520, 29);

                    this.txtFullName.Location = new System.Drawing.Point(40, 150);

        

                    // Email

                    this.lblEmail = new Label();

                    this.lblEmail.Text = "Email Address";

                    this.lblEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblEmail.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblEmail.Location = new System.Drawing.Point(40, 190);

                    this.lblEmail.AutoSize = true;

        

                    this.txtEmail = new TextBox();

                    this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.txtEmail.Size = new System.Drawing.Size(250, 29);

                    this.txtEmail.Location = new System.Drawing.Point(40, 220);

        

                    // Contact Phone

                    this.lblContactPhone = new Label();

                    this.lblContactPhone.Text = "Contact Phone";

                    this.lblContactPhone.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblContactPhone.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblContactPhone.Location = new System.Drawing.Point(310, 190);

                    this.lblContactPhone.AutoSize = true;

        

                    this.txtContactPhone = new TextBox();

                    this.txtContactPhone.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.txtContactPhone.Size = new System.Drawing.Size(250, 29);

                    this.txtContactPhone.Location = new System.Drawing.Point(310, 220);

        

                    // Username

                    this.lblUsername = new Label();

                    this.lblUsername.Text = "Username";

                    this.lblUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblUsername.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblUsername.Location = new System.Drawing.Point(40, 260);

                    this.lblUsername.AutoSize = true;

        

                    this.txtUsername = new TextBox();

                    this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.txtUsername.Size = new System.Drawing.Size(250, 29);

                    this.txtUsername.Location = new System.Drawing.Point(40, 290);

        

                    // Role

                    this.lblRole = new Label();

                    this.lblRole.Text = "User Role";

                    this.lblRole.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblRole.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblRole.Location = new System.Drawing.Point(310, 260);

                    this.lblRole.AutoSize = true;

        

                    this.cmbRole = new ComboBox();

                    this.cmbRole.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.cmbRole.Size = new System.Drawing.Size(250, 29);

                    this.cmbRole.Location = new System.Drawing.Point(310, 290);

                    this.cmbRole.DropDownStyle = ComboBoxStyle.DropDownList;

        

                    // Password

                    this.lblPassword = new Label();

                    this.lblPassword.Text = "Password (min. 8 characters)";

                    this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblPassword.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblPassword.Location = new System.Drawing.Point(40, 330);

                    this.lblPassword.AutoSize = true;

        

                    this.txtPassword = new TextBox();

                    this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.txtPassword.Size = new System.Drawing.Size(250, 29);

                    this.txtPassword.Location = new System.Drawing.Point(40, 360);

                    this.txtPassword.UseSystemPasswordChar = true;

        

                    // Confirm Password

                    this.lblConfirmPassword = new Label();

                    this.lblConfirmPassword.Text = "Confirm Password";

                    this.lblConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblConfirmPassword.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblConfirmPassword.Location = new System.Drawing.Point(310, 330);

                    this.lblConfirmPassword.AutoSize = true;

        

                    this.txtConfirmPassword = new TextBox();

                    this.txtConfirmPassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.txtConfirmPassword.Size = new System.Drawing.Size(250, 29);

                    this.txtConfirmPassword.Location = new System.Drawing.Point(310, 360);

                    this.txtConfirmPassword.UseSystemPasswordChar = true;

        

                    // --- Student Fields Panel ---

                    this.pnlStudentFields = new Panel();

                    this.pnlStudentFields.Location = new System.Drawing.Point(40, 400);

                    this.pnlStudentFields.Size = new System.Drawing.Size(520, 200);

                    this.pnlStudentFields.Visible = false; // Initially hidden

        

                    // Student ID

                    this.lblStudentId = new Label();

                    this.lblStudentId.Text = "Student ID";

                    this.lblStudentId.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblStudentId.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblStudentId.Location = new System.Drawing.Point(0, 10);

                    this.lblStudentId.AutoSize = true;

        

                    this.txtStudentId = new TextBox();

                    this.txtStudentId.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.txtStudentId.Size = new System.Drawing.Size(250, 29);

                    this.txtStudentId.Location = new System.Drawing.Point(0, 40);

        

                    // Date of Birth

                    this.lblDOB = new Label();

                    this.lblDOB.Text = "Date of Birth";

                    this.lblDOB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblDOB.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblDOB.Location = new System.Drawing.Point(270, 10);

                    this.lblDOB.AutoSize = true;

        

                    this.dtpDOB = new DateTimePicker();

                    this.dtpDOB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.dtpDOB.Size = new System.Drawing.Size(250, 29);

                    this.dtpDOB.Location = new System.Drawing.Point(270, 40);

                    this.dtpDOB.Format = DateTimePickerFormat.Short;

        

                    // Gender

                    this.lblGender = new Label();

                    this.lblGender.Text = "Gender";

                    this.lblGender.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblGender.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblGender.Location = new System.Drawing.Point(0, 80);

                    this.lblGender.AutoSize = true;

        

                    this.cmbGender = new ComboBox();

                    this.cmbGender.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.cmbGender.Size = new System.Drawing.Size(250, 29);

                    this.cmbGender.Location = new System.Drawing.Point(0, 110);

                    this.cmbGender.DropDownStyle = ComboBoxStyle.DropDownList;

                    this.cmbGender.Items.AddRange(new object[] { "Male", "Female", "Other" });

        

                    // Blood Type

                    this.lblBloodType = new Label();

                    this.lblBloodType.Text = "Blood Type (Optional)";

                    this.lblBloodType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.lblBloodType.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.lblBloodType.Location = new System.Drawing.Point(270, 80);

                    this.lblBloodType.AutoSize = true;

        

                    this.txtBloodType = new TextBox();

                    this.txtBloodType.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);

                    this.txtBloodType.Size = new System.Drawing.Size(250, 29);

                    this.txtBloodType.Location = new System.Drawing.Point(270, 110);

        

                    this.pnlStudentFields.Controls.Add(this.lblStudentId);

                    this.pnlStudentFields.Controls.Add(this.txtStudentId);

                    this.pnlStudentFields.Controls.Add(this.lblDOB);

                    this.pnlStudentFields.Controls.Add(this.dtpDOB);

                    this.pnlStudentFields.Controls.Add(this.lblGender);

                    this.pnlStudentFields.Controls.Add(this.cmbGender);

                    this.pnlStudentFields.Controls.Add(this.lblBloodType);

                    this.pnlStudentFields.Controls.Add(this.txtBloodType);

        

                    // Buttons

                    this.btnCancel = new Button();

                    this.btnCancel.Text = "Cancel";

                    this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

                    this.btnCancel.ForeColor = System.Drawing.Color.FromArgb(52, 73, 94);

                    this.btnCancel.BackColor = System.Drawing.Color.FromArgb(236, 240, 241);

                    this.btnCancel.Size = new System.Drawing.Size(250, 50);

                    this.btnCancel.Location = new System.Drawing.Point(40, 620);

                    this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

        

                    this.btnRegister = new Button();

                    this.btnRegister.Text = "Create User Account";

                    this.btnRegister.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);

                    this.btnRegister.ForeColor = System.Drawing.Color.White;

                    this.btnRegister.BackColor = System.Drawing.Color.FromArgb(52, 152, 219);

                    this.btnRegister.Size = new System.Drawing.Size(250, 50);

                    this.btnRegister.Location = new System.Drawing.Point(310, 620);

                    this.btnRegister.Click += new System.EventHandler(this.btnRegister_Click);

        

                    this.Controls.Add(this.lblTitle);

                    this.Controls.Add(this.lblSubtitle);

                    this.Controls.Add(this.lblFullName);

                    this.Controls.Add(this.txtFullName);

                    this.Controls.Add(this.lblEmail);

                    this.Controls.Add(this.txtEmail);

                    this.Controls.Add(this.lblContactPhone);

                    this.Controls.Add(this.txtContactPhone);

                    this.Controls.Add(this.lblUsername);

                    this.Controls.Add(this.txtUsername);

                    this.Controls.Add(this.lblRole);

                    this.Controls.Add(this.cmbRole);

                    this.Controls.Add(this.lblPassword);

                    this.Controls.Add(this.txtPassword);

                    this.Controls.Add(this.lblConfirmPassword);

                    this.Controls.Add(this.txtConfirmPassword);

                    this.Controls.Add(this.pnlStudentFields);

                    this.Controls.Add(this.btnCancel);

                    this.Controls.Add(this.btnRegister);

                }

        

                #endregion

        

                private System.Windows.Forms.Label lblTitle;

                private System.Windows.Forms.Label lblSubtitle;

                private System.Windows.Forms.Label lblFullName;

                private System.Windows.Forms.TextBox txtFullName;

                private System.Windows.Forms.Label lblEmail;

                private System.Windows.Forms.TextBox txtEmail;

                private System.Windows.Forms.Label lblContactPhone;

                private System.Windows.Forms.TextBox txtContactPhone;

                private System.Windows.Forms.Label lblUsername;

                private System.Windows.Forms.TextBox txtUsername;

                private System.Windows.Forms.Label lblRole;

                private System.Windows.Forms.ComboBox cmbRole;

                private System.Windows.Forms.Label lblPassword;

                private System.Windows.Forms.TextBox txtPassword;

                private System.Windows.Forms.Label lblConfirmPassword;

                private System.Windows.Forms.TextBox txtConfirmPassword;

                private System.Windows.Forms.Panel pnlStudentFields;

                private System.Windows.Forms.Label lblStudentId;

                private System.Windows.Forms.TextBox txtStudentId;

                private System.Windows.Forms.Label lblDOB;

                private System.Windows.Forms.DateTimePicker dtpDOB;

                private System.Windows.Forms.Label lblGender;

                private System.Windows.Forms.ComboBox cmbGender;

                private System.Windows.Forms.Label lblBloodType;

                private System.Windows.Forms.TextBox txtBloodType;

                private System.Windows.Forms.Button btnRegister;

                private System.Windows.Forms.Button btnCancel;

    }
}