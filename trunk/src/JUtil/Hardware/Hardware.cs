using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;

namespace JUtil
{
    public class Hardware
    {
        private static string cpuId;
        public static string CpuId
        {
            get
            {
                if(cpuId==null)
                {
                    // 專案請先加入參考 System.Management
                    // 透過 ManagementObjectSearcher 類別用類似 SQL 的語法查詢
                    ManagementObjectSearcher wmiSearcher
                                = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

                    // 使用 ManagementObjectSearcher 的 Get 方法取得所有集合
                    foreach (ManagementObject obj in wmiSearcher.Get())
                    {
                        // 取得CPU 序號
                        cpuId = obj["ProcessorId"].ToString();
                    }
                }

                return cpuId.Trim().ToUpper();
            }
        }
    }
}
