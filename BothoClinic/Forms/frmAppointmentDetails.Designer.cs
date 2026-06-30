namespace BothoClinic.Forms
{
    partial class frmAppointmentDetails
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCancelAppointment = new System.Windows.Forms.Button();
            this.btnReschedule = new System.Windows.Forms.Button();
            this.btnCompleteAppointment = new System.Windows.Forms.Button();
            this.btnStartConsultation = new System.Windows.Forms.Button();
            this.btnAssignToMe = new System.Windows.Forms.Button();
            this.btnViewConsultation = new System.Windows.Forms.Button();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.grpPatientInfo = new System.Windows.Forms.GroupBox();
            this.lblBloodType = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lblGender = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblDOB = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblPatientPhone = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblPatientEmail = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lblStudentId = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPatientName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.grpAppointmentInfo = new System.Windows.Forms.GroupBox();
            this.lblClaimedDate = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblCreatedDate = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lblCurrentProvider = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtReason = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.lblTimeSlot = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.lblAppointmentDate = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.lblAppointmentId = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.grpPatientInfo.SuspendLayout();
            this.grpAppointmentInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.pnlContent);
            this.pnlMain.Controls.Add(this.pnlButtons);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(900, 700);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnViewConsultation);
            this.pnlButtons.Controls.Add(this.btnAssignToMe);
            this.pnlButtons.Controls.Add(this.btnStartConsultation);
            this.pnlButtons.Controls.Add(this.btnCompleteAppointment);
            this.pnlButtons.Controls.Add(this.btnReschedule);
            this.pnlButtons.Controls.Add(this.btnCancelAppointment);
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(20, 590);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(860, 90);
            this.pnlButtons.TabIndex = 1;
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(20);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(760, 30);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(80, 35);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCancelAppointment
            // 
            this.btnCancelAppointment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCancelAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelAppointment.ForeColor = System.Drawing.Color.White;
            this.btnCancelAppointment.Location = new System.Drawing.Point(560, 30);
            this.btnCancelAppointment.Name = "btnCancelAppointment";
            this.btnCancelAppointment.Size = new System.Drawing.Size(100, 35);
            this.btnCancelAppointment.TabIndex = 6;
            this.btnCancelAppointment.Text = "Cancel Appt";
            this.btnCancelAppointment.UseVisualStyleBackColor = false;
            this.btnCancelAppointment.Click += new System.EventHandler(this.btnCancelAppointment_Click);
            // 
            // btnReschedule
            // 
            this.btnReschedule.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnReschedule.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReschedule.ForeColor = System.Drawing.Color.Black;
            this.btnReschedule.Location = new System.Drawing.Point(450, 30);
            this.btnReschedule.Name = "btnReschedule";
            this.btnReschedule.Size = new System.Drawing.Size(100, 35);
            this.btnReschedule.TabIndex = 5;
            this.btnReschedule.Text = "Reschedule";
            this.btnReschedule.UseVisualStyleBackColor = false;
            this.btnReschedule.Click += new System.EventHandler(this.btnReschedule_Click);
            // 
            // btnCompleteAppointment
            // 
            this.btnCompleteAppointment.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnCompleteAppointment.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCompleteAppointment.ForeColor = System.Drawing.Color.White;
            this.btnCompleteAppointment.Location = new System.Drawing.Point(340, 30);
            this.btnCompleteAppointment.Name = "btnCompleteAppointment";
            this.btnCompleteAppointment.Size = new System.Drawing.Size(100, 35);
            this.btnCompleteAppointment.TabIndex = 4;
            this.btnCompleteAppointment.Text = "Complete";
            this.btnCompleteAppointment.UseVisualStyleBackColor = false;
            this.btnCompleteAppointment.Click += new System.EventHandler(this.btnCompleteAppointment_Click);
            // 
            // btnStartConsultation
            // 
            this.btnStartConsultation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnStartConsultation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStartConsultation.ForeColor = System.Drawing.Color.White;
            this.btnStartConsultation.Location = new System.Drawing.Point(220, 30);
            this.btnStartConsultation.Name = "btnStartConsultation";
            this.btnStartConsultation.Size = new System.Drawing.Size(110, 35);
            this.btnStartConsultation.TabIndex = 3;
            this.btnStartConsultation.Text = "Start Consult";
            this.btnStartConsultation.UseVisualStyleBackColor = false;
            this.btnStartConsultation.Click += new System.EventHandler(this.btnStartConsultation_Click);
            // 
            // btnAssignToMe
            // 
            this.btnAssignToMe.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnAssignToMe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAssignToMe.ForeColor = System.Drawing.Color.White;
            this.btnAssignToMe.Location = new System.Drawing.Point(110, 30);
            this.btnAssignToMe.Name = "btnAssignToMe";
            this.btnAssignToMe.Size = new System.Drawing.Size(100, 35);
            this.btnAssignToMe.TabIndex = 2;
            this.btnAssignToMe.Text = "Assign to Me";
            this.btnAssignToMe.UseVisualStyleBackColor = false;
            this.btnAssignToMe.Click += new System.EventHandler(this.btnAssignToMe_Click);
            // 
            // btnViewConsultation
            // 
            this.btnViewConsultation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnViewConsultation.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewConsultation.ForeColor = System.Drawing.Color.White;
            this.btnViewConsultation.Location = new System.Drawing.Point(20, 30);
            this.btnViewConsultation.Name = "btnViewConsultation";
            this.btnViewConsultation.Size = new System.Drawing.Size(80, 35);
            this.btnViewConsultation.TabIndex = 1;
            this.btnViewConsultation.Text = "View Notes";
            this.btnViewConsultation.UseVisualStyleBackColor = false;
            this.btnViewConsultation.Click += new System.EventHandler(this.btnViewConsultation_Click);
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.grpPatientInfo);
            this.pnlContent.Controls.Add(this.grpAppointmentInfo);
            this.pnlContent.Controls.Add(this.lblTitle);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(20, 20);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(860, 570);
            this.pnlContent.TabIndex = 0;
            this.pnlContent.BackColor = System.Drawing.Color.White;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Appointment Details";
            // 
            // grpAppointmentInfo
            // 
            this.grpAppointmentInfo.Controls.Add(this.lblClaimedDate);
            this.grpAppointmentInfo.Controls.Add(this.label15);
            this.grpAppointmentInfo.Controls.Add(this.lblCreatedDate);
            this.grpAppointmentInfo.Controls.Add(this.label17);
            this.grpAppointmentInfo.Controls.Add(this.lblCurrentProvider);
            this.grpAppointmentInfo.Controls.Add(this.label19);
            this.grpAppointmentInfo.Controls.Add(this.txtReason);
            this.grpAppointmentInfo.Controls.Add(this.label21);
            this.grpAppointmentInfo.Controls.Add(this.lblStatus);
            this.grpAppointmentInfo.Controls.Add(this.label23);
            this.grpAppointmentInfo.Controls.Add(this.lblTimeSlot);
            this.grpAppointmentInfo.Controls.Add(this.label25);
            this.grpAppointmentInfo.Controls.Add(this.lblAppointmentDate);
            this.grpAppointmentInfo.Controls.Add(this.label27);
            this.grpAppointmentInfo.Controls.Add(this.lblAppointmentId);
            this.grpAppointmentInfo.Controls.Add(this.label29);
            this.grpAppointmentInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpAppointmentInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.grpAppointmentInfo.Location = new System.Drawing.Point(20, 80);
            this.grpAppointmentInfo.Name = "grpAppointmentInfo";
            this.grpAppointmentInfo.Size = new System.Drawing.Size(820, 200);
            this.grpAppointmentInfo.TabIndex = 1;
            this.grpAppointmentInfo.TabStop = false;
            this.grpAppointmentInfo.Text = "Appointment Information";
            // 
            // Appointment Info Labels
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label29.Location = new System.Drawing.Point(20, 35);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(89, 19);
            this.label29.TabIndex = 0;
            this.label29.Text = "Appointment ID:";
            
            this.lblAppointmentId.AutoSize = true;
            this.lblAppointmentId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAppointmentId.Location = new System.Drawing.Point(150, 35);
            this.lblAppointmentId.Name = "lblAppointmentId";
            this.lblAppointmentId.Size = new System.Drawing.Size(16, 19);
            this.lblAppointmentId.TabIndex = 1;
            this.lblAppointmentId.Text = "-";
            
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label27.Location = new System.Drawing.Point(300, 35);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(38, 19);
            this.label27.TabIndex = 2;
            this.label27.Text = "Date:";
            
            this.lblAppointmentDate.AutoSize = true;
            this.lblAppointmentDate.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblAppointmentDate.Location = new System.Drawing.Point(380, 35);
            this.lblAppointmentDate.Name = "lblAppointmentDate";
            this.lblAppointmentDate.Size = new System.Drawing.Size(16, 19);
            this.lblAppointmentDate.TabIndex = 3;
            this.lblAppointmentDate.Text = "-";
            
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label25.Location = new System.Drawing.Point(550, 35);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(38, 19);
            this.label25.TabIndex = 4;
            this.label25.Text = "Time:";
            
            this.lblTimeSlot.AutoSize = true;
            this.lblTimeSlot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTimeSlot.Location = new System.Drawing.Point(620, 35);
            this.lblTimeSlot.Name = "lblTimeSlot";
            this.lblTimeSlot.Size = new System.Drawing.Size(16, 19);
            this.lblTimeSlot.TabIndex = 5;
            this.lblTimeSlot.Text = "-";
            
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label23.Location = new System.Drawing.Point(20, 70);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(47, 19);
            this.label23.TabIndex = 6;
            this.label23.Text = "Status:";
            
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(150, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(16, 19);
            this.lblStatus.TabIndex = 7;
            this.lblStatus.Text = "-";
            
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label21.Location = new System.Drawing.Point(20, 105);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(54, 19);
            this.label21.TabIndex = 8;
            this.label21.Text = "Reason:";
            
            this.txtReason.Location = new System.Drawing.Point(150, 105);
            this.txtReason.Multiline = true;
            this.txtReason.Name = "txtReason";
            this.txtReason.ReadOnly = true;
            this.txtReason.Size = new System.Drawing.Size(650, 50);
            this.txtReason.TabIndex = 9;
            
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label19.Location = new System.Drawing.Point(300, 70);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 19);
            this.label19.TabIndex = 10;
            this.label19.Text = "Provider:";
            
            this.lblCurrentProvider.AutoSize = true;
            this.lblCurrentProvider.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCurrentProvider.Location = new System.Drawing.Point(380, 70);
            this.lblCurrentProvider.Name = "lblCurrentProvider";
            this.lblCurrentProvider.Size = new System.Drawing.Size(16, 19);
            this.lblCurrentProvider.TabIndex = 11;
            this.lblCurrentProvider.Text = "-";
            
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label17.Location = new System.Drawing.Point(20, 170);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(59, 19);
            this.label17.TabIndex = 12;
            this.label17.Text = "Created:";
            
            this.lblCreatedDate.AutoSize = true;
            this.lblCreatedDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCreatedDate.Location = new System.Drawing.Point(150, 170);
            this.lblCreatedDate.Name = "lblCreatedDate";
            this.lblCreatedDate.Size = new System.Drawing.Size(16, 19);
            this.lblCreatedDate.TabIndex = 13;
            this.lblCreatedDate.Text = "-";
            
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label15.Location = new System.Drawing.Point(400, 170);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(60, 19);
            this.label15.TabIndex = 14;
            this.label15.Text = "Claimed:";
            
            this.lblClaimedDate.AutoSize = true;
            this.lblClaimedDate.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblClaimedDate.Location = new System.Drawing.Point(480, 170);
            this.lblClaimedDate.Name = "lblClaimedDate";
            this.lblClaimedDate.Size = new System.Drawing.Size(16, 19);
            this.lblClaimedDate.TabIndex = 15;
            this.lblClaimedDate.Text = "-";
            // 
            // grpPatientInfo
            // 
            this.grpPatientInfo.Controls.Add(this.lblBloodType);
            this.grpPatientInfo.Controls.Add(this.label13);
            this.grpPatientInfo.Controls.Add(this.lblGender);
            this.grpPatientInfo.Controls.Add(this.label11);
            this.grpPatientInfo.Controls.Add(this.lblDOB);
            this.grpPatientInfo.Controls.Add(this.label9);
            this.grpPatientInfo.Controls.Add(this.lblPatientPhone);
            this.grpPatientInfo.Controls.Add(this.label7);
            this.grpPatientInfo.Controls.Add(this.lblPatientEmail);
            this.grpPatientInfo.Controls.Add(this.label5);
            this.grpPatientInfo.Controls.Add(this.lblStudentId);
            this.grpPatientInfo.Controls.Add(this.label3);
            this.grpPatientInfo.Controls.Add(this.lblPatientName);
            this.grpPatientInfo.Controls.Add(this.label1);
            this.grpPatientInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.grpPatientInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.grpPatientInfo.Location = new System.Drawing.Point(20, 300);
            this.grpPatientInfo.Name = "grpPatientInfo";
            this.grpPatientInfo.Size = new System.Drawing.Size(820, 150);
            this.grpPatientInfo.TabIndex = 2;
            this.grpPatientInfo.TabStop = false;
            this.grpPatientInfo.Text = "Patient Information";
            
            // Patient Info Labels
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label1.Location = new System.Drawing.Point(20, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 19);
            this.label1.TabIndex = 0;
            this.label1.Text = "Patient Name:";
            
            this.lblPatientName.AutoSize = true;
            this.lblPatientName.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblPatientName.Location = new System.Drawing.Point(150, 35);
            this.lblPatientName.Name = "lblPatientName";
            this.lblPatientName.Size = new System.Drawing.Size(16, 19);
            this.lblPatientName.TabIndex = 1;
            this.lblPatientName.Text = "-";
            
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label3.Location = new System.Drawing.Point(400, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 19);
            this.label3.TabIndex = 2;
            this.label3.Text = "Student ID:";
            
            this.lblStudentId.AutoSize = true;
            this.lblStudentId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStudentId.Location = new System.Drawing.Point(500, 35);
            this.lblStudentId.Name = "lblStudentId";
            this.lblStudentId.Size = new System.Drawing.Size(16, 19);
            this.lblStudentId.TabIndex = 3;
            this.lblStudentId.Text = "-";
            
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label5.Location = new System.Drawing.Point(20, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(44, 19);
            this.label5.TabIndex = 4;
            this.label5.Text = "Email:";
            
            this.lblPatientEmail.AutoSize = true;
            this.lblPatientEmail.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPatientEmail.Location = new System.Drawing.Point(150, 70);
            this.lblPatientEmail.Name = "lblPatientEmail";
            this.lblPatientEmail.Size = new System.Drawing.Size(16, 19);
            this.lblPatientEmail.TabIndex = 5;
            this.lblPatientEmail.Text = "-";
            
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label7.Location = new System.Drawing.Point(20, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 19);
            this.label7.TabIndex = 6;
            this.label7.Text = "Phone:";
            
            this.lblPatientPhone.AutoSize = true;
            this.lblPatientPhone.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPatientPhone.Location = new System.Drawing.Point(150, 105);
            this.lblPatientPhone.Name = "lblPatientPhone";
            this.lblPatientPhone.Size = new System.Drawing.Size(16, 19);
            this.lblPatientPhone.TabIndex = 7;
            this.lblPatientPhone.Text = "-";
            
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label9.Location = new System.Drawing.Point(400, 70);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 19);
            this.label9.TabIndex = 8;
            this.label9.Text = "DOB:";
            
            this.lblDOB.AutoSize = true;
            this.lblDOB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDOB.Location = new System.Drawing.Point(500, 70);
            this.lblDOB.Name = "lblDOB";
            this.lblDOB.Size = new System.Drawing.Size(16, 19);
            this.lblDOB.TabIndex = 9;
            this.lblDOB.Text = "-";
            
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label11.Location = new System.Drawing.Point(400, 105);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(54, 19);
            this.label11.TabIndex = 10;
            this.label11.Text = "Gender:";
            
            this.lblGender.AutoSize = true;
            this.lblGender.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGender.Location = new System.Drawing.Point(500, 105);
            this.lblGender.Name = "lblGender";
            this.lblGender.Size = new System.Drawing.Size(16, 19);
            this.lblGender.TabIndex = 11;
            this.lblGender.Text = "-";
            
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.label13.Location = new System.Drawing.Point(600, 105);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(78, 19);
            this.label13.TabIndex = 12;
            this.label13.Text = "Blood Type:";
            
            this.lblBloodType.AutoSize = true;
            this.lblBloodType.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBloodType.Location = new System.Drawing.Point(700, 105);
            this.lblBloodType.Name = "lblBloodType";
            this.lblBloodType.Size = new System.Drawing.Size(16, 19);
            this.lblBloodType.TabIndex = 13;
            this.lblBloodType.Text = "-";
            // 
            // frmAppointmentDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAppointmentDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Appointment Details";
            this.pnlMain.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.grpPatientInfo.ResumeLayout(false);
            this.grpPatientInfo.PerformLayout();
            this.grpAppointmentInfo.ResumeLayout(false);
            this.grpAppointmentInfo.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCancelAppointment;
        private System.Windows.Forms.Button btnReschedule;
        private System.Windows.Forms.Button btnCompleteAppointment;
        private System.Windows.Forms.Button btnStartConsultation;
        private System.Windows.Forms.Button btnAssignToMe;
        private System.Windows.Forms.Button btnViewConsultation;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.GroupBox grpPatientInfo;
        private System.Windows.Forms.Label lblBloodType;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label lblGender;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblDOB;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblPatientPhone;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblPatientEmail;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStudentId;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPatientName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox grpAppointmentInfo;
        private System.Windows.Forms.Label lblClaimedDate;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblCreatedDate;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblCurrentProvider;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label lblTimeSlot;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label lblAppointmentDate;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label lblAppointmentId;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label lblTitle;
    }
}