using System;
using System.Windows.Forms;
using RBox.Model;

namespace RBox.WinForms
{
    public partial class FormCreateShare : Form
    {
        public Guid UserId;

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
                    Close();
                }
            }
            catch
            {
                MessageBox.Show(@"Error: User not found", @"Failed to share file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private User FindUser()
        {
            try
            {
                var user = new User
                {
                    UserLogin = tbUserLogin.Text
                };
                var client = new ServiceClient();
                return client.FindUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + ex.Message);
                throw;
            }
        }

        private bool CheckUserName()
        {
            if (tbUserLogin.Text == "")
            {
                MessageBox.Show(@"enter username");
                return false;
            }
            return true;
        }
    }
}
