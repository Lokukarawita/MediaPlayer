// Decompiled with JetBrains decompiler
// Type: MediaPlayerFinal.MainWindow
// Assembly: MediaPlayerFinal, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: FA266A67-626E-46F0-81D3-A2E3F3CE1F98
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\MediaPlayerFinal.exe

using KA.Audio;
using MediaPlayerFinal.Properties;
using Microsoft.Win32;
using RadioDesk.Core;
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Forms.Integration;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using System.Xml;

namespace MediaPlayerFinal
{
    public partial class MainWindow : Window, IComponentConnector
    {
        private int forInfoLoop = 1;
        private int currentMasterVolume = 100;
        private OpenFileDialog dlgOpen = new OpenFileDialog();
        private SaveFileDialog dlgSave = new SaveFileDialog();
        private CurrentFileInfo cfinf = new CurrentFileInfo();
        private RadioDesk.Core.TagInfo tg = new RadioDesk.Core.TagInfo();
        private DataSet dsClips;
        private DataSet dsAds;
        private DataSet dsSongs;
        private DataSet dsTracks;
        private RadioDeskPlayer plyr;
        private RadioDeskEqualizer eq;
        private RadioDeskMicControl mic;
        private RadioDeskClipPlayer clipP;
        private RadioDeskPlaylist plst;
        private DispatcherTimer tmr1;
        private DispatcherTimer tmr2;
        private DispatcherTimer tmr4;
        //internal Grid grdLayoutRoot;
        internal StackPanel splSearch;
        internal TreeView tvwLibrary;
        internal TreeViewItem treechildTitle;
        internal StackPanel stpchildTitle;
        internal TreeViewItem treechildArtist;
        internal StackPanel childArtist;
        internal TreeViewItem treechildAlbum;
        internal StackPanel stpchildAlbum;
        internal TreeViewItem treechildYear;
        internal StackPanel childYear;
        internal TabControl tbcFeatures2;
        internal TabItem tbiMixer;
        internal Grid griMixer;
        internal Slider sliMasterVolume;
        internal Slider sliPlayerVolume;
        internal Slider sliMicVolume;
        internal System.Windows.Shapes.Rectangle rectangle1;
        internal Label label4;
        internal Label label5;
        internal CheckBox chkMuteMaster;
        internal System.Windows.Shapes.Rectangle rectangle2;
        internal Label label6;
        internal Label label7;
        internal CheckBox chkMutePlayer;
        internal System.Windows.Shapes.Rectangle rectangle3;
        internal Label label8;
        internal Label label9;
        internal CheckBox chkMuteMic;
        internal TabItem tbiEqualizer;
        internal Grid grdEquilizer;
        internal Slider sliEqPreAmp;
        internal Label lblEqPreamp;
        internal Slider sliEq1;
        internal Label lblEq1;
        internal Slider sliEq2;
        internal Label lblEq2;
        internal Slider sliEq3;
        internal Label lblEq3;
        internal Slider sliEq4;
        internal Label lblEq4;
        internal Slider sliEq5;
        internal Label lblEq6;
        internal Slider sliEq6;
        internal Label label3;
        internal Label lblEq7;
        internal Slider sliEq7;
        internal Label lblEq8;
        internal Slider sliEq8;
        internal Label lblEq9;
        internal Slider sliEq9;
        internal Label lblEq10;
        internal Slider sliEq10;
        internal StackPanel splButtons;
        internal CheckBox chkEnableEQ;
        internal Button btnEQSave;
        internal Button btnEQLoad;
        internal ComboBox cmbEQPreset;
        internal Label label15;
        internal TabItem tbiEffects;
        internal Grid gridEffects;
        internal Label lblStereoEffects;
        internal Label lblTempo;
        internal Label lblPitch;
        internal Label lblRate;
        internal StackPanel spStereoEffects;
        internal StackPanel spTempo;
        internal ComboBox cmbTempo;
        internal StackPanel spPitch;
        internal ComboBox cmbPitch;
        internal StackPanel spRate;
        internal ComboBox cmbRate;
        internal ComboBox cmbStereoEffects;
        internal TabItem tbiMic;
        internal Slider sliEchoLevel;
        internal Label lblEchoLevel;
        internal Slider sliEchoVolume;
        internal Label lblEchoVolume;
        internal CheckBox chkMicEffectsEnable;
        internal TabControl tbcFeatures1;
        internal TabItem tbiPlaylist;
       // internal ListBox lstPlaylist;
        internal MenuItem mnuPlaylistPlay;
        internal MenuItem mnuPlaylistInfo;
        internal MenuItem mnuPlaylistRemove;
        internal MenuItem mnuPlaylistRemoveAll;
        internal MenuItem mnuPlaylistMode;
        internal MenuItem mnuPlaylistNormal;
        internal MenuItem mnuPlaylistRepeat;
        internal MenuItem mnuPlaylistRepeatAll;
        internal MenuItem mnuPlaylistShuffled;
        internal MenuItem mnuPlaylistSave;
        internal MenuItem mnuPlaylistLoad;
        internal TabItem tbiClipPlayer;
        internal Slider sliClipsAdsVol;
        internal ComboBox cmbClips;
        internal ComboBox cmbAds;
        internal StackPanel sp;
        internal System.Windows.Controls.Image image2;
        internal System.Windows.Shapes.Rectangle rectangle9;
        internal Label label13;
        internal Label lblSelectedClipAd;
        internal Button btnClipPlay;
        internal Button btnClipStop;
        internal StackPanel splPlayerButtons;
        internal StackPanel sptext;
        internal StackPanel spButtons;
        internal Button btnprevious;
        internal Button btnRewind;
        internal Button btnStop;
        internal Button btnPlay;
        internal Button btnpause;
        internal Button btnForward;
        internal Button btnNext;
        internal StackPanel stackPanel1;
        internal TextBox txtSearch;
        internal System.Windows.Controls.Image image1;
        internal StackPanel stackPanel2;
        internal Button btnshuf;
        internal System.Windows.Shapes.Rectangle rectangle4;
        internal Label label10;
        internal Label label11;
        internal Label label12;
        internal System.Windows.Shapes.Rectangle rectangle5;
        internal WindowsFormsHost windowsFormsHost1;
        internal VuMeter VUML;
        internal WindowsFormsHost windowsFormsHost2;
        internal VuMeter VUMR;
        internal System.Windows.Shapes.Rectangle rectangle6;
        internal WindowsFormsHost windowsFormsHost3;
        internal VuMeter VUMM;
        internal ListView listView1;
        internal ContextMenu mnuResultGrid;
        internal MenuItem mnuListViewAddToPlaylist;
        internal MenuItem mnuListViewInfo;
        internal MenuItem mnuListViewSearchMedia;
        internal GridView gvSearchResult;
        internal System.Windows.Shapes.Rectangle rectangle8;
        internal Label lblInformationBar;
        internal Label lblCurrentTime;
        internal Label lblTotalTime;
        internal Label label1;
        internal Label lblShuffleInfo;
        internal System.Windows.Shapes.Rectangle rectangle7;
        internal Slider sliSeekBar;
        internal System.Windows.Controls.Image imgAlbumArt;
        private bool _contentLoaded;

