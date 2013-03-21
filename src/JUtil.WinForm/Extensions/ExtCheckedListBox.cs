using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    /// <summary>
    /// Extensions of CheckedListBox
    /// </summary>
    public static class ExtCheckedListBox
    {
        /// <summary>
        /// get all checked Text in CheckedListBox
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ArrayList GetCheckedTextArray(this CheckedListBox source)
        {
            ArrayList arr = new ArrayList();

            foreach (object checkedItem in source.CheckedItems)
            {
                arr.Add(checkedItem.ToString());
            }
            return arr;
        }

        /// <summary>
        /// fill check-box-list by DataTable's field values
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dt"></param>
        /// <param name="field"></param>
        /// <param name="initCheckState"></param>
        public static void Fill(this CheckedListBox source, DataTable dt, string field, CheckState initCheckState)
        {
            source.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string fieldValue = row[field].ToString();
                source.Items.Add(fieldValue, initCheckState);
            }
        }

        public static void Fill(this CheckedListBox source, string[] values, CheckState initCheckState)
        {
            source.Items.Clear();

            foreach (string value in values)
                source.Items.Add(value, initCheckState);
        }
    } // end of ExtCheckedListBox
}
