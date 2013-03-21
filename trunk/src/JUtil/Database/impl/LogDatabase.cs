using System.Collections;
using System;
using System.Data;
using System.Text;
using System.Collections.Generic;

namespace JUtil
{
    /// <summary>
    /// eSqlLogMode
    /// </summary>
    [Flags]
    public enum eSqlLogMode
    {
        /// <summary>
        /// 不LOG
        /// </summary>
        NONE = 0,

        /// <summary>
        /// LOG查詢SQL
        /// </summary>
        SELECT = 1,

        /// <summary>
        /// LOG執行SQL
        /// </summary>
        EXECUTE = 2,

        /// <summary>
        /// LOG發生Exception時的SQL
        /// </summary>
        ERROR = 4,

        /// <summary>
        /// Logging所有類型
        /// </summary>
        ALL = SELECT | EXECUTE | ERROR
    } // end of eSqlLogMode


    /// <summary>
    /// LogDatabase
    /// </summary>
    public class LogDatabase : IDatabase
    {
        public delegate string fpGetPostmark();

        private readonly IDatabase db;
        private readonly eSqlLogMode sqlLogMode;
        private readonly fpGetPostmark GetPostmark;

        public LogDatabase(IDatabase db, eSqlLogMode sqlLogMode, fpGetPostmark GetPostmark)
        {
            this.db = db;

            this.sqlLogMode = sqlLogMode;

            this.GetPostmark = GetPostmark;
        }

        public LogDatabase(IDatabase db, eSqlLogMode sqlLogMode)
        {
            this.db = db;

            this.sqlLogMode = sqlLogMode;

            this.GetPostmark = null;
        }

        private string Postmark()
        {
            string result = string.Empty;

            string postmark = GetPostmark == null ? string.Empty : GetPostmark();

            if (!string.IsNullOrEmpty(postmark))
                result = string.Format("{0}\n", postmark);

            return result;
        }

        private string Postmark(Exception ex)
        {
            string result = string.Empty;

            string postmark = GetPostmark == null ? string.Empty : GetPostmark();

            if (!string.IsNullOrEmpty(postmark))
                result = string.Format("{0}:{1}\n", postmark, ex.Message);
            else
                result = string.Format("{0}\n", ex.Message);

            return result;
        }

        #region Logging

        private bool CanLogSelect { get { return (sqlLogMode & eSqlLogMode.SELECT) == eSqlLogMode.SELECT; } }
        private bool CanLogExecute { get { return (sqlLogMode & eSqlLogMode.EXECUTE) == eSqlLogMode.EXECUTE; } }
        private bool CanLogError { get { return (sqlLogMode & eSqlLogMode.ERROR) == eSqlLogMode.ERROR; } }

