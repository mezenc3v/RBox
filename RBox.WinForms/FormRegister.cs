using System;
using System.Drawing;
using System.Windows.Forms;
using RBox.Model;

namespace RBox.WinForms
{
    public partial class FormRegister : Form
    {
        private const string DefaultName = @"your name";
        private const string DefaultLogin = @"your email";
        private const string DefaultPassword = @"your password";
        private const string DefaultRepeatPassword = @"repeat your password";

        public FormRegister()
        {
            InitializeComponent();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {
            tbName.ForeColor = Color.Gray;
            tbLogin.ForeColor = Color.Gray;
            tbPassword.ForeColor = Color.Gray;
            tbRepeatPassword.ForeColor = Color.Gray;

            tbName.Text = DefaultName;
            tbLogin.Text = DefaultLogin;
            tbPassword.Text = DefaultPassword;
            tbRepeatPassword.Text = DefaultRepeatPassword;
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            var check = CheckFields();
            if (check)
            {
                CreateUser();
                Close();
            }
        }

        private void CreateUser()
        {
            try
            {
                var user = new User
                {
                    Name = tbName.Text,
                    Password = tbPassword.Text,
                    UserLogin = tbLogin.Text
                };
                var client = new ServiceClient();
                client.CreateUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + ex.Message);
            }
        }

        private bool CheckFields()
        {
            if (tbPassword.Text == DefaultPassword || tbLogin.Text == DefaultLogin
                || tbName.Text == DefaultName || tbRepeatPassword.Text == DefaultRepeatPassword
                || tbPassword.Text == "" || tbLogin.Text == "" || tbName.Text == "")
            {
                MessageBox.Show(@"Not all fields are filled");
                return false;
            }
            if (tbPassword.Text != tbRepeatPassword.Text)
            {
                MessageBox.Show(@"Passwords do not match");
                return false;
            }
            return true;
        }

        private void FillWithDefault()
        {
            if (tbName.Text == "")
            {
                tbName.Text = DefaultName;
                tbName.ForeColor = Color.Gray;
            }
            else if (tbName.Text != DefaultName)
            {
                tbName.ForeColor = DefaultForeColor;
            }
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
            if (tbRepeatPassword.Text == "")
            {
                tbRepeatPassword.ForeColor = Color.Gray;
                tbRepeatPassword.Text = DefaultRepeatPassword;
            }
            else if (tbRepeatPassword.Text != DefaultRepeatPassword)
            {
                tbRepeatPassword.ForeColor = DefaultForeColor;
            }
        }

        private void tbName_Click(object sender, EventArgs e)
        {
            FillWithDefault();
            if (tbName.Text == DefaultName)
            {
                tbName.Text = "";
            }

            tbName.ForeColor = DefaultForeColor;
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

        private void tbRepeatPassword_Click(object sender, EventArgs e)
        {
            FillWithDefault();
            if (tbRepeatPassword.Text == DefaultRepeatPassword)
            {
                tbRepeatPassword.Text = "";
            }
            tbRepeatPassword.ForeColor = DefaultForeColor;
        }
    }
}