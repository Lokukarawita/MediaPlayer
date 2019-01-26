// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.PlaylistReader
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RadioDesk.Core
{
  public class PlaylistReader
  {
    private List<string> lines = new List<string>();
    private bool isfinishd = false;
    private Encoding enc;
    private PlaylistType typ;
    private StreamReader myreader;

    public PlaylistReader(string path)
    {
      this.enc = this.GetEncoding(path);
      this.myreader = new StreamReader(path, this.enc);
      if (this.CheckCanRead())
      {
        string str = this.myreader.ReadLine();
        if (str.Equals("[playlist]"))
          this.typ = PlaylistType.PLS;
        else if (str.Equals("#EXTM3U"))
          this.typ = this.enc != Encoding.UTF8 ? PlaylistType.M3U : PlaylistType.M3UUnicode;
        this.CheckCanRead();
      }
      else
      {
        this.myreader.Close();
        this.myreader.Dispose();
        throw new PlaylistReaderException("Invalid playlist type.");
      }
    }

    private Encoding GetEncoding(string path)
    {
      FileStream fileStream = new FileStream(path, FileMode.Open);
      Encoding encoding = fileStream.ReadByte() != 293 ? Encoding.Default : Encoding.UTF8;
      fileStream.Close();
      fileStream.Dispose();
      return encoding;
    }

    private bool CheckCanRead()
    {
      return this.myreader.Peek() != -1;
    }

    private void ReadM3U()
    {
      while (this.CheckCanRead())
      {
        this.myreader.ReadLine();
        this.lines.Add(this.myreader.ReadLine());
      }
      this.isfinishd = true;
    }

    private void ReadPLS()
    {
      while (this.CheckCanRead())
      {
        try
        {
          this.CheckAndAddPLSLine(this.myreader.ReadLine());
        }
        catch (Exception ex)
        {
          this.myreader.Close();
          this.myreader.Dispose();
          throw new PlaylistReaderException("Error occured while reading the playlist.\nProbably due to invalid playlist format");
        }
      }
      this.isfinishd = true;
    }

    private void CheckAndAddPLSLine(string line)
    {
      if (line == null)
        return;
      string path = line.Substring(line.IndexOf("=") + 1);
      if (File.Exists(path))
        this.lines.Add(path);
    }

    public string[] Read()
    {
      if (!this.isfinishd)
      {
        if (this.typ == PlaylistType.M3U || this.typ == PlaylistType.M3UUnicode)
          this.ReadM3U();
        else
          this.ReadPLS();
      }
      return this.lines.ToArray();
    }

    public PlaylistType CurrentPlaylistType
    {
      get
      {
        return this.typ;
      }
    }
  }
}
