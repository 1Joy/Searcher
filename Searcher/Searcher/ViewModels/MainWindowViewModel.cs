using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Searcher.ViewModels
{
    public class MainWindowViewModel:NotificationObject
    {
        private Visibility mainPageVisibility;

        public Visibility MainPageVisibility
        {
            get { return mainPageVisibility; }
            set {
                mainPageVisibility = value;
                RaisePrepertyChange("MainPageVisibility");
            }
        }

        private Visibility filePageVisibility;

        public Visibility FilePageVisibility
        {
            get { return filePageVisibility; }
            set {
                filePageVisibility = value;
                RaisePrepertyChange("FilePageVisibility");
            }
        }


    }
}
