using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoUtils.GhostArchiver.Core
{
    internal class FileIdentifier
    {
        private readonly string _name;
        private readonly string _folderPath;
        private readonly int _identifier;

        public string Name
        {
            get => _name;
        }

        public string FolderPath
        {
            get => _folderPath;
        }

        public int Identifier
        {
            get => _identifier;
        }

        public FileIdentifier(int identifier, string name, string folderPath)
        {
            _identifier = identifier;
            _name = name.Trim();
            _folderPath = folderPath.Trim();
        }
    }
}
