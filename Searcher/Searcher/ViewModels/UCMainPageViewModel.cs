using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Searcher.ViewModels
{
    public class UCMainPageViewModel:NotificationObject
    {
        /// <summary>
        /// 磁盘列表
        /// </summary>
        private DriveInfo[] diskList;

        public DriveInfo[] DiskList
        {
            get { return diskList; }
            set {
                diskList = value;
                RaisePrepertyChange("DiskList");
            }
        }

        /// <summary>
        /// 磁盘选择的显示状态
        /// </summary>
        private Visibility diskSelectPanelVisibility;

        public Visibility DiskSelectPanelVisibility
        {
            get { return diskSelectPanelVisibility; }
            set {
                diskSelectPanelVisibility = value;
                RaisePrepertyChange("DiskSelectPanelVisibility");
            }
        }

        /// <summary>
        /// 文件路径的显示状态
        /// </summary>
        private Visibility filePathselectPanelVisibility; 

        public Visibility FilePathselectPanelVisibility
        {
            get { return filePathselectPanelVisibility; }
            set {
                filePathselectPanelVisibility = value;
                RaisePrepertyChange("FilePathselectPanelVisibility");
            }
        }


        public UCMainPageViewModel()
        {
            DiskList = DriveInfo.GetDrives();
        }

        public void ChangeSearchLocationBox(string target)
        {

        }

    }
}
