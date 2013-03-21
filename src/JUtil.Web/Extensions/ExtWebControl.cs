using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI;

namespace JUtil.Web.Extensions
{
    /// <summary>
    /// Enhance WebControl functionality
    /// </summary>
    public static class ExtWebControl
    {
        /// <summary>
        /// 輸出此WebControl對應Client端的html code
        /// </summary>
        /// <param name="wc"></param>
        /// <returns></returns>
        public static string ToHtml(this WebControl wc)
        {
            StringWriter stringWriter = new StringWriter();

            HtmlTextWriter htmlWriter = new HtmlTextWriter(stringWriter);

            wc.RenderControl(htmlWriter);

            return stringWriter.ToString();
        }

        /// <summary>
        /// append class
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="className"></param>
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

        /// <summary>
        /// remove class
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="className"></param>
        public static void RemoveClass(this WebControl wc, string className)
        {
            wc.CssClass = wc.CssClass.Replace(className, string.Empty);
            wc.CssClass = wc.CssClass.Trim();
        }

        /// <summary>
        /// append class
        /// </summary>
        /// <param name="tis"></param>
        /// <param name="className"></param>
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

        /// <summary>
        /// remove class
        /// </summary>
        /// <param name="tis"></param>
        /// <param name="className"></param>
        public static void RemoveClass(this Style tis, string className)
        {
            tis.CssClass = tis.CssClass.Replace(className, string.Empty);
            tis.CssClass = tis.CssClass.Trim();
        }


    } // end of ExtWebControl
}
