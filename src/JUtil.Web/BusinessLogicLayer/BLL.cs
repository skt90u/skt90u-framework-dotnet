using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Linq;
using System;
using JUtil.Extensions;

namespace JUtil.Web
{
    /// <summary>
    /// 建立Business Logic Layer基本操作，
    /// 只需override以下函式
    ///   - CreateDbObject : 建立資料庫操作物件
    ///   - GetOrderBy
    ///   - GetSelectSQLNoOrderBy
    ///   - GetSelectDetailsVwSQL
    ///   - GetInsertSQL
    ///   - GetUpdateSQL
    ///   - GetDeleteSQL
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [DataObject(true)]
    public abstract class BLL<T> : IDatabase
        where T : class
    {
        #region IDatabase object
        private IDatabase _db;
        private IDatabase db
        {
            get
            {
                _db = _db ?? CreateDbObject();
                return _db;
            }
        }
        #endregion

        #region SelectParameters object
        /// <summary>
        /// 查詢條件所需要的參數
        /// </summary>
        public Hashtable SelectParameters
        {
            get
            {
                if (_SelectParameters == null)
                {
                    _SelectParameters = new Hashtable();
                }
                return _SelectParameters;
            }
        }
        private Hashtable _SelectParameters;
        #endregion

        #region OldRec
        /// <summary>
        /// OldValues
        /// </summary>
        public T OldRec
        {
            get
            {
                if (_OldRec == null)
                {
                    throw new Exception("必須先執行SelectDetail操作才能取得OldRec");
                }
                return _OldRec;
            }
        }
        private T _OldRec;
        #endregion

        #region OldValues object
        /// <summary>
        /// OldValues
        /// </summary>
        public Hashtable OldValues
        {
            get
            {
                if (_OldValues == null)
                {
                    _OldValues = new Hashtable();
                }
                return _OldValues;
            }
        }
        private Hashtable _OldValues;
        #endregion

        #region IDatabase 成員

        public void ExecuteSQL(ICollection<string> sqls)
        {
            db.ExecuteSQL(sqls);
        }

        public void ExecuteSQL(string sql)
        {
            db.ExecuteSQL(sql);
        }

        public int SelectCountSQL(string sql)
        {
            return db.SelectCountSQL(sql);
        }

        public DataSet SelectSQL(List<KeyValuePair<string, string>> sqls)
        {
            return db.SelectSQL(sqls);
        }

        public DataTable SelectSQL(string sql)
        {
            return db.SelectSQL(sql);
        }

        public void ExecuteSP(string procedureName, Hashtable inParameters, Hashtable outParameters, Hashtable retParameters)
        {
            db.ExecuteSP(procedureName, inParameters, outParameters, retParameters);
        }

        public void ExecuteSQL(string sql, Hashtable inParameters)
        {
            db.ExecuteSQL(sql, inParameters);
        }

        public DbConnectionConfig DbConnectionConfig
        {
            get { return db.DbConnectionConfig; }
        }

        public DataTable GetSchema(string collectionName)
        {
            return db.GetSchema(collectionName);
        }

        public DataTable GetTableSchema(string sql)
        {
            return db.GetTableSchema(sql);
        }

        public bool TestConnection()
        {
            return db.TestConnection();
        }

        #endregion

        #region Abstract members

        /// <summary>
        /// CreateDbObject
        /// </summary>
        protected abstract IDatabase CreateDbObject();

        /// <summary>
        /// GetOrderBy
        /// </summary>
        protected abstract string GetOrderBy();

        /// <summary>
        /// GetSelectSQLNoOrderBy
        /// </summary>
        protected abstract string GetSelectSQLNoOrderBy(Hashtable SelectParameters);

        /// <summary>
        /// GetSelectSQL
        /// </summary>
        protected string GetSelectSQL(Hashtable SelectParameters)
        {
            string sql = GetSelectSQLNoOrderBy(SelectParameters);

            string orderBy = GetOrderBy();

            sql = string.Format("{0} {1}", sql, orderBy);

            return sql;
        }

        /// <summary>
        /// GetSelectDetailsVwSQL
        /// </summary>
        protected abstract string GetSelectDetailsVwSQL(Hashtable SelectParameters);

        /// <summary>
        /// GetInsertSQL
        /// </summary>
        public abstract List<string> GetInsertSQL(T aRec);

        /// <summary>
        /// GetUpdateSQL
        /// </summary>
        public abstract List<string> GetUpdateSQL(T aRec);

        /// <summary>
        /// GetDeleteSQL
        /// </summary>
        public abstract List<string> GetDeleteSQL(T aRec);

        #endregion

        #region DataObject Operations

        #region SelectPaging
        /// <summary>
        /// SelectPaging
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public DataTable SelectPaging(int startRowIndex, int maximumRows)
        {
            if (SelectParameters.Count == 0)
                return null;

            string sql = GetSelectSQL(SelectParameters);

            DataTable dt = SelectSQL(sql);

            if (dt.Rows.Count == 0)
                return null;

            var pagedData = dt.AsEnumerable().Skip(startRowIndex).Take(maximumRows);

            DataTable ret = pagedData.CopyToDataTable();

            return ret; 
        }
        #endregion

        #region SelectNonPaging
        /// <summary>
        /// SelectNonPaging
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable SelectNonPaging()
        {
            if (SelectParameters.Count == 0)
                return null;

            DataTable dt = SelectSQL(GetSelectSQL(SelectParameters));

            if (dt.Rows.Count == 0)
                return null;

            return dt;
        }
        #endregion

        #region SelectDetailsVw
        /// <summary>
        /// SelectDetailsVw
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public DataTable SelectDetailsVw()
        {
            if (SelectParameters.Count == 0)
                return null;

            DataTable dt = SelectSQL(GetSelectDetailsVwSQL(SelectParameters));


            // --------------------------------------------------
            // 設定 OldRec
            // --------------------------------------------------
            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];

                _OldRec = row.ConvertTo<T>();
            }

            // --------------------------------------------------
            // 設定 OldValues (未來要移除，使用OldRec取代)
            // --------------------------------------------------
            OldValues.Clear();

            if (dt.Rows.Count == 1)
            {
                DataRow row = dt.Rows[0];
                foreach (DataColumn col in dt.Columns)
                {
                    OldValues[col.ColumnName] = row[col.ColumnName];
                }
            }

            return dt;
        }
        #endregion

        #region Insert
        /// <summary>
        /// Insert
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Insert, false)]
        public void Insert(T aRec)
        {
            ExecuteSQL(GetInsertSQL(aRec));
        }
        #endregion

        #region Update
        /// <summary>
        /// Update
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Update, false)]
        public void Update(T aRec)
        {
            ExecuteSQL(GetUpdateSQL(aRec));
        }
        #endregion

        #region Delete
        /// <summary>
        /// Delete
        /// </summary>
        [DataObjectMethod(DataObjectMethodType.Delete, false)]
        public void Delete(T aRec)
        {
            ExecuteSQL(GetDeleteSQL(aRec));
        }
        #endregion

        #region SelectCount
        /// <summary>
        /// SelectCount
        /// </summary>
        public int SelectCount(int startRowIndex, int maximumRows)
        {
            return RecordCount;
        }
        #endregion
        #endregion

        #region RecordCount
        /// <summary>
        /// RecordCount
        /// </summary>
        public int RecordCount
        {
            get
            {
                string sql = GetSelectSQL(SelectParameters);

                int Count = SelectCountSQL(sql);

                return Count;
            }
        }
        #endregion


    }
}

