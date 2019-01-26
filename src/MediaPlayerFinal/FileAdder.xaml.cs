// Decompiled with JetBrains decompiler
// Type: MediaPlayerFinal.FileAdder
// Assembly: MediaPlayerFinal, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: FA266A67-626E-46F0-81D3-A2E3F3CE1F98
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\MediaPlayerFinal.exe

using HML.IO.Search;
using MediaPlayerFinal.Properties;
using RadioDesk.Data.Extra;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;

namespace MediaPlayerFinal
{
    public partial class FileAdder : Window, IComponentConnector
    {
        private const string exts = "*.mp3|*.ogg|*.flac|*.ac3|*.aac|*.wav|*.pcm";
        private bool cancel;
        private bool started;
        private double fails;
        private double success;
        private RecursiveSearch rs;
        private Thread trd;
        //internal System.Windows.Controls.GroupBox groupBox1;
        //internal System.Windows.Controls.TextBox txtPath;
        //internal System.Windows.Controls.Button btnBrowseF1;
        //internal System.Windows.Controls.Label label2;
        //internal System.Windows.Controls.GroupBox groupBox2;
        //internal System.Windows.Controls.ProgressBar prgProgress;
        //internal System.Windows.Controls.Label lblStatus;
        //internal System.Windows.Controls.Label label3;
        //internal System.Windows.Controls.Label label1;
        //internal System.Windows.Controls.Button btnStart;
        //internal System.Windows.Controls.Label lblSubtask;
        //private bool _contentLoaded;

        public FileAdder()
        {
            this.InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.trd = new Thread(new ThreadStart(this.AddFiles));
        }

