using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace JUtil.ResourceManagement
{
    /// <summary>
    /// 指定解析resourceId的方式
    /// </summary>
    public enum ResourceIdPolicy
    {
        /// <summary>
        /// 取得完整的ResourceName
        /// </summary>
        FullResourceName = 0,

        /// <summary>
        /// 只取檔名
        /// </summary>
        FileName = 1
    }

    /// <summary>
    /// 取得指定路徑的內嵌資源
    /// </summary>
    public sealed class ResourcesLoader
    {
        private static string LookupResourceName(Assembly assembly, string resourceFilename)
        {
            string[] ResourceNames = assembly.GetManifestResourceNames();

            foreach (string resourceName in ResourceNames)
            {
                if (resourceName.Contains(resourceFilename))
                    return resourceName;
            }

            string error = string.Format("Cannot find any embedded resource named '{0}' in {1}", resourceFilename, assembly.FullName);
            throw new Exception(error);
        }

        public static string LookupResourceId(Assembly assembly, string resourceFilename)
        {
            string resourceName = LookupResourceName(assembly, resourceFilename);

            return GetResourceId(resourceName);
        }

        private static string GetResourceId(string resourceName)
        {
            string resourceId = string.Empty;

            switch (resourceIdPolicy)
            {
                case ResourceIdPolicy.FullResourceName:
                    {
                        resourceId = resourceName;

                        break;
                    }

                case ResourceIdPolicy.FileName:
                    {
                        char delimiter = '.';
                        int pos = resourceName.LastIndexOf(delimiter);
                        pos = resourceName.LastIndexOf(delimiter, pos - 1);
                        resourceId = resourceName.Substring(pos + 1);

                        break;
                    }
            }

            return resourceId;
        }

        private Hashtable resources = new Hashtable();

        private readonly string filePath;

        /// <summary>
        /// 解析resourceId的方式目前尚未設定對外介面
        /// </summary>
        private static ResourceIdPolicy resourceIdPolicy;

        static ResourcesLoader()
        {
            bool isWebApp = JUtil.Environment.ApplicationType == ApplicationType.WebApplication;

            resourceIdPolicy = isWebApp ? ResourceIdPolicy.FullResourceName : ResourceIdPolicy.FileName;
        }

        #region ctor
        
        /// <summary>
        /// 根據指定路徑，取得內嵌資源
        /// </summary>
        /// <param name="filePath">assembly路徑</param>
        public ResourcesLoader(string filePath)
        {
            this.filePath = filePath;

            Initialize();
        }

        /// <summary>
        /// 載入目前assembly路徑的內嵌資源
        /// </summary>
        public ResourcesLoader()
            :this(System.Reflection.Assembly.GetExecutingAssembly().Location)
        {
        }

        #endregion

        /// <summary>
        /// 根據resourceId取得對應的內嵌資源
        /// </summary>
        /// <remarks>
        /// resourceId是根據ResourceIdPolicy決定解析出來的
        /// </remarks>
        public Stream this[string resourceId]
        {
            get
            {
                Stream stream = (Stream)resources[resourceId];

                // 修正成找不到resourceId對應的Stream，就拋出例外
                if (stream == null)
                {
                    string error = string.Empty;
                    error += string.Format("找不到{0}對應的資源，請確定資源已經設定成內嵌資源。", resourceId);
                    error += string.Format("若已設定為內嵌資源，仍然找不到;");
                    error += string.Format("若為WebApplication請使用FullResourceName作為resourceId，其他請使用FileName作為resourceId。");
                    throw new Exception(error);
                }

                return (Stream)stream;
            }
        }

        private void Initialize()
        {
            string extension = System.IO.Path.GetExtension(filePath).ToLower();
            switch (extension)
            {
                case ".exe":
                case ".dll":
                    LoadResourcesFromAssemblyFile(filePath);
                    break;

                default:
                    {
                        string error = string.Format("無法解析副檔名為 {0 }的內嵌資源", extension);
                        throw new ResourcesLoaderException(error);
                    }
            }
        }

        private void LoadResourcesFromAssemblyFile(string filePath)
        {
            resources.Clear();

            Assembly assem = Assembly.LoadFrom(filePath);
            foreach (string resourceName in assem.GetManifestResourceNames())
            {
                string resourceID = GetResourceId(resourceName);

                Stream stream = assem.GetManifestResourceStream(resourceName);

                resources.Add(resourceID, stream);
            }
        }

        


    } // end of ResourcesLoader


    /// <summary>
    /// ResourcesLoader專用Exception
    /// </summary>
    public class ResourcesLoaderException : Exception
    {
        /// <summary>
        /// ctor of ResourcesLoaderException
        /// </summary>
        /// <param name="message"></param>
        public ResourcesLoaderException(string message)
            : base(message)
        {
        }
    
    
    } // end of ResourcesLoaderException
}


