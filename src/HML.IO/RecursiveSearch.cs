// Decompiled with JetBrains decompiler
// Type: HML.IO.Search.RecursiveSearch
// Assembly: HML.IO, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1C3A47EF-9E7B-42FA-BC01-DB283160390B
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\HML.IO.dll

using HML.Threading;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace HML.IO.Search
{
    public sealed class RecursiveSearch : IDisposable
    {
        private List<string> files = new List<string>();
        private List<string> notSearched = new List<string>();
        private List<string> rcntFiles = new List<string>();
        private string rcntFDir;
        private Form uiform;
        private string stpath;
        private string[] patterns;
        private int dirSearched;
        private bool useThread;
        private bool cancel;
        private SimpleThreadRunner str;

        public event SearchProgressDelegate SearchInProgess;

        public event SearchFinishedDelegate SearchFinished;

        public RecursiveSearch(string StartingPath)
        {
            this.uiform = (Form)null;
            this.patterns = new string[1];
            this.patterns[0] = "*.*";
            this.stpath = StartingPath;
            this.str = new SimpleThreadRunner(new ThreadStart(this.doSearchInThread), (Form)null);
            this.str.TaskDone += new SimpleThreadRunner.TaskDoneEventHandler(this.str_TaskDone);
        }

        public RecursiveSearch(string StartingPath, string Patterns)
        {
            this.uiform = (Form)null;
            this.patterns = Patterns.Split('|');
            this.stpath = StartingPath;
            this.str = new SimpleThreadRunner(new ThreadStart(this.doSearchInThread), (Form)null);
            this.str.TaskDone += new SimpleThreadRunner.TaskDoneEventHandler(this.str_TaskDone);
        }

        public RecursiveSearch(string StartingPath, Form UIForm)
        {
            this.uiform = UIForm;
            this.patterns = new string[1];
            this.patterns[0] = "*.*";
            this.stpath = StartingPath;
            this.str = new SimpleThreadRunner(new ThreadStart(this.doSearchInThread), (Form)null);
            this.str.TaskDone += new SimpleThreadRunner.TaskDoneEventHandler(this.str_TaskDone);
        }

        public RecursiveSearch(string StartingPath, Form UIForm, string Patterns)
        {
            this.uiform = UIForm;
            this.patterns = Patterns.Split('|');
            this.stpath = StartingPath;
            this.str = new SimpleThreadRunner(new ThreadStart(this.doSearchInThread), (Form)null);
            this.str.TaskDone += new SimpleThreadRunner.TaskDoneEventHandler(this.str_TaskDone);
        }

        ~RecursiveSearch()
        {
            this.str = (SimpleThreadRunner)null;
            this.files = (List<string>)null;
            this.notSearched = (List<string>)null;
            this.patterns = (string[])null;
            this.stpath = (string)null;
        }

        private void str_TaskDone(object sender, EventArgs e)
        {
            if (this.uiform != null)
            {
                this.uiform.BeginInvoke((Delegate)new RecursiveSearch.RaiseEventUsingDelegate(this.RaiseSearchFinishedEvent));
            }
            else
            {
                if (this.SearchFinished == null)
                    return;
                this.RaiseSearchFinishedEvent();
            }
        }

        private void doSearchInThread()
        {
            this.doSearch(this.stpath);
        }

        private void doSearch(string path)
        {
            try
            {
                ++this.dirSearched;
                this.rcntFiles.Clear();
                this.rcntFDir = (string)null;
                foreach (string pattern in this.patterns)
                {
                    string[] files = Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly);
                    this.files.AddRange((IEnumerable<string>)Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));
                    this.rcntFiles.AddRange((IEnumerable<string>)files);
                }
                if (this.uiform != null && this.useThread)
                    this.uiform.BeginInvoke((Delegate)new RecursiveSearch.SearchInProgressEventDelegate(this.RaiseSearchInProgressEvent), (object)path, (object)this.files.Count, (object)this.rcntFiles.ToArray(), (object)this.rcntFDir);
                else
                    this.RaiseSearchInProgressEvent(path, this.files.Count, this.rcntFiles.ToArray(), this.rcntFDir);
                foreach (string directory in Directory.GetDirectories(path))
                    this.doSearch(directory);
            }
            catch (UnauthorizedAccessException ex)
            {
                this.notSearched.Add(path);
                this.rcntFDir = path;
            }
        }

        private void RaiseSearchFinishedEvent()
        {
            if (this.SearchFinished == null)
                return;
            this.SearchFinished((object)this, new EventArgs());
        }

        private void RaiseSearchInProgressEvent(string path, int filecnt, string[] cuf, string cuff)
        {
            if (this.SearchInProgess == null)
                return;
            this.SearchInProgess(new ProgressEventArgs(path, filecnt, cuf, cuff));
        }

        public void Search()
        {
            if (this.str.ThreadExecutionState != RunnerState.Waiting)
            {
                this.str = new SimpleThreadRunner(new ThreadStart(this.doSearchInThread), this.uiform);
                this.str.TaskDone += new SimpleThreadRunner.TaskDoneEventHandler(this.str_TaskDone);
            }
            this.str.Start();
        }

        public void Stop()
        {
            this.str.Stop();
        }

        public void Clear()
        {
            this.files.Clear();
            this.notSearched.Clear();
            this.dirSearched = 0;
        }

        public bool WasAborted
        {
            get
            {
                return this.str.ThreadExecutionState == RunnerState.Stopped;
            }
        }

        public string StartingFolder
        {
            get
            {
                return this.stpath;
            }
        }

        public int SearchedFolderCount
        {
            get
            {
                return this.dirSearched - this.notSearched.Count;
            }
        }

        public int FailedFolderCount
        {
            get
            {
                return this.notSearched.Count;
            }
        }

        public string[] FilesFound
        {
            get
            {
                return this.files.ToArray();
            }
        }

        public string[] IgnoredDirectories
        {
            get
            {
                return this.notSearched.ToArray();
            }
        }

        public void Dispose()
        {
            this.str = (SimpleThreadRunner)null;
            this.files = (List<string>)null;
            this.notSearched = (List<string>)null;
            this.patterns = (string[])null;
            this.stpath = (string)null;
            GC.Collect();
        }

        private delegate void RaiseEventUsingDelegate();

        private delegate void SearchInProgressEventDelegate(
          string pa,
          int fc,
          string[] cuf,
          string curff);
    }
}
