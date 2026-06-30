namespace BothoClinic.Forms
{
    partial class frmRescheduleAppointment
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblTime = new System.Windows.Forms.Label();
            this.dtpTime = new System.Windows.Forms.DateTimePicker();
            this.lblProvider = new System.Windows.Forms.Label();
            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.lblTransfer = new System.Windows.Forms.Label();
            this.cmbTransferTo = new System.Windows.Forms.ComboBox();
            this.chkEmergency = new System.Windows.Forms.CheckBox();
            this.lblNotes = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.btnReschedule = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblTransfer
            // 
            this.lblTransfer.AutoSize = true;
            this.lblTransfer.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTransfer.Location = new System.Drawing.Point(20, 185);
            this.lblTransfer.Name = "lblTransfer";
            this.lblTransfer.Size = new System.Drawing.Size(136, 19);
            this.lblTransfer.TabIndex = 11;
            this.lblTransfer.Text = "Transfer To Provider:";
            // 
            // cmbTransferTo
            // 
            this.cmbTransferTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTransferTo.Location = new System.Drawing.Point(160, 185);
            this.cmbTransferTo.Name = "cmbTransferTo";
            this.cmbTransferTo.Size = new System.Drawing.Size(160, 23);
            this.cmbTransferTo.TabIndex = 12;
            // 
            // chkEmergency
            // 
            this.chkEmergency.AutoSize = true;
            this.chkEmergency.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.chkEmergency.Location = new System.Drawing.Point(20, 215);
            this.chkEmergency.Name = "chkEmergency";
            this.chkEmergency.Size = new System.Drawing.Size(155, 19);
            this.chkEmergency.TabIndex = 13;
            this.chkEmergency.Text = "Treat as emergency visit";
            this.chkEmergency.UseVisualStyleBackColor = true;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reschedule Appointment";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDate.Location = new System.Drawing.Point(20, 70);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(69, 19);
            this.lblDate.TabIndex = 1;
            this.lblDate.Text = "New Date:";
            // 
            // dtpDate
            // 
            this.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpDate.Location = new System.Drawing.Point(120, 70);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(200, 23);
            this.dtpDate.TabIndex = 2;
            // 
            // lblTime
            // 
            this.lblTime.AutoSize = true;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTime.Location = new System.Drawing.Point(20, 110);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(70, 19);
            this.lblTime.TabIndex = 3;
            this.lblTime.Text = "New Time:";
            // 
            // dtpTime
            // 
            this.dtpTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.dtpTime.Location = new System.Drawing.Point(120, 110);
            this.dtpTime.Name = "dtpTime";
            this.dtpTime.ShowUpDown = true;
            this.dtpTime.Size = new System.Drawing.Size(200, 23);
            this.dtpTime.TabIndex = 4;
            // 
            // lblProvider
            // 
            this.lblProvider.AutoSize = true;
            this.lblProvider.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblProvider.Location = new System.Drawing.Point(20, 150);
            this.lblProvider.Name = "lblProvider";
            this.lblProvider.Size = new System.Drawing.Size(65, 19);
            this.lblProvider.TabIndex = 5;
            this.lblProvider.Text = "Provider:";
            // 
            // cmbProvider
            // 
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProvider.Location = new System.Drawing.Point(120, 150);
            this.cmbProvider.Name = "cmbProvider";
            this.cmbProvider.Size = new System.Drawing.Size(200, 23);
            this.cmbProvider.TabIndex = 6;
            // 
            // lblNotes
            // 
            this.lblNotes.AutoSize = true;
            this.lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNotes.Location = new System.Drawing.Point(20, 245);
            this.lblNotes.Name = "lblNotes";
            this.lblNotes.Size = new System.Drawing.Size(46, 19);
            this.lblNotes.TabIndex = 7;
            this.lblNotes.Text = "Notes:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(20, 270);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.PlaceholderText = "Optional notes about the reschedule or transfer...";
            this.txtNotes.Size = new System.Drawing.Size(300, 60);
            this.txtNotes.TabIndex = 8;
            // 
            // btnReschedule
            // 
            this.btnReschedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnReschedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReschedule.ForeColor = System.Drawing.Color.White;
            this.btnReschedule.Location = new System.Drawing.Point(120, 350);
            this.btnReschedule.Name = "btnReschedule";
            this.btnReschedule.Size = new System.Drawing.Size(100, 35);
            this.btnReschedule.TabIndex = 9;
            this.btnReschedule.Text = "Reschedule";
            this.btnReschedule.UseVisualStyleBackColor = false;
            this.btnReschedule.Click += new System.EventHandler(this.btnReschedule_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(230, 350);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(80, 35);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmRescheduleAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(350, 420);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReschedule);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.lblNotes);
            this.Controls.Add(this.chkEmergency);
            this.Controls.Add(this.cmbTransferTo);
            this.Controls.Add(this.lblTransfer);
            this.Controls.Add(this.cmbProvider);
            this.Controls.Add(this.lblProvider);
            this.Controls.Add(this.dtpTime);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRescheduleAppointment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reschedule Appointment";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.DateTimePicker dtpTime;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Label lblTransfer;
        private System.Windows.Forms.ComboBox cmbTransferTo;
        private System.Windows.Forms.CheckBox chkEmergency;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnReschedule;
        private System.Windows.Forms.Button btnCancel;
    }
}