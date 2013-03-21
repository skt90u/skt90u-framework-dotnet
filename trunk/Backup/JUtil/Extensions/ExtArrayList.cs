using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;

namespace JUtil.Extensions
{
    //
    // Extension System.Collections.ArrayList
    //
    public static class ExtArrayList
    {
        public static void Fill(this ArrayList source, DataTable dt, string field)
        {
            source.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string fieldValue = row[field].ToString();
                source.Add(fieldValue);
            }
        }

        public static string ToSqlItems(this ArrayList source)
        {
            string sqlItems = string.Empty;
            
            bool firstItem = true;
            foreach (object item in source)
            {
                if (firstItem)
                {
                    firstItem = false;
                }
                else
                {
                    sqlItems += ",";
                }
                sqlItems += string.Format("'{0}'", item.ToString());
            }
            return sqlItems;
        }


    } // end of ExtArrayList
}
