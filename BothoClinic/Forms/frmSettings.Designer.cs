namespace BothoClinic
{
    partial class frmSettings
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
            this.chkEmailNotifications = new System.Windows.Forms.CheckBox();
            this.chkSMSNotifications = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkEmailNotifications
            // 
            this.chkEmailNotifications.AutoSize = true;
            this.chkEmailNotifications.Location = new System.Drawing.Point(12, 12);
            this.chkEmailNotifications.Name = "chkEmailNotifications";
            this.chkEmailNotifications.Size = new System.Drawing.Size(114, 17);
            this.chkEmailNotifications.TabIndex = 0;
            this.chkEmailNotifications.Text = "Email Notifications";
            this.chkEmailNotifications.UseVisualStyleBackColor = true;
            // 
            // chkSMSNotifications
            // 
            this.chkSMSNotifications.AutoSize = true;
            this.chkSMSNotifications.Location = new System.Drawing.Point(12, 35);
            this.chkSMSNotifications.Name = "chkSMSNotifications";
            this.chkSMSNotifications.Size = new System.Drawing.Size(111, 17);
            this.chkSMSNotifications.TabIndex = 1;
            this.chkSMSNotifications.Text = "SMS Notifications";
            this.chkSMSNotifications.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(12, 68);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(260, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save Settings";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 103);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.chkSMSNotifications);
            this.Controls.Add(this.chkEmailNotifications);
            this.Name = "frmSettings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkEmailNotifications;
        private System.Windows.Forms.CheckBox chkSMSNotifications;
        private System.Windows.Forms.Button btnSave;
    }
}
