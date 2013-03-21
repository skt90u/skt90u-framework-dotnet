using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JUtil
{
    /// <remarks>
    /// lazy initialize DbConnection when using it at first time called by SelectSQL or ExecuteSQL,
    /// and close DbConnection in Finalize() or Dispose()
    /// </remarks>
    internal class WebDatabase : IDatabase
    {
        public WebDatabase(DbConnectionConfig dbConnectionConfig)
        {
            this.dbConnectionConfig = dbConnectionConfig;

            dbProvider = dbConnectionConfig.DbProvider;
            connectionString = dbConnectionConfig.ConnectionString;
        }

        #region FieldsPrivate

        private readonly DbConnectionConfig dbConnectionConfig;
        private readonly DbProvider dbProvider;
        private readonly string connectionString;

        #region DbProviderFactory

        private DbProviderFactory factory = null;
        private DbProviderFactory Factory
        {
            get
            {
                if (factory == null)
                {
                    foreach (DbInfo dbInfo in DbInfo.ArrDbInfo)
                    {
                        if (dbInfo.DatabaseMode == dbProvider)
                        {
                            factory = DbProviderFactories.GetFactory(dbInfo.Provider);
                            break;
                        }
                    }

                    if (factory == null)
                    {
                        throw new Exception("specify DatabaseMode can not be supported.");
                    }
                }

                return factory;
            }
        }

        #endregion

        #endregion

        #region MethodsPrivate

        #region CreateConnection

        private DbConnection CreateConnection()
        {
            DbConnection conn = Factory.CreateConnection();
            conn.ConnectionString = connectionString;
            return conn;
        }

        #endregion

        #region CreateAdapter

        private DbDataAdapter CreateAdapter()
        {
            return Factory.CreateDataAdapter();
        }

        #endregion

        #region CreateDbCommand

        private DbCommand CreateDbCommand(DbConnection conn)
        {
            DbCommand command = Factory.CreateCommand();

            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }

            command.Connection = conn;

            return command;
        }

        #endregion

        #region CreateTransaction

        private DbTransaction CreateTransaction(DbConnection conn, DbCommand command)
        {
            DbTransaction transaction = conn.BeginTransaction();
            command.Transaction = transaction;
            return transaction;
        }

        #endregion

        #endregion

        #region IDatabase 成員

        public DataTable SelectSQL(string sql)
        {
            DataTable dt = null;

            using (DbConnection conn = CreateConnection())
            {
                using (DbCommand command = CreateDbCommand(conn))
                {
                    command.CommandText = sql;

                    using (DbDataAdapter adapter = CreateAdapter())
                    {
                        // http://msdn.microsoft.com/zh-tw/library/fks3666w.aspx
                        adapter.SelectCommand = command;

                        dt = new DataTable();
                        adapter.Fill(dt);
                    }
                }
            }

            return dt;
        }

        public DataSet SelectSQL(List<KeyValuePair<string, string>> sqls)
        {
            DataSet ds = null;

            using (DbConnection conn = CreateConnection())
            {
                ds = new DataSet();

                foreach (KeyValuePair<string, string> querySQL in sqls)
                {
                    using (DbCommand command = CreateDbCommand(conn))
                    {
                        command.CommandText = querySQL.Value;

                        using (DbDataAdapter adapter = CreateAdapter())
                        {
                            // http://msdn.microsoft.com/zh-tw/library/fks3666w.aspx
                            adapter.SelectCommand = command;

                            adapter.Fill(ds, querySQL.Key);
                        }
                    }
                }
            }

            return ds;
        }

        public int SelectCountSQL(string sql)
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

            using (DataTable dt = SelectSQL(recordcountSQL))
            {
                if (dt.Rows.Count == 1)
                    count = Convert.ToInt32(dt.Rows[0][aliasField]);
            }

            return count;
        }

        public void ExecuteSQL(string sql)
        {
            ExecuteSQL(new List<string> { sql });
        }

        public void ExecuteSQL(ICollection<string> sqls)
        {
            using (DbConnection conn = CreateConnection())
            {
                using (DbCommand command = CreateDbCommand(conn))
                {
                    using (DbTransaction transaction = CreateTransaction(conn, command))
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
                        catch (Exception)
                        {
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            throw;
                        }
                    } //using (DbTransaction transaction = CreateTransaction(conn, command))
                } //using (DbCommand command = CreateDbCommand(conn))
            } //using (DbConnection conn = CreateConnection())
        }

        private DbParameter CreateParameter(ParameterDirection direction, string key, object value)
        {
            TypeCode typeCode = Type.GetTypeCode(value.GetType());
            DbType dbType = System.Web.UI.WebControls.Parameter.ConvertTypeCodeToDbType(typeCode);

            DbParameter parameter = Factory.CreateParameter();
            parameter.Direction = direction;
            parameter.ParameterName = key;
            parameter.DbType = dbType;
            parameter.Value = value;

            return parameter;
        }

        private void AddDbParameters(DbCommand command, ParameterDirection direction, Hashtable Parameters)
        {
            if (Parameters == null) return;
            
            foreach (object oKey in Parameters.Keys)
            {
                string key = oKey.ToString();
                object value = Parameters[oKey];

                command.Parameters.Add(CreateParameter(direction, key, value));
            }
        }

        private void GetDbParameters(DbCommand command, Hashtable Parameters)
        {
            if (Parameters == null) return;

            foreach (object oKey in Parameters.Keys)
            {
                string key = oKey.ToString();
                object value = command.Parameters[key].Value;
                Parameters[oKey] = value;
            }
        }

        public void ExecuteSP(string procedureName,
                              Hashtable inParameters,
                              Hashtable outParameters,
                              Hashtable retParameters)
        {
            using (DbConnection conn = CreateConnection())
            {
                using (DbCommand command = CreateDbCommand(conn))
                {
                    using (DbTransaction transaction = CreateTransaction(conn, command))
                    {
                        try
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandText = procedureName;

                            AddDbParameters(command, ParameterDirection.Input, inParameters);
                            AddDbParameters(command, ParameterDirection.Output, outParameters);
                            AddDbParameters(command, ParameterDirection.ReturnValue, retParameters);

                            command.ExecuteNonQuery();

                            transaction.Commit();

                            GetDbParameters(command, outParameters);
                            GetDbParameters(command, retParameters);
                        }
                        catch (Exception)
                        {
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            throw;
                        }
                    } //using (DbTransaction transaction = CreateTransaction(conn, command))
                } //using (DbCommand command = CreateDbCommand(conn))
            } //using (DbConnection conn = CreateConnection())
        }

        public void ExecuteSQL(string sql, Hashtable inParameters)
        {
            using (DbConnection conn = CreateConnection())
            {
                using (DbCommand command = CreateDbCommand(conn))
                {
                    using (DbTransaction transaction = CreateTransaction(conn, command))
                    {
                        try
                        {
                            command.CommandText = sql;

                            AddDbParameters(command, ParameterDirection.Input, inParameters);

                            command.ExecuteNonQuery();

                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception)
                            {
                                throw;
                            }
                            throw;
                        }
                    } //using (DbTransaction transaction = CreateTransaction(conn, command))
                } //using (DbCommand command = CreateDbCommand(conn))
            } //using (DbConnection conn = CreateConnection())
        }

        public DataTable GetTableSchema(string sql)
        {
            //
            // reference : http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqldatareader.GetTableSchema.aspx
            //
            DataTable dt = null;

            using (DbConnection conn = CreateConnection())
            {
                using (DbCommand command = CreateDbCommand(conn))
                {
                    command.CommandText = sql;
                    using (DbDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                    {
                        dt = reader.GetSchemaTable();
                    }
                }
            }

            return dt;
        }

        public DataTable GetSchema(string collectionName)
        {
            DataTable dt = null;

            using (DbConnection conn = CreateConnection())
            {
                conn.Open();

                dt = string.IsNullOrEmpty(collectionName) ? conn.GetSchema() : conn.GetSchema(collectionName);

                conn.Close();
            }

            return dt;
        }

        public bool TestConnection()
        {
            try
            {
                bool hasConnection = false;

                using (DbConnection conn = CreateConnection())
                {
                    conn.Open();

                    hasConnection = (conn.State & ConnectionState.Open) > 0;

                    conn.Close();
                }

                return hasConnection;
            }
            catch (Exception) { return false; }
        }

        public DbConnectionConfig DbConnectionConfig
        {
            get { return dbConnectionConfig; }
        }

        #endregion


    } // end of XDatabase
}
