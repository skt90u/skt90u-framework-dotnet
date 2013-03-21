using System.Data;

namespace JUtil
{
    public class TableSchema
    {
        public static TableSchema GetBySql(IDatabase db, string sql)
        {
            DataTable dt = db.GetTableSchema(sql);

            return new TableSchema(dt);
        }

        public static TableSchema GetByTable(IDatabase db, string table)
        {
            string sql = string.Format("select * from {0}", table);
            
            return GetBySql(db, sql);
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
