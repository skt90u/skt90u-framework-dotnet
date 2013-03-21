using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace JFramework
{
    /// <summary>
    /// 在第一次呼叫SelectSQL or ExecuteSQL 才會建立一個 Connection，並且在之後呼叫 SelectSQL or ExecuteSQL
    /// 都是使用相同Connection，只有在物件的Finalize() or Dispose() 才會關閉 Connection
    /// </summary>
    public class Database : IDatabase, IDisposable
    {
        public Database(DatabaseMode databaseMode, string connectionString)
        {
            this.databaseMode = databaseMode;
            this.connectionString = connectionString;
        }

        ~Database()
        {
            Dispose(false);
        }

        #region FieldsPrivate
        private readonly DatabaseMode databaseMode;
        private readonly string connectionString;
        #region DbProviderFactory
        private DbProviderFactory factory = null;
        private DbProviderFactory Factory
        {
            get
            {
                if (factory == null)
                {
                    foreach (DbInfo dbInfo in DbInfo.arrDbInfo)
                    {
                        if (dbInfo.databaseMode == databaseMode)
                        {
                            factory = DbProviderFactories.GetFactory(dbInfo.provider);
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

        #region IDatabase
        public DataTable SelectSQL(string querySQL)
        {
            DataTable dt = null;
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    command.CommandText = querySQL;

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

        public DataSet SelectSQL(List<KeyValuePair<string, string>> querySQLs)
        {
            DataSet ds = null;

            try
            {
                ds = new DataSet();

                foreach (KeyValuePair<string, string> querySQL in querySQLs)
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

        public int SelectRecordCountSQL(string querySQL)
        {
            int count = 0;
            try
            {
                string aliasField = "RECORDCOUNT";
                // sql server :
                // http://tw.myblog.yahoo.com/franklkjii/article?mid=437
                string aliasTable = "RECORDCOUNT_TABLE";
                string recordcountSQL = string.Format("SELECT COUNT(*) AS {0} FROM ({1}) {2}", aliasField, querySQL, aliasTable);
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

        public void ExecuteSQL(ICollection<string> SQLs)
        {
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    using (DbTransaction transaction = CreateTransaction(command))
                    {
                        try
                        {
                            IEnumerator<string> enumerator = SQLs.GetEnumerator();
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

        public void ExecuteSQL(string sql)
        {
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    command.CommandText = sql;
                    command.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
        }

        public DataTable GetSchemaTable(string querySQL)
        {
            DataTable dt = null;
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    command.CommandText = querySQL;
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

        public DbDataReader GetDataReader(string querySQL)
        {
            DbDataReader reader = null;
            try
            {
                using (DbCommand command = CreateDbCommand())
                {
                    command.CommandText = querySQL;
                    reader = command.ExecuteReader();
                }
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
            return reader;
        }

        public int ConnectionCount()
        {
            try
            {
                // so far, only sql server can be supported 
                string sql = string.Format("select * from master.dbo.sysprocesses p join master.dbo.sysdatabases d on p.dbID = d.dbID where d.name = '{0}'", Connection.Database);
                return SelectRecordCountSQL(sql);
            }
            catch (Exception)
            {
                //Dispose(true);
                throw;
            }
        }
        #endregion
    }
}
