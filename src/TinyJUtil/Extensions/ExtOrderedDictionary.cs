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
    public static class ExtOrderedDictionary
    {
        /// <summary>
        /// 將 OrderedDictionary內容轉換成指定型別
        /// </summary>
        public static DalType ConvertTo<DalType>(this OrderedDictionary od)
        {
            Type dalType = typeof(DalType);

            return (DalType)od.ConvertTo(dalType);
        }

        /// <summary>
        /// 將 OrderedDictionary內容轉換成指定型別
        /// </summary>
        public static object ConvertTo(this OrderedDictionary od, Type dalType)
        {
            object instance = Activator.CreateInstance(dalType);

            PropertyInfo[] props = dalType.GetProperties();
            foreach (PropertyInfo prop in props)
            {
                object value = od[prop.Name];

                value = Convert.IsDBNull(value) ? null : value.ConvertTo(prop.PropertyType);

                //value = value.ConvertTo(prop.PropertyType);

                prop.SetValue(instance, value, null);
            }

            return instance;
        }


    } // end of ExtDataRow
}
