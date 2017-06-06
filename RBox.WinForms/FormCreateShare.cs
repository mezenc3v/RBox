using System;
using System.Drawing;
using System.Windows.Forms;

namespace RBox.WinForms
{
    public partial class FormCreateShare : Form
    {
        public string UserLogin;

        public FormCreateShare()
        {
            InitializeComponent();
        }

        private void btnCreateShare_Click(object sender, EventArgs e)
        {
            var check = CheckUserName();
            if (check)
            {
                UserLogin = tbUserLogin.Text;
                Close();
            }
        }

        private bool CheckUserName()
        {
            if (tbUserLogin.Text == "")
            {
                MessageBox.Show(@"Enter user's login");
                return false;
            }
            return true;
        }

        private void FormCreateShare_Load(object sender, EventArgs e)
        {
            BackColor = ColorTranslator.FromHtml("#CBDCEF");
        }
    }
}
