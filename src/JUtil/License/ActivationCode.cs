using System;
using System.Collections.Generic;

namespace JUtil
{
    public class ActivationCode
    {
        public static string Generate(string softwareName, string encodingCertificationCode)
        {
            string serialNumber = string.Empty;
            string cpuId = string.Empty;

            CertificationCode.Parse(encodingCertificationCode, out serialNumber, out cpuId);

            LicenseManagement.UpdateUsedCpuId(softwareName, serialNumber, cpuId);

            serialNumber = serialNumber.Replace("-", "");

            string actCode = string.Format("{0}-{1}", serialNumber, cpuId);

            return Encode(actCode);
        }

        public static string GenerateButNoUpdate(string softwareName, string encodingCertificationCode)
        {
            string serialNumber = string.Empty;
            string cpuId = string.Empty;

            CertificationCode.Parse(encodingCertificationCode, out serialNumber, out cpuId);

            serialNumber = serialNumber.Replace("-", "");

            string actCode = string.Format("{0}-{1}", serialNumber, cpuId);

            return Encode(actCode);
        }

        public static void Parse(string encodingStr, out string serialNumber, out string cpuId)
        {
            string decodingStr = Decode(encodingStr);

            string[] tokens = decodingStr.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length != 2)
                throw new Exception(String.Format("ActivationCode format error,can not parse: {0}", encodingStr));

            serialNumber = string.Empty;

            for (int i = 0; i < tokens[0].Length; i++)
            {
                serialNumber += tokens[0][i];

                if ((i + 1) % 5 == 0)
                    serialNumber += '-';
            }

            serialNumber = serialNumber.TrimEnd(new char[] { '-' });

            cpuId = tokens[1];
        }

        public static void Check(string softwareName, string actCode)
        {
            try
            {
                string serialNumber = string.Empty;
                string cpuId = string.Empty;

                Parse(actCode, out serialNumber, out cpuId);

                SerialNumber.Check(softwareName, serialNumber);

                if (JUtil.Hardware.CpuId != cpuId)
                    throw new Exception("ActivationCode is not match with local computer settings");
            }
            catch (Exception e)
            {
                throw new Exception(string.Format("ActivationCode is not correct,{0}", e.Message));
            }
        }

        private static IList<char> AcOpts
        {
            get { return CharMap.GetMap(); }

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
                    int idx = AcOpts.IndexOf(c);

                    idx = idx + (i + 1) * 5;

                    do
                    {
                        if (0 <= idx && idx < AcOpts.Count)
                            break;

                        idx -= AcOpts.Count;

                    } while (true);

                    encodingStr += AcOpts[idx];
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
                    int idx = AcOpts.IndexOf(c);

                    idx = idx - (i + 1) * 5;

                    do
                    {
                        if (0 <= idx && idx < AcOpts.Count)
                            break;

                        idx += AcOpts.Count;

                    } while (true);

                    decodingStr += AcOpts[idx];
                }
            }

            return decodingStr;
        }
    }
}
