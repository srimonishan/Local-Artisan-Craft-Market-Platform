using System;
using System.Windows.Forms;

namespace Craft_Market_Platform.Forms
{
    public partial class DashboardForm : Form
    {
        private int _adminId;
        private string _fullName;

        public DashboardForm(int adminId, string fullName)
        {
            InitializeComponent();
            _adminId = adminId;
            _fullName = fullName;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {
            lblWelcome.Text = $"Welcome, {_fullName}!";
        }
    }
}
