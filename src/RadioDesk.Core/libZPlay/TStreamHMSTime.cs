// Decompiled with JetBrains decompiler
// Type: libZPlay.TStreamHMSTime
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System.Runtime.InteropServices;

namespace libZPlay
{
  [StructLayout(LayoutKind.Explicit)]
  public struct TStreamHMSTime
  {
    [FieldOffset(0)]
    public uint hour;
    [FieldOffset(4)]
    public uint minute;
    [FieldOffset(8)]
    public uint second;
    [FieldOffset(12)]
    public uint millisecond;
  }
}
