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
            this.lstvItems.ItemsSource = litems;
            foreach (var item in currentPL.Entries)
            {
                var ple = new MLPlayListEntry(item);
                litems.Add(ple);
            }
        }

        public void AddItems(List<PlaylistEntry> entries)
        {
            foreach (var item in entries)
            {
                item.Playlist = currentPL;
                currentPL.Entries.Add(item);


                var uiitem = new MLPlayListEntry(item);
                litems.Add(uiitem);
            }

            DB.SavePlaylist(currentPL);
        }

        public class MLPlayListEntry
        {
            public MLPlayListEntry()
            {

            }
            public MLPlayListEntry(PlaylistEntry entry)
            {
                this.Title = entry.Title;
                this.Duration = entry.Duration;
                this.Entry = entry;
            }

            public string Title { get; set; }
            public TimeSpan Duration { get; set; }
            public PlaylistEntry Entry { get; set; }
        }
    }
}
