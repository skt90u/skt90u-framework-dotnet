using JUtil.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace JUtil
{
    /// <remarks>
    /// lazy initialize DbConnection when using it at first time called by SelectSQL or ExecuteSQL,
    /// and close DbConnection in Finalize() or Dispose()
    /// </remarks>
    internal class Database : IDisposable, IDatabase
    {
        public Database(DbConnectionConfig dbConnectionConfig)
        {
            this.dbConnectionConfig = dbConnectionConfig;

            dbProvider = dbConnectionConfig.DbProvider;
            connectionString = dbConnectionConfig.ConnectionString;
        }

        /// <summary>
        /// dtor
        /// </summary>
        ~Database()
        {
            Dispose(false);
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

        #region DbConnection
        private DbConnection connection = null;
        private DbConnection Connection
        {
            get
            {
                if (connection == null)
                {
                    connection = Factory.CreateConnection();
                    connection.ConnectionString = connectionString;
                    disposed = false;
                }

                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                return connection;
            }
        }
        #endregion

        #endregion

        #region MethodsPrivate

        private DbCommand CreateDbCommand()
        {
            DbCommand command = Factory.CreateCommand();
            command.Connection = Connection;
            return command;
        }

        private DbDataAdapter CreateDataAdapter()
        {
            return Factory.CreateDataAdapter();
        }

        private DbTransaction CreateTransaction(DbCommand command)
        {
            DbTransaction transaction = connection.BeginTransaction();
            command.Transaction = transaction;
            return transaction;
        }

        #endregion

        #region IDisposable
        // reference : http://stackoverflow.com/questions/628752/why-call-disposefalse-in-the-destructor
       
        private bool disposed = false;
        
        private void Dispose(bool disposing)
        {
            // ------------------------------------------------------------
            // Dispose(bool disposing) executes in two distinct scenarios.
            // If disposing equals true, the method has been called directly
            // or indirectly by a user's code. Managed and unmanaged resources
            // can be disposed.
            // If disposing equals false, the method has been called by the
            // runtime from inside the finalizer and you should not reference
            // other objects. Only unmanaged resources can be disposed.
            // ------------------------------------------------------------

            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources. (like component.Dispose())
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                
                //if (connection != null && connection.State == ConnectionState.Open)
                //{
                //    connection.Close();
                //    connection = null;
                //}

                // Note disposing has been done.
                disposed = true;

            }
        }

        /// <summary>
        /// cleaned-up function
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
        #endregion

        #region IDatabase 成員

        public DataTable SelectSQL(string sql)
        {
            DataTable dt = null;
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    command.CommandText = sql;

                    using (DbDataAdapter adapter = CreateDataAdapter())
                    {
                        // http://msdn.microsoft.com/zh-tw/library/fks3666w.aspx
                        adapter.SelectCommand = command;

                        dt = new DataTable();
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
            return dt;
        }

        public DataSet SelectSQL(List<KeyValuePair<string, string>> sqls)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();

                foreach (KeyValuePair<string, string> querySQL in sqls)
                {
                    using (DbCommand command = CreateDbCommand())
                    {
                        command.CommandText = querySQL.Value;

                        using (DbDataAdapter adapter = CreateDataAdapter())
                        {
                            // http://msdn.microsoft.com/zh-tw/library/fks3666w.aspx
                            adapter.SelectCommand = command;

                            adapter.Fill(ds, querySQL.Key);
                        }
                    }
                }
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
            return ds;
        }

        public int SelectCountSQL(string sql)
        {
            int count = 0;
            try
            {
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
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
            return count;
        }

        public void ExecuteSQL(string sql)
        {
            ExecuteSQL(new List<string>{sql});
        }

        public void ExecuteSQL(ICollection<string> sqls)
        {
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    using (DbTransaction transaction = CreateTransaction(command))
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
                            throw ;
                        }
                    } //using (DbTransaction transaction = CreateTransaction(conn, command))
                } //using (DbCommand command = CreateDbCommand(conn))
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
        }

        private DbParameter CreateParameter(ParameterDirection direction, string key, object value)
        {
            DbParameter parameter = Factory.CreateParameter();
            parameter.Direction = direction;
            parameter.ParameterName = key;
            parameter.DbType = value.GetType().ToDbType();
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
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    using (DbTransaction transaction = CreateTransaction(command))
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
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
        }

        public void ExecuteSQL(string sql, Hashtable inParameters)
        {
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    using (DbTransaction transaction = CreateTransaction(command))
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
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
        }

        public DataTable GetTableSchema(string sql)
        {
            //
            // reference : http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqldatareader.GetTableSchema.aspx
            //
            DataTable dt = null;
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    command.CommandText = sql;
                    using (DbDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                    {
                        dt = reader.GetSchemaTable();
                    }
                }
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
            return dt;
        }

        public DataTable GetSchema(string collectionName)
        {
            DataTable dt = null;
            try
            {
                DbConnection conn = Connection;

                dt = string.IsNullOrEmpty(collectionName) ? conn.GetSchema() : conn.GetSchema(collectionName);
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
            return dt;
        }

        public bool TestConnection()
        {
            try
            {
                DbConnection conn = Connection;

                return (conn.State & ConnectionState.Open) > 0;
            }
            catch (Exception) { return false; }
        }

        public DbConnectionConfig DbConnectionConfig
        {
            get { return this.DbConnectionConfig; }
        }

        #endregion


    } // end of Database
}
