using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace JUtil
{
    public interface IDatabase
    {
        /// <summary>
        /// 一次查詢單一資料表
        /// </summary>
        DataTable SelectSQL(string sql);

        /// <summary>
        /// 一次查詢多筆資料表
        /// </summary>
        DataSet SelectSQL(List<KeyValuePair<string, string>> sqls);

        /// <summary>
        /// 查詢資料筆數
        /// </summary>
        int SelectCountSQL(string sql);

        /// <summary>
        /// 一次執行單一資料庫異動操作(新增，移除，更新資料)
        /// (若失敗，則Rollback.)
        /// </summary>
        void ExecuteSQL(string sql);

        /// <summary>
        /// 一次執行多個資料庫異動操作(新增，移除，更新資料)
        /// (若失敗，則Rollback.)
        /// </summary>
        void ExecuteSQL(ICollection<string> sqls);

        /// <summary>
        /// 一次執行單一資料庫異動操作(新增，移除，更新資料)
        /// (若失敗，則Rollback.)
        /// 當使用 ExecuteSQL(sql), e.g. insert sql 字串長度過常會出錯
        /// 可嘗試使用此function
        /// </summary>
        void ExecuteSQL(string sql, Hashtable inParameters);

        /// <summary>
        /// 執行 store procedure 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="inParameters"></param>
        /// <param name="outParameters"></param>
        /// <param name="retParameters"></param>
        void ExecuteSP(string procedureName, Hashtable inParameters, Hashtable outParameters, Hashtable retParameters);

        /// <summary>
        /// 取得所查詢的資料表的Schema
        /// </summary>
        /// <remarks>
        /// CodeGenerator用於產生相關參數
        /// </remarks>
        DataTable GetTableSchema(string sql);

        /// <summary>
        /// 取得資料庫相關的結構(Databases，Tables，Views)
        /// </summary>
        /// <remarks>
        /// SqlParser專用
        /// </remarks>
        DataTable GetSchema(string collectionName);

        /// <summary>
        /// 測試是否可連線
        /// </summary>
        bool TestConnection();

        DbConnectionConfig DbConnectionConfig { get; }
    }
}
