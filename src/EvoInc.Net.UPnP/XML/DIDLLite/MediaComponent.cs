using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoInc.Net.UPnP.XML.DIDLLite
{
    public abstract class MediaComponent
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string UPnPClass { get; set; }
    }
}
