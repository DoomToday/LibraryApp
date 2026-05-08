using System.Text.Json;
using LibraryCatalog.Models; 
using LibraryCatalog.Utilities;

namespace LibraryCatalog.Repositories
{
    public class UserRepository
    {
        private readonly string _filePath = "users.json";
        private List<User> _users;

        public UserRepository()
        {
            _users = LoadUsers();

            // Seed a default admin if the file is completely empty/new
            if (!_users.Any())
            {
                SeedDefaultAdmin();
            }
        }

        private List<User> LoadUsers()
        {
            if (!File.Exists(_filePath))
                return new List<User>();

            try
            {
                string json = File.ReadAllText(_filePath);
                return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
            }
            catch
            {
                // If you somehow corrupt your JSON file manually, return an empty list 
                // instead of letting the app completely crash on startup.
                return new List<User>();
            }
        }

        public void SaveUsers()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(_users, options);
            File.WriteAllText(_filePath, json);
        }

        private void SeedDefaultAdmin()
        {
            // USING THE HASHER WE JUST WROTE. 
            var admin = new User(
                "admin",
                PasswordHasher.HashPassword("admin123!"),
                UserRole.Admin
            );
            _users.Add(admin);
            SaveUsers();
        }

        public User GetUserByUsername(string username)
        {
            // Case-insensitive search is always better for usernames.
            return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void AddUser(User user)
        {
            if (GetUserByUsername(user.Username) != null)
                throw new InvalidOperationException("A user with this username already exists.");

            _users.Add(user);
            SaveUsers();
        }
        public void ToggleFavorite(Guid userId, int bookId)
        {
            // Find the specific user making the request
            var user = _users.FirstOrDefault(u => u.Id == userId);
            if (user == null) return;

            // Failsafe in case JSON loaded a null list
            if (user.FavoriteBookIds == null)
                user.FavoriteBookIds = new List<int>();

            // If they already favorited it, unfavorite it. Otherwise, add it.
            if (user.FavoriteBookIds.Contains(bookId))
            {
                user.FavoriteBookIds.Remove(bookId);
            }
            else
            {
                user.FavoriteBookIds.Add(bookId);
            }

            // Save the updated user file!
            SaveUsers();
        }
    }
}