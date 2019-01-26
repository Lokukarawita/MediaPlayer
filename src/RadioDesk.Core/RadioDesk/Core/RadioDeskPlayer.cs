// Decompiled with JetBrains decompiler
// Type: RadioDesk.Core.RadioDeskPlayer
// Assembly: RadioDesk.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 855D0D1C-52A8-4CAD-BE15-1C45DC37A846
// Assembly location: D:\RadioDeskPlayer[HomeEdition]Beta1\RadioDeskPlayer[HomeEdition]\RadioDesk.Core.dll

using libZPlay;
using System;

namespace RadioDesk.Core
{
    public sealed class RadioDeskPlayer
    {
        private bool wasPlaying = false;
        private bool mute = false;
        private TStreamStatus stream_status;
        private ZPlay player;
        private string current_file;
        private TStreamTime stream_time;
        private TStreamInfo stream_info;
        private int volL;
        private int volR;

        public RadioDeskPlayer()
        {
            this.player = new ZPlay();
            this.stream_status = new TStreamStatus();
            this.stream_info = new TStreamInfo();
            this.player.GetPlayerVolume(ref this.volL, ref this.volR);
        }

        public void Play(string path)
        {
            this.current_file = path;
            if (!this.player.OpenFile(this.current_file, TStreamFormat.sfAutodetect))
                throw new MediaPlaybackException(this.player.GetError());
            this.player.StartPlayback();
            this.wasPlaying = true;
        }

        public void Play()
        {
            if (this.current_file == null || (this.player == null || this.IsPlaying))
                return;
            this.player.StartPlayback();
            this.wasPlaying = true;
        }

        public void Stop()
        {
            if (this.player == null)
                return;
            this.player.Close();
            this.wasPlaying = false;
        }

        public void Pause()
        {
            if (this.player == null)
                return;
            if (this.IsPaused)
            {
                this.player.ResumePlayback();
                this.wasPlaying = true;
            }
            else
                this.player.PausePlayback();
        }

        public void Resume()
        {
            if (this.player == null)
                return;
            this.player.ResumePlayback();
            this.wasPlaying = true;
        }

        public void SetPlayDirection(bool reverse)
        {
            if (this.current_file == null || (this.player == null || !this.IsPlaying))
                return;
            this.player.ReverseMode(reverse);
        }

        public void SeekForward(uint seconds)
        {
            this.player.GetStreamInfo(ref this.stream_info);
            this.player.GetStatus(ref this.stream_status);
            if (this.player == null)
                return;
            TStreamTime time = new TStreamTime();
            this.player.GetPosition(ref time);
            if (time.sec + seconds <= this.stream_info.Length.sec && this.stream_status.fPlay)
            {
                TStreamTime seekto = new TStreamTime() { sec = seconds };
                this.player.Seek(TTimeFormat.tfSecond, ref seekto, TSeekMethod.smFromCurrentForward);
            }
        }

        public void SeekBackward(uint seconds)
        {
            this.player.GetStreamInfo(ref this.stream_info);
            this.player.GetStatus(ref this.stream_status);
            if (this.player == null)
                return;
            TStreamTime time = new TStreamTime();
            this.player.GetPosition(ref time);
            if (time.sec - seconds >= 0U && this.stream_status.fPlay)
            {
                TStreamTime seekto = new TStreamTime() { sec = seconds };
                this.player.Seek(TTimeFormat.tfSecond, ref seekto, TSeekMethod.smFromCurrentBackward);
            }
        }

        public void Seek(uint ToSeconds)
        {
            TStreamTime seekto = new TStreamTime() { sec = ToSeconds };
            this.player.Seek(TTimeFormat.tfSecond, ref seekto, TSeekMethod.smFromBeginning);
        }

