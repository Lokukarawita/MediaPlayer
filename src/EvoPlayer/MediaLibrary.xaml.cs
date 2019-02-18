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
using MaterialDesignThemes.Wpf;
using EvoPlayer.Core.Data.Domain;

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

        //ops
        private bool shutDownRequested = false;
        private object currentView = null;

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
            var plists = DB.GetPlaylists();
            foreach (var item in plists)
            {
                MLTree_AddPlaylistItem(item);
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
                            MLTree_AddNetworkItem(d);
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




        private void MLTree_AddNetworkItem(Device device)
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
        private void MLTree_AddPlaylistItem(Playlist pl)
        {
            ContextMenu menu = new ContextMenu();
            var menuPLDelete = new MenuItem();
            menuPLDelete.Header = "Delete";
            menuPLDelete.Click += MenuPLDelete_Click;
            menuPLDelete.Tag = pl.Id;
            menu.Items.Add(menuPLDelete);

            TreeViewItem trviPli = new TreeViewItem();
            trviPli.Header = pl.PlaylistName;
            trviPli.Tag = pl.Id;
            trviPli.ContextMenu = menu;
            trviPlaylist.Items.Add(trviPli);
        }


        private void MenuPLDelete_Click(object sender, RoutedEventArgs e)
        {
            if (trvMLLoc.SelectedItem != null)
            {
                var tvi = (TreeViewItem)trvMLLoc.SelectedItem;
                var tvp = (TreeViewItem)(tvi.Parent != null ? tvi.Parent : null);
                if (tvi.Tag != null && tvi.Header.ToString() != "Now Playing" && tvp != null && tvp.Name == nameof(trviPlaylist))
                {
                    var plId = (int)tvi.Tag;

                    var rslt = MessageBox.Show(this, "Delete playlist " + tvi.Header + " ?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (rslt == MessageBoxResult.Yes)
                    {
                        DB.DeletePlaylist(plId);

                        for (int i = 0; i < trviPlaylist.Items.Count; i++)
                        {
                            var trvi = (TreeViewItem)trviPlaylist.Items[i];
                            var id = (int)trvi.Tag;
                            if (id == plId)
                            {
                                trviPlaylist.Items.Remove(trvi);
                                break;
                            }
                        }
                    }
                }
            }
        }

        private void mnuCreatePlaylist_Click(object sender, RoutedEventArgs e)
        {
            dlgCreatePL.IsOpen = true;
        }

        private void dlgCreatePL_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            try
            {
                var param = (bool)eventArgs.Parameter;
                if (param)
                {
                    var name = txtNewPlaylistName.Text;
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        var newId = DB.CreatePlaylist(name);
                        var pl = DB.GetPlaylist(newId);
                        MLTree_AddPlaylistItem(pl);
                        //trviPlaylist.Items.Add()
                    }
                }
            }
            catch (Exception)
            {
                
            }
            finally
            {
                txtNewPlaylistName.Text = "";
            }

        }



        public void ShutDown()
        {
            shutDownRequested = true;
            this.Close();
        }

        private void trvMLLoc_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            TreeViewItem tvSelected = null, tvParent = null;

            if (e.NewValue != null)
            {
                if (e.NewValue.GetType() == typeof(TreeViewItem))
                {
                    tvSelected = e.NewValue as TreeViewItem;
                    if (tvSelected.Parent != null)
                    {
                        tvParent = tvSelected.Parent as TreeViewItem;
                    }
                }
            }
            //-- validate --
            if (tvParent == null)
                return;

            ctrlView.Children.Clear();

            //-- process --
            if (tvParent.Name == "trviPlaylist")
            {
                var plId = (int)tvSelected.Tag;
                var playlist = DB.GetPlaylist(plId);

                var playlistView = new Comp.ML.MLPlaylistView(playlist);
                this.currentView = playlistView;
                ctrlView.Children.Add(playlistView);
            }
            else if (tvSelected.Name == "trviMusic")
            {
                var lmv = new Comp.ML.MLLocalMediaView();
                this.currentView = lmv;
                ctrlView.Children.Add(lmv);
            }
        }
    }
}
