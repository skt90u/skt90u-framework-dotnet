using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Collections;

namespace JUtil.Extensions
{
    /// <summary>
    /// Extension System.Data.DataRow
    /// </summary>
    public static class ExtDataRow
    {
        /// <summary>
        /// 將 DataRow內容轉換成指定型別
        /// </summary>
        /// <typeparam name="DalType">待轉換指定型別</typeparam>
        /// <param name="row">an DataRow</param>
        /// <returns></returns>
        public static DalType ConvertTo<DalType>(this DataRow row) 
        {
            Type dalType = typeof(DalType);

            DalType dalObject = (DalType)Activator.CreateInstance(dalType);

            PropertyInfo[] props = dalType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object value = null;

                //如果找不到 columnName 指定的資料行，會拋出ArgumentException
                try
                {
                    value = row[prop.Name];
                }
                catch (Exception ex)
                {
                    string warning = string.Format("row[\"{0}\"]: {1}(若此Dal欄位是額外新增的，無DataRow對應的欄位，可忽略此訊息。)", prop.Name, ex.Message);
                    Log.W(warning);
                }

                value = (value == null || Convert.IsDBNull(value))
                            ? null
                            : value.ConvertTo(prop.PropertyType);

                prop.SetValue(dalObject, value, null);
            }

            return dalObject;
        }

        /// <summary>
        /// 將 DataRow內容轉換成OrderedDictionary型別
        /// </summary>
        /// <param name="row">an DataRow</param>
        /// <returns></returns>
        public static OrderedDictionary ConvertTo(this DataRow row) 
        {
            OrderedDictionary od = new OrderedDictionary();

            DataColumnCollection columns = row.Table.Columns;
            foreach (DataColumn column in columns)
            {
                string key = column.ColumnName;
                object value = row[key];
                od.Add(key, value);
            }

            return od;
        }

        /// <summary>
        /// 將指定型別內容轉寫入DataRow
        /// </summary>
        /// <typeparam name="DalType">指定型別</typeparam>
        /// <param name="row">待寫入DataRow</param>
        /// <param name="dalObject">指定型別內容</param>
        public static void LoadFrom<DalType>(this DataRow row, DalType dalObject) 
        {
            Type dalType = typeof(DalType);

            PropertyInfo[] props = dalType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                Type colType = row[prop.Name].GetType();

                object value = prop.GetValue(dalObject, null);

                value = value.ConvertTo(colType);

                row[prop.Name] = value;
            }
        }

        /// <summary>
        /// 將指定型別內容轉寫入OrderedDictionary
        /// </summary>
        /// <param name="row">待寫入DataRow</param>
        /// <param name="od">OrderedDictionary內容</param>
        public static void LoadFrom(this DataRow row, OrderedDictionary od) 
        {
            foreach (DictionaryEntry item in od)
            {
                row[item.Key.ToString()] = item.Value;
            }
        }


    } // end of ExtDataRow
}
