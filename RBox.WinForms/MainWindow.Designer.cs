namespace RBox.WinForms
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            this.LbFiles = new System.Windows.Forms.ListBox();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.tsmUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemRegister = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemCloseUser = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemAboutExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemAddFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.btnDownload = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnShare = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbFiles
            // 
            this.LbFiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LbFiles.FormattingEnabled = true;
            this.LbFiles.Location = new System.Drawing.Point(12, 27);
            this.LbFiles.Name = "LbFiles";
            this.LbFiles.Size = new System.Drawing.Size(312, 303);
            this.LbFiles.TabIndex = 0;
            this.LbFiles.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LbFiles_DrawItem);
            this.LbFiles.SelectedIndexChanged += new System.EventHandler(this.LbFiles_SelectedIndexChanged);
            this.LbFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LbFiles_MouseMove);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUser,
            this.tsmFiles,
            this.tsmHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(337, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            this.MainMenu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainMenu_MouseMove);
            // 
            // tsmUser
            // 
            this.tsmUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmItemLogin,
            this.tsmItemRegister,
            this.tsmItemCloseUser,
            this.tsmItemAboutExit});
            this.tsmUser.Name = "tsmUser";
            this.tsmUser.Size = new System.Drawing.Size(42, 20);
            this.tsmUser.Text = "User";
            // 
            // tsmItemLogin
            // 
            this.tsmItemLogin.Name = "tsmItemLogin";
            this.tsmItemLogin.Size = new System.Drawing.Size(152, 22);
            this.tsmItemLogin.Text = "Login";
            this.tsmItemLogin.Click += new System.EventHandler(this.tsmItemLogin_Click);
            // 
            // tsmItemRegister
            // 
            this.tsmItemRegister.Name = "tsmItemRegister";
            this.tsmItemRegister.Size = new System.Drawing.Size(152, 22);
            this.tsmItemRegister.Text = "Register";
            this.tsmItemRegister.Click += new System.EventHandler(this.tsmItemRegister_Click);
            // 
            // tsmItemCloseUser
            // 
            this.tsmItemCloseUser.Name = "tsmItemCloseUser";
            this.tsmItemCloseUser.Size = new System.Drawing.Size(152, 22);
            this.tsmItemCloseUser.Text = "Close User";
            this.tsmItemCloseUser.Click += new System.EventHandler(this.tsmItemCloseUser_Click);
            // 
            // tsmItemAboutExit
            // 
            this.tsmItemAboutExit.Name = "tsmItemAboutExit";
            this.tsmItemAboutExit.Size = new System.Drawing.Size(152, 22);
            this.tsmItemAboutExit.Text = "Exit";
            this.tsmItemAboutExit.Click += new System.EventHandler(this.tsmItemAboutExit_Click);
            // 
            // tsmFiles
            // 
            this.tsmFiles.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmItemAddFile});
            this.tsmFiles.Name = "tsmFiles";
            this.tsmFiles.Size = new System.Drawing.Size(42, 20);
            this.tsmFiles.Text = "Files";
            // 
            // tsmItemAddFile
            // 
            this.tsmItemAddFile.Name = "tsmItemAddFile";
            this.tsmItemAddFile.Size = new System.Drawing.Size(152, 22);
            this.tsmItemAddFile.Text = "Add file";
            this.tsmItemAddFile.Click += new System.EventHandler(this.tsmItemAddFile_Click);
            // 
            // tsmHelp
            // 
            this.tsmHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmItemAbout});
            this.tsmHelp.Name = "tsmHelp";
            this.tsmHelp.Size = new System.Drawing.Size(44, 20);
            this.tsmHelp.Text = "Help";
            // 
            // tsmItemAbout
            // 
            this.tsmItemAbout.Name = "tsmItemAbout";
            this.tsmItemAbout.Size = new System.Drawing.Size(107, 22);
            this.tsmItemAbout.Text = "About";
            this.tsmItemAbout.Click += new System.EventHandler(this.tsmItemAbout_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(12, 336);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(100, 23);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(118, 336);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 23);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnShare
            // 
            this.btnShare.Location = new System.Drawing.Point(224, 336);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(100, 23);
            this.btnShare.TabIndex = 4;
            this.btnShare.Text = "Share";
            this.btnShare.UseVisualStyleBackColor = true;
            this.btnShare.Click += new System.EventHandler(this.btnShare_Click);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(337, 366);
            this.Controls.Add(this.btnShare);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.LbFiles);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainWindow";
            this.Text = "RBox";
            this.Load += new System.EventHandler(this.MainWindow_Load);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox LbFiles;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem tsmUser;
        private System.Windows.Forms.ToolStripMenuItem tsmItemLogin;
        private System.Windows.Forms.ToolStripMenuItem tsmItemCloseUser;
        private System.Windows.Forms.ToolStripMenuItem tsmItemAboutExit;
        private System.Windows.Forms.ToolStripMenuItem tsmFiles;
        private System.Windows.Forms.ToolStripMenuItem tsmItemAddFile;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmItemAbout;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnShare;
        private System.Windows.Forms.ToolStripMenuItem tsmItemRegister;
        private System.Windows.Forms.ToolTip toolTip;
    }
}

