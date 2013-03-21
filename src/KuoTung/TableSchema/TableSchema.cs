using System.Data;
using System.Data.Common;

namespace KuoTung
{
    public class TableSchema
    {
        public static TableSchema Create(DbProvider dbProvider, string connectionString, string sql)
        {
            DatabaseCore databaseCore = new DatabaseCore(dbProvider, connectionString);

            DataTable dt = GetTableSchema(databaseCore, sql);

            return new TableSchema(dt);
        }

        private static DataTable GetTableSchema(DatabaseCore databaseCore, string sql)
        {
            DataTable dt;

            //
            // reference : http://msdn.microsoft.com/en-us/library/system.data.sqlclient.sqldatareader.GetTableSchema.aspx
            //
            using (DbCommand command = databaseCore.CreateDbCommand())
            {
                command.CommandText = sql;
                using (DbDataReader reader = command.ExecuteReader(CommandBehavior.KeyInfo))
                {
                    dt = reader.GetSchemaTable();
                }
            }

            return dt;
        }

        private TableSchema(DataTable dt)
        {
            int count = dt.Rows.Count;

            Fields = new FieldSchema[count];

            for (int i = 0; i < count; i++)
                Fields[i] = new FieldSchema(dt.Rows[i]);
        }

        public readonly FieldSchema[] Fields;
        
    }
}
