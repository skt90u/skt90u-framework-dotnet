using System.Text;
using System.Windows;
using System.Xml;

namespace JUtil.Wpf.Extensions
{
    public static class ExtUIElement
    {
        #region 將 UIElement 對應的 Xaml 寫入指定檔案
        /// <summary>
        /// 將 Xaml 寫入指定檔案
        /// </summary>
        public static void SaveAs(this UIElement el, string path)
        {
            //
            // [How Do I] 設定XML格式
            // 中文編碼  http://blog.miniasp.com/post/2009/10/03/XmlWriter-and-Encoding-issues.aspx
            // XML格式化 http://www.silverlightchina.net/html/study/WPF/2010/1011/2534.html
            //
            string codepage = "big5";
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
                System.Windows.Markup.XamlWriter.Save(el, writer);
            }
        }
        #endregion


    } // end of ExtUIElement
}
