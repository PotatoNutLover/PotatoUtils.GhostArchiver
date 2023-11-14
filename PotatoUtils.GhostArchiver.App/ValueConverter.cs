using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PotatoUtils.GhostArchiver.App
{
    internal static class ValueConverter
    {
        public static float BytesToMbytes(int bytes)
        {
            return (float)bytes / 1024f / 1024f;
        }

        public static int MbytesToBytes(float Mbytes)
        {
            return (Int32)(Mbytes * 1024f * 1024f);
        }

    }
}
