<%@ WebService Language="C#" Class="DateTimeService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[ScriptService]
public class DateTimeService  : System.Web.Services.WebService {

    [WebMethod]
    [ScriptMethod(UseHttpGet=true)]
    public string GetTimeAsString() {
        return DateTime.Now.ToString();
    }
}

