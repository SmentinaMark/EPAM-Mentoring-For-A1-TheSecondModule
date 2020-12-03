using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp1
{
    public partial class FileSystemVisitor : IEnumerable<string>
    {
        int i = 0;

        private readonly string _path;
        private readonly int _countElement;
        private readonly string _pattern;

        private readonly IGetDirectoriesAndFiles _directoriesAndFiles;

        bool filter = true;

        public FileSystemVisitor(string path, int count ,IGetDirectoriesAndFiles directoriesAndFiles, string pattern = null)
        {
            if (pattern == null)
            {
                filter = false;
            }

            _path = path;
            _countElement = count;
            _pattern = pattern != null ? pattern : "*";

            _directoriesAndFiles = directoriesAndFiles;

            AnalizePath(_path);
        }

        public IEnumerable<string> SearchDirectoryAndFiles()
        {
            OnSearchStarted(new EventArgs());

            var directories = _directoriesAndFiles.GetDirectories(_path, _pattern);
            foreach (string directory in directories)
            {
                if(filter)
                {
                    OnFilteredDirectoryFinded(new EventArgs());
                    yield return directory;

                    i++;
                    if (Stop(_countElement))
                    {
                        break;
                    }
                }
                else
                {
                    OnDirectoryFinded(new EventArgs());
                    yield return directory;

                    i++;
                    if (Stop(_countElement))
                    {
                        break;
                    }
                }
            }
            Console.WriteLine();

            i = 0;

            var files = _directoriesAndFiles.GetFiles(_path, _pattern);
            foreach (string file in files)
            {
                if(filter)
                {
                    OnFilteredFileFinded(new EventArgs());
                    yield return file;

                    i++;
                    if (Stop(_countElement))
                    {
                         break;
                    }
                }
                else
                {
                    OnFileFinded(new EventArgs());
                    yield return file;

                    i++;
                    if (Stop(_countElement))
                    {
                         break;
                    }
                }
            }
            OnSearchCompleted(new EventArgs());
        }
        public bool Stop(int count)
        {
            if(i >= count)
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
                _directoriesAndFiles.LogPath(path);
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

        public event EventHandler<EventArgs> DirectoryFinded;
        public event EventHandler<EventArgs> FileFinded;

        public event EventHandler<EventArgs> FilteredDirectoryFinded;
        public event EventHandler<EventArgs> FilteredFileFinded;

        protected virtual void OnSearchStarted(EventArgs args)
        {
            SearchStarted?.Invoke(this, args);
        }

        protected virtual void OnSearchCompleted(EventArgs args)
        {
            SearchFinished?.Invoke(this, args);
        }

        protected virtual void OnDirectoryFinded(EventArgs args)
        {
            DirectoryFinded?.Invoke(this, args);
        }

        protected virtual void OnFileFinded(EventArgs args)
        {
            FileFinded?.Invoke(this, args);
        }

        protected virtual void OnFilteredDirectoryFinded(EventArgs args)
        {
            FilteredDirectoryFinded?.Invoke(this, args);
        }

        protected virtual void OnFilteredFileFinded(EventArgs args)
        {
            FilteredFileFinded?.Invoke(this, args);
        }
        #endregion
    }
}
