using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUtil
{
    public class SerialNumber
    {
        public const int Length = 10;

        public static void Check(string softwareName, string serialNumber)
        {
            softwareName = softwareName.ToUpper();

            serialNumber = serialNumber.Replace("-", "");

            for (int i = 0; i < serialNumber.Length; i++)
            {
                int idx = SnOpts.IndexOf(serialNumber[i]);

                int baseIdx = IndexOfBase(i, softwareName);

                int randomNum = idx - baseIdx;

                if (randomNum % (i + 1) != 0)
                    throw new Exception("SerialNumber is not correct");
            }
        }

        public static string Generate(string softwareName)
        {
            softwareName = softwareName.ToUpper();

            string serialNumber = string.Empty;

            for (int i = 0; i < SerialNumber.Length; i++)
            {
                serialNumber += GenerateChar(i, softwareName);

                if ((i + 1) % 5 == 0)
                    serialNumber += '-';
            }

            serialNumber = serialNumber.TrimEnd(new char[] { '-' });

            return serialNumber;
        }

        private static IList<char> SnOpts
        {
            get { return CharMap.GetMap(); }

        }

        private static Random random = new Random();
        
        private static char GenerateChar(int i, string softwareName)
        {
            int idx = -1;

            int baseIdx = IndexOfBase(i, softwareName);

            int randomNum = -1;
            do
            {
                randomNum = random.Next(0, 100);

                if (randomNum % (i + 1) != 0)
                    continue;

                idx = randomNum + baseIdx;

                if (0 <= idx && idx < SnOpts.Count)
                    break;

            } while (true);

            return SnOpts[idx];
        }

        private static int IndexOfBase(int i, string softwareName)
        {
            char c = softwareName[i % softwareName.Length];

            return SnOpts.IndexOf(c);
        }

    }
}