        #region LogSelectSql(string sql)
        private void LogSelectSql(string sql)
        {
            if (!CanLogSelect) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(Postmark());
            sb.Append("\n");

            sb.Append(sql);
            sb.Append("\n");

            Log.D(sb.ToString());
        }
        #endregion
        #region LogSelectSql(List<KeyValuePair<string, string>> SQLs)
        private void LogSelectSql(List<KeyValuePair<string, string>> SQLs)
        {
            if (!CanLogSelect) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(Postmark());
            sb.Append("\n");

            foreach (KeyValuePair<string, string> sql in SQLs)
            {
                string title = sql.Key.ToString();
                string content = sql.Value.ToString();
                string item = string.Format("{0}:{1}\n", title, content);
                sb.Append(item);
            }

            Log.D(sb.ToString());
        }
        #endregion
        #region LogExecuteSql(string sql)
        private void LogExecuteSql(string sql)
        {
            if (!CanLogExecute) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(Postmark());
            sb.Append("\n");

            sb.Append(sql);
            sb.Append("\n");

            Log.D(sb.ToString());
        }
        #endregion
        #region LogExecuteSql(ICollection<string> SQLs)
        private void LogExecuteSql(ICollection<string> SQLs)
        {
            if (!CanLogExecute) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(Postmark());
            sb.Append("\n");

            foreach (string sql in SQLs)
            {
                sb.Append(sql);
                sb.Append("\n");
            }

            Log.D(sb.ToString());
        }
        #endregion
        #region LogErrorSql(string sql, Exception ex)
        private void LogErrorSql(string sql, Exception ex)
        {
            if (!CanLogError) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(Postmark(ex));

            sb.Append(sql);
            sb.Append("\n");

            Log.E(sb.ToString());
        }
        #endregion
        #region LogErrorSql(ICollection<string> SQLs, Exception ex)
        private void LogErrorSql(ICollection<string> SQLs, Exception ex)
        {
            if (!CanLogError) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(Postmark(ex));

            foreach (string sql in SQLs)
            {
                sb.Append(sql);
                sb.Append("\n");
            }

            Log.E(sb.ToString());
        }
        #endregion
        #region LogErrorSql(List<KeyValuePair<string, string>> SQLs, Exception ex)
        private void LogErrorSql(List<KeyValuePair<string, string>> SQLs, Exception ex)
        {
            if (!CanLogError) return;

            StringBuilder sb = new StringBuilder();

            sb.Append(Postmark(ex));

            foreach (KeyValuePair<string, string> KVP in SQLs)
            {
                string sql = string.Format("{0}:{1}\n", KVP.Key, KVP.Value);
                sb.Append(sql);
            }

            Log.E(sb.ToString());
        }
        #endregion

        #endregion


        #region IDatabase 成員

        public DataTable SelectSQL(string sql)
        {
            DataTable dt = null;
            try
            {
                dt = db.SelectSQL(sql);

                LogSelectSql(sql);
            }
            catch (Exception ex)
            {
                LogErrorSql(sql, ex);

                throw;
            }
            return dt;
        }

        public DataSet SelectSQL(List<KeyValuePair<string, string>> sqls)
        {
            DataSet ds = null;
            try
            {
                ds = db.SelectSQL(sqls);

                LogSelectSql(sqls);
            }
            catch (Exception ex)
            {
                LogErrorSql(sqls, ex);

                throw;
            }
            return ds;
        }

        public int SelectCountSQL(string sql)
        {
            int Count = 0;
            try
            {
                Count = db.SelectCountSQL(sql);

                LogSelectSql(sql);
            }
            catch (Exception ex)
            {
                LogErrorSql(sql, ex);

                throw;
            }
            return Count;
        }

        public void ExecuteSQL(string sql)
        {
            try
            {
                db.ExecuteSQL(sql);

                LogExecuteSql(sql);
            }
            catch (Exception ex)
            {
                LogErrorSql(sql, ex);

                throw;
            }
        }

        public void ExecuteSQL(ICollection<string> sqls)
        {
            try
            {
                db.ExecuteSQL(sqls);

                LogExecuteSql(sqls);
            }
            catch (Exception ex)
            {
                LogErrorSql(sqls, ex);

                throw;
            }
        }

        public void ExecuteSQL(string sql, Hashtable inParameters)
        {
            try
            {
                db.ExecuteSQL(sql, inParameters);

                //LogExecuteSql(sql, inParameters);
            }
            catch (Exception)
            {
                //LogErrorSql(sql, inParameters, ex);

                throw;
            }
        }

        public void ExecuteSP(string procedureName, Hashtable inParameters, Hashtable outParameters, Hashtable retParameters)
        {
            try
            {
                db.ExecuteSP(procedureName, inParameters, outParameters, retParameters);

                //LogExecuteSql(sql, inParameters);
            }
            catch (Exception)
            {
                //LogErrorSql(sql, inParameters, ex);

                throw;
            }
        }

        public DataTable GetTableSchema(string sql)
        {
            return db.GetTableSchema(sql);
        }

        public DataTable GetSchema(string collectionName)
        {
            return db.GetSchema(collectionName);
        }

        public bool TestConnection()
        {
            return db.TestConnection();
        }

        public DbConnectionConfig DbConnectionConfig{get{return db.DbConnectionConfig;}}

        #endregion
    } // end of LogDatabase
}
