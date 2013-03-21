using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Net;

namespace JUtil.Web
{
    public class Utils
    {
        /// <summary>
        /// 獲得當前絕對路徑
        /// </summary>
        /// <param name="strPath">指定的路徑</param>
        /// <returns>絕對路徑</returns>
        public static string GetMapPath(string strPath)
        {
            if (HttpContext.Current != null)
            {
                return HttpContext.Current.Server.MapPath(strPath);
            }
            else //非web程序引用
            {
                strPath = strPath.Replace("/", "\\");
                if (strPath.StartsWith("\\"))
                {
                    strPath = strPath.Substring(strPath.IndexOf('\\', 1)).TrimStart('\\');
                }
                return System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, strPath);
            }
        }

        /// <summary>
        /// hack tip:通過更新web.config文件方式來重啟IIS進程池（注：iis中web園數量須大于1,且為非虛擬主機用戶才可調用該方法）
        /// </summary>
        public static void RestartIISProcess()
        {           
            try
            {
                System.Xml.XmlDocument xmldoc = new System.Xml.XmlDocument();
                xmldoc.Load(Utils.GetMapPath("~/web.config"));
                System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(Utils.GetMapPath("~/web.config"), null);
                writer.Formatting = System.Xml.Formatting.Indented;
                xmldoc.WriteTo(writer);
                writer.Flush();
                writer.Close();
            }
            catch
            { ; }
        }

        /// <summary>
        /// http POST请求url
        /// </summary>
        /// <param name="apiUrl"></param>
        /// <param name="method_name"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string GetHttpWebResponse(string url, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = postData.Length;
            request.Timeout = 20000;

            HttpWebResponse response = null;

            try
            {
                StreamWriter swRequestWriter = new StreamWriter(request.GetRequestStream());
                swRequestWriter.Write(postData);

                if (swRequestWriter != null)
                    swRequestWriter.Close();

                response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    return reader.ReadToEnd();
                }
            }
            finally
            {
                if (response != null)
                    response.Close();
            }
        }

        /// <summary>
        /// 根據Url獲得源文件內容
        /// </summary>
        /// <param name="url">合法的Url地址</param>
        public static string GetSourceTextByUrl(string url)
        {
            try
            {
                WebRequest request = WebRequest.Create(url);
                request.Timeout = 20000;//20秒超時
                WebResponse response = request.GetResponse();

                Stream resStream = response.GetResponseStream();
                StreamReader sr = new StreamReader(resStream, System.Text.Encoding.Default);
                return sr.ReadToEnd();
            }
            catch { return ""; }
        }

        /// <summary>
        /// 替換html字符
        /// </summary>
        public static string EncodeHtml(string strHtml)
        {
            if (strHtml != "")
            {
                strHtml = strHtml.Replace(",", "&def");
                strHtml = strHtml.Replace("'", "&dot");
                strHtml = strHtml.Replace(";", "&dec");
                return strHtml;
            }
            return "";
        }

        /// <summary>
        /// 轉換為靜態html
        /// </summary>
        public void TransHtml(string path, string outpath)
        {
            Page page = new Page();
            StringWriter writer = new StringWriter();
            page.Server.Execute(path, writer);
            FileStream fs;
            if (File.Exists(page.Server.MapPath("") + "\\" + outpath))
            {
                File.Delete(page.Server.MapPath("") + "\\" + outpath);
                fs = File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            else
            {
                fs = File.Create(page.Server.MapPath("") + "\\" + outpath);
            }
            byte[] bt = Encoding.Default.GetBytes(writer.ToString());
            fs.Write(bt, 0, bt.Length);
            fs.Close();
        }

        public static string GetIP()
        {
            string result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (string.IsNullOrEmpty(result))
                result = HttpContext.Current.Request.UserHostAddress;

            if (string.IsNullOrEmpty(result) || !JUtil.Validation.IsIP(result))
                return "127.0.0.1";

            return result;
        }

        /// <summary>
        /// 以指定的ContentType輸出指定文件文件
        /// </summary>
        /// <param name="filepath">文件路徑</param>
        /// <param name="filename">輸出的文件名</param>
        /// <param name="filetype">將文件輸出時設置的ContentType</param>
        public static void ResponseFile(string filepath, string filename, string filetype)
        {
            Stream iStream = null;

            // 緩衝區為10k
            byte[] buffer = new Byte[10000];
            // 文件長度
            int length;
            // 需要讀的數據長度
            long dataToRead;

            try
            {
                // 打開文件
                iStream = new FileStream(filepath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                // 需要讀的數據長度
                dataToRead = iStream.Length;

                HttpContext.Current.Response.ContentType = filetype;
                if (HttpContext.Current.Request.ServerVariables["HTTP_USER_AGENT"].IndexOf("MSIE") > -1)
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(filename.Trim()).Replace("+", " "));
                else
                    HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + filename.Trim());

                while (dataToRead > 0)
                {
                    // 檢查客戶端是否還處于連接狀態
                    if (HttpContext.Current.Response.IsClientConnected)
                    {
                        length = iStream.Read(buffer, 0, 10000);
                        HttpContext.Current.Response.OutputStream.Write(buffer, 0, length);
                        HttpContext.Current.Response.Flush();
                        buffer = new Byte[10000];
                        dataToRead = dataToRead - length;
                    }
                    else
                    {
                        // 如果不再連接則跳出死循環
                        dataToRead = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("Error : " + ex.Message);
            }
            finally
            {
                if (iStream != null)
                {
                    // 關閉文件
                    iStream.Close();
                }
            }
            HttpContext.Current.Response.End();
        }

        public static string GetRootUrl()
        {
            string protocol = HttpContext.Current.Request.Url.Scheme;
            string host = HttpContext.Current.Request.Url.Host.ToString();
            int port = HttpContext.Current.Request.Url.Port;

            string IIS_HOME = string.Format("{0}://{1}{2}",
                                 protocol,
                                 host,
                                 (port == 80 || port == 0) ? "" : ":" + port);

            string VIRTUAL_ROOT = System.Web.HttpRuntime.AppDomainAppVirtualPath;

            string url = CombineURL(IIS_HOME, VIRTUAL_ROOT);
            return url;
        }

        public static string CombineURL(string ParentUrl, string FolderOrFile)
        {
            string Url = ParentUrl;

            if (!ParentUrl.EndsWith("/"))
                Url += "/";

            string folderOrFile = FolderOrFile;

            bool isFile = folderOrFile.Contains(".");

            while (folderOrFile.StartsWith("/"))
                folderOrFile = folderOrFile.Substring(1, folderOrFile.Length - 1);

            while (folderOrFile.EndsWith("/"))
                folderOrFile = folderOrFile.Substring(0, folderOrFile.Length - 1);

            Url += folderOrFile;

            if (!isFile)
                Url += "/";

            return Url;
        }
    }
}
