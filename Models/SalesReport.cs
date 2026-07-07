using System;
using System.Data;
using System.Data.SqlClient;
using Craft_Market_Platform.Database;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Craft_Market_Platform.Models
{
    // SalesReport encapsulates all reporting logic in one place (Abstraction).
    // GenerateReport() returns a SalesReportData object that the UI can display
    // without knowing anything about SQL or the database schema.
    public class SalesReport
    {
        private readonly DatabaseConnection _db;

        public SalesReport()
        {
            _db = new DatabaseConnection();
        }

        // Holds the results of a sales report query
        public class SalesReportData
        {
            public int    TotalOrders        { get; set; }
            public decimal TotalRevenue      { get; set; }
            public int    TotalItemsSold     { get; set; }
            public string TopProduct         { get; set; }
            public DataTable OrderBreakdown  { get; set; }
        }

        // GenerateReport — single method that fetches all summary figures.
        // The UI only calls this one method; all SQL is hidden here (Abstraction).
        public SalesReportData GenerateReport()
        {
            var report = new SalesReportData();

            using (var conn = _db.GetConnection())
            {
                conn.Open();

                // --- Total orders and revenue ---
                using (var cmd = conn.CreateCommand())
                {
                    // Try Orders + OrderItems tables first; fall back gracefully
                    cmd.CommandText = @"
                        SELECT
                            COUNT(DISTINCT o.OrderID)       AS TotalOrders,
                            ISNULL(SUM(oi.Quantity * oi.UnitPrice), 0) AS TotalRevenue,
                            ISNULL(SUM(oi.Quantity), 0)     AS TotalItemsSold
                        FROM Orders o
                        LEFT JOIN OrderItems oi ON o.OrderID = oi.OrderID";
                    try
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                report.TotalOrders    = Convert.ToInt32(reader["TotalOrders"]);
                                report.TotalRevenue   = Convert.ToDecimal(reader["TotalRevenue"]);
                                report.TotalItemsSold = Convert.ToInt32(reader["TotalItemsSold"]);
                            }
                        }
                    }
                    catch
                    {
                        // If schema differs, provide zero defaults
                        report.TotalOrders    = 0;
                        report.TotalRevenue   = 0m;
                        report.TotalItemsSold = 0;
                    }
                }

                // --- Top selling product ---
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT TOP 1 p.ProductName
                        FROM OrderItems oi
                        JOIN Products p ON oi.ProductID = p.ProductID
                        GROUP BY p.ProductName
                        ORDER BY SUM(oi.Quantity) DESC";
                    try
                    {
                        var scalar = cmd.ExecuteScalar();
                        report.TopProduct = scalar != null && scalar != DBNull.Value
                            ? scalar.ToString()
                            : "N/A";
                    }
                    catch
                    {
                        report.TopProduct = "N/A";
                    }
                }

                // --- Per-product breakdown ---
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT
                            p.ProductName,
                            c.CategoryName,
                            ISNULL(SUM(oi.Quantity), 0)                  AS UnitsSold,
                            ISNULL(SUM(oi.Quantity * oi.UnitPrice), 0)   AS Revenue
                        FROM Products p
                        LEFT JOIN Categories c  ON p.CategoryID  = c.CategoryID
                        LEFT JOIN OrderItems oi ON p.ProductID   = oi.ProductID
                        GROUP BY p.ProductName, c.CategoryName
                        ORDER BY Revenue DESC";
                    try
                    {
                        var adapter = new SqlDataAdapter(cmd);
                        var dt      = new DataTable();
                        adapter.Fill(dt);
                        report.OrderBreakdown = dt;
                    }
                    catch
                    {
                        report.OrderBreakdown = new DataTable();
                    }
                }
            }

            return report;
        }

        // Generate PDF Report
        public void GeneratePdfReport(SalesReportData data, string filePath)
        {
            Document doc = new Document(PageSize.A4, 50, 50, 50, 50);
            PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            
            doc.Open();
            
            // Title
            Font titleFont = FontFactory.GetFont("Arial", 20, Font.BOLD);
            Paragraph title = new Paragraph("Sales Report\n\n", titleFont);
            title.Alignment = Element.ALIGN_CENTER;
            doc.Add(title);
            
            // Summary
            Font normalFont = FontFactory.GetFont("Arial", 12, Font.NORMAL);
            Font boldFont = FontFactory.GetFont("Arial", 12, Font.BOLD);
            
            doc.Add(new Paragraph($"Total Orders: {data.TotalOrders}", normalFont));
            doc.Add(new Paragraph($"Total Revenue: RM {data.TotalRevenue:N2}", normalFont));
            doc.Add(new Paragraph($"Total Items Sold: {data.TotalItemsSold}", normalFont));
            doc.Add(new Paragraph($"Top Product: {data.TopProduct}\n\n", normalFont));
            
            // Breakdown Table
            if (data.OrderBreakdown != null && data.OrderBreakdown.Rows.Count > 0)
            {
                PdfPTable table = new PdfPTable(data.OrderBreakdown.Columns.Count);
                table.WidthPercentage = 100;
                
                // Headers
                foreach (DataColumn column in data.OrderBreakdown.Columns)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(column.ColumnName, boldFont));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }
                
                // Data
                foreach (DataRow row in data.OrderBreakdown.Rows)
                {
                    foreach (object item in row.ItemArray)
                    {
                        table.AddCell(new Phrase(item.ToString(), normalFont));
                    }
                }
                
                doc.Add(table);
            }
            else
            {
                doc.Add(new Paragraph("No product breakdown data available.", normalFont));
            }
            
            doc.Close();
        }
    }
}
