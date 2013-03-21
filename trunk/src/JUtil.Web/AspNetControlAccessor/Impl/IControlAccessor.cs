using System.Web.UI;

namespace JUtil.Web
{
    interface IControlAccessor
    {
        bool IsMatchType(Control ctl);
        object GetValue(Control ctl);
        void SetValue(Control ctl, object value);
    }
}
