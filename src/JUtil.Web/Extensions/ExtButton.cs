using System;
using System.Reflection;
using System.Web.UI.WebControls;

namespace JUtil.Web.Extensions
{
    /// <summary>Enhance button functionality</summary>
    public static class ExtButton
    {

        /// <summary>
        /// raise server side click event on button even if we don't know 
        /// click handler's name
        /// </summary>
        /// <param name="btn"></param>
        /// <param name="e">e.g. null</param>
        /// <remarks>
        /// sometime, we want to fire a server side click event manually. 
        /// but we have no ideal what is the click handler's name of callee button.
        /// due to this requirement, I create this function can raise click event on
        /// button even if we don't know the handler's name
        /// </remarks>
        public static void RaiseClick(this Button btn, EventArgs e)
        {
            object[] args = new object[] { e };
            Type t = btn.GetType();
            MethodInfo mi = t.GetMethod("OnClick", BindingFlags.NonPublic | BindingFlags.Instance);
            mi.Invoke(btn, args);

            
        }


    } // end of ExtButton
}
