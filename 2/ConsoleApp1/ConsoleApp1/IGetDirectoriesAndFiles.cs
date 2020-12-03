using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public interface IGetDirectoriesAndFiles
    {
       IEnumerable<string> GetDirectories(string path, string pattern);
       IEnumerable<string> GetFiles(string path, string pattern);
       void LogPath(string path);
    }
}
