// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.RadioDeskMicControl
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using libZPlay;
using System;

namespace RadioDesk.Core
{
  public sealed class RadioDeskMicControl : IDisposable
  {
    private TStreamStatus stream_status = new TStreamStatus();
    private bool enabled = false;
    private ZPlay objzlib;
    private int invol;
    private string str;
    private int vol;

    public RadioDeskMicControl()
    {
      this.objzlib = new ZPlay();
      this.invol = 40;
      this.str = "wavein://src=mic;volume=" + this.invol.ToString() + ";";
      this.SetBasicEchoParameters();
      this.objzlib.EnableEcho(false);
      this.objzlib.OpenFile(this.str, TStreamFormat.sfWaveIn);
      this.objzlib.StartPlayback();
      this.objzlib.SetPlayerVolume(0, 0);
      this.vol = 40;
    }

    public RadioDeskMicControl(int InputVolume)
    {
      this.objzlib = new ZPlay();
      this.invol = InputVolume;
      this.str = "wavein://src=mic;volume=" + this.invol.ToString() + ";";
      this.SetBasicEchoParameters();
      this.objzlib.EnableEcho(false);
      this.objzlib.OpenFile(this.str, TStreamFormat.sfWaveIn);
      this.objzlib.StartPlayback();
      this.objzlib.SetPlayerVolume(0, 0);
      this.vol = 40;
    }

    private void SetBasicEchoParameters()
    {
      TEchoEffect[] EchoEffectArray = new TEchoEffect[2];
      EchoEffectArray[0].nLeftDelay = 500;
      EchoEffectArray[0].nLeftSrcVolume = 50;
      EchoEffectArray[0].nLeftEchoVolume = 30;
      EchoEffectArray[0].nRightDelay = 500;
      EchoEffectArray[0].nRightSrcVolume = 50;
      EchoEffectArray[0].nRightEchoVolume = 30;
      EchoEffectArray[1].nLeftDelay = 30;
      EchoEffectArray[1].nLeftSrcVolume = 50;
      EchoEffectArray[1].nLeftEchoVolume = 30;
      EchoEffectArray[1].nRightDelay = 30;
      EchoEffectArray[1].nRightSrcVolume = 50;
      EchoEffectArray[1].nRightEchoVolume = 30;
      this.objzlib.SetEchoParam(ref EchoEffectArray, 2);
    }

    private void SetEchoVolume(int level)
    {
      TEchoEffect[] EchoEffectArray = new TEchoEffect[2];
      this.objzlib.GetEchoParam(ref EchoEffectArray);
      EchoEffectArray[0].nLeftEchoVolume = level;
      EchoEffectArray[0].nRightEchoVolume = level;
      this.objzlib.SetEchoParam(ref EchoEffectArray, 2);
    }

    private int GetEchoVolume()
    {
      TEchoEffect[] EchoEffectArray = new TEchoEffect[2];
      this.objzlib.GetEchoParam(ref EchoEffectArray);
      return EchoEffectArray[0].nLeftEchoVolume;
    }

    private void SetEchoLevel(int level)
    {
      TEchoEffect[] EchoEffectArray = new TEchoEffect[2];
      this.objzlib.GetEchoParam(ref EchoEffectArray);
      EchoEffectArray[0].nLeftDelay = level;
      EchoEffectArray[0].nRightDelay = level;
      this.objzlib.SetEchoParam(ref EchoEffectArray, 2);
    }

    private int GetEchoLevel()
    {
      TEchoEffect[] EchoEffectArray = new TEchoEffect[2];
      this.objzlib.GetEchoParam(ref EchoEffectArray);
      return EchoEffectArray[0].nLeftDelay;
    }

    public void Enable()
    {
      if (this.enabled)
        return;
      this.objzlib.SetPlayerVolume(this.vol, this.vol);
      this.enabled = true;
    }

    public void Disable()
    {
      if (!this.enabled)
        return;
      int LeftVolume = 0;
      int RightVolume = 0;
      this.objzlib.GetPlayerVolume(ref LeftVolume, ref RightVolume);
      this.vol = LeftVolume;
      this.objzlib.SetPlayerVolume(0, 0);
      this.enabled = false;
    }

    public int Volume
    {
      get
      {
        return this.vol;
      }
      set
      {
        if (value > 100)
          value = 100;
        else if (value < 0)
          value = 0;
        this.vol = value;
        if (!this.enabled)
          return;
        this.objzlib.SetPlayerVolume(this.vol, this.vol);
      }
    }

    public bool EchoEnable
    {
      get
      {
        this.objzlib.GetStatus(ref this.stream_status);
        return this.stream_status.fEcho;
      }
      set
      {
        this.objzlib.EnableEcho(value);
      }
    }

    public int EchoLevel
    {
      get
      {
        return this.GetEchoLevel();
      }
      set
      {
        this.SetEchoLevel(value);
      }
    }

    public int EchoVolume
    {
      get
      {
        return this.GetEchoVolume();
      }
      set
      {
        this.SetEchoVolume(value);
      }
    }

    public int LeftVUMeterValue
    {
      get
      {
        int LeftChannel = 0;
        int RightChannel = 0;
        this.objzlib.GetVUData(ref LeftChannel, ref RightChannel);
        return LeftChannel;
      }
    }

    public int RightVUMeterValue
    {
      get
      {
        int LeftChannel = 0;
        int RightChannel = 0;
        this.objzlib.GetVUData(ref LeftChannel, ref RightChannel);
        return RightChannel;
      }
    }

    public bool IsEnabled
    {
      get
      {
        return this.enabled;
      }
    }

    public void Dispose()
    {
      if (this.objzlib != null)
      {
        this.objzlib.SetPlayerVolume(0, 0);
        this.objzlib.GetStatus(ref this.stream_status);
        if (this.stream_status.fPause || this.stream_status.fPlay)
          this.objzlib.StopPlayback();
      }
      this.objzlib.Close();
      this.objzlib = (ZPlay) null;
    }
  }
}
