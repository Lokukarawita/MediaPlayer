using EvoPlayer.Core.Data.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Ops
{
    public class PlaybackStatusEventArgs : EventArgs
    {
        public PlaybackStatus CurrentStatus { get; set; }
        public PlaylistEntry Item { get; set; }
    }
}
