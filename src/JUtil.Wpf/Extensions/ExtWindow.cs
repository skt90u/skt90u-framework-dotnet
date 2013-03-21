using System;
using System.Windows;
using System.Windows.Forms;

namespace JUtil.Wpf.Extensions
{
    /// <summary>
    /// Extensions of Window
    /// </summary>
    public static class ExtWindow
    {
        /// <summary>
        /// create a popup window and hide current window until pop window close.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        public static void PopWindow<T>(this Window self) where T : Window
        {
            T dlg = (Activator.CreateInstance(typeof(T))) as T;

            self.Hide();
            dlg.ShowDialog();
            self.Show();
        }

        /// <summary>
        /// full screen a window
        /// </summary>
        /// <param name="self"></param>
        public static void FullScreen(this Window self)
        {
            //
            // [How Do I] 設定顯示視窗永遠在最上層
            // http://stackoverflow.com/questions/2573842/setting-an-xaml-window-always-on-top-but-no-topmost-property
            //
            self.Topmost = true;

            //
            // [How Do I] Developing full screen WPF applications
            // http://social.msdn.microsoft.com/forums/en-US/wpf/thread/03c7c966-4b77-4f41-849f-a4a4c0974eb3/
            //
            self.WindowStyle = WindowStyle.None;
            self.Cursor = null;
            
            self.WindowState = WindowState.Maximized;
        }

        /// <summary>
        /// full screen a window to a monitor by monitor id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="screenNo"></param>
        /// <returns></returns>
        public static T ExploreWindow<T>(int screenNo) where T : Window
        {
            int count = Screen.AllScreens.Length;
            if (screenNo < 0 || count <= screenNo)
            {
                string error = string.Format("The screen ({0}) that you specified is out of available screens", screenNo);
                throw new IndexOutOfRangeException(error);
            }

            Screen screen = Screen.AllScreens[screenNo];

            T window = (Activator.CreateInstance(typeof(T))) as T;
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.Manual;
            window.Top = screen.Bounds.Top;
            window.Left = screen.Bounds.Left;

            //
            // (1) 必須要使用Show, 而不是ShowDialog, 否則主畫面會等到這個視窗結束才執行
            // (2) 最大劃必須等到 Show完成後, 在執行
            // 
            window.Show();

            window.FullScreen();

            return window;
        }


    } // end of ExtWindow
}

