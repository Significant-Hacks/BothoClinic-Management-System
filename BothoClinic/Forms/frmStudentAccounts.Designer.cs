namespace BothoClinic
{
    partial class frmStudentAccounts
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
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnResetPassword = new System.Windows.Forms.Button();
            this.btnDeactivateStudent = new System.Windows.Forms.Button();
            this.btnDeleteStudent = new System.Windows.Forms.Button();
            this.btnEditStudent = new System.Windows.Forms.Button();
            this.btnAddStudent = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvStudents
            // 
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvStudents.Location = new System.Drawing.Point(0, 0);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.Size = new System.Drawing.Size(800, 390);
            this.dgvStudents.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnResetPassword);
            this.pnlButtons.Controls.Add(this.btnDeactivateStudent);
            this.pnlButtons.Controls.Add(this.btnDeleteStudent);
            this.pnlButtons.Controls.Add(this.btnEditStudent);
            this.pnlButtons.Controls.Add(this.btnAddStudent);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 390);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(800, 60);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnResetPassword
            // 
            this.btnResetPassword.Location = new System.Drawing.Point(430, 19);
            this.btnResetPassword.Name = "btnResetPassword";
            this.btnResetPassword.Size = new System.Drawing.Size(100, 23);
            this.btnResetPassword.TabIndex = 4;
            this.btnResetPassword.Text = "Reset Password";
            this.btnResetPassword.UseVisualStyleBackColor = true;
            // 
            // btnDeactivateStudent
            // 
            this.btnDeactivateStudent.Location = new System.Drawing.Point(324, 19);
            this.btnDeactivateStudent.Name = "btnDeactivateStudent";
            this.btnDeactivateStudent.Size = new System.Drawing.Size(100, 23);
            this.btnDeactivateStudent.TabIndex = 3;
            this.btnDeactivateStudent.Text = "Deactivate Student";
            this.btnDeactivateStudent.UseVisualStyleBackColor = true;
            // 
            // btnDeleteStudent
            // 
            this.btnDeleteStudent.Location = new System.Drawing.Point(218, 19);
            this.btnDeleteStudent.Name = "btnDeleteStudent";
            this.btnDeleteStudent.Size = new System.Drawing.Size(100, 23);
            this.btnDeleteStudent.TabIndex = 2;
            this.btnDeleteStudent.Text = "Delete Student";
            this.btnDeleteStudent.UseVisualStyleBackColor = true;
            // 
            // btnEditStudent
            // 
            this.btnEditStudent.Location = new System.Drawing.Point(112, 19);
            this.btnEditStudent.Name = "btnEditStudent";
            this.btnEditStudent.Size = new System.Drawing.Size(100, 23);
            this.btnEditStudent.TabIndex = 1;
            this.btnEditStudent.Text = "Edit Student";
            this.btnEditStudent.UseVisualStyleBackColor = true;
            // 
            // btnAddStudent
            // 
            this.btnAddStudent.Location = new System.Drawing.Point(6, 19);
            this.btnAddStudent.Name = "btnAddStudent";
            this.btnAddStudent.Size = new System.Drawing.Size(100, 23);
            this.btnAddStudent.TabIndex = 0;
            this.btnAddStudent.Text = "Add Student";
            this.btnAddStudent.UseVisualStyleBackColor = true;
            // 
            // frmStudentAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.dgvStudents);
            this.Name = "frmStudentAccounts";
            this.Text = "Student Accounts";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnResetPassword;
        private System.Windows.Forms.Button btnDeactivateStudent;
        private System.Windows.Forms.Button btnDeleteStudent;
        private System.Windows.Forms.Button btnEditStudent;
        private System.Windows.Forms.Button btnAddStudent;
    }
}