        public MainWindow()
        {
            this.InitializeComponent();
            this.plyr = new RadioDeskPlayer();
            this.eq = new RadioDeskEqualizer(this.plyr);
            this.eq.SettingsLoaded += new EQSettingsLoaded(this.eq_SettingsLoaded);
            this.clipP = new RadioDeskClipPlayer();
            this.mic = new RadioDeskMicControl();
            this.plst = new RadioDeskPlaylist();
            this.tmr1 = new DispatcherTimer();
            this.tmr2 = new DispatcherTimer();
            this.tmr4 = new DispatcherTimer();
            this.tmr1.Interval = new TimeSpan(0, 0, 0, 0, 20);
            this.tmr2.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            this.tmr4.Interval = new TimeSpan(0, 0, 0, 0, 5000);
            this.tmr1.Tick += new EventHandler(this.tmr1_Tick);
            this.tmr2.Tick += new EventHandler(this.tmr2_Tick);
            this.tmr4.Tick += new EventHandler(this.tmr4_Tick);
            this.tmr1.Start();
            this.dsAds = new DataSet();
            this.dsClips = new DataSet();
            this.dsSongs = new DataSet();
            this.dsTracks = new DataSet();
            this.dsTracks.ReadXmlSchema(Assembly.GetExecutingAssembly().GetManifestResourceStream("MediaPlayerFinal.TracksDBSch.xsd"));
            this.lstPlaylist.ItemsSource = (IEnumerable)this.dsTracks.Tables[0].DefaultView;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ResizeMode = ResizeMode.CanMinimize;
            this.treechildArtist.IsSelected = true;
            this.dsAds = ExtendedQuerying.GetAllAds();
            this.dsClips = ExtendedQuerying.GetAllClips();
            this.cmbClips.ItemsSource = (IEnumerable)this.dsClips.Tables[0].DefaultView;
            this.cmbAds.ItemsSource = (IEnumerable)this.dsAds.Tables[0].DefaultView;
            this.chkEnableEQ.IsChecked = new bool?(true);
            this.cfinf.userStop = false;
            this.cfinf.shuffled = Settings.Default.Shuffled;
            if (this.cfinf.shuffled)
                this.mnuPlaylistShuffled.IsChecked = true;
            this.cfinf.repeat = Settings.Default.Repeat;
            switch (this.cfinf.repeat)
            {
                case 0:
                    this.mnuPlaylistRepeatAll.IsChecked = false;
                    this.mnuPlaylistRepeat.IsChecked = false;
                    this.mnuPlaylistNormal.IsChecked = true;
                    break;
                case 1:
                    this.mnuPlaylistRepeatAll.IsChecked = false;
                    this.mnuPlaylistRepeat.IsChecked = true;
                    this.mnuPlaylistNormal.IsChecked = false;
                    break;
                case 2:
                    this.mnuPlaylistRepeatAll.IsChecked = true;
                    this.mnuPlaylistRepeat.IsChecked = false;
                    this.mnuPlaylistNormal.IsChecked = false;
                    break;
            }
            if (Settings.Default.LastEQType == "Custom")
            {
                if (File.Exists(Settings.Default.LastEQPath))
                {
                    this.eq.Load(Settings.Default.LastEQPath);
                    this.SetEQPresetCombobox((EQPreset)1234);
                }
                else
                {
                    this.SetEQPresetCombobox(EQPreset.Default);
                    Settings.Default.LastEQType = "Preset";
                    Settings.Default.LastEQPreset = EQPreset.Default;
                    Settings.Default.Save();
                }
            }
            else
            {
                this.eq.SetEQByUsing(Settings.Default.LastEQPreset, true);
                this.SetEQPresetCombobox(Settings.Default.LastEQPreset);
            }
        }

        private void tmr4_Tick(object sender, EventArgs e)
        {
            switch (this.forInfoLoop)
            {
                case 1:
                    this.lblInformationBar.Content = (object)this.cfinf.Title;
                    this.forInfoLoop = 2;
                    break;
                case 2:
                    this.lblInformationBar.Content = (object)this.cfinf.Artist;
                    this.forInfoLoop = 3;
                    break;
                case 3:
                    this.lblInformationBar.Content = (object)this.cfinf.Album;
                    this.forInfoLoop = 4;
                    break;
                case 4:
                    this.lblInformationBar.Content = (object)this.cfinf.Year;
                    this.forInfoLoop = 1;
                    break;
            }
        }

        private void tmr3_Tick(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            this.ChangeTitle("RadioDeskPlayer      -      " + now.ToString("dddd, MMMM dd, yyyy") + "      -      " + now.ToString("hh:mm:ss tt"));
        }

        private void tmr2_Tick(object sender, EventArgs e)
        {
            if (!this.cfinf.userStop && this.plyr.IsStopped)
                this.CheckAndAdvance(false);
            this.lblTotalTime.Content = (object)RadioDeskUtils.FormatToTime(this.plyr.Duration);
            this.lblCurrentTime.Content = (object)RadioDeskUtils.FormatToTime(this.plyr.CurrentPosoition);
            this.sliSeekBar.Value = (double)this.plyr.CurrentPosoition;
        }

        private void tmr1_Tick(object sender, EventArgs e)
        {
            if (this.plyr != null)
            {
                this.VUML.Level = this.plyr.LeftVUMeterValue;
                this.VUMR.Level = this.plyr.RightVUMeterValue;
            }
            if (this.mic == null || !this.mic.IsEnabled)
                return;
            this.VUMM.Level = this.mic.LeftVUMeterValue >= this.mic.RightVUMeterValue ? this.mic.LeftVUMeterValue : this.mic.RightVUMeterValue;
        }

        public void CheckAndAdvance(bool islistView)
        {
            if (this.lstPlaylist.Dispatcher.CheckAccess())
            {
                Console.WriteLine("Repeat mode : " + this.cfinf.repeat.ToString());
                if (this.cfinf.repeat == 1)
                    islistView = true;
                if (!islistView && this.cfinf.shuffled)
                {
                    int index = new Random().Next(0, this.lstPlaylist.Items.Count);
                    this.cfinf.idx = index;
                    this.cfinf.Artist = ((DataRowView)this.lstPlaylist.Items[index])["artist"].ToString();
                    this.cfinf.Album = ((DataRowView)this.lstPlaylist.Items[index])["album"].ToString();
                    this.cfinf.Path = ((DataRowView)this.lstPlaylist.Items[index])["location"].ToString();
                    this.cfinf.Title = ((DataRowView)this.lstPlaylist.Items[index])["song_name"].ToString();
                    this.cfinf.Year = ((DataRowView)this.lstPlaylist.Items[index])["song_year"].ToString();
                    this.lblInformationBar.Content = (object)this.cfinf.Title;
                    this.plyr.Play(this.cfinf.Path);
                    this.lstPlaylist.SelectedIndex = index;
                    this.cfinf.userStop = false;
                    Console.WriteLine("@Playing : {0}", (object)this.cfinf.Path);
                    this.tg.File = this.cfinf.Path;
                    this.sliSeekBar.Maximum = (double)this.plyr.Duration;
                    this.imgAlbumArt.Source = this.tg.FrontCover == null ? (ImageSource)new BitmapImage(new Uri("/MediaPlayerFinal;component/Images/music.png", UriKind.Relative)) : (ImageSource)this.ConvertToWPFImageSource(this.tg.FrontCover);
                }
                else if (this.cfinf.repeat == 0 && this.cfinf.idx + 1 == this.lstPlaylist.Items.Count && !islistView)
                {
                    Console.WriteLine("@ inside repeat 0");
                    this.sliSeekBar.Value = 0.0;
                    this.VUML.Level = 0;
                    this.VUMR.Level = 0;
                    Console.WriteLine("@ finished repeat 0");
                }
                else if (islistView)
                {
                    Console.WriteLine("@Play Current file");
                    this.cfinf.Artist = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["artist"].ToString();
                    this.cfinf.Album = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["album"].ToString();
                    this.cfinf.Path = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["location"].ToString();
                    this.cfinf.Title = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["song_name"].ToString();
                    this.cfinf.Year = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["song_year"].ToString();
                    this.lblInformationBar.Content = (object)this.cfinf.Title;
                    this.plyr.Play(this.cfinf.Path);
                    this.cfinf.userStop = false;
                    Console.WriteLine("@Playing : {0}", (object)this.cfinf.Path);
                    this.tg.File = this.cfinf.Path;
                    this.sliSeekBar.Maximum = (double)this.plyr.Duration;
                    this.imgAlbumArt.Source = this.tg.FrontCover == null ? (ImageSource)new BitmapImage(new Uri("/MediaPlayerFinal;component/Images/music.png", UriKind.Relative)) : (ImageSource)this.ConvertToWPFImageSource(this.tg.FrontCover);
                }
                else if (this.cfinf.idx + 1 != this.lstPlaylist.Items.Count)
                {
                    Console.WriteLine("@Play next file");
                    ++this.cfinf.idx;
                    this.cfinf.Artist = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["artist"].ToString();
                    this.cfinf.Album = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["album"].ToString();
                    this.cfinf.Path = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["location"].ToString();
                    this.cfinf.Title = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["song_name"].ToString();
                    this.cfinf.Year = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["song_year"].ToString();
                    this.lstPlaylist.SelectedIndex = this.cfinf.idx;
                    this.lblInformationBar.Content = (object)this.cfinf.Title;
                    this.cfinf.userStop = false;
                    this.plyr.Play(this.cfinf.Path);
                    this.tg.File = this.cfinf.Path;
                    this.sliSeekBar.Maximum = (double)this.plyr.Duration;
                    this.imgAlbumArt.Source = this.tg.FrontCover == null ? (ImageSource)new BitmapImage(new Uri("/MediaPlayerFinal;component/Images/music.png", UriKind.Relative)) : (ImageSource)this.ConvertToWPFImageSource(this.tg.FrontCover);
                }
                else
                {
                    Console.WriteLine("@Play first file");
                    this.cfinf.idx = 0;
                    this.cfinf.Artist = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["artist"].ToString();
                    this.cfinf.Album = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["album"].ToString();
                    this.cfinf.Path = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["location"].ToString();
                    this.cfinf.Title = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["song_name"].ToString();
                    this.cfinf.Year = ((DataRowView)this.lstPlaylist.Items[this.cfinf.idx])["song_year"].ToString();
                    this.lstPlaylist.SelectedIndex = this.cfinf.idx;
                    this.lblInformationBar.Content = (object)this.cfinf.Title;
                    if (this.plyr != null)
                        this.plyr.Stop();
                    this.cfinf.userStop = false;
                    this.plyr.Play(this.cfinf.Path);
                    this.tg.File = this.cfinf.Path;
                    this.sliSeekBar.Maximum = (double)this.plyr.Duration;
                    this.imgAlbumArt.Source = this.tg.FrontCover == null ? (ImageSource)new BitmapImage(new Uri("/MediaPlayerFinal;component/Images/music.png", UriKind.Relative)) : (ImageSource)this.ConvertToWPFImageSource(this.tg.FrontCover);
                }
            }
            else
                this.Dispatcher.BeginInvoke((Delegate)new MainWindow.delPlaylistAdvance(this.CheckAndAdvance), DispatcherPriority.Send, (object)islistView);
        }

