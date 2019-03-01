﻿using EvoPlayer.Core.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Ops.libZPlay
{
    public class LibZPlayPlayer : IPlayer
    {
        private PlaylistController _plctrl;
        private IEqualizerController _eqctrl;
        private Lib.ZPlay _zplay;

        private StreamRead _streamReader;
        private Lib.TCallbackFunc _zplayCallback;
        private Lib.TCallbackMessage _zplayCallbackTypes;

        public event EventHandler<PlaybackStatusEventArgs> PlaybackStatusUpdate;

        public LibZPlayPlayer()
        {
            _zplay = new Lib.ZPlay();
            _plctrl = new PlaylistController();
            _eqctrl = new LibZPlayEqualizerController(_zplay);

            _zplayCallback = new Lib.TCallbackFunc(LibZPlay_Callback);
            _zplayCallbackTypes =
                Lib.TCallbackMessage.MsgPlayAsync |
                Lib.TCallbackMessage.MsgStopAsync |
                Lib.TCallbackMessage.MsgStreamBufferDoneAsync |
                Lib.TCallbackMessage.MsgStreamNeedMoreDataAsync;
            _zplay.SetCallbackFunc(_zplayCallback, _zplayCallbackTypes, 0);
        }
        ~LibZPlayPlayer()
        {
            if (_zplay != null)
            {
                _zplay.StopPlayback();
                _zplay.Close();
                _zplayCallback = null;
            }

            if (_streamReader != null)
            {
                _streamReader.Close();
            }
        }


        private void OnPlayBackStatus(PlaybackStatusEventArgs args)
        {
            if (PlaybackStatusUpdate != null)
            {
                PlaybackStatusUpdate(this, args);
            }
        }


        private int LibZPlay_Callback(uint objptr, int user_data, Lib.TCallbackMessage msg, uint param1, uint param2)
        {
            switch (msg)
            {
                case Lib.TCallbackMessage.MsgStopAsync:
                    OnPlayBackStatus(new PlaybackStatusEventArgs() { CurrentStatus = Status, Item = _plctrl.Current });
                    break;
                case Lib.TCallbackMessage.MsgPlayAsync:
                    OnPlayBackStatus(new PlaybackStatusEventArgs() { CurrentStatus = Status, Item = _plctrl.Current });
                    break;
                case Lib.TCallbackMessage.MsgEnterLoopAsync:
                    break;
                case Lib.TCallbackMessage.MsgExitLoopAsync:
                    break;
                case Lib.TCallbackMessage.MsgEnterVolumeSlideAsync:
                    break;
                case Lib.TCallbackMessage.MsgExitVolumeSlideAsync:
                    break;
                case Lib.TCallbackMessage.MsgStreamBufferDoneAsync:
                    var buf = _streamReader.GetBytes();
                    _zplay.PushDataToStream(ref buf, (uint)buf.Length);
                    break;
                case Lib.TCallbackMessage.MsgStreamNeedMoreDataAsync:
                    break;
                case Lib.TCallbackMessage.MsgNextSongAsync:
                    break;
                case Lib.TCallbackMessage.MsgStop:
                    break;
                case Lib.TCallbackMessage.MsgPlay:
                    break;
                case Lib.TCallbackMessage.MsgEnterLoop:
                    break;
                case Lib.TCallbackMessage.MsgExitLoop:
                    break;
                case Lib.TCallbackMessage.MsgEnterVolumeSlide:
                    break;
                case Lib.TCallbackMessage.MsgExitVolumeSlide:
                    break;
                case Lib.TCallbackMessage.MsgStreamBufferDone:
                    var next = _streamReader.GetBytes();
                    _zplay.PushDataToStream(ref next, (uint)next.Length);
                    break;
                case Lib.TCallbackMessage.MsgStreamNeedMoreData:
                    break;
                case Lib.TCallbackMessage.MsgNextSong:
                    break;
                case Lib.TCallbackMessage.MsgWaveBuffer:
                    break;
                default:
                    break;
            }

            return 0;
        }


        private void OpenFile(PlaylistEntry entry)
        {
            if (entry.StorageType == MediaStorageType.Local)
                OpenLocalFile(entry.Path);
            else
                OpenStream(entry.Path);
        }
        private void OpenStream(string streamPath)
        {
            _streamReader = new StreamRead(streamPath);
            var bytes = _streamReader.GetBytes();
            _zplay.OpenStream(true, true, ref bytes, (uint)bytes.Length, Lib.TStreamFormat.sfMp3);
        }
        private void OpenLocalFile(string path)
        {
            _zplay.OpenFile(path, Lib.TStreamFormat.sfAutodetect);
        }


        private Lib.TStreamStatus GetStatus()
        {
            Lib.TStreamStatus status = default(Lib.TStreamStatus);
            _zplay.GetStatus(ref status);
            return status;
        }


        public void Play()
        {
            if (Status == PlaybackStatus.Stopped)
            {
                if (!_plctrl.IsEmpty)
                {

                    var current = _plctrl.Current;
                    OpenFile(current);
                }
            }
            _zplay.StartPlayback();
        }
        public void Pause()
        {
            _zplay.PausePlayback();
        }
        public void Stop()
        {
            _zplay.StopPlayback();
        }
        public void Next()
        {
            //throw new NotImplementedException();
        }
        public void Previous()
        {
            //throw new NotImplementedException();
        }
        public void Seek(double seconds)
        {
            throw new NotImplementedException();
        }
        public VUMeterInfo VUMeter()
        {
            int fLeft = 0, fRight = 0;
            _zplay.GetVUData(ref fLeft, ref fRight);
            return new VUMeterInfo() { FLeft = fLeft, FRight = fRight };
        }


        public PlaylistController Playlist
        {
            get
            {
                return _plctrl;
            }
        }
        public PlaybackStatus Status
        {
            get
            {

                var status = GetStatus();
                if (status.fPlay)
                    return PlaybackStatus.Playing;
                else if (status.fPause)
                    return PlaybackStatus.Paused;
                else
                    return PlaybackStatus.Stopped;

            }
        }
        public int Volume
        {
            get
            {
                int lv = 0, rv = 0;
                _zplay.GetMasterVolume(ref lv, ref rv);
                return lv;
            }
            set
            {
                if (value > 100)
                    _zplay.SetMasterVolume(100, 100);
                else if (value < 0)
                    _zplay.SetMasterVolume(0, 0);
                else
                    _zplay.SetMasterVolume(value, value);
            }
        }

        #region Internal classes

        private class StreamRead
        {
            private string url;
            private System.Net.HttpWebResponse resp;
            private System.IO.Stream stream;

            public StreamRead(string url)
            {
                this.url = url;
                var req = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(url);
                resp = (System.Net.HttpWebResponse)req.GetResponse();
                stream = resp.GetResponseStream();
            }


            public byte[] GetBytes()
            {
                var buffer = new byte[200000];
                var read = stream.Read(buffer, 0, buffer.Length);
                if (read == 0)
                {
                    return new byte[0];
                }
                else
                {
                    Array.Resize<byte>(ref buffer, read);
                    return buffer;
                }
            }


            public void Close()
            {
                stream.Close();
                resp.Close();
                resp.Dispose();
            }
        } 
        #endregion
    }
}