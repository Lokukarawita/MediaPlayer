using EvoInc.Net.UPnP.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSSDP
{
    public class Upnp2
    {
        static DiscoveryService ssdp = new DiscoveryService(); 
        
        static void Main(string[] args)
        {
            ssdp.DeviceStatusUpdate += ssdp_DeviceStatusUpdate;
            ssdp.FilterByService = new string[] { "urn:schemas-upnp-org:device:MediaServer:1" };
            //ssdp.SearchPacketIntervalInSeconds = 1;
            ssdp.StartAsync();
            Console.ReadLine();
            ssdp.Stop();
            Console.ReadLine();
        }

        static void ssdp_DeviceStatusUpdate(object sender, DiscoveredEventArgs e)
        {
            Console.WriteLine(e.Device.Location);
        }
    }
}
