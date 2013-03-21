using System;
using System.Collections.Generic;

namespace KuoTung
{
    internal static class DbProviderAssemblies
    {
        private static Dictionary<DbProvider, string> _map = new Dictionary<DbProvider, string>
        {
            {DbProvider.MySql, "MySql.Data.MySqlClient"},
            {DbProvider.SqlServer, "System.Data.SqlClient"},
            {DbProvider.Oracle, "System.Data.OracleClient"},
            {DbProvider.Teradata, "Teradata.Client.Provider"},
            {DbProvider.Sqlite, "System.Data.SQLite"},
            {DbProvider.OleDb, "System.Data.OleDb"},
        };

        internal static string GetAssemblyName(DbProvider dbProvider)
        {
            if (!_map.ContainsKey(dbProvider))
                throw new ArgumentException(string.Format("specify dbProvider({0}) can not be found relatived assembly", dbProvider.ToString()));
            
            return _map[dbProvider];
        }
    } 
}
