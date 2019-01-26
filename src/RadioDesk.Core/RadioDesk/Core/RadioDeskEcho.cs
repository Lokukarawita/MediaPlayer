// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.RadioDeskEcho
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using libZPlay;
using System.Reflection;

namespace RadioDesk.Core
{
  public sealed class RadioDeskEcho
  {
    private ZPlay libzply_obj;
    private TStreamStatus stream_status;
    private TEchoEffect[] t_echoeffect;

    public RadioDeskEcho(RadioDeskPlayer ply)
    {
      this.libzply_obj = (ZPlay) ply.GetType().GetField("player", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) ply);
      this.t_echoeffect = new TEchoEffect[2];
      this.t_echoeffect[0].nLeftDelay = 2000;
      this.t_echoeffect[0].nLeftSrcVolume = 50;
      this.t_echoeffect[0].nLeftEchoVolume = 30;
      this.t_echoeffect[0].nRightDelay = 2000;
      this.t_echoeffect[0].nRightSrcVolume = 50;
      this.t_echoeffect[0].nRightEchoVolume = 30;
      this.t_echoeffect[1].nLeftDelay = 30;
      this.t_echoeffect[1].nLeftSrcVolume = 50;
      this.t_echoeffect[1].nLeftEchoVolume = 30;
      this.t_echoeffect[1].nRightDelay = 30;
      this.t_echoeffect[1].nRightSrcVolume = 50;
      this.t_echoeffect[1].nRightEchoVolume = 30;
    }

    private void GetCurrentEffects()
    {
      this.libzply_obj.GetEchoParam(ref this.t_echoeffect);
    }

    private void SetCurrentEffects()
    {
      this.libzply_obj.SetEchoParam(ref this.t_echoeffect, 20);
    }

    public bool Enabled
    {
      get
      {
        this.libzply_obj.GetStatus(ref this.stream_status);
        return this.stream_status.fEcho;
      }
      set
      {
        this.libzply_obj.EnableEcho(value);
      }
    }

    public int FrontLeftDelay
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[0].nLeftDelay;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[0].nLeftDelay = value;
        this.SetCurrentEffects();
      }
    }

    public int FrontRightDelay
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[0].nRightDelay;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[0].nRightDelay = value;
        this.SetCurrentEffects();
      }
    }

    public int FrontLeftEchoVolume
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[0].nLeftEchoVolume;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[0].nLeftEchoVolume = value;
        this.SetCurrentEffects();
      }
    }

    public int FrontRightEchoVolume
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[0].nRightEchoVolume;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[0].nRightEchoVolume = value;
        this.SetCurrentEffects();
      }
    }

    public int FrontLeftSourceVolume
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[0].nLeftSrcVolume;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[0].nLeftSrcVolume = value;
        this.SetCurrentEffects();
      }
    }

    public int FrontRightSourceVolume
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[0].nRightSrcVolume;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[0].nRightSrcVolume = value;
        this.SetCurrentEffects();
      }
    }

    public int BackLeftDelay
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[1].nLeftDelay;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[1].nLeftDelay = value;
        this.SetCurrentEffects();
      }
    }

    public int BackRightDelay
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[1].nRightDelay;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[1].nRightDelay = value;
        this.SetCurrentEffects();
      }
    }

    public int BackLeftEchoVolume
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[1].nLeftEchoVolume;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[1].nLeftEchoVolume = value;
        this.SetCurrentEffects();
      }
    }

    public int BackRightEchoVolume
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[1].nRightEchoVolume;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[1].nRightEchoVolume = value;
        this.SetCurrentEffects();
      }
    }

    public int BackLeftSourceVolume
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[1].nLeftSrcVolume;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[1].nLeftSrcVolume = value;
        this.SetCurrentEffects();
      }
    }

    public int BackRightSourceVolume
    {
      get
      {
        this.GetCurrentEffects();
        return this.t_echoeffect[1].nRightSrcVolume;
      }
      set
      {
        this.GetCurrentEffects();
        this.t_echoeffect[1].nRightSrcVolume = value;
        this.SetCurrentEffects();
      }
    }
  }
}
