using EvoPlayer.Core.Data;
using EvoPlayer.Core.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Ops
{
    public class PlaylistController
    {
        private Random rand;
        private int? currentItem;
        private Playlist playlist;


        public PlaylistController()
        {
            //Get not playing
            playlist = DB.GetPlaylist(1);
            currentItem = 0;
            rand = new Random();
        }
        public PlaylistController(Playlist p)
        {
            if (p == null)
            {
                playlist = DB.GetPlaylist(1);
            }
            else
            {
                playlist = p;
            }

            currentItem = 0;
            rand = new Random();
        }


        public PlaylistEntry Next()
        {
            PlaylistEntry returnvalue = null;

            if (!IsEmpty)
            {
                if (Shuffled)
                {
                    currentItem = rand.Next(0, Count);
                    return playlist.Entries[currentItem.Value];
                }
                else
                {
                    if (RepeatMode == PlaybackRepeatMode.Track)
                    {
                        return playlist.Entries[currentItem.Value];
                    }
                    else if ((currentItem + 1) >= Count)
                    {
                        if (RepeatMode == PlaybackRepeatMode.List)
                        {
                            currentItem = 0;
                            returnvalue = playlist.Entries[currentItem.Value];
                        }
                    }
                    else
                    {
                        currentItem++;
                        returnvalue = playlist.Entries[currentItem.Value];
                    }
                }
            }

            return returnvalue;
        }
        public PlaylistEntry GoTo(int number)
        {
            if (number >= Count)
                return null;
            else
            {
                currentItem = number;
                return playlist.Entries[currentItem.Value];
            }
        }
        public void ChangePlaylist(Playlist newPlaylist)
        {
            if (newPlaylist == null)
            {
                throw new ArgumentNullException(nameof(newPlaylist));
            }
            else
            {
                playlist = newPlaylist;
                currentItem = 0;
            }
        }


        public bool IsEmpty
        {
            get
            {
                if (playlist == null) return true;
                else if (playlist.Entries == null || playlist.Entries.Count == 0) return true;
                else return false;
            }
        }
        public int Count
        {
            get
            {
                if (IsEmpty) return 0;
                else return playlist.Entries.Count;
            }
        }
        public PlaylistEntry Current
        {
            get
            {
                if (IsEmpty) return null;
                else if (currentItem.Value >= playlist.Entries.Count)
                    return null;
                else
                    return playlist.Entries[currentItem.Value];


            }
        }
        public List<PlaylistEntry> All
        {
            get
            {
                return playlist.Entries.ToList();
            }
        }
        public bool Shuffled { get; set; }
        public PlaybackRepeatMode RepeatMode { get; set; }
    }
}
