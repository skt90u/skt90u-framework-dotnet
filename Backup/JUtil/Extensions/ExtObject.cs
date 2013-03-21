using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.ComponentModel;

namespace JUtil.Extensions
{
    /// <summary>Enhance Object functionality</summary>
    public static class ExtObject
    {

        /// <summary>determines whether the specified value is of numeric type.</summary>
        public static bool IsNumericType(this object value)
        {
            return (value is byte || value is sbyte || value is short || value is ushort || value is int || value is uint || value is long || value is ulong || value is float || value is double || value is decimal);
        }

        #region "Type Casting"

        /// <summary>implement a casting operation that support nullable object type-casting</summary>
        public static object ConvertTo(this object value, Type conversionType)
        {
            //http://aspalliance.com/852
            if (conversionType.IsGenericType && conversionType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
            {
                NullableConverter nullableConverter = new NullableConverter(conversionType);
                conversionType = nullableConverter.UnderlyingType;
            }
            return Convert.ChangeType(value, conversionType);
        }

        /// <summary>determines whether the specified value is positive.</summary>
        public static bool IsPositive(this object value, bool zeroIsPositive)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.SByte:
                    return (zeroIsPositive ? Convert.ToSByte(value) >= 0 : Convert.ToSByte(value) > 0);
                case TypeCode.Int16:
                    return (zeroIsPositive ? Convert.ToInt16(value) >= 0 : Convert.ToInt16(value) > 0);
                case TypeCode.Int32:
                    return (zeroIsPositive ? Convert.ToInt32(value) >= 0 : Convert.ToInt32(value) > 0);
                case TypeCode.Int64:
                    return (zeroIsPositive ? Convert.ToInt64(value) >= 0 : Convert.ToInt64(value) > 0);
                case TypeCode.Single:
                    return (zeroIsPositive ? Convert.ToSingle(value) >= 0 : Convert.ToSingle(value) > 0);
                case TypeCode.Double:
                    return (zeroIsPositive ? Convert.ToDouble(value) >= 0 : Convert.ToDouble(value) > 0);
                case TypeCode.Decimal:
                    return (zeroIsPositive ? Convert.ToDecimal(value) >= 0 : Convert.ToDecimal(value) > 0);
                case TypeCode.Byte:
                    return (zeroIsPositive ? true : Convert.ToByte(value) > 0);
                case TypeCode.UInt16:
                    return (zeroIsPositive ? true : Convert.ToUInt16(value) > 0);
                case TypeCode.UInt32:
                    return (zeroIsPositive ? true : Convert.ToUInt32(value) > 0);
                case TypeCode.UInt64:
                    return (zeroIsPositive ? true : Convert.ToUInt64(value) > 0);
                case TypeCode.Char:
                    return (zeroIsPositive ? true : Convert.ToChar(value) != '\0' );
                default:
                    return false;
            }
        }

        /// <summary>converts the specified values boxed type to its correpsonding unsigned type.</summary>
        public static object ToUnsigned(this object value)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.SByte:
                    return Convert.ToByte(Convert.ToSByte(value));
                case TypeCode.Int16:
                    return Convert.ToUInt16(Convert.ToInt16(value));
                case TypeCode.Int32:
                    return Convert.ToUInt32(Convert.ToInt32(value));
                case TypeCode.Int64:
                    return Convert.ToUInt64(Convert.ToInt64(value));
                case TypeCode.Byte:
                    return value;
                case TypeCode.UInt16:
                    return value;
                case TypeCode.UInt32:
                    return value;
                case TypeCode.UInt64:
                    return value;
                case TypeCode.Single:
                    return Convert.ToUInt32(Convert.ToSingle(value));
                case TypeCode.Double:
                    return Convert.ToUInt64(Math.Truncate(Convert.ToDouble(value)));
                case TypeCode.Decimal:
                    return Convert.ToUInt64(Math.Truncate(Convert.ToDecimal(value)));
                default:
                    return null;
            }
        }

        /// <summary>converts the specified values boxed type to its correpsonding integer type.</summary>
        public static object ToInteger(this object value, bool round)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.SByte:
                    return value;
                case TypeCode.Int16:
                    return value;
                case TypeCode.Int32:
                    return value;
                case TypeCode.Int64:
                    return value;
                case TypeCode.Byte:
                    return value;
                case TypeCode.UInt16:
                    return value;
                case TypeCode.UInt32:
                    return value;
                case TypeCode.UInt64:
                    return value;
                case TypeCode.Single:
                    return (round ? Convert.ToInt32(Math.Round(Convert.ToSingle(value))) : Convert.ToInt32(Math.Truncate(Convert.ToSingle(value))));
                case TypeCode.Double:
                    return (round ? Convert.ToInt64(Math.Round(Convert.ToDouble(value))) : Convert.ToInt64(Math.Truncate(Convert.ToDouble(value))));
                case TypeCode.Decimal:
                    return (round ? Math.Round(Convert.ToDecimal(value)) : Convert.ToDecimal(value));
                default:
                    return null;
            }
        }

        public static long UnboxToLong(this object value, bool round)
        {
            switch (Type.GetTypeCode(value.GetType()))
            {
                case TypeCode.SByte:
                    return Convert.ToInt64(Convert.ToSByte(value));
                case TypeCode.Int16:
                    return Convert.ToInt64(Convert.ToInt16(value));
                case TypeCode.Int32:
                    return Convert.ToInt64(Convert.ToInt32(value));
                case TypeCode.Int64:

                    return Convert.ToInt64(value);
                case TypeCode.Byte:
                    return Convert.ToInt64(Convert.ToByte(value));
                case TypeCode.UInt16:
                    return Convert.ToInt64(Convert.ToUInt16(value));
                case TypeCode.UInt32:
                    return Convert.ToInt64(Convert.ToUInt32(value));
                case TypeCode.UInt64:

                    return Convert.ToInt64(Convert.ToUInt64(value));
                case TypeCode.Single:
                    return (round ? Convert.ToInt64(Math.Round(Convert.ToSingle(value))) : Convert.ToInt64(Math.Truncate(Convert.ToSingle(value))));
                case TypeCode.Double:
                    return (round ? Convert.ToInt64(Math.Round(Convert.ToDouble(value))) : Convert.ToInt64(Math.Truncate(Convert.ToDouble(value))));
                case TypeCode.Decimal:
                    return (round ? Convert.ToInt64(Math.Round(Convert.ToDecimal(value))) : Convert.ToInt64(Math.Truncate(Convert.ToDecimal(value))));
                default:

                    return 0;
            }
        }
        #endregion


    } // end of ExtObject
}
