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
using EvoPlayer.Core.Data;
using EvoPlayer.Core.Data.Domain;
using System.Collections.ObjectModel;

namespace EvoPlayer.Comp.ML
{
    /// <summary>
    /// Interaction logic for MLLocalMediaView.xaml
    /// </summary>
    public partial class MLLocalMediaView : UserControl, INotifyResize
    {
        private ObservableCollection<Playlist> playListsVM = new ObservableCollection<Playlist>();
        private ObservableCollection<AlbumViewModel> albumsVM = new ObservableCollection<AlbumViewModel>();
        private ObservableCollection<TrackViewModel> trackVM = new ObservableCollection<TrackViewModel>();

        public MLLocalMediaView()
        {
            InitializeComponent();


            this.lstLocalAlbums.ItemsSource = albumsVM;
            this.lstLocalTracks.ItemsSource = trackVM;

            InitView();
        }


        private void InitView()
        {
            var artists = DB.GetLocalArtists();
            foreach (var art in artists)
            {
                lstLocalArtist.Items.Add(art);
            }

            if (artists.Count > 0)
            {
                lstLocalArtist.SelectedIndex = 0;
            }

            var pls = DB.GetPlaylists();
            pls.ForEach(x => { playListsVM.Add(x); });

            mnuAlbumAddToPlaylist.ItemsSource = playListsVM;
            mnuArtistAddToPlaylist.ItemsSource = playListsVM;
            mnuTrackAddToPlaylist.ItemsSource = playListsVM;
        }

        private void lstLocalArtist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstLocalArtist.SelectedItem != null)
            {
                var albums = DB.GetLocalAlbums(lstLocalArtist.SelectedItem.ToString());
                albumsVM.Clear();

                albums.ForEach(x =>
                {
                    albumsVM.Add(new AlbumViewModel(x));
                });

                if (albumsVM.Count > 0)
                {
                    lstLocalAlbums.SelectedIndex = 0;
                }
            }
        }

        private void lstLocalAlbums_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstLocalArtist.SelectedItem != null && lstLocalAlbums.SelectedItem != null)
            {
                var alvm = (AlbumViewModel)lstLocalAlbums.SelectedItem;
                List<LocalMediaItem> tracks = DB.GetLocalTracks(lstLocalArtist.SelectedItem.ToString(), alvm.AlbumTitle);
                trackVM.Clear();

                tracks.ForEach(x =>
                {
                    trackVM.Add(new TrackViewModel(x));
                });


            }
        }

        public void WindowResized()
        {
            //var traks = this.gridMain.RowDefinitions[1];
            //lstLocalTracks.MaxHeight = traks.ActualHeight;
        }

        internal class AlbumViewModel
        {
            public AlbumViewModel(LocalMediaItem i)
            {
                this.AlbumTitle = i.Album;
                using (var tag = TagLib.File.Create(i.Path))
                {
                    if (tag.Tag != null && tag.Tag.Pictures != null && tag.Tag.Pictures.Length > 0)
                    {
                        AlbumArt = new BitmapImage();
                        using (var mem = new System.IO.MemoryStream(tag.Tag.Pictures[0].Data.ToArray()))
                        {
                            mem.Position = 0;
                            RenderOptions.SetBitmapScalingMode(AlbumArt, BitmapScalingMode.HighQuality);
                            AlbumArt.BeginInit();
                            AlbumArt.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                            AlbumArt.CacheOption = BitmapCacheOption.OnLoad;
                            AlbumArt.UriSource = null;
                            AlbumArt.StreamSource = mem;
                            AlbumArt.EndInit();
                        }
                        AlbumArt.Freeze();
                    }
                    else
                    {
                        AlbumArt = new BitmapImage(new Uri("pack://application:,,,/Resources/music_album.png"));
                    }
                }
            }

            public BitmapImage AlbumArt { get; set; }
            public string AlbumTitle { get; set; }
        }

        internal class TrackViewModel
        {
            public TrackViewModel(LocalMediaItem i)
            {
                this.Album = i.Album;
                this.Artist = i.Artist;
                this.Duration = i.Duration;
                this.Title = i.Title;
            }

            public string Artist { get; set; }
            public string Album { get; set; }
            public string Title { get; set; }
            public TimeSpan Duration { get; set; }
        }

        private void lstLocalTracks_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gv = ((GridView)lstLocalTracks.View);
            for (int i = 0; i < gv.Columns.Count; i++)
            {
                gv.Columns[i].Width = gv.Columns[i].ActualWidth;
                gv.Columns[i].Width = double.NaN;
            }
        }

        private void mnuArtistPlay_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuArtistAddToPlaylist_Click(object sender, RoutedEventArgs e)
        {
        }

        private void mnuAlbumAddToPlaylist_Click(object sender, RoutedEventArgs e)
        {

        }

        private void mnuTrackAddToPlaylist_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
