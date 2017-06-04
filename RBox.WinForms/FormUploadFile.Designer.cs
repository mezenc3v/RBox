namespace RBox.WinForms
{
    partial class FormUploadFile
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbUpload = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pbUpload)).BeginInit();
            this.SuspendLayout();
            // 
            // pbUpload
            // 
            this.pbUpload.Location = new System.Drawing.Point(12, 12);
            this.pbUpload.Name = "pbUpload";
            this.pbUpload.Size = new System.Drawing.Size(260, 237);
            this.pbUpload.TabIndex = 0;
            this.pbUpload.TabStop = false;
            this.pbUpload.Click += new System.EventHandler(this.pbUpload_Click);
            this.pbUpload.Paint += new System.Windows.Forms.PaintEventHandler(this.pbUpload_Paint);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "fileName";
            // 
            // FormUploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 261);
            this.Controls.Add(this.pbUpload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormUploadFile";
            this.Text = "Upload file";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FormUploadFile_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FormUploadFile_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pbUpload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbUpload;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
    }
}