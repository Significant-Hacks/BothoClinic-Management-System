using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Linq;
using BothoClinic.Models;
using Microsoft.VisualBasic;

namespace BothoClinic.Forms
{
    public partial class frmAddGuestPatient : Form
    {
        private DatabaseHelper dbHelper;
        private User _currentUser;
        private bool isStudentSelected = false;
        private StudentInfo selectedStudent = null!;
        
        // Control declarations that are needed
        private Button btnSave = null!;
        private ComboBox cmbPatientType = null!;
        private ComboBox cmbEmergencyStatus = null!;
        private TextBox txtStudentId = null!;
        private Button btnValidateStudent = null!;
        private Label lblStudentInfo = null!;
        private TextBox txtFullName = null!;
        private TextBox txtPhone = null!;
        private Label lblGuestId = null!;
        private TextBox txtGuestId = null!;
        private Button btnSearchGuest = null!;
        private Label lblGuestName = null!;
        private TextBox txtGuestName = null!;
        private Button btnSearchGuestName = null!;
        private Label lblGuestInfo = null!;
        private Label lblEmergencyNumber = null!;
        private TextBox txtEmergencyNumber = null!;
        private Label lblEmergencyCode = null!;
        private TextBox txtEmergencyCode = null!;
        private TextBox txtEmergencyContact = null!;
        private TextBox txtEmergencyPhone = null!;
        private TextBox txtAddress = null!;
        private DateTimePicker dtpAppointmentDate = null!;
        private DateTimePicker dtpAppointmentTime = null!;
        private TextBox txtReason = null!;

