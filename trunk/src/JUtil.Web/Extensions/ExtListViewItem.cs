using System.Web.UI.WebControls;

namespace JUtil.Web.Extensions
{
    /// <summary>Enhance ListViewItem functionality</summary>
    public static class ExtListViewItem
    {

        /// <summary>tell if aItem is InsertItem</summary>
        /// <remarks>http://msdn.microsoft.com/zh-tw/library/system.web.ui.webcontrols.listview.insertitem.aspx</remarks>
        public static bool isInsertItem(this ListViewItem aItem)
        {
            return ListViewItemType.InsertItem == aItem.ItemType;
        }

        /// <summary>tell if aItem is EditItem</summary>
        /// <remarks>http://www.blog.ingenuitynow.net/ASPNet+ListView+EditItem.aspx</remarks>
        public static bool isEditItem(this ListViewItem aItem)
        {
            ListView lsv = (ListView)aItem.NamingContainer;

            if (lsv == null) return false;

            return (ListViewItemType.DataItem == aItem.ItemType) && (lsv.EditIndex == ((ListViewDataItem)aItem).DisplayIndex);
        }

        /// <summary>tell if aItem is ReadOnlyItem</summary>
        /// <remarks>http://www.blog.ingenuitynow.net/ASPNet+ListView+EditItem.aspx</remarks>
        public static bool isReadOnlyItem(this ListViewItem aItem)
        {
            ListView lsv = (ListView)aItem.NamingContainer;

            if (lsv == null) return false;

            return (ListViewItemType.DataItem == aItem.ItemType) && (lsv.EditIndex != ((ListViewDataItem)aItem).DisplayIndex);
        }


    } // end of ExtListViewItem
}
