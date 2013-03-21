using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace JUtil.Reflection
{
    /// <summary>
    /// assembly版本資訊
    /// </summary>
    public class AssemblyAccessor
    {
        private Assembly _assembly;

        /// <summary>
        /// 根據assembly路徑取得assembly版本資訊
        /// </summary>
        /// <param name="assemblyName"></param>
        public AssemblyAccessor(String assemblyName)
        {
            if (assemblyName == null)
                _assembly = Assembly.GetEntryAssembly();
            else
            {
                String absolutePath = System.IO.Path.GetFullPath(assemblyName);
                if (!File.Exists(absolutePath))
                {
                    string error = string.Format("無法讀取assembly版本資訊，找不到{0}", absolutePath);
                    throw new FileNotFoundException(error);
                }

                _assembly = Assembly.LoadFile(absolutePath);
            }
        }

        #region Assembly Attribute Accessors

        /// <summary>
        /// get Title description of an assembly
        /// </summary>
        public string AssemblyTitle
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (!string.IsNullOrEmpty(titleAttribute.Title))
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        /// <summary>
        /// get Version description of an assembly
        /// </summary>
        public string AssemblyVersion
        {
            get
            {
                return _assembly.GetName().Version.ToString();
            }
        }

        public string FileVersion
        {
            get
            {
                FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(_assembly.Location);
                return fvi.ProductVersion;
            }
        }

        /// <summary>
        /// get description of an assembly
        /// </summary>
        public string AssemblyDescription
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        /// <summary>
        /// get Product description of an assembly
        /// </summary>
        public string AssemblyProduct
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        /// <summary>
        /// get Copyright description of an assembly
        /// </summary>
        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        /// <summary>
        /// get Company description of an assembly
        /// </summary>
        public string AssemblyCompany
        {
            get
            {
                object[] attributes = _assembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return string.Empty;
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion


    } // end of AssemblyAccessor
}
