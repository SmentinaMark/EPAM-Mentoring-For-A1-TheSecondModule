using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"C:\Старые данные\Программа с регистрацией";

            string pattern = "*";

            GetDirectoriesAndFiles directoriesAndFiles = new GetDirectoriesAndFiles();

            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path: path, count: 4, directoriesAndFiles: directoriesAndFiles, pattern: Func(pattern, filter => pattern));

            #region Events
            fileSystemVisitor.SearchStarted += (e, Args) => { Console.WriteLine("Search Started"); };
            fileSystemVisitor.SearchFinished += (e, Args) => { Console.WriteLine("Search Finished"); };

            fileSystemVisitor.DirectoryFinded += (e, Args) => { Console.WriteLine("Directory Finded"); };
            fileSystemVisitor.FileFinded += (e, Args) => { Console.WriteLine("File Finded"); };

            fileSystemVisitor.FilteredDirectoryFinded += (e, Args) => { Console.WriteLine("Filtered Directory Finded"); };
            fileSystemVisitor.FilteredFileFinded += (e, Args) => { Console.WriteLine("Filtered File Finded"); };
            #endregion 

            try
            {
                foreach (var FoldersAndFiles in fileSystemVisitor.SearchDirectoryAndFiles())
                {
                    Console.WriteLine(FoldersAndFiles);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static string Func(string filter, Func<string, string> filters) => filters(filter);
    }
}





