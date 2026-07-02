using System;
using System.Windows.Forms;
using Craft_Market_Platform.Database;

namespace Craft_Market_Platform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            DatabaseConnection db = new DatabaseConnection();

            if (db.TestConnection())
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