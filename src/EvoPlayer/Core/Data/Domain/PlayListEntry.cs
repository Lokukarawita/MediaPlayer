using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Data.Domain
{
    public class PlaylistEntry : MediaItem
    {
        public int Id { get; set; }
        public Playlist Playlist { get; set; }
    }
}
