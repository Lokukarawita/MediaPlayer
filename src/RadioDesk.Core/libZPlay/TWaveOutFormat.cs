// Decompiled with JetBrains decompiler
// Type: libZPlay.TWaveOutFormat
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

namespace libZPlay
{
  public enum TWaveOutFormat : uint
  {
    format_invalid = 0,
    format_11khz_8bit_mono = 1,
    format_11khz_8bit_stereo = 2,
    format_11khz_16bit_mono = 4,
    format_11khz_16bit_stereo = 8,
    format_22khz_8bit_mono = 16, // 0x00000010
    format_22khz_8bit_stereo = 32, // 0x00000020
    format_22khz_16bit_mono = 64, // 0x00000040
    format_22khz_16bit_stereo = 128, // 0x00000080
    format_44khz_8bit_mono = 256, // 0x00000100
    format_44khz_8bit_stereo = 512, // 0x00000200
    format_44khz_16bit_mono = 1024, // 0x00000400
    format_44khz_16bit_stereo = 2048, // 0x00000800
  }
}
