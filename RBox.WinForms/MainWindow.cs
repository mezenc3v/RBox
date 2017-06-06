using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using RBox.Model;
using RBox.WinForms.Extensions;

namespace RBox.WinForms
{
    public partial class MainWindow : Form
    {
        private ServiceClient _client;
        private File _currFile;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            ttHelpMessages.SetToolTip(btnAddFile, "Upload file");
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
            var formShare = new FormCreateShare
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(Location.X, Location.Y)
            };
            await Task.Run(() => formShare.ShowDialog());

            var user = new User
            {
                UserLogin = formShare.UserLogin
            };
            var client = new ServiceClient();
            try
            {
                var foundUser = client.FindUser(user);

                if (foundUser.UserId != _client.CurrentUser.UserId && foundUser.UserId != Guid.Empty)
                {
                    var share = new Share
                    {
                        FileId = _currFile.FileId,
                        UserId = foundUser.UserId
                    };
                    try
                    {
                        _client.CreateShare(share);
                        rtbLogs.AppendLine(@"You shared a file " + _currFile.Name + " with the user " + formShare.UserLogin, Color.DarkGreen);
                    }
                    catch
                    {
                        rtbLogs.AppendLine(@"Unable to share file", Color.Red);
                    }
                }
                else if (foundUser.UserId != Guid.Empty)
                {
                    rtbLogs.AppendLine(@"You are already the owner of this file", Color.Red);
                }
            }
            catch
            {
                rtbLogs.AppendLine(@"User not found", Color.Red);
            }

            

            UpdateUserInformation();
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you shure?", @"Delete file", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    rtbLogs.AppendLine(@"Deleting file, please wait...", Color.DarkGreen);
                    DisableAllButtons();
                    DisableMenu();

                    await Task.Run(() => _client.DeleteFile(_currFile.FileId));

