using System;
using System.Collections.Generic;
using System.IO;
using ConsoleApp1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace UnitTestsConsoleApp1
{
    [TestClass]
    public class Class1
    {
        static string PathForMockTest = @"C:\Старые данные\Программа с регистрацией";
        static string PatternForMockTest = "*";

        private FileSystemVisitor _fileSystemVisitor;
        private GetDirectoriesAndFiles _getDirectoriesAndFiles;

        [TestInitialize]
        public void Setup()
        {
            _getDirectoriesAndFiles = new GetDirectoriesAndFiles();

            _fileSystemVisitor = new FileSystemVisitor(PathForMockTest, 3 ,_getDirectoriesAndFiles, PatternForMockTest);
        }

        [TestMethod]
        public void GetDirectories_NullExeptionPath_ExpectedExeption()
        {
            string path = null;
            string pattern = "*";

            _getDirectoriesAndFiles = new GetDirectoriesAndFiles();

            Assert.ThrowsException<ArgumentNullException>(() => _getDirectoriesAndFiles.GetDirectories(path, pattern));
        }

        [TestMethod]
        public void GetFiles_NullExeptionPath_ExpectedExeption()
        {
            string path = null;
            string pattern = "*";

            _getDirectoriesAndFiles = new GetDirectoriesAndFiles();

            Assert.ThrowsException<ArgumentNullException>(() => _getDirectoriesAndFiles.GetFiles(path, pattern));
        }

        [TestMethod]
        public void GetPath_StringPath_ExpectedReturnLastPath()
        {
            _fileSystemVisitor.AnalizePath(PathForMockTest);

            Assert.AreEqual(PathForMockTest, _getDirectoriesAndFiles.LastPath);
        }
    }
}
