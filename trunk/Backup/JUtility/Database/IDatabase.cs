using System.Data;
using System.Collections.Generic;
using System.Data.Common;

namespace JFramework
{
    interface IDatabase
    {
        DataTable SelectSQL(string querySQL);
        DataSet SelectSQL(List<KeyValuePair<string, string>> querySQLs);
        int SelectRecordCountSQL(string querySQL);
        void ExecuteSQL(ICollection<string> SQLs);
        void ExecuteSQL(string sql);
        DataTable GetSchemaTable(string querySQL);
        DbDataReader GetDataReader(string querySQL);
        int ConnectionCount();
    }
}
