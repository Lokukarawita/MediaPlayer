using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Ops
{
    public interface IPlayer
    {
        void Play();
        void Stop();
        void Pause();
        void Next();
        void Previous();
        void Seek(double seconds);
        VUMeterInfo VUMeter();

        int Volume { get; set; }
        PlaybackStatus Status { get; }
        PlaylistController Playlist { get; }
    }
}
