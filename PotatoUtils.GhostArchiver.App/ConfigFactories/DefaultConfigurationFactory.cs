using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoUtils.GhostArchiver.App.ConfigFactories
{
    public class DefaultConfigFactory : IConfigFactory
    {
        public Configuration GetConfig()
        {
            Configuration configuration = new Configuration()
            {
                FileExtension = "*.har",
                FolderPath = "C:\\Users\\$USERNAME\\Desktop\\харлоги",
                MinFileSize = 20000000,
                AttemptCount = 40,
                AttemptDelay = 1
            };

            return configuration;
        }
    }
}
