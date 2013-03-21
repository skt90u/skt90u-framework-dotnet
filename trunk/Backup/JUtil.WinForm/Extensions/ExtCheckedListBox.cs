using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    public static class ExtCheckedListBox
    {
        public static ArrayList GetCheckedTextArray(this CheckedListBox source)
        {
            ArrayList arr = new ArrayList();

            foreach (object checkedItem in source.CheckedItems)
            {
                arr.Add(checkedItem.ToString());
            }
            return arr;
        }

        public static void Fill(this CheckedListBox source, DataTable dt, string field, CheckState initCheckState)
        {
            source.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string fieldValue = row[field].ToString();
                source.Items.Add(fieldValue, initCheckState);
            }
        }


    } // end of ExtCheckedListBox
}
