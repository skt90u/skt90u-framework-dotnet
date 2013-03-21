using System;
using System.Data;
using JUtil.Extensions;
using Microsoft.Win32;

namespace JUtil
{
    public class License
    {
        public static void Check(string softwareName)
        {
            try
            {
                License license = License.Create(softwareName);

                SerialNumber.Check(softwareName, license.serialNumber);

                if (license.cpuId != JUtil.Hardware.CpuId)
                    throw new Exception("License is not match with local computer settings");

                // Check 
                //string usedCpuId = LicenseRemoteProxy.GetUsedCpuId(license.serialNumber);
                //if (license.cpuId != usedCpuId)
                //    throw new Exception("此License已經在別台電腦使用");
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("Failed to verify license, {0}", e.Message));
            }
        }

        public License()
        {
        }

        public License(DataRow row)
        {
            comment = row["COMMENT"] == DBNull.Value ? string.Empty : Convert.ToString(row["COMMENT"]).Trim();
            softwareName = row["SOFTWARENAME"] == DBNull.Value ? string.Empty : Convert.ToString(row["SOFTWARENAME"]).Trim();
            serialNumber = row["SERIALNUMBER"] == DBNull.Value ? string.Empty : Convert.ToString(row["SERIALNUMBER"]).Trim();
            cpuId = row["CPUID"] == DBNull.Value ? string.Empty : Convert.ToString(row["CPUID"]).Trim();
        }

        public static License Create(string softwareName)
        {
            string encodingXml = GetEncodingLicenseString(softwareName);

            if(JUtil.Extensions.ExtString.StrIsNullOrEmpty(encodingXml))
                throw new Exception("Can not find License");

            string xml = JUtil.Security.Decode(encodingXml);

            License license = null;

            try
            {
                license = JUtil.Serialization.Xml.Deserialize<License>(xml);
            }
            catch (Exception)
            {
                throw new Exception("License format error");
            }

            return license;
        }

        public static void Generate(string softwareName, string actCode)
        {
            string serialNumber;
            string cpuId;
            ActivationCode.Parse(actCode, out serialNumber, out cpuId);

            License license = new License();
            license.comment = string.Empty;
            license.softwareName = softwareName;
            license.serialNumber = serialNumber;
            license.cpuId = cpuId;

            string xml = JUtil.Serialization.Xml.Serialize(license);

            string encodingXml = JUtil.Security.Encode(xml);

            SetEncodingLicenseString(softwareName, encodingXml);
        }

        public static string GetEncodingLicenseString(string softwareName)
        {
            // http://www.codeproject.com/Articles/3389/Read-write-and-delete-from-registry-with-C

            string name = string.Format(@"Software\{0}\", softwareName);

            try
            {
                RegistryKey reg = Registry.LocalMachine.OpenSubKey(name);

                return reg == null ? string.Empty : (string)reg.GetValue("License");
            }
            catch (Exception e)
            {
                Log.E(e);

                return string.Empty;
            }
        }

        public static void SetEncodingLicenseString(string softwareName, string value)
        {
            // http://www.codeproject.com/Articles/3389/Read-write-and-delete-from-registry-with-C

            string name = string.Format(@"Software\{0}\", softwareName);

            try
            {
                // I have to use CreateSubKey 
                // (create or open it if already exits), 
                // 'cause OpenSubKey open a subKey as read-only
                RegistryKey reg = Registry.LocalMachine.CreateSubKey(name);

                reg.SetValue("License", value);
            }
            catch (Exception e)
            {
                Log.E(e);
            }
        }

        public string comment { get; set; }
        public string softwareName { get; set; }
        public string serialNumber { get; set; }
        public string cpuId { get; set; }

        public static void UT()
        {
            string softwareName = "DigAvControl";

            try
            {
                License.Check(softwareName);
            }
            catch (Exception e)
            {
                Log.E(e);

                try
                {
                    UT_LicenseActivator(softwareName);
                }
                catch (Exception ee)
                {
                    Log.E(ee);
                }
            }
        }

        private static void UT_LicenseActivator(string softwareName)
        {
            try
            {
                // Client Side
                // 輸入序號
                string serialNumber = SerialNumber.Generate(softwareName);

                // Client Side
                // 判斷序號是否正確
                SerialNumber.Check(softwareName, serialNumber);

                // Client Side
                // 如果正確,就產生驗證碼,並將驗證碼上傳至伺服器
                string encodingStr = CertificationCode.Generate(serialNumber);

                // Server Side
                // 伺服器判斷驗證碼是否正確
                CertificationCode.Check(softwareName, encodingStr);

                // Server Side
                // 如果正確,就產生啟用碼,並將啟用碼回傳至Client
                string actCode = ActivationCode.Generate(softwareName, encodingStr);

                // Client Side
                // 判斷啟用碼是否正確,如果正確,就產生License檔
                ActivationCode.Check(softwareName, actCode);

                License.Generate(softwareName, actCode);
            }
            catch (Exception e)
            {
                Log.E(e);
            }
        }
    }
}
