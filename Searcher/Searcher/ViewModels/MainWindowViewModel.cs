using Prism.Commands;
using Prism.Mvvm;
using System.Windows;

namespace Searcher.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public DelegateCommand<string> WindowControlCommand { get; private set; }

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
    }
}
