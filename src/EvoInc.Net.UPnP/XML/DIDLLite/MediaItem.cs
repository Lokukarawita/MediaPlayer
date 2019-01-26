using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoInc.Net.UPnP.XML.DIDLLite
{
    public class MediaItem : MediaComponent
    {
        public MediaItem()
        {
            Resources = new List<MediaResource>();
        }

        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public string AlbumArtURI { get; set; }
        public bool Restricted { get; set; }

        public List<MediaResource> Resources { get; set; }
    }
}
