using Searcher.Core.Searcher;
using Searcher.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Searcher.Core
{
    /// <summary>
    /// 检索执行器
    /// </summary>
    public class SearchExecutor
    {
        public static SearchExecutor SearchExecutorInstance = new SearchExecutor();
        private List<BaseSearcher> _searchers;
        /// <summary>
        /// 检索的文件后缀
        /// </summary>
        private string[] _searchFileSuffixs;

        /// <summary>
        /// 搜索结果
        /// </summary>
        public ObservableCollection<SearchResultModel> SearchResults { get; set; } = new ObservableCollection<SearchResultModel>();

        /// <summary>
        /// 搜索进度
        /// </summary>
        public SearchProgressModel SearchProgress { get; set; }


        private SearchExecutor()
        {
            CreateSearcher();
        }

        /// <summary>
        /// 创建检索对象
        /// </summary>
        private void CreateSearcher()
        {            
            var txtSearcher = new TxtSearcher();
            var wordSearcher = new WordSearcher();
            txtSearcher.ReportFindFileEvent += AddResult;
            wordSearcher.ReportFindFileEvent += AddResult;

            txtSearcher.FindNextEvent += UpdateSearchProgress;
            wordSearcher.FindNextEvent += UpdateSearchProgress;

            _searchers = new List<BaseSearcher>
            {
                txtSearcher,
                //wordSearcher
            };
            List<string> tempSuffix = new List<string> ();
            foreach (var item in _searchers)
            {
                tempSuffix.AddRange(item.FileSuffixs.Split('|'));
            }
            _searchFileSuffixs = tempSuffix.ToArray();
        }

        /// <summary>
        /// 开始执行检索
        /// </summary>
        public void StartSearch(string targetStr)
        {
            var mft = new MFTScanner();
            SearchProgress = new SearchProgressModel();
            List<string> fileFullPaths = new List<string>();

            //遍历磁盘,获取文件名
            foreach (var item in DriveInfo.GetDrives().Where(drive => drive.DriveType == DriveType.Fixed))
            {                
                fileFullPaths.AddRange(mft.EnumerateFiles(item.Name, _searchFileSuffixs).ToList());
            }
            SearchProgress.TotalFileCount = fileFullPaths.Count;
            Parallel.ForEach(_searchers, search =>
            {
                search.StartSearch(targetStr, fileFullPaths);
            });
        }

        /// <summary>
        /// 将找到的结果文件，添加到记录
        /// </summary>
        /// <param name="fileName">找到的文件全路径</param>
        private void AddResult(string fileName)
        {
            var result = new SearchResultModel(fileName);
            Application.Current.Dispatcher.Invoke(() =>
            {
                SearchResults.Add(result);
            });
        }

        /// <summary>
        /// 更新已搜索文件数目，更新进度
        /// </summary>
        /// <param name="s"></param>
        /// <param name="args"></param>
        private void UpdateSearchProgress(object s,EventArgs args)
        {
            SearchProgress.SearchedFileCount++;
        }
    }
}
