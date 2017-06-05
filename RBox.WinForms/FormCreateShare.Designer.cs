namespace RBox.WinForms
{
    partial class FormCreateShare
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
            this.btnCreateShare = new System.Windows.Forms.Button();
            this.labelUserLogin = new System.Windows.Forms.Label();
            this.tbUserLogin = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCreateShare
            // 
            this.btnCreateShare.Location = new System.Drawing.Point(75, 51);
            this.btnCreateShare.Name = "btnCreateShare";
            this.btnCreateShare.Size = new System.Drawing.Size(75, 23);
            this.btnCreateShare.TabIndex = 15;
            this.btnCreateShare.Text = "Share";
            this.btnCreateShare.UseVisualStyleBackColor = true;
            this.btnCreateShare.Click += new System.EventHandler(this.btnCreateShare_Click);
            // 
            // labelUserLogin
            // 
            this.labelUserLogin.AutoSize = true;
            this.labelUserLogin.Location = new System.Drawing.Point(12, 9);
            this.labelUserLogin.Name = "labelUserLogin";
            this.labelUserLogin.Size = new System.Drawing.Size(54, 13);
            this.labelUserLogin.TabIndex = 14;
            this.labelUserLogin.Text = "User login";
            // 
            // tbUserLogin
            // 
            this.tbUserLogin.Location = new System.Drawing.Point(12, 25);
            this.tbUserLogin.Name = "tbUserLogin";
            this.tbUserLogin.Size = new System.Drawing.Size(214, 20);
            this.tbUserLogin.TabIndex = 13;
            // 
            // FormCreateShare
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(237, 77);
            this.Controls.Add(this.btnCreateShare);
            this.Controls.Add(this.labelUserLogin);
            this.Controls.Add(this.tbUserLogin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormCreateShare";
            this.Text = "FormCreateShare";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCreateShare;
        private System.Windows.Forms.Label labelUserLogin;
        private System.Windows.Forms.TextBox tbUserLogin;
    }
}