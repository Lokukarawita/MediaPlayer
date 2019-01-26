using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoInc.Net.UPnP.XML.DIDLLite
{
    public class DIDLLiteObject
    {
        public DIDLLiteObject()
        {
            Items = new List<MediaComponent>();
        }

        public List<MediaComponent> Items { get; set; }

    }
}