        public frmAddGuestPatient(User currentUser)
        {
            if (currentUser == null)
            {
                throw new ArgumentNullException(nameof(currentUser), "Current user cannot be null");
            }
            
            InitializeComponent();
            _currentUser = currentUser;
            dbHelper = new DatabaseHelper();
            
            try
            {
                SetupForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing form: {ex.Message}\n\nStack Trace: {ex.StackTrace}", 
                    "Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void SetupForm()
        {
            try
            {
                this.Text = "Add New Patient";
                this.Size = new Size(800, 700);
                this.StartPosition = FormStartPosition.CenterParent;
                this.FormBorderStyle = FormBorderStyle.FixedDialog;
                this.MaximizeBox = false;
                this.MinimizeBox = false;

                // Header Panel
                var headerPanel = new Panel()
                {
                    Height = 80,
                    Dock = DockStyle.Top,
                    BackColor = Color.FromArgb(52, 152, 219),
                    Padding = new Padding(20)
                };

            var headerLabel = new Label()
            {
                Text = "Add New Patient",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(20, 25)
            };

            var subHeaderLabel = new Label()
            {
                Text = "Register a guest patient or add emergency student patient",
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(220, 220, 220),
                AutoSize = true,
                Location = new Point(20, 50)
            };

            headerPanel.Controls.Add(headerLabel);
            headerPanel.Controls.Add(subHeaderLabel);

            // Main Content Panel with ScrollableArea
            var contentPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.White,
                AutoScroll = true
            };

            // Patient Type Selection (Row 1)
            var lblPatientType = new Label()
            {
                Name = "lblPatientType",
                Text = "Patient Type *",
                Location = new Point(20, 20),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            cmbPatientType = new ComboBox()
            {
                Name = "cmbPatientType",
                Location = new Point(150, 20),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 11F),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbPatientType.Items.AddRange(new string[] { "Guest Patient", "Student (Emergency)" });
            cmbPatientType.SelectedIndex = 0;

            // Emergency Status Control (Row 2) - Only visible in Guest mode
            var lblEmergencyStatus = new Label()
            {
                Name = "lblEmergencyStatus",
                Text = "Emergency Status",
                Location = new Point(20, 60),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Visible = false // Hidden by default, shown only in Guest mode
            };

            cmbEmergencyStatus = new ComboBox()
            {
                Name = "cmbEmergencyStatus",
                Location = new Point(150, 60),
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 11F),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Visible = false // Hidden by default, shown only in Guest mode
            };
            cmbEmergencyStatus.Items.AddRange(new object[] { "False", "True" });
            cmbEmergencyStatus.SelectedIndex = 0; // Default to False

            // Student ID Validation (Row 2) - Right below Patient Type
            var lblStudentId = new Label()
            {
                Name = "lblStudentId",
                Text = "Student ID *",
                Location = new Point(20, 60),
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                Visible = false
            };

            txtStudentId = new TextBox()
            {
                Name = "txtStudentId",
                Location = new Point(150, 60),
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 11F),
                PlaceholderText = "Enter Student ID (e.g., S2023001)",
                Visible = false
            };
            txtStudentId.TextChanged += TxtStudentId_TextChanged;
            txtStudentId.KeyDown += TxtStudentId_KeyDown;

            btnValidateStudent = new Button()
            {
                Name = "btnValidateStudent",
                Text = "Validate",
                Location = new Point(360, 60),
                Size = new Size(80, 30),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 9F),
                Visible = false
            };
            btnValidateStudent.FlatAppearance.BorderSize = 0;
            btnValidateStudent.Click += BtnValidateStudent_Click;

            // Student Info Display (Row 3) - Below Student ID with space for validation message
            lblStudentInfo = new Label()
            {
                Name = "lblStudentInfo",
                Text = "",
                Location = new Point(20, 100),
                Size = new Size(600, 30),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(40, 167, 69),
                Visible = false
            };

            // Guest Patient Fields (Row 4-8) - MOVED DOWN TO ACCOMMODATE GUEST SEARCH
            var lblFullName = new Label()
            {
                Text = "Full Name *",
                Location = new Point(20, 160), // Moved down to accommodate Guest search
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            txtFullName = new TextBox()
            {
                Location = new Point(150, 160), // Moved down to accommodate Guest search
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 11F),
                MaxLength = 100
            };

            var lblPhone = new Label()
            {
                Text = "Phone Number *",
                Location = new Point(420, 160), // Moved down to accommodate Guest search
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            txtPhone = new TextBox()
            {
                Location = new Point(550, 160), // Moved down to accommodate Guest search
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 11F),
                MaxLength = 15
            };

            // Row 2 - Guest ID Search (for Guest patients only) - RIGHT BELOW PATIENT TYPE
            lblGuestId = new Label()
            {
                Text = "Guest ID",
                Location = new Point(20, 60), // Right below Patient Type dropdown
                Size = new Size(80, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            txtGuestId = new TextBox()
            {
                Location = new Point(110, 60), // Right below Patient Type dropdown
                Size = new Size(120, 30),
                Font = new Font("Segoe UI", 11F),
                PlaceholderText = "Enter Guest ID"
            };

            btnSearchGuest = new Button()
            {
                Text = "Search",
                Location = new Point(240, 60), // Right below Patient Type dropdown
                Size = new Size(80, 30),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSearchGuest.Click += BtnSearchGuest_Click;
            txtGuestId.KeyDown += TxtGuestId_KeyDown;

            // Guest Name Search
            lblGuestName = new Label()
            {
                Text = "or Name",
                Location = new Point(330, 60), // Right below Patient Type dropdown
                Size = new Size(60, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            txtGuestName = new TextBox()
            {
                Location = new Point(400, 60), // Right below Patient Type dropdown
                Size = new Size(150, 30),
                Font = new Font("Segoe UI", 11F),
                PlaceholderText = "Enter Guest Name"
            };

            btnSearchGuestName = new Button()
            {
                Text = "Search",
                Location = new Point(560, 60), // Right below Patient Type dropdown
                Size = new Size(80, 30),
                Font = new Font("Segoe UI", 10F),
                BackColor = Color.FromArgb(52, 152, 219),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnSearchGuestName.Click += BtnSearchGuestName_Click;
            txtGuestName.KeyDown += TxtGuestName_KeyDown;

            // Guest Info Display
            lblGuestInfo = new Label()
            {
                Text = "",
                Location = new Point(20, 100), // Below Guest search controls
                Size = new Size(600, 30),
                Font = new Font("Segoe UI", 10F),
                ForeColor = Color.FromArgb(40, 167, 69)
            };

            // Emergency Number field (phone number for emergency contact)
            lblEmergencyNumber = new Label()
            {
                Text = "Emergency Number",
                Location = new Point(20, 200), // Below Phone Number field
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            txtEmergencyNumber = new TextBox()
            {
                Location = new Point(150, 200), // Below Phone Number field
                Size = new Size(250, 30),
                Font = new Font("Segoe UI", 11F),
                MaxLength = 15,
                PlaceholderText = "Emergency contact number"
            };

            // Emergency Code field (generated unique identifier for emergency instance)
            lblEmergencyCode = new Label()
            {
                Text = "EMG. Code",
                Location = new Point(420, 200), // Next to Emergency Number field
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            txtEmergencyCode = new TextBox()
            {
                Location = new Point(550, 200), // Next to Emergency Number field
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 11F),
                MaxLength = 20,
                ReadOnly = true, // This will be auto-generated
                BackColor = Color.FromArgb(240, 240, 240),
                PlaceholderText = "Auto-generated emergency code"
            };

            // Emergency Contact Controls (for backward compatibility) - REMOVED TO PREVENT FLOATING BOXES
            txtEmergencyContact = new TextBox()
            {
                Location = new Point(-1000, -1000), // Move off-screen
                Size = new Size(1, 1), // Minimal size
                Font = new Font("Segoe UI", 11F),
                Visible = false // Hidden by default
            };

            txtEmergencyPhone = new TextBox()
            {
                Location = new Point(-1000, -1000), // Move off-screen
                Size = new Size(1, 1), // Minimal size
                Font = new Font("Segoe UI", 11F),
                Visible = false // Hidden by default
            };

            // Row 6 - Address (moved down to accommodate Emergency fields)
            var lblAddress = new Label()
            {
                Text = "Address",
                Location = new Point(20, 240), // Moved down to accommodate Emergency fields
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            txtAddress = new TextBox()
            {
                Location = new Point(150, 240), // Moved down to accommodate Emergency fields
                Size = new Size(600, 60),
                Font = new Font("Segoe UI", 11F),
                Multiline = true,
                MaxLength = 200
            };

            // Appointment Information Section (Row 7-10) - moved down to accommodate Emergency fields
            var lblAppointmentSection = new Label()
            {
                Text = "Appointment Information",
                Location = new Point(20, 320), // Moved down to accommodate Emergency fields
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.FromArgb(52, 152, 219)
            };

            // Row 8 - moved down to accommodate Emergency fields
            var lblAppointmentDate = new Label()
            {
                Text = "Appointment Date *",
                Location = new Point(20, 360), // Moved down to accommodate Emergency fields
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            dtpAppointmentDate = new DateTimePicker()
            {
                Location = new Point(150, 360), // Moved down to accommodate Emergency fields
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 11F),
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Today
            };

            var lblAppointmentTime = new Label()
            {
                Text = "Appointment Time *",
                Location = new Point(420, 360), // Moved down to accommodate Emergency fields
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            dtpAppointmentTime = new DateTimePicker()
            {
                Location = new Point(550, 360), // Moved down to accommodate Emergency fields
                Size = new Size(200, 30),
                Font = new Font("Segoe UI", 11F),
                Format = DateTimePickerFormat.Time,
                ShowUpDown = true,
                Value = DateTime.Now
            };

            // Row 9 - moved down to accommodate Emergency fields
            var lblReason = new Label()
            {
                Text = "Reason for Visit *",
                Location = new Point(20, 400), // Moved down to accommodate Emergency fields
                Size = new Size(120, 25),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold)
            };

            txtReason = new TextBox()
            {
                Location = new Point(150, 400), // Moved down to accommodate Emergency fields
                Size = new Size(600, 60),
                Font = new Font("Segoe UI", 11F),
                Multiline = true,
                MaxLength = 500,
                PlaceholderText = "Describe the reason for the visit or emergency"
            };

            // Notes field removed - not supported by database schema

            // Button Panel
            var buttonPanel = new Panel()
            {
                Height = 60,
                Dock = DockStyle.Bottom,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(20)
            };

            btnSave = new Button()
            {
                Text = "Save Patient",
                Size = new Size(150, 35),
                Location = new Point(300, 12),
                BackColor = Color.FromArgb(40, 167, 69),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold)
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;

            var btnCancel = new Button()
            {
                Text = "Cancel",
                Size = new Size(80, 35),
                Location = new Point(470, 12),
                BackColor = Color.FromArgb(108, 117, 125),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F)
            };
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.Click += (s, e) => this.Close();

            buttonPanel.Controls.Add(btnSave);
            buttonPanel.Controls.Add(btnCancel);

            // Add all controls to content panel
            contentPanel.Controls.Add(lblPatientType);
            contentPanel.Controls.Add(cmbPatientType);
            contentPanel.Controls.Add(lblEmergencyStatus);
            contentPanel.Controls.Add(cmbEmergencyStatus);
            contentPanel.Controls.Add(lblStudentId);
            contentPanel.Controls.Add(txtStudentId);
            contentPanel.Controls.Add(btnValidateStudent);
            contentPanel.Controls.Add(lblStudentInfo);
            contentPanel.Controls.Add(lblFullName);
            contentPanel.Controls.Add(txtFullName);
            contentPanel.Controls.Add(lblPhone);
            contentPanel.Controls.Add(txtPhone);
            contentPanel.Controls.Add(lblEmergencyNumber);
            contentPanel.Controls.Add(txtEmergencyNumber);
            contentPanel.Controls.Add(lblEmergencyCode);
            contentPanel.Controls.Add(txtEmergencyCode);
            contentPanel.Controls.Add(lblGuestId);
            contentPanel.Controls.Add(txtGuestId);
            contentPanel.Controls.Add(btnSearchGuest);
            contentPanel.Controls.Add(lblGuestName);
            contentPanel.Controls.Add(txtGuestName);
            contentPanel.Controls.Add(btnSearchGuestName);
            contentPanel.Controls.Add(lblGuestInfo);
            contentPanel.Controls.Add(txtEmergencyContact);
            contentPanel.Controls.Add(txtEmergencyPhone);
            contentPanel.Controls.Add(lblAddress);
            contentPanel.Controls.Add(txtAddress);
            contentPanel.Controls.Add(lblAppointmentSection);
            contentPanel.Controls.Add(lblAppointmentDate);
            contentPanel.Controls.Add(dtpAppointmentDate);
            contentPanel.Controls.Add(lblAppointmentTime);
            contentPanel.Controls.Add(dtpAppointmentTime);
            contentPanel.Controls.Add(lblReason);
            contentPanel.Controls.Add(txtReason);
            // Notes controls removed - not supported by database schema

            this.Controls.Add(contentPanel);
            this.Controls.Add(headerPanel);
            this.Controls.Add(buttonPanel);
            
            // Wire up events AFTER all controls are created and added
            cmbPatientType.SelectedIndexChanged += CmbPatientType_SelectedIndexChanged;
            cmbEmergencyStatus.SelectedIndexChanged += CmbEmergencyStatus_SelectedIndexChanged;
            
            // Initialize form state for Guest Patient (default selection)
            InitializeGuestMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in SetupForm: {ex.Message}\n\nStack Trace: {ex.StackTrace}", 
                    "SetupForm Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        private void InitializeGuestMode()
        {
            try
            {
                // Set initial state for Guest Patient mode
                isStudentSelected = false;
                
                // Show Guest search controls (with null checks)
                if (lblGuestId != null) lblGuestId.Visible = true;
                if (txtGuestId != null) txtGuestId.Visible = true;
                if (btnSearchGuest != null) btnSearchGuest.Visible = true;
                if (lblGuestName != null) lblGuestName.Visible = true;
                if (txtGuestName != null) txtGuestName.Visible = true;
                if (btnSearchGuestName != null) btnSearchGuestName.Visible = true;
                if (lblGuestInfo != null) lblGuestInfo.Visible = true;
                
                // Hide student controls
                var lblStudentId = this.Controls.Find("lblStudentId", true).FirstOrDefault();
                var txtStudentIdControl = this.Controls.Find("txtStudentId", true).FirstOrDefault();
                var btnValidateStudent = this.Controls.Find("btnValidateStudent", true).FirstOrDefault();
                var lblStudentInfo = this.Controls.Find("lblStudentInfo", true).FirstOrDefault();
                
                if (lblStudentId != null) lblStudentId.Visible = false;
                if (txtStudentIdControl != null) txtStudentIdControl.Visible = false;
                if (btnValidateStudent != null) btnValidateStudent.Visible = false;
                if (lblStudentInfo != null) lblStudentInfo.Visible = false;
                
                // Hide Emergency Contact field for guests
                if (txtEmergencyContact != null) txtEmergencyContact.Visible = false;
                
                // Set initial label for Emergency Phone field (will show Guest ID)
                var lblEmergencyPhone = this.Controls.Find("lblEmergencyPhone", true).FirstOrDefault();
                if (lblEmergencyPhone != null) lblEmergencyPhone.Text = "Guest ID *";
                
                // Clear Guest ID field initially
                if (txtEmergencyPhone != null) txtEmergencyPhone.Text = "";
                if (lblGuestInfo != null) lblGuestInfo.Text = "Enter Guest ID or Name to search for existing guest, or leave empty for new guest";
            }
            catch (Exception ex)
            {
                // Handle initialization error gracefully
                System.Diagnostics.Debug.WriteLine($"Error initializing guest mode: {ex.Message}");
            }
        }

        // Event Handlers
        
        private void CmbEmergencyStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                bool isEmergency = cmbEmergencyStatus.SelectedIndex == 1; // True
                
                if (isEmergency)
                {
                    // Emergency mode: Show EMG Code field, hide Guest ID search
                    if (lblEmergencyCode != null && txtEmergencyCode != null)
                    {
                        lblEmergencyCode.Visible = true;
                        txtEmergencyCode.Visible = true;
                        
                        // Generate EMG Code - checks database first for existing codes
                        GenerateGuestEmergencyCode();
                    }
                    
                    // Hide Guest ID search controls
                    var lblGuestId = this.Controls.Find("lblGuestId", true).FirstOrDefault();
                    var txtGuestId = this.Controls.Find("txtGuestId", true).FirstOrDefault();
                    var btnSearchGuest = this.Controls.Find("btnSearchGuest", true).FirstOrDefault();
                    var lblGuestName = this.Controls.Find("lblGuestName", true).FirstOrDefault();
                    var txtGuestName = this.Controls.Find("txtGuestName", true).FirstOrDefault();
                    var btnSearchGuestName = this.Controls.Find("btnSearchGuestName", true).FirstOrDefault();
                    var lblGuestInfo = this.Controls.Find("lblGuestInfo", true).FirstOrDefault();
                    
                    if (lblGuestId != null) lblGuestId.Visible = false;
                    if (txtGuestId != null) txtGuestId.Visible = false;
                    if (btnSearchGuest != null) btnSearchGuest.Visible = false;
                    if (lblGuestName != null) lblGuestName.Visible = false;
                    if (txtGuestName != null) txtGuestName.Visible = false;
                    if (btnSearchGuestName != null) btnSearchGuestName.Visible = false;
                    
                    // Update info message
                    if (lblGuestInfo != null) lblGuestInfo.Text = "Emergency mode: EMG Code will be auto-generated";
                }
                else
                {
                    // Non-emergency mode: Show Guest ID search, hide EMG Code
                    if (lblEmergencyCode != null && txtEmergencyCode != null)
                    {
                        lblEmergencyCode.Visible = false;
                        txtEmergencyCode.Visible = false;
                        txtEmergencyCode.Text = ""; // Clear EMG Code
                    }
                    
                    // Show Guest ID search controls
                    var lblGuestId = this.Controls.Find("lblGuestId", true).FirstOrDefault();
                    var txtGuestId = this.Controls.Find("txtGuestId", true).FirstOrDefault();
                    var btnSearchGuest = this.Controls.Find("btnSearchGuest", true).FirstOrDefault();
                    var lblGuestName = this.Controls.Find("lblGuestName", true).FirstOrDefault();
                    var txtGuestName = this.Controls.Find("txtGuestName", true).FirstOrDefault();
                    var btnSearchGuestName = this.Controls.Find("btnSearchGuestName", true).FirstOrDefault();
                    var lblGuestInfo = this.Controls.Find("lblGuestInfo", true).FirstOrDefault();
                    
                    if (lblGuestId != null) lblGuestId.Visible = true;
                    if (txtGuestId != null) txtGuestId.Visible = true;
                    if (btnSearchGuest != null) btnSearchGuest.Visible = true;
                    if (lblGuestName != null) lblGuestName.Visible = true;
                    if (txtGuestName != null) txtGuestName.Visible = true;
                    if (btnSearchGuestName != null) btnSearchGuestName.Visible = true;
                    
                    // Update info message
                    if (lblGuestInfo != null) lblGuestInfo.Text = "Enter Guest ID or Name to search for existing guest, or leave empty for new guest";
                    
                    // Generate Guest ID if name is available
                    if (!string.IsNullOrWhiteSpace(txtFullName.Text))
                    {
                        GenerateGuestId();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in Emergency Status change: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbPatientType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                isStudentSelected = cmbPatientType.SelectedIndex == 1; // Student (Emergency)
                
                // FIRST: CLEAR ALL FIELDS - This is the most important part!
                // Clear all text fields (with null checks)
                if (txtStudentId != null) txtStudentId.Text = "";
                if (txtFullName != null) txtFullName.Text = "";
                if (txtPhone != null) txtPhone.Text = "";
                if (txtEmergencyNumber != null) txtEmergencyNumber.Text = "";
                if (txtEmergencyCode != null) txtEmergencyCode.Text = "";
                if (txtEmergencyContact != null) txtEmergencyContact.Text = "";
                if (txtEmergencyPhone != null) txtEmergencyPhone.Text = "";
                if (txtAddress != null) txtAddress.Text = "";
                if (txtReason != null) txtReason.Text = "";
                // txtNotes removed - not supported by database schema
                if (txtGuestId != null) txtGuestId.Text = "";
                if (txtGuestName != null) txtGuestName.Text = "";
                if (lblGuestInfo != null) lblGuestInfo.Text = "";
                
                // Clear student info label
                var lblStudentInfoClear = this.Controls.Find("lblStudentInfo", true).FirstOrDefault() as Label;
                if (lblStudentInfoClear != null) lblStudentInfoClear.Text = "";
                
                // Reset selected student and existing guest ID
                selectedStudent = null;
                existingGuestId = null;
                
                // Reset field colors
                ResetFieldColors();

            // Show/hide student validation controls
            var lblStudentId = this.Controls.Find("lblStudentId", true).FirstOrDefault();
            var txtStudentIdControl = this.Controls.Find("txtStudentId", true).FirstOrDefault();
            var btnValidateStudent = this.Controls.Find("btnValidateStudent", true).FirstOrDefault();
            var lblStudentInfo = this.Controls.Find("lblStudentInfo", true).FirstOrDefault();

            if (isStudentSelected)
            {
                // Show student validation controls
                if (lblStudentId != null) lblStudentId.Visible = true;
                if (txtStudentIdControl != null) txtStudentIdControl.Visible = true;
                if (btnValidateStudent != null) btnValidateStudent.Visible = true;
                if (lblStudentInfo != null) lblStudentInfo.Visible = true;

                // Hide Guest search controls
                lblGuestId.Visible = false;
                txtGuestId.Visible = false;
                btnSearchGuest.Visible = false;
                lblGuestName.Visible = false;
                txtGuestName.Visible = false;
                btnSearchGuestName.Visible = false;
                lblGuestInfo.Visible = false;

                // Show Emergency Contact text field for students
                txtEmergencyContact.Visible = true;

                // Update label for Emergency Phone field
                var lblEmergencyPhone = this.Controls.Find("lblEmergencyPhone", true).FirstOrDefault();
                if (lblEmergencyPhone != null) lblEmergencyPhone.Text = "EmergencyNum *";

                // Clear student data (already done in ClearAllFields but ensure it's null)
                selectedStudent = null;
            }
            else
            {
                // Hide student validation controls
                if (lblStudentId != null) lblStudentId.Visible = false;
                if (txtStudentIdControl != null) txtStudentIdControl.Visible = false;
                if (btnValidateStudent != null) btnValidateStudent.Visible = false;
                if (lblStudentInfo != null) lblStudentInfo.Visible = false;

                // Show Guest search controls (with null checks)
                if (lblGuestId != null) lblGuestId.Visible = true;
                if (txtGuestId != null) txtGuestId.Visible = true;
                if (btnSearchGuest != null) btnSearchGuest.Visible = true;
                if (lblGuestName != null) lblGuestName.Visible = true;
                if (txtGuestName != null) txtGuestName.Visible = true;
                if (btnSearchGuestName != null) btnSearchGuestName.Visible = true;
                if (lblGuestInfo != null) lblGuestInfo.Visible = true;

                // Hide Emergency Contact text field for guests
                if (txtEmergencyContact != null) txtEmergencyContact.Visible = false;

                // Update label for Emergency Phone field (will show Guest ID)
                var lblEmergencyPhone = this.Controls.Find("lblEmergencyPhone", true).FirstOrDefault();
                if (lblEmergencyPhone != null) lblEmergencyPhone.Text = "Guest ID *";

                // Clear student data (already done in ClearAllFields but ensure it's null)
                selectedStudent = null;
                
                // Set initial guest info message
                if (lblGuestInfo != null) lblGuestInfo.Text = "Enter Guest ID or Name to search for existing guest, or leave empty for new guest";
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error in CmbPatientType_SelectedIndexChanged: {ex.Message}\n\nStack Trace: {ex.StackTrace}", 
                    "Dropdown Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }


        private void ClearAllFields()
        {
            // FORCE CLEAR ALL TEXT FIELDS - use direct control references
            txtStudentId.Text = "";
            lblStudentInfo.Text = "";
            txtFullName.Text = "";
            txtPhone.Text = "";
            txtEmergencyContact.Text = "";
            txtEmergencyPhone.Text = "";
            txtAddress.Text = "";
            txtReason.Text = "";
            
            // Clear Guest search fields
            txtGuestId.Text = "";
            txtGuestName.Text = "";
            lblGuestInfo.Text = "";

            // Reset date/time pickers to current date/time
            dtpAppointmentDate.Value = DateTime.Now.Date;
            dtpAppointmentTime.Value = DateTime.Now;

            // Clear student and guest data
            selectedStudent = null;
            existingGuestId = null;
            
            // Reset field colors
            ResetFieldColors();
        }

        private void TxtStudentId_TextChanged(object sender, EventArgs e)
        {
            // Clear validation when student ID changes
            var lblStudentInfo = this.Controls.Find("lblStudentInfo", true).FirstOrDefault();
            if (lblStudentInfo != null)
            {
                lblStudentInfo.Text = "";
                lblStudentInfo.ForeColor = Color.FromArgb(40, 167, 69);
            }
            selectedStudent = null;
        }

        private void BtnValidateStudent_Click(object sender, EventArgs e)
        {
            ValidateStudent();
        }

        private void BtnSearchGuest_Click(object sender, EventArgs e)
        {
            SearchGuestById();
        }

        private void BtnSearchGuestName_Click(object sender, EventArgs e)
        {
            SearchGuestByName();
        }

        // ENTER key event handlers for triggering search/validate buttons
        private void TxtStudentId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound
                BtnValidateStudent_Click(btnValidateStudent, EventArgs.Empty);
            }
        }

        private void TxtGuestId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound
                BtnSearchGuest_Click(btnSearchGuest, EventArgs.Empty);
            }
        }

        private void TxtGuestName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true; // Prevent the beep sound
                BtnSearchGuestName_Click(btnSearchGuestName, EventArgs.Empty);
            }
        }

        private void SearchGuestById()
        {
            try
            {
                string guestId = txtGuestId.Text.Trim();
                if (string.IsNullOrWhiteSpace(guestId))
                {
                    lblGuestInfo.Text = "Please enter a Guest ID to search";
                    lblGuestInfo.ForeColor = Color.Red;
                    return;
                }

                string sql = @"
                    SELECT 
                        g.GuestPatientId,
                        g.FullName,
                        g.PhoneNumber,
                        g.EmergencyContact,
                        g.EmergencyPhone,
                        g.EmergencyNumber,
                        g.Address,
                        COUNT(a.AppointmentId) as VisitCount
                    FROM GuestPatients g
                    LEFT JOIN Appointments a ON g.GuestPatientId = a.GuestPatientId
                    WHERE g.EmergencyPhone = @GuestId 
                    AND g.IsActive = 1
                    GROUP BY g.GuestPatientId, g.FullName, g.PhoneNumber, g.EmergencyContact, g.EmergencyPhone, g.EmergencyNumber, g.Address";

                var result = dbHelper.ExecuteQuery(sql, new { GuestId = guestId });

                if (result.Rows.Count > 0)
                {
                    var row = result.Rows[0];
                    var guestInfo = new ExistingGuestInfo
                    {
                        GuestPatientId = Convert.ToInt32(row["GuestPatientId"]),
                        FullName = row["FullName"].ToString(),
                        PhoneNumber = row["PhoneNumber"].ToString(),
                        EmergencyContact = row["EmergencyContact"]?.ToString() ?? "",
                        EmergencyPhone = row["EmergencyPhone"]?.ToString() ?? "",
                        EmergencyNumber = row["EmergencyNumber"]?.ToString() ?? "",
                        Address = row["Address"]?.ToString(),
                        VisitCount = Convert.ToInt32(row["VisitCount"])
                    };

                    // Auto-fill guest data
                    AutoFillGuestData(guestInfo);
                    
                    lblGuestInfo.Text = $"✓ Guest found: {guestInfo.FullName} (Guest ID: GP{guestInfo.GuestPatientId:D3}, Visits: {guestInfo.VisitCount})";
                    lblGuestInfo.ForeColor = Color.FromArgb(40, 167, 69);
                }
                else
                {
                    lblGuestInfo.Text = "✗ Guest not found with this ID";
                    lblGuestInfo.ForeColor = Color.Red;
                    existingGuestId = null;
                }
            }
            catch (Exception ex)
            {
                lblGuestInfo.Text = $"Error searching guest: {ex.Message}";
                lblGuestInfo.ForeColor = Color.Red;
            }
        }

        private void SearchGuestByName()
        {
            try
            {
                string guestName = txtGuestName.Text.Trim();
                if (string.IsNullOrWhiteSpace(guestName))
                {
                    lblGuestInfo.Text = "Please enter a Guest Name to search";
                    lblGuestInfo.ForeColor = Color.Red;
                    return;
                }

                string sql = @"
                    SELECT 
                        g.GuestPatientId,
                        g.FullName,
                        g.PhoneNumber,
                        g.EmergencyContact,
                        g.EmergencyPhone,
                        g.EmergencyNumber,
                        g.Address,
                        COUNT(a.AppointmentId) as VisitCount
                    FROM GuestPatients g
                    LEFT JOIN Appointments a ON g.GuestPatientId = a.GuestPatientId
                    WHERE g.FullName LIKE @GuestName 
                    AND g.IsActive = 1
                    GROUP BY g.GuestPatientId, g.FullName, g.PhoneNumber, g.EmergencyContact, g.EmergencyPhone, g.EmergencyNumber, g.Address
                    ORDER BY COUNT(a.AppointmentId) DESC";

                var result = dbHelper.ExecuteQuery(sql, new { GuestName = $"%{guestName}%" });

                if (result.Rows.Count > 0)
                {
                    if (result.Rows.Count == 1)
                    {
                        // Single match - auto-fill
                        var row = result.Rows[0];
                        var guestInfo = new ExistingGuestInfo
                        {
                            GuestPatientId = Convert.ToInt32(row["GuestPatientId"]),
                            FullName = row["FullName"].ToString(),
                            PhoneNumber = row["PhoneNumber"].ToString(),
                            EmergencyContact = row["EmergencyContact"]?.ToString() ?? "",
                            EmergencyPhone = row["EmergencyPhone"]?.ToString() ?? "",
                            EmergencyNumber = row["EmergencyNumber"]?.ToString() ?? "",
                            Address = row["Address"]?.ToString(),
                            VisitCount = Convert.ToInt32(row["VisitCount"])
                        };

                        AutoFillGuestData(guestInfo);
                        
                        lblGuestInfo.Text = $"✓ Guest found: {guestInfo.FullName} (Guest ID: GP{guestInfo.GuestPatientId:D3}, Visits: {guestInfo.VisitCount})";
                        lblGuestInfo.ForeColor = Color.FromArgb(40, 167, 69);
                    }
                    else
                    {
                        // Multiple matches - show list
                        ShowGuestSelectionDialog(result);
                    }
                }
                else
                {
                    lblGuestInfo.Text = "✗ No guests found with this name";
                    lblGuestInfo.ForeColor = Color.Red;
                    existingGuestId = null;
                }
            }
            catch (Exception ex)
            {
                lblGuestInfo.Text = $"Error searching guest: {ex.Message}";
                lblGuestInfo.ForeColor = Color.Red;
            }
        }

        private void AutoFillGuestData(ExistingGuestInfo guestInfo)
        {
            // Fill basic information
            txtFullName.Text = guestInfo.FullName;
            txtPhone.Text = guestInfo.PhoneNumber;
            txtAddress.Text = guestInfo.Address ?? "";
            
            // Fill emergency number (the phone number to call in emergency)
            txtEmergencyNumber.Text = !string.IsNullOrWhiteSpace(guestInfo.EmergencyNumber) 
                ? guestInfo.EmergencyNumber 
                : guestInfo.EmergencyPhone; // Fallback to EmergencyPhone if EmergencyNumber is empty
            
            // Fill emergency contact name if available
            txtEmergencyContact.Text = guestInfo.EmergencyContact ?? "";
            
            // SYNC hidden EmergencyPhone field with visible EmergencyNumber field for validation
            txtEmergencyPhone.Text = txtEmergencyNumber.Text;
            
            // Store the guest ID
            existingGuestId = guestInfo.GuestPatientId;
            
            // ALWAYS check and auto-fill emergency code for existing guests
            CheckAndFillExistingEmergencyCode();
        }

        /// <summary>
        /// Checks database for existing emergency code for today's date for the current guest.
        /// If found, auto-fills the emergency code field. If not found, generates a new one.
        /// This ensures consistent emergency tracking for returning guests.
        /// </summary>
        private void CheckAndFillExistingEmergencyCode()
        {
            try
            {
                // Only proceed if we have an existing guest ID
                if (existingGuestId == null)
                {
                    return;
                }

                // Query to check for existing emergency code for this guest today
                string checkSql = @"
                    SELECT TOP 1 EmergencyCode
                    FROM Appointments
                    WHERE GuestPatientId = @GuestPatientId
                      AND EmergencyCode IS NOT NULL
                      AND CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)
                    ORDER BY AppointmentId DESC";

                var result = dbHelper.ExecuteQuery(checkSql, new { GuestPatientId = existingGuestId });

                if (result.Rows.Count > 0 && result.Rows[0]["EmergencyCode"] != DBNull.Value)
                {
                    // Existing emergency code found for today - reuse it
                    string existingCode = result.Rows[0]["EmergencyCode"].ToString();
                    txtEmergencyCode.Text = existingCode;
                }
                else
                {
                    // No existing emergency code for today - generate a new one
                    GenerateNewEmergencyCodeForGuest();
                }
            }
            catch (Exception ex)
            {
                // Handle error gracefully - don't break the auto-fill process
                System.Diagnostics.Debug.WriteLine($"Error checking existing emergency code: {ex.Message}");
                // Fall back to generating a new code
                GenerateNewEmergencyCodeForGuest();
            }
        }

        /// <summary>
        /// Generates a new emergency code for an existing guest.
        /// Format: EMG{INITIALS}{SEQUENCE}{DATE}
        /// </summary>
        private void GenerateNewEmergencyCodeForGuest()
        {
            try
            {
                if (existingGuestId == null || string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    return;
                }

                // Get initials from guest name
                string[] nameParts = txtFullName.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string initials = "";
                foreach (string part in nameParts)
                {
                    if (!string.IsNullOrEmpty(part))
                    {
                        initials += part[0].ToString().ToUpper();
                    }
                }
                if (initials.Length > 3) initials = initials.Substring(0, 3);

                // Count existing emergency codes for this guest today to get sequence number
                string countSql = @"
                    SELECT COUNT(*)
                    FROM Appointments
                    WHERE GuestPatientId = @GuestPatientId
                      AND EmergencyCode IS NOT NULL
                      AND CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)";

                var countResult = dbHelper.ExecuteScalar(countSql, new { GuestPatientId = existingGuestId });
                int sequenceNumber = (countResult != null ? Convert.ToInt32(countResult) : 0) + 1;

                // Generate the code: EMG{INITIALS}{SEQUENCE:000}{DATE:YYYYMMDD}
                string dateString = DateTime.Now.ToString("yyyyMMdd");
                string emergencyCode = $"EMG{initials}{sequenceNumber:D3}{dateString}";

                // Set the emergency code in the field
                txtEmergencyCode.Text = emergencyCode;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error generating new emergency code: {ex.Message}");
                // Set a fallback code
                txtEmergencyCode.Text = $"EMG{DateTime.Now:yyyyMMddHHmmss}";
            }
        }

        private void ShowGuestSelectionDialog(DataTable guests)
        {
            var guestList = new List<ExistingGuestInfo>();
            foreach (DataRow row in guests.Rows)
            {
                guestList.Add(new ExistingGuestInfo
                {
                    GuestPatientId = Convert.ToInt32(row["GuestPatientId"]),
                    FullName = row["FullName"].ToString(),
                    PhoneNumber = row["PhoneNumber"].ToString(),
                    EmergencyContact = row["EmergencyContact"]?.ToString() ?? "",
                    EmergencyPhone = row["EmergencyPhone"]?.ToString() ?? "",
                    EmergencyNumber = row["EmergencyNumber"]?.ToString() ?? "",
                    Address = row["Address"]?.ToString(),
                    VisitCount = Convert.ToInt32(row["VisitCount"])
                });
            }

            var message = "Multiple guests found:\n\n";
            for (int i = 0; i < guestList.Count; i++)
            {
                var guest = guestList[i];
                message += $"{i + 1}. {guest.FullName} (Guest ID: GP{guest.GuestPatientId:D3}, Visits: {guest.VisitCount})\n";
            }
            message += "\nPlease select a guest by entering the number (1-" + guestList.Count + "):";

            var input = Microsoft.VisualBasic.Interaction.InputBox(message, "Select Guest", "1");
            
            if (int.TryParse(input, out int selection) && selection >= 1 && selection <= guestList.Count)
            {
                var selectedGuest = guestList[selection - 1];
                AutoFillGuestData(selectedGuest);
                
                lblGuestInfo.Text = $"✓ Guest selected: {selectedGuest.FullName} (Guest ID: GP{selectedGuest.GuestPatientId:D3}, Visits: {selectedGuest.VisitCount})";
                lblGuestInfo.ForeColor = Color.FromArgb(40, 167, 69);
            }
            else
            {
                lblGuestInfo.Text = "Guest selection cancelled";
                lblGuestInfo.ForeColor = Color.Orange;
            }
        }

        private void ValidateStudent()
        {
            try
            {
                string studentId = txtStudentId.Text.Trim();
                if (string.IsNullOrWhiteSpace(studentId))
                {
                    ShowStudentInfo("Please enter a Student ID", Color.Red);
                    return;
                }

                string sql = @"
                    SELECT u.UserId, u.FullName, p.StudentId, u.ContactPhone, u.ContactEmail
                    FROM Users u
                    INNER JOIN Patients p ON u.UserId = p.UserId
                    WHERE u.RoleId = 3 AND u.IsActive = 1 AND p.StudentId = @StudentId";

                var result = dbHelper.ExecuteQuery(sql, new { StudentId = studentId });

                if (result.Rows.Count > 0)
                {
                    var row = result.Rows[0];
                    selectedStudent = new StudentInfo
                    {
                        UserId = Convert.ToInt32(row["UserId"]),
                        FullName = row["FullName"].ToString(),
                        StudentId = row["StudentId"].ToString(),
                        Phone = row["ContactPhone"]?.ToString() ?? "",
                        Email = row["ContactEmail"]?.ToString() ?? ""
                    };

                    ShowStudentInfo($"✓ Validated: {selectedStudent.FullName} ({selectedStudent.StudentId})", Color.FromArgb(40, 167, 69));
                    
                    // Auto-fill student details
                    AutoFillStudentDetails();
                }
                else
                {
                    ShowStudentInfo("✗ Student ID not found. Please check the ID or treat as Guest Patient.", Color.Red);
                    selectedStudent = null;
                }
            }
            catch (Exception ex)
            {
                ShowStudentInfo($"Error validating student: {ex.Message}", Color.Red);
                selectedStudent = null;
            }
        }

        private void AutoFillStudentDetails()
        {
            if (selectedStudent == null) return;

            // Auto-fill Full Name and Phone Number
            txtFullName.Text = selectedStudent.FullName;
            txtPhone.Text = selectedStudent.Phone;

            // Generate emergency code for student (checks previous emergencies)
            GenerateStudentEmergencyCode();
            
            // Generate emergency number from student ID and first emergency date
            GenerateEmergencyNumber();
        }

        private void GenerateEmergencyCode()
        {
            try
            {
                // Generate EMG Code for Guest Emergency: Name Initials + Emergency Count + Date
                string nameInitials = GetNameInitials();
                int emergencyCount = GetTotalEmergencyCount() + 1;
                string dateFormat = DateTime.Now.ToString("yyyyMMdd");
                
                string emergencyCode = $"EMG{nameInitials}{emergencyCount:D3}{dateFormat}";
                txtEmergencyCode.Text = emergencyCode;
            }
            catch (Exception ex)
            {
                // Fallback EMG Code
                string emergencyCode = $"EMG{DateTime.Now:yyyyMMddHHmm}";
                txtEmergencyCode.Text = emergencyCode;
            }
        }

        private void GenerateGuestEmergencyCode()
        {
            if (existingGuestId == null)
            {
                // If no existing guest, just generate a new one
                GenerateEmergencyCode();
                return;
            }

            try
            {
                // Step 1: Check if this guest already has an emergency code for TODAY
                string checkSql = @"
                    SELECT TOP 1 EmergencyCode
                    FROM Appointments
                    WHERE GuestPatientId = @GuestPatientId
                      AND EmergencyCode IS NOT NULL
                      AND CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)
                    ORDER BY AppointmentId DESC";

                var existingCode = dbHelper.ExecuteScalar(checkSql, new { GuestPatientId = existingGuestId.Value });

                if (existingCode != null && !string.IsNullOrWhiteSpace(existingCode.ToString()))
                {
                    // Use the existing emergency code
                    txtEmergencyCode.Text = existingCode.ToString();
                    return;
                }

                // Step 2: No existing code for today, so generate a new one
                // Count previous emergencies for this guest on TODAY
                string countSql = @"
                    SELECT COUNT(*) 
                    FROM Appointments 
                    WHERE GuestPatientId = @GuestPatientId 
                      AND EmergencyCode IS NOT NULL 
                      AND CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)";

                var countResult = dbHelper.ExecuteScalar(countSql, new { GuestPatientId = existingGuestId.Value });
                int todayEmergencyCount = Convert.ToInt32(countResult);

                // Step 3: Generate new emergency code
                string nameInitials = GetNameInitials();
                string datePart = DateTime.Now.ToString("yyyyMMdd");
                string sequencePart = (todayEmergencyCount + 1).ToString("D3"); // 001, 002, etc.

                string emergencyCode = $"EMG{nameInitials}{sequencePart}{datePart}";
                
                // Step 4: Set it in the Emergency Code field
                txtEmergencyCode.Text = emergencyCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating guest emergency code: {ex.Message}", 
                    "Code Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                // Fallback code on error
                txtEmergencyCode.Text = $"EMG{GetNameInitials()}ERR{DateTime.Now:yyyyMMdd}";
            }
        }

        private void GenerateGuestId()
        {
            try
            {
                // Generate Guest ID: Name Initials + Appointment Number + Time
                string nameInitials = GetNameInitials();
                int appointmentNumber = GetNextAppointmentNumber();
                string timeFormat = DateTime.Now.ToString("HHmm");
                
                string guestId = $"{nameInitials}{appointmentNumber:D3}{timeFormat}";
                txtEmergencyPhone.Text = guestId; // Use emergency phone field for Guest ID
            }
            catch (Exception ex)
            {
                // Fallback Guest ID
                string guestId = $"GUEST{DateTime.Now:yyyyMMddHHmm}";
                txtEmergencyPhone.Text = guestId;
            }
        }

        private string GetNameInitials()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                    return "XX"; // Default if no name
                
                var nameParts = txtFullName.Text.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                string initials = "";
                
                foreach (var part in nameParts.Take(2)) // Take first 2 name parts
                {
                    initials += part[0].ToString().ToUpper();
                }
                
                return initials.PadRight(2, 'X'); // Ensure at least 2 characters
            }
            catch
            {
                return "XX";
            }
        }

