using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EvoPlayer.Core.IO
{
    public class SearchLocalDisk
    {
        private System.Windows.Threading.Dispatcher uidisp;
        private string startingDir;
        private string[] filters;

        public event EventHandler<SearchCompletedEventArgs> SearchCompleted;
        public event EventHandler<SearchProgressEventArgs> SearchProgress;


        public SearchLocalDisk()
        {

        }

        private void OnSearchProgress(string directory, List<string> found, Queue<string> subdir, List<string> failedDir)
        {
            if (SearchProgress != null)
            {
                var evt = new SearchProgressEventArgs()
                {
                    CurrentDirectory = directory,
                    FailedDirectories = failedDir,
                    FoundFiles = found,
                    RemainingDirectoryCount = subdir.Count

                };
                uidisp.Invoke(SearchProgress, this, evt);
            }
        }
        private void OnSearchCompleted(string directory, List<string> found, Queue<string> subdir, List<string> failedDir, int totaldir)
        {
            if (SearchCompleted != null)
            {
                var evt = new SearchCompletedEventArgs()
                {
                    CurrentDirectory = directory,
                    FailedDirectories = failedDir,
                    FoundFiles = found,
                    RemainingDirectoryCount = 0,
                    TotalDirectoryCount = totaldir,
                    Status = true
                };
                uidisp.Invoke(SearchCompleted, this, evt);
            }
        }

        private List<string> DoSearch(string directory, string[] filters, List<string> found, Queue<string> subdir, List<string> failedDir, int dircount)
        {
            try
            {
                OnSearchProgress(directory, found, subdir, failedDir);
            }
            catch (Exception)
            {


            }


            try
            {
                var dirs = Directory.GetDirectories(directory, "*.*", SearchOption.TopDirectoryOnly);
                if (dirs.Length > 0)
                {
                    dirs.ToList().ForEach(x => { subdir.Enqueue(x); });
                }
            }
            catch (Exception)
            {
                failedDir.Add(directory);
            }


            foreach (var filter in filters)
            {
                try
                {
                    var files = Directory.GetFiles(directory, filter, SearchOption.TopDirectoryOnly).ToList();
                    found.AddRange(files);

                }
                catch (Exception) { }
            }

            if (subdir.Count == 0)
            {
                OnSearchCompleted(directory, found, subdir, failedDir, dircount);
                return found;
            }
            else
            {
                var next = subdir.Dequeue();
                return DoSearch(next, filters, found, subdir, failedDir, ++dircount);
            }
        }



        public void Search(string path, string[] filters)
        {
            uidisp = Application.Current.Dispatcher;
            this.startingDir = path;
            this.filters = filters;
            Task.Factory.StartNew(() =>
            {
                DoSearch(
                    startingDir,
                    this.filters,
                    new List<string>(),
                    new Queue<string>(),
                    new List<string>(),
                    1);
            });
        }
    }
}
