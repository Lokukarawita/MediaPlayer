using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.WindowsAPICodePack.Taskbar;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ThumbnailToolBarButton btninfo;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            btninfo = new ThumbnailToolBarButton(Properties.Resources.iconfinder_icon_play_211876, "Info");
            btninfo.Click += btninfo_Click;

            TaskbarManager.Instance.ThumbnailToolBars.AddButtons(new System.Windows.Interop.WindowInteropHelper(this).Handle, btninfo);
        }

        void btninfo_Click(object sender, ThumbnailButtonClickedEventArgs e)
        {
            MessageBox.Show(this, "Aaa");
        }


    }
}
