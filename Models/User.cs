using System;

namespace Craft_Market_Platform.Models
{
    // Base class for users in the system
    // Demonstrates INHERITANCE: common user properties live here
    public class User
    {
        // Common properties shared by different user types
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
