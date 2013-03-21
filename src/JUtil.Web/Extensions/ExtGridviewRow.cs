using JUtil.Extensions;
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
    /// <summary>
    /// GridView support command types
    /// </summary>
    public enum GridviewCommand
    {
        //
        // http://msdn.microsoft.com/zh-tw/library/system.web.ui.webcontrols.buttonfield.commandname(v=vs.80).aspx
        //

        /// <summary>
        /// GridviewCommand.Cancel
        /// </summary>
        Cancel = 0,

        /// <summary>
        /// GridviewCommand.Delete
        /// </summary>
        Delete = 1,

        /// <summary>
        /// GridviewCommand.Edit
        /// </summary>
        Edit = 2,

        /// <summary>
        /// GridviewCommand.Insert
        /// </summary>
        Insert = 3,

        /// <summary>
        /// GridviewCommand.New
        /// </summary>
        New = 4,

        /// <summary>
        /// GridviewCommand.Page
        /// </summary>
        Page = 5,

        /// <summary>
        /// GridviewCommand.Select
        /// </summary>
        Select = 6,

        /// <summary>
        /// GridviewCommand.Sort
        /// </summary>
        Sort = 7,

        /// <summary>
        /// GridviewCommand.Update
        /// </summary>
        Update = 8
    }

    /// <summary>Enhance GridviewRow functionality</summary>
    public static class ExtGridviewRow
    {
        /// <summary>
        /// get gridview command button according to GridviewCommand type
        /// </summary>
        public static IButtonControl GetButton(this GridViewRow aRow, GridviewCommand GridviewCommand)
        {
            return aRow.GetButton(GridviewCommand.ToString());
        }

        /// <summary>
        /// get button from GridViewRow relay on Button's CommandName
        /// </summary>
        public static IButtonControl GetButton(this GridViewRow aRow, string CommandName)
        {
            if (aRow.RowType != DataControlRowType.DataRow)
            {
                return null;
            }

            //Control oControl = aRow.Cells[0];
            foreach (Control oControl in aRow.Cells)
            {
                Control oChildControl = null;
                foreach (Control oChildControl_loopVariable in oControl.Controls)
                {
                    oChildControl = oChildControl_loopVariable;
                    if ((oChildControl) is IButtonControl)
                    {
                        if (string.Compare(((IButtonControl)oChildControl).CommandName, CommandName, true) == 0)
                        {
                            return oChildControl as IButtonControl;
                        }
                    }
                }
            }
            
            return null;
        }

        /// <summary>
        /// set button's text from GridViewRow relay on Button's CommandName
        /// </summary>
        public static void SetButtonText(this GridViewRow aRow, string CommandName, string txt)
        {
            IButtonControl btn = GetButton(aRow, CommandName);
            if (btn != null)
            {
                btn.Text = txt;
            }
        }

        /// <summary>Extract GridViewRow's Values to an OrderedDictionary</summary>
        public static DalType ExtractValues<DalType>(this GridViewRow aRow)
            where DalType : class
        {
            OrderedDictionary od = aRow.ExtractValues();

            DalType retVal = (od == null) ? null : od.ConvertTo<DalType>();

            return retVal;
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


    } // end of ExtGridviewRow
}
