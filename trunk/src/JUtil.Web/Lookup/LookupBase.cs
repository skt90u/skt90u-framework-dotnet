using System;
using System.Collections;
using System.Data;
using JUtil.ResourceManagement;
using System.Web.SessionState;

namespace JUtil.Web
{
    /// <summary>
    /// LookupBase
    /// </summary>
    public abstract class LookupBase
    {
        /// <summary>
        /// 所關聯的資料庫
        /// </summary>
        protected abstract IDatabase GetDbObject();

        //private IDictionary cache = null;
        //private IDictionary Cache
        //{
        //    get
        //    {
        //        cache = cache ?? CreateCache();

        //        return cache;
        //    }
        //}
        //private IDictionary CreateCache()
        //{
        //    bool isWebApp = JUtil.Environment.ApplicationType == ApplicationType.WebApplication;

        //    IDictionary retValue = isWebApp
        //        ? (new HttpSessionStateAdapter(StateManagement.Session)) as IDictionary<string, object>
        //        : new Hashtable();

        //    return retValue;
        //}

        private HttpSessionState Cache = StateManagement.Session;

        private void CheckRules(DataTable dt, string section)
        {
            if (dt.Columns.Contains("VALUE") == false)
            {
                string error = string.Format("In section {0}, cannot find out field named VALUE.", section);
                throw new Exception(error);
            }

            if (dt.Columns.Contains("TEXT") == false)
            {
                string error = string.Format("In section {0}, cannot find out field named TEXT.", section);
                throw new Exception(error);
            }
        }

        public string GetSQL(DatabaseType databaseType, string section, params object[] args)
        {
            string sql = GetSQL(section);

            return Formatter.ObjectFormat(databaseType, sql, args);
        }

        public string GetSQL(string section, params object[] args)
        {
            return SqlFile.GetSQL(GetType(), section, args);
        }

        private string GetCacheTableKey(string section, params object[] args)
        {
            string key = GetSQL(section, args);
            return key = string.Format("table_{0}", key);
        }

        private string GetCacheHashKey(string section, params object[] args)
        {
            string key = GetSQL(section, args);
            return key = string.Format("hash_{0}", key);
        }

        /// <summary>
        /// GetTable
        /// </summary>
        public DataTable GetTable(string section, params object[] args)
        {
            string sql = GetSQL(section, args);

            DataTable dt = GetDbObject().SelectSQL(sql);

            return dt;
        }

        public DataTable GetTable(DatabaseType databaseType, string section, params object[] args)
        {
            string sql = GetSQL(databaseType, section, args);

            DataTable dt = GetDbObject().SelectSQL(sql);

            return dt;
        }

        /// <summary>
        /// GetRecordCount
        /// </summary>
        public int GetRecordCount(string section, params object[] args)
        {
            string sql = GetSQL(section, args);

            return GetDbObject().SelectCountSQL(sql);
        }

        /// <summary>
        /// GetHash
        /// </summary>
        public Hashtable GetHash(string section, params object[] args)
        {
            DataTable dt = GetTable(section, args);

            CheckRules(dt, section);

            Hashtable hash = new Hashtable();

            foreach (DataRow row in dt.Rows)
            {
                //object key = row["VALUE"];
                //object value = row["TEXT"];

                // 遇到一個問題，當key為Decimal而查詢的參數為Integer即使數值相等，也會誤判
                // 因此存成string先避開此一問題
                string key = row["VALUE"].ToString();
                string value = row["TEXT"].ToString();

                try
                {
                    hash.Add(key, value);
                }
                catch (Exception ex)
                {
                    Log.E(ex);
                }
            }

            return hash;
        }

        /// <summary>
        /// GetCacheTable
        /// </summary>
        public DataTable GetCacheTable(string section, params object[] args)
        {
            string key = GetCacheTableKey(section, args);

            object data = Cache[key];
            if (data == null)
            {
                data = GetTable(section, args);

                Cache.Add(key, data);
            }
            return data as DataTable;
        }

        /// <summary>
        /// GetCacheHash
        /// </summary>
        public Hashtable GetCacheHash(string section, params object[] args)
        {
            string key = GetCacheHashKey(section, args);

            object data = Cache[key];

            if (data == null)
            {
                data = GetHash(section, args);

                Cache.Add(key, data);
            }
            return data as Hashtable;
        }

        /// <summary>
        /// KeyToValue
        /// </summary>
        public string KeyToValue(object key, string section, params object[] args)
        {
            try
            {
                if (DBNull.Value.Equals(key) || key == null) return string.Empty;

                Hashtable hash = GetCacheHash(section, args);

                string index = key.ToString();

                object o = hash[index];
                
                // 如果找不到對應值就輸出原本代碼
                return o == null ? key.ToString() : o.ToString();
            }
            catch (Exception ex)
            {
                Log.E(ex);

                return key.ToString();
            }
        }

        /// <summary>
        /// ValueToKey
        /// </summary>
        public string ValueToKey(object value, string section, params object[] args)
        {
            try
            {
                if (DBNull.Value.Equals(value) || value == null) return string.Empty;

                Hashtable hash = GetCacheHash(section, args);

                object o = null;
                foreach (DictionaryEntry entry in hash)
                {
                    string strValue = value.ToString();

                    if (entry.Value.Equals(strValue))
                    {
                        o = entry.Key;
                        break;
                    }
                }

                // 如果找不到對應值就輸出原本代碼
                return o == null ? value.ToString() : o.ToString();
            }
            catch (Exception ex)
            {
                Log.E(ex);

                return value.ToString();
            }
        }

    }
}
