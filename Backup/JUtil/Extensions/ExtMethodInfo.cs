using System.Reflection;

namespace JUtil.Extensions
{
    //
    // Extension System.Windows.Forms.ComboBox
    //
    public static class ExtMethodInfo
    {
        public static bool IsReturnVoid(this MethodInfo source)
        {
            return source.ReturnType.Name == "Void";
        }


    } // end of ExtMethodInfo
}
