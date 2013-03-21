<%@ WebService Language="C#" Class="AspNetAjaxInAction.StarbucksService" %>

using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Script.Services;
using System.Collections.Generic;

namespace AspNetAjaxInAction
{      
  [ScriptService]
  [GenerateScriptType(typeof(Employee))]
  [WebService(Namespace = "http://aspnetajaxinaction.com/")]
  [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
  public class StarbucksService : System.Web.Services.WebService
  {     

    // Demo purposes only, try to avoid using HTTP GET for security 
    // concerns with JSON hijacking.
    [ScriptMethod(UseHttpGet=true)]
    [WebMethod]
    public List<Beverage> GetDeals()
    {
        System.Threading.Thread.Sleep(3000);
    
      List<Beverage> beverages = new List<Beverage>();

      Beverage b1 = new Beverage("House Blend",
                            "Our most popular coffee",
                            2.49);
      
      Beverage b2 = new Beverage("French Roast",
                            "Dark, bold flavor",
                            2.99);

      beverages.Add(b1);
      beverages.Add(b2);
      return beverages;
    }
      
      
    [WebMethod]
    public int GetLocationCount(int zipCode)
    {
      int locations = 0;
      switch (zipCode)
      {
        case 92618:
         locations = 148;
         break;
                            
        case 90623:
         locations = 3;
         break;
                
        case 90017:
         locations = 29;
         break;                
            
        default:
         break;
      }

      return locations;
    }

  }
}

