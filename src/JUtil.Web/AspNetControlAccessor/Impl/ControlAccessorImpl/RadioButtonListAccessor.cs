using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JUtil.Web
{
    class RadioButtonListAccessor : IControlAccessor
    {
        #region IControlAccessor 成員

        public bool IsMatchType(Control ctl)
        {
            bool match = ctl is RadioButtonList ||
                         ctl.GetType().IsSubclassOf(typeof(RadioButtonList));
            return match;
        }

        public object GetValue(Control ctl)
        {
            return (ctl as RadioButtonList).SelectedValue;
        }

        public void SetValue(Control ctl, object value)
        {
            (ctl as RadioButtonList).SelectedValue = Convert.ToString(value);
        }

        #endregion
    }
}
