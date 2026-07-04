using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Craft_Market_Platform.Database;

namespace Craft_Market_Platform.Forms
{
    // Small popup to quickly add an artisan without leaving the product form
    public class AddArtisanQuickForm : Form
    {
        private Label lblFullName;
        private TextBox txtFullName;
        private Label lblEmail;
        private TextBox txtEmail;
        private Label lblPhone;
        private TextBox txtPhone;
        private Label lblAddress;
        private TextBox txtAddress;
        private Button btnSave;
        private Button btnCancel;

        private DatabaseConnection _db = new DatabaseConnection();

        // Expose the newly created artisan id to the caller
        public int NewArtisanId { get; private set; }

        public AddArtisanQuickForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.lblFullName = new Label();
            this.txtFullName = new TextBox();
            this.lblEmail = new Label();
            this.txtEmail = new TextBox();
            this.lblPhone = new Label();
            this.txtPhone = new TextBox();
            this.lblAddress = new Label();
            this.txtAddress = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            // FullName
            this.lblFullName.AutoSize = true;
            this.lblFullName.Location = new Point(12, 15);
            this.lblFullName.Text = "Full Name";
            this.txtFullName.Location = new Point(110, 12);
            this.txtFullName.Width = 260;

            // Email
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new Point(12, 50);
            this.lblEmail.Text = "Email";
            this.txtEmail.Location = new Point(110, 47);
            this.txtEmail.Width = 260;

            // Phone
            this.lblPhone.AutoSize = true;
            this.lblPhone.Location = new Point(12, 85);
            this.lblPhone.Text = "Phone";
            this.txtPhone.Location = new Point(110, 82);
            this.txtPhone.Width = 260;

            // Address
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new Point(12, 120);
            this.lblAddress.Text = "Address";
            this.txtAddress.Location = new Point(110, 117);
            this.txtAddress.Width = 260;
            this.txtAddress.Height = 60;
            this.txtAddress.Multiline = true;

            // Save
            this.btnSave.Text = "Save";
            this.btnSave.Location = new Point(110, 190);
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.BackColor = Color.FromArgb(10, 132, 255);
            this.btnSave.ForeColor = Color.White;
            this.btnSave.FlatStyle = FlatStyle.Flat;
            this.btnSave.FlatAppearance.BorderSize = 0;
            this.btnSave.Click += BtnSave_Click;

            // Cancel
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Location = new Point(230, 190);
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.Click += (s, e) => this.DialogResult = DialogResult.Cancel;

            // Form
            this.ClientSize = new Size(390, 235);
            this.Controls.Add(this.lblFullName);
            this.Controls.Add(this.txtFullName);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Add Artisan";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var fullName = txtFullName.Text.Trim();
            var email = txtEmail.Text.Trim();
            var phone = txtPhone.Text.Trim();
            var address = txtAddress.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                MessageBox.Show(this, "Please enter full name.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = _db.GetConnection())
                using (var cmd = conn.CreateCommand())
                {
                    // Insert artisan and return new id
                    cmd.CommandText = "INSERT INTO Artisans (FullName, Email, Phone, Address, Status, CreatedAt) VALUES (@fn, @em, @ph, @addr, @st, GETDATE()); SELECT CAST(SCOPE_IDENTITY() AS int);";
                    cmd.Parameters.AddWithValue("@fn", fullName);
                    cmd.Parameters.AddWithValue("@em", (object)email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ph", (object)phone ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@addr", (object)address ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@st", "Approved");
                    conn.Open();
                    var result = cmd.ExecuteScalar();
                    NewArtisanId = result != null ? Convert.ToInt32(result) : 0;
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Failed to add artisan: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
