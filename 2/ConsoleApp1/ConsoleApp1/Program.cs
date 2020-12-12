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

            GetDirectoriesAndFiles getDirectoriesAndFiles = new GetDirectoriesAndFiles();

            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(path: path, countItems: 4, getDirectoriesAndFiles: getDirectoriesAndFiles, pattern: FuncMethod(pattern, filter => pattern));

            FilteredElementsArgs filteredElementsArgs = new FilteredElementsArgs();
            ElementsArgs elementsArgs = new ElementsArgs();

            #region Events
            fileSystemVisitor.SearchStarted += (e, Args) => { Console.WriteLine("Search Started"); };
            fileSystemVisitor.SearchFinished += (e, Args) => { Console.WriteLine("Search Finished"); };

            fileSystemVisitor.DirectoryFinded += (e, Args) => { Console.WriteLine("Directory Finded"); };
            fileSystemVisitor.FileFinded += (e, Args) => { Console.WriteLine("File Finded"); };

            fileSystemVisitor.FilteredDirectoryFinded += (e, Args) => { Console.WriteLine("Filtered Directory Finded"); };
            fileSystemVisitor.FilteredFileFinded += (e, Args) => { Console.WriteLine("Filtered File Finded"); };
            #endregion 

            List<string> listItems = new List<string>();

            try
            {
                Console.WriteLine("Списокк элементов: ");
                foreach (var DirectoryOrFile in fileSystemVisitor.SearchDirectoryAndFiles())
                {
                    listItems.Add(DirectoryOrFile);
                    Console.WriteLine(DirectoryOrFile);
                }

                if(filteredElementsArgs.FilteredRemoveItem == true || elementsArgs.RemoveItem == true)
                {
                    Console.Write("\nУкажите номер элемента: ");
                    int number = Convert.ToInt32(Console.ReadLine());

                    listItems.RemoveAt(number);
                }
               
                Console.WriteLine("\nСписок элементов после удаления: ");
                foreach (var item in listItems)
                {
                    Console.WriteLine(item);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static string FuncMethod(string filter, Func<string, string> filters) => filters(filter);
    }
}





