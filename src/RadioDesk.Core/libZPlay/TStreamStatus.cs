// Decompiled with JetBrains decompiler
// Type: libZPlay.TStreamStatus
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System.Runtime.InteropServices;

namespace libZPlay
{
  [StructLayout(LayoutKind.Explicit)]
  public struct TStreamStatus
  {
    [FieldOffset(0)]
    public bool fPlay;
    [FieldOffset(4)]
    public bool fPause;
    [FieldOffset(8)]
    public bool fEcho;
    [FieldOffset(12)]
    public bool fEqualizer;
    [FieldOffset(16)]
    public bool fVocalCut;
    [FieldOffset(20)]
    public bool fSideCut;
    [FieldOffset(24)]
    public bool fChannelMix;
    [FieldOffset(28)]
    public bool fSlideVolume;
    [FieldOffset(32)]
    public int nLoop;
    [FieldOffset(36)]
    public bool fReverse;
    [FieldOffset(40)]
    public int nSongIndex;
    [FieldOffset(44)]
    public int nSongsInQueue;
  }
}
