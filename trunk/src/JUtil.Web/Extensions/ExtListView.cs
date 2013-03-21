using System.Web.UI.WebControls;

namespace JUtil.Web.Extensions
{
    /// <summary>Enhance ListViewItem functionality</summary>
    public static class ExtListView
    {
        /// <summary>
        /// 模擬DetailView的ChangeMode
        /// </summary>
        public static void ChangeMode(this ListView lsv, DetailsViewMode newMode)
        {
            switch (newMode)
            {
                case DetailsViewMode.ReadOnly:
                    {
                        // Hide InsertItemTemplate
                        lsv.InsertItemPosition = InsertItemPosition.None;
                        // Hide EditItemTemplate
                        lsv.EditIndex = -1;
                        lsv.SelectedIndex = 0;
                    } break;

                case DetailsViewMode.Insert:
                    {
                        // In Insert mode, we just want to see InsertItemTemplate
                        // so we must do following 3 steps
                        // 1) Hide EditItemTemplate
                        lsv.EditIndex = -1;
                        // 2) Hide ItemTemplate
                        //    ( In order to accomplish it, we must set Primary Key(s) to be NULL,
                        //      then OpenDetailsView function will return NULL while Pk(s) is NULL )

                        // [原因] 由於ListView的 Insert、Update、Select是一起執行的，這表示新增單筆資料時，
                        //        OpenDetailsView仍然會執行，為了使新增單筆資料時，只有InsertItemplate顯示
                        //        必須使OpenDetailsView回傳NULL。
                        // [做法] 根據OpenDetailsView對主鍵值限定
                        //        "正常情況 DetailsView 主鍵值不得為null, 若特殊狀況允許null, 則必須移除過濾條件"
                        //        If BvUtil.IsNullObj(KeyValues("EmployeeID")) Then Return Nothing
                        // 3) Show InsertItemTemplate
                        lsv.InsertItemPosition = InsertItemPosition.FirstItem;
                    } break;

                case DetailsViewMode.Edit:
                    {
                        // Hide InsertItemTemplate
                        lsv.InsertItemPosition = InsertItemPosition.None;
                        // Show EditItemTemplate
                        lsv.EditIndex = 0;
                    } break;
            }
        }

        


    } // end of ExtListViewItem
}
