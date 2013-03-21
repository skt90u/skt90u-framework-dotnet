using System.Data;
using System.Data.Common;
using System.Collections.Specialized;
using System;
using System.Diagnostics;
using System.Collections.Generic;

using  MySql.Data.MySqlClient;
using  System.Data.SqlClient;
using  System.Data.OracleClient;
using  Teradata.Client.Provider;
using  System.Data.SQLite;
using  System.Data.OleDb;

/*
namespace JFramework.Database
{
    public class XDatabase : IDatabase
    {
        #region Support DatabaseMode
        public enum DatabaseMode
        {
            MySql = 0,
            SqlServer = 1,
            Oracle = 2,
            Teradata = 3,
            Sqlite = 4,
            OleDb = 5
        }
        #endregion

        #region SelectSQL(string querySQL)
        /// <summary>
        /// Execute [Select] SQL
        /// </summary>
        /// <param name="querySQL"></param>
        /// <returns>
        /// code snippet : jselect
        /// string querySQL = $selected$
        /// using(DataTable dt = XDatabaseObj.SelectSQL(querySQL)){
        ///     foreach(DataRow row in dt.Rows){
        ///     $end$
        ///     }
        /// }
        /// </returns>
        public DataTable SelectSQL(string querySQL)
        {
            DataTable dt = null;
            try
            {
                using (DbConnection conn = CreateDbConnection())
                {
                    using (DbCommand command = CreateDbCommand(conn))
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        #endregion
        #region SelectSQL(List<KeyValuePair<string, string> > querySQLs)
        /// <summary>
        /// Execute [Select] SQL
        /// </summary>
        /// <param name="querySQL"></param>
        /// <returns>
        /// using(DataSet ds = XDatabaseObj.SelectSQL(querySQLs)){
        ///  ...
        /// }
        /// </returns>
        public DataSet SelectSQL(List<KeyValuePair<string, string> > querySQLs)
        {
            DataSet ds = null;

            try
            {
                using (DbConnection conn = CreateDbConnection())
                {
                    ds = new DataSet();

                    foreach (KeyValuePair<string, string> querySQL in querySQLs)
                    {
                       using (DbCommand command = CreateDbCommand(conn))
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
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }
        #endregion
        #region SelectRecordCountSQL(string querySQL)
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
            catch (Exception ex)
            {
                throw ex;
            }
            return count;
        }
        #endregion
        #region ExecuteSQL(ICollection<string> SQLs)
        public void ExecuteSQL(ICollection<string> SQLs)
        {
            try
            {
                using (DbConnection conn = CreateDbConnection())
                {
                    using (DbCommand command = CreateDbCommand(conn))
                    {
                        using (DbTransaction transaction = CreateTransaction(conn, command))
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
                            catch (Exception exExecuteSQL)
                            {
                                try
                                {
                                    transaction.Rollback();
                                }
                                catch (Exception exRollback)
                                {
                                    throw exRollback;
                                }
                                throw exExecuteSQL;
                            }
                        } //using (DbTransaction transaction = CreateTransaction(conn, command))
                    } //using (DbCommand command = CreateDbCommand(conn))
                } //using (DbConnection conn = CreateDbConnection())
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region ExecuteSQL(string sql)
        public void ExecuteSQL(string sql)
        {
            try
            {
                using (DbConnection conn = CreateDbConnection())
                {
                    using (DbCommand command = CreateDbCommand(conn))
                    {
                        command.CommandText = sql;

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region GetSchemaTable(string querySQL)
        public DataTable GetSchemaTable(string querySQL)
        {
            DataTable dt = null;
            try
            {
                using (DbConnection conn = CreateDbConnection())
                {
                    using (DbCommand command = CreateDbCommand(conn))
                    {
                        command.CommandText = querySQL;
                        using (DbDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                        {
                            dt = reader.GetSchemaTable();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        #endregion
        #region GetDataReader(string querySQL)
        [Obsolete("沒測試過，我想應該不行work, 因為DbConnection回傳之後就關閉了")]
        public DbDataReader GetDataReader(string querySQL)
        {
            DbDataReader reader = null;
            try
            {
                using (DbConnection conn = CreateDbConnection())
                {
                    using (DbCommand command = CreateDbCommand(conn))
                    {
                        command.CommandText = querySQL;

                        reader = command.ExecuteReader();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return reader;
        }
        #endregion

        #region support database information
        class DbInfo
        {
            public readonly DatabaseMode databaseMode;
            public readonly string provider;

            public DbInfo(DatabaseMode databaseMode, string provider)
            {
                this.databaseMode = databaseMode;
                this.provider = provider;
            }
        }

        private static readonly DbInfo[] arrDbInfo = new DbInfo[] {
	            new DbInfo(DatabaseMode.MySql, "MySql.Data.MySqlClient"),
	            new DbInfo(DatabaseMode.SqlServer, "System.Data.SqlClient"),
	            new DbInfo(DatabaseMode.Oracle, "System.Data.OracleClient"),
	            new DbInfo(DatabaseMode.Teradata, "Teradata.Client.Provider"),
	            new DbInfo(DatabaseMode.Sqlite, "System.Data.SQLite"),
	            new DbInfo(DatabaseMode.OleDb, "System.Data.OleDb")
        };
        #endregion

        private readonly DatabaseMode databaseMode;
        private readonly string connectionString;

        public XDatabase(DatabaseMode databaseMode, string connectionString)
        {
            this.databaseMode = databaseMode;
            this.connectionString = connectionString;
        }

        private DbProviderFactory GetDbProviderFactory()
        {
            try
            {
                string provider = string.Empty;
                foreach (DbInfo dbInfo in arrDbInfo)
                {
                    if (dbInfo.databaseMode == databaseMode)
                    {
                        provider = dbInfo.provider;
                        break;
                    }
                }
                Debug.Assert(!string.IsNullOrEmpty(provider));

                return DbProviderFactories.GetFactory(provider);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DbConnection CreateDbConnection()
        {
            try
            {
                DbProviderFactory factory = GetDbProviderFactory();
                DbConnection conn = factory.CreateConnection();
                conn.ConnectionString = connectionString;
                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DbDataAdapter CreateDataAdapter()
        {
            try
            {
                DbProviderFactory factory = GetDbProviderFactory();
                DbDataAdapter adapter = factory.CreateDataAdapter();
                return adapter;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DbCommand CreateDbCommand(DbConnection conn)
        {
            try
            {
                DbProviderFactory factory = GetDbProviderFactory();
                DbCommand command = factory.CreateCommand();

                if (conn.State == ConnectionState.Closed)
                    conn.Open();

                command.Connection = conn;

                return command;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DbTransaction CreateTransaction(DbConnection conn, DbCommand command)
        {
            try
            {
                DbTransaction transaction = conn.BeginTransaction();
                command.Transaction = transaction;
                return transaction;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
*/