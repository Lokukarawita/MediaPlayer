using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EvoPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MediaLibrary _ml;

        public MainWindow()
        {
            InitializeComponent();

            this.Closing += MainWindow_Closing;

            _ml = new MediaLibrary();


            Core.IO.SearchLocalDisk disk = new Core.IO.SearchLocalDisk();
            disk.SearchProgress += Disk_SearchProgress;
            disk.SearchCompleted += Disk_SearchCompleted;
            MessageBox.Show("Test");
            disk.Search(@"D:\Songs\A\English", Core.Data.AppSettings.ALL_SUPPORTED_EXTENTIONS);
        }

        private void Disk_SearchCompleted(object sender, Core.IO.SearchCompletedEventArgs e)
        {
            Console.WriteLine("Found " + e.FoundFiles.Count);
        }

        private void Disk_SearchProgress(object sender, Core.IO.SearchProgressEventArgs e)
        {
            Console.WriteLine("Searching " + e.CurrentDirectory + ", Found " + e.FoundFiles.Count);
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShutDown();
        }

        private void ShutDown()
        {
            _ml.ShutDown();
        }

        private void btnML_Click(object sender, RoutedEventArgs e)
        {
            if (_ml != null)
            {
                if (_ml.IsVisible && !_ml.IsActive)
                {
                    _ml.Focus();
                }
                else if (!_ml.IsVisible)
                {
                    _ml.Show();
                }
            }
        }
    }
}
