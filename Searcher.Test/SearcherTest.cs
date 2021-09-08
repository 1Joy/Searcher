using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searcher.Core.Searcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Searcher.Test
{
    [TestClass]
    public class SearcherTest
    {
        [TestMethod]
        [DataRow("今天", @"D:\Code\JoyCode\Searcher\Searcher.Test\bin\Debug\testFiles\testFile1.docx")]
        public void Test_SearchWordDocx(string targetStr, string fileFullPath)
        {
            BaseSearcher searcher = new WordSearcher();
            searcher.StartSearch(targetStr,new List<string>() { fileFullPath });
            BaseSearcher.ReportFindFileEvent += (file) =>
            {
                Assert.AreEqual(fileFullPath, file);
            };

            Thread.Sleep(1000);
        }

        [TestMethod]
        [DataRow("今天", @"D:\Code\JoyCode\Searcher\Searcher.Test\bin\Debug\testFiles\testFile2.doc")]
        public void Test_SearchWordDoc(string targetStr, string fileFullPath)
        {
            BaseSearcher searcher = new WordSearcher();
            searcher.StartSearch(targetStr, new List<string>() { fileFullPath });
            BaseSearcher.ReportFindFileEvent += (file) =>
            {
                Assert.AreEqual(fileFullPath, file);
            };

            Thread.Sleep(1000);
        }
    }
}
