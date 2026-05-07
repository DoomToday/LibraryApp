using System.Security.Cryptography;
using System.Text;
using LibraryCatalog.Models; // Make sure this is here so we can access UserRole!

namespace LibraryCatalog.Services
{
    public class AuthService
    {
        private readonly string _adminUsername = "admin";
        private readonly string _adminPasswordHash;

        public AuthService()
        {
            // I'm pre-hashing the password "admin123" here. 
            // In a real app, you'd NEVER do this in the constructor, you'd pull it from a secure DB!
            _adminPasswordHash = ComputeSha256Hash("admin123");
        }

        public bool AuthenticateAdmin(string username, string password)
        {
            // If they leave it blank, instant rejection!
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (username == _adminUsername && ComputeSha256Hash(password) == _adminPasswordHash)
            {
                return true;
            }

            return false;
        }

        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}