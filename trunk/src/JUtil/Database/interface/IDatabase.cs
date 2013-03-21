using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JUtil
{
#if false
    /// <summary>
    /// 提供資料庫基本操作(原為IDatabase)
    /// </summary>
    public interface IDatabase
    {
        /// <summary>
        /// select data
        /// </summary>
        /// <param name="querySQL"></param>
        /// <returns></returns>
        DataTable SelectSQL(string querySQL);

        /// <summary>
        /// select multi-data
        /// </summary>
        /// <param name="querySQLs"></param>
        /// <returns></returns>
        DataSet SelectSQL(List<KeyValuePair<string, string>> querySQLs);

        /// <summary>
        /// count the selected data
        /// </summary>
        /// <param name="querySQL"></param>
        /// <returns></returns>
        int SelectRecordCountSQL(string querySQL);

        /// <summary>
        /// insert, update, delete data
        /// </summary>
        /// <param name="SQLs"></param>
        void ExecuteSQL(ICollection<string> SQLs);

        /// <summary>
        /// insert, update, delete data
        /// </summary>
        /// <param name="sql"></param>
        void ExecuteSQL(string sql);

        /// <summary>
        /// get schema of selected data
        /// </summary>
        /// <param name="querySQL"></param>
        /// <returns></returns>
        DataTable GetTableSchema(string querySQL);

        /// <summary>
        /// get DataReader of selected data
        /// </summary>
        /// <param name="querySQL"></param>
        /// <returns></returns>
        DbDataReader GetDataReader(string querySQL);

        /// <summary>
        /// count current database connections
        /// </summary>
        /// <returns></returns>
        int ConnectionCount();

        /// <summary>
        /// test connection
        /// </summary>
        /// <returns></returns>
        bool TestConnection();

        /// <summary>
        /// GetSchema
        /// </summary>
        /// <returns></returns>
        DataTable GetSchema(string collectionName);
    } // end of AIDatabase
#endif
}
