using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvoInc.Net.UPnP.Discovery
{
    public class DiscoveredDevice
    {
        #region Parsig
        private static KeyValuePair<string, string>? GetKeyValuePir(string kvvalue)
        {
            if (string.IsNullOrWhiteSpace(kvvalue)) return null;
            var splitted = kvvalue.Split(new char[] { ':' }, 2);
            if (splitted != null &&
                splitted.Length > 1)
            {
                var kT = string.IsNullOrWhiteSpace(splitted[0]) ? null : splitted[0].Trim();
                var vT = string.IsNullOrWhiteSpace(splitted[1]) ? null : splitted[1].Trim();
                var kvp = new KeyValuePair<string, string>?(new KeyValuePair<string, string>(kT, vT));
                return kvp;
            }

            return null;
        }
        public static bool TryParse(string ssdpMessage, out DiscoveredDevice device)
        {
            if (!ssdpMessage.Contains("NOTIFY") && !ssdpMessage.Contains("HTTP/1.1 200 OK"))
            {
                device = null;
                return false;
            }

            DiscoveredDevice ssdpDevice = new DiscoveredDevice();
            ssdpDevice.Raw = ssdpMessage;

            var sections = ssdpMessage.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (sections != null && sections.Length > 0)
            {

                foreach (var item in sections)
                {
                    var kvPair = GetKeyValuePir(item);
                    if (kvPair.HasValue && !string.IsNullOrWhiteSpace(kvPair.Value.Key))
                    {
                        switch (kvPair.Value.Key.ToUpper())
                        {
                            case "LOCATION": ssdpDevice.Location = kvPair.Value.Value; continue;
                            case "ST": ssdpDevice.Target = kvPair.Value.Value; continue;
                            case "NT": ssdpDevice.Target = kvPair.Value.Value; continue;
                            case "USN": ssdpDevice.USN = kvPair.Value.Value; continue;
                            case "SERVER": ssdpDevice.Server = kvPair.Value.Value; continue;
                            case "NTS": ssdpDevice.NTS = kvPair.Value.Value; continue;
                            default:
                                break;
                        }
                    }
                }
            }

            device = ssdpDevice;
            return true;
        } 
        #endregion

        public string Location { get; set; }
        public string Server { get; set; }
        public string Target { get; set; }
        public string USN { get; set; }
        public string NTS { get; set; }
        public string Raw { get; set; }
    }
}
