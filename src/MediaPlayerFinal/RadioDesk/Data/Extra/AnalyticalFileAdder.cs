// Decompiled with JetBrains decompiler
// Type: RadioDesk.Data.Extra.AnalyticalFileAdder
// Assembly: MediaPlayerFinal, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: FA266A67-626E-46F0-81D3-A2E3F3CE1F98
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\MediaPlayerFinal.exe

using RadioDesk.Core;
using System;
using System.Data.OleDb;
using System.IO;

namespace RadioDesk.Data.Extra
{
  public static class AnalyticalFileAdder
  {
    private static TagInfo tg;
    private static DBConnection dbcon;
    private static SongInfo sngInf;

    public static void AddSong(string path)
    {
      try
      {
        if (!File.Exists(path))
          return;
        AnalyticalFileAdder.tg = new TagInfo();
        AnalyticalFileAdder.dbcon = new DBConnection(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "media_Database.accdb"));
        AnalyticalFileAdder.sngInf = new SongInfo();
        AnalyticalFileAdder.dbcon.Open();
        AnalyticalFileAdder.tg.File = path;
        AnalyticalFileAdder.sngInf.sngName = AnalyticalFileAdder.tg.Title == null ? Path.GetFileNameWithoutExtension(path) : AnalyticalFileAdder.tg.Title;
        AnalyticalFileAdder.sngInf.artist = AnalyticalFileAdder.tg.Artist == null ? "Unknown Artist" : AnalyticalFileAdder.tg.Artist;
        AnalyticalFileAdder.sngInf.album = AnalyticalFileAdder.tg.Album == null ? "Unknown Album" : AnalyticalFileAdder.tg.Album;
        AnalyticalFileAdder.sngInf.genre = AnalyticalFileAdder.tg.Geners == null ? "Unknown Genre" : AnalyticalFileAdder.tg.Geners;
        AnalyticalFileAdder.sngInf.MIME = AnalyticalFileAdder.tg.MIMEType;
        AnalyticalFileAdder.sngInf.location = path;
        AnalyticalFileAdder.sngInf.bitrate = AnalyticalFileAdder.tg.BitRate;
        AnalyticalFileAdder.sngInf.disk = (int) AnalyticalFileAdder.tg.Disk;
        AnalyticalFileAdder.sngInf.length = AnalyticalFileAdder.tg.Duration.TotalSeconds;
        AnalyticalFileAdder.sngInf.year = (int) AnalyticalFileAdder.tg.Year;
        AnalyticalFileAdder.InsertSong(AnalyticalFileAdder.sngInf);
        AnalyticalFileAdder.InsertArtist(AnalyticalFileAdder.sngInf.artist);
        AnalyticalFileAdder.InsertAlbum(AnalyticalFileAdder.sngInf.album, AnalyticalFileAdder.sngInf.artist);
      }
      catch (DBConnectionException ex)
      {
        throw new FileAdderException("Cannot connect to database.");
      }
      catch (TagReadException ex)
      {
        throw new FileAdderException("Cannot open this type of file.");
      }
      catch (Exception ex)
      {
        throw new FileAdderException("Error Occured.");
      }
      finally
      {
        if (AnalyticalFileAdder.dbcon != null)
        {
          if (AnalyticalFileAdder.dbcon.IsOpened)
            AnalyticalFileAdder.dbcon.Close();
          AnalyticalFileAdder.dbcon.Dispose();
        }
        AnalyticalFileAdder.tg = (TagInfo) null;
      }
    }

