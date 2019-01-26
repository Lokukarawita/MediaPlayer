using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoInc.Net.UPnP.Discovery
{
    public class DiscoveredEventArgs : EventArgs
    {
        public DiscoveryType DiscoveredUsing { get; set; }
        public DiscoveryEvent Event { get; set; }
        public DiscoveredDevice Device { get; set; }
    }
}
