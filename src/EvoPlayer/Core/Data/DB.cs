using EvoPlayer.Core.Data.Domain;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Data
{
    public class DB
    {
        private static string C_ML_MLITEMS = "ml_medialist";
        private static string C_PL_LISTS = "pl_lists";
        private static string C_PL_ENTRIES = "pl_entries";


        private static string DB_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EvoPlayer.db");


        static DB()
        {
            var mapper = BsonMapper.Global;

            mapper.Entity<LocalMediaItem>()
                .Id(x => x.Id, true);

            mapper.Entity<PlaylistEntry>()
                .Id(x => x.Id)
                .DbRef(x => x.Playlist, C_PL_LISTS);

            mapper.Entity<Playlist>()
                .Id(x => x.Id)
                .DbRef(x => x.Entries, C_PL_ENTRIES);

            InitDatabase();
        }

        private static void InitDatabase()
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var cPLists = db.GetCollection<Playlist>(C_PL_LISTS);
                var count = cPLists.Count();
                if (count == 0)
                {
                    Playlist p = new Playlist()
                    {
                        PlaylistName = "Now Playing"
                    };
                    cPLists.Insert(p);
                }
            }
        }


        public static List<Playlist> GetPlaylists()
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var cPLists = db.GetCollection<Playlist>(C_PL_LISTS);
                return cPLists.FindAll().ToList();
            }
        }
        public static Playlist GetPlaylist(int id)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var cPLists = db.GetCollection<Playlist>(C_PL_LISTS);
                return cPLists.Include(x => x.Entries).FindById(id);
            }
        }
        public static int CreatePlaylist(string name)
        {
            return CreatePlaylist(name, new List<PlaylistEntry>());
        }
        public static int CreatePlaylist(string name, List<PlaylistEntry> entries)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var pl = new Playlist() { PlaylistName = name };
                pl.Entries.AddRange(entries);
                var col = db.GetCollection<Playlist>(C_PL_LISTS);
                var value = col.Insert(pl).AsInt32;
                return value;
            }
        }
        public static void SavePlaylist(Playlist pl)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                foreach (var item in pl.Entries)
                {
                    var ple = db.GetCollection<PlaylistEntry>(C_PL_ENTRIES);
                    ple.Upsert(item);
                }
                var col = db.GetCollection<Playlist>(C_PL_LISTS);
                col.Upsert(pl);
            }
        }
    }
}
