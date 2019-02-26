using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvoPlayer.Core.Data.Domain;

namespace EvoPlayer.Core.Player
{
    public class DatabasePlaylistControl : IPlaylistControl<PlaylistEntry>
    {
        private int currentItem;
        private List<PlaylistEntry> entries;

        public DatabasePlaylistControl()
        {
            currentItem = 0;
            entries = new List<PlaylistEntry>();
        }

        public void Add(PlaylistEntry[] entries)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public PlaylistEntry Next(bool shuffled)
        {
            throw new NotImplementedException();
        }

        public void Remove(PlaylistEntry[] entries)
        {
            throw new NotImplementedException();
        }


        public bool IsEmpty
        {
            get
            {
                return entries.Count == 0;
            }
        }
    }
}
