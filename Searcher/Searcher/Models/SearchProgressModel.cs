using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher.Models
{
    /// <summary>
    /// 搜索进度
    /// </summary>
    public class SearchProgressModel : BindableBase
    {
        /// <summary>
        /// 搜索总文件数
        /// </summary>
        private int _totalFileCount;
        public int TotalFileCount
        {
            get { return _totalFileCount; }
            set
            {
                _totalFileCount = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 已经搜索过的文件数
        /// </summary>
        private int _searchedFileCount;
        public int SearchedFileCount
        {
            get { return _searchedFileCount; }
            set
            {
                _searchedFileCount = value;
                //SearchProgress = SearchedFileCount / (double)TotalFileCount;
            }
        }

        /// <summary>
        /// 进度
        /// </summary>
        private double _searchProgress;
        public double SearchProgress
        {
            get { return _searchProgress; }
            set
            {
                _searchProgress = value;
                RaisePropertyChanged();
            }
        }
    }
}
