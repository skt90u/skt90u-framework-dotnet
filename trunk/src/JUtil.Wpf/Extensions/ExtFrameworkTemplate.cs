using System.Text;
using System.Windows;
using System.Xml;

namespace JUtil.Wpf.Extensions
{
    /// <summary>
    /// Extensions of FrameworkTemplate
    /// </summary>
    public static class ExtFrameworkTemplate
    {
        /// <summary>
        /// Dumping the default template to a string
        /// </summary>
        public static string Dump(this FrameworkTemplate template)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = new string(' ', 4);
            settings.NewLineOnAttributes = true;

            StringBuilder sb = new StringBuilder();
            using (XmlWriter writer = XmlWriter.Create(sb, settings))
            {
                System.Windows.Markup.XamlWriter.Save(template, writer);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Dumping the default template to a file
        /// </summary>
        public static void Dump(this FrameworkTemplate template, string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = new string(' ', 4);
            settings.NewLineOnAttributes = true;

            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                System.Windows.Markup.XamlWriter.Save(template, writer);
            }
        }


    } // end of ExtFrameworkTemplate
}
