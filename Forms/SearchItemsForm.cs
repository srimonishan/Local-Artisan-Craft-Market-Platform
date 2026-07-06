using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using Craft_Market_Platform.Database;
using Craft_Market_Platform.Models;

namespace Craft_Market_Platform.Forms
{
    // SearchItemsForm — Feature 9: Search Items
    // Demonstrates POLYMORPHISM: the "Search" button calls either
    //   ProductSearch.Search(name)            — when no category is chosen, or
    //   ProductSearch.Search(name, category)  — when a category is selected.
    // Both are overloads of the same method name on ProductSearch.
    public class SearchItemsForm : Form
    {
        // ── Controls ────────────────────────────────────────────────────────────
        private Label           lblTitle;
        private Label           lblName;
        private TextBox         txtName;
        private Label           lblCategory;
        private ComboBox        cbCategory;
        private Button          btnSearch;
        private DataGridView    dgvResults;
        private Label           lblResultCount;

        private readonly ProductSearch    _search = new ProductSearch();
        private readonly DatabaseConnection _db   = new DatabaseConnection();

        // ── Constructor ─────────────────────────────────────────────────────────
        public SearchItemsForm()
        {
            InitializeComponent();
            this.Load += SearchItemsForm_Load;
        }

        // ── Form Load ───────────────────────────────────────────────────────────
        private void SearchItemsForm_Load(object sender, EventArgs e)
        {
            LoadCategories();
            // Show all products on open
            PerformSearch();
        }

        // ── Load category dropdown ───────────────────────────────────────────────
        private void LoadCategories()
        {
            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd  = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT CategoryName FROM Categories ORDER BY CategoryName";
                    conn.Open();

                    cbCategory.Items.Clear();
                    cbCategory.Items.Add("All"); // first item = no filter

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                            cbCategory.Items.Add(reader.GetString(0));
                    }
                    cbCategory.SelectedIndex = 0;
                }
            }
            catch
            {
                cbCategory.Items.Clear();
                cbCategory.Items.Add("All");
                cbCategory.SelectedIndex = 0;
            }
        }

        // ── Search handler ───────────────────────────────────────────────────────
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        // Allow pressing Enter in the text box to trigger a search
        private void TxtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                PerformSearch();
            }
        }

        // ── Core search logic — POLYMORPHISM in action ───────────────────────────
        private void PerformSearch()
        {
            try
            {
                string name     = txtName.Text.Trim();
                string category = cbCategory.SelectedItem?.ToString() ?? "All";

                DataTable results;

                // POLYMORPHISM: same method name, two different overloads chosen at runtime
                if (category == "All")
                {
                    // Overload 1: Search(string name) — search by name only
                    results = _search.Search(name);
                }
                else
                {
                    // Overload 2: Search(string name, string category) — name + category
                    results = _search.Search(name, category);
                }

                dgvResults.DataSource   = results;
                lblResultCount.Text     = $"{results.Rows.Count} result(s) found.";
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Search failed: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ── InitializeComponent ──────────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.lblTitle      = new Label();
            this.lblName       = new Label();
            this.txtName       = new TextBox();
            this.lblCategory   = new Label();
            this.cbCategory    = new ComboBox();
            this.btnSearch     = new Button();
            this.dgvResults    = new DataGridView();
            this.lblResultCount = new Label();

            this.SuspendLayout();

            // ── Title ─────────────────────────────────────────────────────────
            this.lblTitle.AutoSize  = true;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(20, 18);
            this.lblTitle.Text      = "🔍 Search Items";

            // ── Name label & text box ─────────────────────────────────────────
            this.lblName.AutoSize  = true;
            this.lblName.ForeColor = System.Drawing.Color.Silver;
            this.lblName.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblName.Location  = new System.Drawing.Point(20, 65);
            this.lblName.Text      = "Product Name:";

            this.txtName.Location  = new System.Drawing.Point(140, 62);
            this.txtName.Width     = 220;
            this.txtName.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.txtName.BackColor = System.Drawing.Color.FromArgb(30, 45, 70);
            this.txtName.ForeColor = System.Drawing.Color.White;
            this.txtName.BorderStyle = BorderStyle.FixedSingle;
            this.txtName.KeyDown  += TxtName_KeyDown;

            // ── Category label & combo ────────────────────────────────────────
            this.lblCategory.AutoSize  = true;
            this.lblCategory.ForeColor = System.Drawing.Color.Silver;
            this.lblCategory.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCategory.Location  = new System.Drawing.Point(20, 100);
            this.lblCategory.Text      = "Category:";

            this.cbCategory.Location      = new System.Drawing.Point(140, 97);
            this.cbCategory.Width         = 220;
            this.cbCategory.Font          = new System.Drawing.Font("Segoe UI", 10F);
            this.cbCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cbCategory.BackColor     = System.Drawing.Color.FromArgb(30, 45, 70);
            this.cbCategory.ForeColor     = System.Drawing.Color.White;
            this.cbCategory.FlatStyle     = FlatStyle.Flat;

            // ── Search button ─────────────────────────────────────────────────
            this.btnSearch.Location  = new System.Drawing.Point(375, 62);
            this.btnSearch.Size      = new System.Drawing.Size(110, 55);
            this.btnSearch.Text      = "Search";
            this.btnSearch.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSearch.BackColor = System.Drawing.Color.FromArgb(10, 132, 255);
            this.btnSearch.ForeColor = System.Drawing.Color.White;
            this.btnSearch.FlatStyle = FlatStyle.Flat;
            this.btnSearch.FlatAppearance.BorderSize = 0;
            this.btnSearch.Cursor    = Cursors.Hand;
            this.btnSearch.Click    += BtnSearch_Click;

            // ── Result count label ────────────────────────────────────────────
            this.lblResultCount.AutoSize  = true;
            this.lblResultCount.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.lblResultCount.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblResultCount.Location  = new System.Drawing.Point(20, 135);
            this.lblResultCount.Text      = "Enter a name and click Search.";

            // ── Results grid ──────────────────────────────────────────────────
            this.dgvResults.Location              = new System.Drawing.Point(20, 160);
            this.dgvResults.Size                  = new System.Drawing.Size(740, 300);
            this.dgvResults.ReadOnly              = true;
            this.dgvResults.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            this.dgvResults.AllowUserToAddRows    = false;
            this.dgvResults.AllowUserToDeleteRows = false;
            this.dgvResults.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvResults.BackgroundColor       = System.Drawing.Color.FromArgb(18, 35, 60);
            this.dgvResults.GridColor             = System.Drawing.Color.FromArgb(40, 60, 90);
            this.dgvResults.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(22, 42, 72);
            this.dgvResults.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvResults.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(10, 132, 255);
            this.dgvResults.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(10, 25, 50);
            this.dgvResults.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvResults.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvResults.EnableHeadersVisualStyles = false;
            this.dgvResults.BorderStyle = BorderStyle.None;

            // ── Form ──────────────────────────────────────────────────────────
            this.ClientSize  = new System.Drawing.Size(780, 480);
            this.BackColor   = System.Drawing.Color.FromArgb(10, 25, 47);
            this.Text        = "Search Items";
            this.Font        = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition   = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.lblResultCount);
            this.Controls.Add(this.dgvResults);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
