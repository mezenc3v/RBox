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
            this.tsmItemLogOut = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemAboutExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.labelCurrUser = new System.Windows.Forms.Label();
            this.ttHelpMessages = new System.Windows.Forms.ToolTip(this.components);
            this.rtbLogs = new System.Windows.Forms.RichTextBox();
            this.btnAddFile = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnShare = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.MainMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // LbFiles
            // 
            this.LbFiles.AllowDrop = true;
            this.LbFiles.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.LbFiles.FormattingEnabled = true;
            this.LbFiles.ItemHeight = 30;
            this.LbFiles.Location = new System.Drawing.Point(10, 25);
            this.LbFiles.Name = "LbFiles";
            this.LbFiles.Size = new System.Drawing.Size(370, 244);
            this.LbFiles.TabIndex = 0;
            this.LbFiles.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.LbFiles_DrawItem);
            this.LbFiles.SelectedIndexChanged += new System.EventHandler(this.LbFiles_SelectedIndexChanged);
            this.LbFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.LbFiles_DragDrop);
            this.LbFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.LbFiles_DragEnter);
            this.LbFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.LbFiles_MouseMove);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmUser,
            this.tsmHelp});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(389, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainMenu_MouseMove);
            // 
            // tsmUser
            // 
            this.tsmUser.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmItemLogin,
            this.tsmItemRegister,
            this.tsmItemLogOut,
            this.tsmItemAboutExit});
            this.tsmUser.Name = "tsmUser";
            this.tsmUser.Size = new System.Drawing.Size(42, 20);
            this.tsmUser.Text = "User";
            // 
            // tsmItemLogin
            // 
            this.tsmItemLogin.Name = "tsmItemLogin";
            this.tsmItemLogin.Size = new System.Drawing.Size(116, 22);
            this.tsmItemLogin.Text = "Login";
            this.tsmItemLogin.Click += new System.EventHandler(this.tsmItemLogin_Click);
            // 
            // tsmItemRegister
            // 
            this.tsmItemRegister.Name = "tsmItemRegister";
            this.tsmItemRegister.Size = new System.Drawing.Size(116, 22);
            this.tsmItemRegister.Text = "Register";
            this.tsmItemRegister.Click += new System.EventHandler(this.tsmItemRegister_Click);
            // 
            // tsmItemLogOut
            // 
            this.tsmItemLogOut.Name = "tsmItemLogOut";
            this.tsmItemLogOut.Size = new System.Drawing.Size(116, 22);
            this.tsmItemLogOut.Text = "Log out";
            this.tsmItemLogOut.Click += new System.EventHandler(this.tsmItemCloseUser_Click);
            // 
            // tsmItemAboutExit
            // 
            this.tsmItemAboutExit.Name = "tsmItemAboutExit";
            this.tsmItemAboutExit.Size = new System.Drawing.Size(116, 22);
            this.tsmItemAboutExit.Text = "Exit";
            this.tsmItemAboutExit.Click += new System.EventHandler(this.tsmItemAboutExit_Click);
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
            // labelCurrUser
            // 
            this.labelCurrUser.BackColor = System.Drawing.Color.Transparent;
            this.labelCurrUser.Location = new System.Drawing.Point(157, 9);
            this.labelCurrUser.Name = "labelCurrUser";
            this.labelCurrUser.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.labelCurrUser.Size = new System.Drawing.Size(225, 13);
            this.labelCurrUser.TabIndex = 6;
            this.labelCurrUser.Text = "    ";
            this.labelCurrUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // rtbLogs
            // 
            this.rtbLogs.Location = new System.Drawing.Point(10, 350);
            this.rtbLogs.Name = "rtbLogs";
            this.rtbLogs.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbLogs.Size = new System.Drawing.Size(370, 70);
            this.rtbLogs.TabIndex = 9;
            this.rtbLogs.Text = "";
            // 
            // btnAddFile
            // 
            this.btnAddFile.BackColor = System.Drawing.Color.Transparent;
            this.btnAddFile.BackgroundImage = global::RBox.WinForms.Properties.Resources.Add;
            this.btnAddFile.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAddFile.Location = new System.Drawing.Point(10, 275);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(70, 70);
            this.btnAddFile.TabIndex = 8;
            this.btnAddFile.UseVisualStyleBackColor = false;
            this.btnAddFile.Click += new System.EventHandler(this.btnAddFile_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.Color.Transparent;
            this.btnUpdate.BackgroundImage = global::RBox.WinForms.Properties.Resources.Refresh;
            this.btnUpdate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnUpdate.Location = new System.Drawing.Point(310, 275);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(70, 70);
            this.btnUpdate.TabIndex = 5;
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnShare
            // 
            this.btnShare.BackColor = System.Drawing.Color.Transparent;
            this.btnShare.BackgroundImage = global::RBox.WinForms.Properties.Resources.Share;
            this.btnShare.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnShare.Location = new System.Drawing.Point(235, 275);
            this.btnShare.Name = "btnShare";
            this.btnShare.Size = new System.Drawing.Size(70, 70);
            this.btnShare.TabIndex = 4;
            this.btnShare.UseVisualStyleBackColor = false;
            this.btnShare.Click += new System.EventHandler(this.btnShare_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.Transparent;
            this.btnDelete.BackgroundImage = global::RBox.WinForms.Properties.Resources.Delete;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Location = new System.Drawing.Point(160, 275);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(70, 70);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.BackColor = System.Drawing.Color.Transparent;
            this.btnDownload.BackgroundImage = global::RBox.WinForms.Properties.Resources.Download;
            this.btnDownload.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDownload.Location = new System.Drawing.Point(85, 275);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(70, 70);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.UseVisualStyleBackColor = false;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // MainWindow
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(389, 431);
            this.Controls.Add(this.rtbLogs);
            this.Controls.Add(this.btnAddFile);
            this.Controls.Add(this.labelCurrUser);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.btnShare);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnDownload);
            this.Controls.Add(this.LbFiles);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.MainMenu;
            this.MaximizeBox = false;
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
        private System.Windows.Forms.ToolStripMenuItem tsmItemLogOut;
        private System.Windows.Forms.ToolStripMenuItem tsmItemAboutExit;
        private System.Windows.Forms.ToolStripMenuItem tsmHelp;
        private System.Windows.Forms.ToolStripMenuItem tsmItemAbout;
        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnShare;
        private System.Windows.Forms.ToolStripMenuItem tsmItemRegister;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label labelCurrUser;
        private System.Windows.Forms.Button btnAddFile;
        private System.Windows.Forms.ToolTip ttHelpMessages;
        private System.Windows.Forms.RichTextBox rtbLogs;
    }
}

