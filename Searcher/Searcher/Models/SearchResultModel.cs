using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.Models
{
    public class SearchResultModel
    {
        public string FileName { get; set; }
        public string FileFullPath { get; set; }

        public SearchResultModel(string file)
        {
            FileName = Path.GetFileName(file);
            FileFullPath = file;
        }
    }
}
