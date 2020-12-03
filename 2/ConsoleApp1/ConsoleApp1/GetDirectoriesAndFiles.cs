using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class GetDirectoriesAndFiles: IGetDirectoriesAndFiles
    {
        public string LastPath;
        public IEnumerable<string> GetDirectories(string path, string pattern)
        {
            return Directory.EnumerateDirectories(path, pattern, SearchOption.AllDirectories);
        }

        public IEnumerable<string> GetFiles(string path, string pattern)
        {
            return Directory.EnumerateFiles(path, pattern, SearchOption.AllDirectories);
        }

        public  void LogPath(string path)
        {
            LastPath = path;
            try
            {
                using (StreamWriter fileLog = File.AppendText("Log.txt"))
                {
                    fileLog.WriteLine(LastPath);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
