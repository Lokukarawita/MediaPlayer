using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Timers;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Collections.Concurrent;

namespace EvoInc.Net.UPnP.Discovery
{
    public class DiscoveryService : IDisposable
    {
        #region Constants
        /// <summary>
        /// Default interval is 5 minutes
        /// </summary>
        public const uint DEFAULT_SEARCH_INTERVAL = 5;
        /// <summary>
        /// Minimum search interval
        /// </summary>
        public const uint MINIMUM_SEARCH_INTERVAL = 1;
        /// <summary>
        /// 
        /// </summary>
        public const string MULTICAST_IPV4 = "239.255.255.250";
        /// <summary>
        /// 
        /// </summary>
        public const int MULTICAST_PORT = 1900; 
        #endregion

        private ConcurrentDictionary<IPAddress, UdpSocket> listOfSockets;
        private Timer tmrSearch;

        #region Public Events
        public event EventHandler<DiscoveredEventArgs> DeviceStatusUpdate; 
        #endregion

        public DiscoveryService()
        {
            SearchCycleGapInSeconds = 120;
            SearchPacketIntervalInSeconds = 1;
            SearchPacketsToSend = 3;

            listOfSockets = new ConcurrentDictionary<IPAddress, UdpSocket>();

            InitSearchTimer();
        }

        private void OnDeviceStatusUpdate(DiscoveredDevice dv, DiscoveryType type, DiscoveryEvent evt)
        {
            if (DeviceStatusUpdate != null)
            {
                DeviceStatusUpdate(this, new DiscoveredEventArgs()
                {
                    Device = dv,
                    Event = evt,
                    DiscoveredUsing = type
                });
            }
        }

