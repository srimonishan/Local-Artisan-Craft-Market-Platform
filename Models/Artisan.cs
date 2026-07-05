using System;
using System.Data.SqlClient;
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

        // UpdateArtisanStatus centralizes the DB update logic for an artisan's Status.
        // This method is intentionally simple for a university OOP example. It uses
        // the DatabaseConnection helper to get a SqlConnection and executes a
        // parameterized UPDATE statement to avoid SQL injection.
        // Note: Artisan inherits common user properties from User (e.g. FullName,
        // Email) — this class demonstrates inheritance and keeps DB logic here.
        public static bool UpdateArtisanStatus(int artisanId, string newStatus)
        {
            try
            {
                var db = new Database.DatabaseConnection();
                using (var conn = db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE Artisans SET Status = @status WHERE ArtisanID = @id";
                    cmd.Parameters.AddWithValue("@status", newStatus);
                    cmd.Parameters.AddWithValue("@id", artisanId);
                    conn.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0;
                }
            }
            catch
            {
                // Swallowing exceptions here keeps the example simple; callers may
                // show user-friendly messages when an update fails.
                return false;
            }
        }

        // Result codes for DeleteArtisan to communicate outcome to callers
        public enum DeleteResult
        {
            Deleted,
            HasProducts,
            Failed
        }

        // DeleteArtisan attempts to delete the artisan record from the database.
        // It first checks the Products table for any rows that reference this
        // artisan to avoid violating foreign key constraints. Returns a
        // DeleteResult so the UI can show an appropriate message.
        public static DeleteResult DeleteArtisan(int artisanId)
        {
            try
            {
                var db = new Database.DatabaseConnection();
                using (var conn = db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    // Check for linked products first
                    cmd.CommandText = "SELECT COUNT(*) FROM Products WHERE ArtisanID = @id";
                    cmd.Parameters.AddWithValue("@id", artisanId);
                    conn.Open();
                    var scalar = cmd.ExecuteScalar();
                    int count = 0;
                    if (scalar != null && scalar != DBNull.Value)
                    {
                        count = Convert.ToInt32(scalar);
                    }

                    if (count > 0)
                    {
                        // Cannot delete while products exist for this artisan
                        return DeleteResult.HasProducts;
                    }

                    // No linked products; safe to delete
                    cmd.Parameters.Clear();
                    cmd.CommandText = "DELETE FROM Artisans WHERE ArtisanID = @id";
                    cmd.Parameters.AddWithValue("@id", artisanId);
                    int rows = cmd.ExecuteNonQuery();
                    return rows > 0 ? DeleteResult.Deleted : DeleteResult.Failed;
                }
            }
            catch
            {
                // For a beginner example we return Failed on exceptions and let
                // the UI show a friendly error message.
                return DeleteResult.Failed;
            }
        }
    }
}
