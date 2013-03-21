using System;
using System.Collections.Generic;

namespace JUtil.CodeGenerator
{
    /// <summary>
    /// 提供建立測試資料的泛用框架
    /// </summary>
    /// <remarks>
    /// 必須備妥以下條件
    /// (1) GetDbObject         - 連接資料庫的物件
    /// (2) GetResetSql         - 重置表格
    /// (3) GetInsertSqlPattern - 新增單筆資料的Sql樣板
    /// (4) GetDbType           - 格式化新增Sql所需參數
    /// (5) CreateData          - 建立所需新增的資料
    /// </remarks>
    /// <typeparam name="T">資料庫表格對應的Dal物件</typeparam>
    public abstract class DataGeneratorBase<T>
        where T : class
    {
        #region Abstract Methods

        protected abstract IDatabase GetDbObject();

        protected abstract string GetResetSql();

        protected abstract string GetInsertSqlPattern();

        protected abstract DatabaseType GetDbType();

        protected abstract List<T> CreateData();

        #endregion

        #region Cache Arguments of DataGenerator

        private Type _DalType;
        protected Type DalType
        {
            get
            {
                if (_DalType == null)
                {
                    _DalType = typeof(T);
                }
                return _DalType;
            }
        }

        private DatabaseType DbType
        {
            get
            {
                return GetDbType();
            }
        }

        private string _InsertSqlPattern;
        private string InsertSqlPattern
        {
            get
            {
                if (_InsertSqlPattern == null)
                {
                    _InsertSqlPattern = GetInsertSqlPattern();
                }
                return _InsertSqlPattern;
            }
        }

        private string _ResetSql;
        private string ResetSql
        {
            get
            {
                if (_ResetSql == null)
                {
                    _ResetSql = GetResetSql();
                }
                return _ResetSql;
            }
        }

        private List<T> _Data;
        private List<T> Data
        {
            get
            {
                if (_Data == null)
                {
                    _Data = CreateData();
                }
                return _Data;
            }
        }

        private IDatabase _DbObject;
        protected IDatabase DbObject
        {
            get
            {
                if (_DbObject == null)
                {
                    _DbObject = GetDbObject();
                }
                return _DbObject;
            }
        }
        #endregion

        /// <summary>
        /// Helper Object for generate random data more easily
        /// </summary>
        protected RandomData RandomData { get { return RandomData.Instance; } }

        /// <summary>
        /// 格式化新增Sql
        /// </summary>
        private string GetInsertSql(T delta)
        {
            return JUtil.Formatter.GenericFormat<T>(DbType, InsertSqlPattern, delta, null);
        }

        public void Run()
        {
            List<string> SQLs = new List<string>();

            SQLs.Add(ResetSql);

            foreach (T delta in Data)
                SQLs.Add(GetInsertSql(delta));

            DbObject.ExecuteSQL(SQLs);
        }
    }

    #region RandomData
    public class RandomData
    {
        public static RandomData Instance
        {
            get
            {
                _Instance = _Instance ?? new RandomData();
                return _Instance;
            }
        }
        private static RandomData _Instance;

        RandomData()
        {
            RandObj = new Random();
        }

        private Random RandObj;

        public T Get<T>(List<T> samples)
        {
            int index = Index(samples.Count);

            return samples[index];
        }

        public int Index(int size)
        {
            return RangeValue(0, size - 1);
        }

        public int RangeValue(int min, int max)
        {
            // 避免 System.ArgumentOutOfRangeException
            if (min > max)
            {
                int temp = min;
                min = max;
                max = min;
            }

            return RandObj.Next(min, max + 1);
        }
    }
    #endregion

}
