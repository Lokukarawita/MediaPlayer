// Decompiled with JetBrains decompiler
// Type: libZPlay.TStreamInfo
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System.Runtime.InteropServices;

namespace libZPlay
{
  [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
  public struct TStreamInfo
  {
    [FieldOffset(0)]
    public int SamplingRate;
    [FieldOffset(4)]
    public int ChannelNumber;
    [FieldOffset(8)]
    public bool VBR;
    [FieldOffset(12)]
    public int Bitrate;
    [FieldOffset(16)]
    public TStreamTime Length;
    [FieldOffset(44)]
    public string Description;
  }
}
