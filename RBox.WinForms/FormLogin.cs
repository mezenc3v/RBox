using System;
using System.Drawing;
using System.Windows.Forms;

namespace RBox.WinForms
{
    public partial class FormLogin : Form
    {

        public string Login;
        public string Password;
        private const string DefaultLogin = @"your email";
        private const string DefaultPassword = @"your password";

        public FormLogin()
        {
            InitializeComponent();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            tbLogin.ForeColor = Color.Gray;
            tbPassword.ForeColor = Color.Gray;

            tbLogin.Text = DefaultLogin;
            tbPassword.Text = DefaultPassword;
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            var check = CheckFields();
            if (check)
            {
                Login = tbLogin.Text;
                Password = tbPassword.Text;
                Close();
            }
        }

        private void tbLogin_Click(object sender, EventArgs e)
        {
            FillWithDefault();
            if (tbLogin.Text == DefaultLogin)
            {
                tbLogin.Text = "";
            }

            tbLogin.ForeColor = DefaultForeColor;
        }

        private void tbPassword_Click(object sender, EventArgs e)
        {
            FillWithDefault();
            if (tbPassword.Text == DefaultPassword)
            {
                tbPassword.Text = "";
            }

            tbPassword.ForeColor = DefaultForeColor;
        }

        private void FillWithDefault()
        {
            if (tbLogin.Text == "")
            {
                tbLogin.ForeColor = Color.Gray;
                tbLogin.Text = DefaultLogin;
            }
            else if (tbLogin.Text != DefaultLogin)
            {
                tbLogin.ForeColor = DefaultForeColor;
            }
            if (tbPassword.Text == "")
            {
                tbPassword.ForeColor = Color.Gray;
                tbPassword.Text = DefaultPassword;
            }
            else if (tbPassword.Text != DefaultPassword)
            {
                tbPassword.ForeColor = DefaultForeColor;
            }
        }

        private bool CheckFields()
        {
            if (tbPassword.Text == DefaultPassword || tbLogin.Text == DefaultLogin
                || tbPassword.Text == "" || tbLogin.Text == "")
            {
                MessageBox.Show(@"Not all fields are filled");
                return false;
            }
            return true;
        }
    }
}
