using LibraryCatalog.Models;
using LibraryCatalog.Repositories;
using LibraryCatalog.Utilities; 

namespace LibraryCatalog.Forms
{
    public partial class AddUserForm : Form
    {
        private UserRepository _userRepository;

        public AddUserForm(UserRepository userRepository)
        {
            InitializeComponent();
            _userRepository = userRepository;

            // Cast the enum values to a list and filter out 'Guest'
            cmbRole.DataSource = Enum.GetValues(typeof(UserRole))
                                     .Cast<UserRole>()
                                     .Where(role => role != UserRole.Guest)
                                     .ToList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // 1. Basic validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Username and password cannot be empty.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Check if the username is already taken
            if (_userRepository.GetUserByUsername(username) != null)
            {
                MessageBox.Show("A user with this username already exists. Please choose another.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // 3. Grab the selected role
            UserRole selectedRole = (UserRole)cmbRole.SelectedItem;

            // 4. Create the user, HASH THE PASSWORD, and save!
            string hashedPass = PasswordHasher.HashPassword(password);
            User newUser = new User(username, hashedPass, selectedRole);

            _userRepository.AddUser(newUser);

            MessageBox.Show($"Successfully created {selectedRole} account for '{username}'.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}