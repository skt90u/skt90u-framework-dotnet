using JUtil.Extensions;
using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace JUtil.Web.Extensions
{
    /// <summary>Enhance gridview functionality</summary>
    public static class ExtGridview
    {
        /// <summary>
        /// Extract SelectedValues
        /// </summary>
        public static OrderedDictionary ExtractSelectedValues(this GridView grv)
        {
            int SelectedIndex = grv.SelectedIndex;

            return SelectedIndex == -1 
                    ? null
                    : grv.Rows[grv.SelectedIndex].ExtractValues();
        }

        /// <summary>
        /// Extract SelectedValues
        /// </summary>
        public static DalType ExtractSelectedValues<DalType>(this GridView grv)
            where DalType : class
        {
            int SelectedIndex = grv.SelectedIndex;

            DalType retVal = SelectedIndex == -1
                    ? null
                    : grv.Rows[grv.SelectedIndex].ExtractValues<DalType>();

            return retVal;
        }

        // ----------------------------------------------------------'
        // The requirements of using ExportExcel/ExportWord function
        // ----------------------------------------------------------'
        //   - if you run ExportExcel/ExportWord functions 
        //     in codebehide, your derived page class must override 
        //     VerifyRenderingInServerForm subroutine, and do nothing.
        //     [ref : http://www.c-sharpcorner.com/uploadfile/dipalchoksi/exportxl_asp2_dc11032006003657am/exportxl_asp2_dc.aspx]
        //
        //   - ExportExcel/ExportWord functions must run under 
        //     PostBack mode, which mean if you put a button inside 
        //     update panel that can raise ExportExcel/ExportWord
        //     you need set the a PostBackTrigger like follow
        //     <asp:PostBackTrigger ControlID="$(your button id)" />
        //     [ref : http://nice-tutorials.blogspot.com/2009/06/export-gridview-to-excel-within-update.html]
        // ----------------------------------------------------------'

        /// <summary>export gridview content to a excel file</summary>
        public static void ExportExcel(this GridView grv, string fileName)
        {
            grv.Export(Encoding.UTF8, fileName, "application/vnd.ms-excel");
        }

        /// <summary>export gridview content to a word file</summary>
        public static void ExportWord(this GridView grv, string fileName)
        {
            grv.Export(Encoding.UTF8, fileName, "application/ms-word");
        }

        #region "core of Export"
        private static void Export(this GridView grv, Encoding enc, string fileName, string contentType)
        {
            if (grv.Rows.Count > 65535)
            {
                string err = string.Format("Out of range! the size of data is {0}, can not exceed 65535.", grv.Rows.Count);
                throw new ArgumentException(err);
            }

            HttpResponse response = HttpContext.Current.Response;
            try
            {
                fileName = HttpUtility.UrlEncode(fileName, enc);
                response.Clear();
                response.Cache.SetCacheability(HttpCacheability.NoCache);
                response.Charset = enc.WebName;
                response.ContentEncoding = enc;
                response.ContentType = contentType;

                string sText = string.Format("<meta http-equiv='Content-Type'; content='{0}';charset='{1}'>", contentType, enc.WebName);
                response.Write(sText);
                response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                System.IO.StringWriter stringWriter = new System.IO.StringWriter();
                System.Web.UI.HtmlTextWriter htmlWriter = new System.Web.UI.HtmlTextWriter(stringWriter);
                grv.RenderControl(htmlWriter);
                response.Write(stringWriter.ToString());
                response.Flush();
                response.End();
            }
            catch (ThreadAbortException)
            {
                // http://msdn.microsoft.com/zh-tw/library/ms182363.aspx
                // CA1002
                //Throw ex
                throw;
            }
        }
        #endregion


    } // end of ExtGridview
}
