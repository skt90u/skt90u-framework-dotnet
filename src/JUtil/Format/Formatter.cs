using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;

namespace JUtil
{
    /// <summary>
    /// 提供其他格式化方式
    /// </summary>
    public static class Formatter
    {
        private const string LegalFieldPattern = @"[A-Za-z]{1}\w*";
        public const string NewValuePatternPrefix = "@";
        public const string OldValuePatternPrefix = "~";

        private static List<string> GetTokens(string sql, string pattern)
        {
            List<string> tokens = new List<string>();

            foreach (Match match in Regex.Matches(sql, pattern, RegexOptions.None))
                if (match.Success)
                    tokens.Add(match.Groups[0].Value);

            return tokens;
        }

        private static string AssignSqlField<T>(DatabaseType dbType, string sql, List<string> tokens, string patternPrefix, T Dal)
        {
            if (Dal == null)
                return sql;

            string result = sql;

            Type DalType = Dal.GetType();

            foreach (string token in tokens)
            {
                if (Dal == null)
                {
                    Log.W("無法此sql對應的欄位，設定成Dal對應的屬性，因為Dal是NULL");

                    return result;
                }

                string propertyName = token.Replace(patternPrefix, "");

                PropertyInfo pi = DalType.GetProperty(propertyName);

                object propertyValue = pi.GetValue(Dal, null);

                result = result.Replace(token, ToSqlField(propertyValue, dbType));
            }

            return result;
        }

        /// <summary>
        /// Format sql according its defined dal relation, in additional you need define which DbType you want
        /// 
        /// e.g. update TableA set Field1 = @Field1, Field2 = @Field2 where Condition1 = ~Condition1
        /// 
        /// where @ stand for newValue's property, ~ stand for oldValue's property
        /// </summary>
        /// <remarks>
        /// string sql = "update TableA set Field1 = @Field1, Field2 = @Field2 where Condition1 = ~Condition1";
        /// string result = Formatter.GenericFormat[Dal](Formatter.DbType.SqlServer, sql, newValue, oldValue);
        /// </remarks>
        public static string GenericFormat<T>(DatabaseType dbType, string sql, T newValue, T oldValue)
        {
            string newValuePattern = string.Format("{0}{1}", NewValuePatternPrefix, LegalFieldPattern);
            string oldValuePattern = string.Format("{0}{1}", OldValuePatternPrefix, LegalFieldPattern);

            string result = sql;

            List<string> newValueMatchTokens = GetTokens(sql, newValuePattern);
            List<string> oldValueMatchTokens = GetTokens(sql, oldValuePattern);

            result = AssignSqlField<T>(dbType, result, newValueMatchTokens, NewValuePatternPrefix, newValue);
            result = AssignSqlField<T>(dbType, result, oldValueMatchTokens, OldValuePatternPrefix, oldValue);

            return result;
        }

        /// <summary>
        /// Format sql according its type, in additional you need define which DbType you want
        /// </summary>
        public static string ObjectFormat(DatabaseType dbType, string sql, params object[] args)
        {
            if (args == null || args.Length == 0) return sql;

            string result = string.Empty;

            int count = 0;

            for (int i = 0; i < sql.Length; i++)
            {
                if (sql[i] == '?')
                {
                    // 如果給定的參數不夠就保留問號
                    if (count < args.Length)
                        result += ToSqlField(args[count], dbType);
                    else
                        result += '?';
                    count++;
                }
                else
                {
                    result += sql[i];
                }
            }

            if (count != args.Length)
            {
                Log.W("您所給的參數數目({0})，與此SQL({1})所包涵的問號數目({2})不一致", args.Length, sql, count);
            }

            return result;
        }

        private static string ToSqlField(object obj, DatabaseType dbType)
        {
            if (obj == null) return "NULL";

            string sqlField = string.Empty;

            TypeCode typeCode = Type.GetTypeCode(obj.GetType());
            switch (typeCode)
            {
                case TypeCode.Boolean:
                    {
                        sqlField = (bool)obj ? "1" : "0";
                    } break;

                case TypeCode.DateTime:
                    {
                        DateTime dt = (DateTime)obj;
                        int YY = dt.Year;
                        int MM = dt.Month;
                        int DD = dt.Day;
                        int HH = dt.Hour;
                        int NN = dt.Minute;
                        int SS = dt.Second;
                        switch (dbType)
                        {
                            case DatabaseType.Oracle:
                                {
                                    sqlField = string.Format("TO_DATE('{0}-{1}-{2} {3}:{4}:{5}', 'YYYY-MM-DD HH24:MI:SS')", YY, MM, DD, HH, NN, SS);
                                } break;
                            case DatabaseType.Sybase:
                            case DatabaseType.SqlServer:
                                {
                                    sqlField = string.Format("convert(datetime, '{0}-{1}-{2} {3}:{4}:{5}', 120)", YY, MM, DD, HH, NN, SS);
                                } break;
                            case DatabaseType.Access:
                                {
                                    sqlField = string.Format("CDate('{0}/{1}/{2} {3}:{4}:{5}')", YY, MM, DD, HH, NN, SS);
                                } break;
                            case DatabaseType.Db2:
                                {
                                    sqlField = string.Format("timestamp('{0}-{1}-{2} {3}:{4}:{5}')", YY, MM, DD, HH, NN, SS);
                                } break;
                            case DatabaseType.Mysql:
                                {
                                    sqlField = string.Format("'{0}/{1}/{2} {3}:{4}:{5}'", YY, MM, DD, HH, NN, SS);
                                } break;
                            case DatabaseType.TeraData:
                                {
                                    sqlField = string.Format("timestamp '{0}-{1}-{2} {3}:{4}:{5}'", YY, MM, DD, HH, NN, SS);
                                } break;
                        }
                    } break;

                case TypeCode.Char:
                case TypeCode.String:
                    {
                        sqlField = string.Format("'{0}'", obj);
                    } break;

                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                case TypeCode.Empty:
                case TypeCode.Object:
                case TypeCode.DBNull:
                case TypeCode.SByte:
                case TypeCode.Byte:
                default:
                    sqlField = obj.ToString();
                    break;
            }

            return sqlField;
        }

        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        public static byte[] HexStringToByteArray(string s)
        {
            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;
        }

        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }
    } // end of Formatter
}
