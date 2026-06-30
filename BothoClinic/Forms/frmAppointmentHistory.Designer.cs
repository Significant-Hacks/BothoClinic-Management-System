
namespace BothoClinic
{
    partial class frmAppointmentHistory
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
            this.dgvAppointmentHistory = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentHistory)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAppointmentHistory
            // 
            this.dgvAppointmentHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointmentHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAppointmentHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvAppointmentHistory.Name = "dgvAppointmentHistory";
            this.dgvAppointmentHistory.Size = new System.Drawing.Size(800, 450);
            this.dgvAppointmentHistory.TabIndex = 0;
            // 
            // frmAppointmentHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvAppointmentHistory);
            this.Name = "frmAppointmentHistory";
            this.Text = "Appointment History";
            this.Load += new System.EventHandler(this.frmAppointmentHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointmentHistory)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvAppointmentHistory;
    }
}
