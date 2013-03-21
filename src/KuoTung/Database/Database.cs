using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;

namespace KuoTung
{
    public class Database
    {
        private readonly DatabaseCore _databaseCore;

        public Database(DbProvider dbProvider, string connectionString)
        {
            _databaseCore = new DatabaseCore(dbProvider, connectionString);
        }

        /// <summary>
        /// 一次查詢單一資料表
        /// </summary>
        public DataTable Select(string sql)
        {
            DataTable dt;

            using (DbCommand command = _databaseCore.CreateDbCommand())
            {
                command.CommandText = sql;

                using (DbDataAdapter adapter = _databaseCore.CreateDataAdapter())
                {
                    // http://msdn.microsoft.com/zh-tw/library/fks3666w.aspx
                    adapter.SelectCommand = command;

                    dt = new DataTable();
                    adapter.Fill(dt);
                }
            }

            return dt;
        }

        /// <summary>
        /// 一次查詢多筆資料表
        /// </summary>
        public DataSet Select(List<KeyValuePair<string, string>> sqls)
        {
            DataSet ds = new DataSet();

            foreach (KeyValuePair<string, string> querySql in sqls)
            {
                using (DbCommand command = _databaseCore.CreateDbCommand())
                {
                    command.CommandText = querySql.Value;

                    using (DbDataAdapter adapter = _databaseCore.CreateDataAdapter())
                    {
                        // http://msdn.microsoft.com/zh-tw/library/fks3666w.aspx
                        adapter.SelectCommand = command;

                        adapter.Fill(ds, querySql.Key);
                    }
                }
            }

            return ds;
        }

        /// <summary>
        /// 查詢資料筆數
        /// </summary>
        public int SelectCount(string sql)
        {
            int count = 0;

            string aliasField = "RECORDCOUNT";
            // sql server :
            // http://tw.myblog.yahoo.com/franklkjii/article?mid=437
            string aliasTable = "RECORDCOUNT_TABLE";

            // SELECT COUNT(*) AS RECORDCOUNT FROM (select * from Categories order by CategoryID ) RECORDCOUNT_TABLE
            // 會發生以下例外
            // 除非同時指定了 TOP 或 FOR XML，否則 ORDER BY 子句在檢視表、內嵌函數、衍生資料表、子查詢及通用資料表運算式中均為無效
            //
            // 解決方式 : 把 order by ... 去除
            string selectSqlNoOrderBy = SqlParser.GetSelectSqlNoOrderBy(sql);

            string recordcountSQL = string.Format("SELECT COUNT(*) AS {0} FROM ({1}) {2}", aliasField, selectSqlNoOrderBy, aliasTable);
            using (DataTable dt = Select(recordcountSQL))
            {
                if (dt.Rows.Count == 1)
                    count = Convert.ToInt32(dt.Rows[0][aliasField]);
            }
            
            return count;
        }

        /// <summary>
        /// 一次執行單一資料庫異動操作(新增，移除，更新資料)
        /// (若失敗，則Rollback.)
        /// </summary>
        public void Execute(string sql)
        {
            Execute(new List<string> { sql });
        }

        /// <summary>
        /// 一次執行單一資料庫異動操作(新增，移除，更新資料)
        /// (若失敗，則Rollback.)
        /// 當使用 Execute(sql), e.g. sql 字串長度過常會出錯
        /// 必須使用此function
        /// </summary>
        public void Execute(string sql, Hashtable inParameters)
        {
            using (DbCommand command = _databaseCore.CreateDbCommand())
            {
                using (DbTransaction transaction = _databaseCore.CreateTransaction(command))
                {
                    try
                    {
                        command.CommandText = sql;

                        AddParameters(command, ParameterDirection.Input, inParameters);

                        command.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void Execute(ICollection<string> sqls)
        {
            using (DbCommand command = _databaseCore.CreateDbCommand())
            {
                using (DbTransaction transaction = _databaseCore.CreateTransaction(command))
                {
                    try
                    {
                        IEnumerator<string> enumerator = sqls.GetEnumerator();
                        while (enumerator.MoveNext())
                        {
                            string sql = enumerator.Current;
                            command.CommandText = sql;
                            command.ExecuteNonQuery();
                        }
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                } 
            } 
        }

        /// <summary>
        /// 執行 store procedure 
        /// </summary>
        /// <remarks>
        /// 好像還是不成功
        /// </remarks>
        public void ExecuteSp(string procedureName, Hashtable inParameters, Hashtable outParameters, Hashtable retParameters)
        {
            using (DbCommand command = _databaseCore.CreateDbCommand())
            {
                using (DbTransaction transaction = _databaseCore.CreateTransaction(command))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = procedureName;

                        AddParameters(command, ParameterDirection.Input, inParameters);
                        AddParameters(command, ParameterDirection.Output, outParameters);
                        AddParameters(command, ParameterDirection.ReturnValue, retParameters);

                        command.ExecuteNonQuery();

                        transaction.Commit();

                        GetParameters(command, outParameters);
                        GetParameters(command, retParameters);
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public DataTable ExecuteSp(string procedureName, Hashtable inParameters)
        {
            DataTable dt;

            using (DbCommand command = _databaseCore.CreateDbCommand())
            {
                using (DbTransaction transaction = _databaseCore.CreateTransaction(command))
                {
                    try
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = procedureName;

                        AddParameters(command, ParameterDirection.Input, inParameters);

                        using (DbDataAdapter adapter = _databaseCore.CreateDataAdapter())
                        {
                            // http://msdn.microsoft.com/zh-tw/library/fks3666w.aspx
                            adapter.SelectCommand = command;

                            dt = new DataTable();
                            adapter.Fill(dt);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }

            return dt;
        }

        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////////////////
        
        private void AddParameters(DbCommand command, ParameterDirection direction, Hashtable parameters)
        {
            if (parameters == null) return;

            foreach (object oKey in parameters.Keys)
            {
                string key = oKey.ToString();
                object value = parameters[oKey];

                command.Parameters.Add(CreateParameter(direction, key, value));
            }
        }

        private DbParameter CreateParameter(ParameterDirection direction, string key, object value)
        {
            if (value == null)
                value = DBNull.Value;

            DbParameter parameter = _databaseCore.DbProviderFactory.CreateParameter();

            parameter.Direction = direction;
            parameter.ParameterName = key;
            parameter.DbType = GetDbType(value.GetType());
            parameter.Value = value;

            return parameter;
        }

        private void GetParameters(DbCommand command, Hashtable parameters)
        {
            if (parameters == null) return;

            Hashtable refParameters = new Hashtable(parameters);

            parameters.Clear();

            foreach (DictionaryEntry de in refParameters)
            {
                string key = de.Key.ToString();
                object value = command.Parameters[key].Value;

                parameters.Add(de.Key, value);
            }
        }

        //private static DbType GetDbType(Type type)
        public static DbType GetDbType(Type type)
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
                /*
                case TypeCode.DBNull:
                case TypeCode.Empty:
                case TypeCode.Object:
                */
                default:
                    return DbType.Object;
            }
        }
    }
}
