// Decompiled with JetBrains decompiler
// Type: libZPlay.TFFTWindow
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

namespace libZPlay
{
  public enum TFFTWindow
  {
    fwRectangular = 1,
    fwHamming = 2,
    fwHann = 3,
    fwCosine = 4,
    fwLanczos = 5,
    fwBartlett = 6,
    fwTriangular = 7,
    fwGauss = 8,
    fwBartlettHann = 9,
    fwBlackman = 10, // 0x0000000A
    fwNuttall = 11, // 0x0000000B
    fwBlackmanHarris = 12, // 0x0000000C
    fwBlackmanNuttall = 13, // 0x0000000D
    fwFlatTop = 14, // 0x0000000E
  }
}
