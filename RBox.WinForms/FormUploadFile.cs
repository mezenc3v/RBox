using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace RBox.WinForms
{
    public partial class FormUploadFile : Form
    {
        public byte[] FileContent;
        public string FileName;

        public FormUploadFile()
        {
            InitializeComponent();
        }

        private void pbUpload_Paint(object sender, PaintEventArgs e)
        {
            if (pbUpload.Image == null)
            {
                var drawFormatText = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };

                var backgroundImage = Properties.Resources.Upload;
                e.Graphics.DrawImage(backgroundImage, (float)backgroundImage.Height / 2,
                    (float)backgroundImage.Height / 2);
                e.Graphics.DrawString("Click or drag files here", new Font("Bebas Neue Book Regular", 16),
                    new SolidBrush(Color.Gray), (float)pbUpload.Height / 2, (float)pbUpload.Width / 8,
                    drawFormatText);

            }
        }

        private void FormUploadFile_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void FormUploadFile_DragDrop(object sender, DragEventArgs e)
        {
            var x = PointToClient(new Point(e.X, e.Y)).X;
            var y = PointToClient(new Point(e.X, e.Y)).Y;

            if (x >= pbUpload.Location.X && x <= pbUpload.Location.X + pbUpload.Width
                && y >= pbUpload.Location.Y && y <= pbUpload.Location.Y + pbUpload.Height)
            {
                FileContent = (byte[])e.Data.GetData(DataFormats.FileDrop);
            }
        }

        private void pbUpload_Click(object sender, EventArgs e)
        {
            var newThread = new Thread(() =>
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileContent = File.ReadAllBytes(openFileDialog.FileName);
                    FileName = openFileDialog.SafeFileName;
                }
                Invoke((Action)Close);
            });
            newThread.SetApartmentState(ApartmentState.STA);
            newThread.Start();
        }
    }
}

