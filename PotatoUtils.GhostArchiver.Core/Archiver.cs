using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PotatoUtils.Logger;
using PotatoUtils.Logger.OutputStreams;
using SharpCompress.Archives;

namespace PotatoUtils.GhostArchiver.Core
{
    public class Archiver
    {
        private readonly FileSystemWatcher _watcher;
        private readonly int _archiveAttempts;
        private readonly float _attemptDelaySec;
        private readonly FileIdentityStorage _fileIdentityStorage;

        private FileInfo _fileInfo;
        private IArchiveProcessor _archiveProcessor;
        private INotifyFilter _notifyFilter;

        public delegate void LogHandler (string message);
        public event LogHandler? OnMessageLogged;


        public FileInfo FileInfo
        {
            get => _fileInfo; 
            private set => _fileInfo = value;
        }

        public IArchiveProcessor ArchiveProcessor
        {
            get => _archiveProcessor;
            private set => _archiveProcessor = value;
        }

        public INotifyFilter NotifyFilter
        {
            get => _notifyFilter;
            private set => _notifyFilter = value;
        }

        public Archiver(FileInfo fileInfo)
        {
            _fileIdentityStorage = new FileIdentityStorage(1000);
            _watcher = new FileSystemWatcher();
            _archiveAttempts = 20;
            _attemptDelaySec = 1f;
            InitializeArchiver(fileInfo, new ZipArchiveProcessor(), new DefaultNotifyFilterPreset());
        }

        public Archiver(FileInfo fileInfo, int archiveAttempts, float attemptDelaySec)
        {
            _fileIdentityStorage = new FileIdentityStorage(1000);
            _watcher = new FileSystemWatcher();
            _archiveAttempts = archiveAttempts;
            _attemptDelaySec = attemptDelaySec;
            InitializeArchiver(fileInfo, new ZipArchiveProcessor(), new DefaultNotifyFilterPreset());
        }

        public Archiver(FileInfo fileInfo, IArchiveProcessor archiver, INotifyFilter notifyFilter, int archiveAttempts, float attemptDelaySec)
        {
            _fileIdentityStorage = new FileIdentityStorage(1000);
            _watcher = new FileSystemWatcher();
            _archiveAttempts = archiveAttempts;
            _attemptDelaySec = attemptDelaySec;
            InitializeArchiver(fileInfo, archiver, notifyFilter);
        }

        private void InitializeArchiver(FileInfo fileInfo, IArchiveProcessor archiver, INotifyFilter notifyFilter)
        {
            _fileInfo = fileInfo;
            _archiveProcessor = archiver;
            _notifyFilter = notifyFilter;
            _watcher.Created += new FileSystemEventHandler(OnCreated);
            InitializeWatcher();
            InitializeLogger();
            PLogger.Log("Archiver init finished");
        }

        public void InitializeWatcher()
        {
            _watcher.Path = _fileInfo.Path;
            _watcher.Filter = _fileInfo.Extension;
            _watcher.NotifyFilter = _notifyFilter.GetFilter();
            _watcher.EnableRaisingEvents = true;
        }

        private void InitializeLogger()
        {
            PLogger.SetOutputExceptionFormat(new DefaultOutputExceptionFormat());
            PLogger.SetOutputFormat(new DefaultOutputFormat());
            EventOutputStream outputStream = new EventOutputStream();
            outputStream.OnMessageLogged += OnOutputMessageLogged;
            PLogger.SetOutputStream(outputStream);
        }

        private void OnCreated(object source, FileSystemEventArgs e)
        {
            PLogger.Log($"File created ({e.FullPath})");
            try
            {
                int sessionId = _fileIdentityStorage.AddFileAndGetSessionId(e.Name, _fileInfo.Path);
                IArchiveProcessor processor = _archiveProcessor.Clone(_archiveAttempts, _attemptDelaySec, _fileInfo.MinSize);
                PLogger.Log($"Session id:{sessionId} created.");
                processor.ArchivingSessionCompleted += DropSession;
                processor.StartProcess(sessionId, e.Name, _fileInfo.Path);
            }
            catch (Exception ex)
            {
                PLogger.LogException(ex.Message);
            }
        }

        public void DropSession(int sessionId, IArchiveProcessor processor)
        {
            _fileIdentityStorage.DropFile(sessionId);
            PLogger.Log($"Session id:{sessionId} dropped.");
            processor.ArchivingSessionCompleted -= DropSession;
        }

        private void OnOutputMessageLogged(string message)
        {
            OnMessageLogged?.Invoke(message);
        }
    }
}