        private void treechildYear_Selected(object sender, RoutedEventArgs e)
        {
            if (this.dsSongs != null)
                this.dsSongs.Reset();
            this.dsSongs = this.txtSearch.Text == "Search" || this.txtSearch.Text == "" ? ExtendedQuerying.SearchByYear("*") : ExtendedQuerying.SearchByYear(this.txtSearch.Text);
            this.listView1.ItemsSource = (IEnumerable)this.dsSongs.Tables[0].DefaultView;
            if (this.listView1.Items.Count == 0)
                return;
            this.listView1.ScrollIntoView(this.listView1.Items[0]);
        }

        private void treechildTitle_Selected(object sender, RoutedEventArgs e)
        {
            if (this.dsSongs != null)
                this.dsSongs.Reset();
            this.dsSongs = this.txtSearch.Text == "Search" || this.txtSearch.Text == "" ? ExtendedQuerying.SearchByTitle("*") : ExtendedQuerying.SearchByTitle(this.txtSearch.Text);
            this.listView1.ItemsSource = (IEnumerable)this.dsSongs.Tables[0].DefaultView;
            if (this.listView1.Items.Count == 0)
                return;
            this.listView1.ScrollIntoView(this.listView1.Items[0]);
        }

        private void treechildArtist_Selected(object sender, RoutedEventArgs e)
        {
            if (this.dsSongs != null)
                this.dsSongs.Reset();
            this.dsSongs = this.txtSearch.Text == "Search" || this.txtSearch.Text == "" ? ExtendedQuerying.SearchByArtist("*") : ExtendedQuerying.SearchByArtist(this.txtSearch.Text);
            this.listView1.ItemsSource = (IEnumerable)this.dsSongs.Tables[0].DefaultView;
            if (this.listView1.Items.Count == 0)
                return;
            this.listView1.ScrollIntoView(this.listView1.Items[0]);
        }

        private void treechildAlbum_Selected(object sender, RoutedEventArgs e)
        {
            if (this.dsSongs != null)
                this.dsSongs.Reset();
            this.dsSongs = this.txtSearch.Text == "Search" || this.txtSearch.Text == "" ? ExtendedQuerying.SearchByAlbum("*") : ExtendedQuerying.SearchByAlbum(this.txtSearch.Text);
            this.listView1.ItemsSource = (IEnumerable)this.dsSongs.Tables[0].DefaultView;
            if (this.listView1.Items.Count == 0)
                return;
            this.listView1.ScrollIntoView(this.listView1.Items[0]);
        }

        private void stpchildTitle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.treechildTitle.IsSelected = true;
        }

