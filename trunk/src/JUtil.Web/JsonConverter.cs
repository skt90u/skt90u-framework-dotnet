using System.Web.Script.Serialization;

namespace JUtil.Web
{
    /// <summary>
    /// Serialization helper class
    /// </summary>
    public class JsonConverter
    {
        /// <summary>
        /// convert string to specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jsonStr"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string jsonStr)
        {
            // [deserialize json string]
            // http://weblogs.asp.net/hajan/archive/2010/07/23/javascriptserializer-dictionary-to-json-serialization-and-deserialization.aspx
            // [deserialize json string to List(Of OrderDictionary)]
            // http://stackoverflow.com/questions/402996/deserializing-json-objects-as-listtype-not-working-with-asmx-service

            JavaScriptSerializer deserializer = new JavaScriptSerializer();

            return deserializer.Deserialize<T>(jsonStr);
        }

        /// <summary>
        /// convert object to string
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string Serialize(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(obj);
        }


    } // end of JsonConverter
}
