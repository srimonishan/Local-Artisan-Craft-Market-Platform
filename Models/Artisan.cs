using System;
using Craft_Market_Platform.Models;

namespace Craft_Market_Platform.Models
{
    // Artisan inherits from User to reuse common properties (Inheritance demo)
    public class Artisan : User
    {
        // Artisan-specific properties
        public int ArtisanID { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
