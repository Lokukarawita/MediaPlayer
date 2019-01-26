using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoInc.Net.UPnP.XML.DIDLLite
{
    public class MediaContainer : MediaComponent
    {
        public bool Restricted { get; set; }
        public bool Searchable { get; set; }
        public string Creator { get; set; }
        public string Genre { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string AlbumArtURI { get; set; }
    }
}
