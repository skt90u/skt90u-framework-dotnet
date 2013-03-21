using System;
using System.Text;
using System.Windows;
using System.Windows.Markup;
using System.Xml;

namespace JUtil.Wpf.Extensions
{
    /// <summary>
    /// Extensions of Wpf Control
    /// </summary>
    public static class ExtControl
    {
        #region 取得Control目前的Template對應的Xaml code
        /// <summary>
        /// 取得Control目前的Template對應的Xaml code, 方便觀察, 學習
        /// </summary>
        /// <remarks>
        /// http://www.cnblogs.com/zhouyinhui/archive/2007/03/28/690993.html
        /// </remarks>
        public static string GetTemplateXamlCode(this System.Windows.Controls.Control control)
        {
            FrameworkTemplate template = control.Template;

            string xaml = "";

            if (template != null)
            {

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = new string(' ', 4);
                settings.NewLineOnAttributes = true;

                StringBuilder strbuild = new StringBuilder();
                XmlWriter xmlwrite = XmlWriter.Create(strbuild, settings);

                try
                {
                    XamlWriter.Save(template, xmlwrite);
                    xaml = strbuild.ToString();
                }
                catch (Exception exc)
                {
                    xaml = exc.Message;
                }
            }
            else
            {
                xaml = "no template";
            }

            return xaml;
        }
        #endregion
    
    
    } // end of ExtControl
}
