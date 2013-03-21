using System.Collections;
using System.Data;
using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    //
    // Extension System.Windows.Forms.ListBox
    //
    public static class ExtListBox
    {
        public static ArrayList ToArrayList(this ListBox source)
        {
            ArrayList array = new ArrayList();
            foreach (object item in source.Items)
            {
                array.Add(item);
            }
            return array;
        }

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

        public static void MoveTo(this ListBox From, ListBox To)
        {
            foreach (object item in From.Items)
            {
                To.Items.Add(item);
            }
            From.Items.Clear();
        }

        public static ArrayList GetSelectedTextArray(this ListBox source)
        {
            ArrayList arr = new ArrayList();

            foreach (object selectedItem in source.SelectedItems)
            {
                arr.Add(selectedItem.ToString());
            }

            return arr;
        }

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
