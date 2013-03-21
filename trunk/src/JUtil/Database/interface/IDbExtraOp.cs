using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace JUtil
{
    #region IDbExtraOp
    /// <summary>
    /// 提供資料庫額外操作
    /// </summary>
    /// <remarks>
    /// 目前的想法是此一介面不會對外公開
    /// (至少在公司專案中避免使用, 否則當專案中繼承此介面，未來擴充此功能會影響到專案的向下相容性)
    /// 
    /// 未來新增功能切記，不可把DbConnection往外部傳，因為資源管理不應再由外部控制。
    /// </remarks>
    public interface IDbExtraOp
    {
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

        DbProvider DbProvider{get;}
        string ConnectionString { get; }
    }
    #endregion
}
