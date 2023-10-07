using System.IO;

namespace PotatoUtils.GhostArchiver.Core
{
    public class FileInfo
    {
        private string _path;
        private string _extension;
        private int _minSize;

        public string Path
        {
            get => _path;
            set => _path = value.Trim();
        }

        public string Extension
        {
            get => _extension;
            set => _extension = value.Trim();
        }

        public int MinSize
        {
            get => _minSize;
            set => _minSize = value;
        }

        public FileInfo (string path, string extension, int minSize)
        {
            Path = path;
            Extension = extension;
            MinSize = minSize;
        }
    }
}