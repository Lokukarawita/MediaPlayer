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


        private static string DB_PATH = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EvoPlayer.edb");


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
                return cPLists.Include(x => x.Entries).FindAll().ToList();
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
        public static Playlist SavePlaylist(Playlist pl)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                foreach (var item in pl.Entries)
                {
                    var ple = db.GetCollection<PlaylistEntry>(C_PL_ENTRIES);
                    item.Playlist = pl;
                    ple.Upsert(item);
                }
                var col = db.GetCollection<Playlist>(C_PL_LISTS);
                col.Upsert(pl);

                return GetPlaylist(pl.Id);
            }
        }
        public static void DeletePlaylist(int plId)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var col = db.GetCollection<Playlist>(C_PL_LISTS);
                col.Delete(plId);
            }
        }
        public static void DeletePlaylistEntry(int entryId)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var col = db.GetCollection<PlaylistEntry>(C_PL_ENTRIES);
                col.Delete(entryId);
            }

        }
        public static void AddToPlaylistByArtist(int playListId, string artist)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var mlCol = db.GetCollection<LocalMediaItem>(C_ML_MLITEMS);
                var tracks = mlCol.Find(x => x.Artist == artist).ToList();
                var playList = GetPlaylist(playListId);
                foreach (var track in tracks)
                {
                    var entry = new PlaylistEntry(track);
                    playList.Entries.Add(entry);
                }
                SavePlaylist(playList);
            }
        }
        public static void AddToPlaylistByAlbum(int playListId, string artist, string albumTitle)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var mlCol = db.GetCollection<LocalMediaItem>(C_ML_MLITEMS);
                var tracks = mlCol.Find(x => x.Artist == artist && x.Album == albumTitle).ToList();
                var playList = GetPlaylist(playListId);
                foreach (var track in tracks)
                {
                    var entry = new PlaylistEntry(track);
                    playList.Entries.Add(entry);
                }
                SavePlaylist(playList);
            }
        }
        public static void AddToPlaylistByTrack(int playListId, int[] trackIDs)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var mlCol = db.GetCollection<LocalMediaItem>(C_ML_MLITEMS);
                var bsonValues = trackIDs.Select(x => new BsonValue(x));
                var query = Query.In("_id", bsonValues);
                var tracks = mlCol.Find(query).ToList();
                var playList = GetPlaylist(playListId);
                var entries = tracks.Select(x => new PlaylistEntry(x)).ToList();
                playList.Entries.AddRange(entries);
                SavePlaylist(playList);
            }
        }


        public static void AddLocalMedia(LocalMediaItem localMI)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var col = db.GetCollection<LocalMediaItem>(C_ML_MLITEMS);
                col.Upsert(localMI);
            }
        }
        public static List<string> GetLocalArtists()
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var col = db.GetCollection<LocalMediaItem>(C_ML_MLITEMS);
                var artists = col.FindAll().GroupBy(x => x.Artist).Select(g => g.First().Artist).ToList();
                return artists;
            }
        }
        public static List<LocalMediaItem> GetLocalAlbums(string artist)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var col = db.GetCollection<LocalMediaItem>(C_ML_MLITEMS);
                var albums = col.Find(x => x.Artist == artist).GroupBy(x => x.Album).Select(g => g.First()).ToList();
                return albums;
            }
        }
        public static List<LocalMediaItem> GetLocalTracks(string artist, string albumTitle)
        {
            using (LiteDatabase db = new LiteDatabase(DB_PATH))
            {
                var col = db.GetCollection<LocalMediaItem>(C_ML_MLITEMS);
                var tracks = col
                    .Find(x => x.Artist == artist && x.Album == albumTitle)
                    .ToList();
                return tracks;
            }
        }
    }
}
