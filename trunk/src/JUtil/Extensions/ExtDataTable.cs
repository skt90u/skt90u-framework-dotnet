using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using JUtil.Extensions;
using System.Text;

namespace JUtil.Extensions
{
    /// <summary>
    /// determine a DataRow is match a specified rule
    /// </summary>
    /// <param name="row"></param>
    /// <returns></returns>
    public delegate bool CallBackPolicyMaker(DataRow row);

    /// <summary>Enhance control functionality</summary>
    public static class ExtDataTable
    {
        /// <remarks>
        /// 需加入參考
        ///     References右鍵AddReferences => COM => Microsoft Excel 10.0 Object Library
        ///     在References會多Excel及Microsoft.Office.Core
        /// </remarks>
        public static void ToExcel(this DataTable dt, string filepath)
        {
            /*
            DataTable thisTable = dt;
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;
 
            try
            {
                oXL = new Excel.Application();
                //加入新的活頁簿
                oWB =(Excel._Workbook)oXL.Workbooks.Add(Excel.XlWBATemplate.xlWBATWorksheet);
                //引用工作表
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
             
                //Sheet名稱
                oSheet.Name = "Excel測試文件";
             
                //加入內容
                oSheet.Cells[1, 1] = "Excel測試文件";
             
                for(int i = 1; i <= thisTable.Rows.Count; i++)
                {
                    for(int j = 1; j <= thisTable.Columns.Count; j++)
                    {
                        oSheet.Cells[i + 1, j] = thisTable.Rows[i - 1][j - 1];
                    }
                }
             
                //合併儲存格範圍
                oRng = oSheet.get_Range(oSheet.Cells[1, 1], oSheet.Cells[1, thisTable.Columns.Count]);  
                oRng.MergeCells = true;//合併儲存格
                oRng.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//文字對齊方式
                oRng.NumberFormat = "yyyy-MM-dd";//設定欄位格式
                oRng.ColumnWidth = 20;//設定欄位寬度
             
                //存檔
                string SavePath = @"D:\";
                string FileName = "Excel測試文件";
             
                //若為EXCEL2000, 將最後一個參數拿掉即可
                oWB.SaveAs(SavePath + FileName, Excel.XlFileFormat.xlWorkbookNormal,
                    null, null, false, false, Excel.XlSaveAsAccessMode.xlShared,
                    false, false, null, null, null);
             
                //關閉文件
                oWB.Close(null, null, null);
                oXL.Workbooks.Close();
                oXL.Quit();
             
                //釋放資源
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oXL);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oSheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(oWB);
                oSheet = null;
                oWB = null;
                oXL = null;
             
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = @"D:\" + FileName + ".xls";
                process.Start();
                process.Close();
             
                //刪除檔案
                if(File.Exists(@"D:\" + FileName + ".xls"))
                    File.Delete(@"D:\" + FileName + ".xls");
            }
            catch(Exception ex)
            {
                throw ex;
            }
            */
        }

        /// <summary>
        /// 將數據表轉換成JSON類型串
        /// </summary>
        /// <param name="dt">要轉換的數據表</param>
        /// <returns></returns>
        public static StringBuilder ToJson(this DataTable dt)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("[\r\n");

            //數據表字段名和類型數組
            string[] dt_field = new string[dt.Columns.Count];
            int i = 0;
            string formatStr = "{{";
            string fieldtype = "";
            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                dt_field[i] = dc.Caption.ToLower().Trim();
                formatStr += "'" + dc.Caption.ToLower().Trim() + "':";
                fieldtype = dc.DataType.ToString().Trim().ToLower();
                if (fieldtype.IndexOf("int") > 0 || fieldtype.IndexOf("deci") > 0 ||
                    fieldtype.IndexOf("floa") > 0 || fieldtype.IndexOf("doub") > 0 ||
                    fieldtype.IndexOf("bool") > 0)
                {
                    formatStr += "{" + i + "}";
                }
                else
                {
                    formatStr += "'{" + i + "}'";
                }
                formatStr += ",";
                i++;
            }

            if (formatStr.EndsWith(","))
                formatStr = formatStr.Substring(0, formatStr.Length - 1);//去掉尾部","號

            formatStr += "}},";

            i = 0;
            object[] objectArray = new object[dt_field.Length];
            foreach (System.Data.DataRow dr in dt.Rows)
            {

                foreach (string fieldname in dt_field)
                {   //對 \ , ' 符號進行轉換 
                    objectArray[i] = dr[dt_field[i]].ToString().Trim().Replace("\\", "\\\\").Replace("'", "\\'");
                    switch (objectArray[i].ToString())
                    {
                        case "True":
                            {
                                objectArray[i] = "true"; break;
                            }
                        case "False":
                            {
                                objectArray[i] = "false"; break;
                            }
                        default: break;
                    }
                    i++;
                }
                i = 0;
                stringBuilder.Append(string.Format(formatStr, objectArray));
            }
            if (stringBuilder.ToString().EndsWith(","))
                stringBuilder.Remove(stringBuilder.Length - 1, 1);//去掉尾部","號

