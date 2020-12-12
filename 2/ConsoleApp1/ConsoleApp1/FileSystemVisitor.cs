using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    public partial class FileSystemVisitor : IEnumerable<string>
    {
        int i = 0;

        private readonly string _path;
        private readonly int _countItems;
        private readonly string _pattern;

        private readonly IGetDirectoriesAndFiles _getDirectoriesAndFiles;

        bool filter = true;

        public FileSystemVisitor(string path, int countItems, IGetDirectoriesAndFiles getDirectoriesAndFiles, string pattern = null)
        {
            if (pattern == null)
            {
                filter = false;
            }

            _path = path;
            _countItems = countItems;
            _pattern = pattern != null ? pattern : "*";

            _getDirectoriesAndFiles = getDirectoriesAndFiles;

            AnalizePath(_path);
        }

        public IEnumerable<string> SearchDirectoryAndFiles()
        {
            OnSearchStarted(new EventArgs());

            var filteredElementArgs = new FilteredElementsArgs();
            var elementArgs = new ElementsArgs();

            var directories = _getDirectoriesAndFiles.GetDirectories(_path, _pattern);
            foreach (string directory in directories)
            {
                if(filter)
                {
                    OnFilteredDirectoryFinded(filteredElementArgs);

                    yield return directory;

                    i++;
                   
                    if (filteredElementArgs.FilteredStopSearch && Stop(_countItems))
                    {
                        break;
                    }
                }
                else
                {
                    OnDirectoryFinded(elementArgs);

                    yield return directory;

                    i++;
                   
                    if (elementArgs.StopSearch && Stop(_countItems))
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();

            i = 0;

            var files = _getDirectoriesAndFiles.GetFiles(_path, _pattern);
            foreach (string file in files)
            {
                if(filter)
                {
                    OnFilteredFileFinded(filteredElementArgs);

                    yield return file;

                    i++;

                    if (filteredElementArgs.FilteredStopSearch && Stop(_countItems))
                    {
                        break;
                    }
                }
                else
                {
                    OnFileFinded(elementArgs);

                    yield return file;

                    i++;

                    if (elementArgs.StopSearch && Stop(_countItems))
                    {
                        break;
                    }
                }
            }
            OnSearchCompleted(new EventArgs());
        }
        public bool Stop(int count)
        {
            if (i >= count)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AnalizePath(string path)
        {
            if(path != null)
            {
                _getDirectoriesAndFiles.LogPath(path);
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            return SearchDirectoryAndFiles().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #region Events
        public event EventHandler<EventArgs> SearchStarted;
        public event EventHandler<EventArgs> SearchFinished;

        public event EventHandler<ElementsArgs> DirectoryFinded; 
        public event EventHandler<ElementsArgs> FileFinded; 

        public event EventHandler<FilteredElementsArgs> FilteredDirectoryFinded; 
        public event EventHandler<FilteredElementsArgs> FilteredFileFinded;

        protected virtual void OnSearchStarted(EventArgs args)
        {
            SearchStarted?.Invoke(this, args);
        }

        protected virtual void OnSearchCompleted(EventArgs args)
        {
            SearchFinished?.Invoke(this, args);
        }

        protected virtual void OnDirectoryFinded(ElementsArgs args)
        {
            DirectoryFinded?.Invoke(this, args);
        }

        protected virtual void OnFileFinded(ElementsArgs args)
        {
            FileFinded?.Invoke(this, args);
        }

        protected virtual void OnFilteredDirectoryFinded(FilteredElementsArgs args)
        {
            FilteredDirectoryFinded?.Invoke(this, args);
        }

        protected virtual void OnFilteredFileFinded(FilteredElementsArgs args)
        {
            FilteredFileFinded?.Invoke(this, args);
        }
        #endregion
    }
}
