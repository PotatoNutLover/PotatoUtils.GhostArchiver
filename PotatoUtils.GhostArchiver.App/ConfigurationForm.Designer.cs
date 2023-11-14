namespace PotatoUtils.GhostArchiver.App
{
    partial class ConfigurationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigurationForm));
            label1 = new Label();
            textBox_fileExtension = new TextBox();
            textBox_folderPath = new TextBox();
            label2 = new Label();
            textBox_minFileSize = new TextBox();
            label3 = new Label();
            textBox_attemtpsCount = new TextBox();
            label4 = new Label();
            textBox_attemptDelay = new TextBox();
            label5 = new Label();
            button_save = new Button();
            button_openFolder = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(79, 15);
            label1.TabIndex = 0;
            label1.Text = "File extension";
            // 
            // textBox_fileExtension
            // 
            textBox_fileExtension.Location = new Point(12, 27);
            textBox_fileExtension.Name = "textBox_fileExtension";
            textBox_fileExtension.Size = new Size(100, 23);
            textBox_fileExtension.TabIndex = 1;
            // 
            // textBox_folderPath
            // 
            textBox_folderPath.Location = new Point(12, 76);
            textBox_folderPath.Name = "textBox_folderPath";
            textBox_folderPath.Size = new Size(217, 23);
            textBox_folderPath.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 58);
            label2.Name = "label2";
            label2.Size = new Size(67, 15);
            label2.TabIndex = 2;
            label2.Text = "Folder path";
            // 
            // textBox_minFileSize
            // 
            textBox_minFileSize.Location = new Point(12, 127);
            textBox_minFileSize.Name = "textBox_minFileSize";
            textBox_minFileSize.Size = new Size(100, 23);
            textBox_minFileSize.TabIndex = 5;
            textBox_minFileSize.TextChanged += textBox_minFileSize_TextChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 109);
            label3.Name = "label3";
            label3.Size = new Size(142, 15);
            label3.TabIndex = 4;
            label3.Text = "Minimal file size (Mbytes)";
            // 
            // textBox_attemtpsCount
            // 
            textBox_attemtpsCount.Location = new Point(12, 178);
            textBox_attemtpsCount.Name = "textBox_attemtpsCount";
            textBox_attemtpsCount.Size = new Size(100, 23);
            textBox_attemtpsCount.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 160);
            label4.Name = "label4";
            label4.Size = new Size(90, 15);
            label4.TabIndex = 6;
            label4.Text = "Attempts count";
            // 
            // textBox_attemptDelay
            // 
            textBox_attemptDelay.Location = new Point(12, 227);
            textBox_attemptDelay.Name = "textBox_attemptDelay";
            textBox_attemptDelay.Size = new Size(100, 23);
            textBox_attemptDelay.TabIndex = 9;
            textBox_attemptDelay.TextChanged += textBox_attemptDelay_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 209);
            label5.Name = "label5";
            label5.Size = new Size(110, 15);
            label5.TabIndex = 8;
            label5.Text = "Attempt delay (sec)";
            // 
            // button_save
            // 
            button_save.Location = new Point(106, 256);
            button_save.Name = "button_save";
            button_save.Size = new Size(75, 23);
            button_save.TabIndex = 10;
            button_save.Text = "Save";
            button_save.UseVisualStyleBackColor = true;
            button_save.Click += button_save_Click;
            // 
            // button_openFolder
            // 
            button_openFolder.Location = new Point(237, 76);
            button_openFolder.Name = "button_openFolder";
            button_openFolder.Size = new Size(39, 23);
            button_openFolder.TabIndex = 11;
            button_openFolder.Text = "...";
            button_openFolder.UseVisualStyleBackColor = true;
            button_openFolder.Click += button_openFolder_Click;
            // 
            // ConfigurationForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(281, 286);
            Controls.Add(button_openFolder);
            Controls.Add(button_save);
            Controls.Add(textBox_attemptDelay);
            Controls.Add(label5);
            Controls.Add(textBox_attemtpsCount);
            Controls.Add(label4);
            Controls.Add(textBox_minFileSize);
            Controls.Add(label3);
            Controls.Add(textBox_folderPath);
            Controls.Add(label2);
            Controls.Add(textBox_fileExtension);
            Controls.Add(label1);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ConfigurationForm";
            Text = "Configuration";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox textBox_fileExtension;
        private TextBox textBox_folderPath;
        private Label label2;
        private TextBox textBox_minFileSize;
        private Label label3;
        private TextBox textBox_attemtpsCount;
        private Label label4;
        private TextBox textBox_attemptDelay;
        private Label label5;
        private Button button_save;
        private Button button_openFolder;
    }
}