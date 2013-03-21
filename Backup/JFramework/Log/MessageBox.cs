// if this dll is used from WinForm
using ApiSource = System.Windows.Forms;

namespace JFramework
{
    internal class MessageBox
    {
        public static void Error(string text, string caption)
        {
            ApiSource.MessageBox.Show(text, 
                                      caption, 
                                      ApiSource.MessageBoxButtons.OK, 
                                      ApiSource.MessageBoxIcon.Error);
        }
    }
}
