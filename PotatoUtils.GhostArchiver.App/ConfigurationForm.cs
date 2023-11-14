using PotatoUtils.GhostArchiver.App.ConfigFactories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace PotatoUtils.GhostArchiver.App
{
    public partial class ConfigurationForm : Form
    {
        private readonly string _configPath;
        private readonly ConfigurationFile _configFile;

        private Configuration _configuration;


        public ConfigurationForm(string configPath)
        {
            InitializeComponent();
            _configPath = configPath;
            _configFile = new ConfigurationFile(configPath, new DefaultConfigFactory());
            _configFile.ReadFile();
            _configuration = _configFile.Configuration;
            PasteConfigValues();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            ParseValuesToConfig();
            PasteConfigValues();
            _configFile.WriteFile(_configuration);
        }

        private void PasteConfigValues()
        {
            textBox_fileExtension.Text = _configuration.FileExtension;
            textBox_folderPath.Text = _configuration.FolderPath;
            textBox_minFileSize.Text = ValueConverter.BytesToMbytes(_configuration.MinFileSize).ToString();
            textBox_attemtpsCount.Text = _configuration.AttemptCount.ToString();
            textBox_attemptDelay.Text = _configuration.AttemptDelay.ToString();
        }

        private void ParseValuesToConfig()
        {
            if (textBox_fileExtension.Text.Trim() != "")
                _configuration.FileExtension = textBox_fileExtension.Text;
            else
                _configuration.FileExtension = "*.";

            if (textBox_folderPath.Text.Trim() != "")
                _configuration.FolderPath = textBox_folderPath.Text;
            else
                _configuration.FolderPath = "C:\\";

            if (float.TryParse(textBox_minFileSize.Text, out float tempValue))
                _configuration.MinFileSize = ValueConverter.MbytesToBytes(tempValue);
            else
                _configuration.MinFileSize = 0;

            if (Int32.TryParse(textBox_attemtpsCount.Text, out int tempValue2))
                _configuration.AttemptCount = tempValue2;
            else
                _configuration.AttemptCount = 0;

            if (float.TryParse(textBox_attemptDelay.Text, out tempValue))
                _configuration.AttemptDelay = tempValue;
            else
                _configuration.AttemptDelay = 0;
        }

        private void button_openFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (File.Exists(_configuration.FolderPath))
                folderBrowserDialog.SelectedPath = _configuration.FolderPath;

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                textBox_folderPath.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void textBox_minFileSize_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBox(textBox_minFileSize);
        }

        private void textBox_attemptDelay_TextChanged(object sender, EventArgs e)
        {
            ValidateTextBox(textBox_attemptDelay);
        }

        private void ValidateTextBox(System.Windows.Forms.TextBox textBox)
        {
            string text = textBox.Text.Trim();
            bool hasComma = false;

            text = text.Replace('.', ',');
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ',')
                    if (hasComma == false)
                        hasComma = true;
                    else
                        text = text.Remove(i, 1);
            }

            textBox.Text = text;
            textBox.SelectionStart = text.Length;
        }

    }
}
