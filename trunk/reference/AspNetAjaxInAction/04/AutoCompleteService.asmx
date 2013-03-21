<%@ WebService Language="C#" Class="AutoCompleteService" %>

using System;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.IO;
using System.Web.Services.Protocols;
using System.Web.Script.Services;


[ScriptService]
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class AutoCompleteService : System.Web.Services.WebService {

    private static string[] autoCompleteList = null;

    [ScriptMethod]
    [WebMethod]
    public string[] GetArtists(string prefixText, int count)
    {
        if (autoCompleteList == null)
        {
            string[] temp = File.ReadAllLines(Server.MapPath("~/App_Data/Artists.txt"));
            Array.Sort(temp, new CaseInsensitiveComparer());
            autoCompleteList = temp;
        }

        int index = Array.BinarySearch(autoCompleteList, prefixText, new CaseInsensitiveComparer());
        if (index < 0)
        {
            index = ~index;
        }

        int matchingCount;
        for (matchingCount = 0;
             matchingCount < count && index + matchingCount <
             autoCompleteList.Length;
             matchingCount++)
        {
            if (!autoCompleteList[index +
              matchingCount].StartsWith(prefixText,
              StringComparison.CurrentCultureIgnoreCase))
            {
                break;
            }
        }

        String[] returnValue = new string[matchingCount];
        if (matchingCount > 0)
        {
            Array.Copy(autoCompleteList, index, returnValue, 0,
              matchingCount);
        }
        return returnValue;

    }    
    
}