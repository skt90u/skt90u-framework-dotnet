using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    /// <summary>
    /// Extension System.Windows.Forms.ListBox
    /// </summary>
    public static class ExtListBox
    {
        /// <summary>
        /// get all values of listbox
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ArrayList ToArrayList(this ListBox source)
        {
            ArrayList array = new ArrayList();
            foreach (object item in source.Items)
            {
                array.Add(item);
            }
            return array;
        }

        /// <summary>
        /// move selected items to another listbox
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        public static void MoveSelectedItemsTo(this ListBox From, ListBox To)
        {
            ArrayList arrSelectedItems = new ArrayList();

            foreach (object selectedItem in From.SelectedItems)
            {
                arrSelectedItems.Add(selectedItem);
            }

            foreach (object selectedItem in arrSelectedItems)
            {
                //if (!To.Items.Contains(selectedItem)) 
                //    To.Items.Add(selectedItem);

                To.Items.Add(selectedItem);
                From.Items.Remove(selectedItem);
            }
        }

        /// <summary>
        /// Clone ListBox
        /// </summary>
        /// <param name="From"></param>
        /// <param name="To"></param>
        public static void MoveTo(this ListBox From, ListBox To)
        {
            foreach (object item in From.Items)
            {
                To.Items.Add(item);
            }
            From.Items.Clear();
        }

        /// <summary>
        /// get selected texts
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static ArrayList GetSelectedTextArray(this ListBox source)
        {
            ArrayList arr = new ArrayList();

            foreach (object selectedItem in source.SelectedItems)
            {
                arr.Add(selectedItem.ToString());
            }

            return arr;
        }

        /// <summary>
        /// fill listbox by DataTable's field values
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dt"></param>
        /// <param name="field"></param>
        public static void Fill(this ListBox source, DataTable dt, string field)
        {
            source.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string fieldValue = row[field].ToString();
                source.Items.Add(fieldValue);
            }
        }


    } // end of ExtListBox
}
