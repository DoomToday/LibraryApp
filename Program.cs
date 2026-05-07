using LibraryCatalog.Forms;
using LibraryCatalog.Models;

namespace Курсова
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

            // 1. Show the Login Form FIRST
            using (var loginForm = new LoginForm())
            {
                // ShowDialog pauses execution here until the Login Form is closed
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // 2. If login was successful (Admin or Guest), grab the role
                    UserRole role = loginForm.LoggedInRole;

                    // 3. NOW we run the Main Form, and we pass the role directly to it!
                    Application.Run(new MainForm(role));
                }
                else
                {
                    // If they clicked the 'X' on the login form, just exit gracefully.
                    Application.Exit();
                }
            }
        }
    }
}