using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoUtils.GhostArchiver.Core
{
    public class DefaultNotifyFilterPreset : INotifyFilter
    {
        public NotifyFilters GetFilter()
        {
            NotifyFilters notifyFilters =
                NotifyFilters.LastAccess |
                NotifyFilters.LastWrite |
                NotifyFilters.FileName |
                NotifyFilters.DirectoryName;

            return notifyFilters;
        }
    }
}
