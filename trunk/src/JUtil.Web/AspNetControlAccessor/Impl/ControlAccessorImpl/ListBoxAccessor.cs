using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

namespace JUtil.Web
{
    class ListBoxAccessor : IControlAccessor
    {
        #region IControlAccessor 成員

        public bool IsMatchType(Control ctl)
        {
            bool match = ctl is ListBox ||
                         ctl.GetType().IsSubclassOf(typeof(ListBox));
            return match;
        }

        public object GetValue(Control ctl)
        {
            ListBox lb = (ListBox)ctl;
            List<KeyValuePair<string, string>> value = new List<KeyValuePair<string, string>>();
            foreach (ListItem item in lb.Items)
            {
                // key-value概念與item的Value-Text別搞混了!
                string key = item.Value;
                string val = item.Text;
                value.Add(new KeyValuePair<string, string>(key, val));
            }
            return value;
        }

        public void SetValue(Control ctl, object value)
        {
            ListBox lb = (ListBox)ctl;
            lb.Items.Clear();

            List<KeyValuePair<string, string>> KVPs = value as List<KeyValuePair<string, string>>;

            foreach (KeyValuePair<string, string> KVP in KVPs)
            {
                string val = KVP.Key;
                string text = KVP.Value;
                lb.Items.Add(new ListItem(text, val));
            }
        }

        #endregion
    }
}
