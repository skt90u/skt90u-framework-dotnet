using System;
using System.Collections;
using System.Data.SqlClient;
using NUnit.Framework;
using System.Collections.Generic;
using System.Data;

namespace KuoTung.TableSchemaTest
{
    [TestFixture]
    public class MySqlTest
    {
        [Test]
        public void TestCreateTableSchema()
        {
            TableSchema ts = TableSchema.Create(DbProvider.MySql, @"Server=localhost;Database=northwind;Uid=root;Pwd=490410056;", "select * from categories");
        }
    }
}
