using System.Web.Script.Serialization;

namespace JUtil.Web
{
    public class JsonConverter
    {
        public static T Deserialize<T>(string jsonStr)
        {
            // [deserialize json string]
            // http://weblogs.asp.net/hajan/archive/2010/07/23/javascriptserializer-dictionary-to-json-serialization-and-deserialization.aspx
            // [deserialize json string to List(Of OrderDictionary)]
            // http://stackoverflow.com/questions/402996/deserializing-json-objects-as-listtype-not-working-with-asmx-service

            T Result = default(T);

            JavaScriptSerializer deserializer = new JavaScriptSerializer();
            Result = deserializer.Deserialize<T>(jsonStr);

            return Result;
        }

        public static string Serialize(object obj)
        {
            string Result = null;

            JavaScriptSerializer serializer = new JavaScriptSerializer();
            Result = serializer.Serialize(obj);

            return Result;
        }


    } // end of JsonConverter
}
