using System;
using System.Windows.Forms;
using RBox.Model;

namespace RBox.WinForms
{
    public partial class FormCreateShare : Form
    {
        public Guid UserId;
        public string UserLogin;

        public FormCreateShare()
        {
            InitializeComponent();
        }

        private void btnCreateShare_Click(object sender, EventArgs e)
        {
            try
            {
                var check = CheckUserName();
                if (check)
                {
                    var user = FindUser();
                    UserId = user.UserId;
                    UserLogin = user.UserLogin;
                    Close();
                }
            }
            catch
            {
                throw new Exception("User not found");
            }
        }

        private User FindUser()
        {
            var user = new User
            {
                UserLogin = tbUserLogin.Text
            };
            var client = new ServiceClient();
            return client.FindUser(user);
        }

        private bool CheckUserName()
        {
            if (tbUserLogin.Text == "")
            {
                MessageBox.Show(@"enter userlogin");
                return false;
            }
            return true;
        }
    }
}
