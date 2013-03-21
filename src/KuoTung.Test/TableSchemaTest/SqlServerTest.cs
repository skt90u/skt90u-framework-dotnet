using System;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace KuoTung.TableSchemaTest
{
    [TestFixture]
    public class SqlServerTest
    {
        [Test]
        public void TestCreateTableSchema()
        {
            TableSchema ts = TableSchema.Create(DbProvider.SqlServer, @"Data Source=BAKERY\SQLEXPRESS;Initial Catalog=NorthWind;User Id=sa;Password=sapass;", "select * from categories");
        }
    }
}
