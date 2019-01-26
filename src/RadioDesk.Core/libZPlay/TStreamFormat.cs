// Decompiled with JetBrains decompiler
// Type: libZPlay.TStreamFormat
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

namespace libZPlay
{
  public enum TStreamFormat
  {
    sfUnknown = 0,
    sfMp3 = 1,
    sfOgg = 2,
    sfWav = 3,
    sfPCM = 4,
    sfFLAC = 5,
    sfFLACOgg = 6,
    sfAC3 = 7,
    sfAacADTS = 8,
    sfWaveIn = 9,
    sfAutodetect = 1000, // 0x000003E8
  }
}
