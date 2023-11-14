using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoUtils.GhostArchiver.Core
{
    public interface IArchiveProcessor
    {
        public delegate void ArchivingSession(int sessionId, IArchiveProcessor processor);
        public event ArchivingSession ArchivingSessionCompleted;

        public void StartProcess(int sessionId, string fileName, string filePath);
        public void DropActiveProcess();
        public IArchiveProcessor Clone();
        public IArchiveProcessor Clone(int archiveAttempts, float attemptDelaySec, int minSize); 
    }
}
