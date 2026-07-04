using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Craft_Market_Platform.Database;
using Craft_Market_Platform.Models;

namespace Craft_Market_Platform.Forms
{
    public class ArtisanListForm : Form
    {
        private DataGridView dgvArtisans;
        private Button btnClose;
        private DatabaseConnection _db = new DatabaseConnection();

        public ArtisanListForm()
        {
            InitializeComponent();
            LoadArtisans();
        }

        private void InitializeComponent()
        {
            this.dgvArtisans = new DataGridView();
            this.btnClose = new Button();

            // dgvArtisans
            this.dgvArtisans.Location = new Point(12, 12);
            this.dgvArtisans.Size = new Size(760, 380);
            this.dgvArtisans.ReadOnly = true;
            this.dgvArtisans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvArtisans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // btnClose
            this.btnClose.Text = "Close";
            this.btnClose.Size = new Size(100, 30);
            this.btnClose.Location = new Point(672, 400);
            this.btnClose.Click += (s, e) => this.Close();
            this.btnClose.BackColor = Color.FromArgb(10, 132, 255); // blue accent
            this.btnClose.ForeColor = Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;

            // Form
            this.Text = "Artisans";
            this.ClientSize = new Size(784, 441);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(10, 25, 47); // dark navy
            this.Controls.Add(this.dgvArtisans);
            this.Controls.Add(this.btnClose);

            // Apply rounded corners to button (simple visual polish)
            this.btnClose.Region = new Region(GetRoundedRect(this.btnClose.ClientRectangle, 8));
        }

        // Returns a GraphicsPath that describes a rounded rectangle
        private GraphicsPath GetRoundedRect(Rectangle rect, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Load artisans from DB, create Artisan objects and bind to grid
        private void LoadArtisans()
        {
            try
            {
                var artisans = new List<Artisan>();
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT ArtisanID, FullName, Email, Phone, Address, Status, CreatedAt FROM Artisans ORDER BY ArtisanID DESC";
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var a = new Artisan
                            {
                                ArtisanID = reader.GetInt32(reader.GetOrdinal("ArtisanID")),
                                FullName = reader["FullName"]?.ToString(),
                                Email = reader["Email"]?.ToString(),
                                Phone = reader["Phone"]?.ToString(),
                                Address = reader["Address"]?.ToString(),
                                Status = reader["Status"]?.ToString(),
                                CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt"))
                            };
                            artisans.Add(a);
                        }
                    }
                }

                // Bind list to DataGridView. This demonstrates that Artisan inherits
                // common properties from User (FullName, Email, Phone).
                dgvArtisans.DataSource = artisans;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to load artisans: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
