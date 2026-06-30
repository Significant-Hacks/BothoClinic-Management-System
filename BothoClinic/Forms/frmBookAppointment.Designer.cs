
namespace BothoClinic
{
    partial class frmBookAppointment
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
            this.ClientSize = new System.Drawing.Size(400, 300);
            this.Text = "Book Appointment";

            // Labels and Controls
            this.lblReason = new System.Windows.Forms.Label();
            this.lblReason.Text = "Reason for Appointment";
            this.lblReason.Location = new System.Drawing.Point(12, 15);

            this.txtReason = new System.Windows.Forms.TextBox();
            this.txtReason.Location = new System.Drawing.Point(12, 35);
            this.txtReason.Size = new System.Drawing.Size(376, 20);

            this.lblDate = new System.Windows.Forms.Label();
            this.lblDate.Text = "Date";
            this.lblDate.Location = new System.Drawing.Point(12, 70);

            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.dtpDate.Location = new System.Drawing.Point(12, 90);
            this.dtpDate.Size = new System.Drawing.Size(200, 20);

            this.lblTime = new System.Windows.Forms.Label();
            this.lblTime.Text = "Time";
            this.lblTime.Location = new System.Drawing.Point(220, 70);

            this.cmbTime = new System.Windows.Forms.ComboBox();
            this.cmbTime.Location = new System.Drawing.Point(220, 90);
            this.cmbTime.Size = new System.Drawing.Size(168, 21);
            this.cmbTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            this.lblProvider = new System.Windows.Forms.Label();
            this.lblProvider.Text = "Provider";
            this.lblProvider.Location = new System.Drawing.Point(12, 120);

            this.cmbProvider = new System.Windows.Forms.ComboBox();
            this.cmbProvider.Location = new System.Drawing.Point(12, 140);
            this.cmbProvider.Size = new System.Drawing.Size(200, 21);
            this.cmbProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;

            // Book Button
            this.btnBook = new System.Windows.Forms.Button();
            this.btnBook.Location = new System.Drawing.Point(313, 170);
            this.btnBook.Name = "btnBook";
            this.btnBook.Size = new System.Drawing.Size(75, 23);
            this.btnBook.Text = "Book";
            this.btnBook.Click += new System.EventHandler(this.btnBook_Click);

            this.Controls.Add(this.lblReason);
            this.Controls.Add(this.txtReason);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.cmbTime);
            this.Controls.Add(this.lblProvider);
            this.Controls.Add(this.cmbProvider);
            this.Controls.Add(this.btnBook);
        }

        #endregion

        private System.Windows.Forms.Label lblReason;
        private System.Windows.Forms.TextBox txtReason;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.ComboBox cmbTime;
        private System.Windows.Forms.Label lblProvider;
        private System.Windows.Forms.ComboBox cmbProvider;
        private System.Windows.Forms.Button btnBook;
    }
}
