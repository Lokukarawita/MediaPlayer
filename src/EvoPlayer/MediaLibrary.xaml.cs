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
using EvoPlayer.Core.Data;

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

        private bool shutDownRequested = false;

        public MediaLibrary()
        {
            InitializeComponent();

            InitMediaLibrary();
        }

        private void InitMediaLibrary()
        {
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
            //netssdp
            netssdp.DeviceStatusUpdate += Netssdp_DeviceStatusUpdate;
            netssdp.SearchPacketsToSend = 1;
            netssdp.FilterByService = new string[] {
                "urn:schemas-upnp-org:device:MediaServer:1"
            };

            tmrNewDeviceQueue.Start();
            netssdp.StartAsync();

            //playlists
            var plists =  DB.GetPlaylists();
            foreach (var item in plists)
            {
                TreeViewItem trviPli = new TreeViewItem();
                trviPli.Header = item.PlaylistName;
                trviPli.Tag = item.Id;
                trviPlaylist.Items.Add(trviPli);
            }
        }

        private void TmrNewDeviceQueue_Tick(object sender, EventArgs e)
        {
            if (newdeviceQueue.Count > 0)
            {
                Device d;
                if (newdeviceQueue.TryDequeue(out d))
                {
                    if (!currentDeviceList.ContainsKey(d.Udn))
                    {
                        if (currentDeviceList.TryAdd(d.Udn, d))
                        {
                            AddDeviceToTree(d);
                        }
                    }
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
                    return image;
                }
            }
            catch (Exception)
            {
                return Properties.Resources.server_icon;
            }
        }

        private void AddDeviceToTree(Device device)
        {
            var model = device.ModelName;
            //var group = GetGroup(device.ModelName);

            var pngIcon = device.Icons.FirstOrDefault(x => x.Height == 48 && x.MimeType == "image/png");
            var jpgIcon = device.Icons.FirstOrDefault(x => x.Height == 48 && x.MimeType == "image/jpeg");

            System.Drawing.Image image = null;

            if (pngIcon != null)
            {
                image = GetDeviceImage(device.ModelName, pngIcon.Url);
            }
            else
            {
                image = GetDeviceImage(device.ModelName, jpgIcon.Url);
            }



            StackPanel stp = new StackPanel();
            stp.Orientation = Orientation.Horizontal;

            System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(image);

            var bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                bmp.GetHbitmap(),
                IntPtr.Zero,
                System.Windows.Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(image.Width, image.Height));
            Image ctrlImg = new Image();
            ctrlImg.Stretch = Stretch.Uniform;
            ctrlImg.Width = 32;
            ctrlImg.Height = 32;
            ctrlImg.Source = bitSrc;



            Viewbox vbimage = new Viewbox();
            vbimage.Width = 32;
            vbimage.Height = 32;
            vbimage.Child = ctrlImg;

            TextBlock tb = new TextBlock();
            tb.Text = device.FriendlyName.Split(':')[1].Trim();
            tb.Margin = new Thickness(8, 8, 0, 0);

            stp.Children.Add(vbimage);
            stp.Children.Add(tb);

            TreeViewItem item = new TreeViewItem();
            item.Header = stp;

            trviNetwork.Items.Add(item);
        }


        private void MediaLibrary_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (!shutDownRequested)
            {
                e.Cancel = true;
                if (this.IsVisible) this.Hide();
            }
            else
            {
                tmrNewDeviceQueue.Stop();
                netssdp.Stop();
            }
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


        public void ShutDown()
        {
            shutDownRequested = true;
            this.Close();
        }

    }
}
