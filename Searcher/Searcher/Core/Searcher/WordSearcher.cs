using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.Core.Searcher
{
    /// <summary>
    /// 
    /// </summary>
    public class WordSearcher : BaseSearcher
    {
        public WordSearcher() : base("word", ".doc|.docx")
        {
        }

        protected override List<string> FilterFileSuffix(List<string> fileNames)
        {
            return fileNames.Where(name => Path.GetExtension(name) == ".doc"|| Path.GetExtension(name)==".docx").ToList();
        }

        protected override bool SearchByTargetStr(string targetStr, string fileFullPath)
        {
            if (base.SearchByTargetStr(targetStr, fileFullPath))
                return true;
            return false;
        }
    }
}
