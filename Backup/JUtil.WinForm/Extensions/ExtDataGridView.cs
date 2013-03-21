using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    public static class ExtDataGridView
    {
        public static void Clear(this DataGridView source)
        {
            int count = source.RowCount;
            for (int i = 0; i < count; i++)
            {
                source.Rows.Remove(source.Rows[0]);
            }
        }


    } // end of ExtDataGridView
}