        private void childArtist_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.treechildArtist.IsSelected = true;
        }

        private void treechildAlbum_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.treechildAlbum.IsSelected = true;
        }

        private void childYear_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.treechildYear.IsSelected = true;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (this.treechildTitle.IsSelected)
                this.treechildTitle_Selected((object)this, new RoutedEventArgs());
            else if (this.treechildArtist.IsSelected)
                this.treechildArtist_Selected((object)this, new RoutedEventArgs());
            else if (this.treechildAlbum.IsSelected)
            {
                this.treechildAlbum_Selected((object)this, new RoutedEventArgs());
            }
            else
            {
                if (!this.treechildYear.IsSelected)
                    return;
                this.treechildYear_Selected((object)this, new RoutedEventArgs());
            }
        }

        private void listView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Windows.Point position = e.GetPosition((IInputElement)this);
                if (position.X <= 156.0 || position.X >= 626.0 || (position.Y <= 102.0 || position.Y >= 587.0))
                    return;
                DataRowView selectedItem = (DataRowView)((Selector)sender).SelectedItem;
                this.tmr1.Start();
                this.tmr2.Start();
                this.tmr4.Start();
                if (this.dsTracks != null)
                    this.dsTracks.Reset();
                this.dsTracks = ExtendedQuerying.GetTracks(selectedItem["artist"].ToString());
                this.lstPlaylist.ItemsSource = (IEnumerable)this.dsTracks.Tables[0].DefaultView;
                this.SelectPlaylistItem(selectedItem["song_name"].ToString());
                this.CheckAndAdvance(true);
            }
            catch (Exception ex)
            {
                this.tmr1.Stop();
                this.tmr2.Stop();
                this.tmr4.Stop();
                this.lblInformationBar.Content = (object)"Error Occured !!!!";
            }
        }

        private void SelectPlaylistItem(string sng)
        {
            for (int index = 0; index < this.lstPlaylist.Items.Count; ++index)
            {
                if (((DataRowView)this.lstPlaylist.Items[index])["song_name"].ToString().Equals(sng))
                {
                    this.lstPlaylist.SelectedIndex = index;
                    this.cfinf.idx = this.lstPlaylist.SelectedIndex;
                }
            }
        }

        private void UpdateSeekBar(uint valu)
        {
            if (this.Dispatcher.CheckAccess())
                this.sliSeekBar.Value = (double)valu;
            else
                this.Dispatcher.Invoke((Delegate)new MainWindow.delUpdateSeekBar(this.UpdateSeekBar), (object)valu);
        }

        private void UpdateLableText(Label lblnam, string valu)
        {
            if (lblnam.Dispatcher.CheckAccess())
            {
                lblnam.Content = (object)valu;
            }
            else
            {
                MainWindow.delUpdateLableText delUpdateLableText = new MainWindow.delUpdateLableText(this.UpdateLableText);
                lblnam.Dispatcher.Invoke((Delegate)delUpdateLableText, (object)lblnam, (object)valu);
            }
        }

        private void ChangeTitle(string title)
        {
            if (this.Dispatcher.CheckAccess())
                this.Title = title;
            else
                this.Dispatcher.Invoke((Delegate)new MainWindow.delChangeTitle(this.ChangeTitle), (object)title);
        }

        private void chkEnableEQ_Checked(object sender, RoutedEventArgs e)
        {
            this.eq.Enable = this.chkEnableEQ.IsChecked.Value;
        }

        private void chkEnableEQ_Unchecked(object sender, RoutedEventArgs e)
        {
            this.eq.Enable = this.chkEnableEQ.IsChecked.Value;
        }

        private void sliEqPreAmp_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.PreampGain = (int)((RangeBase)sender).Value;
        }

        private void sliEq1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band60Hz = (int)((RangeBase)sender).Value;
        }

        private void sliEq2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band170Hz = (int)((RangeBase)sender).Value;
        }

        private void sliEq3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band310Hz = (int)((RangeBase)sender).Value;
        }

        private void sliEq4_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band600Hz = (int)((RangeBase)sender).Value;
        }

        private void sliEq5_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band1KHz = (int)((RangeBase)sender).Value;
        }

        private void sliEq6_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band3KHz = (int)((RangeBase)sender).Value;
        }

        private void sliEq7_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band6KHz = (int)((RangeBase)sender).Value;
        }

        private void sliEq8_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band12KHz = (int)((RangeBase)sender).Value;
        }

        private void sliEq9_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band14KHz = (int)((RangeBase)sender).Value;
        }

        private void sliEq10_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.eq == null)
                return;
            this.eq.Band16KHz = (int)((RangeBase)sender).Value;
        }

        private void SetEQPresetCombobox(EQPreset preq)
        {
            switch (preq)
            {
                case EQPreset.Default:
                    this.cmbEQPreset.SelectedIndex = 0;
                    break;
                case EQPreset.BassTreble:
                    this.cmbEQPreset.SelectedIndex = 1;
                    break;
                default:
                    this.cmbEQPreset.SelectedIndex = 2;
                    break;
            }
        }

        private void cmbEQPreset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.plyr == null)
                return;
            string str = ((ContentControl)e.AddedItems[0]).Content.ToString();
            if (str == "Custom")
            {
                this.EnableOrDisableEqualizerControls(true);
            }
            else
            {
                EQPreset pre = (EQPreset)Enum.Parse(typeof(EQPreset), str, true);
                this.eq.SetEQByUsing(pre, true);
                Settings.Default.LastEQType = "Preset";
                Settings.Default.LastEQPreset = pre;
                Settings.Default.Save();
                this.EnableOrDisableEqualizerControls(false);
            }
        }

        private void btnEQLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.FileName = (string)null;
            openFileDialog.Filter = "Equalizer Settings (*.req)|*.req";
            openFileDialog.Title = "Select RadioDesk Equalizer Settings File";
            openFileDialog.ShowDialog();
            if (!(openFileDialog.FileName != ""))
                return;
            try
            {
                this.eq.Load(openFileDialog.FileName);
                Settings.Default.LastEQType = "Custom";
                Settings.Default.LastEQPreset = EQPreset.Default;
                Settings.Default.LastEQPath = openFileDialog.FileName;
                Settings.Default.Save();
                this.SetEQPresetCombobox((EQPreset)1234);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "Error Occured", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void eq_SettingsLoaded(object sender, EventArgs e)
        {
            this.chkEnableEQ.IsChecked = new bool?(this.eq.Enable);
            this.sliEqPreAmp.Value = (double)this.eq.PreampGain;
            this.sliEq1.Value = (double)this.eq.Band60Hz;
            this.sliEq2.Value = (double)this.eq.Band170Hz;
            this.sliEq3.Value = (double)this.eq.Band310Hz;
            this.sliEq4.Value = (double)this.eq.Band600Hz;
            this.sliEq5.Value = (double)this.eq.Band1KHz;
            this.sliEq6.Value = (double)this.eq.Band3KHz;
            this.sliEq7.Value = (double)this.eq.Band6KHz;
            this.sliEq8.Value = (double)this.eq.Band12KHz;
            this.sliEq9.Value = (double)this.eq.Band14KHz;
            this.sliEq10.Value = (double)this.eq.Band16KHz;
        }

        private void EnableOrDisableEqualizerControls(bool Enable)
        {
            this.sliEqPreAmp.IsEnabled = Enable;
            this.sliEq1.IsEnabled = Enable;
            this.sliEq2.IsEnabled = Enable;
            this.sliEq3.IsEnabled = Enable;
            this.sliEq4.IsEnabled = Enable;
            this.sliEq5.IsEnabled = Enable;
            this.sliEq6.IsEnabled = Enable;
            this.sliEq7.IsEnabled = Enable;
            this.sliEq8.IsEnabled = Enable;
            this.sliEq9.IsEnabled = Enable;
            this.sliEq10.IsEnabled = Enable;
        }

        private void btnEQSave_Click(object sender, RoutedEventArgs e)
        {
            this.dlgSave.Title = "Save RadiDesk Equalizer Settings";
            this.dlgSave.FileName = "";
            this.dlgSave.Filter = "Equalizer Settings (*.req)|*.req";
            this.dlgSave.ShowDialog();
            if (this.dlgSave.FileName == null)
                return;
            try
            {
                Settings.Default.LastEQType = "Custom";
                Settings.Default.LastEQPreset = EQPreset.Default;
                Settings.Default.LastEQPath = this.dlgSave.FileName;
                Settings.Default.Save();
                this.eq.Save(this.dlgSave.FileName);
            }
            catch (Exception ex)
            {
                int num = (int)MessageBox.Show(ex.Message, "Error Occured", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void sliMasterVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.plyr == null)
                return;
            this.plyr.MasterVolumeLeft = (int)this.sliMasterVolume.Value;
            this.plyr.MasterVolumeRight = (int)this.sliMasterVolume.Value;
        }

        private void sliPlayerVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.plyr == null)
                return;
            this.plyr.VolumeLeft = (int)this.sliPlayerVolume.Value;
            this.plyr.VolumeRight = (int)this.sliPlayerVolume.Value;
        }

        private void sliMicVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.mic == null)
                return;
            this.mic.Volume = (int)this.sliMicVolume.Value;
        }

        private void chkMutePlayer_Checked(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null)
                return;
            this.plyr.Mute = this.chkMutePlayer.IsChecked.Value;
            this.sliPlayerVolume.IsEnabled = false;
        }

        private void chkMutePlayer_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null)
                return;
            this.plyr.Mute = this.chkMutePlayer.IsChecked.Value;
            this.sliPlayerVolume.IsEnabled = true;
        }

        private void chkMuteMaster_Checked(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null)
                return;
            this.currentMasterVolume = (int)this.sliMasterVolume.Value;
            this.plyr.MasterVolumeLeft = 0;
            this.plyr.MasterVolumeRight = 0;
            this.sliMasterVolume.IsEnabled = false;
        }

        private void chkMuteMaster_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null)
                return;
            this.sliMasterVolume.IsEnabled = true;
            this.plyr.MasterVolumeLeft = this.currentMasterVolume;
            this.plyr.MasterVolumeRight = this.currentMasterVolume;
        }

        private void chkMuteMic_Checked(object sender, RoutedEventArgs e)
        {
            if (this.mic == null)
                return;
            this.mic.Disable();
            this.sliMicVolume.IsEnabled = false;
            this.VUMM.Level = 0;
        }

        private void chkMuteMic_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.mic == null)
                return;
            this.mic.Enable();
            this.sliMicVolume.IsEnabled = true;
        }

        private void cmbStereoEffects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.plyr == null)
                return;
            this.plyr.StereoEffect = (StereoEffectType)Enum.Parse(typeof(StereoEffectType), ((ContentControl)e.AddedItems[0]).Content.ToString(), true);
        }

        private void cmbTempo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.plyr == null)
                return;
            this.plyr.Tempo = (PlayEffectRate)Enum.Parse(typeof(PlayEffectRate), ((ContentControl)e.AddedItems[0]).Content.ToString());
        }

        private void cmbPitch_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.plyr == null)
                return;
            this.plyr.Pitch = (PlayEffectRate)Enum.Parse(typeof(PlayEffectRate), ((ContentControl)e.AddedItems[0]).Content.ToString());
        }

        private void cmbRate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.plyr == null)
                return;
            this.plyr.Rate = (PlayEffectRate)Enum.Parse(typeof(PlayEffectRate), ((ContentControl)e.AddedItems[0]).Content.ToString());
        }

        private void chkMicEffectsEnable_Checked(object sender, RoutedEventArgs e)
        {
            if (this.mic == null)
                return;
            this.mic.EchoEnable = true;
            this.mic.EchoLevel = (int)this.sliEchoLevel.Value;
            this.mic.EchoVolume = (int)this.sliEchoVolume.Value;
        }

        private void chkMicEffectsEnable_Unchecked(object sender, RoutedEventArgs e)
        {
            if (this.mic == null)
                return;
            this.mic.EchoEnable = false;
        }

        private void sliEchoLevel_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.mic == null)
                return;
            this.mic.EchoLevel = (int)this.sliEchoLevel.Value;
        }

        private void sliEchoVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.mic == null)
                return;
            this.mic.EchoVolume = (int)this.sliEchoVolume.Value;
        }

        private void btnForward_Click(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null || !this.plyr.IsPlaying)
                return;
            this.plyr.SeekForward(5U);
        }

        private void btnRewind_Click(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null || !this.plyr.IsPlaying)
                return;
            this.plyr.SeekBackward(5U);
        }

        private void btnpause_Click(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null)
                return;
            if (this.plyr.IsPlaying)
            {
                this.plyr.Pause();
            }
            else
            {
                if (!this.plyr.IsPaused)
                    return;
                this.cfinf.userStop = false;
                this.plyr.Resume();
                this.tmr2.Start();
                this.tmr1.Start();
                this.tmr4.Start();
            }
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null || this.lstPlaylist.Items.Count <= 0)
                return;
            if (this.plyr.IsPaused)
            {
                this.cfinf.userStop = false;
                this.plyr.Resume();
                this.StartStopTimers(true);
            }
            else if (this.cfinf.Path != null)
            {
                this.cfinf.userStop = false;
                this.CheckAndAdvance(true);
                this.StartStopTimers(true);
            }
            else
            {
                if (this.cfinf.Path != null)
                    return;
                if (this.cfinf.idx == -1)
                    this.cfinf.idx = 0;
                this.CheckAndAdvance(true);
                this.StartStopTimers(true);
                this.cfinf.userStop = false;
            }
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this.cfinf.userStop = true;
            this.tmr2.Stop();
            this.plyr.Stop();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null || this.lstPlaylist.Items.Count == 0 || this.cfinf.idx == -1)
                return;
            if (this.cfinf.shuffled)
            {
                this.CheckAndAdvance(false);
                this.StartStopTimers(true);
            }
            else
            {
                if (this.cfinf.idx + 1 >= this.lstPlaylist.Items.Count)
                    return;
                ++this.cfinf.idx;
                this.CheckAndAdvance(true);
                this.StartStopTimers(true);
                this.lstPlaylist.SelectedIndex = this.cfinf.idx;
            }
        }

        private void btnprevious_Click(object sender, RoutedEventArgs e)
        {
            if (this.plyr == null || this.lstPlaylist.Items.Count == 0 || this.cfinf.idx == -1)
                return;
            if (this.cfinf.shuffled)
            {
                this.CheckAndAdvance(false);
                this.StartStopTimers(true);
            }
            else
            {
                if (this.cfinf.idx - 1 < 0)
                    return;
                --this.cfinf.idx;
                this.CheckAndAdvance(true);
                this.StartStopTimers(true);
                this.lstPlaylist.SelectedIndex = this.cfinf.idx;
            }
        }

        private void btnClipPlay_Click(object sender, RoutedEventArgs e)
        {
            if (this.clipP == null || !this.clipP.IsLoaded || !this.clipP.IsStopped)
                return;
            this.clipP.Play();
        }

        private void btnClipStop_Click(object sender, RoutedEventArgs e)
        {
            if (this.clipP == null || !this.clipP.IsLoaded || this.clipP.IsStopped)
                return;
            this.clipP.Stop();
        }

        private void cmbClips_DropDownClosed(object sender, EventArgs e)
        {
            DataRowView selectedValue = (DataRowView)this.cmbClips.SelectedValue;
            if (selectedValue == null || selectedValue[1].ToString() == null)
                return;
            this.clipP.Load(selectedValue[1].ToString(), false, true);
            this.lblSelectedClipAd.Content = (object)("Clip - " + selectedValue["title"].ToString());
        }

        private void cmbAds_DropDownClosed(object sender, EventArgs e)
        {
            DataRowView selectedValue = (DataRowView)this.cmbAds.SelectedValue;
            if (selectedValue == null || selectedValue[1].ToString() == null)
                return;
            this.clipP.Load(selectedValue[1].ToString(), false, true);
            this.lblSelectedClipAd.Content = (object)("Ad - " + selectedValue["adv_name"].ToString());
        }

        private void sliClips_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.clipP == null)
                return;
            this.clipP.Volume = (int)this.sliClipsAdsVol.Value;
        }

        private void sliSeekBar_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.tmr2.Stop();
        }

        private void sliSeekBar_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (this.plyr == null || !this.plyr.IsPlaying)
                return;
            this.plyr.Seek((uint)this.sliSeekBar.Value);
            this.tmr2.Start();
        }

        private void mnuListViewInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView selectedItem = (DataRowView)this.listView1.SelectedItem;
                Info info = new Info(selectedItem[1].ToString(), selectedItem[2].ToString(), selectedItem[3].ToString());
                info.Owner = (Window)this;
                info.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                info.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }

        private void mnuListViewAddToPlaylist_Click(object sender, RoutedEventArgs e)
        {
            if (this.listView1.SelectedIndex == -1)
                return;
            DataRowView selectedItem = (DataRowView)this.listView1.SelectedItem;
            DataRow row = this.dsTracks.Tables[0].NewRow();
            row["DisplayView"] = (object)(selectedItem["artist"].ToString() + " - " + selectedItem["song_name"].ToString());
            row["song_id"] = selectedItem["song_id"];
            row["song_name"] = selectedItem["song_name"];
            row["artist"] = selectedItem["artist"];
            row["album"] = selectedItem["album"];
            row["length"] = selectedItem["length"];
            row["bit_rate"] = selectedItem["bit_rate"];
            row["genre"] = selectedItem["genre"];
            row["song_year"] = selectedItem["song_year"];
            row["disk"] = selectedItem["disk"];
            row["MIME_type"] = selectedItem["MIME_type"];
            row["location"] = selectedItem["location"];
            row["TimeString"] = selectedItem["TimeString"];
            row["checked"] = selectedItem["checked"];
            this.dsTracks.Tables[0].Rows.Add(row);
            if (this.lstPlaylist.SelectedIndex != -1)
                return;
            this.lstPlaylist.SelectedIndex = 0;
            this.cfinf.Path = (string)null;
        }

        private void mnuListViewSearchMedia_Click(object sender, RoutedEventArgs e)
        {
            if ((this.plyr.IsPlaying || this.plyr.IsPaused ? MessageBox.Show("Current playback need to be stopped in-order to continue.\nDo you want to stop current playback?", "Add Media To Library", MessageBoxButton.YesNo, MessageBoxImage.Question) : MessageBoxResult.Yes) != MessageBoxResult.Yes)
                return;
            this.btnStop_Click((object)this, new RoutedEventArgs());
            FileAdder fileAdder = new FileAdder();
            fileAdder.Owner = (Window)this;
            fileAdder.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            fileAdder.ShowDialog();
            this.txtSearch.Text = "";
            this.txtSearch.Text = "Search";
        }

        private void txtSearch_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (!(this.txtSearch.Text == "Search"))
                return;
            this.txtSearch.Text = "";
        }

        private void txtSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!(this.txtSearch.Text == ""))
                return;
            this.txtSearch.Text = "Search";
        }

        private void lstPlaylist_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            this.lstPlaylist.ToolTip = (object)"Use Context Menu";
        }

        private void mnuPlaylistPlay_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstPlaylist.SelectedIndex == -1)
                return;
            this.cfinf.idx = this.lstPlaylist.SelectedIndex;
            this.CheckAndAdvance(true);
            this.tmr1.Start();
            this.tmr2.Start();
            this.tmr4.Start();
        }

        private void mnuPlaylistInfo_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstPlaylist.SelectedIndex == -1)
                return;
            DataRowView selectedItem = (DataRowView)this.lstPlaylist.SelectedItem;
            Info info = new Info(selectedItem["song_name"].ToString(), selectedItem["artist"].ToString(), selectedItem["album"].ToString());
            info.Owner = (Window)this;
            info.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            info.ShowDialog();
        }

        private void mnuPlaylistRemove_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstPlaylist.SelectedIndex == -1 || this.lstPlaylist.Items.Count <= 0)
                return;
            if (this.cfinf.idx == this.lstPlaylist.SelectedIndex)
            {
                this.StartStopTimers(false);
                this.plyr.Stop();
                this.dsTracks.Tables[0].Rows.RemoveAt(this.cfinf.idx);
                if (this.cfinf.idx >= this.lstPlaylist.Items.Count)
                    --this.cfinf.idx;
                if (this.cfinf.idx == -1)
                {
                    this.cfinf.Reset();
                    this.lblInformationBar.Content = (object)"Information Bar";
                    this.imgAlbumArt.Source = (ImageSource)new BitmapImage(new Uri("/MediaPlayerFinal;component/Images/music.png", UriKind.Relative));
                }
                else
                    this.SetCurrentPlaybackData(this.cfinf.idx);
                this.lstPlaylist.SelectedIndex = this.cfinf.idx;
            }
            else if (this.cfinf.idx > this.lstPlaylist.SelectedIndex)
            {
                this.dsTracks.Tables[0].Rows.RemoveAt(this.lstPlaylist.SelectedIndex);
                --this.cfinf.idx;
                this.lstPlaylist.SelectedIndex = this.cfinf.idx;
            }
            else
            {
                this.dsTracks.Tables[0].Rows.RemoveAt(this.lstPlaylist.SelectedIndex);
                this.lstPlaylist.SelectedIndex = this.cfinf.idx;
            }
        }

        private void SetCurrentPlaybackData(int idx)
        {
            if (this.lstPlaylist.Items.Count != 0)
            {
                this.cfinf.Artist = ((DataRowView)this.lstPlaylist.Items[idx])["artist"].ToString();
                this.cfinf.Album = ((DataRowView)this.lstPlaylist.Items[idx])["album"].ToString();
                this.cfinf.Title = ((DataRowView)this.lstPlaylist.Items[idx])["song_name"].ToString();
                this.cfinf.Year = ((DataRowView)this.lstPlaylist.Items[idx])["song_year"].ToString();
                if (this.cfinf.Path != ((DataRowView)this.lstPlaylist.Items[idx])["location"].ToString())
                {
                    this.cfinf.Path = ((DataRowView)this.lstPlaylist.Items[idx])["location"].ToString();
                    this.plyr.Play(this.cfinf.Path);
                    this.plyr.Stop();
                }
                this.lblInformationBar.Content = (object)this.cfinf.Title;
                this.tg.File = this.cfinf.Path;
                this.imgAlbumArt.Source = this.tg.FrontCover == null ? (ImageSource)new BitmapImage(new Uri("/MediaPlayerFinal;component/Images/music.png", UriKind.Relative)) : (ImageSource)this.ConvertToWPFImageSource(this.tg.FrontCover);
            }
            else
            {
                this.lblInformationBar.Content = (object)"Information Bar";
                this.VUML.Level = 0;
                this.VUMR.Level = 0;
                this.sliSeekBar.Value = 0.0;
                this.imgAlbumArt.Source = (ImageSource)new BitmapImage(new Uri("/MediaPlayerFinal;component/Images/music.png", UriKind.Relative));
            }
        }

        private void mnuPlaylistRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            if (this.lstPlaylist.Items.Count == 0)
                return;
            this.StartStopTimers(false);
            this.plyr.Stop();
            this.dsTracks.Tables[0].Clear();
            this.cfinf.Reset();
            this.cfinf.idx = -1;
            this.lblInformationBar.Content = (object)"Information Bar";
            this.imgAlbumArt.Source = (ImageSource)new BitmapImage(new Uri("/MediaPlayerFinal;component/Images/music.png", UriKind.Relative));
        }

        private void mnuPlaylistNormal_Click(object sender, RoutedEventArgs e)
        {
            if (!this.mnuPlaylistNormal.IsChecked)
                return;
            this.mnuPlaylistRepeat.IsChecked = false;
            this.mnuPlaylistRepeatAll.IsChecked = false;
            this.cfinf.repeat = 0;
            Settings.Default.Repeat = 0;
            Settings.Default.Save();
        }

        private void mnuPlaylistRepeat_Click(object sender, RoutedEventArgs e)
        {
            if (!this.mnuPlaylistRepeat.IsChecked)
                return;
            this.mnuPlaylistNormal.IsChecked = false;
            this.mnuPlaylistRepeatAll.IsChecked = false;
            this.cfinf.repeat = 1;
            Settings.Default.Repeat = 1;
            Settings.Default.Save();
        }

        private void mnuPlaylistRepeatAll_Click(object sender, RoutedEventArgs e)
        {
            if (!this.mnuPlaylistRepeatAll.IsChecked)
                return;
            this.mnuPlaylistRepeat.IsChecked = false;
            this.mnuPlaylistNormal.IsChecked = false;
            this.cfinf.repeat = 2;
            Settings.Default.Repeat = 2;
            Settings.Default.Save();
        }

        private void mnuPlaylistShuffled_Click(object sender, RoutedEventArgs e)
        {
            if (this.mnuPlaylistShuffled.IsChecked)
            {
                this.cfinf.shuffled = true;
                Settings.Default.Shuffled = true;
                Settings.Default.Save();
            }
            else
            {
                this.cfinf.shuffled = false;
                Settings.Default.Shuffled = false;
                Settings.Default.Save();
            }
        }

        private void mnuPlaylistSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.dsTracks == null || this.lstPlaylist.Items.Count == 0)
                return;
            this.dlgSave.Filter = "RadioDeskPlaylist (.rpl)|*.rpl";
            this.dlgSave.Title = "Save RadioDesk Playlist";
            this.dlgSave.FileName = "";
            bool? nullable = this.dlgSave.ShowDialog();
            if ((!nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) == 0)
                return;
            XmlWriter writer = XmlWriter.Create(this.dlgSave.FileName, new XmlWriterSettings()
            {
                Encoding = Encoding.UTF32
            });
            this.dsTracks.DataSetName = this.dlgSave.SafeFileName;
            this.dsTracks.WriteXml(writer);
        }

        private void mnuPlaylistLoad_Click(object sender, RoutedEventArgs e)
        {
            if (this.dsTracks == null)
                return;
            this.dlgOpen.Filter = "RadioDeskPlaylist (.rpl)|*.rpl";
            this.dlgOpen.Title = "Open RadioDesk Playlist";
            this.dlgOpen.FileName = "";
            bool? nullable = this.dlgOpen.ShowDialog();
            if ((!nullable.GetValueOrDefault() ? 0 : (nullable.HasValue ? 1 : 0)) == 0)
                return;
            XmlReader reader = XmlReader.Create((TextReader)new StreamReader(this.dlgOpen.FileName, Encoding.UTF32));
            this.dsTracks.DataSetName = this.dlgOpen.SafeFileName;
            int num = (int)this.dsTracks.ReadXml(reader);
            this.StartStopTimers(false);
            if (this.lstPlaylist.Items.Count == 0)
                return;
            this.lstPlaylist.SelectedIndex = 0;
        }

        private void StartStopTimers(bool start)
        {
            if (start)
            {
                this.tmr1.Start();
                this.tmr2.Start();
                this.tmr4.Start();
            }
            else
            {
                this.sliSeekBar.Value = 0.0;
                this.VUML.Level = 0;
                this.VUMR.Level = 0;
                this.tmr1.Stop();
                this.tmr2.Stop();
                this.tmr4.Stop();
            }
        }

        private BitmapImage ConvertToWPFImageSource(System.Drawing.Image SourceImg)
        {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            MemoryStream memoryStream = new MemoryStream();
            this.tg.FrontCover.Save((Stream)memoryStream, ImageFormat.Bmp);
            memoryStream.Seek(0L, SeekOrigin.Begin);
            bitmapImage.StreamSource = (Stream)memoryStream;
            bitmapImage.EndInit();
            return bitmapImage;
        }

        private void btnshuf_Click(object sender, RoutedEventArgs e)
        {
        }

        private void checkNotResponding()
        {
            while (true)
            {
                if (!Process.GetCurrentProcess().Responding)
                {
                    int num = (int)MessageBox.Show("Not working");
                    this.plyr.Stop();
                }
                Thread.Sleep(3000);
            }
        }

        private void lblInformationBar_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (this.cfinf.Artist == null || this.cfinf.Album == null)
                return;
            if (this.cfinf.Title == null)
                return;
            try
            {
                Info info = new Info(this.cfinf.Title, this.cfinf.Artist, this.cfinf.Album);
                info.Owner = (Window)this;
                info.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                info.ShowDialog();
            }
            catch (Exception ex)
            {
            }
        }

        //[DebuggerNonUserCode]
        //public void InitializeComponent()
        //{
        //    if (this._contentLoaded)
        //        return;
        //    this._contentLoaded = true;
        //    Application.LoadComponent((object)this, new Uri("/MediaPlayerFinal;component/mainwindow.xaml", UriKind.Relative));
        //}

        //[DebuggerNonUserCode]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //void IComponentConnector.Connect(int connectionId, object target)
        //{
        //    switch (connectionId)
        //    {
        //        case 1:
        //            ((FrameworkElement)target).Loaded += new RoutedEventHandler(this.Window_Loaded);
        //            break;
        //        case 2:
        //            this.grdLayoutRoot = (Grid)target;
        //            break;
        //        case 3:
        //            this.splSearch = (StackPanel)target;
        //            break;
        //        case 4:
        //            this.tvwLibrary = (TreeView)target;
        //            break;
        //        case 5:
        //            this.treechildTitle = (TreeViewItem)target;
        //            this.treechildTitle.Selected += new RoutedEventHandler(this.treechildTitle_Selected);
        //            break;
        //        case 6:
        //            this.stpchildTitle = (StackPanel)target;
        //            this.stpchildTitle.MouseUp += new MouseButtonEventHandler(this.stpchildTitle_MouseUp);
        //            break;
        //        case 7:
        //            this.treechildArtist = (TreeViewItem)target;
        //            this.treechildArtist.Selected += new RoutedEventHandler(this.treechildArtist_Selected);
        //            break;
        //        case 8:
        //            this.childArtist = (StackPanel)target;
        //            this.childArtist.MouseUp += new MouseButtonEventHandler(this.childArtist_MouseUp);
        //            break;
        //        case 9:
        //            this.treechildAlbum = (TreeViewItem)target;
        //            this.treechildAlbum.Selected += new RoutedEventHandler(this.treechildAlbum_Selected);
        //            this.treechildAlbum.MouseUp += new MouseButtonEventHandler(this.treechildAlbum_MouseUp);
        //            break;
        //        case 10:
        //            this.stpchildAlbum = (StackPanel)target;
        //            break;
        //        case 11:
        //            this.treechildYear = (TreeViewItem)target;
        //            this.treechildYear.Selected += new RoutedEventHandler(this.treechildYear_Selected);
        //            break;
        //        case 12:
        //            this.childYear = (StackPanel)target;
        //            this.childYear.MouseUp += new MouseButtonEventHandler(this.childYear_MouseUp);
        //            break;
        //        case 13:
        //            this.tbcFeatures2 = (TabControl)target;
        //            break;
        //        case 14:
        //            this.tbiMixer = (TabItem)target;
        //            break;
        //        case 15:
        //            this.griMixer = (Grid)target;
        //            break;
        //        case 16:
        //            this.sliMasterVolume = (Slider)target;
        //            this.sliMasterVolume.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliMasterVolume_ValueChanged);
        //            break;
        //        case 17:
        //            this.sliPlayerVolume = (Slider)target;
        //            this.sliPlayerVolume.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliPlayerVolume_ValueChanged);
        //            break;
        //        case 18:
        //            this.sliMicVolume = (Slider)target;
        //            this.sliMicVolume.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliMicVolume_ValueChanged);
        //            break;
        //        case 19:
        //            this.rectangle1 = (System.Windows.Shapes.Rectangle)target;
        //            break;
        //        case 20:
        //            this.label4 = (Label)target;
        //            break;
        //        case 21:
        //            this.label5 = (Label)target;
        //            break;
        //        case 22:
        //            this.chkMuteMaster = (CheckBox)target;
        //            this.chkMuteMaster.Checked += new RoutedEventHandler(this.chkMuteMaster_Checked);
        //            this.chkMuteMaster.Unchecked += new RoutedEventHandler(this.chkMuteMaster_Unchecked);
        //            break;
        //        case 23:
        //            this.rectangle2 = (System.Windows.Shapes.Rectangle)target;
        //            break;
        //        case 24:
        //            this.label6 = (Label)target;
        //            break;
        //        case 25:
        //            this.label7 = (Label)target;
        //            break;
        //        case 26:
        //            this.chkMutePlayer = (CheckBox)target;
        //            this.chkMutePlayer.Checked += new RoutedEventHandler(this.chkMutePlayer_Checked);
        //            this.chkMutePlayer.Unchecked += new RoutedEventHandler(this.chkMutePlayer_Unchecked);
        //            break;
        //        case 27:
        //            this.rectangle3 = (System.Windows.Shapes.Rectangle)target;
        //            break;
        //        case 28:
        //            this.label8 = (Label)target;
        //            break;
        //        case 29:
        //            this.label9 = (Label)target;
        //            break;
        //        case 30:
        //            this.chkMuteMic = (CheckBox)target;
        //            this.chkMuteMic.Checked += new RoutedEventHandler(this.chkMuteMic_Checked);
        //            this.chkMuteMic.Unchecked += new RoutedEventHandler(this.chkMuteMic_Unchecked);
        //            break;
        //        case 31:
        //            this.tbiEqualizer = (TabItem)target;
        //            break;
        //        case 32:
        //            this.grdEquilizer = (Grid)target;
        //            break;
        //        case 33:
        //            this.sliEqPreAmp = (Slider)target;
        //            this.sliEqPreAmp.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEqPreAmp_ValueChanged);
        //            break;
        //        case 34:
        //            this.lblEqPreamp = (Label)target;
        //            break;
        //        case 35:
        //            this.sliEq1 = (Slider)target;
        //            this.sliEq1.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq1_ValueChanged);
        //            break;
        //        case 36:
        //            this.lblEq1 = (Label)target;
        //            break;
        //        case 37:
        //            this.sliEq2 = (Slider)target;
        //            this.sliEq2.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq2_ValueChanged);
        //            break;
        //        case 38:
        //            this.lblEq2 = (Label)target;
        //            break;
        //        case 39:
        //            this.sliEq3 = (Slider)target;
        //            this.sliEq3.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq3_ValueChanged);
        //            break;
        //        case 40:
        //            this.lblEq3 = (Label)target;
        //            break;
        //        case 41:
        //            this.sliEq4 = (Slider)target;
        //            this.sliEq4.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq4_ValueChanged);
        //            break;
        //        case 42:
        //            this.lblEq4 = (Label)target;
        //            break;
        //        case 43:
        //            this.sliEq5 = (Slider)target;
        //            this.sliEq5.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq5_ValueChanged);
        //            break;
        //        case 44:
        //            this.lblEq6 = (Label)target;
        //            break;
        //        case 45:
        //            this.sliEq6 = (Slider)target;
        //            this.sliEq6.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq6_ValueChanged);
        //            break;
        //        case 46:
        //            this.label3 = (Label)target;
        //            break;
        //        case 47:
        //            this.lblEq7 = (Label)target;
        //            break;
        //        case 48:
        //            this.sliEq7 = (Slider)target;
        //            this.sliEq7.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq7_ValueChanged);
        //            break;
        //        case 49:
        //            this.lblEq8 = (Label)target;
        //            break;
        //        case 50:
        //            this.sliEq8 = (Slider)target;
        //            this.sliEq8.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq8_ValueChanged);
        //            break;
        //        case 51:
        //            this.lblEq9 = (Label)target;
        //            break;
        //        case 52:
        //            this.sliEq9 = (Slider)target;
        //            this.sliEq9.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq9_ValueChanged);
        //            break;
        //        case 53:
        //            this.lblEq10 = (Label)target;
        //            break;
        //        case 54:
        //            this.sliEq10 = (Slider)target;
        //            this.sliEq10.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEq10_ValueChanged);
        //            break;
        //        case 55:
        //            this.splButtons = (StackPanel)target;
        //            break;
        //        case 56:
        //            this.chkEnableEQ = (CheckBox)target;
        //            this.chkEnableEQ.Checked += new RoutedEventHandler(this.chkEnableEQ_Checked);
        //            this.chkEnableEQ.Unchecked += new RoutedEventHandler(this.chkEnableEQ_Unchecked);
        //            break;
        //        case 57:
        //            this.btnEQSave = (Button)target;
        //            this.btnEQSave.Click += new RoutedEventHandler(this.btnEQSave_Click);
        //            break;
        //        case 58:
        //            this.btnEQLoad = (Button)target;
        //            this.btnEQLoad.Click += new RoutedEventHandler(this.btnEQLoad_Click);
        //            break;
        //        case 59:
        //            this.cmbEQPreset = (ComboBox)target;
        //            this.cmbEQPreset.SelectionChanged += new SelectionChangedEventHandler(this.cmbEQPreset_SelectionChanged);
        //            break;
        //        case 60:
        //            this.label15 = (Label)target;
        //            break;
        //        case 61:
        //            this.tbiEffects = (TabItem)target;
        //            break;
        //        case 62:
        //            this.gridEffects = (Grid)target;
        //            break;
        //        case 63:
        //            this.lblStereoEffects = (Label)target;
        //            break;
        //        case 64:
        //            this.lblTempo = (Label)target;
        //            break;
        //        case 65:
        //            this.lblPitch = (Label)target;
        //            break;
        //        case 66:
        //            this.lblRate = (Label)target;
        //            break;
        //        case 67:
        //            this.spStereoEffects = (StackPanel)target;
        //            break;
        //        case 68:
        //            this.spTempo = (StackPanel)target;
        //            break;
        //        case 69:
        //            this.cmbTempo = (ComboBox)target;
        //            this.cmbTempo.SelectionChanged += new SelectionChangedEventHandler(this.cmbTempo_SelectionChanged);
        //            break;
        //        case 70:
        //            this.spPitch = (StackPanel)target;
        //            break;
        //        case 71:
        //            this.cmbPitch = (ComboBox)target;
        //            this.cmbPitch.SelectionChanged += new SelectionChangedEventHandler(this.cmbPitch_SelectionChanged);
        //            break;
        //        case 72:
        //            this.spRate = (StackPanel)target;
        //            break;
        //        case 73:
        //            this.cmbRate = (ComboBox)target;
        //            this.cmbRate.SelectionChanged += new SelectionChangedEventHandler(this.cmbRate_SelectionChanged);
        //            break;
        //        case 74:
        //            this.cmbStereoEffects = (ComboBox)target;
        //            this.cmbStereoEffects.SelectionChanged += new SelectionChangedEventHandler(this.cmbStereoEffects_SelectionChanged);
        //            break;
        //        case 75:
        //            this.tbiMic = (TabItem)target;
        //            break;
        //        case 76:
        //            this.sliEchoLevel = (Slider)target;
        //            this.sliEchoLevel.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEchoLevel_ValueChanged);
        //            break;
        //        case 77:
        //            this.lblEchoLevel = (Label)target;
        //            break;
        //        case 78:
        //            this.sliEchoVolume = (Slider)target;
        //            this.sliEchoVolume.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliEchoVolume_ValueChanged);
        //            break;
        //        case 79:
        //            this.lblEchoVolume = (Label)target;
        //            break;
        //        case 80:
        //            this.chkMicEffectsEnable = (CheckBox)target;
        //            this.chkMicEffectsEnable.Checked += new RoutedEventHandler(this.chkMicEffectsEnable_Checked);
        //            this.chkMicEffectsEnable.Unchecked += new RoutedEventHandler(this.chkMicEffectsEnable_Unchecked);
        //            break;
        //        case 81:
        //            this.tbcFeatures1 = (TabControl)target;
        //            break;
        //        case 82:
        //            this.tbiPlaylist = (TabItem)target;
        //            break;
        //        case 83:
        //            this.lstPlaylist = (ListBox)target;
        //            this.lstPlaylist.PreviewMouseDoubleClick += new MouseButtonEventHandler(this.lstPlaylist_PreviewMouseDoubleClick);
        //            break;
        //        case 84:
        //            this.mnuPlaylistPlay = (MenuItem)target;
        //            this.mnuPlaylistPlay.Click += new RoutedEventHandler(this.mnuPlaylistPlay_Click);
        //            break;
        //        case 85:
        //            this.mnuPlaylistInfo = (MenuItem)target;
        //            this.mnuPlaylistInfo.Click += new RoutedEventHandler(this.mnuPlaylistInfo_Click);
        //            break;
        //        case 86:
        //            this.mnuPlaylistRemove = (MenuItem)target;
        //            this.mnuPlaylistRemove.Click += new RoutedEventHandler(this.mnuPlaylistRemove_Click);
        //            break;
        //        case 87:
        //            this.mnuPlaylistRemoveAll = (MenuItem)target;
        //            this.mnuPlaylistRemoveAll.Click += new RoutedEventHandler(this.mnuPlaylistRemoveAll_Click);
        //            break;
        //        case 88:
        //            this.mnuPlaylistMode = (MenuItem)target;
        //            break;
        //        case 89:
        //            this.mnuPlaylistNormal = (MenuItem)target;
        //            this.mnuPlaylistNormal.Click += new RoutedEventHandler(this.mnuPlaylistNormal_Click);
        //            break;
        //        case 90:
        //            this.mnuPlaylistRepeat = (MenuItem)target;
        //            this.mnuPlaylistRepeat.Click += new RoutedEventHandler(this.mnuPlaylistRepeat_Click);
        //            break;
        //        case 91:
        //            this.mnuPlaylistRepeatAll = (MenuItem)target;
        //            this.mnuPlaylistRepeatAll.Click += new RoutedEventHandler(this.mnuPlaylistRepeatAll_Click);
        //            break;
        //        case 92:
        //            this.mnuPlaylistShuffled = (MenuItem)target;
        //            this.mnuPlaylistShuffled.Click += new RoutedEventHandler(this.mnuPlaylistShuffled_Click);
        //            break;
        //        case 93:
        //            this.mnuPlaylistSave = (MenuItem)target;
        //            this.mnuPlaylistSave.Click += new RoutedEventHandler(this.mnuPlaylistSave_Click);
        //            break;
        //        case 94:
        //            this.mnuPlaylistLoad = (MenuItem)target;
        //            this.mnuPlaylistLoad.Click += new RoutedEventHandler(this.mnuPlaylistLoad_Click);
        //            break;
        //        case 95:
        //            this.tbiClipPlayer = (TabItem)target;
        //            break;
        //        case 96:
        //            this.sliClipsAdsVol = (Slider)target;
        //            this.sliClipsAdsVol.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.sliClips_ValueChanged);
        //            break;
        //        case 97:
        //            this.cmbClips = (ComboBox)target;
        //            this.cmbClips.DropDownClosed += new EventHandler(this.cmbClips_DropDownClosed);
        //            break;
        //        case 98:
        //            this.cmbAds = (ComboBox)target;
        //            this.cmbAds.DropDownClosed += new EventHandler(this.cmbAds_DropDownClosed);
        //            break;
        //        case 99:
        //            this.sp = (StackPanel)target;
        //            break;
        //        case 100:
        //            this.image2 = (System.Windows.Controls.Image)target;
        //            break;
        //        case 101:
        //            this.rectangle9 = (System.Windows.Shapes.Rectangle)target;
        //            break;
        //        case 102:
        //            this.label13 = (Label)target;
        //            break;
        //        case 103:
        //            this.lblSelectedClipAd = (Label)target;
        //            break;
        //        case 104:
        //            this.btnClipPlay = (Button)target;
        //            this.btnClipPlay.Click += new RoutedEventHandler(this.btnClipPlay_Click);
        //            break;
        //        case 105:
        //            this.btnClipStop = (Button)target;
        //            this.btnClipStop.Click += new RoutedEventHandler(this.btnClipStop_Click);
        //            break;
        //        case 106:
        //            this.splPlayerButtons = (StackPanel)target;
        //            break;
        //        case 107:
        //            this.sptext = (StackPanel)target;
        //            break;
        //        case 108:
        //            this.spButtons = (StackPanel)target;
        //            break;
        //        case 109:
        //            this.btnprevious = (Button)target;
        //            this.btnprevious.Click += new RoutedEventHandler(this.btnprevious_Click);
        //            break;
        //        case 110:
        //            this.btnRewind = (Button)target;
        //            this.btnRewind.Click += new RoutedEventHandler(this.btnRewind_Click);
        //            break;
        //        case 111:
        //            this.btnStop = (Button)target;
        //            this.btnStop.Click += new RoutedEventHandler(this.btnStop_Click);
        //            break;
        //        case 112:
        //            this.btnPlay = (Button)target;
        //            this.btnPlay.Click += new RoutedEventHandler(this.btnPlay_Click);
        //            break;
        //        case 113:
        //            this.btnpause = (Button)target;
        //            this.btnpause.Click += new RoutedEventHandler(this.btnpause_Click);
        //            break;
        //        case 114:
        //            this.btnForward = (Button)target;
        //            this.btnForward.Click += new RoutedEventHandler(this.btnForward_Click);
        //            break;
        //        case 115:
        //            this.btnNext = (Button)target;
        //            this.btnNext.Click += new RoutedEventHandler(this.btnNext_Click);
        //            break;
        //        case 116:
        //            this.stackPanel1 = (StackPanel)target;
        //            break;
        //        case 117:
        //            this.txtSearch = (TextBox)target;
        //            this.txtSearch.TextChanged += new TextChangedEventHandler(this.txtSearch_TextChanged);
        //            this.txtSearch.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.txtSearch_PreviewMouseLeftButtonDown);
        //            this.txtSearch.LostFocus += new RoutedEventHandler(this.txtSearch_LostFocus);
        //            break;
        //        case 118:
        //            this.image1 = (System.Windows.Controls.Image)target;
        //            break;
        //        case 119:
        //            this.stackPanel2 = (StackPanel)target;
        //            break;
        //        case 120:
        //            this.btnshuf = (Button)target;
        //            this.btnshuf.Click += new RoutedEventHandler(this.btnshuf_Click);
        //            break;
        //        case 121:
        //            this.rectangle4 = (System.Windows.Shapes.Rectangle)target;
        //            break;
        //        case 122:
        //            this.label10 = (Label)target;
        //            break;
        //        case 123:
        //            this.label11 = (Label)target;
        //            break;
        //        case 124:
        //            this.label12 = (Label)target;
        //            break;
        //        case 125:
        //            this.rectangle5 = (System.Windows.Shapes.Rectangle)target;
        //            break;
        //        case 126:
        //            this.windowsFormsHost1 = (WindowsFormsHost)target;
        //            break;
        //        case (int)sbyte.MaxValue:
        //            this.VUML = (VuMeter)target;
        //            break;
        //        case 128:
        //            this.windowsFormsHost2 = (WindowsFormsHost)target;
        //            break;
        //        case 129:
        //            this.VUMR = (VuMeter)target;
        //            break;
        //        case 130:
        //            this.rectangle6 = (System.Windows.Shapes.Rectangle)target;
        //            break;
        //        case 131:
        //            this.windowsFormsHost3 = (WindowsFormsHost)target;
        //            break;
        //        case 132:
        //            this.VUMM = (VuMeter)target;
        //            break;
        //        case 133:
        //            this.listView1 = (ListView)target;
        //            this.listView1.MouseDoubleClick += new MouseButtonEventHandler(this.listView1_MouseDoubleClick);
        //            break;
        //        case 134:
        //            this.mnuResultGrid = (ContextMenu)target;
        //            break;
        //        case 135:
        //            this.mnuListViewAddToPlaylist = (MenuItem)target;
        //            this.mnuListViewAddToPlaylist.Click += new RoutedEventHandler(this.mnuListViewAddToPlaylist_Click);
        //            break;
        //        case 136:
        //            this.mnuListViewInfo = (MenuItem)target;
        //            this.mnuListViewInfo.Click += new RoutedEventHandler(this.mnuListViewInfo_Click);
        //            break;
        //        case 137:
        //            this.mnuListViewSearchMedia = (MenuItem)target;
        //            this.mnuListViewSearchMedia.Click += new RoutedEventHandler(this.mnuListViewSearchMedia_Click);
        //            break;
        //        case 138:
        //            this.gvSearchResult = (GridView)target;
        //            break;
        //        case 139:
        //            this.rectangle8 = (System.Windows.Shapes.Rectangle)target;
        //            break;
        //        case 140:
        //            this.lblInformationBar = (Label)target;
        //            this.lblInformationBar.MouseDoubleClick += new MouseButtonEventHandler(this.lblInformationBar_MouseDoubleClick);
        //            break;
        //        case 141:
        //            this.lblCurrentTime = (Label)target;
        //            break;
        //        case 142:
        //            this.lblTotalTime = (Label)target;
        //            break;
        //        case 143:
        //            this.label1 = (Label)target;
        //            break;
        //        case 144:
        //            this.lblShuffleInfo = (Label)target;
        //            break;
        //        case 145:
        //            this.rectangle7 = (System.Windows.Shapes.Rectangle)target;
        //            break;
        //        case 146:
        //            this.sliSeekBar = (Slider)target;
        //            this.sliSeekBar.PreviewMouseDown += new MouseButtonEventHandler(this.sliSeekBar_PreviewMouseDown);
        //            this.sliSeekBar.PreviewMouseUp += new MouseButtonEventHandler(this.sliSeekBar_PreviewMouseUp);
        //            break;
        //        case 147:
        //            this.imgAlbumArt = (System.Windows.Controls.Image)target;
        //            break;
        //        default:
        //            this._contentLoaded = true;
        //            break;
        //    }
        //}

        private delegate void delUpdateLableText(Label lblnam, string valu);

        private delegate void delUpdateSeekBar(uint valu);

        private delegate void delChangeTitle(string title);

        private delegate void delPlaylistAdvance(bool isListView);
    }
}
