
namespace BothoClinic
{
    partial class frmReporting
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
            this.ClientSize = new System.Drawing.Size(400, 200);
            this.Text = "Clinic Activity Report";

            // Labels and DatePickers
            this.lblStartDate = new System.Windows.Forms.Label();
            this.lblStartDate.Text = "Start Date";
            this.lblStartDate.Location = new System.Drawing.Point(12, 15);

            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.dtpStartDate.Location = new System.Drawing.Point(12, 35);
            this.dtpStartDate.Size = new System.Drawing.Size(170, 20);

            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblEndDate.Text = "End Date";
            this.lblEndDate.Location = new System.Drawing.Point(200, 15);

            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.dtpEndDate.Location = new System.Drawing.Point(200, 35);
            this.dtpEndDate.Size = new System.Drawing.Size(170, 20);

            // Export Button
            this.btnExport = new System.Windows.Forms.Button();
            this.btnExport.Location = new System.Drawing.Point(295, 150);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.Text = "Export to CSV";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);

            this.Controls.Add(this.lblStartDate);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this.lblEndDate);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.btnExport);
        }

        #endregion

        private System.Windows.Forms.Label lblStartDate;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Button btnExport;
    }
}
