// Decompiled with JetBrains decompiler
// Type: MediaPlayerFinal.ExtendedQuerying
// Assembly: MediaPlayerFinal, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: FA266A67-626E-46F0-81D3-A2E3F3CE1F98
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\MediaPlayerFinal.exe

using System;
using System.Data;
using System.Data.OleDb;
using System.IO;

namespace MediaPlayerFinal
{
  public static class ExtendedQuerying
  {
    private static DBConnection dbcon = new DBConnection(Path.Combine(AppDomain.CurrentDomain.BaseDirectory.ToString(), "media_Database.accdb"));

    public static DataSet SearchByArtist(string SearchPhrase)
    {
      string SQL;
      if (SearchPhrase == "*" || SearchPhrase == "'")
      {
        SQL = "SELECT * FROM songs Order By Artist, song_name";
      }
      else
      {
        SearchPhrase = SearchPhrase.Replace("'", "''");
        SQL = "SELECT * FROM songs WHERE artist LIKE '%" + SearchPhrase + "%' Order By Artist, song_name";
      }
      return ExtendedQuerying.ExecuteQuery(SQL, "Artist");
    }

    public static DataSet SearchByTitle(string SearchPhrase)
    {
      string SQL;
      if (SearchPhrase == "*")
      {
        SQL = "SELECT * FROM songs Order By song_name, Artist";
      }
      else
      {
        SearchPhrase = SearchPhrase.Replace("'", "''");
        SQL = "SELECT * FROM songs WHERE song_name LIKE '%" + SearchPhrase + "%' Order By song_name, Artist";
      }
      return ExtendedQuerying.ExecuteQuery(SQL, "songs");
    }

    public static DataSet SearchByAlbum(string SearchPhrase)
    {
      string SQL;
      if (SearchPhrase == "*")
      {
        SQL = "SELECT * FROM songs Order By album, song_name";
      }
      else
      {
        SearchPhrase = SearchPhrase.Replace("'", "''");
        SQL = "SELECT * FROM songs WHERE album LIKE '%" + SearchPhrase + "%' Order By album, song_name";
      }
      return ExtendedQuerying.ExecuteQuery(SQL, "songs");
    }

    public static DataSet SearchByYear(string SearchPhrase)
    {
      string SQL;
      if (SearchPhrase == "*")
      {
        SQL = "SELECT * FROM songs Order By song_year, song_name";
      }
      else
      {
        SearchPhrase = SearchPhrase.Replace("'", "''");
        SQL = "SELECT * FROM songs WHERE song_year LIKE '%" + SearchPhrase + "%' Order By song_year, song_name";
      }
      return ExtendedQuerying.ExecuteQuery(SQL, "songs");
    }

    public static DataSet GetTrackDetails(string track, string artist)
    {
      track = track.Replace("'", "''");
      artist = artist.Replace("'", "''");
      return ExtendedQuerying.ExecuteQuery("SELECT * FROM songs WHERE song_name='" + track + "' AND artist='" + artist + "'", "Track");
    }

    public static DataSet GetArtistDetails(string artist)
    {
      artist = artist.Replace("'", "''");
      return ExtendedQuerying.ExecuteQuery("SELECT * FROM artist WHERE artist_name='" + artist + "'", "Artist");
    }

    public static DataSet GetAlbumDetails(string album, string artist)
    {
      album = album.Replace("'", "''");
      artist = artist.Replace("'", "''");
      return ExtendedQuerying.ExecuteQuery("SELECT * FROM Album WHERE album_name='" + album + "' AND artist='" + artist + "'", "Album");
    }

    public static DataSet GetTracks(string artist)
    {
      artist = artist.Replace("'", "''");
      return ExtendedQuerying.ExecuteQuery("SELECT (artist + ' - ' + song_name) AS DisplayView,* FROM songs WHERE artist='" + artist + "' ORDER BY song_name", "songs");
    }

    public static DataSet GetTracks(string artist, string album)
    {
      artist = artist.Replace("'", "''");
      album = album.Replace("'", "''");
      return ExtendedQuerying.ExecuteQuery("SELECT (artist + ' - ' + song_name) AS DisplayView,* FROM songs WHERE artist='" + artist + "' AND Album='" + album + "' ORDER BY song_name", "songs");
    }

    public static DataSet GetAllClips()
    {
      return ExtendedQuerying.ExecuteQuery("SELECT title, location FROM Clips", "Clips");
    }

    public static DataSet GetAllAds()
    {
      return ExtendedQuerying.ExecuteQuery("SELECT adv_name, location FROM Advertisment", "Ads");
    }

    private static DataSet ExecuteQuery(string SQL, string tableName)
    {
      DataSet dataSet = new DataSet();
      OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(new OleDbCommand(SQL, ExtendedQuerying.dbcon.oledbconn));
      if (!ExtendedQuerying.dbcon.IsOpened)
        ExtendedQuerying.dbcon.Open();
      oleDbDataAdapter.Fill(dataSet, tableName);
      ExtendedQuerying.dbcon.Close();
      return dataSet;
    }
  }
}
