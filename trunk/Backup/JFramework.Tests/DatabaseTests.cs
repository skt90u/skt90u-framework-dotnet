using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.Common;
using System.Collections.Generic;
using NUnit.Framework;

namespace JFramework.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
		//
        // declare the testee
        //
        private IDatabase instance = null;

        private string querySQL = string.Empty;

        private string executeSQL = string.Empty; 
		//
		// sets up the fixture, for example, open a network connection. This method is called before a test is executed
		//
        [SetUp]
        public void SetUp()
        {
            //querySQL = "select * from TRA_PTT_VER order by exported_date_time";
            querySQL = "select * from TRA_PTT_VER";
            executeSQL = "update multiengine set code_id = 9999 where dv_id = 9999"; 

            DatabaseMode databaseMode = DatabaseMode.OleDb;
            string connectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Password=sapass;Initial Catalog=ITS;Data Source=JELLY\\SQLEXPRESS";
            instance = new Database(databaseMode, connectionString);
        }	
		
		//
		// tears down the fixture, for example, close a network connection. This method is called after a test is executed
		//
        [TearDown]
        public void TearDown()
        {
            instance = null;
        }

        #region TestCases

		[Test]
        public void testSelectSQLDataTable()
		{
            try
            {
                DataTable actual = instance.SelectSQL(querySQL);
            }
            catch (Exception ex)
            {
                Log.E(ex);
                throw;
            }		
		}

		[Test]
        public void testSelectSQLDataSet()
		{
            try
            {
                List<KeyValuePair<string, string>> querySQLs = new List<KeyValuePair<string, string>>();
                querySQLs.Add(new KeyValuePair<string, string>("testSelectSQLDataSet", querySQL));
                DataSet actual = instance.SelectSQL(querySQLs);
            }
            catch (Exception ex)
            {
                Log.E(ex);
                throw;
            }		
		}

		[Test]
		public void testSelectRecordCountSQL()
		{
            try
            {
                int actual = instance.SelectRecordCountSQL(querySQL);
            }
            catch (Exception ex)
            {
                Log.E(ex);
                throw;
            }		
		}

		[Test]
		public void testExecuteSQL()
		{
            try
            {
                instance.ExecuteSQL(executeSQL);
            }
            catch (Exception ex)
            {
                Log.E(ex);
                throw;
            }		
		}

		[Test]
		public void testExecuteSQLs()
		{
            try
            {
                List<string> SQLs = new List<string>();
                SQLs.Add(executeSQL);
				instance.ExecuteSQL(SQLs);
            }
            catch (Exception ex)
            {
                Log.E(ex);
                throw;
            }		
		}

		[Test]
		public void testGetSchemaTable()
		{
            try
            {
                DataTable actual = instance.GetSchemaTable(querySQL);
            }
            catch (Exception ex)
            {
                Log.E(ex);
                throw;
            }		
		}

		[Test]
		public void testGetDataReader()
		{
            try
            {
                DbDataReader actual = instance.GetDataReader(querySQL);
            }
            catch (Exception ex)
            {
                Log.E(ex);
                throw;
            }		
		}

		[Test]
        [Ignore]
		public void testConnectionCount()
		{
            try
            {
				int actual = instance.ConnectionCount();
            }
            catch (Exception ex)
            {
                Log.E(ex);
                throw;
            }		
		}
        #endregion		
    }
}
