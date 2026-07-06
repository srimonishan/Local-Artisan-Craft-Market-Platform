using System;
using System.Data;
using System.Windows.Forms;
using Craft_Market_Platform.Models;

namespace Craft_Market_Platform.Forms
{
    // SalesReportForm — Feature 10: Sales Report
    // One button triggers GenerateReport() on the SalesReport model.
    // The model (Abstraction) hides all SQL; the form only renders data.
    public class SalesReportForm : Form
    {
        // ── Controls ────────────────────────────────────────────────────────────
        private Label        lblTitle;
        private Button       btnGenerateReport;
        private Panel        pnlSummary;
        private Label        lblOrders;
        private Label        lblRevenue;
        private Label        lblItemsSold;
        private Label        lblTopProduct;
        private DataGridView dgvBreakdown;
        private Label        lblBreakdownTitle;

        private readonly SalesReport _report = new SalesReport();

        // ── Constructor ─────────────────────────────────────────────────────────
        public SalesReportForm()
        {
            InitializeComponent();
        }

        // ── Generate Report button ───────────────────────────────────────────────
        private void BtnGenerateReport_Click(object sender, EventArgs e)
        {
            try
            {
                btnGenerateReport.Enabled = false;
                btnGenerateReport.Text    = "Loading…";

                // One method call — all SQL is hidden inside SalesReport (Abstraction)
                SalesReport.SalesReportData data = _report.GenerateReport();

                // Populate summary labels
                lblOrders.Text     = $"📦  Total Orders:      {data.TotalOrders}";
                lblRevenue.Text    = $"💰  Total Revenue:     RM {data.TotalRevenue:N2}";
                lblItemsSold.Text  = $"🛒  Total Items Sold:  {data.TotalItemsSold}";
                lblTopProduct.Text = $"⭐  Top Product:       {data.TopProduct}";

                // Populate breakdown grid
                if (data.OrderBreakdown != null)
                    dgvBreakdown.DataSource = data.OrderBreakdown;

                pnlSummary.Visible    = true;
                dgvBreakdown.Visible  = true;
                lblBreakdownTitle.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to generate report: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnGenerateReport.Enabled = true;
                btnGenerateReport.Text    = "Generate Report";
            }
        }

