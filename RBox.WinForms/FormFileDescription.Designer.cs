namespace RBox.WinForms
{
    partial class FormFileDescription
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
            this.btnUploadFile = new System.Windows.Forms.Button();
            this.labelFileDescription = new System.Windows.Forms.Label();
            this.tbFileDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnUploadFile
            // 
            this.btnUploadFile.Location = new System.Drawing.Point(75, 160);
            this.btnUploadFile.Name = "btnUploadFile";
            this.btnUploadFile.Size = new System.Drawing.Size(75, 23);
            this.btnUploadFile.TabIndex = 17;
            this.btnUploadFile.Text = "ОК";
            this.btnUploadFile.UseVisualStyleBackColor = true;
            this.btnUploadFile.Click += new System.EventHandler(this.btnUploadFile_Click);
            // 
            // labelFileDescription
            // 
            this.labelFileDescription.AutoSize = true;
            this.labelFileDescription.Location = new System.Drawing.Point(9, 9);
            this.labelFileDescription.Name = "labelFileDescription";
            this.labelFileDescription.Size = new System.Drawing.Size(77, 13);
            this.labelFileDescription.TabIndex = 16;
            this.labelFileDescription.Text = "File description";
            // 
            // tbFileDescription
            // 
            this.tbFileDescription.Location = new System.Drawing.Point(12, 25);
            this.tbFileDescription.Multiline = true;
            this.tbFileDescription.Name = "tbFileDescription";
            this.tbFileDescription.Size = new System.Drawing.Size(214, 129);
            this.tbFileDescription.TabIndex = 14;
            // 
            // FormFileDescription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(239, 191);
            this.Controls.Add(this.btnUploadFile);
            this.Controls.Add(this.labelFileDescription);
            this.Controls.Add(this.tbFileDescription);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FormFileDescription";
            this.Text = "File description";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnUploadFile;
        private System.Windows.Forms.Label labelFileDescription;
        private System.Windows.Forms.TextBox tbFileDescription;
    }
}