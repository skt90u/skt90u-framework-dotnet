using System;
using System.Collections.Generic;
using System.Data;

namespace JUtil
{
    public class LicenseManagement
    {
        public static void Add(License license)
        {
            string sql =
                string.Format(
                    "insert into LICENSE(COMMENT, SOFTWARENAME, SERIALNUMBER, CPUID) values('{0}', '{1}', '{2}', '{3}')",
                    license.comment,
                    license.softwareName,
                    license.serialNumber,
                    license.cpuId);

            GetDbObject().ExecuteSQL(sql);
        }

        public static void Update(License license)
        {
            string sql = string.Format("update LICENSE set COMMENT = '{0}', SOFTWARENAME = '{1}', SERIALNUMBER = '{2}', CPUID = '{3}' where SOFTWARENAME = '{4}' and SERIALNUMBER = '{5}'",
                    license.comment,
                    license.softwareName,
                    license.serialNumber,
                    license.cpuId,
                    license.softwareName,
                    license.serialNumber);

            GetDbObject().ExecuteSQL(sql);
        }

        public static void Delete(License license)
        {
            string sql = string.Format("delete from LICENSE where SOFTWARENAME = '{0}' and SERIALNUMBER = '{1}'", license.softwareName, license.serialNumber);

            GetDbObject().ExecuteSQL(sql);
        }

        public static List<License> Select(string softwareName, string serialNumber)
        {
            List<License> list = new List<License>();

            string sql = String.Format("select * from LICENSE where SOFTWARENAME = '{0}' and SERIALNUMBER = '{1}'", softwareName, serialNumber);

            DataTable dt = GetDbObject().SelectSQL(sql);
            foreach (DataRow row in dt.Rows)
                list.Add(new License(row));

            return list;
        }

        public static List<License> Select()
        {
            List<License> list = new List<License>();

            string sql = String.Format("select * from LICENSE");

            DataTable dt = GetDbObject().SelectSQL(sql);
            foreach (DataRow row in dt.Rows)
                list.Add(new License(row));

            return list;
        }

        public static string GetUsedCpuId(string softwareName, string serialNumber)
        {
            List<License> list = Select(softwareName, serialNumber);

            return (list.Count > 0) ? list[0].cpuId : string.Empty;
        }

        public static void UpdateUsedCpuId(string softwareName, string serialNumber, string cpuId)
        {
            List<License> list = Select(softwareName, serialNumber);

            foreach(License license in list)
            {
                license.cpuId = cpuId;

                Update(license);
            }
        }

        private static string dbPath;
        public static string DbPath
        {
            get
            {
                if (dbPath == null)
                {
                    if (Net.LocalIP == "192.168.0.100")
                    {
                        // 使用筆記型電腦
                        dbPath = @"Y:\GoogleProjectHosting\jelly-dotnet-framework\src\JUtil\License\LicenseManagement.sl3";
                    }
                    else
                    {
                        // 使用家用電腦
                        dbPath = @"E:\GoogleProjectHosting\jelly-dotnet-framework\src\JUtil\License\LicenseManagement.sl3";
                    }
                    
                    if (false == JUtil.Path.File.Exists(dbPath))
                    {
                        // 現場執行使用
                        string localDb = JUtil.Path.File.GetAbsolutePath(JUtil.Path.Directory.Application, "LicenseManagement.sl3");

                        if (JUtil.Path.File.Exists(localDb))
                            dbPath = localDb;
                    }

                    Log.I("dbPath: {0}", dbPath);
                }

                return dbPath;
            }
        }

        private static IDatabase GetDbObject()
        {
            if (false == JUtil.Path.File.Exists(DbPath))
                throw new Exception(string.Format("Can not find any Sqlite database in path {0}", DbPath));

            DbConnectionConfig dbConnectionConfig = new DbConnectionConfig { ConnectionString = String.Format("Data Source={0};Version=3;", DbPath), DbProvider = DbProvider.Sqlite };

            IDatabase db = DbUtil.GetNoneHoldingConnectionDb(dbConnectionConfig);

            return db;
        }
    }
}
