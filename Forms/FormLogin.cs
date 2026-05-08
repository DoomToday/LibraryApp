using LibraryCatalog.Forms;
using LibraryCatalog.Models;
using LibraryCatalog.Repositories;
using LibraryCatalog.Utilities;

namespace LibraryCatalog
{
    public partial class LoginForm : Form
    {

        private UserRepository _userRepository;

        // The Main Form will read this after the Login Form closes
        public User LoggedInUser { get; private set; }

        public LoginForm(UserRepository userRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;
        }

        // The button where they type the password
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Enter both a username and password.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 1. Try to pull the user from the JSON repository
            User user = _userRepository.GetUserByUsername(username);

            // If the user is null, they don't exist. 
            // IMPORTANT: Never tell the user "Username not found". That's a security vulnerability.
            // Give a vague error for both wrong username AND wrong password.
            if (user == null || !PasswordHasher.VerifyPassword(password, user.PasswordHash))
            {
                MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // If you make it here, the login was successful!
            MessageBox.Show($"Welcome back, {user.Role}!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Set the property so Program.cs can read it
            this.LoggedInUser = user;

            // Tell Program.cs the login was a success
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // The separate Guest button
        private void btnGuest_Click(object sender, EventArgs e)
        {
            // Create a dummy user object on the fly for the guest
            this.LoggedInUser = new User("Guest", "", UserRole.Guest);

            // Tell Program.cs the guest entry was a success
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}