using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OracleClient;
using JUtil;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace JUtil
{
    public class ConnectionStringParser
    {
        public ConnectionStringParser(DbConnectionConfig dbConnectionConfig)
        {
            DbProvider dbProvider = dbConnectionConfig.DbProvider;
            string connectionString = dbConnectionConfig.ConnectionString;

            switch (dbProvider)
            {
                case DbProvider.MySql: { throw new NotImplementedException(); } 
                case DbProvider.Teradata: { throw new NotImplementedException(); } 
                case DbProvider.Sqlite: { throw new NotImplementedException(); } 

                case DbProvider.SqlServer:
                    {
                        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(connectionString);

                        DataSource = builder.DataSource;
                        UserID = builder.UserID;
                        Password = builder.Password;
                    } break;

                case DbProvider.Oracle:
                    {
                        OracleConnectionStringBuilder builder = new OracleConnectionStringBuilder(connectionString);

                        DataSource = builder.DataSource;
                        UserID = builder.UserID;
                        Password = builder.Password;
                    } break;

                case DbProvider.OleDb:
                    {
                        OleDbConnectionStringBuilder builder = new OleDbConnectionStringBuilder(connectionString);
                        DataSource = builder.DataSource;
                        UserID = builder["UserID"].ToString();
                        Password = builder["Password"].ToString();
                    } break;
            }
        }

        public readonly string DataSource;
        public readonly string UserID;
        public readonly string Password;
    }
}
