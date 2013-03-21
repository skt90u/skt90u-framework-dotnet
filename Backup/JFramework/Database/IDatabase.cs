using System.Data;
using System.Data.Common;
using System.Collections.Generic;

namespace JFramework
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
    }
}
