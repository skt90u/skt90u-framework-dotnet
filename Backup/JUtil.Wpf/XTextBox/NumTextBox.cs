using System.Windows.Input;

namespace JUtil.Wpf
{
    public class NumTextBox : System.Windows.Controls.TextBox
    {
        protected override void OnKeyDown(KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.D0:
                case Key.D1:
                case Key.D2:
                case Key.D3:
                case Key.D4:
                case Key.D5:
                case Key.D6:
                case Key.D7:
                case Key.D8:
                case Key.D9:
                    {
                        base.OnKeyDown(e);
                    } break;
                default:
                    {
                        e.Handled = true;
                    }break;
            }
        }

        
    } // end of NumTextBox
}
