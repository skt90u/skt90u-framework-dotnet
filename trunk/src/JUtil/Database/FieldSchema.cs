using System;
using System.Data;
using System.Reflection;
using System.Text;

namespace JUtil
{
    public class FieldSchema
    {
	    internal FieldSchema(DataRow row)
	    {
            ColumnName = toString(row, "ColumnName");
            ColumnOrdinal = toInt(row, "ColumnOrdinal");
            ColumnSize = toInt(row, "ColumnSize");
            NumericPrecision = toInt(row, "NumericPrecision");
            NumericScale = toInt(row, "NumericScale");
            DataType = toType(row, "DataType");
            ProviderType = toInt(row, "ProviderType");
            IsLong = toBool(row, "IsLong");
            AllowDBNull = toBool(row, "AllowDBNull");
            IsUnique = toBool(row, "IsUnique");
            IsKey = toBool(row, "IsKey");
            BaseSchemaName = toString(row, "BaseSchemaName");
            BaseTableName = toString(row, "BaseTableName");
            BaseColumnName = toString(row, "BaseColumnName");

            // --------------------------------------------------
            // SqlServer Only
            // --------------------------------------------------
            //IsReadOnly = toBool(row, "IsReadOnly");
            //IsRowVersion = toBool(row, "IsRowVersion");
            //IsAutoIncrement = toBool(row, "IsAutoIncrement");
            //BaseCatalogName = toString(row, "BaseCatalogName");

            // --------------------------------------------------
            // Oracle Only
            // --------------------------------------------------
            //IsAliased = toBool(row, "IsAliased");
            //IsExpression = toBool(row, "IsExpression");
            //ProviderSpecificDataType = toType(row, "ProviderSpecificDataType");
	    }

        public readonly string ColumnName;
        public readonly int? ColumnOrdinal;
        public readonly int? ColumnSize;
        public readonly int? NumericPrecision;
        public readonly int? NumericScale;
        public readonly Type DataType;
        public readonly int? ProviderType;
        public readonly bool? IsLong;
        public readonly bool? AllowDBNull;
        public readonly bool? IsUnique;
        public readonly bool? IsKey;
        public readonly string BaseSchemaName;
        public readonly string BaseTableName;
        public readonly string BaseColumnName;

        // --------------------------------------------------
        // SqlServer Only
        // --------------------------------------------------
        //public readonly bool? IsReadOnly;
        //public readonly bool? IsRowVersion;
        //public readonly bool? IsAutoIncrement;
        //public readonly string BaseCatalogName;

        // --------------------------------------------------
        // Oracle Only
        // --------------------------------------------------
        //public readonly bool IsAliased;
        //public readonly bool IsExpression;
        //public readonly Type ProviderSpecificDataType;

        public override string ToString()
        {
            StringBuilder output = new StringBuilder();

            Type t = GetType();

            output.Append("\n");

            foreach (FieldInfo fi in t.GetFields())
                output.AppendFormat("{0}: {1}\n", fi.Name, fi.GetValue(this));

            return output.ToString();
        }
        private bool isNonsense(object o)
        {
            return (o == null || DBNull.Value.Equals(o));
        }

        private object GetCell(DataRow row, string columnName)
        {
            try
            {
                return row[columnName];
            }
            catch
            {
                Log.E("在row中，找不到資料行{0}對應的資料", columnName);

                return null;
            }
        }
        private Type toType(DataRow row, string columnName)
        {
            object o = GetCell(row, columnName);

            if (isNonsense(o)) return null;

            return o as Type;
        }

        private string toString(DataRow row, string columnName)
        {
            object o = GetCell(row, columnName);

            if (isNonsense(o)) return null;

            return Convert.ToString(o);
        }

        private bool? toBool(DataRow row, string columnName)
        {
            object o = GetCell(row, columnName);

            if (isNonsense(o)) return null;

            return Convert.ToBoolean(o);
        }

        private int? toInt(DataRow row, string columnName)
        {
            object o = GetCell(row, columnName);

            if (isNonsense(o)) return null;

            return Convert.ToInt32(o);
        }
    }
}
