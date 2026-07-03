namespace Craft_Market_Platform.Forms
{
    partial class DashboardForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new System.Drawing.Point(30, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(100, 30);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Welcome";
            // 
            // btnAddCraftItem
            // 
            this.btnAddCraftItem = new System.Windows.Forms.Button();
            this.btnAddCraftItem.Location = new System.Drawing.Point(30, 80);
            this.btnAddCraftItem.Name = "btnAddCraftItem";
            this.btnAddCraftItem.Size = new System.Drawing.Size(140, 30);
            this.btnAddCraftItem.TabIndex = 1;
            this.btnAddCraftItem.Text = "Add Craft Item";
            this.btnAddCraftItem.UseVisualStyleBackColor = true;
            this.btnAddCraftItem.Click += new System.EventHandler(this.btnAddCraftItem_Click);
            // 
            // dgvProducts
            // 
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.dgvProducts.Location = new System.Drawing.Point(30, 130);
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.Size = new System.Drawing.Size(740, 250);
            this.dgvProducts.TabIndex = 2;
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;

            // 
            // btnDelete
            // 
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDelete.Location = new System.Drawing.Point(190, 80);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 30);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.BackColor = System.Drawing.Color.DarkRed;
            this.btnDelete.ForeColor = System.Drawing.Color.White;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);

            // 
            // Dashboard counts labels
            // 
            this.lblTotalProducts = new System.Windows.Forms.Label();
            this.lblTotalProducts.AutoSize = true;
            this.lblTotalProducts.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalProducts.ForeColor = System.Drawing.Color.White;
            this.lblTotalProducts.Location = new System.Drawing.Point(520, 20);
            this.lblTotalProducts.Name = "lblTotalProducts";
            this.lblTotalProducts.Text = "Products: 0";

            this.lblTotalArtisans = new System.Windows.Forms.Label();
            this.lblTotalArtisans.AutoSize = true;
            this.lblTotalArtisans.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalArtisans.ForeColor = System.Drawing.Color.White;
            this.lblTotalArtisans.Location = new System.Drawing.Point(520, 50);
            this.lblTotalArtisans.Name = "lblTotalArtisans";
            this.lblTotalArtisans.Text = "Artisans: 0";

            this.lblTotalOrders = new System.Windows.Forms.Label();
            this.lblTotalOrders.AutoSize = true;
            this.lblTotalOrders.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalOrders.ForeColor = System.Drawing.Color.White;
            this.lblTotalOrders.Location = new System.Drawing.Point(520, 80);
            this.lblTotalOrders.Name = "lblTotalOrders";
            this.lblTotalOrders.Text = "Orders: 0";
            // 
            // DashboardForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.BackColor = System.Drawing.Color.FromArgb(10, 25, 47); // dark navy background
            this.Controls.Add(this.lblWelcome);
            this.Controls.Add(this.btnAddCraftItem);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.lblTotalProducts);
            this.Controls.Add(this.lblTotalArtisans);
            this.Controls.Add(this.lblTotalOrders);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "DashboardForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dashboard";
            this.Load += new System.EventHandler(this.DashboardForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblWelcome;
        private System.Windows.Forms.Button btnAddCraftItem;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lblTotalProducts;
        private System.Windows.Forms.Label lblTotalArtisans;
        private System.Windows.Forms.Label lblTotalOrders;
    }
}
