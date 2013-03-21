using  MySql.Data.MySqlClient;
using  System.Data.SqlClient;
using  System.Data.OracleClient;
using  Teradata.Client.Provider;
using  System.Data.SQLite;
using  System.Data.OleDb;

namespace JFramework
{
    internal class DbInfo
    {
        public readonly DatabaseMode databaseMode;
        public readonly string provider;

        private DbInfo(DatabaseMode databaseMode, string provider)
        {
            this.databaseMode = databaseMode;
            this.provider = provider;
        }

        public static readonly DbInfo[] arrDbInfo = new DbInfo[] {
	            new DbInfo(DatabaseMode.MySql, "MySql.Data.MySqlClient"),
	            new DbInfo(DatabaseMode.SqlServer, "System.Data.SqlClient"),
	            new DbInfo(DatabaseMode.Oracle, "System.Data.OracleClient"),
	            new DbInfo(DatabaseMode.Teradata, "Teradata.Client.Provider"),
	            new DbInfo(DatabaseMode.Sqlite, "System.Data.SQLite"),
	            new DbInfo(DatabaseMode.OleDb, "System.Data.OleDb")
        };
    }
}
