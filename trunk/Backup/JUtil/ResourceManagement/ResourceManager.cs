using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using System.IO;

namespace JUtil.ResourceManagement
{
    public class ResourceManager
    {
        private static Hashtable resourcesLoaders = new Hashtable();

        public static Stream GetResource(string assemblyName, string resourceName)
        {
            Stream retVal = null;

            ResourcesLoader rsLoader = GetRSLoader( assemblyName);
            if(rsLoader != null){
                retVal = rsLoader[resourceName];
            }

            return retVal;
        }

        public static ResourcesLoader GetRSLoader(string assemblyName)
        {
            ResourcesLoader rsLoader = resourcesLoaders[assemblyName] as ResourcesLoader;
            if (rsLoader == null)
            {
                rsLoader = new ResourcesLoader(assemblyName);
                resourcesLoaders.Add(assemblyName, rsLoader);
            }

            return rsLoader;
        }

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