        private int GetNextAppointmentNumber()
        {
            try
            {
                // Get count of appointments for today + 1
                string sql = @"
                    SELECT COUNT(*) 
                    FROM Appointments 
                    WHERE CAST(CreatedDateTime AS DATE) = CAST(GETDATE() AS DATE)";
                
                var result = dbHelper.ExecuteScalar(sql);
                return Convert.ToInt32(result) + 1;
            }
            catch
            {
                return 1; // Default to 1 if error
            }
        }

        private int GetTotalEmergencyCount()
        {
            try
            {
                // Get total count of all emergency appointments ever recorded
                string sql = @"
                    SELECT COUNT(*) 
                    FROM Appointments a
                    LEFT JOIN GuestPatients g ON a.GuestPatientId = g.GuestPatientId
                    WHERE a.Status = 'Confirmed' OR g.EmergencyPhone LIKE '%EMG%'";
                
                var result = dbHelper.ExecuteScalar(sql);
                return Convert.ToInt32(result);
            }
            catch
            {
                return 0; // Default to 0 if error
            }
        }

        private void GenerateEmergencyNumber()
        {
            try
            {
                // Get the first emergency appointment date for this student
                string emergencySql = @"
                    SELECT TOP 1 a.CreatedDateTime
                    FROM Appointments a
                    INNER JOIN Patients p ON a.PatientId = p.PatientId
                    WHERE p.UserId = @UserId AND a.Status = 'Confirmed'
                    ORDER BY a.CreatedDateTime ASC";

                var emergencyResult = dbHelper.ExecuteQuery(emergencySql, new { UserId = selectedStudent.UserId });

                DateTime firstEmergencyDate;
                if (emergencyResult.Rows.Count > 0)
                {
                    firstEmergencyDate = Convert.ToDateTime(emergencyResult.Rows[0]["CreatedDateTime"]);
                }
                else
                {
                    // If no previous emergency, use current date
                    firstEmergencyDate = DateTime.Now;
                }

                // Generate emergency number: StudentID + YYYYMMDD format
                string emergencyNumber = $"{selectedStudent.StudentId}{firstEmergencyDate:yyyyMMdd}";
                
                // Fill the emergency phone field
                txtEmergencyPhone.Text = emergencyNumber;
            }
            catch (Exception ex)
            {
                // If error generating emergency number, use a default format
                string emergencyNumber = $"{selectedStudent.StudentId}{DateTime.Now:yyyyMMdd}";
                txtEmergencyPhone.Text = emergencyNumber;
            }
        }

