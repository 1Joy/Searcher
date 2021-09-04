using Prism.Commands;
using Prism.Mvvm;
using Searcher.Core;
using Searcher.Models;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace Searcher.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand<string> WindowControlCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }

        public SearchExecutor SearchExecutorInstance { get; set; } = SearchExecutor.SearchExecutorInstance;

        /// <summary>
        /// 搜索的内容
        /// </summary>
        private string _searchInput;
        public string SearchInput
        {
            get { return _searchInput; }
            set
            {
                _searchInput = value;
                RaisePropertyChanged();                
            }
        }

        /// <summary>
        /// 搜索按钮能否被点击
        /// </summary>
        private bool _canSearch = true;
        public bool CanSearch
        {
            get { return _canSearch; }
            set { _canSearch = value;
                RaisePropertyChanged();
            }
        }


        /// <summary>
        /// 当前窗体显示状态
        /// </summary>
        private WindowState _currentWindowState = WindowState.Normal;
        public WindowState CurrentWindowState
        {
            get => _currentWindowState;
            set { _currentWindowState = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 最大化图标
        /// </summary>
        private string _maxBtnContent = "\ue62b";
        public string MaxBtnContent
        {
            get => _maxBtnContent;
            set { _maxBtnContent = value;
                RaisePropertyChanged();
            }
        }




        public MainWindowViewModel()
        {
            InitCommand();
        }

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitCommand()
        {
            WindowControlCommand = new DelegateCommand<string>(WindowControl);
            SearchCommand = new DelegateCommand(StartSearch, CanStartSearch).ObservesCanExecute(()=>CanSearch);
        }

        

        /// <summary>
        /// 控制窗体显示，最小化，最大化
        /// </summary>
        /// <param name="controlStr"></param>
        private void WindowControl(string controlStr)
        {
            if (controlStr == "min")
            {
                CurrentWindowState = WindowState.Minimized;
            }
            else if (controlStr == "max"&&CurrentWindowState==WindowState.Normal)
            {
                CurrentWindowState = WindowState.Maximized;
                MaxBtnContent = "\ue65b";
            }
            else if (controlStr == "max" && CurrentWindowState == WindowState.Maximized)
            {
                CurrentWindowState = WindowState.Normal;
                MaxBtnContent = "\ue62b";
            }
        }

        private bool CanStartSearch()
        {
            return CanSearch;
        }

        /// <summary>
        /// 开始检索
        /// </summary>
        private void StartSearch()
        {
            if (string.IsNullOrEmpty(SearchInput) || string.IsNullOrWhiteSpace(SearchInput))
            {
                return;
            }
            CanSearch = false;
            Task.Run(() =>
            {
                SearchExecutorInstance.StartSearch(SearchInput);
            }).ContinueWith((t)=> { CanSearch = true; });
            
        }
    }
}
