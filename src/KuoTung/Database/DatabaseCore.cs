using System.Data;
using System.Data.Common;

namespace KuoTung
{
    internal class DatabaseCore
    {
        public DatabaseCore(DbProvider dbProvider, string connectionString)
        {
            DbProvider = dbProvider;
            ConnectionString = connectionString;
        }

        ~DatabaseCore()
        {
            if (dbConnection != null && dbConnection.State == ConnectionState.Open)
                dbConnection.Close();
        }

        internal readonly DbProvider DbProvider;
        internal readonly string ConnectionString;

        #region DbProviderFactory
        private DbProviderFactory dbProviderFactory = null;
        internal DbProviderFactory DbProviderFactory
        {
            get
            {
                if (dbProviderFactory == null)
                {
                    string providerName = DbProviderAssemblies.GetAssemblyName(DbProvider);

                    dbProviderFactory = DbProviderFactories.GetFactory(providerName);
                }

                return dbProviderFactory;
            }
        }
        #endregion
        #region DbConnection
        private DbConnection dbConnection = null;
        internal DbConnection DbConnection
        {
            get
            {
                if (dbConnection == null)
                {
                    dbConnection = DbProviderFactory.CreateConnection();
                    dbConnection.ConnectionString = ConnectionString;
                }

                if (dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                return dbConnection;
            }
        }
        #endregion
        #region CreateDbCommand
        internal DbCommand CreateDbCommand()
        {
            DbCommand dbCommand = DbProviderFactory.CreateCommand();
            dbCommand.Connection = DbConnection;
            return dbCommand;
        }
        #endregion
        #region CreateDataAdapter
        internal DbDataAdapter CreateDataAdapter()
        {
            return DbProviderFactory.CreateDataAdapter();
        }
        #endregion
        #region CreateTransaction
        internal DbTransaction CreateTransaction(DbCommand dbCommand)
        {
            DbTransaction dbTransaction = DbConnection.BeginTransaction();
            dbCommand.Transaction = dbTransaction;
            return dbTransaction;
        }
        #endregion
    }
}
