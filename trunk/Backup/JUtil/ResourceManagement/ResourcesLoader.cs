using System;
using System.Collections;
using System.IO;
using System.Reflection;

namespace JUtil.ResourceManagement
{
    public sealed class ResourcesLoader
    {
        private Hashtable resources = new Hashtable();

        private readonly string fileName;

        public ResourcesLoader(string fileName)
        {
            this.fileName = fileName;

            Initialize();
        }

        public ResourcesLoader()
            :this(System.Reflection.Assembly.GetExecutingAssembly().Location)
        {
        }

        public Stream this[string resourceName]
        {
            get
            {
                return (Stream)resources[resourceName];
            }
        }

        private void Initialize()
        {
            string extension = System.IO.Path.GetExtension(fileName).ToLower();
            switch (extension)
            {
                case ".exe":
                case ".dll":
                    LoadResourcesFromAssemblyFile(fileName);
                    break;

                default:
                    throw new ResourcesLoaderException("Unknown file format");
            }
        }

        private void LoadResourcesFromAssemblyFile(string fileName)
        {
            resources.Clear();

            Assembly assem = Assembly.LoadFrom(fileName);
            foreach (string resourceName in assem.GetManifestResourceNames())
            {
                Stream stream = assem.GetManifestResourceStream(resourceName);

                //resources.Add(resourceName, stream);

                char delimiter = '.';
                int pos = resourceName.LastIndexOf(delimiter);
                pos = resourceName.LastIndexOf(delimiter, pos - 1);
                string resourceFile = resourceName.Substring(pos + 1);
                resources.Add(resourceFile, stream);
            }
        }


    } // end of ResourcesLoader



    public class ResourcesLoaderException : Exception
    {
        public ResourcesLoaderException(string message)
            : base(message)
        {
        }
    
    
    } // end of ResourcesLoaderException
}


