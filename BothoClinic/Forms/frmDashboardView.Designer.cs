namespace BothoClinic.Forms
{
    partial class frmDashboardView
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlScheduleCard = new System.Windows.Forms.Panel();
            this.pnlScheduleHeader = new System.Windows.Forms.Panel();
            this.lblTodaysScheduleTitle = new System.Windows.Forms.Label();
            this.flowStats = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStatPatients = new System.Windows.Forms.Panel();
            this.lblStatPatientsValue = new System.Windows.Forms.Label();
            this.lblStatPatientsTitle = new System.Windows.Forms.Label();
            this.pnlStatCompleted = new System.Windows.Forms.Panel();
            this.lblStatCompletedValue = new System.Windows.Forms.Label();
            this.lblStatCompletedTitle = new System.Windows.Forms.Label();
            this.pnlStatInProgress = new System.Windows.Forms.Panel();
            this.lblStatInProgressValue = new System.Windows.Forms.Label();
            this.lblStatInProgressTitle = new System.Windows.Forms.Label();
            this.pnlStatPending = new System.Windows.Forms.Panel();
            this.lblStatPendingValue = new System.Windows.Forms.Label();
            this.lblStatPendingTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlScheduleCard.SuspendLayout();
            this.pnlScheduleHeader.SuspendLayout();
            this.flowStats.SuspendLayout();
            this.pnlStatPatients.SuspendLayout();
            this.pnlStatCompleted.SuspendLayout();
            this.pnlStatInProgress.SuspendLayout();
            this.pnlStatPending.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlMain.Controls.Add(this.pnlScheduleCard);
            this.pnlMain.Controls.Add(this.flowStats);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(850, 600);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlScheduleCard
            // 
            this.pnlScheduleCard.BackColor = System.Drawing.Color.White;
            this.pnlScheduleCard.Controls.Add(this.pnlScheduleHeader);
            this.pnlScheduleCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlScheduleCard.Location = new System.Drawing.Point(0, 120);
            this.pnlScheduleCard.Margin = new System.Windows.Forms.Padding(10);
            this.pnlScheduleCard.Name = "pnlScheduleCard";
            this.pnlScheduleCard.Padding = new System.Windows.Forms.Padding(20);
            this.pnlScheduleCard.Size = new System.Drawing.Size(850, 440);
            this.pnlScheduleCard.TabIndex = 1;
            // 
            // pnlScheduleHeader
            // 
            this.pnlScheduleHeader.BackColor = System.Drawing.Color.White;
            this.pnlScheduleHeader.Controls.Add(this.lblTodaysScheduleTitle);
            this.pnlScheduleHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlScheduleHeader.Location = new System.Drawing.Point(20, 20);
            this.pnlScheduleHeader.Name = "pnlScheduleHeader";
            this.pnlScheduleHeader.Size = new System.Drawing.Size(810, 60);
            this.pnlScheduleHeader.TabIndex = 0;
            // 
            // lblTodaysScheduleTitle
            // 
            this.lblTodaysScheduleTitle.AutoSize = true;
            this.lblTodaysScheduleTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTodaysScheduleTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTodaysScheduleTitle.Location = new System.Drawing.Point(10, 10);
            this.lblTodaysScheduleTitle.Name = "lblTodaysScheduleTitle";
            this.lblTodaysScheduleTitle.Size = new System.Drawing.Size(184, 30);
            this.lblTodaysScheduleTitle.TabIndex = 0;
            this.lblTodaysScheduleTitle.Text = "Today's Schedule";
            // 
            // flowStats
            // 
            this.flowStats.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.flowStats.Controls.Add(this.pnlStatPatients);
            this.flowStats.Controls.Add(this.pnlStatCompleted);
            this.flowStats.Controls.Add(this.pnlStatInProgress);
            this.flowStats.Controls.Add(this.pnlStatPending);
            this.flowStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowStats.Location = new System.Drawing.Point(0, 0);
            this.flowStats.Name = "flowStats";
            this.flowStats.Padding = new System.Windows.Forms.Padding(10);
            this.flowStats.Size = new System.Drawing.Size(850, 120);
            this.flowStats.TabIndex = 0;
            // 
            // pnlStatPatients
            // 
            this.pnlStatPatients.BackColor = System.Drawing.Color.White;
            this.pnlStatPatients.Controls.Add(this.lblStatPatientsValue);
            this.pnlStatPatients.Controls.Add(this.lblStatPatientsTitle);
            this.pnlStatPatients.Location = new System.Drawing.Point(20, 20);
            this.pnlStatPatients.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStatPatients.Name = "pnlStatPatients";
            this.pnlStatPatients.Size = new System.Drawing.Size(180, 80);
            this.pnlStatPatients.TabIndex = 0;
            // 
            // lblStatPatientsValue
            // 
            this.lblStatPatientsValue.AutoSize = true;
            this.lblStatPatientsValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStatPatientsValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.lblStatPatientsValue.Location = new System.Drawing.Point(15, 35);
            this.lblStatPatientsValue.Name = "lblStatPatientsValue";
            this.lblStatPatientsValue.Size = new System.Drawing.Size(32, 37);
            this.lblStatPatientsValue.TabIndex = 1;
            this.lblStatPatientsValue.Text = "0";
            // 
            // lblStatPatientsTitle
            // 
            this.lblStatPatientsTitle.AutoSize = true;
            this.lblStatPatientsTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatPatientsTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatPatientsTitle.Location = new System.Drawing.Point(15, 10);
            this.lblStatPatientsTitle.Name = "lblStatPatientsTitle";
            this.lblStatPatientsTitle.Size = new System.Drawing.Size(82, 15);
            this.lblStatPatientsTitle.TabIndex = 0;
            this.lblStatPatientsTitle.Text = "Patients Today";
            // 
            // pnlStatCompleted
            // 
            this.pnlStatCompleted.BackColor = System.Drawing.Color.White;
            this.pnlStatCompleted.Controls.Add(this.lblStatCompletedValue);
            this.pnlStatCompleted.Controls.Add(this.lblStatCompletedTitle);
            this.pnlStatCompleted.Location = new System.Drawing.Point(220, 20);
            this.pnlStatCompleted.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStatCompleted.Name = "pnlStatCompleted";
            this.pnlStatCompleted.Size = new System.Drawing.Size(180, 80);
            this.pnlStatCompleted.TabIndex = 1;
            // 
            // lblStatCompletedValue
            // 
            this.lblStatCompletedValue.AutoSize = true;
            this.lblStatCompletedValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStatCompletedValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblStatCompletedValue.Location = new System.Drawing.Point(15, 35);
            this.lblStatCompletedValue.Name = "lblStatCompletedValue";
            this.lblStatCompletedValue.Size = new System.Drawing.Size(32, 37);
            this.lblStatCompletedValue.TabIndex = 1;
            this.lblStatCompletedValue.Text = "0";
            // 
            // lblStatCompletedTitle
            // 
            this.lblStatCompletedTitle.AutoSize = true;
            this.lblStatCompletedTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatCompletedTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatCompletedTitle.Location = new System.Drawing.Point(15, 10);
            this.lblStatCompletedTitle.Name = "lblStatCompletedTitle";
            this.lblStatCompletedTitle.Size = new System.Drawing.Size(64, 15);
            this.lblStatCompletedTitle.TabIndex = 0;
            this.lblStatCompletedTitle.Text = "Completed";
            // 
            // pnlStatInProgress
            // 
            this.pnlStatInProgress.BackColor = System.Drawing.Color.White;
            this.pnlStatInProgress.Controls.Add(this.lblStatInProgressValue);
            this.pnlStatInProgress.Controls.Add(this.lblStatInProgressTitle);
            this.pnlStatInProgress.Location = new System.Drawing.Point(420, 20);
            this.pnlStatInProgress.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStatInProgress.Name = "pnlStatInProgress";
            this.pnlStatInProgress.Size = new System.Drawing.Size(180, 80);
            this.pnlStatInProgress.TabIndex = 2;
            // 
            // lblStatInProgressValue
            // 
            this.lblStatInProgressValue.AutoSize = true;
            this.lblStatInProgressValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStatInProgressValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            this.lblStatInProgressValue.Location = new System.Drawing.Point(15, 35);
            this.lblStatInProgressValue.Name = "lblStatInProgressValue";
            this.lblStatInProgressValue.Size = new System.Drawing.Size(32, 37);
            this.lblStatInProgressValue.TabIndex = 1;
            this.lblStatInProgressValue.Text = "0";
            // 
            // lblStatInProgressTitle
            // 
            this.lblStatInProgressTitle.AutoSize = true;
            this.lblStatInProgressTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatInProgressTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatInProgressTitle.Location = new System.Drawing.Point(15, 10);
            this.lblStatInProgressTitle.Name = "lblStatInProgressTitle";
            this.lblStatInProgressTitle.Size = new System.Drawing.Size(66, 15);
            this.lblStatInProgressTitle.TabIndex = 0;
            this.lblStatInProgressTitle.Text = "In Progress";
            // 
            // pnlStatPending
            // 
            this.pnlStatPending.BackColor = System.Drawing.Color.White;
            this.pnlStatPending.Controls.Add(this.lblStatPendingValue);
            this.pnlStatPending.Controls.Add(this.lblStatPendingTitle);
            this.pnlStatPending.Location = new System.Drawing.Point(620, 20);
            this.pnlStatPending.Margin = new System.Windows.Forms.Padding(10);
            this.pnlStatPending.Name = "pnlStatPending";
            this.pnlStatPending.Size = new System.Drawing.Size(180, 80);
            this.pnlStatPending.TabIndex = 3;
            // 
            // lblStatPendingValue
            // 
            this.lblStatPendingValue.AutoSize = true;
            this.lblStatPendingValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblStatPendingValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.lblStatPendingValue.Location = new System.Drawing.Point(15, 35);
            this.lblStatPendingValue.Name = "lblStatPendingValue";
            this.lblStatPendingValue.Size = new System.Drawing.Size(32, 37);
            this.lblStatPendingValue.TabIndex = 1;
            this.lblStatPendingValue.Text = "0";
            // 
            // lblStatPendingTitle
            // 
            this.lblStatPendingTitle.AutoSize = true;
            this.lblStatPendingTitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatPendingTitle.ForeColor = System.Drawing.Color.Gray;
            this.lblStatPendingTitle.Location = new System.Drawing.Point(15, 10);
            this.lblStatPendingTitle.Name = "lblStatPendingTitle";
            this.lblStatPendingTitle.Size = new System.Drawing.Size(49, 15);
            this.lblStatPendingTitle.TabIndex = 0;
            this.lblStatPendingTitle.Text = "Pending";
            // 
            // frmDashboardView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 600);
            this.Controls.Add(this.pnlMain);
            this.Name = "frmDashboardView";
            this.Text = "Dashboard";
            this.pnlMain.ResumeLayout(false);
            this.pnlScheduleCard.ResumeLayout(false);
            this.pnlScheduleHeader.ResumeLayout(false);
            this.pnlScheduleHeader.PerformLayout();
            this.flowStats.ResumeLayout(false);
            this.pnlStatPatients.ResumeLayout(false);
            this.pnlStatPatients.PerformLayout();
            this.pnlStatCompleted.ResumeLayout(false);
            this.pnlStatCompleted.PerformLayout();
            this.pnlStatInProgress.ResumeLayout(false);
            this.pnlStatInProgress.PerformLayout();
            this.pnlStatPending.ResumeLayout(false);
            this.pnlStatPending.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlScheduleCard;
        private System.Windows.Forms.Panel pnlScheduleHeader;
        private System.Windows.Forms.Label lblTodaysScheduleTitle;
        private System.Windows.Forms.FlowLayoutPanel flowStats;
        private System.Windows.Forms.Panel pnlStatPatients;
        private System.Windows.Forms.Label lblStatPatientsValue;
        private System.Windows.Forms.Label lblStatPatientsTitle;
        private System.Windows.Forms.Panel pnlStatCompleted;
        private System.Windows.Forms.Label lblStatCompletedValue;
        private System.Windows.Forms.Label lblStatCompletedTitle;
        private System.Windows.Forms.Panel pnlStatInProgress;
        private System.Windows.Forms.Label lblStatInProgressValue;
        private System.Windows.Forms.Label lblStatInProgressTitle;
        private System.Windows.Forms.Panel pnlStatPending;
        private System.Windows.Forms.Label lblStatPendingValue;
        private System.Windows.Forms.Label lblStatPendingTitle;
    }
}