    private static void InsertSong(SongInfo sngI)
    {
      if (AnalyticalFileAdder.GetSongID(sngI.sngName, sngI.artist) != 0)
        return;
      OleDbCommand oleDbCommand = new OleDbCommand("INSERT INTO songs(song_name,artist,album,length,bit_rate,genre,song_year,disk,MIME_type,location,TimeString,checked) VALUES(@sngn,@ar,@al,@len,@brt,@gnr,@year,@dsk,@mtype,@loc,@tims,@chkd)", AnalyticalFileAdder.dbcon.oledbconn);
      oleDbCommand.Parameters.AddWithValue("@sngn", (object) sngI.sngName);
      oleDbCommand.Parameters.AddWithValue("@ar", (object) sngI.artist);
      oleDbCommand.Parameters.AddWithValue("@al", (object) sngI.album);
      oleDbCommand.Parameters.AddWithValue("@len", (object) sngI.length);
      oleDbCommand.Parameters.AddWithValue("@brt", (object) sngI.bitrate);
      oleDbCommand.Parameters.AddWithValue("@gnr", (object) sngI.genre);
      oleDbCommand.Parameters.AddWithValue("@year", (object) sngI.year);
      oleDbCommand.Parameters.AddWithValue("@dsk", (object) sngI.disk);
      oleDbCommand.Parameters.AddWithValue("@mtype", (object) sngI.MIME);
      oleDbCommand.Parameters.AddWithValue("@loc", (object) sngI.location);
      oleDbCommand.Parameters.AddWithValue("@tims", (object) AnalyticalFileAdder.ToTimeString(sngI.length));
      oleDbCommand.Parameters.AddWithValue("@chkd", (object) 0);
      oleDbCommand.ExecuteNonQuery();
    }

    private static void InsertArtist(string arName)
    {
      if (AnalyticalFileAdder.GetArtistID(arName) != 0)
        return;
      OleDbCommand oleDbCommand = new OleDbCommand("INSERT INTO Artist(artist_name,checked) VALUES(@arn,@chkd)", AnalyticalFileAdder.dbcon.oledbconn);
      oleDbCommand.Parameters.AddWithValue("@arn", (object) arName);
      oleDbCommand.Parameters.AddWithValue("@chkd", (object) 0);
      oleDbCommand.ExecuteNonQuery();
    }

    private static void InsertAlbum(string alName, string arName)
    {
      if (AnalyticalFileAdder.GetAlbumID(alName, arName) != 0)
        return;
      OleDbCommand oleDbCommand = new OleDbCommand("INSERT INTO Album(album_name,artist,checked) VALUES(@aln,@artist,@chkd)", AnalyticalFileAdder.dbcon.oledbconn);
      oleDbCommand.Parameters.AddWithValue("@aln", (object) alName);
      oleDbCommand.Parameters.AddWithValue("@artist", (object) arName);
      oleDbCommand.Parameters.AddWithValue("@chkd", (object) 0);
      oleDbCommand.ExecuteNonQuery();
    }

    private static int GetSongID(string sName, string arName)
    {
      OleDbCommand oleDbCommand = new OleDbCommand("SELECT song_ID FROM songs WHERE song_name=@snam AND artist=@arnam", AnalyticalFileAdder.dbcon.oledbconn);
      oleDbCommand.Parameters.AddWithValue("@snam", (object) sName);
      oleDbCommand.Parameters.AddWithValue("@arnam", (object) arName);
      object obj = oleDbCommand.ExecuteScalar();
      if (obj != null)
        return (int) obj;
      return 0;
    }

    private static int GetAlbumID(string alName, string arName)
    {
      OleDbCommand oleDbCommand = new OleDbCommand("SELECT album_ID FROM Album WHERE album_name=@aln AND artist=@arnam", AnalyticalFileAdder.dbcon.oledbconn);
      oleDbCommand.Parameters.AddWithValue("@aln", (object) alName);
      oleDbCommand.Parameters.AddWithValue("@arnam", (object) arName);
      object obj = oleDbCommand.ExecuteScalar();
      if (obj != null)
        return (int) obj;
      return 0;
    }

    private static int GetArtistID(string arName)
    {
      OleDbCommand oleDbCommand = new OleDbCommand("SELECT artist_ID FROM Artist WHERE artist_name=@arn", AnalyticalFileAdder.dbcon.oledbconn);
      oleDbCommand.Parameters.AddWithValue("@arn", (object) arName);
      object obj = oleDbCommand.ExecuteScalar();
      if (obj != null)
        return (int) obj;
      return 0;
    }

    private static string ToTimeString(double length)
    {
      int num1 = (int) length / 60;
      int num2 = num1 / 60;
      if (num1 >= 60)
        num1 = 0;
      int num3 = (int) length % 60;
      if (num3 >= 60)
        num3 = 0;
      if (num2 <= 0)
        return num1.ToString() + ":" + num3.ToString("00");
      return num2.ToString() + ":" + num1.ToString("00") + ":" + num3.ToString("00");
    }
  }
}
