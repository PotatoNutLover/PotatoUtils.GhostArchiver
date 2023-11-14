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
                _configuration = ReadFile();
        }

        public Configuration ReadFile()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));

            try
            {
                using (FileStream stream = new FileStream(_configurationFilePath, FileMode.OpenOrCreate))
                {
                    return (Configuration)serializer.Deserialize(stream);
                }
            }
            catch
            {
                CreateFile();
                return _configuration;
            }
        }

        public void WriteFile(Configuration configuration)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Configuration));


            using (FileStream stream = new FileStream(_configurationFilePath, FileMode.Create))
            {
                serializer.Serialize(stream, configuration);
            }
        }

        private void CreateFile()
        {
            _configuration = _configFactory.GetConfig();
            WriteFile(_configuration);
        }
    }
}
