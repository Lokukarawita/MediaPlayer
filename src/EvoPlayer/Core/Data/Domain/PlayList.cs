using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Data.Domain
{
    public class Playlist
    {
        public Playlist()
        {
            Entries = new List<PlaylistEntry>();
        }

        public int Id { get; set; }
        public string PlaylistName { get; set; }
        public List<PlaylistEntry> Entries { get; set; }
    }
}
