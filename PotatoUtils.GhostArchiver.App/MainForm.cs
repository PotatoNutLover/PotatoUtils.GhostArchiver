using PotatoUtils.GhostArchiver.Core;
using SharpCompress.Common;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using FileInfo = PotatoUtils.GhostArchiver.Core.FileInfo;
using PotatoUtils.Logger;
using PotatoUtils.Logger.OutputStreams;
using PotatoUtils.GhostArchiver.App.ConfigFactories;


namespace PotatoUtils.GhostArchiver.App
{
    public partial class MainForm : Form
    {
        private const string ConfigurationFilePath = "Settings.config";

        private ConfigurationFile _configurationFile;
        private Archiver _archiver;

        public MainForm()
        {
            InitializeComponent();
            textBox1.Multiline = true;
            InitializeLogger();
        }

        private void InitializeLogger()
        {
            PLogger.SetOutputExceptionFormat(new DefaultOutputExceptionFormat());
            PLogger.SetOutputFormat(new DefaultOutputFormat());
            EventOutputStream outputStream = new EventOutputStream();
            outputStream.OnMessageLogged += Log;
            PLogger.SetOutputStream(outputStream);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Log(string message)
        {
            if (textBox1.InvokeRequired == true)
            {
                textBox1.Invoke((MethodInvoker)delegate
                {
                    textBox1.AppendText($"{message}\n");
                    textBox1.AppendText(Environment.NewLine);
                    textBox1.ScrollToCaret();
                });
            }
            else
                textBox1.AppendText($"{message}\n");

        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (TryLoadConfig())
                if(TryInitializeArchiver())
                {
                    button_Start.Enabled = false;
                    button_Start.Text = "Started";
                }
        }

        private bool TryLoadConfig()
        {
            try
            {
                _configurationFile = new ConfigurationFile(ConfigurationFilePath, new DefaultConfigFactory());
                return ValidateConfig();
            }
            catch (Exception exception)
            {
                PLogger.LogException(exception.Message);
                return false;
            }
        }

        private bool ValidateConfig()
        {
            ValidationContext context = new ValidationContext(_configurationFile.Configuration);
            List<ValidationResult> result = new List<ValidationResult>();

            if (!Validator.TryValidateObject(_configurationFile.Configuration, context, result, true))
            {
                PLogger.Log("Configuration not loaded.\r\n Correct config and press start button.\r\n Result: \r\n");
                foreach (var error in result)
                    PLogger.LogException($"  {error.ErrorMessage}; \r\n");
                return false;
            }
            else
            {
                PLogger.Log($"Configuration loaded. \r\n {_configurationFile.Configuration.ToString()}");
            }

            return true;
        }

        private bool TryInitializeArchiver()
        {
            try
            {
                InitializeArchiver();
            }
            catch(Exception exception)
            {
                PLogger.LogException(exception.Message);
                return false;
            }
            return true;
        }

        private void InitializeArchiver()
        {
            FileInfo fileInfo = new FileInfo
            (
                _configurationFile.Configuration.FolderPath,
                _configurationFile.Configuration.FileExtension,
                _configurationFile.Configuration.MinFileSize
            );
            _archiver = new Archiver
            (
                fileInfo,
                _configurationFile.Configuration.AttemptCount,
                _configurationFile.Configuration.AttemptDelay
            );

            _archiver.OnMessageLogged += Log;
            PLogger.Log("Init finished.\r\n");
        }

        private void button_openConfig_Click(object sender, EventArgs e)
        {
            try
            {
                OpenConfig();
            }
            catch (Exception exception)
            {
                PLogger.LogException(exception.Message);
            }
        }

        private static void OpenConfig()
        {
            if (File.Exists(ConfigurationFilePath))
                Process.Start("notepad.exe", ConfigurationFilePath);
            else
                MessageBox.Show("Configuration file does not exists. \r\n Restart app and press Start button to generate new config file.");
        }

        private void button_copyLog_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 0)
                Clipboard.SetText(textBox1.Text);
        }
    }
}