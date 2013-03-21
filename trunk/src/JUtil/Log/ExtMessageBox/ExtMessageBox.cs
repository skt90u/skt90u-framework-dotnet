using JUtil.Extensions;
using System;

namespace JUtil
{
    /// <summary>
    /// Extensions of MessageBox
    /// </summary>
    public static class ExtMessageBox
    {
        private static IExtMessageBox instance;
        /// <summary>
        /// instance of IExtMessageBox
        /// </summary>
        public static IExtMessageBox Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = getInstance();
                }
                return instance; 
            }

            set 
            { 
                instance = value; 
            }
        }

        /// <summary>
        /// 讓Client端能夠重新設定MessageBox
        /// </summary>
        public static void RegisterMessageBox(IExtMessageBox MessageBox)
        {
            Instance = MessageBox;
        }

        private static IExtMessageBox getInstance()
        {
            string error = string.Empty;
            string msgBxTypeName = string.Empty;

            ApplicationType applicationType = JUtil.Environment.ApplicationType;
            switch (applicationType)
            {
                case ApplicationType.ConsoleApplication:
                    {
                        msgBxTypeName = "JUtil.ConsoleMessageBox";
                        break;
                    }
                
                case ApplicationType.WindowsFormsApplication:
                    {
                        msgBxTypeName = "JUtil.WinFormMessageBox";
                        break;
                    }

                case ApplicationType.WpfApplication:
                    {
                        msgBxTypeName = "JUtil.WpfMessageBox";
                        break;
                    }

                case ApplicationType.WebApplication:
                    {
                        msgBxTypeName = "JUtil.WebMessageBox";
                        break;
                    }
                
                default:
                    {
                        error = string.Format("{0}尚未實做對應的IExtMessageBox", applicationType.ToString());
                        throw new Exception(error);
                    }
            }

            Type typeMsgBx = ExtType.GetType(msgBxTypeName);
            if (null == typeMsgBx)
            {
                error = string.Format("從載入的assembly中無法取得名為{0}的Type", msgBxTypeName);
                throw new Exception(error);
            }

            return (IExtMessageBox)Activator.CreateInstance(typeMsgBx);
        }

        /// <summary>
        /// show Error MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public static void Error(string text, string caption)
        {
            Instance.Error(text, caption);
        }

        /// <summary>
        /// show Info MessageBox
        /// </summary>
        /// <param name="text"></param>
        /// <param name="caption"></param>
        public static void Info(string text, string caption)
        {
            Instance.Info(text, caption);
        }


    } // end of ExtMessageBox
}