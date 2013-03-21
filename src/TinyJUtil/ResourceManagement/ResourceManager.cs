using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace JUtil.ResourceManagement
{
    /// <summary>
    /// 集中管理內嵌資源
    /// </summary>
    public static class ResourceManager
    {
        private static Hashtable resourcesLoaders;

        static ResourceManager()
        {
            resourcesLoaders = new Hashtable();
        }

        /// <summary>
        /// 取得指定路徑的指定內嵌資源
        /// </summary>
        /// <param name="assemblyName">assembly路徑</param>
        /// <param name="resourceName">內嵌資源名稱</param>
        public static Stream GetResource(string assemblyPath, string resourceName)
        {
            Stream retVal = null;

            ResourcesLoader rsLoader = GetRSLoader(assemblyPath);
            if(rsLoader != null){
                retVal = rsLoader[resourceName];
            }

            return retVal;
        }

        /// <summary>
        /// 取得指定路徑的ResourcesLoader
        /// </summary>
        /// <param name="assemblyName">assembly路徑</param>
        public static ResourcesLoader GetRSLoader(string assemblyPath)
        {
            ResourcesLoader rsLoader = resourcesLoaders[assemblyPath] as ResourcesLoader;
            if (rsLoader == null)
            {
                rsLoader = new ResourcesLoader(assemblyPath);
                resourcesLoaders.Add(assemblyPath, rsLoader);
            }

            return rsLoader;
        }

        /// <summary>
        /// 取得目前assembly路徑的ResourcesLoader
        /// </summary>
        public static ResourcesLoader LocalResource
        {
            get
            {
                MethodBase caller = (new StackTrace()).GetFrame(1).GetMethod();
                string assemblyName = caller.Module.FullyQualifiedName;

                return GetRSLoader(assemblyName);
            }
        }


    } // end of ResourceManager
}
