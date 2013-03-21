using System;
using System.Web.UI.WebControls;
using JUtil.Web.Extensions;
using System.Diagnostics;
using System.Web.UI;

namespace JUtil.Web.WebControls
{
    /// <summary>
    /// 要繼承 ListView 必須加入參考System.Web.Extensions.dll (http://msdn.microsoft.com/zh-tw/library/system.web.ui.webcontrols.listview.aspx)
    /// </summary>
    public class XListView : ListView
    {
        #region Initialize Items
        /// <summary>
        /// Initialize Insert Item
        /// </summary>
        public event EventHandler<ListViewItemEventArgs> InitInsertItem;

        /// <summary>
        /// Initialize Edit Item
        /// </summary>
        public event EventHandler<ListViewItemEventArgs> InitEditItem;

        /// <summary>
        /// Initialize ReadOnly Item
        /// </summary>
        public event EventHandler<ListViewItemEventArgs> InitReadOnlyItem;

        // 如何使用
        // 假設你的ListView叫做lsv, 請定義以下函式
        //Private Sub lsv_InitInsertItem(ByVal sender As Object, ByVal e As ListViewItemEventArgs) Handles lsv.InitInsertItem

        /// <summary>
        /// override OnInit function
        /// </summary>
        protected override void OnInit(System.EventArgs e)
        {
            this.DataBound += new EventHandler(XListView_DataBound);

            base.OnInit(e);
        }

        void XListView_DataBound(object sender, EventArgs e)
        {
            if (InsertItem != null)
            {
                if (InitInsertItem != null)
                {
                    InitInsertItem(this, new ListViewItemEventArgs(InsertItem));
                }
            }

            foreach (ListViewDataItem aItem in Items)
            {
                if (aItem.isReadOnlyItem())
                {
                    if (InitReadOnlyItem != null)
                    {
                        InitReadOnlyItem(this, new ListViewItemEventArgs(aItem));
                    }
                }
                else if (aItem.isEditItem())
                {
                    if (InitEditItem != null)
                    {
                        InitEditItem(this, new ListViewItemEventArgs(aItem));
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// XListView 用以模擬DetailView的 CurrentMode 行為, 
        /// </summary>
        public DetailsViewMode CurrentMode
        {
            get
            {
                DetailsViewMode Result = DetailsViewMode.ReadOnly;

                if ((InsertItemPosition == InsertItemPosition.None) && (EditIndex == -1))
                {
                    Result = DetailsViewMode.ReadOnly;
                }
                else if (!(InsertItemPosition == InsertItemPosition.None) && (EditIndex == -1))
                {
                    Result = DetailsViewMode.Insert;
                }
                else if ((InsertItemPosition == InsertItemPosition.None) && !(EditIndex == -1))
                {
                    Result = DetailsViewMode.Edit;
                }
                else
                {
                    Debug.Assert(false);
                }

                return Result;
            }
        }

        /// <summary>
        /// 根據目前模式取出對應的控制項
        /// </summary>
        public Control ExtFindControl(string field)
        {
            Control control = null;
            switch (CurrentMode)
            {
                case DetailsViewMode.Insert:
                    {
                        ListViewItem aContainer = InsertItem;
                        control = aContainer.FindControl(field);
                    }
                    break;

                case DetailsViewMode.Edit:
                    {
                        ListViewItem aContainer = EditItem;
                        control = aContainer.FindControl(field);
                    }
                    break;

                case DetailsViewMode.ReadOnly:
                    {
                        int selectedIndex = SelectedIndex;
                        if (-1 == SelectedIndex && 1 == Items.Count)
                        {
                            selectedIndex = 0;
                        }

                        if ((selectedIndex >= 0) && (selectedIndex < Items.Count))
                        {
                            ListViewItem aContainer = Items[selectedIndex];
                            control = aContainer.FindControl(field);
                        }
                    }
                    break;

                default:
                    break;
            }
            return control;
        }

        
    }
}
