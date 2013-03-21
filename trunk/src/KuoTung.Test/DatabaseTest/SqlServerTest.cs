using System;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace KuoTung.DatabaseTest
{
    [TestFixture]
    public class SqlServerTest
    {
        private Database database;
 
        [SetUp]
        protected void SetUp()
        {
            database = new Database(DbProvider.SqlServer, @"Data Source=BAKERY\SQLEXPRESS;Initial Catalog=NorthWind;User Id=sa;Password=sapass;");
        }

        [Test]
        public void TestSelect()
        {
            string sql = "select * from Categories";

            DataTable dt = database.Select(sql);

            Assert.AreEqual(8, dt.Rows.Count);
        }

        [Test]
        public void TestMultiSelect()
        {
            List<KeyValuePair<string, string>> sqls = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Categories", "select * from Categories"),
                new KeyValuePair<string, string>("Customers", "select * from Customers"),
            };

            DataSet ds = database.Select(sqls);

            Assert.AreEqual(8, ds.Tables["Categories"].Rows.Count);
            Assert.AreEqual(91, ds.Tables["Customers"].Rows.Count);
        }

        [Test]
        public void TestSelectCount()
        {
            string sql = "select * from Categories";

            int count = database.SelectCount(sql);

            Assert.AreEqual(8, count);
        }

        [Test]
        public void TestExecute()
        {
            string sql = string.Format("update Categories set Description = '{0}' where 1 = 0", DateTime.Now.ToString());

            database.Execute(sql);
        }
        
        [Test]
        public void TestPreStatementExecute()
        {
            Hashtable inParameters = new Hashtable();

            inParameters.Add("Description", DateTime.Now.ToString());

            database.Execute("update Categories set Description = @Description where 1 = 1", inParameters);
        }

        [Test]
        public void TestMultiExecute()
        {
            List<string> sqls = new List<string>
            {
                "update Categories set Description = '' where 1 = 0",
                "update Categories set Description = '' where 1 = 0",
            };

            database.Execute(sqls);
        }

        [Test]
        public void TestExecuteSp()
        {
            Hashtable outParameters = new Hashtable();

            outParameters.Add("@Count", 0);

            database.ExecuteSp("GetCount", null, outParameters, null);

            int count = Int32.Parse(outParameters["@Count"].ToString());

            Assert.AreEqual(8, count);
        }

        [Test]
        public void TestExecuteSpTable()
        {
            DataTable dt = database.ExecuteSp("SelectTable", null);
          
            Assert.AreEqual(8, dt.Rows.Count);
        }
    }
}