        private void btnBrowseF1_Click(object sender, RoutedEventArgs e)
        {
            this.lblStatus.Content = (object)"Ready";
            this.lblSubtask.Content = (object)"";
            this.started = false;
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = false;
            if (Settings.Default.LastSearchDir == null)
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            else
                folderBrowserDialog.SelectedPath = Settings.Default.LastSearchDir;
            folderBrowserDialog.Description = "Select Folder Containing Audio Files";
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                this.txtPath.Text = folderBrowserDialog.SelectedPath;
            folderBrowserDialog.Dispose();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            this.lblStatus.Content = (object)"Ready";
            this.lblSubtask.Content = (object)"";
            if (this.started)
            {
                if (System.Windows.MessageBox.Show("Do you realy want to stop the process?", "Stop process", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
                    return;
                this.started = false;
                this.cancel = true;
                this.btnStart.Content = (object)"Start";
                this.rs.Stop();
            }
            else if (this.txtPath.Text != "")
            {
                this.rs = new RecursiveSearch(this.txtPath.Text, "*.mp3|*.ogg|*.flac|*.ac3|*.aac|*.wav|*.pcm");
                this.rs.SearchFinished += new SearchFinishedDelegate(this.rs_SearchFinished);
                this.rs.SearchInProgess += new SearchProgressDelegate(this.rs_SearchInProgess);
                this.rs.Search();
                Settings.Default.LastSearchDir = this.txtPath.Text;
                Settings.Default.Save();
                this.prgProgress.IsIndeterminate = true;
                this.lblStatus.Content = (object)"Searching For Media. Please Wait.....";
                this.cancel = false;
                this.btnStart.Content = (object)"Stop";
                this.started = true;
            }
            else
            {
                int num = (int)System.Windows.MessageBox.Show("Please select a folder.", "Empty Folder1", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void rs_SearchInProgess(ProgressEventArgs pea)
        {
            if (pea.NumberOfFilesFound != 0)
                this.SetLableText(this.lblStatus, "Searching For Media. Please Wait..... \t\t (" + pea.NumberOfFilesFound.ToString() + " files found)");
            this.SetLableText(this.lblSubtask, pea.CurrentDirectory);
        }

        private void rs_SearchFinished(object sender, EventArgs e)
        {
            if (this.rs.WasAborted)
            {
                this.SetButtonContent(this.btnStart, "Start");
                this.SetLableText(this.lblSubtask, "");
                this.SetProgressbarStyle(ProgressBarStyle.Blocks);
                this.SetLableText(this.lblStatus, "Ready");
                this.SetLableText(this.lblSubtask, "");
                this.started = false;
            }
            else
            {
                this.SetLableText(this.lblStatus, this.rs.SearchedFolderCount.ToString() + " Folders Searched And Found " + (object)this.rs.FilesFound.Length + " Files");
                this.SetLableText(this.lblSubtask, "Ready to Add Them To The Library");
                this.SetProgressbarStyle(ProgressBarStyle.Blocks);
                this.SetProgressBarBounds((double)this.rs.FilesFound.Length, 0.0);
                if (System.Windows.MessageBox.Show("Do you want to add these files to the library?", "Found Files", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    try
                    {
                        this.trd.Start();
                    }
                    catch (ThreadStateException ex)
                    {
                        this.trd = new Thread(new ThreadStart(this.AddFiles));
                        this.trd.Start();
                    }
                }
                else
                {
                    this.SetButtonContent(this.btnStart, "Start");
                    this.SetLableText(this.lblSubtask, "");
                    this.SetProgressbarStyle(ProgressBarStyle.Blocks);
                    this.SetLableText(this.lblStatus, "Ready");
                    this.SetLableText(this.lblSubtask, "");
                    this.started = false;
                }
            }
        }

        private void AddFiles()
        {
            string[] filesFound = this.rs.FilesFound;
            long longLength = filesFound.LongLength;
            this.SetProgressBarBounds((double)filesFound.Length, 0.0);
            this.SetProgressbarValue(0.0);
            string txt1 = this.success.ToString() + " file(s) out of " + (object)longLength + " files were added to the Library, None failed.";
            this.SetLableText(this.lblStatus, "Adding Files to Library. Please Wait.....");
            this.SetLableText(this.lblSubtask, txt1);
            this.fails = 0.0;
            this.success = 0.0;
            foreach (string path in filesFound)
            {
                try
                {
                    if (this.cancel)
                    {
                        this.started = false;
                        this.SetButtonContent(this.btnStart, "Start");
                        this.SetProgressbarValue(0.0);
                        this.SetLableText(this.lblSubtask, "");
                        this.trd.Abort();
                    }
                    AnalyticalFileAdder.AddSong(path);
                    ++this.success;
                }
                catch (FileAdderException ex)
                {
                    ++this.fails;
                    continue;
                }
                string txt2;
                if (this.fails != 0.0)
                    txt2 = this.success.ToString() + " file(s) out of " + (object)longLength + " files were added to the Library, " + (object)this.fails + " failed.";
                else
                    txt2 = this.success.ToString() + " file(s) out of " + (object)longLength + " files were added to the Library, None failed.";
                this.SetLableText(this.lblSubtask, txt2);
                this.SetProgressbarValue(this.success + this.fails);
            }
            this.SetProgressbarValue(0.0);
            this.SetLableText(this.lblStatus, "Done");
            this.started = false;
            this.SetButtonContent(this.btnStart, "Start");
        }

        private void SetLableText(System.Windows.Controls.Label lbl, string txt)
        {
            if (lbl.Parent.Dispatcher.CheckAccess())
                lbl.Content = (object)txt;
            else
                this.Dispatcher.Invoke((Delegate)new FileAdder.delSetLablelContent(this.SetLableText), (object)lbl, (object)txt);
        }

        private void SetButtonContent(System.Windows.Controls.Button btn, string txt)
        {
            if (btn.Parent.Dispatcher.CheckAccess())
                btn.Content = (object)txt;
            else
                this.Dispatcher.Invoke((Delegate)new FileAdder.delSetButtonContent(this.SetButtonContent), (object)btn, (object)txt);
        }

        private void SetProgressbarStyle(ProgressBarStyle styl)
        {
            if (this.prgProgress.Parent.Dispatcher.CheckAccess())
            {
                if (styl == ProgressBarStyle.Marquee)
                    this.prgProgress.IsIndeterminate = true;
                else
                    this.prgProgress.IsIndeterminate = false;
            }
            else
                this.Dispatcher.Invoke((Delegate)new FileAdder.delSetProgressbarStyle(this.SetProgressbarStyle), (object)styl);
        }

        private void SetProgressBarBounds(double max, double min)
        {
            if (this.prgProgress.Parent.Dispatcher.CheckAccess())
            {
                this.prgProgress.Maximum = max;
                this.prgProgress.Minimum = min;
            }
            else
                this.Dispatcher.Invoke((Delegate)new FileAdder.delSetProgressBarBounds(this.SetProgressBarBounds), (object)max, (object)min);
        }

        private void SetProgressbarValue(double val)
        {
            if (this.Dispatcher.CheckAccess())
                this.prgProgress.Value = val;
            else
                this.Dispatcher.Invoke((Delegate)new FileAdder.delSetProgressbarValue(this.SetProgressbarValue), (object)val);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (this.started && System.Windows.MessageBox.Show("Closing this windows will stop current procresses. Do you want to continue?", "Quit", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
                e.Cancel = true;
            if (this.trd != null && this.trd.ThreadState == System.Threading.ThreadState.Running)
                this.trd.Abort();
            if (this.rs == null)
                return;
            this.rs.Stop();
        }

        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    System.Windows.Application.LoadComponent((object)this, new Uri("/MediaPlayerFinal;component/fileadder.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            ((FrameworkElement)target).Loaded += new RoutedEventHandler(this.Window_Loaded);
        //            ((Window)target).Closing += new CancelEventHandler(this.Window_Closing);
        //            break;
        //        case 2:
        //            this.groupBox1 = (System.Windows.Controls.GroupBox)target;
        //            break;
        //        case 3:
        //            this.txtPath = (System.Windows.Controls.TextBox)target;
        //            break;
        //        case 4:
        //            this.btnBrowseF1 = (System.Windows.Controls.Button)target;
        //            this.btnBrowseF1.Click += new RoutedEventHandler(this.btnBrowseF1_Click);
        //            break;
        //        case 5:
        //            this.label2 = (System.Windows.Controls.Label)target;
        //            break;
        //        case 6:
        //            this.groupBox2 = (System.Windows.Controls.GroupBox)target;
        //            break;
        //        case 7:
        //            this.prgProgress = (System.Windows.Controls.ProgressBar)target;
        //            break;
        //        case 8:
        //            this.lblStatus = (System.Windows.Controls.Label)target;
        //            break;
        //        case 9:
        //            this.label3 = (System.Windows.Controls.Label)target;
        //            break;
        //        case 10:
        //            this.label1 = (System.Windows.Controls.Label)target;
        //            break;
        //        case 11:
        //            this.btnStart = (System.Windows.Controls.Button)target;
        //            this.btnStart.Click += new RoutedEventHandler(this.btnStart_Click);
        //            break;
        //        case 12:
        //            this.lblSubtask = (System.Windows.Controls.Label)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}

        private delegate void delSetLablelContent(System.Windows.Controls.Label lbl, string txt);

        private delegate void delSetProgressbarStyle(ProgressBarStyle styl);

        private delegate void delSetProgressBarBounds(double max, double min);

        private delegate void delSetProgressbarValue(double val);

        private delegate void delSetButtonContent(System.Windows.Controls.Button btn, string txt);
    }
}
