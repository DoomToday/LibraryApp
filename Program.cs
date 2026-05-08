using LibraryCatalog.Forms;
using LibraryCatalog.Models;
using LibraryCatalog.Repositories;

namespace LibraryCatalog
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var centralUserRepository = new UserRepository();

            using (var loginForm = new LoginForm(centralUserRepository))
            {
                // ShowDialog pauses execution here until the Login Form is closed
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Grab the FULL User object from the login form
                    User loggedInUser = loginForm.LoggedInUser;

                    // Pass the User directly to the MainForm
                    Application.Run(new MainForm(loggedInUser, centralUserRepository));
                }
                else
                {
                    Application.Exit();
                }
            }
        }
    }
}