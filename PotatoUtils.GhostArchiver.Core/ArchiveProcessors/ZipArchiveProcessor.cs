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
        public event IArchiveProcessor.ArchivingSession ArchivingSessionCompleted;

        private const int DefaultArchiveAttempts = 20;
        private const float DefaultAttemptDelaySec = 1.0f;
        private const int DefaultMinSize = 0;

        private readonly int _archiveAttempts;
        private readonly float _attemptDelaySec;
        private readonly int _minSize;
        
        private int _sessionId;
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

        public void StartProcess(int sessionId, string fileName, string filePath)
        {
            PLogger.Log($"Start archiving process {_fileName}");
            _fileName = fileName;
            _filePath = filePath;
            _sessionId = sessionId;

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
                    ArchivingSessionCompleted?.Invoke(_sessionId, this);
                    return;
                }
                catch (IOException exception)
                {
                    PLogger.Log($"Attempt {counter}. File ({_filePath}\\{_fileName}) is busy by antoher process, process falls asleep for {_attemptDelaySec} second");
                    Thread.Sleep((int)(_attemptDelaySec * 1000));
                    continue;
                }
                catch (Exception exception)
                {
                    PLogger.LogException(exception.Message);
                    PLogger.Log("Stop archiving process");
                }
            }

            PLogger.Log($"Failed to archive file {_fileName} by {counter} attempts");
            ArchivingSessionCompleted?.Invoke(_sessionId, this);
        }

        private void Archivate()
        {
            string path = _filePath + "\\" + _fileName;

            if (IsFileSizeValid(path) == false)
                if (TryAwaitFileSizeIncrease(path) == false)
                    return;

            using (var archive = ZipArchive.Create())
            {
                archive.AddEntry(_fileName, $"{_filePath}\\{_fileName}");
                archive.SaveTo($"{_filePath}\\{_fileName}.zip", CompressionType.Deflate);
                PLogger.Log($"Archiving process finished successfully on {_fileName}");
            }
        }

        private bool TryAwaitFileSizeIncrease(string path)
        {
            PLogger.Log($"Try await file({_fileName}) size increase");
            long firstFileSizeShot = 0;
            long secondFileSizeShot = 0;
            int counter = 0;

            while (true)
            {
                firstFileSizeShot = GetFileSize(path);
                PLogger.Log($"Current file ({_fileName}) size: {firstFileSizeShot}");
                Thread.Sleep((int)(_attemptDelaySec * 1000));
                secondFileSizeShot = GetFileSize(path);
                PLogger.Log($"Current file ({_fileName}) size: {secondFileSizeShot}");
                counter++;
                if (IsFileSizeValid(path))
                {
                    PLogger.Log($"Current file ({_fileName}) size is valid.");
                    return true;
                }
                else if (firstFileSizeShot >= secondFileSizeShot)
                {
                    PLogger.Log($"Current file ({_fileName}) size is not valid. Archive process cancelled. {counter} Attempts.");
                    return false;
                }
                else if (counter >= _archiveAttempts)
                {
                    PLogger.Log($"Current file ({_fileName}) size is not valid. Archive process cancelled. {counter} Attempts.");
                    return false;
                }
            }
        }

        private bool IsFileSizeValid(string path)
        {
            using (FileStream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                stream.ReadByte();
                stream.Close();
            }

            long size = GetFileSize(path);
            if (size < _minSize)
            {
                PLogger.Log($"File {_fileName} size is smaller ({size}) than minimal size ({_minSize}).");
                return false;
            }

            return true;
        }

        private long GetFileSize(string path)
        {
            System.IO.FileInfo info = new System.IO.FileInfo(path);
            return info.Length;
        }
    }
}
