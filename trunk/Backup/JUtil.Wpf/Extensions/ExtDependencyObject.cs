using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace JUtil.Wpf.Extensions
{
    public class ExtDependencyObject
    {
        #region Find all controls in WPF Window by type
        /// <summary>
        /// Find all controls in WPF Window by type
        /// </summary>
        /// <remarks>
        /// reference by http://stackoverflow.com/questions/974598/find-all-controls-in-wpf-window-by-type
        /// 
        /// usage :
        /// foreach (TextBlock tb in FindVisualChildren<TextBlock>(window))
        /// {
        ///   // do something with tb here
        /// }
        /// </remarks>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        #endregion

        #region Find parent by specified child
        /// <summary>
        /// Find parent by specified child
        /// </summary>
        /// <remarks>
        /// reference by : http://stackoverflow.com/questions/636383/wpf-ways-to-find-controls
        /// usage : 
        /// Window owner = UIHelper.FindVisualParent<Window>(myControl);
        /// </remarks>
        public static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            // get parent item
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);

            // we've reached the end of the tree
            if (parentObject == null) return null;

            // check if the parent matches the type we're looking for
            T parent = parentObject as T;

            // return if type-match or use recursion to proceed with next level
            return (parent != null) ? parent : FindVisualParent<T>(parentObject);
        }
        #endregion
    }
}
