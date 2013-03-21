<%@ WebService Language="C#" Class="HRService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

[ScriptService]
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class HRService  : System.Web.Services.WebService {

    [ScriptMethod]
    [WebMethod]
    public int GetEmployeeCount(string department)
    {
        //System.Threading.Thread.Sleep(2000);
        return HumanResources.GetEmployeeCount(department);
    }
        
}

