using System.Windows.Forms;

namespace JUtil.WinForm
{
    /// <summary>
    /// Extensions of Form
    /// </summary>
    public class XForm : Form
    {
        private const bool supportEscExit = true;

        /// <summary>
        /// ctor of XForm
        /// </summary>
        public XForm()
        {
            KeyPreview = true;
        }

        /// <summary>
        /// extra hot key [ESC] can close current form
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            // [Exit form while you press esc]
            // http://www.eggheadcafe.com/community/aspnet/2/10074078/on-escape-key-pressed-exit-winform.aspx
            // use the keydown event of the form and write this code, but make sure to set KeyPreview property 
            // of the form to true as the key events of the form work only after setting this
            if (supportEscExit && e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            base.OnKeyDown(e);
        }


    } // end of XForm
}
