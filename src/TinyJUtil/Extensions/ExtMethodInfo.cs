using System.Reflection;

namespace JUtil.Extensions
{
    /// <summary>
    /// Extensions of MethodInfo
    /// </summary>
    public static class ExtMethodInfo
    {
        /// <summary>
        /// determine whether a function return void
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool IsReturnVoid(this MethodInfo source)
        {
            return source.ReturnType.Name == "Void";
        }


    } // end of ExtMethodInfo
}