        public bool RequestHandle()
        {
            try
            {
                if (this.IsPlaying)
                {
                    this.player.GetPosition(ref this.stream_time);
                    this.player.StopPlayback();
                    this.wasPlaying = true;
                }
                this.player.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ReleaseHandle()
        {
            try
            {
                if (!this.player.OpenFile(this.current_file, TStreamFormat.sfAutodetect))
                    return false;
                if (this.wasPlaying)
                {
                    TStreamTime Position = new TStreamTime();
                    Position.sec = this.stream_time.sec;
                    this.player.StartPlayback();
                    this.player.Seek(TTimeFormat.tfSecond, ref Position, TSeekMethod.smFromBeginning);
                    this.wasPlaying = true;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public int VolumeLeft
        {
            get
            {
                int LeftVolume = 0;
                int RightVolume = 0;
                this.player.GetPlayerVolume(ref LeftVolume, ref RightVolume);
                return LeftVolume;
            }
            set
            {
                int LeftVolume = 0;
                int RightVolume = 0;
                this.player.GetPlayerVolume(ref LeftVolume, ref RightVolume);
                if (value > 100)
                    value = 100;
                else if (value < 0)
                    value = 0;
                this.volL = value;
                this.player.SetPlayerVolume(value, RightVolume);
            }
        }

        public int VolumeRight
        {
            get
            {
                int LeftVolume = 0;
                int RightVolume = 0;
                this.player.GetPlayerVolume(ref LeftVolume, ref RightVolume);
                return RightVolume;
            }
            set
            {
                int LeftVolume = 0;
                int RightVolume = 0;
                this.player.GetPlayerVolume(ref LeftVolume, ref RightVolume);
                if (value > 100)
                    value = 100;
                else if (value < 0)
                    value = 0;
                this.volR = value;
                this.player.SetPlayerVolume(LeftVolume, value);
            }
        }

        public int VolumeBoth
        {
            get
            {
                int LeftVolume = 0;
                int RightVolume = 0;
                this.player.GetPlayerVolume(ref LeftVolume, ref RightVolume);
                if (LeftVolume >= RightVolume)
                    return LeftVolume;
                return RightVolume;
            }
            set
            {
                if (value > 100)
                    value = 100;
                else if (value < 0)
                    value = 0;
                this.volL = value;
                this.volR = value;
                this.player.SetPlayerVolume(value, value);
            }
        }

        public int MasterVolumeLeft
        {
            get
            {
                int LeftVolume = 0;
                int RightVolume = 0;
                this.player.GetMasterVolume(ref LeftVolume, ref RightVolume);
                return LeftVolume;
            }
            set
            {
                int LeftVolume = 0;
                int RightVolume = 0;
                this.player.GetMasterVolume(ref LeftVolume, ref RightVolume);
                if (value > 100)
                    value = 100;
                else if (value < 0)
                    value = 0;
                this.player.SetMasterVolume(value, RightVolume);
            }
        }

        public int MasterVolumeRight
        {
            get
            {
                int LeftVolume = 0;
                int RightVolume = 0;
                this.player.GetMasterVolume(ref LeftVolume, ref RightVolume);
                return RightVolume;
            }
            set
            {
                int LeftVolume = 0;
                int RightVolume = 0;
                this.player.GetMasterVolume(ref LeftVolume, ref RightVolume);
                if (value > 100)
                    value = 100;
                else if (value < 0)
                    value = 0;
                this.player.SetMasterVolume(LeftVolume, value);
            }
        }

        public bool Mute
        {
            get
            {
                return this.mute;
            }
            set
            {
                if (value)
                {
                    this.player.GetPlayerVolume(ref this.volL, ref this.volR);
                    this.player.SetPlayerVolume(0, 0);
                    this.mute = true;
                }
                else
                {
                    this.player.SetPlayerVolume(this.volL, this.volR);
                    this.mute = false;
                }
            }
        }

        public uint CurrentPosoition
        {
            get
            {
                TStreamTime time = new TStreamTime();
                this.player.GetPosition(ref time);
                return time.sec;
            }
        }

        public uint Duration
        {
            get
            {
                this.player.GetStreamInfo(ref this.stream_info);
                return this.stream_info.Length.sec;
            }
        }

        public int LeftVUMeterValue
        {
            get
            {
                int LeftChannel = 0;
                int RightChannel = 0;
                this.player.GetVUData(ref LeftChannel, ref RightChannel);
                return LeftChannel;
            }
        }

        public int RightVUMeterValue
        {
            get
            {
                int LeftChannel = 0;
                int RightChannel = 0;
                this.player.GetVUData(ref LeftChannel, ref RightChannel);
                return RightChannel;
            }
        }

        private StereoEffectType GetActiveStereoEffect()
        {
            this.player.GetStatus(ref this.stream_status);
            if (this.stream_status.fSideCut)
                return StereoEffectType.BassCut;
            return this.stream_status.fVocalCut ? StereoEffectType.VocalCut : StereoEffectType.None;
        }

        public StereoEffectType StereoEffect
        {
            get
            {
                return this.GetActiveStereoEffect();
            }
            set
            {
                if (value == StereoEffectType.VocalCut)
                    this.player.StereoCut(true, false, true);
                else if (value == StereoEffectType.BassCut)
                    this.player.StereoCut(true, true, false);
                else
                    this.player.StereoCut(false, false, false);
            }
        }

        public PlayEffectRate Pitch
        {
            get
            {
                return (PlayEffectRate)this.player.GetPitch();
            }
            set
            {
                if (value == this.Pitch)
                    return;
                this.player.SetPitch((int)value);
            }
        }

        public PlayEffectRate Rate
        {
            get
            {
                return (PlayEffectRate)this.player.GetRate();
            }
            set
            {
                if (value == this.Rate)
                    return;
                this.player.SetRate((int)value);
            }
        }

        public PlayEffectRate Tempo
        {
            get
            {
                return (PlayEffectRate)this.player.GetTempo();
            }
            set
            {
                if (value == this.Tempo)
                    return;
                this.player.SetTempo((int)value);
            }
        }

        public bool IsPlaying
        {
            get
            {
                this.player.GetStatus(ref this.stream_status);
                return this.stream_status.fPlay;
            }
        }

        public bool IsPaused
        {
            get
            {
                this.player.GetStatus(ref this.stream_status);
                return this.stream_status.fPause;
            }
        }

        public bool IsStopped
        {
            get
            {
                this.player.GetStatus(ref this.stream_status);
                return !this.stream_status.fPlay && !this.stream_status.fPause;
            }
        }

        public bool IsReversed
        {
            get
            {
                this.player.GetStatus(ref this.stream_status);
                return this.stream_status.fReverse;
            }
        }

        [Obsolete("Not for developer use", false)]
        private void LaterRemove()
        {
        }
    }
}