        private void GenerateStudentEmergencyCode()
        {
            if (selectedStudent == null)
            {
                txtEmergencyCode.Text = "";
                return;
            }

            try
            {
                // Step 1: Get the student's PatientId
                string patientSql = "SELECT PatientId FROM Patients WHERE StudentId = @StudentId";
                var patientIdResult = dbHelper.ExecuteScalar(patientSql, new { StudentId = selectedStudent.StudentId });

                if (patientIdResult == null)
                {
                    txtEmergencyCode.Text = "";
                    return;
                }

                int patientId = Convert.ToInt32(patientIdResult);

                // Step 2: Count previous emergencies for this student on TODAY
                string countSql = @"
                    SELECT COUNT(*) 
                    FROM Appointments 
                    WHERE PatientId = @PatientId 
                      AND EmergencyCode IS NOT NULL 
                      AND CAST(AppointmentDate AS DATE) = CAST(GETDATE() AS DATE)";

                var countResult = dbHelper.ExecuteScalar(countSql, new { PatientId = patientId });
                int todayEmergencyCount = Convert.ToInt32(countResult);

                // Step 3: Generate new emergency code
                string studentId = selectedStudent.StudentId;
                string datePart = DateTime.Now.ToString("yyyyMMdd");
                string sequencePart = (todayEmergencyCount + 1).ToString("D3"); // 001, 002, etc.

                string emergencyCode = $"{studentId}-EMG-{datePart}-{sequencePart}";

                // Step 4: Set it in the Emergency Code field
                txtEmergencyCode.Text = emergencyCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error generating emergency code: {ex.Message}", 
                    "Code Generation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                // Fallback code on error
                if (selectedStudent != null)
                {
                    txtEmergencyCode.Text = $"{selectedStudent.StudentId}-EMG-ERR";
                }
                else
                {
                    txtEmergencyCode.Text = "EMG-ERR";
                }
            }
        }


