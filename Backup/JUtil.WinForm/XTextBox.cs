using System.Windows.Forms;

namespace JUtil.WinForm
{
    class XTextBox : TextBox
    {
        private const bool supportSelectAll = true;
        
        /// <remarks>
        /// 提供SelectAll功能
        /// </remarks>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            // [SelectAll]
            // http://schotime.net/blog/index.php/2008/03/12/select-all-ctrla-for-textbox/
            if (supportSelectAll && 
                e.Control && 
                e.KeyCode == Keys.A)
            {
                SelectAll();
            }
                
            base.OnKeyDown(e);
        }


    } // end of XTextBox
}
