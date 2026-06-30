namespace BothoClinic
{
    partial class frmAuditLogs
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
            this.dgvAuditLogs = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLogs)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAuditLogs
            // 
            this.dgvAuditLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAuditLogs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAuditLogs.Location = new System.Drawing.Point(0, 0);
            this.dgvAuditLogs.Name = "dgvAuditLogs";
            this.dgvAuditLogs.Size = new System.Drawing.Size(800, 450);
            this.dgvAuditLogs.TabIndex = 0;
            // 
            // frmAuditLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvAuditLogs);
            this.Name = "frmAuditLogs";
            this.Text = "Audit Logs";
            this.Load += new System.EventHandler(this.frmAuditLogs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAuditLogs)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAuditLogs;
    }
}
