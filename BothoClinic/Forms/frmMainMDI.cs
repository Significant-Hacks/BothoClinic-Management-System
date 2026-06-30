using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms;
using BothoClinic.Models; // Assuming i have a User model or similar structure

namespace BothoClinic
{
    /// <summary>
    /// The main Multiple Document Interface (MDI) Parent Form.
    /// This form acts as the central hub for the application, managing user authentication state,
    /// form navigation, and user context across the system.
    /// </summary>
    public partial class frmMainMDI : Form
    {
        // Property to hold the current user's details after successful login
        public User? CurrentUser { get; private set; }

        public frmMainMDI()
        {
            InitializeComponent();
            this.IsMdiContainer = true; // Crucial: Designates this as the parent container
            this.WindowState = FormWindowState.Maximized; // Start maximized for better user experience
        }

        /// <summary>
        /// Called when the main MDI form first loads. Initializes the application state by
        /// immediately showing the Login Form.
        /// </summary>
        private void frmMainMDI_Load(object sender, EventArgs e)
        {

        }

        private void frmMainMDI_Shown(object sender, EventArgs e)
        {
            ShowLoginForm();
        }

        /// <summary>
        /// Displays the Login Form and sets up the event handler for a successful login.
        /// All forms currently open in the MDI area are closed first.
        /// </summary>
        public void ShowLoginForm()
        {
            // Close all currently open MDI children forms
            CloseAllMdiChildren();

            using var loginForm = new frmLogin();
            {
                // Show the login form as a modal dialog.
                // The code will pause here until the login form is closed.
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // If login was successful, proceed to handle the logged-in user.
                    HandleLoginSuccessful(loginForm.AuthenticatedUser);
                }
                else
                {
                    // If the login form is closed without a successful login (e.g., user clicks Exit),
                    // then close the main application.
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Event handler that is called when a user successfully logs in.
        /// </summary>
        /// <param name="sender">The source of the event (frmLogin).</param>
        /// <param name="user">The User object containing the logged-in user's details.</param>
        public void HandleLoginSuccessful(User user)
        {
            // 1. Store the logged-in user details
            CurrentUser = user;
            UserSession.Login(user);

            // 2. Close any other MDI children (just in case)
            CloseAllMdiChildren();

            // 3. Update the MDI title
            this.Text = $"Botho Clinic Management System - Logged in as: {user.FullName} ({user.RoleName})";

            // 4. Show the appropriate dashboard based on the user's role
            if (user.RoleName != null)
            {
                ShowDashboard(user.RoleName, user);
            }
            else
            {
                MessageBox.Show("User role is not defined. Please contact support.", "Role Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowLoginForm();
            }
        }

        private void ShowDashboard(string roleName, User user)
        {
            Form dashboardForm;

            if (roleName.Equals("Administrator", StringComparison.OrdinalIgnoreCase))
            {
                dashboardForm = new frmAdminDashboard(user);
            }
            else if (roleName.Equals("Provider", StringComparison.OrdinalIgnoreCase))
            {
                dashboardForm = new frmProviderDashboard(user);
            }
            else if (roleName.Equals("Student", StringComparison.OrdinalIgnoreCase))
            {
                dashboardForm = new frmStudentDashboard(user);
            }
            else
            {
                MessageBox.Show($"Unrecognized role: {roleName}. Please contact support.", "Role Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ShowLoginForm();
                return;
            }

            dashboardForm.MdiParent = this;
            dashboardForm.Dock = DockStyle.Fill;
            dashboardForm.FormBorderStyle = FormBorderStyle.None;
            dashboardForm.Show();
        }

        /// <summary>
        /// Global logout functionality. Clears the user context and returns to the login screen.
        /// </summary>
        public void Logout()
        {
            if (MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // UserSession.Logout() will handle the audit logging
                UserSession.Logout();
                CurrentUser = null; // Clear the current user context
                ShowLoginForm();
            }
        }

        /// <summary>
        /// Utility to close all child forms open in the MDI container.
        /// </summary>
        private void CloseAllMdiChildren()
        {
            foreach (Form childForm in this.MdiChildren)
            {
                childForm.Close();
            }
        }

        // This method will be used by the Logout Menu item (if you add one to the MDI Parent)
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Logout();
        }
    }
}
