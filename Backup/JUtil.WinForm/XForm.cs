using System.Windows.Forms;

namespace JUtil.WinForm
{
    public class XForm : Form
    {
        private const bool supportEscExit = true;

        public XForm()
        {
            KeyPreview = true;
        }

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
