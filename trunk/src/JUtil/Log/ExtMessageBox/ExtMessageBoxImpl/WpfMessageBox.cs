//
// require : PresentationFramework.dll
//
using MessageBoxBase = System.Windows;

namespace JUtil
{
    /// <summary>
    /// an implementation of IExtMessageBox interface in WPF platform
    /// </summary>
    public class WpfMessageBox : IExtMessageBox
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
                MessageBoxBase.MessageBoxButton.OK,
                MessageBoxBase.MessageBoxImage.Error);
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
                MessageBoxBase.MessageBoxButton.OK,
                MessageBoxBase.MessageBoxImage.Information);
        }

        #endregion
    }
}
