using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.Core.Searcher
{
    /// <summary>
    /// 检索父类
    /// </summary>
    public abstract class BaseSearcher
    {
        public delegate void ReportFindFileDelegate(string fileFullPath);
        public ReportFindFileDelegate ReportFindFileEvent;

        public EventHandler FindNextEvent;

        /// <summary>
        /// 检索类型
        /// </summary>
        protected string Tag { get; set; }

        /// <summary>
        /// 检索的文件后缀名,如果有多个，就用竖线分开；如：.doc|.docx
        /// </summary>
        public string FileSuffixs { get; protected set; }

        protected BaseSearcher(string tag,string suffixs)
        {
            Tag = tag;
            FileSuffixs = suffixs;
        }

        /// <summary>
        /// 开始搜索
        /// </summary>
        /// <param name="targetStr">目标文字</param>
        /// <param name="fileNames">搜索的文件名称</param>
        public void StartSearch(string targetStr, List<string> fileNames)
        {
            //先过滤
            fileNames = FilterFileSuffix(fileNames);
            foreach (var item in fileNames)
            {
                FindNextEvent?.Invoke(this, EventArgs.Empty);
                if (!File.Exists(item))
                    continue;
                if(SearchByTargetStr(targetStr, item))
                {
                    ReportFindFileEvent?.Invoke(item);
                }
                
            }
        }

        /// <summary>
        /// 过滤目录
        /// </summary>
        /// <param name="fileNames"></param>
        /// <returns></returns>
        protected abstract List<string> FilterFileSuffix(List<string> fileNames);

        /// <summary>
        /// 根据关键字搜索文件名和文件内容
        /// </summary>
        /// <param name="targetStr">搜索关键字</param>
        /// <param name="fileFullPath">文件名</param>
        /// <returns>true：搜索到了关键字</returns>
        protected virtual bool SearchByTargetStr(string targetStr, string fileFullPath)
        {            
            //搜索文件名称里面是否包含关键字
            string name = Path.GetFileName(fileFullPath);
            if (name.Contains(targetStr))
                return true;
            return false;
        }

        /// <summary>
        /// 判断路径是否是目录
        /// </summary>
        /// <param name="path">待判断路径</param>
        /// <returns></returns>
        protected bool PathIsDirectory(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            return (fileInfo.Attributes & FileAttributes.Directory) != 0;
        }
    }
}