        //M-SEARCH
        private string GetMsearch()
        {
            //"ssdp:all";
            //upnp:rootdevice

            string st = "ssdp:all";

            //if (FilterByService != null && FilterByService.Length == 0)
            //{
            //    st = FilterByService[0];
            //}

            StringBuilder builder = new StringBuilder();
            builder.Append("M-SEARCH * HTTP/1.1\r\n");
            builder.Append("Host:239.255.255.250:1900\r\n");
            builder.Append("Man:\"ssdp:discover\"\r\n");
            builder.Append("ST:").Append(st).Append("\r\n");
            builder.Append("User-Agent:UPnP/1.0 DLNADOC/1.50 EvoInc/1.0\r\n");
            builder.Append("MX:3\r\n");
            builder.Append("\r\n");
            builder.Append("\r\n");

            return builder.ToString();
        }
        private void TmrSearch_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                Debug.WriteLine("DiscoveryService: Sending {0} M-SEARCH Packets", SearchPacketsToSend);
                for (int i = 0; i < SearchPacketsToSend; i++)
                {
                    string msearch = GetMsearch();
                    var bytes = Encoding.ASCII.GetBytes(msearch);
                    Debug.WriteLine("DiscoveryService: Sending M-SEARCH #{0}", i + 1);

                    var sockets = listOfSockets.ToList();
                    foreach (var item in sockets)
                    {
                        try
                        {
                            bool rv = item.Value.Send(bytes);
                            if (rv)
                            {
                                Debug.WriteLineIf(!rv, "DiscoveryService: Failed to send M-SEARCH on IP " + item.Value.Address.ToString());
                            }
                        }
                        catch (Exception) { }
                    }

                    Debug.WriteLine("DiscoveryService: Sleeping M-SEARCH for {0}", TimeSpan.FromSeconds(SearchPacketIntervalInSeconds));
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(SearchPacketIntervalInSeconds));
                }

            }
            catch (Exception) { }
        }
        
        //socket callbacks
        private void OnUdpMessage(UdpSocket socket, string message)
        {
            try
            {
                Debug.WriteLine(message);
                ProcessSSDPMessage(message);
            }
            catch (Exception) { }
        }
        private void OnUdpError(UdpSocket socket, Exception ex)
        {
            socket.Dispose();
            Debug.WriteLine("DiscoveryService: Socket failure detected on IP " + socket.Address.ToString());
            UdpSocket skt;
            if (listOfSockets.TryRemove(socket.Address, out skt))
            {
                Debug.WriteLine("DiscoveryService: " + socket.Address.ToString() + " Socket removed successfully");
            }
            else
            {
                Debug.WriteLine("DiscoveryService: " + socket.Address.ToString() + " Socket remove failed");
            }
        }

        //SSDP process
        private void ProcessSSDPMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                return;

            DiscoveredDevice ssdpDevice;
            DiscoveryType foundwith = DiscoveryType.MSEARCH;
            DiscoveryEvent discovEvent = DiscoveryEvent.DeviceFound;

            if (message.Contains("M-SEARCH"))
            {
                return;
            }
            else if (message.Contains("NOTIFY"))
            {
                foundwith = DiscoveryType.NOTIFY;

                if (message.Contains("DiscoveryService:alive"))
                    discovEvent = DiscoveryEvent.DeviceFound;
                else if (message.Contains("DiscoveryService:byebye"))
                    discovEvent = DiscoveryEvent.DeviceRemoved;
            }
            else if (message.Contains("HTTP/1.1 200 OK"))
            {
                foundwith = DiscoveryType.MSEARCH;
            }

            if (DiscoveredDevice.TryParse(message, out ssdpDevice))
            {
                try
                {
                    ValidateDiscoveredDevice(ssdpDevice, foundwith);
                    var ok = FilterDevice(ssdpDevice, foundwith);
                    if (ok)
                    {
                        OnDeviceStatusUpdate(ssdpDevice, foundwith, discovEvent);
                    }

                }
                catch (Exception) { }
            }
        }
        private void ValidateDiscoveredDevice(DiscoveredDevice dv, DiscoveryType foundWith)
        {
            if (foundWith == DiscoveryType.NOTIFY && (string.IsNullOrWhiteSpace(dv.USN) || string.IsNullOrWhiteSpace(dv.Location)))
            {
                throw new Exception("Invalid device details");
            }
        }
        private bool FilterDevice(DiscoveredDevice dv, DiscoveryType foundWith)
        {
            if (FilterByService == null || FilterByService.Length == 0)
                return true;
            else if (Array.IndexOf(FilterByService, dv.Target) > -1)
                return true;
            else
                return false;
        }

        //init
        private void InitSockets()
        {
            var ifaces = FindNetworkInterfaces();

            var remoteIP = IPAddress.Parse(MULTICAST_IPV4);
            var ep = new IPEndPoint(remoteIP, MULTICAST_PORT);

            foreach (var iface in ifaces)
            {
                if (!listOfSockets.ContainsKey(iface))
                {
                    UdpSocket skt = new UdpSocket(iface, ep, new Action<UdpSocket, string>(OnUdpMessage), new Action<UdpSocket, Exception>(OnUdpError));
                    listOfSockets.AddOrUpdate(iface, skt, (x, y) => { return y; });
                }
            }
        }
        private void InitSearchTimer()
        {
            bool wasrunnig = false;
            if (tmrSearch == null)
            {
                tmrSearch = new Timer();
                tmrSearch.Elapsed += TmrSearch_Elapsed;

            }
            else
            {
                wasrunnig = tmrSearch.Enabled;
                tmrSearch.Stop();
            }

            tmrSearch.Interval = TimeSpan.FromSeconds(SearchCycleGapInSeconds).TotalMilliseconds;
            if (wasrunnig) tmrSearch.Start();
        }

        //startup
        private static List<IPAddress> FindNetworkInterfaces(bool addLoopback = false)
        {
            var ninterfaces = NetworkInterface.GetAllNetworkInterfaces();
            var activeInterfaces = new System.Collections.Concurrent.ConcurrentBag<IPAddress>();
            Parallel.ForEach<NetworkInterface>(ninterfaces, new ParallelOptions() { MaxDegreeOfParallelism = 5 },
                iface =>
                {
                    if (iface.OperationalStatus == OperationalStatus.Up)
                    {
                        var ipprops = iface.GetIPProperties();
                        if (ipprops.UnicastAddresses != null && ipprops.UnicastAddresses.Count > 0)
                        {
                            var ipV4List = ipprops.UnicastAddresses
                            .Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork && x.IsDnsEligible)
                            .ToList();

                            if (ipV4List.Count > 0)
                            {
                                activeInterfaces.Add(ipV4List.First().Address);
                            }
                        }
                    }
                });

            if (addLoopback) activeInterfaces.Add(IPAddress.Loopback);
            return activeInterfaces.ToList();
        }
        private void BeginStockets()
        {
            foreach (var item in listOfSockets)
            {
                item.Value.BeginListen();
            }
        }
        private void NetworkChange_NetworkAddressChanged(object sender, EventArgs e)
        {
            var ifaces = FindNetworkInterfaces();
            var remoteIP = IPAddress.Parse(MULTICAST_IPV4);
            var ep = new IPEndPoint(remoteIP, MULTICAST_PORT);

            foreach (var iface in ifaces)
            {
                if (!listOfSockets.ContainsKey(iface))
                {
                    UdpSocket skt = new UdpSocket(iface, ep, new Action<UdpSocket, string>(OnUdpMessage), new Action<UdpSocket, Exception>(OnUdpError));
                    listOfSockets.AddOrUpdate(iface, skt, (x, y) => { return y; });
                    if (tmrSearch.Enabled) skt.BeginListen();
                }
            }

            foreach (var key in listOfSockets.Keys)
            {
                if (ifaces.IndexOf(key) == -1)
                {
                    UdpSocket skt;
                    if (listOfSockets.TryGetValue(key, out skt))
                    {
                        skt.Dispose();
                        listOfSockets.TryRemove(key, out skt);
                    }
                }
            }
        }

        //--
        public void StartAsync()
        {
            InitSockets();
            //
            BeginStockets();
            //
            NetworkChange.NetworkAddressChanged += NetworkChange_NetworkAddressChanged;
            //
            tmrSearch.Start();
            Task.Factory.StartNew(() => { TmrSearch_Elapsed(this, null); });
            //
            IsActive = true;
        }
        public void Stop()
        {
            try
            {
                NetworkChange.NetworkAddressChanged -= NetworkChange_NetworkAddressChanged;
                //
                tmrSearch.Stop();
                //
                foreach (var item in listOfSockets)
                {
                    item.Value.Dispose();
                }
                //
                listOfSockets.Clear();
                //
                IsActive = false;
            }
            catch (Exception)
            { }

        }
        public void Dispose()
        {
            try
            {
                if (tmrSearch != null) { tmrSearch.Stop(); }
                foreach (var item in listOfSockets)
                {
                    try
                    {
                        item.Value.Dispose();
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
        }


        /// <summary>
        /// Fully qualified searvice URN
        /// </summary>
        public string[] FilterByService { get; set; }

        /// <summary>
        /// Interval between two search packets in seconds. Default 1 second.
        /// </summary>
        public uint SearchPacketIntervalInSeconds { get; set; }
        /// <summary>
        /// Number for M-SEARCH packets to send in a search cycle. Default is 3.
        /// </summary>
        public uint SearchPacketsToSend { get; set; }
        /// <summary>
        /// Interval between two search cycles in seconds. Default is 120 (2 minutes).
        /// </summary>
        public uint SearchCycleGapInSeconds { get; set; }
        /// <summary>
        /// Is started
        /// </summary>
        public bool IsActive { get; private set; }
    }
}

#region Backup
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Net.Sockets;
//using System.Net;
//using System.Timers;
//using System.Diagnostics;
//using System.Net.NetworkInformation;

//namespace EvoInc.Net.UPnP.Discovery
//{
//    public class DiscoveryService : IDisposable
//    {
//        /// <summary>
//        /// Default interval is 5 minutes
//        /// </summary>
//        public const uint DEFAULT_SEARCH_INTERVAL = 5;
//        /// <summary>
//        /// Minimum search interval
//        /// </summary>
//        public const uint MINIMUM_SEARCH_INTERVAL = 1;
//        /// <summary>
//        /// 
//        /// </summary>
//        public const string MULTICAST_IPV4 = "239.255.255.250";
//        /// <summary>
//        /// 
//        /// </summary>
//        public const int MULTICAST_PORT = 1900;

//        private IPAddress ipMultiCast;
//        private EndPoint epRemote;
//        private EndPoint epLocal;

//        private byte[] buffer = new byte[2048];
//        private Socket sktSearcher;
//        private UdpClient multicastListener;

//        private Timer tmrSearch;

//        public event EventHandler<DiscoveredEventArgs> DeviceStatusUpdate;

//        public DiscoveryService()
//        {
//            ipMultiCast = IPAddress.Parse(MULTICAST_IPV4);
//            epRemote = new IPEndPoint(ipMultiCast, MULTICAST_PORT);
//            epLocal = new IPEndPoint(IPAddress.Any, 0);

//            SearchCycleGapInSeconds = 120;
//            SearchPacketIntervalInSeconds = 1;
//            SearchPacketsToSend = 3;

//            InitSearchTimer();

//            LocalEps();
//        }

//        private void OnDeviceStatusUpdate(DiscoveredDevice dv, DiscoveryType type, DiscoveryEvent evt)
//        {
//            if (DeviceStatusUpdate != null)
//            {
//                DeviceStatusUpdate(this, new DiscoveredEventArgs()
//                {
//                    Device = dv,
//                    Event = evt,
//                    DiscoveredUsing = type
//                });
//            }
//        }

//        private List<IPAddress> GetNetworkInterfaces()
//        {
//            var ninterfaces = NetworkInterface.GetAllNetworkInterfaces();
//            var activeInterfaces = new System.Collections.Concurrent.ConcurrentBag<IPAddress>();
//            Parallel.ForEach<NetworkInterface>(ninterfaces, new ParallelOptions() { MaxDegreeOfParallelism = 5 },
//                iface =>
//                {
//                    if (iface.OperationalStatus == OperationalStatus.Up)
//                    {
//                        var ipprops = iface.GetIPProperties();
//                        if (ipprops.UnicastAddresses != null && ipprops.UnicastAddresses.Count > 0)
//                        {
//                            var ipV4List = ipprops.UnicastAddresses
//                            .Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork && x.IsDnsEligible)
//                            .ToList();

//                            if (ipV4List.Count > 0)
//                            {
//                                activeInterfaces.Add(ipV4List.First().Address);
//                            }
//                        }
//                    }
//                });


//            return activeInterfaces.ToList();
//        }
//        private string GetMsearch()
//        {
//            //"DiscoveryService:all";
//            //upnp:rootdevice

//            string st = "DiscoveryService:all";

//            //if (FilterByService != null && FilterByService.Length == 0)
//            //{
//            //    st = FilterByService[0];
//            //}

//            StringBuilder builder = new StringBuilder();
//            builder.Append("M-SEARCH * HTTP/1.1\r\n");
//            builder.Append("Host: 239.255.255.250:1900\r\n");
//            builder.Append("Man: \"DiscoveryService:discover\"\r\n");
//            builder.Append("ST: ").Append(st).Append("\r\n");
//            builder.Append("User-Agent: UPnP/1.0 DLNADOC/1.50 EvoInc/1.0\r\n");
//            builder.Append("MX: 3\r\n");
//            builder.Append("\r\n");
//            builder.Append("\r\n");

//            return builder.ToString();
//        }
//        private void TmrSearch_Elapsed(object sender, ElapsedEventArgs e)
//        {
//            try
//            {
//                Debug.WriteLine("DiscoveryService: Sending {0} M-SEARCH Packets", SearchPacketsToSend);
//                for (int i = 0; i < SearchPacketsToSend; i++)
//                {
//                    string msearch = GetMsearch();
//                    var bytes = Encoding.ASCII.GetBytes(msearch);
//                    Debug.WriteLine("DiscoveryService: Sending M-SEARCH #{0}", i + 1);
//                    sktSearcher.SendTo(bytes, epRemote);
//                    Debug.WriteLine("DiscoveryService: Sleeping M-SEARCH for {0}", TimeSpan.FromSeconds(SearchPacketIntervalInSeconds));
//                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(SearchPacketIntervalInSeconds));
//                }

//            }
//            catch (Exception) { }
//        }
//        private void SktSearcher_Callback(IAsyncResult result)
//        {
//            try
//            {
//                EndPoint epClient = new IPEndPoint(IPAddress.Parse(MULTICAST_IPV4), MULTICAST_PORT);
//                int length = sktSearcher.EndReceiveFrom(result, ref epClient);
//                var message = Encoding.ASCII.GetString(buffer, 0, length);
//                Debug.WriteLine(message);
//                ProcessSSDPMessage(message);

//                sktSearcher.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(SktSearcher_Callback), null);
//            }
//            catch (Exception) { }
//        }
//        private void MulticastListener_Callback(IAsyncResult result)
//        {
//            try
//            {
//                IPEndPoint epClient = new IPEndPoint(IPAddress.Parse(MULTICAST_IPV4), MULTICAST_PORT);
//                var bytes = multicastListener.EndReceive(result, ref epClient);
//                var message = Encoding.ASCII.GetString(bytes);
//                Debug.WriteLine(message);
//                ProcessSSDPMessage(message);

//                multicastListener.BeginReceive(new AsyncCallback(MulticastListener_Callback), null);
//            }
//            catch (Exception) { }
//        }

//        private void ProcessSSDPMessage(string message)
//        {
//            if (string.IsNullOrWhiteSpace(message))
//                return;

//            DiscoveredDevice ssdpDevice;
//            DiscoveryType foundwith = DiscoveryType.MSEARCH;
//            DiscoveryEvent discovEvent = DiscoveryEvent.DeviceFound;

//            if (message.Contains("M-SEARCH"))
//            {
//                return;
//            }
//            else if (message.Contains("NOTIFY"))
//            {
//                foundwith = DiscoveryType.NOTIFY;

//                if (message.Contains("DiscoveryService:alive"))
//                    discovEvent = DiscoveryEvent.DeviceFound;
//                else if (message.Contains("DiscoveryService:byebye"))
//                    discovEvent = DiscoveryEvent.DeviceRemoved;
//            }
//            else if (message.Contains("HTTP/1.1 200 OK"))
//            {
//                foundwith = DiscoveryType.MSEARCH;
//            }

//            if (DiscoveredDevice.TryParse(message, out ssdpDevice))
//            {
//                try
//                {
//                    ValidateDiscoveredDevice(ssdpDevice, foundwith);
//                    var ok = FilterDevice(ssdpDevice, foundwith);
//                    if (ok)
//                    {
//                        OnDeviceStatusUpdate(ssdpDevice, foundwith, discovEvent);
//                    }

//                }
//                catch (Exception) { }
//            }
//        }

//        private void ValidateDiscoveredDevice(DiscoveredDevice dv, DiscoveryType foundWith)
//        {
//            if (foundWith == DiscoveryType.NOTIFY && (string.IsNullOrWhiteSpace(dv.USN) || string.IsNullOrWhiteSpace(dv.Location)))
//            {
//                throw new Exception("Invalid device details");
//            }
//        }
//        private bool FilterDevice(DiscoveredDevice dv, DiscoveryType foundWith)
//        {
//            if (FilterByService == null || FilterByService.Length == 0)
//                return true;
//            else if (Array.IndexOf(FilterByService, dv.Target) > -1)
//                return true;
//            else
//                return false;
//        }

//        private void InitSockets()
//        {
//            sktSearcher = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
//            sktSearcher.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
//            sktSearcher.Bind(epLocal);
//            try
//            {
//                multicastListener = new UdpClient();
//                multicastListener.EnableBroadcast = true;
//                multicastListener.ExclusiveAddressUse = false;
//                multicastListener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
//                multicastListener.Client.Bind(new IPEndPoint(IPAddress.Any, 0));
//                multicastListener.JoinMulticastGroup(ipMultiCast);
//            }
//            catch (Exception) { }
//        }
//        private void InitSearchTimer()
//        {
//            bool wasrunnig = false;
//            if (tmrSearch == null)
//            {
//                tmrSearch = new Timer();
//                tmrSearch.Elapsed += TmrSearch_Elapsed;

//            }
//            else
//            {
//                wasrunnig = tmrSearch.Enabled;
//                tmrSearch.Stop();
//            }

//            tmrSearch.Interval = TimeSpan.FromSeconds(SearchCycleGapInSeconds).TotalMilliseconds;
//            if (wasrunnig) tmrSearch.Start();
//        }

//        public void StartAsync()
//        {
//            InitSockets();
//            //
//            sktSearcher.BeginReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref epRemote, new AsyncCallback(SktSearcher_Callback), null);
//            //
//            multicastListener.BeginReceive(new AsyncCallback(MulticastListener_Callback), null);
//            //
//            tmrSearch.Start();
//            Task.Factory.StartNew(() => { TmrSearch_Elapsed(this, null); });
//        }
//        public void Stop()
//        {
//            try
//            {
//                tmrSearch.Stop();
//                sktSearcher.Close();
//                multicastListener.Close();
//                sktSearcher.Dispose();
//            }
//            catch (Exception)
//            { }

//        }
//        public void Dispose()
//        {
//            try
//            {
//                if (tmrSearch != null) { tmrSearch.Stop(); }
//                if (sktSearcher != null)
//                {
//                    if (sktSearcher.Connected) sktSearcher.Disconnect(false);
//                    sktSearcher.Close();
//                }
//                if (this.multicastListener != null)
//                {
//                    multicastListener.Close();
//                }
//            }
//            catch (Exception) { }
//        }

//        public string[] FilterByService { get; set; }

//        /// <summary>
//        /// Interval between two search packets in seconds. Default 1 second.
//        /// </summary>
//        public uint SearchPacketIntervalInSeconds { get; set; }
//        /// <summary>
//        /// Number for M-SEARCH packets to send in a search cycle. Default is 3.
//        /// </summary>
//        public uint SearchPacketsToSend { get; set; }
//        /// <summary>
//        /// Interval between two search cycles in seconds. Default is 120 (2 minutes).
//        /// </summary>
//        public uint SearchCycleGapInSeconds { get; set; }



//        private void LocalEps()
//        {
//            var ifcae = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
//            foreach (var item in ifcae)
//            {
//                // var ipprops = item.

//            }
//        }
//    }
//}  
#endregion

