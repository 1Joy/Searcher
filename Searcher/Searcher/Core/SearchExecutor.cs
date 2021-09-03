using Searcher.Core.Searcher;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.Core
{
    /// <summary>
    /// 检索执行器
    /// </summary>
    public class SearchExecutor
    {
        public static SearchExecutor SearchExecutorInstance = new SearchExecutor();
        private List<BaseSearcher> _searchers;

        private SearchExecutor()
        {

            CreateSearcher();
        }

        /// <summary>
        /// 创建检索对象
        /// </summary>
        private void CreateSearcher()
        {
            _searchers = new List<BaseSearcher>();
        }

        /// <summary>
        /// 开始执行检索
        /// </summary>
        public void StartSearch(string targetStr)
        {
            //读取磁盘
            var driveNames = DriveInfo.GetDrives().Where(drive=>drive.DriveType== DriveType.Fixed).Select(dirve=>dirve.Name);
            foreach (var item in _searchers)
            {
                //item
            }
        }
    }
}
