using System.Text;
using System.Xml;

namespace JUtil.Extensions
{
    /// <summary>
    /// Extensions of XmlDocument
    /// </summary>
    public static class ExtXmlDocument
    {
        #region 根據編碼方式儲存XmlDocument
        /// <summary>
        ///  根據編碼方式儲存XmlDocument
        /// </summary>
        public static void SaveAs(this XmlDocument self, string codepage, string path)
        {
            //
            // 設定XML格式
            //
            // [reference]
            // 中文編碼  http://blog.miniasp.com/post/2009/10/03/XmlWriter-and-Encoding-issues.aspx
            // XML格式化 http://www.silverlightchina.net/html/study/WPF/2010/1011/2534.html
            //
            Encoding enc = Encoding.GetEncoding(codepage);
            int paddingSize = 4;

            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.OmitXmlDeclaration = false;
            settings.IndentChars = new string(' ', paddingSize);
            settings.NewLineOnAttributes = false;
            settings.Encoding = enc;

            //
            // 寫入
            //
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                self.Save(writer);    
            }
        }
        #endregion


    } // end of ExtXmlDocument
}
