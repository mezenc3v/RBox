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
            LbFiles.ItemHeight = 30;
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            ttHelpMessages.SetToolTip(btnAddFile, "Add file");
            ttHelpMessages.SetToolTip(btnDelete, "Delete file");
            ttHelpMessages.SetToolTip(btnDownload, "Download file");
            ttHelpMessages.SetToolTip(btnShare, "Share file");
            ttHelpMessages.SetToolTip(btnUpdate, "Update file list");

            MainMenu.BackColor = ColorTranslator.FromHtml("#CBDCEF");
            BackColor = MainMenu.BackColor = ColorTranslator.FromHtml("#CBDCEF");

            _client = new ServiceClient();
            CheckMenu();
            CheckButtons();
        }

        private async void btnShare_Click(object sender, EventArgs e)
        {
            try
            {
                var formShare = new FormCreateShare
                {
                    StartPosition = FormStartPosition.Manual,
                    Location = new Point(Location.X, Location.Y)
                };
                await Task.Run(() => formShare.ShowDialog());
                if (formShare.UserId != _client.CurrentUser.UserId && formShare.UserId != Guid.Empty)
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
            UpdateUserInformation();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you shure?", @"Delete file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _client.DeleteFile(_currFile);
            }
            UpdateUserInformation();
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
            var aboutForm = new FormAbout
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(Location.X, Location.Y)
            };
            aboutForm.Show();
        }

        private async void tsmItemLogin_Click(object sender, EventArgs e)
        {
            var formLogin = new FormLogin
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(Location.X, Location.Y)
            };

            await Task.Run(() =>
            {
                formLogin.ShowDialog();
            });
            UpdateUserInformation();
        }

        private async void tsmItemRegister_Click(object sender, EventArgs e)
        {
            var formRegister = new FormRegister
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(Location.X, Location.Y)
            };
            await Task.Run(() => formRegister.ShowDialog());
            UpdateUserInformation();
        }

        private void tsmItemCloseUser_Click(object sender, EventArgs e)
        {
            _client?.CloseUser();
            UpdateUserInformation();
        }

        private void tsmItemAboutExit_Click(object sender, EventArgs e)
        {
            _client?.CloseUser();
            Close();
        }

        private void LbFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currFile = ((File)LbFiles.SelectedItem).FileId;
        }

        private void LbFiles_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#BCD1EA")), e.Bounds);
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

                if (ttHelpMessages.GetToolTip(lb) != toolTipString)
                    ttHelpMessages.SetToolTip(lb, toolTipString);
            }
            else
                ttHelpMessages.Hide(lb);
        }

        private void MainMenu_MouseMove(object sender, MouseEventArgs e)
        {
            CheckMenu();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateUserInformation();
        }

        private async void btnAddFile_Click(object sender, EventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    if (openFileDialog.SafeFileName != null)
                    {
                        var fileContent = System.IO.File.ReadAllBytes(openFileDialog.FileName);

                        var formFileDescription = new FormFileDescription
                        {
                            StartPosition = FormStartPosition.Manual,
                            Location = new Point(Location.X, Location.Y)
                        };

                        await Task.Run(() => formFileDescription.ShowDialog());
                        var file = new File
                        {
                            Description = formFileDescription.FileDescription,
                            Name = openFileDialog.SafeFileName,
                            UserId = _client.CurrentUser.UserId
                        };

                        var fileId = _client.CreateFile(file);
                        _client.UploadFileContent(fileId, fileContent);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error: " + Environment.NewLine + ex.Message, @"File was not uploaded", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            UpdateUserInformation();
        }

        private void CheckMenu()
        {
            if (_client?.CurrentUser == null)
            {
                tsmItemLogOut.Enabled = false;

                tsmItemRegister.Enabled = true;
                tsmItemLogin.Enabled = true;
            }
            else
            {
                tsmItemLogOut.Enabled = true;

                tsmItemRegister.Enabled = false;
                tsmItemLogin.Enabled = false;
            }
        }

        private void CheckButtons()
        {
            if (_client?.CurrentUser == null)
            {
                btnAddFile.Enabled = false;
                btnDelete.Enabled = false;
                btnDownload.Enabled = false;
                btnShare.Enabled = false;
                btnUpdate.Enabled = false;
            }
            else
            {
                btnAddFile.Enabled = true;
                btnDelete.Enabled = true;
                btnDownload.Enabled = true;
                btnShare.Enabled = true;
                btnUpdate.Enabled = true;
            }

            if (LbFiles.Items.Count == 0)
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

        private void UpdateUserInformation()
        {
            if (_client?.CurrentUser != null)
            {
                var files = _client?.GetUserFiles();
                var shares = _client?.GetSharedFiles();

                LbFiles.DataSource = files?.Concat(shares).ToArray();

                labelCurrUser.Text = _client.CurrentUser.UserLogin;
            }
            else
            {
                LbFiles.DataSource = new File[0];
                labelCurrUser.Text = "";
            }
            CheckButtons();
        }
    }
}
