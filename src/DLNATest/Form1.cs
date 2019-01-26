using EvoInc.Net.UPnP.Discovery;
using EvoInc.Net.UPnP.XML.DIDLLite;
using ParkSquare.UPnP;
//using Rssdp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DLNATest
{
    public partial class Form1 : Form
    {

        List<Device> devicesList = new List<Device>();
        //SsdpDeviceLocator locator = new SsdpDeviceLocator();
        DiscoveryService locator = new DiscoveryService();
        ConcurrentQueue<Device> foundDevices = new ConcurrentQueue<Device>();
        ConcurrentDictionary<string, Device> addedServers = new ConcurrentDictionary<string, Device>();



        public Form1()
        {
            InitializeComponent();

            locator.FilterByService = new string[] {
                "urn:schemas-upnp-org:device:MediaServer:1"
            };
            locator.SearchPacketIntervalInSeconds = 1;
            locator.DeviceStatusUpdate += locator_DeviceStatusUpdate;
            locator.StartAsync();

            //locator.NotificationFilter = "urn:schemas-upnp-org:device:MediaServer:1";
            //locator.DeviceAvailable += Locator_DeviceAvailable;
            //locator.StartListeningForNotifications();
            //var x = locator.SearchAsync().Result;
        }

        void locator_DeviceStatusUpdate(object sender, DiscoveredEventArgs e)
        {
            SimpleHttpClient client = new SimpleHttpClient();
            DeviceFactory factory = new DeviceFactory(client);
            var device = factory.Create(new Uri(e.Device.Location));
            if (device == null)
            {
                return;
            }
            devicesList.Add(device);
            foundDevices.Enqueue(device);
        }

        //private void Locator_DeviceAvailable(object sender, DeviceAvailableEventArgs e)
        //{
        //    SimpleHttpClient client = new SimpleHttpClient();
        //    DeviceFactory factory = new DeviceFactory(client);
        //    var device = factory.Create(e.DiscoveredDevice.DescriptionLocation);
        //    if (device == null)
        //    {
        //        return;
        //    }
        //    devicesList.Add(device);
        //    foundDevices.Enqueue(device);
        //}

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

        private int GetDeviceImage(string serverType, Uri uri)
        {
            using (System.Net.WebClient client = new System.Net.WebClient())
            {
                var data = client.DownloadData(uri.ToString());
                var ms = new System.IO.MemoryStream(data);
                var image = Image.FromStream(ms);
                imageList1.Images.Add(image);
                var idx = imageList1.Images.Count - 1;
                return idx;
            }
        }

        private ListViewGroup GetGroup(string name)
        {
            foreach (ListViewGroup item in listView1.Groups)
            {
                if (item.Header == name)
                    return item;

            }
            var group = new ListViewGroup()
            {
                Header = name,
                Name = name.Replace(" ", "_")
            };
            listView1.Groups.Add(group);
            return group;
        }




        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!foundDevices.IsEmpty)
            {
                Device dv;
                if (foundDevices.TryDequeue(out dv))
                {
                    if (!addedServers.ContainsKey(dv.Udn))
                    {
                        addedServers.TryAdd(dv.Udn, dv);
                        AddDeviceToTree(dv);
                    }

                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null && e.Node.Parent.Name == "Node1")
            {
                bredcrumMain.Controls.Clear();
            }

            if (e.Node.Tag != null)
            {
                selectedDevice = addedServers[e.Node.Tag.ToString()];
                controlService = selectedDevice.Services.FirstOrDefault(x => x.ServiceType.Name == "ContentDirectory");
                currentObjectId = "0";
                Browse(currentObjectId);

                LinkLabel lb = new LinkLabel();
                lb.Text = selectedDevice.FriendlyName.Split(':')[1].Trim();
                lb.Tag = "0";
                lb.Click += BredcrumbItem_Click;
                lb.AutoSize = true;
                bredcrumMain.Controls.Add(lb);
            }

        }


        private Device selectedDevice = null;
        private Service controlService = null;
        private string currentObjectId = "0";

        private string currentUserstate = null;
        private Dictionary<string, MediaItem> currentMediaItemList = new Dictionary<string, MediaItem>();

        private void Browse(string objectId = "0")
        {
            string soapBody = "<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\" s:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">" +
                "<s:Body><u:Browse xmlns:u=\"urn:schemas-upnp-org:service:ContentDirectory:1\">" +
                "<ObjectID>" + objectId + "</ObjectID>" +
                "<BrowseFlag>BrowseDirectChildren</BrowseFlag>" +
                "<Filter>dc:title,dc:date,res,res@protocolInfo,res@size,res@duration,res@resolution,res@dlna:ifoFileURI,res@pv:subtitleFileType,res@pv:subtitleFileUri,upnp:albumArtURI,upnp:album,upnp:artist,res@pxn:ResumePoint,res@pxn:ChapterList,item@pxn:ContentSourceType,item@pxn</Filter>" +
                "<StartingIndex>0</StartingIndex>" +
                "<RequestedCount>5000</RequestedCount>" +
                "<SortCriteria></SortCriteria>" +
                "</u:Browse>" +
                "</s:Body>" +
                "</s:Envelope>";
            currentUserstate = Guid.NewGuid().ToString("D");
            using (WebClient client = new WebClient())
            {
                client.Headers.Add(HttpRequestHeader.ContentType, "text/xml; charset=\"utf-8\"");
                client.Headers.Add("SOAPACTION", "\"urn:schemas-upnp-org:service:ContentDirectory:1#Browse\"");
                client.Headers.Add(HttpRequestHeader.UserAgent, "MediaPlayerFinal Browser");
                client.UploadStringCompleted += (sender, resp) =>
                {

                    if (currentUserstate == resp.UserState.ToString())
                    {
                        if (!resp.Cancelled && resp.Error == null)
                        {
                            //success
                            var responseText = resp.Result;
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(responseText);
                            var result = doc.GetElementsByTagName("Result");
                            if (result != null && result.Count > 0)
                            {
                                var didl = result[0].InnerText;
                                var decoded = System.Net.WebUtility.HtmlDecode(didl);
                                ProcessDIDL(decoded);
                            }
                        }
                        else
                        {
                            //failed
                        }
                    }
                };
                client.UploadStringAsync(controlService.ControlUrl, "POST", soapBody, currentUserstate);
            }
        }


        private void ProcessDIDL(string xml)
        {
            var parser = new DIDLLiteParser();
            var didlobj = parser.ParseDIDL(xml);

            listView1.BeginUpdate();
            listView1.Items.Clear();
            currentMediaItemList.Clear();
            imageListMediaItems.Images.Clear();

            if (didlobj.Items.Count > 0)
            {
                foreach (var didlcomp in didlobj.Items)
                {
                    if (didlcomp.GetType() == typeof(MediaContainer))
                    {
                        var container = didlcomp as MediaContainer;

                        ListViewItem item = new ListViewItem();
                        item.Text = container.Title;
                        item.Tag = container.Id;

                        if (container.UPnPClass == "object.container.storageFolder" ||
                            container.UPnPClass == "object.container.genre.musicGenre" ||
                            container.UPnPClass == "object.container")
                        {
                            listView1.LargeImageList = imageList2;
                            item.ImageKey = "i_container.png";

                        }
                        else if (container.UPnPClass == "object.container.person.musicArtist" ||
                            container.UPnPClass == "object.container.album.musicAlbum")
                        {
                            listView1.LargeImageList = imageListMediaItems;

                            if (container.AlbumArtURI == null)
                            {
                                imageListMediaItems.Images.Add(Properties.Resources.iconfinder_Music_Book_86975);
                                item.ImageIndex = imageListMediaItems.Images.Count - 1;
                            }
                            else
                            {
                                using (WebClient wc = new WebClient())
                                {
                                    try
                                    {
                                        var data = wc.DownloadData(container.AlbumArtURI);
                                        var ms = new MemoryStream(data);
                                        var img = Image.FromStream(ms);
                                        imageListMediaItems.Images.Add(img);
                                        item.ImageIndex = imageListMediaItems.Images.Count - 1;
                                    }
                                    catch (Exception)
                                    {
                                        imageListMediaItems.Images.Add(Properties.Resources.iconfinder_Music_Book_86975);
                                        item.ImageIndex = imageListMediaItems.Images.Count - 1;
                                    }

                                }
                            }
                        }

                        listView1.Items.Add(item);
                    }
                    else if (didlcomp.GetType() == typeof(MediaItem))
                    {
                        var mediaItem = didlcomp as MediaItem;

                        listView1.LargeImageList = imageListMediaItems;

                        ListViewItem item = new ListViewItem();
                        item.Text = mediaItem.Title;
                        item.Tag = mediaItem.Id;


                        if (mediaItem.AlbumArtURI == null)
                        {
                            imageListMediaItems.Images.Add(Properties.Resources.iconfinder_Music_Book_86975);
                            item.ImageIndex = imageListMediaItems.Images.Count - 1;
                        }
                        else
                        {
                            using (WebClient wc = new WebClient())
                            {
                                try
                                {
                                    var data = wc.DownloadData(mediaItem.AlbumArtURI);
                                    var ms = new MemoryStream(data);
                                    var img = Image.FromStream(ms);
                                    imageListMediaItems.Images.Add(img);
                                    item.ImageIndex = imageListMediaItems.Images.Count - 1;
                                }
                                catch (Exception)
                                {
                                    imageListMediaItems.Images.Add(Properties.Resources.iconfinder_Music_Book_86975);
                                    item.ImageIndex = imageListMediaItems.Images.Count - 1;
                                }

                            }
                        }

                        currentMediaItemList.Add(mediaItem.Id, mediaItem);
                        listView1.Items.Add(item);
                    }
                }
            }

            listView1.EndUpdate();
        }

        void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                var previousId = currentObjectId;
                currentObjectId = listView1.SelectedItems[0].Tag.ToString();
                if (currentMediaItemList.ContainsKey(currentObjectId))
                {
                    var mediaItem = currentMediaItemList[currentObjectId];
                    var resource = mediaItem.Resources.Where(x => x.Protocol.Mime.Contains("audio")).FirstOrDefault();
                    if (resource != null)
                    {
                        axWindowsMediaPlayer1.URL = resource.Uri;
                        axWindowsMediaPlayer1.Ctlcontrols.play();
                    }
                }
                else
                {
                    var lblArrow = new Label() { Text = ">" };
                    lblArrow.AutoSize = true;
                    bredcrumMain.Controls.Add(lblArrow);

                    var lblLink = new LinkLabel();
                    lblLink.AutoSize = true;
                    lblLink.Text = listView1.SelectedItems[0].Text;
                    lblLink.Tag = currentObjectId;
                    lblLink.Click += BredcrumbItem_Click;
                    bredcrumMain.Controls.Add(lblLink);
                    Browse(currentObjectId);
                }
            }
        }


        private void BredcrumbItem_Click(object sender, EventArgs e)
        {
            var ctrl = sender as Control;
            var idx = bredcrumMain.Controls.IndexOf(ctrl);
            var removeList = new List<Control>();

            if ((idx + 1) != bredcrumMain.Controls.Count)
            {


                for (int i = 0; i < bredcrumMain.Controls.Count; i++)
                {
                    if (i > idx)
                    {
                        removeList.Add(bredcrumMain.Controls[i]);
                    }
                }

                foreach (var item in removeList)
                {
                    bredcrumMain.Controls.Remove(item);
                }
            }

            currentObjectId = ctrl.Tag.ToString();
            Browse(currentObjectId);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //SsdpDeviceLocator locator = new SsdpDeviceLocator();
            //SimpleHttpClient client = new SimpleHttpClient();
            //DeviceFactory factory = new DeviceFactory(client);
            //var result = locator.SearchAsync().Result;
            //foreach (var item in result)
            //{
            //    if (item.NotificationType == "urn:schemas-upnp-org:device:MediaServer:1")
            //    {
            //        var device = factory.Create(item.DescriptionLocation);
            //        devicesList.Add(device);
            //        foundDevices.Enqueue(device);
            //    }
            //}
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            locator.Stop();
            locator.Dispose();
        }
    }
}
