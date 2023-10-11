using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PotatoUtils.GhostArchiver.Core
{
    internal class FileIdentityStorage
    {
        private readonly List<FileIdentifier> _storage;
        private readonly int _capacity;

        private int _idCounter;

        public FileIdentityStorage(int capacity)
        {
            _storage = new List<FileIdentifier>();
            _capacity = capacity;
            _idCounter = 0;
        }

        public int AddFileAndGetSessionId(string name, string folderPath)
        {
            FileIdentifier file = new FileIdentifier(_idCounter ,name, folderPath);

            if (IndexOf(file) == -1 && _capacity > _storage.Count)
            {
                _storage.Add(file);
                _idCounter++;
                return file.Identifier;
            }  
            else
                throw new Exception($"File identity storage has file with name ({file.Name}) and folder path ({file.FolderPath})");
        }

        public void DropFile(int sessionId)
        {
            int index = IndexOf(sessionId);
            if (index > -1)
                _storage.RemoveAt(index);
            if (_storage.Count == 0)
                _idCounter = 0;
        }

        public int IndexOf(int sessionId)
        {
            FileIdentifier? request = _storage.Where(x => x.Identifier == sessionId).FirstOrDefault();
            if(request != null)
                return _storage.IndexOf(request);
            else
                return -1;
        }

        private int IndexOf(FileIdentifier file)
        {
            FileIdentifier? request = _storage.Where(x => x.Name == file.Name && x.FolderPath == x.FolderPath).FirstOrDefault();
            if (request != null)
                return _storage.IndexOf(request);
            else
                return -1;
        }
    }
}
