using System;
using System.Collections.Generic;
using System.Data;

namespace JUtil.Extensions
{
    public delegate bool CallBackPolicyMaker(DataRow row);

    /// <summary>Enhance control functionality</summary>
    public static class ExtDataTable
    {
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


    } // end of ExtDataTable
}
