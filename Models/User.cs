using System;

namespace LibraryManagement.Models
{
    // Keeping the enum outside the class is generally cleaner, 
    // but putting it in the same file keeps them logically grouped.
    public enum UserRole
    {
        Admin,
        Librarian
    }

    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        // Do NOT store plain text passwords. Even in a local JSON file.
        // I'm explicitly naming this PasswordHash so you don't "accidentally" store "admin123" here.
        public string PasswordHash { get; set; }

        public UserRole Role { get; set; }

        public User(string username, string passwordHash, UserRole role)
        {
            Id = Guid.NewGuid();
            Username = username;
            PasswordHash = passwordHash;
            Role = role;
        }

        // Parameterless constructor is strictly required for JSON deserialization to work properly
        public User() { }
    }
}