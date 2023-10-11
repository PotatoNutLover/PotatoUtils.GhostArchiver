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
            textBox_minFileSize.Text = _configuration.MinFileSize.ToString();
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

            if (Int32.TryParse(textBox_minFileSize.Text, out int tempValue))
                _configuration.MinFileSize = tempValue;
            else
                _configuration.MinFileSize = 0;

            if (Int32.TryParse(textBox_attemtpsCount.Text, out tempValue))
                _configuration.AttemptCount = tempValue;
            else
                _configuration.AttemptCount = 0;

            if (float.TryParse(textBox_attemptDelay.Text, out float tempValue2))
                _configuration.AttemptDelay = tempValue2;
            else
                _configuration.AttemptDelay = 0;
        }
    }
}
