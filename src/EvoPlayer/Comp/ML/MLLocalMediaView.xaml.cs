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
    public partial class MLLocalMediaView : UserControl
    {
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

        internal class AlbumViewModel
        {
            public AlbumViewModel(LocalMediaItem i)
            {
                this.AlbumTitle = i.Album;
                using (var tag = TagLib.File.Create(i.Path))
                {
                    AlbumArt = new BitmapImage();
                    using (var mem = new System.IO.MemoryStream(tag.Tag.Pictures[0].Data.ToArray()))
                    {
                        mem.Position = 0;
                        AlbumArt.BeginInit();
                        AlbumArt.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                        AlbumArt.CacheOption = BitmapCacheOption.OnLoad;
                        AlbumArt.UriSource = null;
                        AlbumArt.StreamSource = mem;
                        AlbumArt.EndInit();
                    }
                    AlbumArt.Freeze();
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
    }
}