                    EnableAllButtons();
                    EnableMenu();
                    rtbLogs.AppendLine(@"File " + _currFile.Name + " has been successfully deleted", Color.DarkGreen);
                }
                catch
                {
                    rtbLogs.AppendLine(@"An error occurred while deleting the file", Color.Red);
                }
            }
            UpdateUserInformation();
        }

        private async void btnDownload_Click(object sender, EventArgs e)
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
                            rtbLogs.AppendLine(@"Downloading file, please wait...", Color.DarkGreen);

                            DisableAllButtons();
                            DisableMenu();
                            await Task.Run(() =>
                            {
                                var content = _client.DownloadFile(item.FileId);
                                System.IO.File.WriteAllBytes(dialog.FileName, content);
                            });
                            EnableAllButtons();
                            EnableMenu();

                            rtbLogs.AppendLine(@"File " + item.Name + " has been successfully downloaded", Color.DarkGreen);
                        }
                    }
                }
            }
            catch
            {
                rtbLogs.AppendLine(@"File not found", Color.Red);
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
            DisableAllButtons();
            DisableMenu();

            var formLogin = new FormLogin
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(Location.X, Location.Y)
            };

            await Task.Run(() =>
            {
                formLogin.ShowDialog();
            });

            if (formLogin.Login != null && formLogin.Password != null)
            {
                var client = new ServiceClient();
                var user = new User
                {
                    UserLogin = formLogin.Login,
                    Password = formLogin.Password
                };

                try
                {
                    client.LoginUser(user);
                    rtbLogs.AppendLine(@"User " + _client.CurrentUser.UserLogin + " is logged in", Color.DarkGreen);
                }
                catch
                {
                    rtbLogs.AppendLine(@"Specified user does not exist", Color.Red);
                }
            }

            EnableAllButtons();
            EnableMenu();
            UpdateUserInformation();
        }

        private async void tsmItemRegister_Click(object sender, EventArgs e)
        {
            var formRegister = new FormRegister
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(Location.X, Location.Y)
            };

            DisableAllButtons();
            DisableMenu();

            await Task.Run(() => formRegister.ShowDialog());

            if (formRegister.Login != null)
            {
                try
                {
                    var user = new User
                    {
                        Name = formRegister.UserName,
                        Password = formRegister.Password,
                        UserLogin = formRegister.Login
                    };
                    var client = new ServiceClient();
                    client.CreateUser(user);

                    rtbLogs.AppendLine(@"User " + _client.CurrentUser.UserLogin + " created", Color.DarkGreen);
                    rtbLogs.AppendLine(@"User " + _client.CurrentUser.UserLogin + " was logged in", Color.DarkGreen);
                }
                catch
                {
                    rtbLogs.AppendLine(@"User already exists", Color.Red);
                }
            }

            EnableAllButtons();
            EnableMenu();

            UpdateUserInformation();
        }

        private void tsmItemCloseUser_Click(object sender, EventArgs e)
        {
            rtbLogs.AppendLine(@"User " + _client.CurrentUser.UserLogin + " is logged out", Color.DarkGreen);
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
            _currFile = (File)LbFiles.SelectedItem;
        }

        private void LbFiles_DrawItem(object sender, DrawItemEventArgs e)
        {
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
            {
                e.Graphics.FillRectangle(new SolidBrush(ColorTranslator.FromHtml("#BCD1EA")), e.Bounds);
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(LbFiles.BackColor), e.Bounds);
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
                {
                    ttHelpMessages.SetToolTip(lb, toolTipString);
                }
            }
            else
            {
                ttHelpMessages.Hide(lb);
            }
        }

        private void MainMenu_MouseMove(object sender, MouseEventArgs e)
        {
            CheckMenu();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DisableAllButtons();
            DisableMenu();
            UpdateUserInformation();
            EnableMenu();
            EnableAllButtons();
            CheckMenu();
            CheckButtons();
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
                        await UploadFile(openFileDialog.FileName, openFileDialog.SafeFileName);
                    }
                }
            }
            catch
            {
                rtbLogs.AppendLine(@"Error uploading file", Color.Red);
            }

            UpdateUserInformation();
        }

        private async void LbFiles_DragDrop(object sender, DragEventArgs e)
        {
            var filePath = ((string[])e.Data.GetData(DataFormats.FileDrop, false)).Last();
            var fileName = System.IO.Path.GetFileName(filePath);

            try
            {
                await UploadFile(filePath, fileName);
            }
            catch
            {
                rtbLogs.AppendLine(@"Error uploading file", Color.Red);
            }

            UpdateUserInformation();
        }

        private void LbFiles_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = _client?.CurrentUser != null ? DragDropEffects.Move : DragDropEffects.None;
        }

        private async Task UploadFile(string filePath, string fileName)
        {
            var fileContent = System.IO.File.ReadAllBytes(filePath);

            var formFileDescription = new FormFileDescription
            {
                StartPosition = FormStartPosition.Manual,
                Location = new Point(Location.X, Location.Y)
            };

            DisableAllButtons();
            DisableMenu();

            await Task.Run(() => formFileDescription.ShowDialog());

            EnableAllButtons();
            EnableMenu();

            var file = new File
            {
                Description = formFileDescription.FileDescription,
                Name = fileName,
                UserId = _client.CurrentUser.UserId
            };

            var fileId = _client.CreateFile(file);

            rtbLogs.AppendLine(@"Uploading file, please wait...", Color.DarkGreen);

            DisableAllButtons();
            DisableMenu();

            await Task.Run(() => _client.UploadFileContent(fileId, fileContent));

            rtbLogs.AppendLine(@"File successfully uploaded", Color.DarkGreen);

            EnableAllButtons();
            EnableMenu();
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
                DisableAllButtons();
            }
            else
            {
                EnableAllButtons();

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

        private void DisableAllButtons()
        {
            btnAddFile.Enabled = false;
            btnDelete.Enabled = false;
            btnDownload.Enabled = false;
            btnShare.Enabled = false;
            btnUpdate.Enabled = false;
        }

        private void EnableAllButtons()
        {
            btnAddFile.Enabled = true;
            btnDelete.Enabled = true;
            btnDownload.Enabled = true;
            btnShare.Enabled = true;
            btnUpdate.Enabled = true;
        }

        private void DisableMenu()
        {
            MainMenu.Enabled = false;
        }

        private void EnableMenu()
        {
            MainMenu.Enabled = true;
        }
    }
}
