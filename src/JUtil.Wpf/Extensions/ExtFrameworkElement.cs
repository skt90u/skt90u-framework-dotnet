using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Xml;

namespace JUtil.Wpf.Extensions
{
    /// <summary>
    /// Extensions of FrameworkElement
    /// </summary>
    public static class ExtFrameworkElement
    {
        #region 根據 Xaml 建立控件
        /// <summary>
        /// 根據 Xaml 建立控件
        /// </summary>
        public static FrameworkElement Create(string path){
            
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.ConformanceLevel = ConformanceLevel.Fragment;

            XmlReader reader = XmlReader.Create(path, settings);

            FrameworkElement element = System.Windows.Markup.XamlReader.Load(reader) as FrameworkElement;

            return element;
        }
        #endregion
    
    
    } // end of ExtFrameworkElement
}
