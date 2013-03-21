using System;
using System.Web;
using System.Net;
using System.Xml;
using System.Globalization;

namespace AspNetAjaxInAction
{
 public class YahooProvider
 {
   private readonly static string apiKey = "YD-xeRMEJo_JX3b3GYHNVfy7N0Xow0Uuko";
   private readonly static string geocodeUriFormat = "http://local.yahooapis.com/MapsService/V1/geocode?appid={0}-&street={1}&zip={2}&city={3}&state={4}";
        
   public static Location GetLocationData(string street,
                                          string zip,
                                          string city,
                                          string state,
                                          string country)
   {
     // Use an invariant culture for formatting numbers.
     NumberFormatInfo numberFormat = new NumberFormatInfo();
     Location loc = new Location();
     XmlTextReader xmlReader = null;

     try
     {
         HttpWebRequest webRequest = GetWebRequest(street, zip, city, state);
         HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();

         using (xmlReader = new XmlTextReader(response.GetResponseStream()))
         {
             while (xmlReader.Read())
             {
                 if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Result")
                 {
                     XmlReader resultReader = xmlReader.ReadSubtree();
                     while (resultReader.Read())
                     {
                         if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Latitude")
                             loc.Latitude = Convert.ToDouble(xmlReader.ReadInnerXml(), numberFormat);

                         if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "Longitude")
                         {
                             loc.Longitude = Convert.ToDouble(xmlReader.ReadInnerXml(), numberFormat);
                             break;
                         }
                     }
                 }
             }
         }
     }
     finally
     {
       if (xmlReader != null)
        xmlReader.Close();                
     }

      // Return the location data.
      return loc;
    }

    private static HttpWebRequest GetWebRequest(string street, string zip, string city, string state)
    {
      string formattedUri = String.Format(geocodeUriFormat, apiKey, street, zip, city, state);
      Uri serviceUri = new Uri(formattedUri, UriKind.Absolute);
      return (HttpWebRequest)System.Net.WebRequest.Create(serviceUri);
    }
 }
}