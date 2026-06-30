namespace BothoClinic
{
    partial class frmConsultation
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
            this.ClientSize = new System.Drawing.Size(900, 700);
            this.Text = "Patient Consultation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Patient Info Label
            this.lblPatientInfo = new System.Windows.Forms.Label();
            this.lblPatientInfo.AutoSize = true;
            this.lblPatientInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblPatientInfo.Location = new System.Drawing.Point(12, 12);
            this.lblPatientInfo.Name = "lblPatientInfo";
            this.lblPatientInfo.Size = new System.Drawing.Size(100, 21);
            this.lblPatientInfo.TabIndex = 0;
            this.lblPatientInfo.Text = "Patient: ";
            this.Controls.Add(this.lblPatientInfo);

            // TabControl
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabControl.Location = new System.Drawing.Point(12, 40);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(876, 620);
            this.tabControl.TabIndex = 1;
            this.Controls.Add(this.tabControl);

            // ==================== VITALS & NOTES TAB ====================
            this.tpVitalsNotes = new System.Windows.Forms.TabPage();
            this.tpVitalsNotes.Text = "Vitals & Notes";
            this.tpVitalsNotes.Padding = new System.Windows.Forms.Padding(10);
            this.tabControl.Controls.Add(this.tpVitalsNotes);

            // Temperature
            this.lblTemperature = new System.Windows.Forms.Label();
            this.lblTemperature.AutoSize = true;
            this.lblTemperature.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTemperature.Location = new System.Drawing.Point(10, 15);
            this.lblTemperature.Name = "lblTemperature";
            this.lblTemperature.Size = new System.Drawing.Size(100, 19);
            this.lblTemperature.TabIndex = 0;
            this.lblTemperature.Text = "Temperature (°C):";
            this.tpVitalsNotes.Controls.Add(this.lblTemperature);

            this.txtTemperature = new System.Windows.Forms.TextBox();
            this.txtTemperature.Location = new System.Drawing.Point(10, 40);
            this.txtTemperature.Name = "txtTemperature";
            this.txtTemperature.Size = new System.Drawing.Size(150, 23);
            this.txtTemperature.TabIndex = 1;
            this.tpVitalsNotes.Controls.Add(this.txtTemperature);

            // Blood Pressure
            this.lblBloodPressure = new System.Windows.Forms.Label();
            this.lblBloodPressure.AutoSize = true;
            this.lblBloodPressure.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBloodPressure.Location = new System.Drawing.Point(180, 15);
            this.lblBloodPressure.Name = "lblBloodPressure";
            this.lblBloodPressure.Size = new System.Drawing.Size(100, 19);
            this.lblBloodPressure.TabIndex = 2;
            this.lblBloodPressure.Text = "Blood Pressure:";
            this.tpVitalsNotes.Controls.Add(this.lblBloodPressure);

            this.txtBloodPressure = new System.Windows.Forms.TextBox();
            this.txtBloodPressure.Location = new System.Drawing.Point(180, 40);
            this.txtBloodPressure.Name = "txtBloodPressure";
            this.txtBloodPressure.Size = new System.Drawing.Size(150, 23);
            this.txtBloodPressure.TabIndex = 3;
            this.txtBloodPressure.PlaceholderText = "e.g., 120/80";
            this.tpVitalsNotes.Controls.Add(this.txtBloodPressure);

            // Other Vitals
            this.lblVitals = new System.Windows.Forms.Label();
            this.lblVitals.AutoSize = true;
            this.lblVitals.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVitals.Location = new System.Drawing.Point(10, 80);
            this.lblVitals.Name = "lblVitals";
            this.lblVitals.Size = new System.Drawing.Size(100, 19);
            this.lblVitals.TabIndex = 4;
            this.lblVitals.Text = "Other Vitals:";
            this.tpVitalsNotes.Controls.Add(this.lblVitals);

            this.txtVitals = new System.Windows.Forms.TextBox();
            this.txtVitals.Location = new System.Drawing.Point(10, 105);
            this.txtVitals.Multiline = true;
            this.txtVitals.Name = "txtVitals";
            this.txtVitals.Size = new System.Drawing.Size(840, 60);
            this.txtVitals.TabIndex = 5;
            this.txtVitals.PlaceholderText = "e.g., Height: 170cm, Weight: 70kg, SpO2: 98%";
            this.tpVitalsNotes.Controls.Add(this.txtVitals);

            // Symptoms
            this.lblSymptoms = new System.Windows.Forms.Label();
            this.lblSymptoms.AutoSize = true;
            this.lblSymptoms.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSymptoms.Location = new System.Drawing.Point(10, 175);
            this.lblSymptoms.Name = "lblSymptoms";
            this.lblSymptoms.Size = new System.Drawing.Size(80, 19);
            this.lblSymptoms.TabIndex = 6;
            this.lblSymptoms.Text = "Symptoms:";
            this.tpVitalsNotes.Controls.Add(this.lblSymptoms);

            this.txtSymptoms = new System.Windows.Forms.TextBox();
            this.txtSymptoms.Location = new System.Drawing.Point(10, 200);
            this.txtSymptoms.Multiline = true;
            this.txtSymptoms.Name = "txtSymptoms";
            this.txtSymptoms.Size = new System.Drawing.Size(840, 60);
            this.txtSymptoms.TabIndex = 7;
            this.tpVitalsNotes.Controls.Add(this.txtSymptoms);

            // Diagnosis
            this.lblDiagnosis = new System.Windows.Forms.Label();
            this.lblDiagnosis.AutoSize = true;
            this.lblDiagnosis.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDiagnosis.Location = new System.Drawing.Point(10, 270);
            this.lblDiagnosis.Name = "lblDiagnosis";
            this.lblDiagnosis.Size = new System.Drawing.Size(80, 19);
            this.lblDiagnosis.TabIndex = 8;
            this.lblDiagnosis.Text = "Diagnosis:";
            this.tpVitalsNotes.Controls.Add(this.lblDiagnosis);

            this.txtDiagnosis = new System.Windows.Forms.TextBox();
            this.txtDiagnosis.Location = new System.Drawing.Point(10, 295);
            this.txtDiagnosis.Multiline = true;
            this.txtDiagnosis.Name = "txtDiagnosis";
            this.txtDiagnosis.Size = new System.Drawing.Size(840, 80);
            this.txtDiagnosis.TabIndex = 9;
            this.tpVitalsNotes.Controls.Add(this.txtDiagnosis);

            // Follow Up Checkbox
            this.chkFollowUp = new System.Windows.Forms.CheckBox();
            this.chkFollowUp.AutoSize = true;
            this.chkFollowUp.Location = new System.Drawing.Point(10, 385);
            this.chkFollowUp.Name = "chkFollowUp";
            this.chkFollowUp.Size = new System.Drawing.Size(150, 19);
            this.chkFollowUp.TabIndex = 10;
            this.chkFollowUp.Text = "Follow-up Needed";
            this.tpVitalsNotes.Controls.Add(this.chkFollowUp);

            // ==================== MEDICAL HISTORY TAB ====================
            this.tpMedicalHistory = new System.Windows.Forms.TabPage();
            this.tpMedicalHistory.Text = "Medical History";
            this.tpMedicalHistory.Padding = new System.Windows.Forms.Padding(10);
            this.tabControl.Controls.Add(this.tpMedicalHistory);

            this.dgvMedicalHistory = new System.Windows.Forms.DataGridView();
            this.dgvMedicalHistory.AllowUserToAddRows = false;
            this.dgvMedicalHistory.AllowUserToDeleteRows = false;
            this.dgvMedicalHistory.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMedicalHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMedicalHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMedicalHistory.Location = new System.Drawing.Point(10, 10);
            this.dgvMedicalHistory.Name = "dgvMedicalHistory";
            this.dgvMedicalHistory.ReadOnly = true;
            this.dgvMedicalHistory.RowTemplate.Height = 25;
            this.dgvMedicalHistory.Size = new System.Drawing.Size(840, 570);
            this.dgvMedicalHistory.TabIndex = 0;
            this.tpMedicalHistory.Controls.Add(this.dgvMedicalHistory);

            // ==================== PRESCRIPTIONS TAB ====================
            this.tpPrescriptions = new System.Windows.Forms.TabPage();
            this.tpPrescriptions.Text = "Prescriptions";
            this.tpPrescriptions.Padding = new System.Windows.Forms.Padding(10);
            this.tabControl.Controls.Add(this.tpPrescriptions);

            this.dgvPrescriptions = new System.Windows.Forms.DataGridView();
            this.dgvPrescriptions.AllowUserToAddRows = false;
            this.dgvPrescriptions.AllowUserToDeleteRows = false;
            this.dgvPrescriptions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPrescriptions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPrescriptions.Location = new System.Drawing.Point(10, 10);
            this.dgvPrescriptions.Name = "dgvPrescriptions";
            this.dgvPrescriptions.ReadOnly = true;
            this.dgvPrescriptions.RowTemplate.Height = 25;
            this.dgvPrescriptions.Size = new System.Drawing.Size(840, 200);
            this.dgvPrescriptions.TabIndex = 0;
            this.tpPrescriptions.Controls.Add(this.dgvPrescriptions);

            // Medication Selection
            this.lblMedication = new System.Windows.Forms.Label();
            this.lblMedication.AutoSize = true;
            this.lblMedication.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMedication.Location = new System.Drawing.Point(10, 220);
            this.lblMedication.Name = "lblMedication";
            this.lblMedication.Size = new System.Drawing.Size(80, 19);
            this.lblMedication.TabIndex = 1;
            this.lblMedication.Text = "Medication:";
            this.tpPrescriptions.Controls.Add(this.lblMedication);

            this.cmbMedication = new System.Windows.Forms.ComboBox();
            this.cmbMedication.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMedication.FormattingEnabled = true;
            this.cmbMedication.Location = new System.Drawing.Point(10, 245);
            this.cmbMedication.Name = "cmbMedication";
            this.cmbMedication.Size = new System.Drawing.Size(200, 23);
            this.cmbMedication.TabIndex = 2;
            this.tpPrescriptions.Controls.Add(this.cmbMedication);

            // Dosage
            this.lblDosage = new System.Windows.Forms.Label();
            this.lblDosage.AutoSize = true;
            this.lblDosage.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDosage.Location = new System.Drawing.Point(220, 220);
            this.lblDosage.Name = "lblDosage";
            this.lblDosage.Size = new System.Drawing.Size(60, 19);
            this.lblDosage.TabIndex = 3;
            this.lblDosage.Text = "Dosage:";
            this.tpPrescriptions.Controls.Add(this.lblDosage);

            this.txtDosage = new System.Windows.Forms.TextBox();
            this.txtDosage.Location = new System.Drawing.Point(220, 245);
            this.txtDosage.Name = "txtDosage";
            this.txtDosage.Size = new System.Drawing.Size(100, 23);
            this.txtDosage.TabIndex = 4;
            this.txtDosage.PlaceholderText = "e.g., 500mg";
            this.tpPrescriptions.Controls.Add(this.txtDosage);

            // Frequency
            this.lblFrequency = new System.Windows.Forms.Label();
            this.lblFrequency.AutoSize = true;
            this.lblFrequency.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblFrequency.Location = new System.Drawing.Point(330, 220);
            this.lblFrequency.Name = "lblFrequency";
            this.lblFrequency.Size = new System.Drawing.Size(80, 19);
            this.lblFrequency.TabIndex = 5;
            this.lblFrequency.Text = "Frequency:";
            this.tpPrescriptions.Controls.Add(this.lblFrequency);

            this.txtFrequency = new System.Windows.Forms.TextBox();
            this.txtFrequency.Location = new System.Drawing.Point(330, 245);
            this.txtFrequency.Name = "txtFrequency";
            this.txtFrequency.Size = new System.Drawing.Size(100, 23);
            this.txtFrequency.TabIndex = 6;
            this.txtFrequency.PlaceholderText = "e.g., 3x daily";
            this.tpPrescriptions.Controls.Add(this.txtFrequency);

            // Duration
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDuration.AutoSize = true;
            this.lblDuration.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDuration.Location = new System.Drawing.Point(440, 220);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(70, 19);
            this.lblDuration.TabIndex = 7;
            this.lblDuration.Text = "Duration:";
            this.tpPrescriptions.Controls.Add(this.lblDuration);

            this.txtDuration = new System.Windows.Forms.TextBox();
            this.txtDuration.Location = new System.Drawing.Point(440, 245);
            this.txtDuration.Name = "txtDuration";
            this.txtDuration.Size = new System.Drawing.Size(100, 23);
            this.txtDuration.TabIndex = 8;
            this.txtDuration.PlaceholderText = "e.g., 7 days";
            this.tpPrescriptions.Controls.Add(this.txtDuration);

            // Instructions
            this.lblInstructions = new System.Windows.Forms.Label();
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblInstructions.Location = new System.Drawing.Point(10, 280);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(80, 19);
            this.lblInstructions.TabIndex = 9;
            this.lblInstructions.Text = "Instructions:";
            this.tpPrescriptions.Controls.Add(this.lblInstructions);

            this.txtInstructions = new System.Windows.Forms.TextBox();
            this.txtInstructions.Location = new System.Drawing.Point(10, 305);
            this.txtInstructions.Multiline = true;
            this.txtInstructions.Name = "txtInstructions";
            this.txtInstructions.Size = new System.Drawing.Size(630, 60);
            this.txtInstructions.TabIndex = 10;
            this.tpPrescriptions.Controls.Add(this.txtInstructions);

            // Add Medication Button
            this.btnAddMedication = new System.Windows.Forms.Button();
            this.btnAddMedication.BackColor = System.Drawing.Color.Green;
            this.btnAddMedication.ForeColor = System.Drawing.Color.White;
            this.btnAddMedication.Location = new System.Drawing.Point(650, 245);
            this.btnAddMedication.Name = "btnAddMedication";
            this.btnAddMedication.Size = new System.Drawing.Size(120, 40);
            this.btnAddMedication.TabIndex = 11;
            this.btnAddMedication.Text = "✓ Add Medication";
            this.btnAddMedication.UseVisualStyleBackColor = false;
            this.btnAddMedication.Click += new System.EventHandler(this.btnAddMedication_Click);
            this.tpPrescriptions.Controls.Add(this.btnAddMedication);

            // ==================== BUTTONS ====================
            this.btnSaveConsultation = new System.Windows.Forms.Button();
            this.btnSaveConsultation.BackColor = System.Drawing.Color.Green;
            this.btnSaveConsultation.ForeColor = System.Drawing.Color.White;
            this.btnSaveConsultation.Location = new System.Drawing.Point(700, 670);
            this.btnSaveConsultation.Name = "btnSaveConsultation";
            this.btnSaveConsultation.Size = new System.Drawing.Size(100, 30);
            this.btnSaveConsultation.TabIndex = 2;
            this.btnSaveConsultation.Text = "✓ Save";
            this.btnSaveConsultation.UseVisualStyleBackColor = false;
            this.btnSaveConsultation.Click += new System.EventHandler(this.btnSaveConsultation_Click);
            this.Controls.Add(this.btnSaveConsultation);

            this.btnCancel = new System.Windows.Forms.Button();
            this.btnCancel.BackColor = System.Drawing.Color.Gray;
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(810, 670);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "✕ Cancel";
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.Controls.Add(this.btnCancel);
        }

        #endregion

        private System.Windows.Forms.Label lblPatientInfo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tpVitalsNotes;
        private System.Windows.Forms.TabPage tpMedicalHistory;
        private System.Windows.Forms.TabPage tpPrescriptions;
        private System.Windows.Forms.Label lblTemperature;
        private System.Windows.Forms.TextBox txtTemperature;
        private System.Windows.Forms.Label lblBloodPressure;
        private System.Windows.Forms.TextBox txtBloodPressure;
        private System.Windows.Forms.Label lblVitals;
        private System.Windows.Forms.TextBox txtVitals;
        private System.Windows.Forms.Label lblSymptoms;
        private System.Windows.Forms.TextBox txtSymptoms;
        private System.Windows.Forms.Label lblDiagnosis;
        private System.Windows.Forms.TextBox txtDiagnosis;
        private System.Windows.Forms.CheckBox chkFollowUp;
        private System.Windows.Forms.DataGridView dgvMedicalHistory;
        private System.Windows.Forms.DataGridView dgvPrescriptions;
        private System.Windows.Forms.Label lblMedication;
        private System.Windows.Forms.ComboBox cmbMedication;
        private System.Windows.Forms.Label lblDosage;
        private System.Windows.Forms.TextBox txtDosage;
        private System.Windows.Forms.Label lblFrequency;
        private System.Windows.Forms.TextBox txtFrequency;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.TextBox txtDuration;
        private System.Windows.Forms.Label lblInstructions;
        private System.Windows.Forms.TextBox txtInstructions;
        private System.Windows.Forms.Button btnAddMedication;
        private System.Windows.Forms.Button btnSaveConsultation;
        private System.Windows.Forms.Button btnCancel;
    }
}
