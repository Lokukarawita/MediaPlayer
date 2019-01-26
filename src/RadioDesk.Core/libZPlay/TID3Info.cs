// Decompiled with JetBrains decompiler
// Type: libZPlay.TID3Info
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System.Runtime.InteropServices;

namespace libZPlay
{
  [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
  public struct TID3Info
  {
    [FieldOffset(0)]
    public string Title;
    [FieldOffset(4)]
    public string Artist;
    [FieldOffset(8)]
    public string Album;
    [FieldOffset(12)]
    public string Year;
    [FieldOffset(16)]
    public string Comment;
    [FieldOffset(20)]
    public string Track;
    [FieldOffset(24)]
    public string Genre;
  }
}
