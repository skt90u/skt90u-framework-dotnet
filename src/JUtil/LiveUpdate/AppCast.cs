using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace JUtil
{
    internal class AppCastItem : IComparable<AppCastItem>
    {
        public String Version;
        public String DownloadLink;

        #region IComparable<NetSparkleAppCastItem> Members

        public int CompareTo(AppCastItem other)
        {
            Version v1 = new Version(this.Version);
            Version v2 = new Version(other.Version);

            return v1.CompareTo(v2);
        }

        #endregion
    }

    internal class AppCast
    {
        private String _castUrl;

        private const String itemNode = "item";
        private const String enclosureNode = "enclosure";
        private const String versionAttribute = "sparkle:version";
        private const String urlAttribute = "url";

        public AppCast(String castUrl)
        {
            _castUrl = castUrl;
        }

        public AppCastItem GetLatestVersion()
        {
            AppCastItem latestVersion = null;

            try
            {
                // build a http web request stream
                WebRequest request = HttpWebRequest.Create(_castUrl);

                // request the cast and build the stream
                WebResponse response = request.GetResponse();

                Stream inputstream = response.GetResponseStream();

                AppCastItem currentItem = null;

                XmlTextReader reader = new XmlTextReader(inputstream);
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case itemNode:
                                {
                                    currentItem = new AppCastItem();
                                    break;
                                }
                            case enclosureNode:
                                {
                                    currentItem.Version = reader.GetAttribute(versionAttribute);
                                    currentItem.DownloadLink = reader.GetAttribute(urlAttribute);
                                    break;
                                }
                        }
                    }
                    else if (reader.NodeType == XmlNodeType.EndElement)
                    {
                        switch (reader.Name)
                        {
                            case itemNode:
                                {
                                    if (latestVersion == null)
                                        latestVersion = currentItem;
                                    else if (currentItem.CompareTo(latestVersion) > 0)
                                    {
                                        latestVersion = currentItem;
                                    }
                                    break;
                                }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.E(ex.InnerException);
            }

            // go ahead
            return latestVersion;
        }


    } // end of AppCast
}
