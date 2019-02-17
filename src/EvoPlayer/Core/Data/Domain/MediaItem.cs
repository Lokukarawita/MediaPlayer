using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoPlayer.Core.Data.Domain
{
    public abstract class MediaItem
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Path { get; set; }
        public TimeSpan Duration { get; set; }
        public MediaStorageType StorageType { get; set; }
    }
}
