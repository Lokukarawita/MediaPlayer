using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EvoInc.Net.UPnP.Discovery;
using ParkSquare.UPnP;
using System.Collections.Concurrent;
using System.Windows.Threading;

namespace EvoPlayer
{
    /// <summary>
    /// Interaction logic for MediaLibrary.xaml
    /// </summary>
    public partial class MediaLibrary : Window
    {
        //--Network--
        private DiscoveryService netssdp;
        private ConcurrentQueue<Device> newdeviceQueue;
        private ConcurrentDictionary<string, Device> currentDeviceList;
        private DispatcherTimer tmrNewDeviceQueue;

        public MediaLibrary()
        {
            InitializeComponent();

            //event
            this.Closing += MediaLibrary_Closing;

            //ssdp
            tmrNewDeviceQueue = new DispatcherTimer();
            tmrNewDeviceQueue.Interval = TimeSpan.FromSeconds(2);
            tmrNewDeviceQueue.Tick += TmrNewDeviceQueue_Tick;
            //
            newdeviceQueue = new ConcurrentQueue<Device>();
            currentDeviceList = new ConcurrentDictionary<string, Device>();
            netssdp = new DiscoveryService();
            netssdp.DeviceStatusUpdate += Netssdp_DeviceStatusUpdate;

        }

        private void TmrNewDeviceQueue_Tick(object sender, EventArgs e)
        {
            if (newdeviceQueue.Count > 0)
            {
                Device d;
                if (newdeviceQueue.TryDequeue(out d))
                {
                    //
                }
            }
        }

        private System.Drawing.Image GetDeviceImage(string serverType, Uri uri)
        {
            try
            {
                using (System.Net.WebClient client = new System.Net.WebClient())
                {
                    var data = client.DownloadData(uri.ToString());
                    var ms = new System.IO.MemoryStream(data);
                    var image = System.Drawing.Image.FromStream(ms);

                }

            }
            catch (Exception)
            {
                return 
            }
        }

        private void AddDeviceToTree(Device device)
        {
            var model = device.ModelName;
            //var group = GetGroup(device.ModelName);

            var pngIcon = device.Icons.FirstOrDefault(x => x.Height == 48 && x.MimeType == "image/png");
            var jpgIcon = device.Icons.FirstOrDefault(x => x.Height == 48 && x.MimeType == "image/jpeg");

            var idx = 0;
            if (pngIcon != null)
            {
                idx = GetDeviceImage(device.ModelName, pngIcon.Url);
            }
            else
            {
                idx = GetDeviceImage(device.ModelName, jpgIcon.Url);
            }

            var item = new TreeNode()
            {
                Text = device.FriendlyName.Split(':')[1].Trim(),
                ImageIndex = idx,
                SelectedImageIndex = idx,
                Tag = device.Udn
            };
            treeView1.Nodes[0].Nodes.Add(item);
            if (!treeView1.Nodes[0].IsExpanded)
                treeView1.Nodes[0].Expand();

        }


        private void MediaLibrary_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            tmrNewDeviceQueue.Stop();
            netssdp.Stop();
        }

        private void Netssdp_DeviceStatusUpdate(object sender, DiscoveredEventArgs e)
        {
            if (e.Event == DiscoveryEvent.DeviceFound)
            {
                SimpleHttpClient client = new SimpleHttpClient();
                DeviceFactory factory = new DeviceFactory(client);

                var device = factory.Create(new Uri(e.Device.Location));
                if (device == null)
                {
                    return;
                }


                if (device != null)
                {
                    newdeviceQueue.Enqueue(device);
                }
            }
        }
    }
}
