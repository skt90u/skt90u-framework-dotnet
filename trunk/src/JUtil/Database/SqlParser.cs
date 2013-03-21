using System;
using System.Text;
using JUtil.Extensions;

namespace JUtil.Extensions
{
    public static partial class ExtString
    {
        /// <summary>
        /// ToSqlToken
        /// </summary>
        public static SqlParser.SqlToken ToSqlToken(this string self)
        {
            SqlParser.SqlToken SqlToken = SqlParser.SqlToken.NOT_TOKEN;
            
            try
            {
                SqlToken = (SqlParser.SqlToken)Enum.Parse(typeof(SqlParser.SqlToken), self, true /*ignoreCase*/);
            }
            catch (Exception) { }

            return SqlToken;
        }
    }
}

namespace JUtil
{
    /// <summary>
    /// SqlParser
    /// </summary>
    public partial class SqlParser
    {
        public enum SqlType
        {
            /// <summary>
            /// ILLEGAL_SQL
            /// </summary>
            ILLEGAL_SQL,

            /// <summary>
            /// SELECT_SQL
            /// </summary>
            SELECT_SQL,

            /// <summary>
            /// INSERT_SQL
            /// </summary>
            INSERT_SQL,

            /// <summary>
            /// UPDATE_SQL
            /// </summary>
            UPDATE_SQL,

            /// <summary>
            /// DELETE_SQL
            /// </summary>
            DELETE_SQL
        }

        /// <summary>
        /// SqlToken
        /// </summary>
        public enum SqlToken
        {
            /// <summary>
            /// NOT_TOKEN
            /// </summary>
            NOT_TOKEN,

            /// <summary>
            /// SELECT
            /// </summary>
            SELECT,

            /// <summary>
            /// INSERT
            /// </summary>
            INSERT,

            /// <summary>
            /// UPDATE
            /// </summary>
            UPDATE,

            /// <summary>
            /// DELETE
            /// </summary>
            DELETE,

            /// <summary>
            /// ORDER
            /// </summary>
            ORDER
        }

        public static SqlType GetSqlType(string sql)
        {
            string[] tokens = GetTokens(sql);

            if (tokens.Length > 0)
            {
                string token = tokens[0];

                SqlToken SqlToken = token.ToSqlToken();

                switch (SqlToken)
                {
                    case SqlToken.SELECT: return SqlType.SELECT_SQL;
                    case SqlToken.INSERT: return SqlType.INSERT_SQL;
                    case SqlToken.UPDATE: return SqlType.UPDATE_SQL;
                    case SqlToken.DELETE: return SqlType.DELETE_SQL;
                }
            }

            return SqlType.ILLEGAL_SQL;
        }

        /// <summary>
        /// 去除Order By ...
        /// </summary>
        public static string GetSelectSqlNoOrderBy(string sql)
        {
            StringBuilder result = new StringBuilder();

            string[] tokens = GetTokens(sql);

            bool MeetSelect = false;

            foreach (string token in tokens)
            {
                if (HitSqlToken(token, SqlToken.ORDER))
                    break;

                if (HitSqlToken(token, SqlToken.SELECT))
                    MeetSelect = true;

                if (!MeetSelect)
                    continue;

                result.AppendFormat("{0} ", token);
            }

            return result.ToString().Trim();
        }

        private static bool HitSqlToken(string token, SqlToken SqlToken)
        {
            return token.ToSqlToken() == SqlToken;
        }

        private static string[] GetTokens(string sql)
        {
            return sql.Split(new char[] { ' ', '\t' });
        }
    }
}
