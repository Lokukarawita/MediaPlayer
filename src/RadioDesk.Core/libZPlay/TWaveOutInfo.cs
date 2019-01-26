// Decompiled with JetBrains decompiler
// Type: libZPlay.TWaveOutInfo
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System.Runtime.InteropServices;

namespace libZPlay
{
  [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
  public struct TWaveOutInfo
  {
    [FieldOffset(0)]
    public uint ManufacturerID;
    [FieldOffset(4)]
    public uint ProductID;
    [FieldOffset(8)]
    public uint DriverVersion;
    [FieldOffset(12)]
    public uint Formats;
    [FieldOffset(16)]
    public uint Channels;
    [FieldOffset(20)]
    public uint Support;
    [FieldOffset(24)]
    public string ProductName;
  }
}