        private void ShowStudentInfo(string message, Color color)
        {
            var lblStudentInfo = this.Controls.Find("lblStudentInfo", true).FirstOrDefault();
            if (lblStudentInfo != null)
            {
                lblStudentInfo.Text = message;
                lblStudentInfo.ForeColor = color;
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            try
            {
                if (isStudentSelected)
                {
                    // Create emergency appointment for selected student
                    if (selectedStudent == null)
                    {
                        MessageBox.Show("Please select a student from the search results.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    CreateEmergencyAppointment();
                }
                else
                {
                    // Create guest patient record
                    CreateGuestPatient();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving patient: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateGuestPatient()
        {
            try
            {
                int guestPatientId;
                
                if (existingGuestId.HasValue)
                {
                    // Use existing guest
                    guestPatientId = existingGuestId.Value;
                    
                    // Update existing guest information if needed
                    string updateSql = @"
                        UPDATE GuestPatients 
                        SET PhoneNumber = @PhoneNumber, 
                            Address = @Address
                        WHERE GuestPatientId = @GuestPatientId";

                    dbHelper.ExecuteNonQuery(updateSql, new
                    {
                        PhoneNumber = txtPhone.Text.Trim(),
                        Address = txtAddress.Text.Trim(),
                        GuestPatientId = guestPatientId
                    });
                }
                else
                {
                    // Create new guest patient
                    string guestSql = @"
                        INSERT INTO GuestPatients (FullName, PhoneNumber, EmergencyContact, EmergencyPhone, Address, CreatedBy, CreatedDate, IsActive)
                        VALUES (@FullName, @PhoneNumber, @EmergencyContact, @EmergencyPhone, @Address, @CreatedBy, @CreatedDate, @IsActive);
                        SELECT SCOPE_IDENTITY();";

                    var result = dbHelper.ExecuteScalar(guestSql, new
                    {
                        FullName = txtFullName.Text.Trim(),
                        PhoneNumber = txtPhone.Text.Trim(),
                        EmergencyContact = string.IsNullOrWhiteSpace(txtEmergencyContact.Text) ? null : txtEmergencyContact.Text.Trim(),
                        EmergencyPhone = string.IsNullOrWhiteSpace(txtEmergencyPhone.Text) ? null : txtEmergencyPhone.Text.Trim(),
                        Address = string.IsNullOrWhiteSpace(txtAddress.Text) ? null : txtAddress.Text.Trim(),
                        CreatedBy = _currentUser.UserID,
                        CreatedDate = DateTime.Now,
                        IsActive = true
                    });

                    if (result == null)
                    {
                        MessageBox.Show("Failed to create guest patient record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    guestPatientId = Convert.ToInt32(result);
                }

                // Create appointment for the guest
                CreateAppointmentForGuest(guestPatientId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating guest patient: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateAppointmentForGuest(int guestPatientId)
        {
            DateTime appointmentDateTime = dtpAppointmentDate.Value.Date.Add(dtpAppointmentTime.Value.TimeOfDay);

            // Generate emergency code for guest if not already filled
            if (string.IsNullOrWhiteSpace(txtEmergencyCode.Text) && cmbEmergencyStatus.SelectedIndex == 1)
            {
                GenerateEmergencyCode(); // Use the guest emergency code generator
            }

            string appointmentSql = @"
                INSERT INTO Appointments (GuestPatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime, EmergencyCode)
                VALUES (@GuestPatientId, @ProviderId, @AppointmentDate, @TimeSlot, @Reason, @Status, @CreatedDateTime, @EmergencyCode);
                SELECT SCOPE_IDENTITY();";

            var appointmentId = dbHelper.ExecuteScalar(appointmentSql, new
            {
                GuestPatientId = guestPatientId,
                ProviderId = _currentUser.UserID,
                AppointmentDate = appointmentDateTime.Date,
                TimeSlot = appointmentDateTime.TimeOfDay,
                Reason = txtReason.Text.Trim(),
                Status = "Scheduled", // guests must be Scheduled initially
                CreatedDateTime = DateTime.Now,
                EmergencyCode = string.IsNullOrWhiteSpace(txtEmergencyCode.Text) ? null : txtEmergencyCode.Text.Trim()
            });

            MessageBox.Show($"Guest patient '{txtFullName.Text}' and appointment created successfully!\n\nGuest ID: {guestPatientId}\nAppointment ID: {appointmentId}",
                "Guest Patient Added", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void CreateEmergencyAppointment()
        {
            if (selectedStudent == null)
            {
                MessageBox.Show("Please validate the student ID first.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get PatientId for the selected student
            string patientSql = "SELECT PatientId FROM Patients WHERE UserId = @UserId";
            var patientId = dbHelper.ExecuteScalar(patientSql, new { UserId = selectedStudent.UserId });

            if (patientId == null)
            {
                MessageBox.Show("Patient record not found for the selected student.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Create emergency appointment with all collected information
            CreateAppointmentForStudent(Convert.ToInt32(patientId));
        }

        private void CreateAppointmentForStudent(int patientId)
        {
            // Combine appointment date and time
            DateTime appointmentDateTime = dtpAppointmentDate.Value.Date.Add(dtpAppointmentTime.Value.TimeOfDay);

            string appointmentSql = @"
                INSERT INTO Appointments (PatientId, ProviderId, AppointmentDate, TimeSlot, Reason, Status, CreatedDateTime, EmergencyCode)
                VALUES (@PatientId, @ProviderId, @AppointmentDate, @TimeSlot, @Reason, @Status, @CreatedDateTime, @EmergencyCode);
                SELECT SCOPE_IDENTITY();";

            var appointmentId = dbHelper.ExecuteScalar(appointmentSql, new
            {
                PatientId = patientId,
                ProviderId = _currentUser.UserID,
                AppointmentDate = appointmentDateTime.Date,
                TimeSlot = appointmentDateTime.TimeOfDay,
                Reason = txtReason.Text.Trim(),
                Status = "Scheduled", // Emergency appointments start as "Scheduled"
                CreatedDateTime = DateTime.Now,
                EmergencyCode = string.IsNullOrWhiteSpace(txtEmergencyCode.Text) ? null : txtEmergencyCode.Text.Trim()
            });

            MessageBox.Show($"Emergency appointment created successfully!\n\nStudent: {selectedStudent.FullName} ({selectedStudent.StudentId})\nAppointment ID: {appointmentId}\nStatus: Confirmed", 
                "Emergency Appointment Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private bool ValidateInput()
        {
            // Clear any previous highlighting
            ResetFieldColors();

            bool isValid = true;
            string errorMessage = "Please correct the following errors:\n\n";

            if (isStudentSelected)
            {
                // Validate student ID
                if (string.IsNullOrWhiteSpace(txtStudentId.Text))
                {
                    HighlightField(txtStudentId);
                    errorMessage += "• Student ID is required\n";
                    isValid = false;
                }
                else if (selectedStudent == null)
                {
                    HighlightField(txtStudentId);
                    errorMessage += "• Please validate the Student ID first\n";
                    isValid = false;
                }

                // Validate reason for visit
                if (string.IsNullOrWhiteSpace(txtReason.Text))
                {
                    HighlightField(txtReason);
                    errorMessage += "• Reason for visit is required\n";
                    isValid = false;
                }
                else if (txtReason.Text.Trim().Length < 5)
                {
                    HighlightField(txtReason);
                    errorMessage += "• Reason for visit must be at least 5 characters\n";
                    isValid = false;
                }
            }
            else
            {
                // Validate Full Name
                if (string.IsNullOrWhiteSpace(txtFullName.Text))
                {
                    HighlightField(txtFullName);
                    errorMessage += "• Full Name is required\n";
                    isValid = false;
                }
                else if (txtFullName.Text.Trim().Length < 2)
                {
                    HighlightField(txtFullName);
                    errorMessage += "• Full Name must be at least 2 characters\n";
                    isValid = false;
                }

                // Validate Phone Number
                if (string.IsNullOrWhiteSpace(txtPhone.Text))
                {
                    HighlightField(txtPhone);
                    errorMessage += "• Phone Number is required\n";
                    isValid = false;
                }
                else if (txtPhone.Text.Trim().Length < 8)
                {
                    HighlightField(txtPhone);
                    errorMessage += "• Phone Number must be at least 8 digits\n";
                    isValid = false;
                }

                // Validate Emergency Phone if Emergency Contact is provided
                if (!string.IsNullOrWhiteSpace(txtEmergencyContact.Text) && string.IsNullOrWhiteSpace(txtEmergencyPhone.Text))
                {
                    HighlightField(txtEmergencyPhone);
                    errorMessage += "• Emergency Phone is required when Emergency Contact is provided\n";
                    isValid = false;
                }
            }

            if (!isValid)
            {
                MessageBox.Show(errorMessage, "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return isValid;
        }

        private void HighlightField(TextBox textBox)
        {
            textBox.BackColor = Color.FromArgb(255, 235, 235);
            textBox.BorderStyle = BorderStyle.FixedSingle;
        }

        private void ResetFieldColors()
        {
            if (txtStudentId != null) txtStudentId.BackColor = Color.White;
            if (txtFullName != null) txtFullName.BackColor = Color.White;
            if (txtPhone != null) txtPhone.BackColor = Color.White;
            if (txtEmergencyNumber != null) txtEmergencyNumber.BackColor = Color.White;
            if (txtEmergencyCode != null) txtEmergencyCode.BackColor = Color.FromArgb(240, 240, 240); // Keep read-only background
            if (txtEmergencyContact != null) txtEmergencyContact.BackColor = Color.White;
            if (txtEmergencyPhone != null) txtEmergencyPhone.BackColor = Color.White;
            if (txtAddress != null) txtAddress.BackColor = Color.White;
            if (txtReason != null) txtReason.BackColor = Color.White;
            // txtNotes removed - not supported by database schema
        }

        private int? existingGuestId; // For tracking existing guest patients
    }

    // Helper class for existing guest information
    public class ExistingGuestInfo
    {
        public int GuestPatientId { get; set; }
        public string FullName { get; set; } = "";
        public string PhoneNumber { get; set; } = "";
        public string EmergencyContact { get; set; } = ""; // Name of emergency contact
        public string EmergencyPhone { get; set; } = ""; // Phone number of emergency contact
        public string EmergencyNumber { get; set; } = ""; // Phone number to call in emergency
        public string Address { get; set; } = "";
        public int VisitCount { get; set; }
    }

    // Helper class for student information
    public class StudentInfo
    {
        public int UserId { get; set; }
        public string FullName { get; set; } = "";
        public string StudentId { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";

        public override string ToString()
        {
            return $"{FullName} ({StudentId}) - {Phone}";
        }
    }
}