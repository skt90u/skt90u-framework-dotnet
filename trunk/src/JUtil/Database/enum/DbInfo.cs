namespace JUtil
{
    internal class DbInfo
    {
        public readonly DbProvider DatabaseMode;
        public readonly string Provider;

        private DbInfo(DbProvider databaseMode, string provider)
        {
            this.DatabaseMode = databaseMode;
            this.Provider = provider;
        }

        public static readonly DbInfo[] ArrDbInfo = new DbInfo[] {
	            new DbInfo(DbProvider.MySql, "MySql.Data.MySqlClient"),
	            new DbInfo(DbProvider.SqlServer, "System.Data.SqlClient"),
	            new DbInfo(DbProvider.Oracle, "System.Data.OracleClient"),
	            new DbInfo(DbProvider.Teradata, "Teradata.Client.Provider"),
	            new DbInfo(DbProvider.Sqlite, "System.Data.SQLite"),
	            new DbInfo(DbProvider.OleDb, "System.Data.OleDb")
        };


    } // end of DbInfo
}
