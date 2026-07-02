using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Craft_Market_Platform.Database;

namespace Craft_Market_Platform.Forms
{
    public partial class LoginForm : Form
    {
        private readonly DatabaseConnection _dbConnection;
        private readonly Color _primaryColor = Color.FromArgb(37, 99, 235);
        private readonly Color _dangerColor = Color.FromArgb(239, 68, 68);
        private readonly Color _successColor = Color.FromArgb(16, 185, 129);
        private readonly Color _accentColor = Color.FromArgb(245, 158, 11);
        private readonly Color _bgColor = Color.FromArgb(15, 23, 34); // #0F172A

        public LoginForm()
        {
            InitializeComponent();
            _dbConnection = new DatabaseConnection();
            ApplyStyles();
        }

        private void ApplyStyles()
        {
            this.BackColor = _bgColor;
            pnlLeft.BackColor = _primaryColor;

            btnLogin.BackColor = _successColor;
            btnLogin.ForeColor = Color.White;
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;

            btnClear.BackColor = _dangerColor;
            btnClear.ForeColor = Color.White;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.FlatAppearance.BorderSize = 0;

            txtUsername.BackColor = Color.FromArgb(30, 41, 59);
            txtUsername.ForeColor = Color.White;
            txtUsername.BorderStyle = BorderStyle.FixedSingle;

            txtPassword.BackColor = Color.FromArgb(30, 41, 59);
            txtPassword.ForeColor = Color.White;
            txtPassword.BorderStyle = BorderStyle.FixedSingle;

            // Handle enter key to login
            this.AcceptButton = btnLogin;
        }

        // Simulating rounded corners for buttons by overriding paint slightly or using standard flat controls.
        // For production simplicity and bug-free scaling, Flat styled buttons with proper colors are preferred in WinForms.
        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                lblError.Text = "Username and Password are required.";
                lblError.Visible = true;
                return;
            }

            lblError.Visible = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                using (var connection = _dbConnection.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT AdminID, FullName FROM Admins WHERE Username=@Username AND PasswordHash=@PasswordHash";

                    using (var cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@Username", username);
                        cmd.Parameters.AddWithValue("@PasswordHash", password); 

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int adminId = Convert.ToInt32(reader["AdminID"]);
                                string fullName = reader["FullName"].ToString();

                                MessageBox.Show("Login Successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                DashboardForm dashboard = new DashboardForm(adminId, fullName);
                                this.Hide();
                                dashboard.ShowDialog();
                                this.Close();
                            }
                            else
                            {
                                lblError.Text = "Invalid Username or Password.";
                                lblError.Visible = true;
                                txtPassword.Clear();
                                txtPassword.Focus();
                            }
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Database error: {sqlEx.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUsername.Clear();
            txtPassword.Clear();
            lblError.Visible = false;
            txtUsername.Focus();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        // Custom rounded styling for panels and buttons
        private void RoundControl(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radius, radius), 180, 90);
            path.AddArc(new Rectangle(control.Width - radius, 0, radius, radius), 270, 90);
            path.AddArc(new Rectangle(control.Width - radius, control.Height - radius, radius, radius), 0, 90);
            path.AddArc(new Rectangle(0, control.Height - radius, radius, radius), 90, 90);
            path.CloseFigure();
            control.Region = new Region(path);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Apply rounded corners after sizing
            RoundControl(btnLogin, 10);
            RoundControl(btnClear, 10);

            // Database connection check requested by user
            if (_dbConnection.TestConnection())
            {
                MessageBox.Show(
                    "Database Connected Successfully!",
                    "Success",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Database Connection Failed!",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }
}
