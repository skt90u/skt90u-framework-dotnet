using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using JFramework;
using System.Data;

namespace Tests
{
    class Program
    {

        static void func1()
        {
            try
            {
                DatabaseMode databaseMode = DatabaseMode.OleDb;
                string connectionString = "Provider=SQLOLEDB.1;Integrated Security=SSPI;Persist Security Info=False;User ID=sa;Password=sapass;Initial Catalog=ITS;Data Source=JELLY\\SQLEXPRESS";
                Database db = new Database(databaseMode, connectionString);
                
                DataTable dt = null;
                dt = db.SelectSQL("select * from TRA_PTT_VER order by exported_date_time");
                dt = db.SelectSQL("select * from TRA_PTT_VER order by exported_date_time");
                dt = db.SelectSQL("select * from TRA_PTT_VER order by exported_date_time");
                dt = db.SelectSQL("select * from TRA_PTT_VER order by exported_date_time");
                int i = dt.Rows.Count;

                Log.D("D");
                Log.I("I");
                Log.W("W");
                Log.E("E");
                //XDatabase xd = new XDatabase(databaseMode, connectionString);
                //DataTable dt = null;
                //dt = xd.SelectSQL("select * from TRA_PTT_VER order by exported_date_time");
                //dt = xd.SelectSQL("select * from TRA_PTT_VER order by exported_date_time");
                //dt = xd.SelectSQL("select * from TRA_PTT_VER order by exported_date_time");
                //dt = xd.SelectSQL("select * from TRA_PTT_VER order by exported_date_time");
                //int i = dt.Rows.Count;
            }
            catch (Exception ex)
            {
                Log.E(ex);
            }
        }
        static void Main(string[] args)
        {
            func1();
            //func1();
            //func1();
        }
    }
}
