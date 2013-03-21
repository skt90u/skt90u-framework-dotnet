using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace JUtil.Web
{
    class CheckBoxAccessor : IControlAccessor
    {
        #region IControlAccessor 成員

        public bool IsMatchType(Control ctl)
        {
            bool match = ctl is CheckBox ||
                         ctl.GetType().IsSubclassOf(typeof(CheckBox));
            return match;
        }

        public object GetValue(Control ctl)
        {
            return (ctl as CheckBox).Checked;
        }

        public void SetValue(Control ctl, object value)
        {
            (ctl as CheckBox).Checked = Convert.ToBoolean(value);
        }

        #endregion
    }
}
