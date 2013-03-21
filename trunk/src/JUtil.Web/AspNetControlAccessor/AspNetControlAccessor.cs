using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Reflection;

namespace JUtil.Web
{
    /// <summary>
    /// 創造這類別的目的是設計一機制能夠存取Asp.net任何一種WebControl
    /// </summary>
    sealed class AspNetControlAccessor
    {
        private static List<IControlAccessor> Accessors = new List<IControlAccessor>();
        private IControlAccessor Accessor;
        private Control ctl;

        static AspNetControlAccessor()
        {
            RegisterAll();
        }

        #region RegisterManually (Obsolete)
        /// <summary>
        /// 已經廢棄了,使用RegisterAll取代
        /// </summary>
        [Obsolete()]
        private static void RegisterManually()
        {
            Register(new TextBoxAccessor());
            Register(new DropDownListAccessor());
            Register(new CheckBoxAccessor());
            Register(new LabelAccessor());
            Register(new RadioButtonListAccessor());
        }
        #endregion

        private static void RegisterAll()
        {
            // 取得目前的Assembly
            Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();

            foreach (Type type in assembly.GetTypes())
            {
                // 註冊所有繼承IControlAccessor介面的類別

                if (!typeof(IControlAccessor).IsAssignableFrom(type))
                    continue;

                if (type.Name.Equals("IControlAccessor"))
                    continue;

                IControlAccessor accessor = System.Activator.CreateInstance(type) as IControlAccessor;
                Register(accessor);
            }
        }

        private static void Register(IControlAccessor Accessor)
        {
            Accessors.Add(Accessor);
        }

        private IControlAccessor GetAccessor(Control ctl)
        {
            if (ctl is IControlAccessor)
                return (IControlAccessor)ctl;

            foreach (IControlAccessor Accessor in Accessors)
            {
                if (Accessor.IsMatchType(ctl))
                    return Accessor;
            }

            throw new Exception("There is no Accessor can match type : " + ctl.GetType().ToString());
        }

        public AspNetControlAccessor(Control ctl)
        {
            this.ctl = ctl;

            Accessor = GetAccessor(ctl);
        }

        public object GetValue()
        {
            return Accessor.GetValue(ctl);
        }

        public void SetValue(object value)
        {
            Accessor.SetValue(ctl, value);
        }
    }
}
