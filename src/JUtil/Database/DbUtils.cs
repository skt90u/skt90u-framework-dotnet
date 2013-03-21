using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace JUtil
{
    public class DbUtils
    {
        IDbExtraOp db;
        private const string NewValuePatternPrefix = JUtil.Formatter.NewValuePatternPrefix;
        private const string OldValuePatternPrefix = JUtil.Formatter.OldValuePatternPrefix;

        public DbUtils(IDbExtraOp db)
        {
            this.db = db;
        }

        public List<string> GetDatabases()
        {
            try
            {
                string collectionName = "Databases";

                DataTable dt = db.GetSchema(collectionName);

                List<string> lst = new List<string>();

                foreach (DataRow row in dt.Rows)
                    lst.Add(row["database_name"].ToString());

                return lst;
            }
            catch (Exception ex)
            {
                string error = string.Format("cannot get databases when you use this DbProvider({0}).", ex.Message);
                throw new Exception(error);
            }
        }

        public List<string> GetTables()
        {
            try
            {
                string collectionName = "Tables";

                DataTable dt = db.GetSchema(collectionName);

                List<string> lst = new List<string>();

                DataRow[] rows = null;

                // DbType    DbProvider table_type
                // SqlServer OleDb      'base table'
                // SqlServer SqlServer  'table'
                if (dt.Columns.Contains("table_type"))
                    rows = dt.Select("table_type in ('base table', 'table')");

                /*
                OWNER	TABLE_NAME	TYPE
                TPDBUSER	A_BACCOUNT_DEPT_BAS	User
                TPDBUSER	FESTIVAL_BALLOT	User
                TPDBUSER	FESTIVAL_BALLOT_ITEM	User
                TPDBUSER	FESTIVAL_REGISTRATION	User
                TPDBUSER	FESTIVAL_REGISTRATION_ITEM	User

                 */
                // DbType    DbProvider TYPE
                // Oracle    Oracle     'USER'
                if (dt.Columns.Contains("TYPE"))
                {
                    ConnectionStringParser parser = new ConnectionStringParser(db.DbProvider, db.ConnectionString);

                    string OWNER = parser.UserID;

                    string whereCondition = string.Format("TYPE in ('User') and OWNER = '{0}'", OWNER);

                    rows = dt.Select(whereCondition);
                }

                foreach (DataRow row in rows)
                    lst.Add(row["table_name"].ToString());

                return lst;
            }
            catch (Exception ex)
            {
                string error = string.Format("cannot get tables when you use this DbProvider({0}).", ex.Message);
                throw new Exception(error);
            }
        }

        public List<string> GetViews()
        {
            try
            {
                string collectionName = "Tables";

                DataTable dt = db.GetSchema(collectionName);

                List<string> lst = new List<string>();

                DataRow[] rows = dt.Select("table_type='view'");

                foreach (DataRow row in rows)
                    lst.Add(row["table_name"].ToString());

                return lst;
            }
            catch (Exception ex)
            {
                string error = string.Format("cannot get views when you use this DbProvider({0}).", ex.Message);
                throw new Exception(error);
            }
        }

        #region GetSchema
        public TableSchema GetSchema(string table)
        {
            return TableSchema.GetByTable(db, table);
        }
        #endregion

        #region GetSelectSql
        public string GetSelectSql(string table)
        {
            TableSchema ts = GetSchema(table);

            StringBuilder output = new StringBuilder();

            output.Append("select");

            foreach (FieldSchema fs in ts.Fields)
                output.AppendFormat(" {0},", fs.ColumnName);

            output.Remove(output.Length - 1, 1);

            output.AppendFormat(" from {0}", table);

            return output.ToString();
        }
        #endregion
        #region GetInsertSql
        public string GetInsertSql(string table)
        {
            TableSchema ts = GetSchema(table);

            StringBuilder output = new StringBuilder();

            output.AppendFormat("insert into {0}(", table);

            for (int i = 0; i < ts.Fields.Length; i++)
            {
                FieldSchema fs = ts.Fields[i];

                if (i != 0)
                    output.Append(",");

                output.AppendFormat(" {0}", fs.ColumnName);
            }

            output.Append(") values(");

            for (int i = 0; i < ts.Fields.Length; i++)
            {
                FieldSchema fs = ts.Fields[i];

                if (i != 0)
                    output.Append(",");

                output.AppendFormat(" @{0}", fs.ColumnName);
            }

            output.Append(")");

            return output.ToString();
        }
        #endregion
        #region GetUpdateSql
        public string GetUpdateSql(string table)
        {
            TableSchema ts = GetSchema(table);

            StringBuilder output = new StringBuilder();

            output.AppendFormat("update {0} set", table);

            for (int i = 0; i < ts.Fields.Length; i++)
            {
                FieldSchema fs = ts.Fields[i];

                if (i != 0)
                    output.Append(",");

                output.AppendFormat(" {0}={1}{0}", fs.ColumnName, NewValuePatternPrefix);
            }

            output.Append(" where");

            bool isFirst = true;
            foreach (FieldSchema fs in ts.Fields)
            {
                if (fs.IsKey == false)
                    continue;

                if (isFirst)
                    isFirst = false;
                else
                    output.Append(",");

                output.AppendFormat(" ({0}={1}{0}) and", fs.ColumnName, OldValuePatternPrefix);
            }

            output.Append(" (1=1)");

            return output.ToString();
        }
        #endregion
        #region GetDeleteSql
        public string GetDeleteSql(string table)
        {
            TableSchema ts = GetSchema(table);

            StringBuilder output = new StringBuilder();
            
            output.AppendFormat("delete from {0} where", table);

            bool isFirst = true;
            foreach (FieldSchema fs in ts.Fields)
            {
                if (fs.IsKey == false)
                    continue;

                if (isFirst)
                    isFirst = false;
                else
                    output.Append(",");

                output.AppendFormat(" ({0}={1}{0}) and", fs.ColumnName, OldValuePatternPrefix);
            }

            output.Append(" (1=1)");

            return output.ToString();
        }
        #endregion
    }
}
