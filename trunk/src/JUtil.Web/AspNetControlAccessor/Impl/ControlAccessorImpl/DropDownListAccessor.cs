using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JUtil.Web
{
    class DropDownListAccessor : IControlAccessor
    {
        #region IControlAccessor 成員

        public bool IsMatchType(Control ctl)
        {
            bool match = ctl is DropDownList ||
                         ctl.GetType().IsSubclassOf(typeof(DropDownList));
            return match;
        }

        public object GetValue(Control ctl)
        {
            return (ctl as DropDownList).SelectedValue;
        }

        public void SetValue(Control ctl, object value)
        {
            (ctl as DropDownList).SelectedValue = value.ToString();
        }

        #endregion
    }
}