        // ── InitializeComponent ──────────────────────────────────────────────────
        private void InitializeComponent()
        {
            this.lblTitle         = new Label();
            this.btnGenerateReport = new Button();
            this.pnlSummary       = new Panel();
            this.lblOrders        = new Label();
            this.lblRevenue       = new Label();
            this.lblItemsSold     = new Label();
            this.lblTopProduct    = new Label();
            this.dgvBreakdown     = new DataGridView();
            this.lblBreakdownTitle = new Label();

            this.pnlSummary.SuspendLayout();
            this.SuspendLayout();

            // ── Title ─────────────────────────────────────────────────────────
            this.lblTitle.AutoSize  = true;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location  = new System.Drawing.Point(20, 18);
            this.lblTitle.Text      = "📊 Sales Report";

            // ── Generate button ───────────────────────────────────────────────
            this.btnGenerateReport.Location  = new System.Drawing.Point(20, 65);
            this.btnGenerateReport.Size      = new System.Drawing.Size(180, 40);
            this.btnGenerateReport.Text      = "Generate Report";
            this.btnGenerateReport.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnGenerateReport.BackColor = System.Drawing.Color.FromArgb(10, 132, 255);
            this.btnGenerateReport.ForeColor = System.Drawing.Color.White;
            this.btnGenerateReport.FlatStyle = FlatStyle.Flat;
            this.btnGenerateReport.FlatAppearance.BorderSize = 0;
            this.btnGenerateReport.Cursor    = Cursors.Hand;
            this.btnGenerateReport.Click    += BtnGenerateReport_Click;

            // ── Summary panel ─────────────────────────────────────────────────
            this.pnlSummary.Location  = new System.Drawing.Point(20, 120);
            this.pnlSummary.Size      = new System.Drawing.Size(740, 130);
            this.pnlSummary.BackColor = System.Drawing.Color.FromArgb(18, 40, 75);
            this.pnlSummary.Visible   = false;
            // Round corners via paint (simple border)
            this.pnlSummary.BorderStyle = BorderStyle.FixedSingle;

            // Summary labels inside panel
            System.Drawing.Font summaryFont = new System.Drawing.Font("Segoe UI", 11F);

            this.lblOrders.AutoSize  = true;
            this.lblOrders.Font      = summaryFont;
            this.lblOrders.ForeColor = System.Drawing.Color.LightCyan;
            this.lblOrders.Location  = new System.Drawing.Point(15, 15);
            this.lblOrders.Text      = "📦  Total Orders:      —";

            this.lblRevenue.AutoSize  = true;
            this.lblRevenue.Font      = summaryFont;
            this.lblRevenue.ForeColor = System.Drawing.Color.LightGreen;
            this.lblRevenue.Location  = new System.Drawing.Point(15, 45);
            this.lblRevenue.Text      = "💰  Total Revenue:     —";

            this.lblItemsSold.AutoSize  = true;
            this.lblItemsSold.Font      = summaryFont;
            this.lblItemsSold.ForeColor = System.Drawing.Color.LightSalmon;
            this.lblItemsSold.Location  = new System.Drawing.Point(380, 15);
            this.lblItemsSold.Text      = "🛒  Total Items Sold:  —";

            this.lblTopProduct.AutoSize  = true;
            this.lblTopProduct.Font      = summaryFont;
            this.lblTopProduct.ForeColor = System.Drawing.Color.Khaki;
            this.lblTopProduct.Location  = new System.Drawing.Point(380, 45);
            this.lblTopProduct.Text      = "⭐  Top Product:       —";

            this.pnlSummary.Controls.Add(this.lblOrders);
            this.pnlSummary.Controls.Add(this.lblRevenue);
            this.pnlSummary.Controls.Add(this.lblItemsSold);
            this.pnlSummary.Controls.Add(this.lblTopProduct);

            // ── Per-product breakdown ─────────────────────────────────────────
            this.lblBreakdownTitle.AutoSize  = true;
            this.lblBreakdownTitle.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBreakdownTitle.ForeColor = System.Drawing.Color.Silver;
            this.lblBreakdownTitle.Location  = new System.Drawing.Point(20, 262);
            this.lblBreakdownTitle.Text      = "Product Breakdown";
            this.lblBreakdownTitle.Visible   = false;

            this.dgvBreakdown.Location              = new System.Drawing.Point(20, 285);
            this.dgvBreakdown.Size                  = new System.Drawing.Size(740, 220);
            this.dgvBreakdown.ReadOnly              = true;
            this.dgvBreakdown.SelectionMode         = DataGridViewSelectionMode.FullRowSelect;
            this.dgvBreakdown.AllowUserToAddRows    = false;
            this.dgvBreakdown.AllowUserToDeleteRows = false;
            this.dgvBreakdown.AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBreakdown.BackgroundColor       = System.Drawing.Color.FromArgb(18, 35, 60);
            this.dgvBreakdown.GridColor             = System.Drawing.Color.FromArgb(40, 60, 90);
            this.dgvBreakdown.DefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(22, 42, 72);
            this.dgvBreakdown.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvBreakdown.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(10, 132, 255);
            this.dgvBreakdown.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(10, 25, 50);
            this.dgvBreakdown.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvBreakdown.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvBreakdown.EnableHeadersVisualStyles = false;
            this.dgvBreakdown.BorderStyle = BorderStyle.None;
            this.dgvBreakdown.Visible     = false;

            // ── Form ──────────────────────────────────────────────────────────
            this.ClientSize  = new System.Drawing.Size(780, 520);
            this.BackColor   = System.Drawing.Color.FromArgb(10, 25, 47);
            this.Text        = "Sales Report";
            this.Font        = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition   = FormStartPosition.CenterParent;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnGenerateReport);
            this.Controls.Add(this.pnlSummary);
            this.Controls.Add(this.lblBreakdownTitle);
            this.Controls.Add(this.dgvBreakdown);

            this.pnlSummary.ResumeLayout(false);
            this.pnlSummary.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
