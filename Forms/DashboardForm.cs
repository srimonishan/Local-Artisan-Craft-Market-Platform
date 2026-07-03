using System;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using Craft_Market_Platform.Database;

namespace Craft_Market_Platform.Forms
{
    public partial class DashboardForm : Form
    {
        private int _adminId;
        private string _fullName;
        private DatabaseConnection _db = new DatabaseConnection();

        public DashboardForm(int adminId, string fullName)
        {
            InitializeComponent();
            _adminId = adminId;
            _fullName = fullName;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {_fullName}!";
            LoadProducts();
            GetDashboardCounts();
        }

        private void btnAddCraftItem_Click(object sender, EventArgs e)
        {
            using (var addForm = new AddCraftItemForm())
            {
                var result = addForm.ShowDialog(this);
                if (result == DialogResult.OK)
                {
                    LoadProducts();
                    GetDashboardCounts();
                }
            }
        }

        private void LoadProducts()
        {
            try
            {
                // Abstraction: LoadProducts hides the SQL details from the UI code
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT p.ProductID, p.ProductName, c.CategoryName, p.Price, p.Stock
                                        FROM Products p
                                        LEFT JOIN Categories c ON p.CategoryID = c.CategoryID
                                        ORDER BY p.ProductID DESC";
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        var dt = new DataTable();
                        da.Fill(dt);
                        dgvProducts.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to load products: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count == 0)
            {
                MessageBox.Show(this, "Please select a product to delete.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var row = dgvProducts.SelectedRows[0];
            var idObj = row.Cells["ProductID"].Value ?? row.Cells[0].Value;
            if (idObj == null)
            {
                MessageBox.Show(this, "Cannot determine ProductID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int productId = Convert.ToInt32(idObj);

            var confirm = MessageBox.Show(this, "Are you sure you want to delete the selected product?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm != DialogResult.Yes) return;

            try
            {
                DeleteProduct(productId);
                LoadProducts();
                GetDashboardCounts();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to delete product: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Abstraction: This method hides the SQL DELETE operation behind a simple method call
        private void DeleteProduct(int productId)
        {
            using (SqlConnection conn = _db.GetConnection())
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Products WHERE ProductID = @id";
                cmd.Parameters.AddWithValue("@id", productId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private void GetDashboardCounts()
        {
            try
            {
                using (SqlConnection conn = _db.GetConnection())
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT TOP 1 TotalProducts, TotalArtisans, TotalOrders FROM DashboardView";
                    conn.Open();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            lblTotalProducts.Text = $"Products: {reader["TotalProducts"]}";
                            lblTotalArtisans.Text = $"Artisans: {reader["TotalArtisans"]}";
                            lblTotalOrders.Text = $"Orders: {reader["TotalOrders"]}";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Non-fatal: show info but allow UI to continue
                lblTotalProducts.Text = "Products: ?";
                lblTotalArtisans.Text = "Artisans: ?";
                lblTotalOrders.Text = "Orders: ?";
                Console.WriteLine("GetDashboardCounts failed: " + ex.Message);
            }
        }
    }
}
