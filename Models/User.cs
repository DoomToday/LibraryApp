using System;

namespace LibraryCatalog.Models
{
    // Keeping the enum outside the class is generally cleaner, 
    // but putting it in the same file keeps them logically grouped.
    public enum UserRole
    {
        Admin,
        Guest,
        User
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public UserRole Role { get; set; }

        public List<int> FavoriteBookIds { get; set; } = new List<int>();

        public User(string username, string passwordHash, UserRole role)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
            FavoriteBookIds = new List<int>();
        }

        // Parameterless constructor is strictly required for JSON deserialization to work properly
        public User() { }
    }
}