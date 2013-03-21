using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JUtil.Web
{
    class LabelAccessor : IControlAccessor
    {
        #region IControlAccessor 成員

        public bool IsMatchType(Control ctl)
        {
            bool match = ctl is Label ||
                         ctl.GetType().IsSubclassOf(typeof(Label));
            return match;
        }

        public object GetValue(Control ctl)
        {
            return (ctl as Label).Text;
        }

        public void SetValue(Control ctl, object value)
        {
            (ctl as Label).Text = value.ToString();
        }

        #endregion
    }
}
