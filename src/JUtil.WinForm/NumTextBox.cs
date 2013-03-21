using System.Windows.Forms;

namespace JUtil.WinForm
{
    /// <summary>
    /// Implement a Number only Textbox
    /// </summary>
    public class NumTextBox : TextBox
    {
        /// <remarks>
        /// implement only accept number 
        /// </remarks>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && 
                !char.IsDigit(e.KeyChar) && 
                e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && 
                Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

    } // end of XTextBox
}
