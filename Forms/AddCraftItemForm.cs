using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using Craft_Market_Platform.Database;

namespace Craft_Market_Platform.Forms
{
    public class AddCraftItemForm : Form
    {
        private Label lblName;
        private TextBox txtName;
        private Label lblDescription;
        private TextBox txtDescription;
        private Label lblPrice;
        private NumericUpDown numPrice;
        private Label lblArtisan;
        private ComboBox cbArtisan;
        private Label lblCategory;
        private ComboBox cbCategory;
        private Label lblStock;
        private NumericUpDown numStock;
        private Button btnSave;
        private Button btnCancel;

        private DatabaseConnection _db = new DatabaseConnection();

        public AddCraftItemForm()
        {
            InitializeComponent();
            this.Load += AddCraftItemForm_Load;
        }

        private void AddCraftItemForm_Load(object sender, EventArgs e)
        {
            // Populate artisans (only approved) and categories
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    conn.Open();

                    // Artisans
                    cmd.CommandText = "SELECT ArtisanID, FullName FROM Artisans WHERE Status = 'Approved' ORDER BY FullName";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dt = new System.Data.DataTable();
                        dt.Load(reader);
                        cbArtisan.DisplayMember = "FullName";
                        cbArtisan.ValueMember = "ArtisanID";
                        cbArtisan.DataSource = dt;
                    }

                    // Categories
                    cmd.CommandText = "SELECT CategoryID, CategoryName FROM Categories ORDER BY CategoryName";
                    using (var reader = cmd.ExecuteReader())
                    {
                        var dtc = new System.Data.DataTable();
                        dtc.Load(reader);
                        cbCategory.DisplayMember = "CategoryName";
                        cbCategory.ValueMember = "CategoryID";
                        cbCategory.DataSource = dtc;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to load artisans or categories: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeComponent()
        {
            this.lblName = new Label();
            this.txtName = new TextBox();
            this.lblDescription = new Label();
            this.txtDescription = new TextBox();
            this.lblPrice = new Label();
            this.numPrice = new NumericUpDown();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(12, 15);
            this.lblName.Text = "Product Name";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(120, 12);
            this.txtName.Width = 260;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(12, 50);
            this.lblDescription.Text = "Description";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(120, 47);
            this.txtDescription.Width = 260;
            this.txtDescription.Height = 80;
            this.txtDescription.Multiline = true;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(12, 140);
            this.lblPrice.Text = "Price";
            // 
            // numPrice
            // 
            this.numPrice.Location = new System.Drawing.Point(120, 138);
            this.numPrice.DecimalPlaces = 2;
            this.numPrice.Maximum = 1000000;
            this.numPrice.Width = 120;

            // 
            // lblStock
            // 
            this.lblStock.AutoSize = true;
            this.lblStock.Location = new System.Drawing.Point(260, 140);
            this.lblStock.Text = "Stock";
            // 
            // numStock
            // 
            this.numStock.Location = new System.Drawing.Point(310, 138);
            this.numStock.Maximum = 1000000;
            this.numStock.Width = 70;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(120, 180);
            this.btnSave.Text = "Save";
            this.btnSave.Click += BtnSave_Click;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(220, 180);
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // 
            // lblArtisan
            // 
            this.lblArtisan.AutoSize = true;
            this.lblArtisan.Location = new System.Drawing.Point(12, 135);
            this.lblArtisan.Text = "Artisan";
            // 
            // cbArtisan
            // 
            this.cbArtisan.Location = new System.Drawing.Point(120, 165);
            this.cbArtisan.Width = 260;
            this.cbArtisan.DropDownStyle = ComboBoxStyle.DropDownList;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(12, 165);
            this.lblCategory.Text = "Category";
            // 
            // cbCategory
            // 
            this.cbCategory.Location = new System.Drawing.Point(120, 195);
            this.cbCategory.Width = 260;
            this.cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;

            // 
            // AddCraftItemForm
            // 
            this.ClientSize = new System.Drawing.Size(420, 260);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.numPrice);
            this.Controls.Add(this.lblStock);
            this.Controls.Add(this.numStock);
            this.Controls.Add(this.lblArtisan);
            this.Controls.Add(this.cbArtisan);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Add Craft Item";
            this.MaximizeBox = false;
            this.MinimizeBox = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var description = txtDescription.Text.Trim();
            var price = numPrice.Value;
            var stock = (int)numStock.Value;

            // Validate required fields
            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show(this, "Please enter a name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cbArtisan.SelectedValue == null || cbCategory.SelectedValue == null)
            {
                MessageBox.Show(this, "Please select an artisan and a category.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var artisanId = Convert.ToInt32(cbArtisan.SelectedValue);
            var categoryId = Convert.ToInt32(cbCategory.SelectedValue);

            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    // INSERT with required foreign keys and correct column names
                    cmd.CommandText = "INSERT INTO Products (ArtisanID, CategoryID, ProductName, Description, Price, Stock) VALUES (@artisan, @category, @name, @desc, @price, @stock)";
                    cmd.Parameters.AddWithValue("@artisan", artisanId);
                    cmd.Parameters.AddWithValue("@category", categoryId);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@desc", description);
                    cmd.Parameters.AddWithValue("@price", price);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to save item: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
