using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JUtil.Web
{
    class TextBoxAccessor : IControlAccessor
    {
        #region IControlAccessor 成員

        public bool IsMatchType(Control ctl)
        {
            bool match = ctl is TextBox || 
                         ctl.GetType().IsSubclassOf(typeof(TextBox));
            return match;
        }

        public object GetValue(Control ctl)
        {
            return (ctl as TextBox).Text;
        }

        public void SetValue(Control ctl, object value)
        {
            (ctl as TextBox).Text = value.ToString();
        }

        #endregion
    }
}
