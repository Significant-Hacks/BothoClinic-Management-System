namespace BothoClinic
{
    partial class frmNotifications
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
            this.dgvNotifications = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotifications)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvNotifications
            // 
            this.dgvNotifications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvNotifications.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvNotifications.Location = new System.Drawing.Point(0, 0);
            this.dgvNotifications.Name = "dgvNotifications";
            this.dgvNotifications.Size = new System.Drawing.Size(800, 450);
            this.dgvNotifications.TabIndex = 0;
            // 
            // frmNotifications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvNotifications);
            this.Name = "frmNotifications";
            this.Text = "Notifications";
            this.Load += new System.EventHandler(this.frmNotifications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvNotifications)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNotifications;
    }
}
