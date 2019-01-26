// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.RadioDeskFFTGraph
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using libZPlay;
using System;
using System.Reflection;

namespace RadioDesk.Core
{
  public sealed class RadioDeskFFTGraph
  {
    private int x = 0;
    private int y = 0;
    private int w = 0;
    private int h = 0;
    private bool isOnHDC = false;
    private IntPtr hand;
    private ZPlay zply_obj;

    public RadioDeskFFTGraph(RadioDeskPlayer ply)
    {
      this.zply_obj = (ZPlay) ply.GetType().GetField("player", BindingFlags.Instance | BindingFlags.NonPublic).GetValue((object) ply);
    }

    public FFTWindowStyle WindowStyle
    {
      get
      {
        return (FFTWindowStyle) this.zply_obj.GetFFTGraphParam(TFFTGraphParamID.gpWindow);
      }
      set
      {
        this.zply_obj.SetFFTGraphParam(TFFTGraphParamID.gpWindow, (int) value);
      }
    }

    public FFTScaleDistribution ScaleDistribution
    {
      get
      {
        return (FFTScaleDistribution) this.zply_obj.GetFFTGraphParam(TFFTGraphParamID.gpHorizontalScale);
      }
      set
      {
        this.zply_obj.SetFFTGraphParam(TFFTGraphParamID.gpHorizontalScale, (int) value);
      }
    }

    public FFTGraphStyle GraphStyle
    {
      get
      {
        return (FFTGraphStyle) this.zply_obj.GetFFTGraphParam(TFFTGraphParamID.gpGraphType);
      }
      set
      {
        this.zply_obj.SetFFTGraphParam(TFFTGraphParamID.gpGraphType, (int) value);
      }
    }

    public void SetControlToDraw(
      IntPtr Handle,
      int XPos,
      int YPos,
      int Width,
      int Height,
      bool OnHDC)
    {
      this.hand = Handle;
      this.x = XPos;
      this.y = YPos;
      this.w = Width;
      this.h = Height;
      this.isOnHDC = OnHDC;
    }

    public void Draw()
    {
      if (this.isOnHDC)
        this.zply_obj.DrawFFTGraphOnHDC(this.hand, this.x, this.y, this.w, this.h);
      else
        this.zply_obj.DrawFFTGraphOnHWND(this.hand, this.x, this.y, this.w, this.h);
    }

    public void ReleaseControl()
    {
      if (this.isOnHDC)
        this.zply_obj.DrawFFTGraphOnHDC(new IntPtr(), 0, 0, 0, 0);
      else
        this.zply_obj.DrawFFTGraphOnHWND(new IntPtr(), 0, 0, 0, 0);
    }
  }
}
