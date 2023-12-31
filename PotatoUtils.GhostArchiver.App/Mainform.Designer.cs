﻿namespace PotatoUtils.GhostArchiver.App
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            textBox1 = new TextBox();
            button_Start = new Button();
            button_copyLog = new Button();
            button_openConfigFile = new Button();
            button_openConfig = new Button();
            button_Abort = new Button();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Font = new Font("Segoe UI", 6.75F, FontStyle.Regular, GraphicsUnit.Point);
            textBox1.Location = new Point(12, 12);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.ScrollBars = ScrollBars.Vertical;
            textBox1.Size = new Size(490, 226);
            textBox1.TabIndex = 0;
            // 
            // button_Start
            // 
            button_Start.Location = new Point(12, 244);
            button_Start.Name = "button_Start";
            button_Start.Size = new Size(75, 23);
            button_Start.TabIndex = 5;
            button_Start.Text = "Start";
            button_Start.UseVisualStyleBackColor = true;
            button_Start.Click += button_Start_Click;
            // 
            // button_copyLog
            // 
            button_copyLog.Location = new Point(427, 244);
            button_copyLog.Name = "button_copyLog";
            button_copyLog.Size = new Size(75, 23);
            button_copyLog.TabIndex = 6;
            button_copyLog.Text = "Copy Log";
            button_copyLog.UseVisualStyleBackColor = true;
            button_copyLog.Click += button_copyLog_Click;
            // 
            // button_openConfigFile
            // 
            button_openConfigFile.Location = new Point(318, 244);
            button_openConfigFile.Name = "button_openConfigFile";
            button_openConfigFile.Size = new Size(103, 23);
            button_openConfigFile.TabIndex = 7;
            button_openConfigFile.Text = "Open config file";
            button_openConfigFile.UseVisualStyleBackColor = true;
            button_openConfigFile.Click += button_openConfigFile_Click;
            // 
            // button_openConfig
            // 
            button_openConfig.Location = new Point(229, 244);
            button_openConfig.Name = "button_openConfig";
            button_openConfig.Size = new Size(83, 23);
            button_openConfig.TabIndex = 8;
            button_openConfig.Text = "Open config";
            button_openConfig.UseVisualStyleBackColor = true;
            button_openConfig.Click += button_openConfig_Click;
            // 
            // button_Abort
            // 
            button_Abort.Enabled = false;
            button_Abort.Location = new Point(93, 244);
            button_Abort.Name = "button_Abort";
            button_Abort.Size = new Size(75, 23);
            button_Abort.TabIndex = 9;
            button_Abort.Text = "Abort";
            button_Abort.UseVisualStyleBackColor = true;
            button_Abort.Click += button_Abort_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.InactiveCaption;
            ClientSize = new Size(514, 273);
            Controls.Add(button_Abort);
            Controls.Add(button_openConfig);
            Controls.Add(button_openConfigFile);
            Controls.Add(button_copyLog);
            Controls.Add(button_Start);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainForm";
            Text = "GhostArchiver by @nuttlover";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button_Start;
        private Button button_copyLog;
        private Button button_openConfigFile;
        private Button button_openConfig;
        private Button button_Abort;
    }
}