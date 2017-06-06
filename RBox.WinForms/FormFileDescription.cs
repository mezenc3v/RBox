using System;
using System.Windows.Forms;

namespace RBox.WinForms
{
    public partial class FormFileDescription : Form
    {
        public string FileDescription;

        public FormFileDescription()
        {
            InitializeComponent();
        }

        private void btnUploadFile_Click(object sender, EventArgs e)
        {
            FileDescription = tbFileDescription.Text;
            Close();
        }
    }
}
