using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace JUtil.Web.Extensions
{
    /// <summary>Enhance WebControl functionality</summary>
    public static class ExtWebControl
    {
        public static void AppendClass(this WebControl wc, string className)
        {
            if (!wc.CssClass.Contains(className))
            {
                if (!string.IsNullOrEmpty(wc.CssClass))
                {
                    className = " " + className;
                }
                wc.CssClass = wc.CssClass + className;
            }
        }

        public static void RemoveClass(this WebControl wc, string className)
        {
            wc.CssClass = wc.CssClass.Replace(className, string.Empty);
            wc.CssClass = wc.CssClass.Trim();
        }

        public static void AppendClass(this Style tis, string className)
        {
            if (!tis.CssClass.Contains(className))
            {
                if (!string.IsNullOrEmpty(tis.CssClass))
                {
                    className = " " + className;
                }
                tis.CssClass = tis.CssClass + className;
            }
        }

        public static void RemoveClass(this Style tis, string className)
        {
            tis.CssClass = tis.CssClass.Replace(className, string.Empty);
            tis.CssClass = tis.CssClass.Trim();
        }


    } // end of ExtWebControl
}
