using System.Windows;

namespace JUtil.Wpf.Extensions
{
    public class ExtMessageBox
    {
        public static void Error(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public static void Info(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }


    } // end of ExtMessageBox
}