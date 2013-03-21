using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JUtil.Wpf.Extensions
{
    /// <summary>
    /// Extensions of Panel
    /// </summary>
    public static class ExtPanel
    {
        /// <summary>
        /// remove all visual children inside a panel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static void RemoveChildren<T>(this Panel self) where T : UIElement
        {
            List<T> childs = new List<T>();

            foreach (T child in ExtDependencyObject.FindVisualChildren<T>(self))
            {
                childs.Add(child);
            }

            foreach (T child in childs)
            {
                self.Children.Remove(child);
            }
        }


    } // end of ExtPanel
}
