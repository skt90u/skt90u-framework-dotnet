using System.Runtime.CompilerServices;
using System.Web.UI;
using System;

namespace JUtil.Web.Extensions
{
    /// <summary>Enhance control functionality</summary>
    public static class ExtControl
    {
        /// <summary>
        /// find the first id-matched and type-matched control from startingControl
        /// </summary>
        /// <returns>
        /// return null if can't be found
        /// </returns>
        /// <remarks>
        /// we must declare T as Control, because we want to use 'found.ID' statement
        /// </remarks>
        public static T XFindControl<T>(this Control startingControl, string id) where T : Control
        {
            T found = null;
            int controlCount = startingControl.Controls.Count;
            if (controlCount > 0)
            {
                for (int i = 0; i <= controlCount - 1; i++)
                {
                    Control activeControl = startingControl.Controls[i];
                    if (activeControl is T)
                    {
                        found = startingControl.Controls[i] as T;
                        if (string.Compare(id, found.ID, true) == 0)
                        {
                            break; // TODO: might not be correct. Was : Exit For
                        }
                        else
                        {
                            found = null;
                        }
                    }
                    else
                    {
                        found = XFindControl<T>(activeControl, id);
                        if (found != null)
                        {
                            break; // TODO: might not be correct. Was : Exit For
                        }
                    }
                }
            }
            return found;
        }

        /// <summary>
        /// find the first id-matched control from startingControl
        /// </summary>
        /// <returns>
        /// return null if can't be found
        /// </returns>
        public static Control XFindControl(this Control container, string id)
        {
            Control ctrl = container.FindControl(id);
            if (ctrl == null)
            {
                for (int i = 0; i <= container.Controls.Count - 1; i++)
                {
                    ctrl = XFindControl(container.Controls[i], id);
                    if (ctrl != null)
                    {
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
            return ctrl;
        }

        /// <summary>
        /// Implement a generic get in control
        /// </summary>
        /// <param name="ctl"></param>
        /// <returns></returns>
        public static object GetValue(this Control ctl)
        {
            if (ctl == null)
                throw new ArgumentNullException("ctl");

            AspNetControlAccessor accessor = new AspNetControlAccessor(ctl);
            return accessor.GetValue();
        }

        /// <summary>
        /// Implement a generic set in control
        /// </summary>
        public static void SetValue(this Control ctl, object value)
        {
            if (ctl == null)
                throw new ArgumentNullException("ctl");

            AspNetControlAccessor accessor = new AspNetControlAccessor(ctl);
            accessor.SetValue(value);
        }

    } // end of ExtExtControl  
}
