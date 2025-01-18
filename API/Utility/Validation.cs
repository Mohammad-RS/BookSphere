using System.Text.RegularExpressions;

namespace App.Utility
{
    public class Validation
    {
        public bool IsTableNameValid(string tableName)
        {
            string[] validTableNames = [
            "User", "Book", "Category", "Review", "Author", "Staff"
            ];

            return validTableNames.Contains(tableName);
        }

        public string PasswordValidation(string pw1)
        {
            if (pw1.Length < 8 || pw1.Length > 16)
            {
                return "Password must be between 8 and 16 characters.";
            }

            if (!Regex.IsMatch(pw1, @"[a-z]"))
            {
                return "Password must contain at least one lowercase letter.";
            }

            if (!Regex.IsMatch(pw1, @"[A-Z]"))
            {
                return "Password must contain at least one uppercase letter.";
            }

            if (!Regex.IsMatch(pw1, @"[0-9]"))
            {
                return "Password must contain at least one digit.";
            }

            return "";
        }

        public string UsernameValidation(string username)
        {
            if (username.Length < 5 || username.Length > 50)
            {
                return "Username must be between 5 and 50 characters.";
            }

            if (username.Contains('@') || username.Contains(' ') || username.Contains('-'))
            {
                return "Username cannot contain '@', '-' or space.";
            }

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                return "Username can only contain english letters, digits and underscores.";
            }

            return "";
        }

        public string EmailValidation(string email)
        {
            if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
            {
                return "Invalid email format.";
            }

            if (email.Length > 100)
            {
                return "Email must be less than 100 characters.";
            }

            return "";
        }
    }
}
