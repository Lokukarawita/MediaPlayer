// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.RadioDeskPlaylist
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System;
using System.Collections.Generic;

namespace RadioDesk.Core
{
  public sealed class RadioDeskPlaylist
  {
    private List<string> itms;
    private int cur_count;
    private int cur_pos;
    private PlaylistMode cur_mode;
    private string cur_itm;

    public event PlaylistLoadSave PlaylistSaved;

    public event PlaylistLoadSave PLaylistLoaded;

    public RadioDeskPlaylist()
    {
      this.itms = new List<string>();
      this.cur_count = this.itms.Count;
      this.cur_pos = 0;
      this.cur_mode = PlaylistMode.Normal;
    }

    private void CheckForErrors()
    {
      if (this.itms == null)
        throw new PlaylistException("Playlist is empty");
      if (this.cur_count == 0 || this.cur_count == -1)
        throw new PlaylistException("Playlist is empty");
    }

    public string GetRandomItem(int minPosotion, int maxPosition)
    {
      Random random = new Random();
      if (minPosotion < 0)
        minPosotion = 0;
      if (maxPosition > this.cur_count - 1)
        maxPosition = this.cur_count;
      int num = random.Next(minPosotion, maxPosition);
      if (this.cur_pos == num)
        num = random.Next(0, this.cur_count);
      this.cur_pos = num;
      return this.itms[this.cur_pos];
    }

    public string GetRandomItem()
    {
      Random random = new Random();
      int num = random.Next(0, this.cur_count);
      if (this.cur_pos == num)
        num = random.Next(0, this.cur_count);
      this.cur_pos = num;
      return this.itms[this.cur_pos];
    }

    public string GetItem(int position)
    {
      this.CheckForErrors();
      if (this.cur_mode != PlaylistMode.Normal)
        return this.GetRandomItem();
      string itm;
      if (position >= this.cur_count)
      {
        this.cur_pos = 0;
        itm = this.itms[this.cur_pos];
      }
      else if (position < 0)
      {
        this.cur_pos = this.cur_count - 1;
        itm = this.itms[this.cur_pos];
      }
      else
      {
        this.cur_pos = position;
        itm = this.itms[this.cur_pos];
      }
      return itm;
    }

    public string GetItem(PlaylistItemDirection direction)
    {
      this.CheckForErrors();
      if (this.cur_mode == PlaylistMode.Shuffled)
      {
        if (direction == PlaylistItemDirection.Previous)
          return this.GetRandomItem(0, this.cur_pos);
        if (direction == PlaylistItemDirection.Next)
          return this.GetRandomItem(this.cur_pos, this.cur_count - 1);
        return this.GetRandomItem();
      }
      switch (direction)
      {
        case PlaylistItemDirection.Previous:
          this.cur_pos = this.cur_pos - 1 <= 0 ? this.cur_count - 1 : this.cur_pos - 1;
          return this.GetItem(this.cur_pos);
        case PlaylistItemDirection.Current:
          return this.itms[this.cur_pos];
        case PlaylistItemDirection.Next:
          this.cur_pos = this.cur_pos + 1 >= this.cur_count ? 0 : this.cur_pos + 1;
          return this.GetItem(this.cur_pos);
        default:
          return this.GetRandomItem();
      }
    }

    public string GetFirst()
    {
      this.CheckForErrors();
      this.cur_pos = 0;
      this.cur_itm = this.itms[this.cur_pos];
      return this.itms[this.cur_pos];
    }

    public string GetLast()
    {
      this.CheckForErrors();
      this.cur_pos = this.cur_count - 1;
      this.cur_itm = this.itms[this.cur_pos];
      return this.itms[this.cur_pos];
    }

    public string[] GetAllItems()
    {
      if (this.itms.Count > 0)
        return this.itms.ToArray();
      return (string[]) null;
    }

    public bool Add(string[] paths)
    {
      try
      {
        foreach (string path in paths)
          this.itms.Add(path);
        this.cur_count = this.itms.Count;
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool Add(string path)
    {
      try
      {
        this.itms.Add(path);
        this.cur_count = this.itms.Count;
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool Remove(string path)
    {
      try
      {
        this.itms.Remove(path);
        this.cur_count = this.itms.Count;
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public bool Remove(string[] paths)
    {
      try
      {
        foreach (string path in paths)
        {
          this.itms.Remove(path);
          this.cur_count = this.itms.Count;
          if (this.cur_pos > this.cur_count - 1)
            this.cur_pos = this.cur_count - 1;
        }
        return true;
      }
      catch (Exception ex)
      {
        return false;
      }
    }

    public void RemoveAll()
    {
      this.itms.Clear();
      this.cur_pos = 0;
      this.cur_count = this.itms.Count;
    }

    public void Save(string path, PlaylistType typ)
    {
      PlaylistWriter playlistWriter = new PlaylistWriter(path, typ);
      playlistWriter.Write(this.itms.ToArray());
      playlistWriter.Close();
      if (this.PlaylistSaved == null)
        return;
      this.PlaylistSaved((object) this, new EventArgs());
    }

    public void Load(string path, bool AppendToCurrent)
    {
      try
      {
        string[] strArray = new PlaylistReader(path).Read();
        if (AppendToCurrent)
        {
          foreach (string str in strArray)
            this.itms.Add(str);
        }
        else
        {
          this.RemoveAll();
          foreach (string str in strArray)
            this.itms.Add(str);
        }
        this.cur_count = this.itms.Count;
        if (this.PLaylistLoaded == null)
          return;
        this.PLaylistLoaded((object) this, new EventArgs());
      }
      catch (PlaylistReaderException ex)
      {
        throw new PlaylistException(ex.Message);
      }
    }

    public int TotalItems
    {
      get
      {
        return this.cur_count;
      }
    }

    public int CurrentPosition
    {
      get
      {
        return this.cur_pos;
      }
    }

    public string CurrentItem
    {
      get
      {
        this.CheckForErrors();
        return this.itms[this.cur_pos];
      }
    }

    public PlaylistMode Mode
    {
      get
      {
        return this.cur_mode;
      }
      set
      {
        this.cur_mode = value;
      }
    }

    public bool IsEmpty
    {
      get
      {
        return this.itms.Count <= 0;
      }
    }

    public bool IsAtBegining
    {
      get
      {
        return !this.IsEmpty && this.CurrentPosition == 0;
      }
    }

    public bool IsAtEnd
    {
      get
      {
        return !this.IsEmpty && this.CurrentPosition == this.cur_count - 1;
      }
    }
  }
}
