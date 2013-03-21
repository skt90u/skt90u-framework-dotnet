using System;
using System.Text;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;

namespace JFramework.Tests
{
    [TestFixture]
    public class LogTests
    {
		//
		// sets up the fixture, for example, open a network connection. This method is called before a test is executed
		//
        [SetUp]
        public void SetUp(){}
		//
		// tears down the fixture, for example, close a network connection. This method is called after a test is executed
		//
        [TearDown]
        public void TearDown(){}

        #region TestCases

        [Test]
		public void testD() {
            Log.I("unit test for Log.D(message), please ingore this message.");
        }

		[Test]
		public void testI(){
            Log.I("unit test for Log.I(message), please ingore this message.");
        }

		[Test]
		public void testW(){
            Log.W("unit test for Log.W(message), please ingore this message.");
        }

		[Test]
		public void testE(){
            Log.E("unit test for Log.E(message), please ingore this message.");
        }
		
		[Test]
        public void testLogException()
		{
            try
            {
                throw new Exception("unit test for Log.E(exception), please ingore this message.");
            }
            catch (Exception ex)
            {
                Log.E(ex);
            }		
		}

        //[Test]
        //public void testReportError()
        //{
        //    Log.ReportError("LogTests", "testReportError");		
        //}

        //[Test]
        //public void testReportException()
        //{
        //    try
        //    {
        //        throw new Exception("test ReportError Exception");
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ReportError(ex);
        //    }		
        //}
        #endregion		
    }
}
