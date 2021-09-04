using Microsoft.VisualStudio.TestTools.UnitTesting;
using Searcher.Core.Searcher;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.Test
{
    [TestClass]
    public class MFTTest
    {
        [TestMethod]
        public void Test1()
        {
            var Mft = new MFTScanner();
            var sp = new Stopwatch();
            sp.Start();
            var result = Mft.EnumerateFiles("d:").ToList();
            Debug.WriteLine($"iii:{sp.ElapsedMilliseconds.ToString()}");
            sp.Restart();
            var result1 = Mft.EnumerateFiles("d:",new string[] { ".txt",".doc",".docx",".pdf"}).ToList();
            Debug.WriteLine($"iiiee:{sp.ElapsedMilliseconds.ToString()}");
        }
    }
}
