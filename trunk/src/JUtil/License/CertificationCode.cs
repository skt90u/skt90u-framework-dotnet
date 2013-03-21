using System;
using System.Collections.Generic;
using JUtil.Extensions;

namespace JUtil
{
    public class CertificationCode
    {
        private static IList<char> CcOpts
        {
            get { return CharMap.GetMap(); }

        }

        public static string Generate(string serialNumber)
        {
            string certificationCode = string.Empty;

            serialNumber = serialNumber.Replace("-", "");

            certificationCode = string.Format("{0}-{1}", serialNumber, JUtil.Hardware.CpuId);

            return Encode(certificationCode);
        }

        public static void Parse(string encodingStr, out string serialNumber, out string cpuId)
        {
            string decodingStr = Decode(encodingStr);

            string[] tokens = decodingStr.Split(new char[] {'-'}, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length != 2)
                throw new Exception(String.Format("CertificationCode format error,can not parse: {0}", encodingStr));

            serialNumber = string.Empty;

            for (int i = 0; i < tokens[0].Length; i++)
            {
                serialNumber += tokens[0][i];

                if ((i + 1) % 5 == 0)
                    serialNumber += '-';
            }

            serialNumber = serialNumber.TrimEnd(new char[] {'-'});

            cpuId = tokens[1];
        }

        public static void Check(string softwareName, string encodingStr)
        {
            try
            {
                string serialNumber = string.Empty;

                string cpuId = string.Empty;

                Parse(encodingStr, out serialNumber, out cpuId);

                SerialNumber.Check(softwareName, serialNumber);

                // 檢查此序號,是否已經被使用,或者被同一個CpuId重新註冊
                
                string usedCpuId = LicenseManagement.GetUsedCpuId(softwareName, serialNumber);
                if (false == ExtString.StrIsNullOrEmpty(usedCpuId))
                    if (usedCpuId.Equals(cpuId) == false)
                        throw new Exception("SerailNumber of CertificationCode has been used by another computer");    
                
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("CertificationCode is not correct, {0}", e.Message));
            }
        }

        private static string Encode(string certificationCode)
        {
            string encodingStr = string.Empty;

            for (int i = 0; i < certificationCode.Length; i++)
            {
                char c = certificationCode[i];

                if (c == '-')
                    encodingStr += c;
                else
                {
                    int idx = CcOpts.IndexOf(c);

                    idx = idx + (i + 1)*10;

                    do
                    {
                        if (0 <= idx && idx < CcOpts.Count)
                            break;

                        idx -= CcOpts.Count;

                    } while (true);

                    encodingStr += CcOpts[idx];
                }
            }

            return encodingStr;
        }

        private static string Decode(string encodingStr)
        {
            string decodingStr = string.Empty;

            for (int i = 0; i < encodingStr.Length; i++)
            {
                char c = encodingStr[i];

                if (c == '-')
                    decodingStr += c;
                else
                {
                    int idx = CcOpts.IndexOf(c);

                    idx = idx - (i + 1)*10;

                    do
                    {
                        if (0 <= idx && idx < CcOpts.Count)
                            break;

                        idx += CcOpts.Count;

                    } while (true);

                    decodingStr += CcOpts[idx];
                }
            }

            return decodingStr;
        }
        
    }
}
