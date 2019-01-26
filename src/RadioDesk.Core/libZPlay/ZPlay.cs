// Decompiled with JetBrains decompiler
// Type: libZPlay.ZPlay
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace libZPlay
{
  public class ZPlay
  {
    private uint objptr;

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern uint zplay_CreateZPlay();

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_DestroyZPlay(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetSettings(uint objptr, int nSettingID, int value);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetSettings(uint objptr, int nSettingID);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern IntPtr zplay_GetError(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern IntPtr zplay_GetErrorW(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetVersion(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetFileFormat(uint objptr, [MarshalAs(UnmanagedType.LPStr)] string pchFileName);

    [DllImport("libzplay.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int zplay_GetFileFormatW(uint objptr, [MarshalAs(UnmanagedType.LPWStr)] string pchFileName);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_OpenFile(uint objptr, [MarshalAs(UnmanagedType.LPStr)] string sFileName, int nFormat);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_AddFile(uint objptr, [MarshalAs(UnmanagedType.LPStr)] string sFileName, int nFormat);

    [DllImport("libzplay.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int zplay_OpenFileW(uint objptr, [MarshalAs(UnmanagedType.LPWStr)] string sFileName, int nFormat);

    [DllImport("libzplay.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int zplay_AddFileW(uint objptr, [MarshalAs(UnmanagedType.LPWStr)] string sFileName, int nFormat);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_OpenStream(
      uint objptr,
      int fBuffered,
      int fManaged,
      [In] byte[] sMemStream,
      uint nStreamSize,
      int nFormat);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_PushDataToStream(
      uint objptr,
      [In] byte[] sMemNewData,
      uint nNewDataize);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_Close(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_Play(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_Stop(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_Pause(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_Resume(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_IsStreamDataFree(uint objptr, [In] byte[] sMemNewData);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern void zplay_GetDynamicStreamLoad(
      uint objptr,
      ref TStreamLoadInfo pStreamLoadInfo);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern void zplay_GetPosition(uint objptr, ref TStreamTime pTime);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_PlayLoop(
      uint objptr,
      int fFormatStartTime,
      ref TStreamTime pStartTime,
      int fFormatEndTime,
      ref TStreamTime pEndTime,
      uint nNumOfCycles,
      uint fContinuePlaying);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_Seek(
      uint objptr,
      TTimeFormat fFormat,
      ref TStreamTime pTime,
      TSeekMethod nMoveMethod);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_ReverseMode(uint objptr, int fEnable);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetMasterVolume(uint objptr, int nLeftVolume, int nRightVolume);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetPlayerVolume(uint objptr, int nLeftVolume, int nRightVolume);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern void zplay_GetMasterVolume(
      uint objptr,
      ref int nLeftVolume,
      ref int nRightVolume);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern void zplay_GetPlayerVolume(
      uint objptr,
      ref int nLeftVolume,
      ref int nRightVolume);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetBitrate(uint objptr, int fAverage);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern void zplay_GetStatus(uint objptr, ref TStreamStatus pStatus);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_MixChannels(
      uint objptr,
      int fEnable,
      uint nLeftPercent,
      uint nRightPercent);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern void zplay_GetVUData(
      uint objptr,
      ref int pnLeftChannel,
      ref int pnRightChannel);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SlideVolume(
      uint objptr,
      TTimeFormat fFormatStart,
      ref TStreamTime pTimeStart,
      int nStartVolumeLeft,
      int nStartVolumeRight,
      TTimeFormat fFormatEnd,
      ref TStreamTime pTimeEnd,
      int nEndVolumeLeft,
      int nEndVolumeRight);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_EnableEqualizer(uint objptr, int fEnable);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetEqualizerPoints(
      uint objptr,
      [In] int[] pnFreqPoint,
      int nNumOfPoints);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetEqualizerPoints(
      uint objptr,
      [In, Out] int[] pnFreqPoint,
      int nNumOfPoints);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetEqualizerParam(
      uint objptr,
      int nPreAmpGain,
      [In] int[] pnBandGain,
      int nNumberOfBands);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetEqualizerParam(
      uint objptr,
      ref int nPreAmpGain,
      [In, Out] int[] pnBandGain,
      int nNumberOfBands);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetEqualizerPreampGain(uint objptr, int nGain);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetEqualizerPreampGain(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetEqualizerBandGain(uint objptr, int nBandIndex, int nGain);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetEqualizerBandGain(uint objptr, int nBandIndex);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_EnableEcho(uint objptr, int fEnable);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_StereoCut(
      uint objptr,
      int fEnable,
      int fOutputCenter,
      int fBassToSides);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetEchoParam(
      uint objptr,
      [In] TEchoEffect[] pEchoEffect,
      int nNumberOfEffects);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetEchoParam(
      uint objptr,
      [In, Out] TEchoEffect[] pEchoEffect,
      int nNumberOfEffects);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetFFTData(
      uint objptr,
      int nFFTPoints,
      int nFFTWindow,
      ref int pnHarmonicNumber,
      [In, Out] int[] pnHarmonicFreq,
      [In, Out] int[] pnLeftAmplitude,
      [In, Out] int[] pnRightAmplitude,
      [In, Out] int[] pnLeftPhase,
      [In, Out] int[] pnRightPhase);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetRate(uint objptr, int nRate);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetRate(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetPitch(uint objptr, int nPitch);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetPitch(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetTempo(uint objptr, int nTempo);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetTempo(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_DrawFFTGraphOnHDC(
      uint objptr,
      IntPtr hdc,
      int nX,
      int nY,
      int nWidth,
      int nHeight);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_DrawFFTGraphOnHWND(
      uint objptr,
      IntPtr hwnd,
      int nX,
      int nY,
      int nWidth,
      int nHeight);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetFFTGraphParam(uint objptr, int nParamID, int nParamValue);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetFFTGraphParam(uint objptr, int nParamID);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_LoadID3W(
      uint objptr,
      int nId3Version,
      ref ZPlay.TID3Info_Internal pId3Info);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_LoadID3ExW(
      uint objptr,
      ref ZPlay.TID3InfoEx_Internal pId3Info,
      int fDecodeEmbededPicture);

    [DllImport("libzplay.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int zplay_LoadFileID3W(
      uint objptr,
      [MarshalAs(UnmanagedType.LPWStr)] string pchFileName,
      int nFormat,
      int nId3Version,
      ref ZPlay.TID3Info_Internal pId3Info);

    [DllImport("libzplay.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int zplay_LoadFileID3ExW(
      uint objptr,
      [MarshalAs(UnmanagedType.LPWStr)] string pchFileName,
      int nFormat,
      ref ZPlay.TID3InfoEx_Internal pId3Info,
      int fDecodeEmbededPicture);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_DetectBPM(uint objptr, uint nMethod);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_DetectFileBPM(
      uint objptr,
      [MarshalAs(UnmanagedType.LPStr)] string sFileName,
      int nFormat,
      uint nMethod);

    [DllImport("libzplay.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int zplay_DetectFileBPMW(
      uint objptr,
      [MarshalAs(UnmanagedType.LPWStr)] string sFileName,
      int nFormat,
      uint nMethod);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetCallbackFunc(
      uint objptr,
      [MarshalAs(UnmanagedType.FunctionPtr)] TCallbackFunc pCallbackFunc,
      TCallbackMessage nMessage,
      int user_data);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_EnumerateWaveOut(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetWaveOutInfoW(
      uint objptr,
      uint nIndex,
      ref ZPlay.TWaveOutInfo_Internal pWaveOutInfo);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetWaveOutDevice(uint objptr, uint nIndex);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_EnumerateWaveIn(uint objptr);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_GetWaveInInfoW(
      uint objptr,
      uint nIndex,
      ref ZPlay.TWaveInInfo_Internal pWaveOutInfo);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern int zplay_SetWaveInDevice(uint objptr, uint nIndex);

    [DllImport("libzplay.dll", CharSet = CharSet.Ansi, SetLastError = true)]
    private static extern void zplay_GetStreamInfoW(
      uint objptr,
      ref ZPlay.TStreamInfo_Internal pInfo);

    [DllImport("libzplay.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern int zplay_SetWaveOutFileW(
      uint objptr,
      [MarshalAs(UnmanagedType.LPWStr)] string sFileName,
      int nFormat,
      int fOutputToSoundcard);

    public ZPlay()
    {
      this.objptr = ZPlay.zplay_CreateZPlay();
      if (this.objptr == 0U)
        throw new Exception("Can't create libZPlay interface.");
      if (this.GetVersion() < 190)
        throw new Exception("Need libZPlay.dll version 1.90 and above.");
    }

    ~ZPlay()
    {
      ZPlay.zplay_DestroyZPlay(this.objptr);
    }

    public int GetVersion()
    {
      return ZPlay.zplay_GetVersion(this.objptr);
    }

    public string GetError()
    {
      return Marshal.PtrToStringUni(ZPlay.zplay_GetErrorW(this.objptr));
    }

    public TStreamFormat GetFileFormat(string FileName)
    {
      return (TStreamFormat) ZPlay.zplay_GetFileFormatW(this.objptr, FileName);
    }

    public bool OpenFile(string FileName, TStreamFormat Format)
    {
      return ZPlay.zplay_OpenFileW(this.objptr, FileName, Convert.ToInt32((object) Format)) == 1;
    }

    public bool SetWaveOutFile(string FileName, TStreamFormat Format, bool fOutputToSoundcard)
    {
      int fOutputToSoundcard1 = 0;
      if (fOutputToSoundcard)
        fOutputToSoundcard1 = 1;
      return ZPlay.zplay_SetWaveOutFileW(this.objptr, FileName, Convert.ToInt32((object) Format), fOutputToSoundcard1) == 1;
    }

    public bool AddFile(string FileName, TStreamFormat Format)
    {
      return ZPlay.zplay_AddFileW(this.objptr, FileName, Convert.ToInt32((object) Format)) == 1;
    }

    public bool OpenStream(
      bool Buffered,
      bool Dynamic,
      ref byte[] MemStream,
      uint StreamSize,
      TStreamFormat nFormat)
    {
      int fBuffered = 0;
      int fManaged = 0;
      if (Buffered)
        fBuffered = 1;
      if (Dynamic)
        fManaged = 1;
      return ZPlay.zplay_OpenStream(this.objptr, fBuffered, fManaged, MemStream, StreamSize, Convert.ToInt32((object) nFormat)) == 1;
    }

    public bool PushDataToStream(ref byte[] MemNewData, uint NewDatSize)
    {
      return ZPlay.zplay_PushDataToStream(this.objptr, MemNewData, NewDatSize) == 1;
    }

    public bool IsStreamDataFree(ref byte[] MemNewData)
    {
      return ZPlay.zplay_IsStreamDataFree(this.objptr, MemNewData) == 1;
    }

    public bool Close()
    {
      return ZPlay.zplay_Close(this.objptr) == 1;
    }

    public void GetPosition(ref TStreamTime time)
    {
      ZPlay.zplay_GetPosition(this.objptr, ref time);
    }

    public bool Seek(TTimeFormat TimeFormat, ref TStreamTime Position, TSeekMethod MoveMethod)
    {
      return ZPlay.zplay_Seek(this.objptr, TimeFormat, ref Position, MoveMethod) == 1;
    }

    public bool ReverseMode(bool Enable)
    {
      if (Enable)
        return ZPlay.zplay_ReverseMode(this.objptr, 1) == 1;
      return ZPlay.zplay_ReverseMode(this.objptr, 0) == 1;
    }

    public bool PlayLoop(
      TTimeFormat TimeFormatStart,
      ref TStreamTime StartPosition,
      TTimeFormat TimeFormatEnd,
      ref TStreamTime EndPosition,
      uint NumberOfCycles,
      bool ContinuePlaying)
    {
      uint fContinuePlaying = !ContinuePlaying ? 0U : 1U;
      return ZPlay.zplay_PlayLoop(this.objptr, Convert.ToInt32((int) TimeFormatStart), ref StartPosition, Convert.ToInt32((int) TimeFormatEnd), ref EndPosition, NumberOfCycles, fContinuePlaying) == 1;
    }

    public bool StartPlayback()
    {
      return ZPlay.zplay_Play(this.objptr) == 1;
    }

    public bool StopPlayback()
    {
      return ZPlay.zplay_Stop(this.objptr) == 1;
    }

    public bool PausePlayback()
    {
      return ZPlay.zplay_Pause(this.objptr) == 1;
    }

    public bool ResumePlayback()
    {
      return ZPlay.zplay_Resume(this.objptr) == 1;
    }

    public bool SetEqualizerParam(int PreAmpGain, ref int[] BandGain, int NumberOfBands)
    {
      return ZPlay.zplay_SetEqualizerParam(this.objptr, PreAmpGain, BandGain, NumberOfBands) == 1;
    }

    public int GetEqualizerParam(ref int PreAmpGain, ref int[] BandGain)
    {
      int nPreAmpGain = 0;
      int equalizerParam = ZPlay.zplay_GetEqualizerParam(this.objptr, ref nPreAmpGain, (int[]) null, 0);
      Array.Resize<int>(ref BandGain, equalizerParam);
      return ZPlay.zplay_GetEqualizerParam(this.objptr, ref PreAmpGain, BandGain, equalizerParam);
    }

    public bool EnableEqualizer(bool Enable)
    {
      if (Enable)
        return ZPlay.zplay_EnableEqualizer(this.objptr, 1) == 1;
      return ZPlay.zplay_EnableEqualizer(this.objptr, 0) == 1;
    }

    public bool SetEqualizerPreampGain(int Gain)
    {
      return ZPlay.zplay_SetEqualizerPreampGain(this.objptr, Gain) == 1;
    }

    public int GetEqualizerPreampGain()
    {
      return ZPlay.zplay_GetEqualizerPreampGain(this.objptr);
    }

    public bool SetEqualizerBandGain(int BandIndex, int Gain)
    {
      return ZPlay.zplay_SetEqualizerBandGain(this.objptr, BandIndex, Gain) == 1;
    }

    public int GetEqualizerBandGain(int BandIndex)
    {
      return ZPlay.zplay_GetEqualizerBandGain(this.objptr, BandIndex);
    }

    public bool SetEqualizerPoints(ref int[] FreqPointArray, int NumberOfPoints)
    {
      return ZPlay.zplay_SetEqualizerPoints(this.objptr, FreqPointArray, NumberOfPoints) == 1;
    }

    public int GetEqualizerPoints(ref int[] FreqPointArray)
    {
      int equalizerPoints = ZPlay.zplay_GetEqualizerPoints(this.objptr, (int[]) null, 0);
      Array.Resize<int>(ref FreqPointArray, equalizerPoints);
      return ZPlay.zplay_GetEqualizerPoints(this.objptr, FreqPointArray, equalizerPoints);
    }

    public bool EnableEcho(bool Enable)
    {
      if (Enable)
        return ZPlay.zplay_EnableEcho(this.objptr, 1) == 1;
      return ZPlay.zplay_EnableEcho(this.objptr, 0) == 1;
    }

    public bool SetEchoParam(ref TEchoEffect[] EchoEffectArray, int NumberOfEffects)
    {
      return ZPlay.zplay_SetEchoParam(this.objptr, EchoEffectArray, NumberOfEffects) == 1;
    }

    public int GetEchoParam(ref TEchoEffect[] EchoEffectArray)
    {
      int echoParam = ZPlay.zplay_GetEchoParam(this.objptr, (TEchoEffect[]) null, 0);
      Array.Resize<TEchoEffect>(ref EchoEffectArray, echoParam);
      return ZPlay.zplay_GetEchoParam(this.objptr, EchoEffectArray, echoParam);
    }

    public bool SetMasterVolume(int LeftVolume, int RightVolume)
    {
      return ZPlay.zplay_SetMasterVolume(this.objptr, LeftVolume, RightVolume) == 1;
    }

    public bool SetPlayerVolume(int LeftVolume, int RightVolume)
    {
      return ZPlay.zplay_SetPlayerVolume(this.objptr, LeftVolume, RightVolume) == 1;
    }

    public void GetMasterVolume(ref int LeftVolume, ref int RightVolume)
    {
      ZPlay.zplay_GetMasterVolume(this.objptr, ref LeftVolume, ref RightVolume);
    }

    public void GetPlayerVolume(ref int LeftVolume, ref int RightVolume)
    {
      ZPlay.zplay_GetPlayerVolume(this.objptr, ref LeftVolume, ref RightVolume);
    }

    public bool SlideVolume(
      TTimeFormat TimeFormatStart,
      ref TStreamTime TimeStart,
      int StartVolumeLeft,
      int StartVolumeRight,
      TTimeFormat TimeFormatEnd,
      ref TStreamTime TimeEnd,
      int EndVolumeLeft,
      int EndVolumeRight)
    {
      return ZPlay.zplay_SlideVolume(this.objptr, TimeFormatStart, ref TimeStart, StartVolumeLeft, StartVolumeRight, TimeFormatEnd, ref TimeEnd, EndVolumeLeft, EndVolumeRight) == 1;
    }

    public bool SetPitch(int Pitch)
    {
      return ZPlay.zplay_SetPitch(this.objptr, Pitch) == 1;
    }

    public int GetPitch()
    {
      return ZPlay.zplay_GetPitch(this.objptr);
    }

    public bool SetRate(int Rate)
    {
      return ZPlay.zplay_SetRate(this.objptr, Rate) == 1;
    }

    public int GetRate()
    {
      return ZPlay.zplay_GetRate(this.objptr);
    }

    public bool SetTempo(int Tempo)
    {
      return ZPlay.zplay_SetTempo(this.objptr, Tempo) == 1;
    }

    public int GetTempo()
    {
      return ZPlay.zplay_GetTempo(this.objptr);
    }

    public int GetBitrate(bool Average)
    {
      if (Average)
        return ZPlay.zplay_GetBitrate(this.objptr, 1);
      return ZPlay.zplay_GetBitrate(this.objptr, 0);
    }

    public bool LoadID3(TID3Version Id3Version, ref TID3Info Info)
    {
      ZPlay.TID3Info_Internal pId3Info = new ZPlay.TID3Info_Internal();
      if (ZPlay.zplay_LoadID3W(this.objptr, Convert.ToInt32((int) Id3Version), ref pId3Info) != 1)
        return false;
      Info.Album = Marshal.PtrToStringUni(pId3Info.Album);
      Info.Artist = Marshal.PtrToStringUni(pId3Info.Artist);
      Info.Comment = Marshal.PtrToStringUni(pId3Info.Comment);
      Info.Genre = Marshal.PtrToStringUni(pId3Info.Genre);
      Info.Title = Marshal.PtrToStringUni(pId3Info.Title);
      Info.Track = Marshal.PtrToStringUni(pId3Info.Track);
      Info.Year = Marshal.PtrToStringUni(pId3Info.Year);
      return true;
    }

    public bool LoadID3Ex(ref TID3InfoEx Info, bool fDecodePicture)
    {
      ZPlay.TID3InfoEx_Internal pId3Info = new ZPlay.TID3InfoEx_Internal();
      if (ZPlay.zplay_LoadID3ExW(this.objptr, ref pId3Info, 0) != 1)
        return false;
      Info.Album = Marshal.PtrToStringUni(pId3Info.Album);
      Info.Artist = Marshal.PtrToStringUni(pId3Info.Artist);
      Info.Comment = Marshal.PtrToStringUni(pId3Info.Comment);
      Info.Genre = Marshal.PtrToStringUni(pId3Info.Genre);
      Info.Title = Marshal.PtrToStringUni(pId3Info.Title);
      Info.Track = Marshal.PtrToStringUni(pId3Info.Track);
      Info.Year = Marshal.PtrToStringUni(pId3Info.Year);
      Info.AlbumArtist = Marshal.PtrToStringUni(pId3Info.AlbumArtist);
      Info.Composer = Marshal.PtrToStringUni(pId3Info.Composer);
      Info.OriginalArtist = Marshal.PtrToStringUni(pId3Info.OriginalArtist);
      Info.Copyright = Marshal.PtrToStringUni(pId3Info.Copyright);
      Info.Encoder = Marshal.PtrToStringUni(pId3Info.Encoder);
      Info.Publisher = Marshal.PtrToStringUni(pId3Info.Publisher);
      Info.BPM = pId3Info.BPM;
      Info.Picture.PicturePresent = false;
      if (fDecodePicture)
      {
        try
        {
          if (pId3Info.PicturePresent == 1)
          {
            byte[] numArray = new byte[Convert.ToInt32(pId3Info.PictureDataSize) + 1];
            Marshal.Copy(pId3Info.PictureData, numArray, 0, pId3Info.PictureDataSize);
            Info.Picture.BitStream = new MemoryStream();
            Info.Picture.BitStream.Write(numArray, 0, pId3Info.PictureDataSize);
            Info.Picture.Bitmap = new Bitmap((Stream) Info.Picture.BitStream);
            Info.Picture.PictureType = pId3Info.PictureType;
            Info.Picture.Description = Marshal.PtrToStringUni(pId3Info.Description);
            Info.Picture.PicturePresent = true;
          }
          else
            Info.Picture.Bitmap = new Bitmap(1, 1);
          return true;
        }
        catch
        {
          Info.Picture.PicturePresent = false;
        }
      }
      return false;
    }

    public bool LoadFileID3(
      string FileName,
      TStreamFormat Format,
      TID3Version Id3Version,
      ref TID3Info Info)
    {
      ZPlay.TID3Info_Internal pId3Info = new ZPlay.TID3Info_Internal();
      if (ZPlay.zplay_LoadFileID3W(this.objptr, FileName, Convert.ToInt32((object) Format), Convert.ToInt32((int) Id3Version), ref pId3Info) != 1)
        return false;
      Info.Album = Marshal.PtrToStringUni(pId3Info.Album);
      Info.Artist = Marshal.PtrToStringUni(pId3Info.Artist);
      Info.Comment = Marshal.PtrToStringUni(pId3Info.Comment);
      Info.Genre = Marshal.PtrToStringUni(pId3Info.Genre);
      Info.Title = Marshal.PtrToStringUni(pId3Info.Title);
      Info.Track = Marshal.PtrToStringUni(pId3Info.Track);
      Info.Year = Marshal.PtrToStringUni(pId3Info.Year);
      return true;
    }

    public bool LoadFileID3Ex(
      string FileName,
      TStreamFormat Format,
      ref TID3InfoEx Info,
      bool fDecodePicture)
    {
      ZPlay.TID3InfoEx_Internal pId3Info = new ZPlay.TID3InfoEx_Internal();
      if (ZPlay.zplay_LoadFileID3ExW(this.objptr, FileName, Convert.ToInt32((object) Format), ref pId3Info, 0) != 1)
        return false;
      Info.Album = Marshal.PtrToStringUni(pId3Info.Album);
      Info.Artist = Marshal.PtrToStringUni(pId3Info.Artist);
      Info.Comment = Marshal.PtrToStringUni(pId3Info.Comment);
      Info.Genre = Marshal.PtrToStringUni(pId3Info.Genre);
      Info.Title = Marshal.PtrToStringUni(pId3Info.Title);
      Info.Track = Marshal.PtrToStringUni(pId3Info.Track);
      Info.Year = Marshal.PtrToStringUni(pId3Info.Year);
      Info.AlbumArtist = Marshal.PtrToStringUni(pId3Info.AlbumArtist);
      Info.Composer = Marshal.PtrToStringUni(pId3Info.Composer);
      Info.OriginalArtist = Marshal.PtrToStringUni(pId3Info.OriginalArtist);
      Info.Copyright = Marshal.PtrToStringUni(pId3Info.Copyright);
      Info.Encoder = Marshal.PtrToStringUni(pId3Info.Encoder);
      Info.Publisher = Marshal.PtrToStringUni(pId3Info.Publisher);
      Info.BPM = pId3Info.BPM;
      Info.Picture.PicturePresent = false;
      if (fDecodePicture)
      {
        try
        {
          if (pId3Info.PicturePresent == 1)
          {
            byte[] numArray = new byte[Convert.ToInt32(pId3Info.PictureDataSize) + 1];
            Marshal.Copy(pId3Info.PictureData, numArray, 0, pId3Info.PictureDataSize);
            Info.Picture.BitStream = new MemoryStream();
            Info.Picture.BitStream.Write(numArray, 0, pId3Info.PictureDataSize);
            Info.Picture.Bitmap = new Bitmap((Stream) Info.Picture.BitStream);
            Info.Picture.PictureType = pId3Info.PictureType;
            Info.Picture.Description = Marshal.PtrToStringUni(pId3Info.Description);
            Info.Picture.PicturePresent = true;
          }
          else
            Info.Picture.Bitmap = new Bitmap(1, 1);
          return true;
        }
        catch
        {
          Info.Picture.PicturePresent = false;
        }
      }
      return false;
    }

    public bool SetCallbackFunc(
      TCallbackFunc CallbackFunc,
      TCallbackMessage Messages,
      int UserData)
    {
      return ZPlay.zplay_SetCallbackFunc(this.objptr, CallbackFunc, Messages, UserData) == 1;
    }

    public int DetectBPM(TBPMDetectionMethod Method)
    {
      return ZPlay.zplay_DetectBPM(this.objptr, Convert.ToUInt32((object) Method));
    }

    public int DetectFileBPM(string FileName, TStreamFormat Format, TBPMDetectionMethod Method)
    {
      return ZPlay.zplay_DetectFileBPMW(this.objptr, FileName, Convert.ToInt32((object) Format), Convert.ToUInt32((object) Method));
    }

    public bool GetFFTData(
      int FFTPoints,
      TFFTWindow FFTWindow,
      ref int HarmonicNumber,
      ref int[] HarmonicFreq,
      ref int[] LeftAmplitude,
      ref int[] RightAmplitude,
      ref int[] LeftPhase,
      ref int[] RightPhase)
    {
      return ZPlay.zplay_GetFFTData(this.objptr, FFTPoints, Convert.ToInt32((int) FFTWindow), ref HarmonicNumber, HarmonicFreq, LeftAmplitude, RightAmplitude, LeftPhase, RightPhase) == 1;
    }

    public bool DrawFFTGraphOnHDC(IntPtr hdc, int X, int Y, int Width, int Height)
    {
      return ZPlay.zplay_DrawFFTGraphOnHDC(this.objptr, hdc, X, Y, Width, Height) == 1;
    }

    public bool DrawFFTGraphOnHWND(IntPtr hwnd, int X, int Y, int Width, int Height)
    {
      return ZPlay.zplay_DrawFFTGraphOnHWND(this.objptr, hwnd, X, Y, Width, Height) == 1;
    }

    public bool SetFFTGraphParam(TFFTGraphParamID ParamID, int ParamValue)
    {
      return ZPlay.zplay_SetFFTGraphParam(this.objptr, Convert.ToInt32((int) ParamID), ParamValue) == 1;
    }

    public int GetFFTGraphParam(TFFTGraphParamID ParamID)
    {
      return ZPlay.zplay_GetFFTGraphParam(this.objptr, Convert.ToInt32((int) ParamID));
    }

    public bool StereoCut(bool Enable, bool OutputCenter, bool BassToSides)
    {
      int fOutputCenter = 0;
      int fBassToSides = 0;
      int fEnable = 0;
      if (OutputCenter)
        fOutputCenter = 1;
      if (BassToSides)
        fBassToSides = 1;
      if (Enable)
        fEnable = 1;
      return ZPlay.zplay_StereoCut(this.objptr, fEnable, fOutputCenter, fBassToSides) == 1;
    }

    public bool MixChannels(bool Enable, uint LeftPercent, uint RightPercent)
    {
      if (Enable)
        return ZPlay.zplay_MixChannels(this.objptr, 1, LeftPercent, RightPercent) == 1;
      return ZPlay.zplay_MixChannels(this.objptr, 0, LeftPercent, RightPercent) == 1;
    }

    public void GetVUData(ref int LeftChannel, ref int RightChannel)
    {
      ZPlay.zplay_GetVUData(this.objptr, ref LeftChannel, ref RightChannel);
    }

    public void GetStreamInfo(ref TStreamInfo info)
    {
      ZPlay.TStreamInfo_Internal pInfo = new ZPlay.TStreamInfo_Internal();
      ZPlay.zplay_GetStreamInfoW(this.objptr, ref pInfo);
      info.Bitrate = pInfo.Bitrate;
      info.ChannelNumber = pInfo.ChannelNumber;
      info.SamplingRate = pInfo.SamplingRate;
      info.VBR = pInfo.VBR;
      info.Length = pInfo.Length;
      info.Description = Marshal.PtrToStringUni(pInfo.Description);
    }

    public void GetStatus(ref TStreamStatus status)
    {
      ZPlay.zplay_GetStatus(this.objptr, ref status);
    }

    public void GetDynamicStreamLoad(ref TStreamLoadInfo StreamLoadInfo)
    {
      ZPlay.zplay_GetDynamicStreamLoad(this.objptr, ref StreamLoadInfo);
    }

    public int EnumerateWaveOut()
    {
      return ZPlay.zplay_EnumerateWaveOut(this.objptr);
    }

    public bool GetWaveOutInfo(uint Index, ref TWaveOutInfo Info)
    {
      ZPlay.TWaveOutInfo_Internal pWaveOutInfo = new ZPlay.TWaveOutInfo_Internal();
      if (ZPlay.zplay_GetWaveOutInfoW(this.objptr, Index, ref pWaveOutInfo) == 0)
        return false;
      Info.Channels = pWaveOutInfo.Channels;
      Info.DriverVersion = pWaveOutInfo.DriverVersion;
      Info.Formats = pWaveOutInfo.Formats;
      Info.ManufacturerID = pWaveOutInfo.ManufacturerID;
      Info.ProductID = pWaveOutInfo.ProductID;
      Info.Support = pWaveOutInfo.Support;
      Info.ProductName = Marshal.PtrToStringUni(pWaveOutInfo.ProductName);
      return true;
    }

    public bool SetWaveOutDevice(uint Index)
    {
      return ZPlay.zplay_SetWaveOutDevice(this.objptr, Index) == 1;
    }

    public int EnumerateWaveIn()
    {
      return ZPlay.zplay_EnumerateWaveIn(this.objptr);
    }

    public bool GetWaveInInfo(uint Index, ref TWaveInInfo Info)
    {
      ZPlay.TWaveInInfo_Internal pWaveOutInfo = new ZPlay.TWaveInInfo_Internal();
      if (ZPlay.zplay_GetWaveInInfoW(this.objptr, Index, ref pWaveOutInfo) == 0)
        return false;
      Info.Channels = pWaveOutInfo.Channels;
      Info.DriverVersion = pWaveOutInfo.DriverVersion;
      Info.Formats = pWaveOutInfo.Formats;
      Info.ManufacturerID = pWaveOutInfo.ManufacturerID;
      Info.ProductID = pWaveOutInfo.ProductID;
      Info.ProductName = Marshal.PtrToStringUni(pWaveOutInfo.ProductName);
      return true;
    }

    public bool SetWaveInDevice(uint Index)
    {
      return ZPlay.zplay_SetWaveInDevice(this.objptr, Index) == 1;
    }

    public int SetSettings(TSettingID SettingID, int Value)
    {
      return ZPlay.zplay_SetSettings(this.objptr, (int) SettingID, Value);
    }

    public int GetSettings(TSettingID SettingID)
    {
      return ZPlay.zplay_GetSettings(this.objptr, (int) SettingID);
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    private struct TStreamInfo_Internal
    {
      [FieldOffset(0)]
      public int SamplingRate;
      [FieldOffset(4)]
      public int ChannelNumber;
      [FieldOffset(8)]
      public bool VBR;
      [FieldOffset(12)]
      public int Bitrate;
      [FieldOffset(16)]
      public TStreamTime Length;
      [FieldOffset(44)]
      public IntPtr Description;
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    private struct TWaveOutInfo_Internal
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
      public IntPtr ProductName;
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    private struct TWaveInInfo_Internal
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
      public IntPtr ProductName;
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    private struct TID3Info_Internal
    {
      [FieldOffset(0)]
      public IntPtr Title;
      [FieldOffset(4)]
      public IntPtr Artist;
      [FieldOffset(8)]
      public IntPtr Album;
      [FieldOffset(12)]
      public IntPtr Year;
      [FieldOffset(16)]
      public IntPtr Comment;
      [FieldOffset(20)]
      public IntPtr Track;
      [FieldOffset(24)]
      public IntPtr Genre;
    }

    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
    private struct TID3InfoEx_Internal
    {
      [FieldOffset(0)]
      public IntPtr Title;
      [FieldOffset(4)]
      public IntPtr Artist;
      [FieldOffset(8)]
      public IntPtr Album;
      [FieldOffset(12)]
      public IntPtr Year;
      [FieldOffset(16)]
      public IntPtr Comment;
      [FieldOffset(20)]
      public IntPtr Track;
      [FieldOffset(24)]
      public IntPtr Genre;
      [FieldOffset(28)]
      public IntPtr AlbumArtist;
      [FieldOffset(32)]
      public IntPtr Composer;
      [FieldOffset(36)]
      public IntPtr OriginalArtist;
      [FieldOffset(40)]
      public IntPtr Copyright;
      [FieldOffset(44)]
      public IntPtr URL;
      [FieldOffset(48)]
      public IntPtr Encoder;
      [FieldOffset(52)]
      public IntPtr Publisher;
      [FieldOffset(56)]
      public int BPM;
      [FieldOffset(60)]
      public int PicturePresent;
      [FieldOffset(64)]
      public int CanDrawPicture;
      [FieldOffset(68)]
      public IntPtr MIMEType;
      [FieldOffset(72)]
      public int PictureType;
      [FieldOffset(76)]
      public IntPtr Description;
      [FieldOffset(80)]
      public IntPtr PictureData;
      [FieldOffset(84)]
      public int PictureDataSize;
      [FieldOffset(88)]
      public IntPtr hBitmap;
      [FieldOffset(92)]
      public int Width;
      [FieldOffset(96)]
      public int Height;
      [FieldOffset(356)]
      public IntPtr reserved;
    }
  }
}
