using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EvoInc.Net.UPnP.XML.DIDLLite
{
    public sealed class DIDLLiteParser
    {
        private T GetChildValue<T>(XName name, XElement element, T defaultValue)
        {
            var elem = element.Element(name);
            if (elem == null)
                return default(T);
            else
            {
                var typeofT = typeof(T);
                if (typeofT == typeof(bool))
                {
                    var value = elem.Value;
                    value = (value == "1" ? "true" : (value == "0" ? "false" : value));
                    return (T)Convert.ChangeType(value, typeofT);

                }
                else
                {
                    return (T)Convert.ChangeType(elem.Value, typeof(T));
                }

            }

        }
        private T GetAttribute<T>(XName name, XElement element, T defaultValue)
        {
            var attrib = element.Attribute(name);
            if (attrib == null)
                return default(T);
            else
            {
                var typeofT = typeof(T);
                if (typeofT == typeof(bool))
                {
                    var value = attrib.Value;
                    value = (value == "1" ? "true" : (value == "0" ? "false" : value));
                    return (T)Convert.ChangeType(value, typeofT);

                }
                else
                {
                    return (T)Convert.ChangeType(attrib.Value, typeof(T));
                }

            }
        }


        private void ParseUPnPComponent(XElement element, MediaComponent component)
        {
            XNamespace upnp = "urn:schemas-upnp-org:metadata-1-0/upnp/";

            //id
            var id = GetAttribute<string>("id", element, null);
            if (id == null)
            {
                throw new DIDLLiteXmlException("Cannot find the Id property for container element");
            }
            else
            {
                component.Id = id;
            }

            //parent id
            var pid = GetAttribute<string>("parentID", element, null);
            if (pid == null)
            {
                throw new DIDLLiteXmlException("Cannot find the parentID property for container element");
            }
            else
            {
                component.ParentId = pid;
            }

            component.UPnPClass = GetChildValue<string>(upnp + "class", element, "Container");
        }
        private void ParseContainers(XDocument document, DIDLLiteObject didlobj)
        {
            XNamespace didlite = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/";
            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            XNamespace upnp = "urn:schemas-upnp-org:metadata-1-0/upnp/";

            var xcontainers = document.Root.Elements(didlite+ "container").ToList();
            foreach (var xcontainer in xcontainers)
            {
                MediaContainer mcontainer = new MediaContainer();

                //Parse components
                ParseUPnPComponent(xcontainer, mcontainer);

                //searchable and restricted
                mcontainer.Searchable = GetAttribute<bool>("searchable", xcontainer, true);
                mcontainer.Restricted = GetAttribute<bool>("restricted", xcontainer, true);

                //dc purl objects
                mcontainer.Title = GetChildValue<string>(dc + "title", xcontainer, "Unkown");
                mcontainer.Creator = GetChildValue<string>(dc + "creator", xcontainer, "Unknown");
                mcontainer.Description = GetChildValue<string>(dc + "description", xcontainer, "Unknown");

                //upnp objects
                mcontainer.Genre = GetChildValue<string>(upnp + "genre", xcontainer, "Unkown");
                mcontainer.AlbumArtURI = GetChildValue<string>(upnp + "albumArtURI", xcontainer, "");

                //add to list
                didlobj.Items.Add(mcontainer);
            }
        }

        private void ParseItemResources(XElement element, MediaItem mediaItem)
        {
            XNamespace didlite = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/";
            var xresources = element.Elements(didlite + "res");
            foreach (var xresource in xresources)
            {
                MediaResource resource = new MediaResource();
                resource.Protocol = new ProtocolInfo();

                //url
                resource.Uri = xresource.Value;

                //protocol
                var xresourceproto = xresource.Attribute("protocolInfo");
                var protostring = xresourceproto.Value;
                var protocomp = protostring.Split(':');
                resource.Protocol.Transport = protocomp[0];
                resource.Protocol.Mime = protocomp[2];
                resource.Protocol.DLNAFlags = protocomp[3];

                //resource
                mediaItem.Resources.Add(resource);
            }
        }

        private void ParseItems(XDocument document, DIDLLiteObject didlobj)
        {
            XNamespace didlite = "urn:schemas-upnp-org:metadata-1-0/DIDL-Lite/";
            XNamespace dc = "http://purl.org/dc/elements/1.1/";
            XNamespace upnp = "urn:schemas-upnp-org:metadata-1-0/upnp/";

            var xitems = document.Root.Elements(didlite + "item").ToList();
            foreach (var xitem in xitems)
            {
                MediaItem mitem = new MediaItem();

                //Parse components
                ParseUPnPComponent(xitem, mitem);

                //searchable and restricted
                mitem.Restricted = GetAttribute<bool>("restricted", xitem, true);

                //dc purl objects
                mitem.Title = GetChildValue<string>(dc + "title", xitem, "Unkown");

                //upnp objects
                //mcontainer.Genre = GetChildValue<string>(upnp + "genre", xcontainer, "Unkown");
                mitem.AlbumArtURI = GetChildValue<string>(upnp + "albumArtURI", xitem, "");
                mitem.Artist = GetChildValue<string>(upnp + "artist", xitem, "Unknown");
                mitem.Album = GetChildValue<string>(upnp + "album", xitem, "Unknown");

                //Parse resources
                ParseItemResources(xitem, mitem);

                //add to list
                didlobj.Items.Add(mitem);
            }

        }

        public DIDLLiteObject ParseDIDL(string xml)
        {
            xml = xml.Replace("&", "&amp;");

            XDocument document = XDocument.Parse(xml);
            DIDLLiteObject didl = new DIDLLiteObject();

            //Parse containers
            ParseContainers(document, didl);

            //Parse Item
            ParseItems(document, didl);


            return didl;
        }
    }
}