            return stringBuilder.Append("\r\n];");
        }

        public static void ToCSV(this DataTable dt, string filepath)
        {
            StringBuilder output = new StringBuilder();

            // output column name
            string header = string.Empty;
            foreach (DataColumn col in dt.Columns)
            {
                if (!string.IsNullOrEmpty(header))
                    header += ", ";

                header += col.ColumnName;
            }
            output.AppendFormat("{0}\n", header);

            // output data
            string rowData;
            foreach (DataRow row in dt.Rows)
            {
                rowData = string.Empty;

                foreach (DataColumn col in dt.Columns)
                {
                    if (!string.IsNullOrEmpty(rowData))
                        rowData += ", ";

                    rowData += row[col.ColumnName];
                }
                output.AppendFormat("{0}\n", rowData);
            }

            output.ToString().SaveAs(filepath);
        }

        /// <summary>
        /// ToDataRow
        /// </summary>
        public static DataRow ToDataRow<T>(this DataTable dt, T aRec)
        {
            DataRow aNewRow = dt.NewRow();
            Type tDal = typeof(T);
            PropertyInfo[] props = tDal.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object propValue = prop.GetValue(aRec, null);
                if (propValue == null)
                {
                    propValue = DBNull.Value;
                }
                aNewRow[prop.Name] = propValue;
            }
            return aNewRow;
        }

        /// <summary>
        /// 判斷所擷取的資料是否符合特定條件(PolicyMaker)
        /// </summary>
        public static bool MatchPolicy(this DataTable source, CallBackPolicyMaker fnPolicyMaker)
        {
            foreach (DataRow row in source.Rows)
                if (fnPolicyMaker(row))
                    return true;

            return false;
        }

        /// <summary>just for debug, display the content of datatable</summary>
        public static void Display(this DataTable dt)
        {
            bool bFirst = true;
            foreach (DataColumn col in dt.Columns)
            {
                if (bFirst)
                {
                    bFirst = false;
                }
                else
                {
                    Console.Write(", ");
                }
                Console.Write(col.ColumnName);
            }
            Console.WriteLine("");

            foreach (DataRow row in dt.Rows)
            {
                bFirst = true;
                foreach (DataColumn col in dt.Columns)
                {
                    if (bFirst)
                    {
                        bFirst = false;
                    }
                    else
                    {
                        Console.Write(", ");
                    }
                    Console.Write(row[col.ColumnName]);
                }
                Console.WriteLine("");
            }
        }

        /// <summary>get all fields's name of datatable</summary>
        public static List<string> GetFieldNames(this DataTable dt)
        {
            List<string> fieldNames = new List<string>();
            for (int i = 0; i <= dt.Columns.Count - 1; i++)
            {
                fieldNames.Add(dt.Columns[i].ColumnName);
            }
            return fieldNames;
        }

        /// <summary>get record count of datatable</summary>
        public static int Count(this DataTable dt)
        {
            return dt.Rows.Count;
        }

        /// <summary>get field count of datatable</summary>
        public static int FieldCount(this DataTable dt)
        {
            return dt.Columns.Count;
        }
        /// <summary>get field's value of n-th data in datatable by field's name</summary>
        public static object GetValue(this DataTable dt, int recNo, string field)
        {
            return dt.Rows[recNo][field];
        }

        /// <summary>get field's value of n-th data in datatable by field's index in the order</summary>
        public static object GetValue(this DataTable dt, int recNo, int fieldNo)
        {
            return dt.Rows[recNo][fieldNo];
        }

        /// <summary>set field's value of n-th data in datatable by field's name</summary>
        public static void SetValue(this DataTable dt, int recNo, string field, object value)
        {
            Type conversionType = dt.FieldType(field);
            dt.Rows[recNo][field] = value.ConvertTo(conversionType);
        }

        /// <summary>set field's value of n-th data in datatable by field's index in the order</summary>
        public static void SetValue(this DataTable dt, int recNo, int fieldNo, object value)
        {
            Type conversionType = dt.FieldType(fieldNo);
            dt.Rows[recNo][fieldNo] = value.ConvertTo(conversionType);
        }

        /// <summary>get field's type from datatable by field's index in the order</summary>
        private static Type FieldType(this DataTable dt, int fieldNo)
        {
            return dt.Columns[fieldNo].DataType;
        }

        /// <summary>get field's type from datatable by field's name</summary>
        private static Type FieldType(this DataTable dt, string field)
        {
            return dt.Columns[field].DataType;
        }

        /// <summary>
        /// Convert DataTable to a list of object with specified type
        /// </summary>
        /// <typeparam name="DalType"></typeparam>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<DalType> ConvertTo<DalType>(this DataTable dt) 
        {
            List<DalType> lst = new List<DalType>();
            foreach (DataRow row in dt.Rows)
            {
                DalType dal = row.ConvertTo<DalType>();
                lst.Add(dal);
            }
            return lst;
        }

        /// <summary>
        /// load DataTable from a list of obct with specified type
        /// </summary>
        /// <typeparam name="DalType"></typeparam>
        /// <param name="dt"></param>
        /// <param name="lst"></param>
        public static void LoadFrom<DalType>(this DataTable dt, List<DalType> lst) 
        {
            foreach (DalType dal in lst)
            {
                DataRow row = dt.NewRow();

                row.LoadFrom<DalType>(dal);

                dt.Rows.Add(row);
            }
        }

        /// <summary>
        /// page DataTable via startRowIndex, maximumRows
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <returns></returns>
        public static DataTable Paging(this DataTable dt, int startRowIndex, int maximumRows)
        {
            if (dt.Rows.Count == 0) return dt.Clone();

            var pagedData = dt.AsEnumerable().Skip(startRowIndex).Take(maximumRows);
            return pagedData.CopyToDataTable();
        }


    } // end of ExtDataTable
}
