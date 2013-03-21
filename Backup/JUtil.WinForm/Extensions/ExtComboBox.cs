using System.Data;
using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    //
    // Extension System.Windows.Forms.ComboBox
    //
    public static class ExtComboBox
    {
        public static void Fill(this ComboBox source, DataTable dt, string field)
        {
            source.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string fieldValue = row[field].ToString();
                source.Items.Add(fieldValue);
            }
        }


    } // end of ExtComboBox
}
