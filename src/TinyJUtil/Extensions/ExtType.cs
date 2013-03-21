using System.Reflection;
using System;
using System.Data;

namespace JUtil.Extensions
{
    /// <summary>
    /// Extensions of Type
    /// </summary>
    public static class ExtType
    {
        /// <summary>
        /// type.Name to CSharpType
        /// </summary>
        public static string ToCSharpType(this Type type)
        {
            string sCSharpType = type.Name;

            switch (type.Name)
            {
                case "Int16":
                case "Int32":
                case "Decimal": return "int";

                case "Byte": return "byte";

                case "String": return "string";

                case "Boolean": return "bool";
            }

            return sCSharpType;
        }

        public static DbType ToDbType(this Type type)
        {
            TypeCode typeCode = Type.GetTypeCode(type);

            // no TypeCode equivalent for TimeSpan or DateTimeOffset 
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    return DbType.Boolean;
                case TypeCode.Byte:
                    return DbType.Byte;
                case TypeCode.Char:
                    return DbType.StringFixedLength;    // ???
                case TypeCode.DateTime: // Used for Date, DateTime and DateTime2 DbTypes
                    return DbType.DateTime;
                case TypeCode.Decimal:
                    return DbType.Decimal;
                case TypeCode.Double:
                    return DbType.Double;
                case TypeCode.Int16:
                    return DbType.Int16;
                case TypeCode.Int32:
                    return DbType.Int32;
                case TypeCode.Int64:
                    return DbType.Int64;
                case TypeCode.SByte:
                    return DbType.SByte;
                case TypeCode.Single:
                    return DbType.Single;
                case TypeCode.String:
                    return DbType.String;
                case TypeCode.UInt16:
                    return DbType.UInt16;
                case TypeCode.UInt32:
                    return DbType.UInt32;
                case TypeCode.UInt64:
                    return DbType.UInt64;
                case TypeCode.DBNull:
                case TypeCode.Empty:
                case TypeCode.Object:
                default:
                    return DbType.Object;
            }
        }

        /// <summary>
        /// another way to get type from typeName
        /// </summary>
        public static Type GetType(string typeName)
        {
            //
            // 在類別庫中，使用System.Type.GetType，而typeName是來自於
            // 另外的自訂的類別庫時，必須使用類似以下格式 (assembly-qualified name)
            // MyBLL.TheDataObject, MyBLL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
            //
            // 若要取得assembly-qualified name，可經由以下程式碼達成
            // string assemblyQualifiedName = typeof(MyBLL.TheDataObject).AssemblyQualifiedName;
            // Type type = System.Type.GetType(assemblyQualifiedName);
            //
            // 詳細內容以參考 http://msdn.microsoft.com/en-us/library/w3f99sx1.aspx
            //
            // 為了避免(assembly-qualified name)的問題，
            // 實做以下功能，使程式能夠不透過assembly-qualified name
            // 也能存取經由typeName取得對應的Type
            //

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (Assembly assembly in assemblies)
            {
                Type[] types = assembly.GetTypes();

                foreach (Type type in types)
                {
                    if (type.ToString().Equals(typeName))
                        return type;
                }
            }

            return null;
        }

    } // end of ExtString
}
