using EvoPlayer.Core.Data;
using EvoPlayer.Core.Data.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace EvoPlayer.Comp.ML
{
    public partial class MLPlaylistView : UserControl
    {
        private ObservableCollection<MLPlayListEntry> litems = new ObservableCollection<MLPlayListEntry>();
        private Playlist currentPL;

        public MLPlaylistView(Playlist playlist) : base()
        {
            InitializeComponent();

            currentPL = playlist;
            InitPlaylist();
        }

        private void InitPlaylist()
        {
            this.litems.Clear();
            this.lstvItems.ItemsSource = litems;
            foreach (var item in currentPL.Entries)
            {
                var ple = new MLPlayListEntry(item);
                litems.Add(ple);
            }
        }

        public void AddItems(List<PlaylistEntry> entries)
        {
            entries.ForEach(x => { x.Playlist = currentPL; });
            currentPL.Entries.AddRange(entries);
            currentPL = DB.SavePlaylist(currentPL);
            InitPlaylist();
        }

        private void mnuRemoveItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (lstvItems.SelectedItems.Count > 0)
            {
                var selected = lstvItems.SelectedItems;
                foreach (var item in selected)
                {
                    var ple = item as MLPlayListEntry;
                    DB.DeletePlaylistEntry(ple.Entry.Id);
                }

                currentPL = DB.GetPlaylist(currentPL.Id);
                InitPlaylist();
            }
        }

        private void lstvItems_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (lstvItems.SelectedItem == null)
                lstvItems.ContextMenu.IsEnabled = false;
            else
                lstvItems.ContextMenu.IsEnabled = true;
        }

        public class MLPlayListEntry
        {
            public MLPlayListEntry()
            {

            }
            public MLPlayListEntry(PlaylistEntry entry)
            {
                this.Artist = entry.Artist;
                this.Title = entry.Title;
                this.Duration = entry.Duration;
                this.Entry = entry;
            }

            public string DisplayTitle
            {
                get
                {
                    return $"{this.Artist} - {this.Title}";
                }
            }
            public string Artist { get; set; }
            public string Title { get; set; }
            public TimeSpan Duration { get; set; }
            public PlaylistEntry Entry { get; set; }
        }
    }
}
