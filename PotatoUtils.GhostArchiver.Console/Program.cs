using PotatoUtils.GhostArchiver.Core;
using FileInfo = PotatoUtils.GhostArchiver.Core.FileInfo;

namespace PotatoUtils.GhostArchiver.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            FileInfo fileInfo = new FileInfo("C:\\Users\\potatoNut\\Desktop\\харлоги", "*.har");
            Archiver archiver = new Archiver(fileInfo);
            archiver.OnMessageLogged += Log;
            while (Console.Read() != 'q') ;
        }

        static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}