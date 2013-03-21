using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JUtil
{
    public interface IDatabase
    {
        DataTable SelectSQL(string querySQL);

        DataSet SelectSQL(List<KeyValuePair<string, string>> querySQLs);

        int SelectRecordCountSQL(string querySQL);

        void ExecuteSQL(ICollection<string> SQLs);

        void ExecuteSQL(string sql);

        DataTable GetSchemaTable(string querySQL);

        DbDataReader GetDataReader(string querySQL);

        int ConnectionCount();


    } // end of IDatabase
}
