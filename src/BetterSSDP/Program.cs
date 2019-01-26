using ManagedUPnP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetterSSDP
{
    class Program
    {
        static void Main(string[] args)
        {
            var msearch = new AutoEventedDiscoveryServices<Service>("urn:schemas-upnp-org:device:MediaServer:1");
            msearch.StatusNotifyAction += msearch_StatusNotifyAction;
            msearch.CanCreateServiceFor += msearch_CanCreateServiceFor;
            msearch.CreateServiceFor += msearch_CreateServiceFor;
            msearch.ReStartAsync();

            Console.ReadLine();
        }

        static void msearch_CreateServiceFor(object sender, AutoEventedDiscoveryServices<Service>.CreateServiceForEventArgs e)
        {
            e.CreatedAutoService = e.Service;
        }

        static void msearch_CanCreateServiceFor(object sender, AutoEventedDiscoveryServices<Service>.CanCreateServiceForEventArgs e)
        {
            e.CanCreate = true;
        }

        static void msearch_StatusNotifyAction(object sender, AutoEventedDiscoveryServices<Service>.StatusNotifyActionEventArgs e)
        {
            switch (e.NotifyAction)
            {
                case AutoDiscoveryServices<Service>.NotifyAction.ServiceAdded:
                    var srv = e.Data as Service;

                    // A new service was found, add it
                    //tvUPnP.AddService((Service)(a.Data));
                    break;

                case AutoDiscoveryServices<Service>.NotifyAction.DeviceRemoved:
                    // A device has been removed, remove it and all services
                    //tvUPnP.RemoveDevice((String)(a.Data));
                    break;
                case AutoDiscoveryServices<Service>.NotifyAction.COMDeviceFound:
                    var srv1 = e.Data as Service;
                    Console.WriteLine(e.Data);
                    break;
                case AutoDiscoveryServices<Service>.NotifyAction.ServiceRemoved:
                    // A service was removed, remove it
                    //tvUPnP.RemoveService((Service)(a.Data));
                    break;
            }
        }
    }
}
