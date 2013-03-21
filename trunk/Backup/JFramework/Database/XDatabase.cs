using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace JFramework
{
    /// <summary>
    /// 每次執行一個SelectSQL or ExecuteSQL 才會建立一個 Connection，並且在函式結束後關閉 Connection
    /// </summary>
    public class XDatabase : IDatabase
    {
        public XDatabase(DatabaseMode databaseMode, string connectionString)
        {
            this.databaseMode = databaseMode;
            this.connectionString = connectionString;
        }

        #region FieldsPrivate
        private readonly DatabaseMode databaseMode;
        private readonly string connectionString;
        #endregion

        #region MethodsPrivate

        private DbProviderFactory GetDbProviderFactory()
        {
            foreach (DbInfo dbInfo in DbInfo.arrDbInfo)
            {
                if (dbInfo.databaseMode == databaseMode)
                {
                    return DbProviderFactories.GetFactory(dbInfo.provider);
                }
            }

            throw new Exception("specify DatabaseMode can not be supported");
        }

        private DbConnection CreateDbConnection()
        {
            DbProviderFactory factory = GetDbProviderFactory();
            DbConnection conn = factory.CreateConnection();
            conn.ConnectionString = connectionString;
            return conn;
        }


        private DbDataAdapter CreateDataAdapter()
        {
            DbProviderFactory factory = GetDbProviderFactory();
            DbDataAdapter adapter = factory.CreateDataAdapter();
            return adapter;
        }

        private DbCommand CreateDbCommand(DbConnection conn)
        {
            DbProviderFactory factory = GetDbProviderFactory();
            DbCommand command = factory.CreateCommand();

            if (conn.State == ConnectionState.Closed)
                conn.Open();

            command.Connection = conn;

            return command;
        }

        private DbTransaction CreateTransaction(DbConnection conn, DbCommand command)
        {
            DbTransaction transaction = conn.BeginTransaction();
            command.Transaction = transaction;
            return transaction;
        }
        #endregion

        #region IDatabase 成員

        public DataTable SelectSQL(string querySQL)
        {
            DataTable dt = null;
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
            return dt;
        }

        public DataSet SelectSQL(List<KeyValuePair<string, string>> querySQLs)
        {
            DataSet ds = null;
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
            return ds;
        }

        public int SelectRecordCountSQL(string querySQL)
        {
            int count = 0;
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
            return count;
        }

        public void ExecuteSQL(ICollection<string> SQLs)
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
            } //using (DbConnection conn = CreateDbConnection())
        }

        public void ExecuteSQL(string sql)
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

        public DataTable GetSchemaTable(string querySQL)
        {
            DataTable dt = null;
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
            return dt;
        }

        public DbDataReader GetDataReader(string querySQL)
        {
            DbDataReader reader = null;
            using (DbConnection conn = CreateDbConnection())
            {
                using (DbCommand command = CreateDbCommand(conn))
                {
                    command.CommandText = querySQL;

                    reader = command.ExecuteReader();
                }
            }
            return reader;
        }

        public int ConnectionCount()
        {
            using (DbConnection Connection = CreateDbConnection())
            {
                // so far, only sql server can be supported 
                string sql = string.Format("select * from master.dbo.sysprocesses p join master.dbo.sysdatabases d on p.dbID = d.dbID where d.name = '{0}'", Connection.Database);
                return SelectRecordCountSQL(sql);    
            }
        }
        #endregion
        
    }
}
