using EvoPlayer.Core.Data;
using EvoPlayer.Core.IO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using TagLib;

namespace EvoPlayer
{
    /// <summary>
    /// Interaction logic for FileAdder.xaml
    /// </summary>
    public partial class FileAdder : Window
    {
        private delegate void DelAddCompleted(decimal total, decimal failed);
        private delegate void DelUpdateAddStatus(decimal total, decimal current, decimal failed);

        private Dispatcher uiThread = Dispatcher.CurrentDispatcher;
        private bool cancel = false;
        private Queue<string> filesToAdd;
        private SearchLocalDisk searcher;

        public FileAdder()
        {
            InitializeComponent();

            this.Closing += FileAdder_Closing;

            searcher = new SearchLocalDisk();
            searcher.SearchProgress += Searcher_SearchProgress;
            searcher.SearchCompleted += Searcher_SearchCompleted;
        }

        private void FileAdder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (searcher.IsSearching)
            {
                e.Cancel = true;
            }
        }

        private void Searcher_SearchCompleted(object sender, SearchCompletedEventArgs e)
        {
            if (e.FoundFiles.Count > 0)
            {
                filesToAdd = new Queue<string>(e.FoundFiles);
                prbProgress.IsIndeterminate = false;
                btnCancel.IsEnabled = true;
                Task.Factory.StartNew(() => AddFilesTask());
            }
            else
            {
                btnClose.IsEnabled = false;
                btnBrowse.IsEnabled = false;
            }
        }

        private void Searcher_SearchProgress(object sender, SearchProgressEventArgs e)
        {
            var currentFldr = e.CurrentDirectory;
            var foundCount = e.FoundFiles.Count;
            lblStatus.Content = $"Searching {currentFldr}, Found {foundCount} files";
        }

        private void AddFilesTask()
        {
            decimal total = filesToAdd.Count;
            decimal failed = 0;
            decimal current = 0;
            while (filesToAdd.Count > 0)
            {
                if (cancel)
                {
                    break;
                }
                else
                {
                    current++;

                    try
                    {
                        var item = filesToAdd.Dequeue();
                        var tagFile = TagLib.File.Create(item);

                        var artist = string.IsNullOrWhiteSpace(tagFile.Tag.Performers.FirstOrDefault()) ? "Unkown Artist" : tagFile.Tag.Performers.FirstOrDefault();
                        var title = string.IsNullOrWhiteSpace(tagFile.Tag.Title) ? "Unkown Track" : tagFile.Tag.Title;
                        var album = string.IsNullOrWhiteSpace(tagFile.Tag.Album) ? "Unkown Track" : tagFile.Tag.Album;
                        var duration = tagFile.Properties.Duration;
                        var albumArt = tagFile.Tag.Pictures != null && tagFile.Tag.Pictures.Length > 0 ? true : false;

                        Core.Data.Domain.LocalMediaItem localMI = new Core.Data.Domain.LocalMediaItem()
                        {
                            Album = album,
                            Artist = artist,
                            Duration = duration,
                            HasAlbumArt = albumArt,
                            Path = item,
                            StorageType = Core.Data.Domain.MediaStorageType.Local,
                            Title = title
                        };
                        Core.Data.DB.AddLocalMedia(localMI);

                        tagFile.Dispose();

                        var del = new DelUpdateAddStatus(this.UpdateAddStatus);
                        uiThread.Invoke(del, total, current, failed);

                    }
                    catch (Exception)
                    {
                        failed++;
                    }


                }
            }

            if (!cancel)
            {
                var del = new DelAddCompleted(this.AddCompleted);
                uiThread.Invoke(del, current - failed, failed);
            }
        }

        private void UpdateAddStatus(decimal total, decimal current, decimal failed)
        {
            if (!cancel)
            {
                string txtAdded = (current - failed).ToString();
                string txtFailed = failed == 0 ? "None" : failed.ToString();
                lblStatus.Content = $"Added {txtAdded} file(s), {txtFailed} failed";

                decimal pcnt = ((current / total) * (decimal)100);
                prbProgress.Value = (double)pcnt;
            }
        }
        private void AddCompleted(decimal added, decimal failed)
        {
            btnBrowse.IsEnabled = true;
            btnClose.IsEnabled = true;
            btnCancel.IsEnabled = false;
            prbProgress.Value = 0;
            lblStatus.Content = $"Done, Added {added} file(s)";
        }

        private void btnBrowse_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = Properties.Settings.Default.LastSearchLocation;
            dlg.ShowNewFolderButton = false;
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings.Default.LastSearchLocation = dlg.SelectedPath;
                Properties.Settings.Default.Save();

                txtLocation.Text = dlg.SelectedPath;

                btnClose.IsEnabled = false;
                btnBrowse.IsEnabled = false;
                prbProgress.IsIndeterminate = true;
                cancel = false;
                btnCancel.IsEnabled = false;

                searcher.Search(txtLocation.Text, AppSettings.ALL_SUPPORTED_EXTENTIONS);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            cancel = true;

            btnBrowse.IsEnabled = true;
            btnClose.IsEnabled = true;
            btnCancel.IsEnabled = false;
            prbProgress.Value = 0;
            lblStatus.Content = "";
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
