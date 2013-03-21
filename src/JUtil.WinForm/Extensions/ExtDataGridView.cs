using System.Windows.Forms;

namespace JUtil.WinForm.Extensions
{
    /// <summary>
    /// Extensions of DataGridView
    /// </summary>
    public static class ExtDataGridView
    {
        /// <summary>
        /// clear data in DataGridView
        /// </summary>
        /// <param name="source"></param>
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