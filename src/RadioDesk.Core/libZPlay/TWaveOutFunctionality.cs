// Decompiled with JetBrains decompiler
// Type: libZPlay.TWaveOutFunctionality
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

namespace libZPlay
{
  public enum TWaveOutFunctionality : uint
  {
    supportPitchControl = 1,
    supportPlaybackRateControl = 2,
    supportVolumeControl = 4,
    supportDirectSound = 6,
    supportSeparateLeftRightVolume = 8,
    supportSync = 16, // 0x00000010
    supportSampleAccuratePosition = 32, // 0x00000020
  }
}
