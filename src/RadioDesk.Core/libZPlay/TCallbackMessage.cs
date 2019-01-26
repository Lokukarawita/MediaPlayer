// Decompiled with JetBrains decompiler
// Type: libZPlay.TCallbackMessage
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

namespace libZPlay
{
  public enum TCallbackMessage
  {
    MsgStopAsync = 1,
    MsgPlayAsync = 2,
    MsgEnterLoopAsync = 4,
    MsgExitLoopAsync = 8,
    MsgEnterVolumeSlideAsync = 16, // 0x00000010
    MsgExitVolumeSlideAsync = 32, // 0x00000020
    MsgStreamBufferDoneAsync = 64, // 0x00000040
    MsgStreamNeedMoreDataAsync = 128, // 0x00000080
    MsgNextSongAsync = 256, // 0x00000100
    MsgStop = 65536, // 0x00010000
    MsgPlay = 131072, // 0x00020000
    MsgEnterLoop = 262144, // 0x00040000
    MsgExitLoop = 524288, // 0x00080000
    MsgEnterVolumeSlide = 1048576, // 0x00100000
    MsgExitVolumeSlide = 2097152, // 0x00200000
    MsgStreamBufferDone = 4194304, // 0x00400000
    MsgStreamNeedMoreData = 8388608, // 0x00800000
    MsgNextSong = 16777216, // 0x01000000
    MsgWaveBuffer = 33554432, // 0x02000000
  }
}
