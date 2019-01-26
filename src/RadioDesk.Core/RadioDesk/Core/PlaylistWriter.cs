// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.PlaylistWriter
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System;
using System.IO;
using System.Text;

namespace RadioDesk.Core
{
  public class PlaylistWriter
  {
    private int count = 1;
    private bool finished = false;
    private StreamWriter my_writer;
    private string path;
    private Encoding enc;
    private PlaylistType cur_type;

    public PlaylistWriter(string path, PlaylistType type)
    {
      this.path = path;
      this.cur_type = type;
      if (type == PlaylistType.M3UUnicode)
      {
        this.enc = Encoding.UTF8;
        this.my_writer = new StreamWriter(this.path, false, this.enc);
        this.my_writer.WriteLine("#EXTM3U");
      }
      else if (type == PlaylistType.M3U)
      {
        this.enc = Encoding.Default;
        this.my_writer = new StreamWriter(this.path, false, this.enc);
        this.my_writer.WriteLine("#EXTM3U");
      }
      else
      {
        this.enc = Encoding.Default;
        this.my_writer = new StreamWriter(this.path, false, this.enc);
        this.my_writer.WriteLine("[playlist]");
      }
    }

    ~PlaylistWriter()
    {
      if (this.finished)
        return;
      if (this.my_writer != null)
        this.my_writer.Close();
      if (File.Exists(this.path))
        File.Delete(this.path);
    }

    private void WriteM3ULine(string path)
    {
      TagInfo tagInfo = new TagInfo(path);
      string str1 = "#EXTINF:" + ((int) tagInfo.Duration.TotalSeconds).ToString() + "," + tagInfo.Artist + " - " + tagInfo.Title;
      string str2 = path;
      this.my_writer.WriteLine(str1);
      this.my_writer.WriteLine(str2);
      this.my_writer.Flush();
    }

    private void WritePLSLine(string path)
    {
      TagInfo tagInfo = new TagInfo(path);
      string artist = tagInfo.Artist;
      string title = tagInfo.Title;
      int totalSeconds = (int) tagInfo.Duration.TotalSeconds;
      string str1 = "File" + (object) this.count + "=" + path;
      string str2 = "Title" + (object) this.count + "=" + artist + " - " + title;
      string str3 = "Length" + (object) this.count + "=" + totalSeconds.ToString();
      ++this.count;
      this.my_writer.WriteLine();
      this.my_writer.WriteLine(str1);
      this.my_writer.WriteLine();
      this.my_writer.WriteLine(str2);
      this.my_writer.WriteLine();
      this.my_writer.WriteLine(str3);
      this.my_writer.Flush();
    }

    private void WritePLSFooter()
    {
      string str1 = "NumberOfEntries=" + (object) this.count;
      string str2 = "Version=2";
      this.my_writer.WriteLine();
      this.my_writer.WriteLine(str1);
      this.my_writer.WriteLine();
      this.my_writer.WriteLine(str2);
    }

    public bool Write(string path)
    {
      try
      {
        if (this.cur_type == PlaylistType.PLS)
          this.WritePLSLine(path);
        else
          this.WriteM3ULine(path);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool Write(string[] path)
    {
      try
      {
        foreach (string path1 in path)
          this.Write(path1);
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool Close()
    {
      try
      {
        if (this.cur_type == PlaylistType.PLS)
          this.WritePLSFooter();
        this.my_writer.Flush();
        this.my_writer.Close();
        this.finished = true;
        this.count = 1;
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }
  }
}
