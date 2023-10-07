using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using PotatoUtils.GhostArchiver.App.ConfigFactories;

namespace PotatoUtils.GhostArchiver.App
{
    public class ConfigurationFile
    {
        private readonly string _configurationFilePath;
        private readonly IConfigFactory _configFactory;

        private Configuration _configuration;

        public Configuration Configuration
        {
            get => _configuration;
        }

        public ConfigurationFile(string configurationFilePath, IConfigFactory configFactory)
        {
            _configurationFilePath = configurationFilePath;
            _configFactory = configFactory;

            if (File.Exists(_configurationFilePath) == false)
                CreateFile();
            else
                ReadFile();
        }

        private void ReadFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));

            using (FileStream stream = new FileStream(_configurationFilePath, FileMode.OpenOrCreate))
            {
                _configuration = (Configuration)serializer.Deserialize(stream);
            }
        }

        private void CreateFile()
        {
            _configuration = _configFactory.GetConfig();
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));

            using (FileStream stream = new FileStream(_configurationFilePath, FileMode.OpenOrCreate))
            {
                serializer.Serialize(stream, _configuration);
            }
        }
    }
}
