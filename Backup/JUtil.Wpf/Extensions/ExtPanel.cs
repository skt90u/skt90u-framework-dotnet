using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace JUtil.Wpf.Extensions
{
    public static class ExtPanel
    {
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
