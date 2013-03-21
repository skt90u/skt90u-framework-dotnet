using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace JUtil.Extensions
{
    /// <summary>
    /// Extension System.Collections.ArrayList
    /// </summary>
    public static class ExtArrayList
    {
        /// <summary>
        /// 將Array內容填入DataTable指定欄位的所有值
        /// </summary>
        /// <param name="source">an ArrayList</param>
        /// <param name="dt">an DataTable</param>
        /// <param name="field">the colName of the DataTable</param>
        public static void Fill(this ArrayList source, DataTable dt, string field)
        {
            source.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string fieldValue = row[field].ToString();
                source.Add(fieldValue);
            }
        }

        /// <summary>
        /// 將Array內容轉成sqlItems字串輸出 [e.g. select * from XXX where AAA in ($SqlItems$) ]
        /// </summary>
        /// <param name="source">an ArrayList</param>
        public static string ToSqlItems(this ArrayList source)
        {
            string sqlItems = string.Empty;
            
            foreach (object item in source)
            {
                if (0 != sqlItems.Length)
                {
                    sqlItems += ",";
                }

                sqlItems += string.Format("'{0}'", item.ToString());
            }

            return sqlItems;
        }


    } // end of ExtArrayList
}
