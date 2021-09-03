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
    public class BaseSearcher
    {
        /// <summary>
        /// 检索类型
        /// </summary>
        protected string Tag { get; set; }

        /// <summary>
        /// 开始搜索
        /// </summary>
        /// <param name="targetStr">目标文字</param>
        /// <param name="paths">搜索的路径</param>
        public void StartSearch(string targetStr,string[] paths)
        {
            foreach (var path in paths)
            {
                if (PathIsDirectory(path))
                {
                    //是文件夹就读取文件
                }
            }
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
