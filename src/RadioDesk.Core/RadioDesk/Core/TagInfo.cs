// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.TagInfo
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using TagLib;

namespace RadioDesk.Core
{
  public sealed class TagInfo
  {
    private string filPath;
    private Properties baseprop;
    private Tag basetag;
    private TagLib.File basefile;

    public TagInfo()
    {
      this.filPath = (string) null;
      this.basefile = (TagLib.File) null;
      this.basetag = (Tag) null;
      this.baseprop = (Properties) null;
    }

    public TagInfo(string path)
    {
      try
      {
        this.filPath = path;
        this.basefile = TagLib.File.Create(path);
        this.basetag = this.basefile.Tag;
        this.baseprop = this.basefile.Properties;
      }
      catch (Exception ex)
      {
        throw new TagReadException("Error occured while reading the tag informaton.");
      }
    }

    private static string FromStringArray(string[] property)
    {
      if (property.Length > 0)
        return property[0].ToString();
      return "";
    }

    private static string FromStringArray(string[] property, int index)
    {
      if (property.Length <= 0)
        return "";
      if (property.Length > index)
        return property[index].ToString();
      return property[0].ToString();
    }

    private static Image FromImageArray(TagLib.IPicture[] covers)
    {
      try
      {
        if (covers.Length > 0)
          return Image.FromStream((Stream) new MemoryStream(covers[0].Data.Data));
        return (Image) null;
      }
      catch (Exception ex)
      {
        return (Image) null;
      }
    }

    private static Image FromImageArray(TagLib.IPicture[] covers, int index)
    {
      try
      {
        if (covers.Length <= 0)
          return (Image) null;
        if (covers.Length > index)
          return Image.FromStream((Stream) new MemoryStream(covers[index].Data.Data));
        return Image.FromStream((Stream) new MemoryStream(covers[0].Data.Data));
      }
      catch (Exception ex)
      {
        return (Image) null;
      }
    }

    private static string[] AddValueToStringArray(string[] ary, string val)
    {
      if (ary.Length == 0)
        return new List<string>() { val }.ToArray();
      ary[0] = val;
      return ary;
    }

    public string File
    {
      get
      {
        return this.filPath;
      }
      set
      {
        if (!System.IO.File.Exists(value))
          return;
        try
        {
          this.filPath = value;
          this.basefile = TagLib.File.Create(value);
          this.basetag = this.basefile.Tag;
          this.baseprop = this.basefile.Properties;
        }
        catch (Exception ex)
        {
          this.basefile = (TagLib.File) null;
          this.basetag = (Tag) null;
          throw new TagReadException("Error occured while reading the tag informaton.");
        }
      }
    }

    public int BitRate
    {
      get
      {
        if (this.baseprop != null)
          return this.baseprop.AudioBitrate;
        return 0;
      }
    }

    public int Channels
    {
      get
      {
        if (this.baseprop != null)
          return this.baseprop.AudioChannels;
        return 0;
      }
    }

    public int SampleRate
    {
      get
      {
        if (this.baseprop != null)
          return this.baseprop.AudioSampleRate;
        return 0;
      }
    }

    public string Codec
    {
      get
      {
        if (this.baseprop != null)
          return ((ICodec[]) this.baseprop.Codecs)[0].Description;
        return "Unknown";
      }
    }

    public int BitsPerSample
    {
      get
      {
        if (this.baseprop != null)
          return this.baseprop.BitsPerSample;
        return 0;
      }
    }

    public string MIMEType
    {
      get
      {
        return this.basefile.MimeType;
      }
    }

    public TimeSpan Duration
    {
      get
      {
        return this.baseprop.Duration;
      }
    }

    public string Artist
    {
      get
      {
        return this.basetag.FirstPerformer;
      }
      set
      {
        this.basetag.Performers = TagInfo.AddValueToStringArray(this.basetag.Performers, value);
      }
    }

    public string Album
    {
      get
      {
        return this.basetag.Album;
      }
      set
      {
        this.basetag.Album = value;
      }
    }

    public string Title
    {
      get
      {
        return this.basetag.Title;
      }
      set
      {
        this.basetag.Title = value;
      }
    }

    public string AlbumArtist
    {
      get
      {
        return this.basetag.FirstAlbumArtist;
      }
      set
      {
        this.basetag.AlbumArtists = TagInfo.AddValueToStringArray(this.basetag.AlbumArtists, value);
      }
    }

    public uint Year
    {
      get
      {
        return this.basetag.Year;
      }
      set
      {
        this.basetag.Year = value;
      }
    }

    public uint Track
    {
      get
      {
        return this.basetag.Track;
      }
      set
      {
        this.basetag.Track = value;
      }
    }

    public uint Disk
    {
      get
      {
        return this.basetag.Disc;
      }
      set
      {
        this.basetag.Disc = value;
      }
    }

    public string Comment
    {
      get
      {
        return this.basetag.Comment;
      }
      set
      {
        this.basetag.Comment = value;
      }
    }

    public string Geners
    {
      get
      {
        return TagInfo.FromStringArray(this.basetag.Genres);
      }
      set
      {
        this.basetag.Genres = TagInfo.AddValueToStringArray(this.basetag.Genres, value);
      }
    }

    public string Composers
    {
      get
      {
        return TagInfo.FromStringArray(this.basetag.Composers);
      }
      set
      {
        this.basetag.Composers = TagInfo.AddValueToStringArray(this.basetag.Composers, value);
      }
    }

    public string Lyrics
    {
      get
      {
        return this.basetag.Lyrics;
      }
      set
      {
        this.basetag.Lyrics = value;
      }
    }

    public Image FrontCover
    {
      get
      {
        return TagInfo.FromImageArray(this.basetag.Pictures);
      }
      set
      {
        MemoryStream memoryStream = new MemoryStream();
        value.Save((Stream) memoryStream, ImageFormat.Jpeg);
        this.basetag.Pictures = new TagLib.IPicture[1]
        {
          (TagLib.IPicture) new Picture(new ByteVector(memoryStream.ToArray()))
        };
      }
    }

    public bool Save()
    {
      try
      {
        this.basefile.Save();
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
