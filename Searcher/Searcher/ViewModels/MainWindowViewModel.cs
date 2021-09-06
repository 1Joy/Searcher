using Prism.Commands;
using Prism.Mvvm;
using Searcher.Core;
using Searcher.Models;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Searcher.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand<string> WindowControlCommand { get; set; }
        public DelegateCommand<string> OpenFloderCommand { get; set; }
        public DelegateCommand SearchCommand { get; set; }

        private SearchExecutor _searchExecutorInstance = SearchExecutor.SearchExecutorInstance;
        public SearchExecutor SearchExecutorInstance
        {
            get => _searchExecutorInstance; 
            set
            {
                _searchExecutorInstance = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 程序集版本
        /// </summary>
        private string _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        public string Version
        {
            get { return _version; }
            set { _version = value;
                RaisePropertyChanged();
            }
        }


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

        /// <summary>
        /// 是否显示结果总体描述
        /// </summary>
        private Visibility _showResultMsg = Visibility.Hidden;
        public Visibility ShowResultMsg
        {
            get { return _showResultMsg; }
            set { _showResultMsg = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 搜索进度
        /// </summary>
        private SearchProgressModel _searchProgress;
        public SearchProgressModel SearchProgress
        {
            get => _searchProgress; 
            set
            {
                _searchProgress = value;
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
            OpenFloderCommand = new DelegateCommand<string>(OpenFloder);
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
            var sp = new Stopwatch();
            Task.Run(() =>
            {
                sp.Start();
                SearchProgress = new SearchProgressModel();
                SearchExecutorInstance.StartSearch(SearchInput, SearchProgress);
            }).ContinueWith((t)=> {
                sp.Stop();
                CanSearch = true;
                ShowResultMsg = Visibility.Visible;
                SearchProgress.UseTime = sp.ElapsedMilliseconds/1000;
            });
            
        }

        /// <summary>
        /// 打开文件路径位置
        /// </summary>
        /// <param name="floderPath">文件路径</param>
        private void OpenFloder(string floderPath)
        {
            //q
            Process.Start("Explorer", "/select," + floderPath);
        }
    }
}
