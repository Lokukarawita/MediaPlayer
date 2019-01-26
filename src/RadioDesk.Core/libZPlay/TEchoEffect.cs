// Decompiled with JetBrains decompiler
// Type: libZPlay.TEchoEffect
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System.Runtime.InteropServices;

namespace libZPlay
{
  [StructLayout(LayoutKind.Explicit)]
  public struct TEchoEffect
  {
    [FieldOffset(0)]
    public int nLeftDelay;
    [FieldOffset(4)]
    public int nLeftSrcVolume;
    [FieldOffset(8)]
    public int nLeftEchoVolume;
    [FieldOffset(12)]
    public int nRightDelay;
    [FieldOffset(16)]
    public int nRightSrcVolume;
    [FieldOffset(20)]
    public int nRightEchoVolume;
  }
}
