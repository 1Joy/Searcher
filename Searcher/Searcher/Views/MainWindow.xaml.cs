using System.Windows;

namespace Searcher.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DockPanel_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(e.LeftButton== System.Windows.Input.MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }
    }
}
