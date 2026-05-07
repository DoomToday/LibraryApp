using System;
using System.Security.Cryptography;

namespace LibraryManagement.Utilities
{
    public static class PasswordHasher
    {
        // 100,000 iterations is a solid, secure standard for modern hardware.
        // It slows down brute-force attacks without lagging your WinForms app.
        private const int Iterations = 100000;
        private const int SaltSize = 16; // 128 bit salt
        private const int KeySize = 32;  // 256 bit key

        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty.");

            // 1. Generate a random cryptographic salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // 2. Hash the password using PBKDF2
            using (var algorithm = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256))
            {
                byte[] key = algorithm.GetBytes(KeySize);

                // 3. Combine the salt and the hash into one array so we only have to save one string to JSON
                byte[] hashBytes = new byte[SaltSize + KeySize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(key, 0, hashBytes, SaltSize, KeySize);

                // Return as a nice, clean Base64 string
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPassword(string enteredPassword, string storedHash)
        {
            if (string.IsNullOrWhiteSpace(enteredPassword) || string.IsNullOrWhiteSpace(storedHash))
                return false;

            try
            {
                // 1. Convert the stored Base64 string back to a byte array
                byte[] hashBytes = Convert.FromBase64String(storedHash);

                // 2. Extract the salt from the beginning of the array
                byte[] salt = new byte[SaltSize];
                Array.Copy(hashBytes, 0, salt, 0, SaltSize);

                // 3. Re-hash the entered password using the EXACT SAME salt and iterations
                using (var algorithm = new Rfc2898DeriveBytes(enteredPassword, salt, Iterations, HashAlgorithmName.SHA256))
                {
                    byte[] keyToCheck = algorithm.GetBytes(KeySize);

                    // 4. Compare the newly generated key with the stored key
                    // If even one byte is off, it's the wrong password.
                    for (int i = 0; i < KeySize; i++)
                    {
                        if (hashBytes[i + SaltSize] != keyToCheck[i])
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}