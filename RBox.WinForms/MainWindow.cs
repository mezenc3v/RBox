using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RBox.Model;

namespace RBox.WinForms
{
    public partial class MainWindow : Form
    {
        private ServiceClient _client;
        private Guid _currFile;

        public MainWindow()
        {
            InitializeComponent();
            LbFiles.DisplayMember = "Name";
            LbFiles.ItemHeight = 30;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            _client = new ServiceClient();
            CheckMenu();
            CheckButtons();
        }

        private async void btnShare_Click(object sender, EventArgs e)
        {
            try
            {
                var formShare = new FormCreateShare();
                await Task.Run(() => formShare.ShowDialog());
                if (formShare.UserId != _client.UserId && formShare.UserId != Guid.Empty)
                {
                    var share = new Share
                    {
                        FileId = _currFile,
                        UserId = formShare.UserId
                    };
                    _client.CreateShare(share);
                }
                else if (formShare.UserId != Guid.Empty)
                {
                    MessageBox.Show(@"You are already the owner of this file", @"Failed to share file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + Environment.NewLine + ex.Message, @"Failed to share file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            UpdateUserData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you shure?", @"Delete file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _client.DeleteFile(_currFile);
            }
            UpdateUserData();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                var item = (File)LbFiles.SelectedItem;
                if (item != null)
                {
                    using (var dialog = new SaveFileDialog())
                    {
                        dialog.FileName = item.Name;
                        if (dialog.ShowDialog() == DialogResult.OK)
                        {
                            var content = _client.DownloadFile(item.FileId);
                            System.IO.File.WriteAllBytes(dialog.FileName, content);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Error: " + Environment.NewLine + exception.Message, @"Failed to download file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsmItemAbout_Click(object sender, EventArgs e)
        {
            var aboutForm = new FormAbout();
            aboutForm.Show();
        }

        private async void tsmItemLogin_Click(object sender, EventArgs e)
        {
            var formLogin = new FormLogin();
            await Task.Run(() => formLogin.ShowDialog());
            UpdateUserData();
        }

        private async void tsmItemRegister_Click(object sender, EventArgs e)
        {
            var formRegister = new FormRegister();
            await Task.Run(() => formRegister.ShowDialog());
            UpdateUserData();
        }

        private async void tsmItemAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                var formUploadFile = new FormUploadFile();
                var formFileDescription = new FormFileDescription();

                await Task.Run(() => formUploadFile.ShowDialog());
                if (formUploadFile.FileName != null)
                {
                    var fileContent = formUploadFile.FileContent;

                    await Task.Run(() => formFileDescription.ShowDialog());
                    var file = new File
                    {
                        Description = formFileDescription.FileDescription,
                        Name = formUploadFile.FileName,
                        UserId = _client.UserId
                    };

                    var fileId = _client.CreateFile(file);
                    _client.UploadFileContent(fileId, fileContent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + Environment.NewLine + ex.Message, @"File was not uploaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateUserData();
        }

        private void tsmItemCloseUser_Click(object sender, EventArgs e)
        {
            _client?.CloseUser();
            UpdateUserData();
        }

        private void tsmItemAboutExit_Click(object sender, EventArgs e)
        {
            _client?.CloseUser();
            Close();
        }

        private void UpdateUserData()
        {
            if (_client == null || _client.UserId == Guid.Empty)
            {
                var files = _client?.GetUserFiles();
                var shares = _client?.GetSharedFiles();

                LbFiles.DataSource = files?.Concat(shares).ToArray();
            }
            else
            {
                LbFiles.DataSource = new File[0];
            }
            CheckButtons();
        }

        private void LbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currFile = ((File)LbFiles.SelectedItem).FileId;
        }

        private void LbFiles_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(Brushes.LightSteelBlue, e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(DefaultBackColor), e.Bounds);
            }

            var font = new Font(FontFamily.GenericSansSerif, Font.Size + 3, FontStyle.Regular);
            if (LbFiles.Items.Count > 0)
            {
                e.Graphics.DrawString(((File)LbFiles.Items[e.Index]).Name,
                    font, Brushes.Black, e.Bounds.X, e.Bounds.Y);
            }

            e.DrawFocusRectangle();
        }

        private void LbFiles_MouseMove(object sender, MouseEventArgs e)
        {
            var lb = (ListBox)sender;
            var index = lb.IndexFromPoint(e.Location);

            if (index >= 0 && index < lb.Items.Count)
            {
                var toolTipString = ((File)lb.Items[index]).Description;

                if (toolTip.GetToolTip(lb) != toolTipString)
                    toolTip.SetToolTip(lb, toolTipString);
            }
            else
                toolTip.Hide(lb);
        }

        private void MainMenu_MouseMove(object sender, MouseEventArgs e)
        {
            CheckMenu();
        }

        private void CheckMenu()
        {
            if (_client == null || _client.UserId == Guid.Empty)
            {
                tsmItemCloseUser.Enabled = false;
                tsmItemAddFile.Enabled = false;

                tsmItemRegister.Enabled = true;
                tsmItemLogin.Enabled = true;
            }
            else
            {
                tsmItemCloseUser.Enabled = true;
                tsmItemAddFile.Enabled = true;

                tsmItemRegister.Enabled = false;
                tsmItemLogin.Enabled = false;
            }
        }

        private void CheckButtons()
        {
            if (_client == null || _client.UserId == Guid.Empty)
            {
                btnDelete.Enabled = false;
                btnDownload.Enabled = false;
                btnShare.Enabled = false;
            }
            else
            {
                btnDelete.Enabled = true;
                btnDownload.Enabled = true;
                btnShare.Enabled = true;
            }
        }
    }
}
