using System;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace JUtil.Web.Extensions
{
    /// <summary>Enhance button functionality</summary>
    public static class ExtListControl
    {
        /// <summary>
        /// move selecting items to another ListControl
        /// </summary>
        public static void MoveTo(this ListControl from, ListControl to)
        {
            List<ListItem> removingItems = new List<ListItem>();

            foreach (ListItem item in from.Items)
            {
                if (item.Selected)
                {
                    item.Selected = false;

                    if (!to.Items.Contains(item))
                        to.Items.Add(item);

                    removingItems.Add(item);
                }
            }

            foreach (ListItem item in removingItems)
                from.Items.Remove(item);
        }

        /// <summary>
        /// move items to another ListControl
        /// </summary>
        public static void MoveAll(this ListControl from, ListControl to)
        {
            List<ListItem> removingItems = new List<ListItem>();

            foreach (ListItem item in from.Items)
            {
                item.Selected = false;

                if (!to.Items.Contains(item))
                    to.Items.Add(item);

                removingItems.Add(item);
            }

            foreach (ListItem item in removingItems)
                from.Items.Remove(item);
        }

    } // end of ExtListControl
}
