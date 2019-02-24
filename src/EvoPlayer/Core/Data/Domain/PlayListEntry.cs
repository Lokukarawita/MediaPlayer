using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Data.Domain
{
    public class PlaylistEntry : MediaItem
    {
        public PlaylistEntry()
        {

        }
        public PlaylistEntry(LocalMediaItem mediaItem)
        {
            this.Album = mediaItem.Album;
            this.Artist = mediaItem.Artist;
            this.Duration = mediaItem.Duration;
            this.Path = mediaItem.Path;
            this.StorageType = mediaItem.StorageType;
            this.Title = mediaItem.Title;
        }

        public int Id { get; set; }
        public Playlist Playlist { get; set; }
    }
}
