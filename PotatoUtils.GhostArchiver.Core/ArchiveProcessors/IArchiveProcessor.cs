using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoUtils.GhostArchiver.Core
{
    public interface IArchiveProcessor
    {
        public void StartProcess(string fileName, string filePath);
        public IArchiveProcessor Clone();
        public IArchiveProcessor Clone(int archiveAttempts, float attemptDelaySec, int minSize); 
    }
}
