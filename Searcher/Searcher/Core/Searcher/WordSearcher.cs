using NPOI.HWPF;
using NPOI.POIFS.FileSystem;
using NPOI.XWPF.UserModel;
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
            using(FileStream fs = new FileStream(fileFullPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                ///todo 这里有一个问题就是直接将其他后缀改为目标后缀会读取失败
                if (fs.Length == 0)
                    return false;
                if (Path.GetExtension(fileFullPath) == ".docx")
                    return SearchDocx(targetStr, fs);
                else
                    return SearchDoc(targetStr, fs);
            }
        }

        /// <summary>
        /// 搜索docx文档
        /// </summary>
        /// <param name="fileFullPath"></param>
        private bool SearchDocx(string targetStr, FileStream fs)
        {
            XWPFDocument document = new XWPFDocument(fs);
            foreach (var paragraph in document.Paragraphs)
            {
                if (paragraph.Text.Contains(targetStr))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 搜索doc文档
        /// </summary>
        /// <param name="targetStr"></param>
        /// <param name="fileFullPath"></param>
        /// <returns></returns>
        private bool SearchDoc(string targetStr, FileStream fs)
        {
            POIFSFileSystem pOIFS = new POIFSFileSystem(fs);
            HWPFDocument document = new HWPFDocument(pOIFS);
            for (int i = 0; i < document.GetRange().NumParagraphs; i++)
            {
                if (document.GetRange().GetParagraph(i).Text.Contains(targetStr))
                    return true;
            }
            return false;
        }
    }
}
