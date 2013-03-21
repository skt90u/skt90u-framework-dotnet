using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUtil
{
    public class Product
    {
        private static List<string> softwareNameList = new List<string>();

        public static List<string> GetSoftwareNameList()
        {
            return softwareNameList ;
        }

        public static string GetDefaultSoftwareName()
        {
            return softwareNameList.Count > 0 ? softwareNameList[0] : string.Empty;
        }

        static Product()
        {
            softwareNameList.Add("DigAvControl");
        }
    }
}
