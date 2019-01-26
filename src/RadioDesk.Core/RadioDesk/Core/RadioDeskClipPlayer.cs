// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.RadioDeskClipPlayer
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using libZPlay;

namespace RadioDesk.Core
{
  public sealed class RadioDeskClipPlayer
  {
    private string _fpath;
    private bool _keeploaded;
    private ZPlay player;
    private TStreamStatus stream_status;

    public RadioDeskClipPlayer()
    {
      this.player = new ZPlay();
      this.stream_status = new TStreamStatus();
    }

    public void Load(string path, bool autoplay, bool keeploaded)
    {
      this._fpath = path;
      this._keeploaded = keeploaded;
      if (this.player != null)
      {
        this.player.GetStatus(ref this.stream_status);
        if (this.stream_status.fPlay || this.stream_status.fPause)
          this.player.Close();
      }
      if (!this.player.OpenFile(this._fpath, TStreamFormat.sfAutodetect))
        throw new MediaPlaybackException(this.player.GetError());
      if (!autoplay)
        return;
      this.player.StartPlayback();
    }

    public void Play()
    {
      this.player.GetStatus(ref this.stream_status);
      if (this.stream_status.fPlay && this.stream_status.fPause)
        return;
      TStreamTime Position = new TStreamTime();
      this.player.GetStatus(ref this.stream_status);
      if (this.stream_status.fPause)
      {
        Position.sec = 0U;
        this.player.Seek(TTimeFormat.tfSecond, ref Position, TSeekMethod.smFromBeginning);
      }
      this.player.StartPlayback();
    }

    public void Stop()
    {
      this.player.GetStatus(ref this.stream_status);
      if (!this.stream_status.fPlay)
        return;
      this.player.PausePlayback();
    }

    public int Volume
    {
      get
      {
        int LeftVolume = 0;
        int RightVolume = 0;
        this.player.GetPlayerVolume(ref LeftVolume, ref RightVolume);
        return LeftVolume >= RightVolume ? LeftVolume : RightVolume;
      }
      set
      {
        if (value > 100)
          value = 100;
        else if (value < 0)
          value = 0;
        this.player.SetPlayerVolume(value, value);
      }
    }

    public bool IsLoaded
    {
      get
      {
        return this._fpath != null;
      }
    }

    public bool IsStopped
    {
      get
      {
        this.player.GetStatus(ref this.stream_status);
        return !this.stream_status.fPlay;
      }
    }
  }
}
