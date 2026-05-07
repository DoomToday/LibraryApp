using System;
using System.Windows.Forms;
using LibraryCatalog.Models;
using LibraryCatalog.Services;

namespace Курсова
{
    public partial class LoginForm : Form
    {
        private AuthService _authService;

        // The Main Form will read this after the Login Form closes
        public UserRole LoggedInRole { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            _authService = new AuthService();
        }

        // The button where they type the password
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (_authService.AuthenticateAdmin(txtUsername.Text, txtPassword.Text))
            {
                LoggedInRole = UserRole.Admin;
                this.DialogResult = DialogResult.OK; // This tells the app the login was successful
                this.Close();
            }
            else
            {
                // Don't just let them in! Yell at them!
                MessageBox.Show("Invalid username or password!", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }

        // The separate Guest button
        private void btnGuest_Click(object sender, EventArgs e)
        {
            LoggedInRole = UserRole.Guest;
            this.DialogResult = DialogResult.OK; // Guest entry is still a "successful" form completion
            this.Close();
        }
    }
}