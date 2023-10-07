using SharpCompress.Archives;
using SharpCompress.Archives.Zip;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotatoUtils.Logger;

namespace PotatoUtils.GhostArchiver.Core
{
    public class ZipArchiveProcessor : IArchiveProcessor
    {
        private const int DefaultArchiveAttempts = 20;
        private const float DefaultAttemptDelaySec = 1.0f;
        private const int DefaultMinSize = 0;

        private readonly int _archiveAttempts;
        private readonly float _attemptDelaySec;
        private readonly int _minSize;

        private string _fileName;
        private string _filePath;

        public ZipArchiveProcessor()
        {
            _archiveAttempts = DefaultArchiveAttempts;
            _attemptDelaySec = DefaultAttemptDelaySec;
            _minSize = DefaultMinSize;
        }

        public ZipArchiveProcessor(int archiveAttempts, float attemptDelaySec, int minSize)
        {
            _archiveAttempts = archiveAttempts;
            _attemptDelaySec = attemptDelaySec;
            _minSize = minSize;
        }

        public void StartProcess(string fileName, string filePath)
        {
            PLogger.Log($"Start archiving process {_fileName}");
            _fileName = fileName;
            _filePath = filePath;

            Thread thread = new Thread(StartArchivateCycle);
            thread.Start();
        }

        public IArchiveProcessor Clone()
        {
            return new ZipArchiveProcessor();
        }

        public IArchiveProcessor Clone(int archiveAttempts, float attemptDelaySec, int minSize)
        {
            return new ZipArchiveProcessor(archiveAttempts, attemptDelaySec, minSize);
        }

        private void StartArchivateCycle()
        {
            int counter = 0;
            for (counter = 0; counter < _archiveAttempts; counter++)
            {
                try
                {
                    Archivate();
                    return;
                }
                catch
                {
                    PLogger.Log($"Attempt {counter}. File ({_filePath}\\{_fileName}) is busy by antoher process, process falls asleep for 1 second");
                    Thread.Sleep((int)(_attemptDelaySec * 1000));
                    continue;
                }
            }

            PLogger.Log($"Failed to archive file {_fileName} by {counter} attempts");
        }

        private void Archivate()
        {
            if (IsFileSizeValid(_filePath + "\\" + _fileName) == false)
                return;

            using (var archive = ZipArchive.Create())
            {
                archive.AddEntry(_fileName, $"{_filePath}\\{_fileName}");
                archive.SaveTo($"{_filePath}\\{_fileName}.zip", CompressionType.Deflate);
                PLogger.Log($"Archiving process finished successfully on {_fileName}");
            }
        }

        private bool IsFileSizeValid(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                stream.ReadByte();
                stream.Close();
            }

            System.IO.FileInfo info = new System.IO.FileInfo(path);
            if (info.Length < _minSize)
            {
                PLogger.Log($"File {_fileName} size is smaller ({info.Length}) than minimal size ({_minSize}). Archiving cancelled.");
                return false;
            }

            return true;
        }
    }
}
