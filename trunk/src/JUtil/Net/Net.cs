using System;
using System.IO;
using System.Net;

namespace JUtil
{
    /// <summary>
    /// the Internet Utility of JUtil
    /// </summary>
    public class Net
    {
        public static bool CheckPort(string ip, int port, int timeout)
        {
            using (System.Net.Sockets.TcpClient tcp = new System.Net.Sockets.TcpClient())
            {
                IAsyncResult ar = tcp.BeginConnect(ip, port, null, null);
                System.Threading.WaitHandle wh = ar.AsyncWaitHandle;
                try
                {
                    if (!ar.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(timeout), false))
                    {
                        tcp.Close();
                        throw new TimeoutException();
                    }

                    tcp.EndConnect(ar);

                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    wh.Close();
                }
            }
        }

        public static string LocalIP
        {
            get
            {
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                if (localIPs.Length > 0)
                    return localIPs[0].ToString();

                return string.Empty;
            }
        }

        /// <summary>
        /// determine a url whether legal or not
        /// </summary>
        public static bool UrlExist(string url)
        {
            try
            {
                WebRequest req = HttpWebRequest.Create(url);
                Stream ret = req.GetResponse().GetResponseStream();
                ret.Close();
                return true;
            }
            catch (Exception ex)
            {
                // 目前對WebRequest使用上還不熟悉，因此想知道有哪些情況會拋出Exception
                string error = string.Format("UrlExist(url = {0}) : {1}", url, ex.Message);
                Log.E(new Exception(error));
                return false;
            }
        }

        /// <summary>
        /// 下載指定檔案
        /// </summary>
        public static void DownloadFileAsync(
                                    string address, 
                                    string fileName,
                                    DownloadProgressChangedEventHandler DownloadProgressChanged,
                                    System.ComponentModel.AsyncCompletedEventHandler DownloadFileCompleted)
        {
            WebClient Client = new WebClient();

            if (DownloadProgressChanged != null)
                Client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(DownloadProgressChanged);

            if (DownloadFileCompleted != null)
                Client.DownloadFileCompleted += new System.ComponentModel.AsyncCompletedEventHandler(DownloadFileCompleted);

            Uri url = new Uri(address);
            Client.DownloadFileAsync(url, fileName);
        }


    } // end of Net
}
