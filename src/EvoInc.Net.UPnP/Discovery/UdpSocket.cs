using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EvoInc.Net.UPnP.Discovery
{
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
            unicastSkt.EnableBroadcast = true;
            unicastSkt.ExclusiveAddressUse = false;
            //unicastSkt.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.Broadcast, 1);

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
