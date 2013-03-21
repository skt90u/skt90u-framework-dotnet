using System.Collections.Generic;
using System.Data;
using System.Collections;

namespace JUtil
{
    #region IDbBasicOp
    /// <summary>
    /// 提供資料庫基本操作
    /// (新增，移除，修改，查詢)
    /// </summary>
    /// <remarks>
    /// 未來不會，也不能再增加任何功能，以防止公司專案要針對此介面做修改
    /// </remarks>
    public interface IDbBasicOp
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
    }
    #endregion
}
