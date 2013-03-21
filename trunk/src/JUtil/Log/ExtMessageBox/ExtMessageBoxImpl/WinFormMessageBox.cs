using MessageBoxBase = System.Windows.Forms;

namespace JUtil
{
    /// <summary>
    /// an implementation of IExtMessageBox interface in WinForm platform
    /// </summary>
    public class WinFormMessageBox : IExtMessageBox
    {
        #region IExtMessageBox 成員

        /// <summary>
        /// show error MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public void Error(string text, string caption)
        {
            MessageBoxBase.MessageBox.Show(
                text, 
                caption,
                MessageBoxBase.MessageBoxButtons.OK,
                MessageBoxBase.MessageBoxIcon.Error);
        }

        /// <summary>
        /// show info MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public void Info(string text, string caption)
        {
            MessageBoxBase.MessageBox.Show(
                text,
                caption,
                MessageBoxBase.MessageBoxButtons.OK,
                MessageBoxBase.MessageBoxIcon.Information);
        }

        #endregion
    }
}
