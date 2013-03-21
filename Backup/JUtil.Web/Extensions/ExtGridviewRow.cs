using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Specialized;

namespace JUtil.Web.Extensions
{
    /// <summary>Enhance GridviewRow functionality</summary>
    public static class ExtGridviewRow
    {

        /// <summary>
        /// get button from GridViewRow relay on Button's CommandName
        /// </summary>
        /// <param name="aRow">GridviewRow</param>
        /// <param name="sCommandName">name of Button (maybe it's same as id , not sure !!)</param>
        /// <returns>return null if aRow is not in DataRow mode</returns>
        public static Button GetButton(this GridViewRow aRow, string sCommandName)
        {
            if (aRow.RowType != DataControlRowType.DataRow)
            {
                return null;
            }

            Control oControl = aRow.Cells[0];
            Control oChildControl = null;
            foreach (Control oChildControl_loopVariable in oControl.Controls)
            {
                oChildControl = oChildControl_loopVariable;
                if ((oChildControl) is Button)
                {
                    if (string.Compare(((Button)oChildControl).CommandName, sCommandName, true) == 0)
                    {
                        return oChildControl as Button;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// set button's text from GridViewRow relay on Button's CommandName
        /// </summary>
        public static void SetButtonText(this GridViewRow aRow, string sCommandName, string txt)
        {
            Button btn = GetButton(aRow, sCommandName);
            if (btn != null)
            {
                btn.Text = txt;
            }
        }

        /// <summary>Extract GridViewRow's Values to an OrderedDictionary</summary>
        public static OrderedDictionary ExtractValues(this GridViewRow aRow)
        {
            if (aRow.RowType != DataControlRowType.DataRow)
            {
                return null;
            }

            GridView grv = aRow.Parent.Parent as GridView;
            DataControlFieldCollection columns = grv.Columns;
            int nColumns = columns.Count;
            OrderedDictionary Result = new OrderedDictionary(nColumns);

            for (int i = 0; i <= nColumns - 1; i++)
            {
                DataControlField aColumn = columns[i];

                if ((aColumn is CommandField))
                    continue;

                OrderedDictionary oDictionary = new OrderedDictionary();
                if ((aColumn is TemplateField))
                {
                    ((TemplateField)aColumn).ExtractValuesFromCell(oDictionary, aRow.Cells[i] as DataControlFieldCell, aRow.RowState, true);
                }
                else
                {
                    aColumn.ExtractValuesFromCell(oDictionary, aRow.Cells[i] as DataControlFieldCell, aRow.RowState, true);
                }

                foreach (DictionaryEntry entry in oDictionary)
                {
                    Result[entry.Key] = entry.Value;
                }
            }
            return Result;
        }
    }
}
