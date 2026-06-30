using System;

namespace BothoClinic.Models
{
    public class User
    {
        // Primary Keys and Foreign Keys
        public int UserID { get; set; }
        public int RoleID { get; set; }

        // User Profile Information
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty; // <-- MUST be added
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public string ContactPhone { get; set; } = string.Empty; // <-- MUST be added

        public string RoleName { get; set; } = string.Empty;

        // Security Properties
        public string PasswordHash { get; set; } = string.Empty;
        public string PasswordSalt { get; set; } = string.Empty;

        // Status Flags
        public bool IsActive { get; set; } = true; // <-- MUST be added
        public bool MustChangePassword { get; set; } = true; // <-- MUST be added
    }
}