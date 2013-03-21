<%@ WebService Language="C#" Class="AspNetAjaxInAction.GeocodeService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;

namespace AspNetAjaxInAction
{
    [ScriptService]
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GeocodeService : System.Web.Services.WebService
    {

        [WebMethod]
        public Location GetLocationData(string street,
                                        string zip,
                                        string city,
                                        string state,
                                        string country)
        {
            return YahooProvider.GetLocationData(street, zip, city, state, country);
        }
    }

}
