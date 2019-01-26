using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace BetterSSDP
{
    class Class1
    {
        private static List<IPAddress> GetNetworkInterfaces(bool addLoopback = false)
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

        static string GetMsearch()
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
            builder.Append("Host: 239.255.255.250:1900\r\n");
            builder.Append("Man: \"ssdp:discover\"\r\n");
            builder.Append("ST: ").Append(st).Append("\r\n");
            builder.Append("User-Agent: UPnP/1.0 DLNADOC/1.50 EvoInc/1.0\r\n");
            builder.Append("MX: 3\r\n");
            builder.Append("\r\n");
            builder.Append("\r\n");

            return builder.ToString();
        }

        static List<UdpSocket> skts = new List<UdpSocket>();
        static IPEndPoint remoteEp = new IPEndPoint(IPAddress.Parse("239.255.255.250"), 1900);

        static void Main(string[] args)
        {
            NetworkChange.NetworkAvailabilityChanged += NetworkChange_NetworkAvailabilityChanged;

            var ips = GetNetworkInterfaces(false);
            foreach (var ip in ips)
            {

                UdpSocket s = new UdpSocket(ip, remoteEp,
                    new Action<UdpSocket, string>(OnMessage),
                    new Action<UdpSocket, Exception>(OnError));
                skts.Add(s);
                skts[skts.Count - 1].BeginListen();
            }

            System.Threading.Thread.Sleep(TimeSpan.FromSeconds(10));

            Task.Factory.StartNew(() =>
            {
                var message = GetMsearch();
                var data = Encoding.ASCII.GetBytes(message);
                foreach (var sk in skts)
                {
                    try
                    {
                        sk.Send(data);

                    }
                    catch (Exception) { }
                }
            });

            Console.ReadLine();
        }

        private static void NetworkChange_NetworkAvailabilityChanged(object sender, NetworkAvailabilityEventArgs e)
        {
            Console.WriteLine();
        }

        static void OnMessage(UdpSocket udp, string message)
        {
            Console.WriteLine(message);

        }
        static void OnError(UdpSocket udp, Exception error)
        {
            Debug.WriteLine(error.Message);
        }
    }

    internal class UdpSocket : IDisposable
    {
        private EndPoint rEp;

        private Socket unicastSkt, multicastSkt;
        private Action<UdpSocket, string> cbOnMessage;
        private Action<UdpSocket, Exception> cbOnError;
        private byte[] unicastBuffer = new byte[1024];
        private byte[] multicastBuffer = new byte[1024];

        public UdpSocket(IPAddress localAddress, IPEndPoint remoteEndpoint, Action<UdpSocket, string> onMessage, Action<UdpSocket, Exception> errorCallback)
        {
            cbOnMessage = onMessage;
            cbOnError = errorCallback;
            rEp = (EndPoint)remoteEndpoint;
            Address = localAddress;

            unicastSkt = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            unicastSkt.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);

            multicastSkt = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            multicastSkt.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
        }

        private void OnUnicastMessage(IAsyncResult ar)
        {
            try
            {
                EndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                var dataLen = unicastSkt.EndReceiveFrom(ar, ref sender);
                var message = Encoding.ASCII.GetString(unicastBuffer, 0, dataLen);

                if (cbOnMessage != null) cbOnMessage(this, message);

                unicastSkt.BeginReceiveFrom(unicastBuffer, 0, unicastBuffer.Length, SocketFlags.None, ref rEp, new AsyncCallback(this.OnUnicastMessage), null);
            }
            catch (Exception ex)
            {
                if (cbOnError != null) cbOnError(this, ex);
            }
        }
        private void OnMulticasttMessage(IAsyncResult ar)
        {
            try
            {
                EndPoint sender = new IPEndPoint(IPAddress.Any, 0);
                var dataLen = multicastSkt.EndReceive(ar);
                var message = Encoding.ASCII.GetString(multicastBuffer, 0, dataLen);

                if (cbOnMessage != null) cbOnMessage(this, message);

                multicastSkt.BeginReceive(multicastBuffer, 0, multicastBuffer.Length, SocketFlags.None, new AsyncCallback(OnMulticasttMessage), null);
            }
            catch (Exception ex)
            {
                if (cbOnError != null) cbOnError(this, ex);
            }
        }

        public void BeginListen()
        {
            //
            unicastSkt.Bind(new IPEndPoint(Address, 0));
            unicastSkt.BeginReceiveFrom(unicastBuffer, 0, unicastBuffer.Length, SocketFlags.None, ref rEp, new AsyncCallback(this.OnUnicastMessage), null);
            //
            var ipep = (IPEndPoint)rEp;
            multicastSkt.Bind(new IPEndPoint(Address, ipep.Port));
            multicastSkt.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(ipep.Address, Address));
            multicastSkt.BeginReceive(multicastBuffer, 0, multicastBuffer.Length, SocketFlags.None, new AsyncCallback(OnMulticasttMessage), null);
        }
        public bool Send(byte[] message)
        {
            try
            {
                unicastSkt.SendTo(message, rEp);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("UdpSocket: {0}\n{1}", ex.Message, ex.ToString());
                return false;
            }

        }

        public void Dispose()
        {
            try
            {
                if (unicastSkt != null)
                {
                    unicastSkt.Shutdown(SocketShutdown.Both);
                    unicastSkt.Close();
                }
                if (multicastSkt != null)
                {
                    multicastSkt.Shutdown(SocketShutdown.Both);
                    unicastSkt.Close();
                }
            }
            catch (Exception) { }
        }

        public IPAddress Address { get; set; }
    }
}
