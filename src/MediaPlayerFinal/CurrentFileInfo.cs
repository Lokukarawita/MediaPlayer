// Decompiled with JetBrains decompiler
// Type: MediaPlayerFinal.CurrentFileInfo
// Assembly: MediaPlayerFinal, Version=1.0.0.1, Culture=neutral, PublicKeyToken=null
// MVID: FA266A67-626E-46F0-81D3-A2E3F3CE1F98
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\MediaPlayerFinal.exe

namespace MediaPlayerFinal
{
  internal struct CurrentFileInfo
  {
    public string Artist;
    public string Title;
    public string Album;
    public string Year;
    public string Path;
    public int idx;
    public int repeat;
    public bool userStop;
    public bool shuffled;

    public void Reset()
    {
      this.Artist = (string) null;
      this.Title = (string) null;
      this.Album = (string) null;
      this.Year = (string) null;
      this.Path = (string) null;
      this.idx = -1;
    }
  }
}
