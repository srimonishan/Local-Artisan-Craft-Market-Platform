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
        private Button btnApprove;
        private Button btnReject;
        private Button btnDelete;
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
            this.btnApprove = new Button();
            this.btnReject = new Button();
            this.btnDelete = new Button();

            // dgvArtisans
            this.dgvArtisans.Location = new Point(12, 12);
            this.dgvArtisans.Size = new Size(760, 380);
            this.dgvArtisans.ReadOnly = true;
            this.dgvArtisans.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvArtisans.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // btnClose
            this.btnClose.Text = "Close";
            this.btnClose.Size = new Size(100, 30);
            this.btnClose.Location = new Point(664, 400);
            this.btnClose.Click += (s, e) => this.Close();
            this.btnClose.BackColor = Color.FromArgb(10, 132, 255); // blue accent
            this.btnClose.ForeColor = Color.White;
            this.btnClose.FlatStyle = FlatStyle.Flat;
            this.btnClose.FlatAppearance.BorderSize = 0;

            // btnApprove
            this.btnApprove.Text = "Approve";
            this.btnApprove.Size = new Size(100, 30);
            this.btnApprove.Location = new Point(340, 400);
            this.btnApprove.BackColor = Color.FromArgb(0, 200, 83); // green
            this.btnApprove.ForeColor = Color.White;
            this.btnApprove.FlatStyle = FlatStyle.Flat;
            this.btnApprove.FlatAppearance.BorderSize = 0;
            this.btnApprove.Click += BtnApprove_Click;

            // btnReject
            this.btnReject.Text = "Reject";
            this.btnReject.Size = new Size(100, 30);
            this.btnReject.Location = new Point(448, 400);
            this.btnReject.BackColor = Color.FromArgb(239, 83, 80); // red
            this.btnReject.ForeColor = Color.White;
            this.btnReject.FlatStyle = FlatStyle.Flat;
            this.btnReject.FlatAppearance.BorderSize = 0;
            this.btnReject.Click += BtnReject_Click;

            // btnDelete
            this.btnDelete.Text = "Delete";
            this.btnDelete.Size = new Size(100, 30);
            this.btnDelete.Location = new Point(556, 400);
            this.btnDelete.BackColor = Color.FromArgb(239, 83, 80); // red
            this.btnDelete.ForeColor = Color.White;
            this.btnDelete.FlatStyle = FlatStyle.Flat;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.Click += BtnDelete_Click;

            // Form
            this.Text = "Artisans";
            this.ClientSize = new Size(784, 441);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = Color.FromArgb(10, 25, 47); // dark navy
            this.Controls.Add(this.dgvArtisans);
            this.Controls.Add(this.btnApprove);
            this.Controls.Add(this.btnReject);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnClose);

            // Apply rounded corners to button (simple visual polish)
            this.btnClose.Region = new Region(GetRoundedRect(this.btnClose.ClientRectangle, 8));
            this.btnApprove.Region = new Region(GetRoundedRect(this.btnApprove.ClientRectangle, 8));
            this.btnReject.Region = new Region(GetRoundedRect(this.btnReject.ClientRectangle, 8));
            this.btnDelete.Region = new Region(GetRoundedRect(this.btnDelete.ClientRectangle, 8));
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

        // Approve button handler: validates selection, confirms, updates DB and refreshes grid
        private void BtnApprove_Click(object sender, EventArgs e)
        {
            if (dgvArtisans.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "Please select an artisan first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var artisan = dgvArtisans.SelectedRows[0].DataBoundItem as Artisan;
            if (artisan == null)
            {
                MessageBox.Show(this, "Please select a valid artisan row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show(this, $"Approve {artisan.FullName}?", "Confirm Approve", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            bool ok = Artisan.UpdateArtisanStatus(artisan.ArtisanID, "Approved");
            if (ok)
            {
                LoadArtisans();
            }
            else
            {
                MessageBox.Show(this, "Failed to update artisan status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Reject button handler: same flow but sets status to 'Rejected'
        private void BtnReject_Click(object sender, EventArgs e)
        {
            if (dgvArtisans.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "Please select an artisan first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var artisan = dgvArtisans.SelectedRows[0].DataBoundItem as Artisan;
            if (artisan == null)
            {
                MessageBox.Show(this, "Please select a valid artisan row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var result = MessageBox.Show(this, $"Reject {artisan.FullName}?", "Confirm Reject", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result != DialogResult.Yes) return;

            bool ok = Artisan.UpdateArtisanStatus(artisan.ArtisanID, "Rejected");
            if (ok)
            {
                LoadArtisans();
            }
            else
            {
                MessageBox.Show(this, "Failed to update artisan status.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Delete button handler: checks selection, confirms, asks the model to
        // delete the artisan. The model will verify there are no linked
        // products before attempting deletion so we avoid FK violations.
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dgvArtisans.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "Please select an artisan first.", "No selection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var artisan = dgvArtisans.SelectedRows[0].DataBoundItem as Artisan;
            if (artisan == null)
            {
                MessageBox.Show(this, "Please select a valid artisan row.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var confirm = MessageBox.Show(this, $"Delete {artisan.FullName}? This cannot be undone.", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            var result = Artisan.DeleteArtisan(artisan.ArtisanID);
            if (result == Artisan.DeleteResult.HasProducts)
            {
                MessageBox.Show(this, "This artisan cannot be deleted because they have existing products. Please delete or reassign those products first.", "Cannot Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (result == Artisan.DeleteResult.Deleted)
            {
                LoadArtisans();
            }
            else
            {
                MessageBox.Show(this, "Failed to delete artisan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
