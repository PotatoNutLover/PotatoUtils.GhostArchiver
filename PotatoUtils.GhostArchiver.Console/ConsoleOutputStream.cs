using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PotatoUtils.Logger;

namespace PotatoUtils.GhostArchiver.ConsoleApp
{
    internal class ConsoleOutputStream : IOutputStream
    {
        public void WriteMessage(MessageInfo messageInfo)
        {
            Console.WriteLine(messageInfo.Message+"\n");
        }
    }
}
