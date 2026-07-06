using System;
using System.Data;
using System.Data.SqlClient;
using Craft_Market_Platform.Database;

namespace Craft_Market_Platform.Models
{
    // ProductSearch demonstrates POLYMORPHISM via method overloading.
    // The same method name "Search" has two signatures:
    //   1. Search(name)            — search by product name only
    //   2. Search(name, category)  — search by product name AND category
    // The caller can invoke either version; the compiler resolves the correct
    // overload based on the arguments supplied.
    public class ProductSearch
    {
        private readonly DatabaseConnection _db;

        public ProductSearch()
        {
            _db = new DatabaseConnection();
        }

        // Overload 1 — search by name only (Polymorphism: same method name, different parameters)
        public DataTable Search(string name)
        {
            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                // LIKE search so partial names are matched
                cmd.CommandText = @"
                    SELECT p.ProductID,
                           p.ProductName,
                           c.CategoryName,
                           p.Price,
                           p.Stock
                    FROM   Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE  p.ProductName LIKE @name
                    ORDER BY p.ProductName";

                cmd.Parameters.AddWithValue("@name", "%" + name + "%");
                conn.Open();

                var adapter = new SqlDataAdapter(cmd);
                var result  = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }

        // Overload 2 — search by name AND category (Polymorphism: same method name, extra parameter)
        public DataTable Search(string name, string category)
        {
            // If no category filter is supplied fall back to the name-only overload
            if (string.IsNullOrWhiteSpace(category) || category == "All")
                return Search(name);

            using (var conn = _db.GetConnection())
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = @"
                    SELECT p.ProductID,
                           p.ProductName,
                           c.CategoryName,
                           p.Price,
                           p.Stock
                    FROM   Products p
                    LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                    WHERE  p.ProductName LIKE @name
                      AND  c.CategoryName = @category
                    ORDER BY p.ProductName";

                cmd.Parameters.AddWithValue("@name",     "%" + name + "%");
                cmd.Parameters.AddWithValue("@category", category);
                conn.Open();

                var adapter = new SqlDataAdapter(cmd);
                var result  = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }
    }
}
