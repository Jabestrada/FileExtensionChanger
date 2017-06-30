namespace FileExtensionChanger.UI.Windows
{
    partial class FileExtensionChangerMain
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
            this.filenamesToChange = new System.Windows.Forms.TextBox();
            this.changeButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // filenamesToChange
            // 
            this.filenamesToChange.Location = new System.Drawing.Point(109, 58);
            this.filenamesToChange.Multiline = true;
            this.filenamesToChange.Name = "filenamesToChange";
            this.filenamesToChange.Size = new System.Drawing.Size(984, 308);
            this.filenamesToChange.TabIndex = 0;
            // 
            // changeButton
            // 
            this.changeButton.Location = new System.Drawing.Point(929, 424);
            this.changeButton.Name = "changeButton";
            this.changeButton.Size = new System.Drawing.Size(164, 58);
            this.changeButton.TabIndex = 1;
            this.changeButton.Text = "Change";
            this.changeButton.UseVisualStyleBackColor = true;
            this.changeButton.Click += new System.EventHandler(this.changeButton_Click);
            // 
            // FileExtensionChangerMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(16F, 31F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 519);
            this.Controls.Add(this.changeButton);
            this.Controls.Add(this.filenamesToChange);
            this.Name = "FileExtensionChangerMain";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox filenamesToChange;
        private System.Windows.Forms.Button changeButton;
    }
}

