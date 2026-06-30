
namespace BothoClinic
{
    partial class frmEditUser
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
            this.ClientSize = new System.Drawing.Size(400, 450);
            this.Text = "Edit User";

            // Labels and TextBoxes
            this.lblFullName = new System.Windows.Forms.Label();
            this.lblFullName.Text = "Full Name";
            this.lblFullName.Location = new System.Drawing.Point(12, 15);

            this.txtFullName = new System.Windows.Forms.TextBox();
            this.txtFullName.Location = new System.Drawing.Point(12, 35);
            this.txtFullName.Size = new System.Drawing.Size(376, 20);

            this.lblContactEmail = new System.Windows.Forms.Label();
            this.lblContactEmail.Text = "Contact Email";
            this.lblContactEmail.Location = new System.Drawing.Point(12, 70);

            this.txtContactEmail = new System.Windows.Forms.TextBox();
            this.txtContactEmail.Location = new System.Drawing.Point(12, 90);
            this.txtContactEmail.Size = new System.Drawing.Size(376, 20);

            this.lblContactPhone = new System.Windows.Forms.Label();
            this.lblContactPhone.Text = "Contact Phone";
            this.lblContactPhone.Location = new System.Drawing.Point(12, 125);

            this.txtContactPhone = new System.Windows.Forms.TextBox();
            this.txtContactPhone.Location = new System.Drawing.Point(12, 145);
            this.txtContactPhone.Size = new System.Drawing.Size(376, 20);

            this.lblRole = new System.Windows.Forms.Label();
            this.lblRole.Text = "Role";
            this.lblRole.Location = new System.Drawing.Point(12, 180);

            this.cmbRole = new System.Windows.Forms.ComboBox();
            this.cmbRole.Location = new System.Drawing.Point(12, 200);
            this.cmbRole.Size = new System.Drawing.Size(376, 21);

            this.chkIsActive = new System.Windows.Forms.CheckBox();
            this.chkIsActive.Text = "Is Active";
            this.chkIsActive.Location = new System.Drawing.Point(12, 235);

            // Save Button
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSave.Location = new System.Drawing.Point(313, 390);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);

            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblContactEmail);
            this.Controls.Add(this.txtContactEmail);
            this.Controls.Add(this.lblContactPhone);
            this.Controls.Add(this.txtContactPhone);
            this.Controls.Add(this.lblRole);
            this.Controls.Add(this.cmbRole);
            this.Controls.Add(this.chkIsActive);
            this.Controls.Add(this.btnSave);
        }

        #endregion

        private System.Windows.Forms.Label lblFullName;
        private System.Windows.Forms.TextBox txtFullName;
        private System.Windows.Forms.Label lblContactEmail;
        private System.Windows.Forms.TextBox txtContactEmail;
        private System.Windows.Forms.Label lblContactPhone;
        private System.Windows.Forms.TextBox txtContactPhone;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.ComboBox cmbRole;
        private System.Windows.Forms.CheckBox chkIsActive;
        private System.Windows.Forms.Button btnSave;
    }
}
