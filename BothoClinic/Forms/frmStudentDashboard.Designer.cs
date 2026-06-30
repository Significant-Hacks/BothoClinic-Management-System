namespace BothoClinic
{
    partial class frmStudentDashboard
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
            pnlSidebar = new Panel();
            btnNavLogout = new Button();
            btnNavSettings = new Button();
            btnNavNotifications = new Button();
            btnNavProfile = new Button();
            btnNavAppointmentHistory = new Button();
            btnNavBookAppointment = new Button();
            btnNavDashboard = new Button();
            pnlSidebarHeader = new Panel();
            lblStudentRole = new Label();
            lblStudentName = new Label();
            lblAvatar = new Label();
            pnlMainContent = new Panel();
            tabControl = new TabControl();
            tpDashboard = new TabPage();
            pnlHealthReminders = new Panel();
            lblRemindersTitle = new Label();
            lbReminders = new ListBox();
            pnlNextAppointment = new Panel();
            lblAppointmentTitle = new Label();
            lblNextApptStatusValue = new Label();
            lblNextApptStatus = new Label();
            lblNextApptReasonValue = new Label();
            lblNextApptReason = new Label();
            lblNextApptProviderValue = new Label();
            lblNextApptProvider = new Label();
            lblNextApptTimeValue = new Label();
            lblNextApptTime = new Label();
            lblNextApptDateValue = new Label();
            lblNextApptDate = new Label();
            tpBookAppointment = new TabPage();
            lblBookingTitle = new Label();
            btnCancelBooking = new Button();
            btnBookAppointment = new Button();
            lblReasonLabel = new Label();
            txtAppointmentReason = new TextBox();
            lblTimeLabel = new Label();
            cmbTimeSlot = new ComboBox();
            lblDateLabel = new Label();
            dtpAppointmentDate = new DateTimePicker();
            tpAppointmentHistory = new TabPage();
            btnRefreshHistory = new Button();
            dgvAppointmentHistory = new DataGridView();
            tpProfile = new TabPage();
            pnlProfileForm = new Panel();
            btnChangePassword = new Button();
            btnSaveProfile = new Button();
            btnEditProfile = new Button();
            lblBloodTypeLabel = new Label();
            txtBloodType = new TextBox();
            lblGenderLabel = new Label();
            txtGender = new TextBox();
            lblDOBLabel = new Label();
            txtDOB = new TextBox();
            lblStudentIdLabel = new Label();
            txtStudentId = new TextBox();
            lblPhoneLabel = new Label();
            txtProfilePhone = new TextBox();
            lblEmailLabel = new Label();
            txtProfileEmail = new TextBox();
            lblFullNameLabel = new Label();
            txtProfileFullName = new TextBox();
            tpNotifications = new TabPage();
            btnRefreshNotifications = new Button();
            btnMarkAsRead = new Button();
            dgvNotifications = new DataGridView();
            tpSettings = new TabPage();
            lblSettingsTitle = new Label();
            btnSaveSettings = new Button();
            chkAppNotifications = new CheckBox();
            chkSMSNotifications = new CheckBox();
            chkEmailNotifications = new CheckBox();
            pnlSidebar.SuspendLayout();
            pnlSidebarHeader.SuspendLayout();
            pnlMainContent.SuspendLayout();
            tabControl.SuspendLayout();
            tpDashboard.SuspendLayout();
            pnlHealthReminders.SuspendLayout();
            pnlNextAppointment.SuspendLayout();
            tpBookAppointment.SuspendLayout();
            tpAppointmentHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAppointmentHistory).BeginInit();
            tpProfile.SuspendLayout();
            pnlProfileForm.SuspendLayout();
            tpNotifications.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvNotifications).BeginInit();
            tpSettings.SuspendLayout();
            SuspendLayout();
            // 
            // pnlSidebar
            // 
            pnlSidebar.BackColor = Color.FromArgb(44, 62, 80);
            pnlSidebar.Controls.Add(btnNavLogout);
            pnlSidebar.Controls.Add(btnNavSettings);
            pnlSidebar.Controls.Add(btnNavNotifications);
            pnlSidebar.Controls.Add(btnNavProfile);
            pnlSidebar.Controls.Add(btnNavAppointmentHistory);
            pnlSidebar.Controls.Add(btnNavBookAppointment);
            pnlSidebar.Controls.Add(btnNavDashboard);
            pnlSidebar.Controls.Add(pnlSidebarHeader);
            pnlSidebar.Dock = DockStyle.Left;
            pnlSidebar.Location = new Point(0, 0);
            pnlSidebar.Margin = new Padding(6, 6, 6, 6);
            pnlSidebar.Name = "pnlSidebar";
            pnlSidebar.Size = new Size(406, 1579);
            pnlSidebar.TabIndex = 0;
            // 
            // btnNavLogout
            // 
            btnNavLogout.Dock = DockStyle.Bottom;
            btnNavLogout.FlatAppearance.BorderSize = 0;
            btnNavLogout.FlatStyle = FlatStyle.Flat;
            btnNavLogout.ForeColor = Color.White;
            btnNavLogout.Location = new Point(0, 1472);
            btnNavLogout.Margin = new Padding(6, 6, 6, 6);
            btnNavLogout.Name = "btnNavLogout";
            btnNavLogout.Padding = new Padding(48, 0, 0, 0);
            btnNavLogout.Size = new Size(406, 107);
            btnNavLogout.TabIndex = 6;
            btnNavLogout.Text = "🚪 Logout";
            btnNavLogout.TextAlign = ContentAlignment.MiddleLeft;
            btnNavLogout.UseVisualStyleBackColor = false;
            // 
            // btnNavSettings
            // 
            btnNavSettings.Dock = DockStyle.Top;
            btnNavSettings.FlatAppearance.BorderSize = 0;
            btnNavSettings.FlatStyle = FlatStyle.Flat;
            btnNavSettings.ForeColor = Color.White;
            btnNavSettings.Location = new Point(0, 750);
            btnNavSettings.Margin = new Padding(6, 6, 6, 6);
            btnNavSettings.Name = "btnNavSettings";
            btnNavSettings.Padding = new Padding(48, 0, 0, 0);
            btnNavSettings.Size = new Size(406, 91);
            btnNavSettings.TabIndex = 5;
            btnNavSettings.Text = "⚙️ Settings";
            btnNavSettings.TextAlign = ContentAlignment.MiddleLeft;
            btnNavSettings.UseVisualStyleBackColor = false;
            // 
            // btnNavNotifications
            // 
            btnNavNotifications.Dock = DockStyle.Top;
            btnNavNotifications.FlatAppearance.BorderSize = 0;
            btnNavNotifications.FlatStyle = FlatStyle.Flat;
            btnNavNotifications.ForeColor = Color.White;
            btnNavNotifications.Location = new Point(0, 663);
            btnNavNotifications.Margin = new Padding(6, 6, 6, 6);
            btnNavNotifications.Name = "btnNavNotifications";
            btnNavNotifications.Padding = new Padding(48, 0, 0, 0);
            btnNavNotifications.Size = new Size(406, 87);
            btnNavNotifications.TabIndex = 4;
            btnNavNotifications.Text = "🔔 Alerts";
            btnNavNotifications.TextAlign = ContentAlignment.MiddleLeft;
            btnNavNotifications.UseVisualStyleBackColor = false;
            // 
            // btnNavProfile
            // 
            btnNavProfile.Dock = DockStyle.Top;
            btnNavProfile.FlatAppearance.BorderSize = 0;
            btnNavProfile.FlatStyle = FlatStyle.Flat;
            btnNavProfile.ForeColor = Color.White;
            btnNavProfile.Location = new Point(0, 575);
            btnNavProfile.Margin = new Padding(6, 6, 6, 6);
            btnNavProfile.Name = "btnNavProfile";
            btnNavProfile.Padding = new Padding(48, 0, 0, 0);
            btnNavProfile.Size = new Size(406, 88);
            btnNavProfile.TabIndex = 3;
            btnNavProfile.Text = "👤 Profile";
            btnNavProfile.TextAlign = ContentAlignment.MiddleLeft;
            btnNavProfile.UseVisualStyleBackColor = false;
            // 
            // btnNavAppointmentHistory
            // 
            btnNavAppointmentHistory.Dock = DockStyle.Top;
            btnNavAppointmentHistory.FlatAppearance.BorderSize = 0;
            btnNavAppointmentHistory.FlatStyle = FlatStyle.Flat;
            btnNavAppointmentHistory.ForeColor = Color.White;
            btnNavAppointmentHistory.Location = new Point(0, 484);
            btnNavAppointmentHistory.Margin = new Padding(6, 6, 6, 6);
            btnNavAppointmentHistory.Name = "btnNavAppointmentHistory";
            btnNavAppointmentHistory.Padding = new Padding(48, 0, 0, 0);
            btnNavAppointmentHistory.Size = new Size(406, 91);
            btnNavAppointmentHistory.TabIndex = 2;
            btnNavAppointmentHistory.Text = "📋 History";
            btnNavAppointmentHistory.TextAlign = ContentAlignment.MiddleLeft;
            btnNavAppointmentHistory.UseVisualStyleBackColor = false;
            // 
            // btnNavBookAppointment
            // 
            btnNavBookAppointment.Dock = DockStyle.Top;
            btnNavBookAppointment.FlatAppearance.BorderSize = 0;
            btnNavBookAppointment.FlatStyle = FlatStyle.Flat;
            btnNavBookAppointment.ForeColor = Color.White;
            btnNavBookAppointment.Location = new Point(0, 396);
            btnNavBookAppointment.Margin = new Padding(6, 6, 6, 6);
            btnNavBookAppointment.Name = "btnNavBookAppointment";
            btnNavBookAppointment.Padding = new Padding(48, 0, 0, 0);
            btnNavBookAppointment.Size = new Size(406, 88);
            btnNavBookAppointment.TabIndex = 1;
            btnNavBookAppointment.Text = "📅 Book";
            btnNavBookAppointment.TextAlign = ContentAlignment.MiddleLeft;
            btnNavBookAppointment.UseVisualStyleBackColor = false;
            // 
            // btnNavDashboard
            // 
            btnNavDashboard.Dock = DockStyle.Top;
            btnNavDashboard.FlatAppearance.BorderSize = 0;
            btnNavDashboard.FlatStyle = FlatStyle.Flat;
            btnNavDashboard.ForeColor = Color.White;
            btnNavDashboard.Location = new Point(0, 299);
            btnNavDashboard.Margin = new Padding(6, 6, 6, 6);
            btnNavDashboard.Name = "btnNavDashboard";
            btnNavDashboard.Padding = new Padding(48, 0, 0, 0);
            btnNavDashboard.Size = new Size(406, 97);
            btnNavDashboard.TabIndex = 0;
            btnNavDashboard.Text = "📊 Dashboard";
            btnNavDashboard.TextAlign = ContentAlignment.MiddleLeft;
            btnNavDashboard.UseVisualStyleBackColor = false;
            // 
            // pnlSidebarHeader
            // 
            pnlSidebarHeader.BackColor = Color.FromArgb(44, 62, 80);
            pnlSidebarHeader.Controls.Add(lblStudentRole);
            pnlSidebarHeader.Controls.Add(lblStudentName);
            pnlSidebarHeader.Controls.Add(lblAvatar);
            pnlSidebarHeader.Dock = DockStyle.Top;
            pnlSidebarHeader.Location = new Point(0, 0);
            pnlSidebarHeader.Margin = new Padding(6, 6, 6, 6);
            pnlSidebarHeader.Name = "pnlSidebarHeader";
            pnlSidebarHeader.Padding = new Padding(28, 43, 28, 43);
            pnlSidebarHeader.Size = new Size(406, 299);
            pnlSidebarHeader.TabIndex = 8;
            // 
            // lblStudentRole
            // 
            lblStudentRole.AutoSize = true;
            lblStudentRole.Font = new Font("Segoe UI", 9F);
            lblStudentRole.ForeColor = Color.Gainsboro;
            lblStudentRole.Location = new Point(155, 160);
            lblStudentRole.Margin = new Padding(6, 0, 6, 0);
            lblStudentRole.Name = "lblStudentRole";
            lblStudentRole.Size = new Size(97, 32);
            lblStudentRole.TabIndex = 0;
            lblStudentRole.Text = "Student";
            // 
            // lblStudentName
            // 
            lblStudentName.AutoSize = true;
            lblStudentName.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStudentName.ForeColor = Color.White;
            lblStudentName.Location = new Point(155, 123);
            lblStudentName.Margin = new Padding(6, 0, 6, 0);
            lblStudentName.Name = "lblStudentName";
            lblStudentName.Size = new Size(200, 37);
            lblStudentName.TabIndex = 1;
            lblStudentName.Text = "Student Name";
            // 
            // lblAvatar
            // 
            lblAvatar.BackColor = Color.FromArgb(231, 76, 60);
            lblAvatar.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAvatar.ForeColor = Color.White;
            lblAvatar.Location = new Point(41, 111);
            lblAvatar.Margin = new Padding(6, 0, 6, 0);
            lblAvatar.Name = "lblAvatar";
            lblAvatar.Size = new Size(88, 93);
            lblAvatar.TabIndex = 2;
            lblAvatar.Text = "S";
            lblAvatar.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pnlMainContent
            // 
            pnlMainContent.Controls.Add(tabControl);
            pnlMainContent.Dock = DockStyle.Fill;
            pnlMainContent.Location = new Point(406, 0);
            pnlMainContent.Margin = new Padding(6, 6, 6, 6);
            pnlMainContent.Name = "pnlMainContent";
            pnlMainContent.Size = new Size(2478, 1579);
            pnlMainContent.TabIndex = 1;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tpDashboard);
            tabControl.Controls.Add(tpBookAppointment);
            tabControl.Controls.Add(tpAppointmentHistory);
            tabControl.Controls.Add(tpProfile);
            tabControl.Controls.Add(tpNotifications);
            tabControl.Controls.Add(tpSettings);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(0, 0);
            tabControl.Margin = new Padding(6, 6, 6, 6);
            tabControl.Name = "tabControl";
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(2478, 1579);
            tabControl.TabIndex = 0;
            // 
            // tpDashboard
            // 
            tpDashboard.Controls.Add(pnlHealthReminders);
            tpDashboard.Controls.Add(pnlNextAppointment);
            tpDashboard.Location = new Point(8, 46);
            tpDashboard.Margin = new Padding(6, 6, 6, 6);
            tpDashboard.Name = "tpDashboard";
            tpDashboard.Padding = new Padding(6, 6, 6, 6);
            tpDashboard.Size = new Size(2095, 1525);
            tpDashboard.TabIndex = 0;
            tpDashboard.Text = "Dashboard";
            tpDashboard.UseVisualStyleBackColor = true;
            // 
            // pnlHealthReminders
            // 
            pnlHealthReminders.BorderStyle = BorderStyle.FixedSingle;
            pnlHealthReminders.Controls.Add(lblRemindersTitle);
            pnlHealthReminders.Controls.Add(lbReminders);
            pnlHealthReminders.Location = new Point(780, 21);
            pnlHealthReminders.Margin = new Padding(6, 6, 6, 6);
            pnlHealthReminders.Name = "pnlHealthReminders";
            pnlHealthReminders.Size = new Size(741, 531);
            pnlHealthReminders.TabIndex = 1;
            // 
            // lblRemindersTitle
            // 
            lblRemindersTitle.AutoSize = true;
            lblRemindersTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblRemindersTitle.Location = new Point(19, 21);
            lblRemindersTitle.Margin = new Padding(6, 0, 6, 0);
            lblRemindersTitle.Name = "lblRemindersTitle";
            lblRemindersTitle.Size = new Size(340, 45);
            lblRemindersTitle.TabIndex = 0;
            lblRemindersTitle.Text = "🔔 Health Reminders";
            // 
            // lbReminders
            // 
            lbReminders.FormattingEnabled = true;
            lbReminders.Location = new Point(19, 75);
            lbReminders.Margin = new Padding(6, 6, 6, 6);
            lbReminders.Name = "lbReminders";
            lbReminders.Size = new Size(699, 420);
            lbReminders.TabIndex = 0;
            // 
            // pnlNextAppointment
            // 
            pnlNextAppointment.BorderStyle = BorderStyle.FixedSingle;
            pnlNextAppointment.Controls.Add(lblAppointmentTitle);
            pnlNextAppointment.Controls.Add(lblNextApptStatusValue);
            pnlNextAppointment.Controls.Add(lblNextApptStatus);
            pnlNextAppointment.Controls.Add(lblNextApptReasonValue);
            pnlNextAppointment.Controls.Add(lblNextApptReason);
            pnlNextAppointment.Controls.Add(lblNextApptProviderValue);
            pnlNextAppointment.Controls.Add(lblNextApptProvider);
            pnlNextAppointment.Controls.Add(lblNextApptTimeValue);
            pnlNextAppointment.Controls.Add(lblNextApptTime);
            pnlNextAppointment.Controls.Add(lblNextApptDateValue);
            pnlNextAppointment.Controls.Add(lblNextApptDate);
            pnlNextAppointment.Location = new Point(19, 21);
            pnlNextAppointment.Margin = new Padding(6, 6, 6, 6);
            pnlNextAppointment.Name = "pnlNextAppointment";
            pnlNextAppointment.Size = new Size(741, 531);
            pnlNextAppointment.TabIndex = 0;
            // 
            // lblAppointmentTitle
            // 
            lblAppointmentTitle.AutoSize = true;
            lblAppointmentTitle.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblAppointmentTitle.Location = new Point(19, 21);
            lblAppointmentTitle.Margin = new Padding(6, 0, 6, 0);
            lblAppointmentTitle.Name = "lblAppointmentTitle";
            lblAppointmentTitle.Size = new Size(355, 45);
            lblAppointmentTitle.TabIndex = 0;
            lblAppointmentTitle.Text = "📅 Next Appointment";
            // 
            // lblNextApptStatusValue
            // 
            lblNextApptStatusValue.AutoSize = true;
            lblNextApptStatusValue.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNextApptStatusValue.Location = new Point(279, 341);
            lblNextApptStatusValue.Margin = new Padding(6, 0, 6, 0);
            lblNextApptStatusValue.Name = "lblNextApptStatusValue";
            lblNextApptStatusValue.Size = new Size(69, 37);
            lblNextApptStatusValue.TabIndex = 9;
            lblNextApptStatusValue.Text = "N/A";
            // 
            // lblNextApptStatus
            // 
            lblNextApptStatus.AutoSize = true;
            lblNextApptStatus.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNextApptStatus.Location = new Point(19, 341);
            lblNextApptStatus.Margin = new Padding(6, 0, 6, 0);
            lblNextApptStatus.Name = "lblNextApptStatus";
            lblNextApptStatus.Size = new Size(103, 37);
            lblNextApptStatus.TabIndex = 8;
            lblNextApptStatus.Text = "Status:";
            // 
            // lblNextApptReasonValue
            // 
            lblNextApptReasonValue.AutoSize = true;
            lblNextApptReasonValue.Location = new Point(279, 277);
            lblNextApptReasonValue.Margin = new Padding(6, 0, 6, 0);
            lblNextApptReasonValue.Name = "lblNextApptReasonValue";
            lblNextApptReasonValue.Size = new Size(56, 32);
            lblNextApptReasonValue.TabIndex = 7;
            lblNextApptReasonValue.Text = "N/A";
            // 
            // lblNextApptReason
            // 
            lblNextApptReason.AutoSize = true;
            lblNextApptReason.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNextApptReason.Location = new Point(19, 277);
            lblNextApptReason.Margin = new Padding(6, 0, 6, 0);
            lblNextApptReason.Name = "lblNextApptReason";
            lblNextApptReason.Size = new Size(116, 37);
            lblNextApptReason.TabIndex = 6;
            lblNextApptReason.Text = "Reason:";
            // 
            // lblNextApptProviderValue
            // 
            lblNextApptProviderValue.AutoSize = true;
            lblNextApptProviderValue.Location = new Point(279, 213);
            lblNextApptProviderValue.Margin = new Padding(6, 0, 6, 0);
            lblNextApptProviderValue.Name = "lblNextApptProviderValue";
            lblNextApptProviderValue.Size = new Size(56, 32);
            lblNextApptProviderValue.TabIndex = 5;
            lblNextApptProviderValue.Text = "N/A";
            // 
            // lblNextApptProvider
            // 
            lblNextApptProvider.AutoSize = true;
            lblNextApptProvider.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNextApptProvider.Location = new Point(19, 213);
            lblNextApptProvider.Margin = new Padding(6, 0, 6, 0);
            lblNextApptProvider.Name = "lblNextApptProvider";
            lblNextApptProvider.Size = new Size(136, 37);
            lblNextApptProvider.TabIndex = 4;
            lblNextApptProvider.Text = "Provider:";
            // 
            // lblNextApptTimeValue
            // 
            lblNextApptTimeValue.AutoSize = true;
            lblNextApptTimeValue.Location = new Point(279, 149);
            lblNextApptTimeValue.Margin = new Padding(6, 0, 6, 0);
            lblNextApptTimeValue.Name = "lblNextApptTimeValue";
            lblNextApptTimeValue.Size = new Size(56, 32);
            lblNextApptTimeValue.TabIndex = 3;
            lblNextApptTimeValue.Text = "N/A";
            // 
            // lblNextApptTime
            // 
            lblNextApptTime.AutoSize = true;
            lblNextApptTime.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNextApptTime.Location = new Point(19, 149);
            lblNextApptTime.Margin = new Padding(6, 0, 6, 0);
            lblNextApptTime.Name = "lblNextApptTime";
            lblNextApptTime.Size = new Size(88, 37);
            lblNextApptTime.TabIndex = 2;
            lblNextApptTime.Text = "Time:";
            // 
            // lblNextApptDateValue
            // 
            lblNextApptDateValue.AutoSize = true;
            lblNextApptDateValue.Location = new Point(279, 85);
            lblNextApptDateValue.Margin = new Padding(6, 0, 6, 0);
            lblNextApptDateValue.Name = "lblNextApptDateValue";
            lblNextApptDateValue.Size = new Size(56, 32);
            lblNextApptDateValue.TabIndex = 1;
            lblNextApptDateValue.Text = "N/A";
            // 
            // lblNextApptDate
            // 
            lblNextApptDate.AutoSize = true;
            lblNextApptDate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblNextApptDate.Location = new Point(19, 85);
            lblNextApptDate.Margin = new Padding(6, 0, 6, 0);
            lblNextApptDate.Name = "lblNextApptDate";
            lblNextApptDate.Size = new Size(85, 37);
            lblNextApptDate.TabIndex = 0;
            lblNextApptDate.Text = "Date:";
            // 
            // tpBookAppointment
            // 
            tpBookAppointment.Controls.Add(lblBookingTitle);
            tpBookAppointment.Controls.Add(btnCancelBooking);
            tpBookAppointment.Controls.Add(btnBookAppointment);
            tpBookAppointment.Controls.Add(lblReasonLabel);
            tpBookAppointment.Controls.Add(txtAppointmentReason);
            tpBookAppointment.Controls.Add(lblTimeLabel);
            tpBookAppointment.Controls.Add(cmbTimeSlot);
            tpBookAppointment.Controls.Add(lblDateLabel);
            tpBookAppointment.Controls.Add(dtpAppointmentDate);
            tpBookAppointment.Location = new Point(8, 46);
            tpBookAppointment.Margin = new Padding(6, 6, 6, 6);
            tpBookAppointment.Name = "tpBookAppointment";
            tpBookAppointment.Padding = new Padding(6, 6, 6, 6);
            tpBookAppointment.Size = new Size(2462, 1525);
            tpBookAppointment.TabIndex = 1;
            tpBookAppointment.Text = "Book Appointment";
            tpBookAppointment.UseVisualStyleBackColor = true;
            // 
            // lblBookingTitle
            // 
            lblBookingTitle.AutoSize = true;
            lblBookingTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblBookingTitle.Location = new Point(37, 43);
            lblBookingTitle.Margin = new Padding(6, 0, 6, 0);
            lblBookingTitle.Name = "lblBookingTitle";
            lblBookingTitle.Size = new Size(518, 51);
            lblBookingTitle.TabIndex = 0;
            lblBookingTitle.Text = "📅 Book New Appointment";
            // 
            // btnCancelBooking
            // 
            btnCancelBooking.BackColor = Color.Gray;
            btnCancelBooking.ForeColor = Color.White;
            btnCancelBooking.Location = new Point(279, 725);
            btnCancelBooking.Margin = new Padding(6, 6, 6, 6);
            btnCancelBooking.Name = "btnCancelBooking";
            btnCancelBooking.Size = new Size(223, 85);
            btnCancelBooking.TabIndex = 4;
            btnCancelBooking.Text = "✕ Clear Form";
            btnCancelBooking.UseVisualStyleBackColor = false;
            // 
            // btnBookAppointment
            // 
            btnBookAppointment.BackColor = Color.Green;
            btnBookAppointment.ForeColor = Color.White;
            btnBookAppointment.Location = new Point(37, 725);
            btnBookAppointment.Margin = new Padding(6, 6, 6, 6);
            btnBookAppointment.Name = "btnBookAppointment";
            btnBookAppointment.Size = new Size(223, 85);
            btnBookAppointment.TabIndex = 3;
            btnBookAppointment.Text = "✓ Book Appointment";
            btnBookAppointment.UseVisualStyleBackColor = false;
            // 
            // lblReasonLabel
            // 
            lblReasonLabel.AutoSize = true;
            lblReasonLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblReasonLabel.Location = new Point(37, 384);
            lblReasonLabel.Margin = new Padding(6, 0, 6, 0);
            lblReasonLabel.Name = "lblReasonLabel";
            lblReasonLabel.Size = new Size(225, 37);
            lblReasonLabel.TabIndex = 2;
            lblReasonLabel.Text = "Reason for Visit:";
            // 
            // txtAppointmentReason
            // 
            txtAppointmentReason.Location = new Point(37, 437);
            txtAppointmentReason.Margin = new Padding(6, 6, 6, 6);
            txtAppointmentReason.Multiline = true;
            txtAppointmentReason.Name = "txtAppointmentReason";
            txtAppointmentReason.Size = new Size(925, 251);
            txtAppointmentReason.TabIndex = 2;
            // 
            // lblTimeLabel
            // 
            lblTimeLabel.AutoSize = true;
            lblTimeLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTimeLabel.Location = new Point(37, 256);
            lblTimeLabel.Margin = new Padding(6, 0, 6, 0);
            lblTimeLabel.Name = "lblTimeLabel";
            lblTimeLabel.Size = new Size(172, 37);
            lblTimeLabel.TabIndex = 1;
            lblTimeLabel.Text = "Select Time:";
            // 
            // cmbTimeSlot
            // 
            cmbTimeSlot.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbTimeSlot.FormattingEnabled = true;
            cmbTimeSlot.Location = new Point(37, 309);
            cmbTimeSlot.Margin = new Padding(6, 6, 6, 6);
            cmbTimeSlot.Name = "cmbTimeSlot";
            cmbTimeSlot.Size = new Size(554, 40);
            cmbTimeSlot.TabIndex = 1;
            // 
            // lblDateLabel
            // 
            lblDateLabel.AutoSize = true;
            lblDateLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDateLabel.Location = new Point(37, 128);
            lblDateLabel.Margin = new Padding(6, 0, 6, 0);
            lblDateLabel.Name = "lblDateLabel";
            lblDateLabel.Size = new Size(169, 37);
            lblDateLabel.TabIndex = 0;
            lblDateLabel.Text = "Select Date:";
            // 
            // dtpAppointmentDate
            // 
            dtpAppointmentDate.Location = new Point(37, 181);
            dtpAppointmentDate.Margin = new Padding(6, 6, 6, 6);
            dtpAppointmentDate.Name = "dtpAppointmentDate";
            dtpAppointmentDate.Size = new Size(554, 39);
            dtpAppointmentDate.TabIndex = 0;
            dtpAppointmentDate.Value = new DateTime(2025, 10, 29, 22, 13, 39, 543);
            // 
            // tpAppointmentHistory
            // 
            tpAppointmentHistory.Controls.Add(btnRefreshHistory);
            tpAppointmentHistory.Controls.Add(dgvAppointmentHistory);
            tpAppointmentHistory.Location = new Point(8, 46);
            tpAppointmentHistory.Margin = new Padding(6, 6, 6, 6);
            tpAppointmentHistory.Name = "tpAppointmentHistory";
            tpAppointmentHistory.Padding = new Padding(6, 6, 6, 6);
            tpAppointmentHistory.Size = new Size(2517, 2999);
            tpAppointmentHistory.TabIndex = 2;
            tpAppointmentHistory.Text = "Appointment History";
            tpAppointmentHistory.UseVisualStyleBackColor = true;
            // 
            // btnRefreshHistory
            // 
            btnRefreshHistory.BackColor = Color.Blue;
            btnRefreshHistory.ForeColor = Color.White;
            btnRefreshHistory.Location = new Point(650, 1323);
            btnRefreshHistory.Margin = new Padding(6, 6, 6, 6);
            btnRefreshHistory.Name = "btnRefreshHistory";
            btnRefreshHistory.Size = new Size(223, 85);
            btnRefreshHistory.TabIndex = 1;
            btnRefreshHistory.Text = "🔄 Refresh";
            btnRefreshHistory.UseVisualStyleBackColor = false;
            // 
            // dgvAppointmentHistory
            // 
            dgvAppointmentHistory.AllowUserToAddRows = false;
            dgvAppointmentHistory.AllowUserToDeleteRows = false;
            dgvAppointmentHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAppointmentHistory.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAppointmentHistory.Location = new Point(19, 21);
            dgvAppointmentHistory.Margin = new Padding(6, 6, 6, 6);
            dgvAppointmentHistory.Name = "dgvAppointmentHistory";
            dgvAppointmentHistory.ReadOnly = true;
            dgvAppointmentHistory.RowHeadersWidth = 82;
            dgvAppointmentHistory.RowTemplate.Height = 25;
            dgvAppointmentHistory.Size = new Size(1523, 1280);
            dgvAppointmentHistory.TabIndex = 0;
            // 
            // tpProfile
            // 
            tpProfile.Controls.Add(pnlProfileForm);
            tpProfile.Location = new Point(8, 46);
            tpProfile.Margin = new Padding(6, 6, 6, 6);
            tpProfile.Name = "tpProfile";
            tpProfile.Padding = new Padding(6, 6, 6, 6);
            tpProfile.Size = new Size(2517, 2999);
            tpProfile.TabIndex = 3;
            tpProfile.Text = "My Profile";
            tpProfile.UseVisualStyleBackColor = true;
            // 
            // pnlProfileForm
            // 
            pnlProfileForm.AutoScroll = true;
            pnlProfileForm.Controls.Add(btnChangePassword);
            pnlProfileForm.Controls.Add(btnSaveProfile);
            pnlProfileForm.Controls.Add(btnEditProfile);
            pnlProfileForm.Controls.Add(lblBloodTypeLabel);
            pnlProfileForm.Controls.Add(txtBloodType);
            pnlProfileForm.Controls.Add(lblGenderLabel);
            pnlProfileForm.Controls.Add(txtGender);
            pnlProfileForm.Controls.Add(lblDOBLabel);
            pnlProfileForm.Controls.Add(txtDOB);
            pnlProfileForm.Controls.Add(lblStudentIdLabel);
            pnlProfileForm.Controls.Add(txtStudentId);
            pnlProfileForm.Controls.Add(lblPhoneLabel);
            pnlProfileForm.Controls.Add(txtProfilePhone);
            pnlProfileForm.Controls.Add(lblEmailLabel);
            pnlProfileForm.Controls.Add(txtProfileEmail);
            pnlProfileForm.Controls.Add(lblFullNameLabel);
            pnlProfileForm.Controls.Add(txtProfileFullName);
            pnlProfileForm.Dock = DockStyle.Fill;
            pnlProfileForm.Location = new Point(6, 6);
            pnlProfileForm.Margin = new Padding(6, 6, 6, 6);
            pnlProfileForm.Name = "pnlProfileForm";
            pnlProfileForm.Size = new Size(2505, 2987);
            pnlProfileForm.TabIndex = 0;
            // 
            // btnChangePassword
            // 
            btnChangePassword.BackColor = Color.Orange;
            btnChangePassword.ForeColor = Color.White;
            btnChangePassword.Location = new Point(483, 960);
            btnChangePassword.Margin = new Padding(6, 6, 6, 6);
            btnChangePassword.Name = "btnChangePassword";
            btnChangePassword.Size = new Size(204, 85);
            btnChangePassword.TabIndex = 9;
            btnChangePassword.Text = "🔐 Change Password";
            btnChangePassword.UseVisualStyleBackColor = false;
            // 
            // btnSaveProfile
            // 
            btnSaveProfile.BackColor = Color.Green;
            btnSaveProfile.Enabled = false;
            btnSaveProfile.ForeColor = Color.White;
            btnSaveProfile.Location = new Point(260, 960);
            btnSaveProfile.Margin = new Padding(6, 6, 6, 6);
            btnSaveProfile.Name = "btnSaveProfile";
            btnSaveProfile.Size = new Size(204, 85);
            btnSaveProfile.TabIndex = 8;
            btnSaveProfile.Text = "✓ Save Changes";
            btnSaveProfile.UseVisualStyleBackColor = false;
            // 
            // btnEditProfile
            // 
            btnEditProfile.BackColor = Color.Blue;
            btnEditProfile.ForeColor = Color.White;
            btnEditProfile.Location = new Point(37, 960);
            btnEditProfile.Margin = new Padding(6, 6, 6, 6);
            btnEditProfile.Name = "btnEditProfile";
            btnEditProfile.Size = new Size(204, 85);
            btnEditProfile.TabIndex = 7;
            btnEditProfile.Text = "✎ Edit Profile";
            btnEditProfile.UseVisualStyleBackColor = false;
            // 
            // lblBloodTypeLabel
            // 
            lblBloodTypeLabel.AutoSize = true;
            lblBloodTypeLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblBloodTypeLabel.Location = new Point(37, 811);
            lblBloodTypeLabel.Margin = new Padding(6, 0, 6, 0);
            lblBloodTypeLabel.Name = "lblBloodTypeLabel";
            lblBloodTypeLabel.Size = new Size(169, 37);
            lblBloodTypeLabel.TabIndex = 6;
            lblBloodTypeLabel.Text = "Blood Type:";
            // 
            // txtBloodType
            // 
            txtBloodType.Location = new Point(37, 864);
            txtBloodType.Margin = new Padding(6, 6, 6, 6);
            txtBloodType.Name = "txtBloodType";
            txtBloodType.ReadOnly = true;
            txtBloodType.Size = new Size(647, 39);
            txtBloodType.TabIndex = 6;
            // 
            // lblGenderLabel
            // 
            lblGenderLabel.AutoSize = true;
            lblGenderLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblGenderLabel.Location = new Point(37, 683);
            lblGenderLabel.Margin = new Padding(6, 0, 6, 0);
            lblGenderLabel.Name = "lblGenderLabel";
            lblGenderLabel.Size = new Size(118, 37);
            lblGenderLabel.TabIndex = 5;
            lblGenderLabel.Text = "Gender:";
            // 
            // txtGender
            // 
            txtGender.Location = new Point(37, 736);
            txtGender.Margin = new Padding(6, 6, 6, 6);
            txtGender.Name = "txtGender";
            txtGender.ReadOnly = true;
            txtGender.Size = new Size(647, 39);
            txtGender.TabIndex = 5;
            // 
            // lblDOBLabel
            // 
            lblDOBLabel.AutoSize = true;
            lblDOBLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDOBLabel.Location = new Point(37, 555);
            lblDOBLabel.Margin = new Padding(6, 0, 6, 0);
            lblDOBLabel.Name = "lblDOBLabel";
            lblDOBLabel.Size = new Size(190, 37);
            lblDOBLabel.TabIndex = 4;
            lblDOBLabel.Text = "Date of Birth:";
            // 
            // txtDOB
            // 
            txtDOB.Location = new Point(37, 608);
            txtDOB.Margin = new Padding(6, 6, 6, 6);
            txtDOB.Name = "txtDOB";
            txtDOB.ReadOnly = true;
            txtDOB.Size = new Size(647, 39);
            txtDOB.TabIndex = 4;
            // 
            // lblStudentIdLabel
            // 
            lblStudentIdLabel.AutoSize = true;
            lblStudentIdLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblStudentIdLabel.Location = new Point(37, 427);
            lblStudentIdLabel.Margin = new Padding(6, 0, 6, 0);
            lblStudentIdLabel.Name = "lblStudentIdLabel";
            lblStudentIdLabel.Size = new Size(160, 37);
            lblStudentIdLabel.TabIndex = 3;
            lblStudentIdLabel.Text = "Student ID:";
            // 
            // txtStudentId
            // 
            txtStudentId.Location = new Point(37, 480);
            txtStudentId.Margin = new Padding(6, 6, 6, 6);
            txtStudentId.Name = "txtStudentId";
            txtStudentId.ReadOnly = true;
            txtStudentId.Size = new Size(647, 39);
            txtStudentId.TabIndex = 3;
            // 
            // lblPhoneLabel
            // 
            lblPhoneLabel.AutoSize = true;
            lblPhoneLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblPhoneLabel.Location = new Point(37, 299);
            lblPhoneLabel.Margin = new Padding(6, 0, 6, 0);
            lblPhoneLabel.Name = "lblPhoneLabel";
            lblPhoneLabel.Size = new Size(105, 37);
            lblPhoneLabel.TabIndex = 2;
            lblPhoneLabel.Text = "Phone:";
            // 
            // txtProfilePhone
            // 
            txtProfilePhone.Location = new Point(37, 352);
            txtProfilePhone.Margin = new Padding(6, 6, 6, 6);
            txtProfilePhone.Name = "txtProfilePhone";
            txtProfilePhone.ReadOnly = true;
            txtProfilePhone.Size = new Size(647, 39);
            txtProfilePhone.TabIndex = 2;
            // 
            // lblEmailLabel
            // 
            lblEmailLabel.AutoSize = true;
            lblEmailLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblEmailLabel.Location = new Point(37, 171);
            lblEmailLabel.Margin = new Padding(6, 0, 6, 0);
            lblEmailLabel.Name = "lblEmailLabel";
            lblEmailLabel.Size = new Size(94, 37);
            lblEmailLabel.TabIndex = 1;
            lblEmailLabel.Text = "Email:";
            // 
            // txtProfileEmail
            // 
            txtProfileEmail.Location = new Point(37, 224);
            txtProfileEmail.Margin = new Padding(6, 6, 6, 6);
            txtProfileEmail.Name = "txtProfileEmail";
            txtProfileEmail.ReadOnly = true;
            txtProfileEmail.Size = new Size(647, 39);
            txtProfileEmail.TabIndex = 1;
            // 
            // lblFullNameLabel
            // 
            lblFullNameLabel.AutoSize = true;
            lblFullNameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblFullNameLabel.Location = new Point(37, 43);
            lblFullNameLabel.Margin = new Padding(6, 0, 6, 0);
            lblFullNameLabel.Name = "lblFullNameLabel";
            lblFullNameLabel.Size = new Size(153, 37);
            lblFullNameLabel.TabIndex = 0;
            lblFullNameLabel.Text = "Full Name:";
            // 
            // txtProfileFullName
            // 
            txtProfileFullName.Location = new Point(37, 96);
            txtProfileFullName.Margin = new Padding(6, 6, 6, 6);
            txtProfileFullName.Name = "txtProfileFullName";
            txtProfileFullName.ReadOnly = true;
            txtProfileFullName.Size = new Size(647, 39);
            txtProfileFullName.TabIndex = 0;
            // 
            // tpNotifications
            // 
            tpNotifications.Controls.Add(btnRefreshNotifications);
            tpNotifications.Controls.Add(btnMarkAsRead);
            tpNotifications.Controls.Add(dgvNotifications);
            tpNotifications.Location = new Point(8, 46);
            tpNotifications.Margin = new Padding(6, 6, 6, 6);
            tpNotifications.Name = "tpNotifications";
            tpNotifications.Padding = new Padding(6, 6, 6, 6);
            tpNotifications.Size = new Size(2517, 2999);
            tpNotifications.TabIndex = 4;
            tpNotifications.Text = "Notifications";
            tpNotifications.UseVisualStyleBackColor = true;
            // 
            // btnRefreshNotifications
            // 
            btnRefreshNotifications.BackColor = Color.Blue;
            btnRefreshNotifications.ForeColor = Color.White;
            btnRefreshNotifications.Location = new Point(706, 1323);
            btnRefreshNotifications.Margin = new Padding(6, 6, 6, 6);
            btnRefreshNotifications.Name = "btnRefreshNotifications";
            btnRefreshNotifications.Size = new Size(223, 85);
            btnRefreshNotifications.TabIndex = 2;
            btnRefreshNotifications.Text = "🔄 Refresh";
            btnRefreshNotifications.UseVisualStyleBackColor = false;
            // 
            // btnMarkAsRead
            // 
            btnMarkAsRead.BackColor = Color.Blue;
            btnMarkAsRead.ForeColor = Color.White;
            btnMarkAsRead.Location = new Point(464, 1323);
            btnMarkAsRead.Margin = new Padding(6, 6, 6, 6);
            btnMarkAsRead.Name = "btnMarkAsRead";
            btnMarkAsRead.Size = new Size(223, 85);
            btnMarkAsRead.TabIndex = 1;
            btnMarkAsRead.Text = "✓ Mark as Read";
            btnMarkAsRead.UseVisualStyleBackColor = false;
            // 
            // dgvNotifications
            // 
            dgvNotifications.AllowUserToAddRows = false;
            dgvNotifications.AllowUserToDeleteRows = false;
            dgvNotifications.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNotifications.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNotifications.Location = new Point(19, 21);
            dgvNotifications.Margin = new Padding(6, 6, 6, 6);
            dgvNotifications.Name = "dgvNotifications";
            dgvNotifications.ReadOnly = true;
            dgvNotifications.RowHeadersWidth = 82;
            dgvNotifications.RowTemplate.Height = 25;
            dgvNotifications.Size = new Size(1523, 1280);
            dgvNotifications.TabIndex = 0;
            // 
            // tpSettings
            // 
            tpSettings.Controls.Add(lblSettingsTitle);
            tpSettings.Controls.Add(btnSaveSettings);
            tpSettings.Controls.Add(chkAppNotifications);
            tpSettings.Controls.Add(chkSMSNotifications);
            tpSettings.Controls.Add(chkEmailNotifications);
            tpSettings.Location = new Point(8, 46);
            tpSettings.Margin = new Padding(6, 6, 6, 6);
            tpSettings.Name = "tpSettings";
            tpSettings.Padding = new Padding(6, 6, 6, 6);
            tpSettings.Size = new Size(2517, 2999);
            tpSettings.TabIndex = 5;
            tpSettings.Text = "Settings";
            tpSettings.UseVisualStyleBackColor = true;
            // 
            // lblSettingsTitle
            // 
            lblSettingsTitle.AutoSize = true;
            lblSettingsTitle.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            lblSettingsTitle.Location = new Point(37, 43);
            lblSettingsTitle.Margin = new Padding(6, 0, 6, 0);
            lblSettingsTitle.Name = "lblSettingsTitle";
            lblSettingsTitle.Size = new Size(520, 51);
            lblSettingsTitle.TabIndex = 0;
            lblSettingsTitle.Text = "⚙️ Notification Preferences";
            // 
            // btnSaveSettings
            // 
            btnSaveSettings.BackColor = Color.Green;
            btnSaveSettings.Font = new Font("Segoe UI", 11F);
            btnSaveSettings.ForeColor = Color.White;
            btnSaveSettings.Location = new Point(56, 427);
            btnSaveSettings.Margin = new Padding(6, 6, 6, 6);
            btnSaveSettings.Name = "btnSaveSettings";
            btnSaveSettings.Size = new Size(223, 85);
            btnSaveSettings.TabIndex = 3;
            btnSaveSettings.Text = "✓ Save Settings";
            btnSaveSettings.UseVisualStyleBackColor = false;
            // 
            // chkAppNotifications
            // 
            chkAppNotifications.AutoSize = true;
            chkAppNotifications.Checked = true;
            chkAppNotifications.CheckState = CheckState.Checked;
            chkAppNotifications.Font = new Font("Segoe UI", 11F);
            chkAppNotifications.Location = new Point(56, 320);
            chkAppNotifications.Margin = new Padding(6, 6, 6, 6);
            chkAppNotifications.Name = "chkAppNotifications";
            chkAppNotifications.Size = new Size(366, 45);
            chkAppNotifications.TabIndex = 2;
            chkAppNotifications.Text = "🔔 In-App Notifications";
            chkAppNotifications.UseVisualStyleBackColor = true;
            // 
            // chkSMSNotifications
            // 
            chkSMSNotifications.AutoSize = true;
            chkSMSNotifications.Font = new Font("Segoe UI", 11F);
            chkSMSNotifications.Location = new Point(56, 235);
            chkSMSNotifications.Margin = new Padding(6, 6, 6, 6);
            chkSMSNotifications.Name = "chkSMSNotifications";
            chkSMSNotifications.Size = new Size(333, 45);
            chkSMSNotifications.TabIndex = 1;
            chkSMSNotifications.Text = "📱 SMS Notifications";
            chkSMSNotifications.UseVisualStyleBackColor = true;
            // 
            // chkEmailNotifications
            // 
            chkEmailNotifications.AutoSize = true;
            chkEmailNotifications.Checked = true;
            chkEmailNotifications.CheckState = CheckState.Checked;
            chkEmailNotifications.Font = new Font("Segoe UI", 11F);
            chkEmailNotifications.Location = new Point(56, 149);
            chkEmailNotifications.Margin = new Padding(6, 6, 6, 6);
            chkEmailNotifications.Name = "chkEmailNotifications";
            chkEmailNotifications.Size = new Size(344, 45);
            chkEmailNotifications.TabIndex = 0;
            chkEmailNotifications.Text = "📧 Email Notifications";
            chkEmailNotifications.UseVisualStyleBackColor = true;
            // 
            // frmStudentDashboard
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2884, 1579);
            Controls.Add(pnlMainContent);
            Controls.Add(pnlSidebar);
            Margin = new Padding(6, 6, 6, 6);
            Name = "frmStudentDashboard";
            Text = "Student Dashboard";
            Load += FrmStudentDashboard_Load;
            pnlSidebar.ResumeLayout(false);
            pnlSidebarHeader.ResumeLayout(false);
            pnlSidebarHeader.PerformLayout();
            pnlMainContent.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tpDashboard.ResumeLayout(false);
            pnlHealthReminders.ResumeLayout(false);
            pnlHealthReminders.PerformLayout();
            pnlNextAppointment.ResumeLayout(false);
            pnlNextAppointment.PerformLayout();
            tpBookAppointment.ResumeLayout(false);
            tpBookAppointment.PerformLayout();
            tpAppointmentHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAppointmentHistory).EndInit();
            tpProfile.ResumeLayout(false);
            pnlProfileForm.ResumeLayout(false);
            pnlProfileForm.PerformLayout();
            tpNotifications.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvNotifications).EndInit();
            tpSettings.ResumeLayout(false);
            tpSettings.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Button btnNavLogout;
        private System.Windows.Forms.Button btnNavSettings;
        private System.Windows.Forms.Button btnNavNotifications;
        private System.Windows.Forms.Button btnNavProfile;
        private System.Windows.Forms.Button btnNavAppointmentHistory;
        private System.Windows.Forms.Button btnNavBookAppointment;
        private System.Windows.Forms.Button btnNavDashboard;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpDashboard;
        private System.Windows.Forms.Panel pnlHealthReminders;
        private System.Windows.Forms.Label lblRemindersTitle;
        private System.Windows.Forms.ListBox lbReminders;
        private System.Windows.Forms.Panel pnlNextAppointment;
        private System.Windows.Forms.Label lblAppointmentTitle;
        private System.Windows.Forms.Label lblNextApptStatusValue;
        private System.Windows.Forms.Label lblNextApptReasonValue;
        private System.Windows.Forms.Label lblNextApptProviderValue;
        private System.Windows.Forms.Label lblNextApptTimeValue;
        private System.Windows.Forms.Label lblNextApptDateValue;
        private System.Windows.Forms.Label lblNextApptStatus;
        private System.Windows.Forms.Label lblNextApptReason;
        private System.Windows.Forms.Label lblNextApptProvider;
        private System.Windows.Forms.Label lblNextApptTime;
        private System.Windows.Forms.Label lblNextApptDate;
        private System.Windows.Forms.TabPage tpBookAppointment;
        private System.Windows.Forms.Label lblBookingTitle;
        private System.Windows.Forms.Button btnCancelBooking;
        private System.Windows.Forms.Button btnBookAppointment;
        private System.Windows.Forms.Label lblReasonLabel;
        private System.Windows.Forms.TextBox txtAppointmentReason;
        private System.Windows.Forms.Label lblTimeLabel;
        private System.Windows.Forms.ComboBox cmbTimeSlot;
        private System.Windows.Forms.Label lblDateLabel;
        private System.Windows.Forms.DateTimePicker dtpAppointmentDate;
        private System.Windows.Forms.TabPage tpAppointmentHistory;
        private System.Windows.Forms.Button btnRefreshHistory;
        private System.Windows.Forms.DataGridView dgvAppointmentHistory;
        private System.Windows.Forms.TabPage tpProfile;
        private System.Windows.Forms.Panel pnlProfileForm;
        private System.Windows.Forms.Button btnChangePassword;
        private System.Windows.Forms.Button btnSaveProfile;
        private System.Windows.Forms.Button btnEditProfile;
        private System.Windows.Forms.Label lblBloodTypeLabel;
        private System.Windows.Forms.TextBox txtBloodType;
        private System.Windows.Forms.Label lblGenderLabel;
        private System.Windows.Forms.TextBox txtGender;
        private System.Windows.Forms.Label lblDOBLabel;
        private System.Windows.Forms.TextBox txtDOB;
        private System.Windows.Forms.Label lblStudentIdLabel;
        private System.Windows.Forms.TextBox txtStudentId;
        private System.Windows.Forms.Label lblPhoneLabel;
        private System.Windows.Forms.TextBox txtProfilePhone;
        private System.Windows.Forms.Label lblEmailLabel;
        private System.Windows.Forms.TextBox txtProfileEmail;
        private System.Windows.Forms.Label lblFullNameLabel;
        private System.Windows.Forms.TextBox txtProfileFullName;
        private System.Windows.Forms.TabPage tpNotifications;
        private System.Windows.Forms.Button btnRefreshNotifications;
        private System.Windows.Forms.Button btnMarkAsRead;
        private System.Windows.Forms.DataGridView dgvNotifications;
        private System.Windows.Forms.TabPage tpSettings;
        private System.Windows.Forms.Label lblSettingsTitle;
        private System.Windows.Forms.Button btnSaveSettings;
        private System.Windows.Forms.CheckBox chkAppNotifications;
        private System.Windows.Forms.CheckBox chkSMSNotifications;
        private System.Windows.Forms.CheckBox chkEmailNotifications;
        private System.Windows.Forms.Panel pnlSidebarHeader;
        private System.Windows.Forms.Label lblStudentRole;
        private System.Windows.Forms.Label lblStudentName;
        private System.Windows.Forms.Label lblAvatar;
    }
}