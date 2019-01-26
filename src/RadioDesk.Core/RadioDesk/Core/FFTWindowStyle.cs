// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.FFTWindowStyle
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

namespace RadioDesk.Core
{
  public enum FFTWindowStyle
  {
    Rectangular = 1,
    Hamming = 2,
    Hann = 3,
    Cosine = 4,
    Lanczos = 5,
    Bartlett = 6,
    Triangular = 7,
    Gauss = 8,
    BartlettHann = 9,
    Blackman = 10, // 0x0000000A
    Nuttall = 11, // 0x0000000B
    BlackmanHarris = 12, // 0x0000000C
    BlackmanNuttall = 13, // 0x0000000D
    FlatTop = 14, // 0x0000000E
  }
}
