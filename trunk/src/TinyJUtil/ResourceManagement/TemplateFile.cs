using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Web.Hosting;
using JUtil;
using JUtil.ResourceManagement;
using System.Security.Permissions;
using System.Reflection;
using System.Text;

namespace JUtil.ResourceManagement
{
    /// <summary>
    /// 提供方便存取Sql檔案的方式
    /// </summary>
    /// <remarks>
    /// 需設定ASPNET帳號，具備App_Data\Template的完全存取權限
    /// </remarks>
    public class TemplateFile
    {
        #region for CodeGenerator

        /// <summary>
        /// 取得reflectedType對應的tpl檔案內容
        /// </summary>
        public static string GetTemplate(Type reflectedType)
        {
            TemplateFile templateFile = GetTemplateFile(reflectedType, "tpl");

            return templateFile.getTemplate();
        }

        /// <summary>
        /// 取得reflectedType對應的tpl檔案內容中名稱為section的區塊
        /// </summary>
        public static string GetTemplate(Type reflectedType, string section)
        {
            TemplateFile templateFile = GetTemplateFile(reflectedType, "tpl");

            return templateFile.getTemplate(section);
        }

        #endregion

        /// <summary>
        /// 根據caller的類別的型別取得對應的tpl檔案內容
        /// </summary>
        public static string GetTemplate()
        {
            Type reflectedType = new StackTrace().GetFrame(1).GetMethod().ReflectedType;

            TemplateFile templateFile = GetTemplateFile(reflectedType, "tpl");

            return templateFile.getTemplate();
        }

        /// <summary>
        /// 根據caller的類別的型別取得對應的tpl檔案內容中名稱為section的區塊
        /// </summary>
        public static string GetTemplate(string section)
        {
            Type reflectedType = new StackTrace().GetFrame(1).GetMethod().ReflectedType;

            TemplateFile templateFile = GetTemplateFile(reflectedType, "tpl");

            return templateFile.getTemplate(section);
        }

        /// <summary>
        /// 取得reflectedType對應檔名且副檔名為extension的檔案內容
        /// </summary>
        private static TemplateFile GetTemplateFile(Type reflectedType, string extension)
        {
            TemplateFile tplFile = null;

            string callerName = reflectedType.Name;
            string fileName = string.Format("{0}.{1}", callerName, extension);

            if (isWebApp)
            {
                // 使用絕對路徑取得對應Template檔案
                //
                // 必須設定以下步驟
                // 假設呼叫TemplateFile.GetTemplate的類別為MyClass
                // 將MyClass.tpl加入tplFileDirectory所在資料夾(App_Data/Template/)
                //
                string filepath = JUtil.Path.File.GetAbsolutePath(tplFileDirectory, fileName);

                if (!JUtil.Path.File.Exists(filepath))
                {
                    string error = string.Format("無法取得檔案({0})。", filepath);

                    throw new TemplateFileException(error);
                }

                tplFile = new TemplateFile(filepath);
            }
            else
            {
                // 使用內嵌資源中定義的對應Template檔案(security issue)
                //
                // 必須設定以下步驟
                // 假設呼叫TemplateFile.GetTemplate的類別為MyClass
                // (1) 將MyClass.tpl加入專案
                // (2) 將MyClass.tpl設定為內嵌資源
                //
                //Stream stream = ResourceManager.LocalResource[fileName];
                string assemblyPath = reflectedType.Assembly.Location;
                Stream stream = ResourceManager.GetResource(assemblyPath, fileName);
                if (stream == null)
                {
                    string error = string.Format("無法取得你要得內嵌資源({0})，若以包涵此檔案，請將此檔案的建置動作設定成內嵌資源。", fileName);

                    throw new TemplateFileException(error);
                }

                tplFile = new TemplateFile(fileName, stream);
            }

            return tplFile;
        }

        #region Constructors

        static TemplateFile()
        {
            isWebApp = JUtil.Environment.ApplicationType == ApplicationType.WebApplication;

            if (isWebApp)
            {
                tplFileDirectory = HostingEnvironment.MapPath(tplFileVirtualDirectory);
            }
        }

        /// <summary>
        /// 用於WebApplication的建構子
        /// </summary>
        /// 
        private TemplateFile(string filepath)
        {
            Debug.Assert(isWebApp == true);

            fileName = System.IO.Path.GetFileName(filepath);

            Encoding encoding = System.Text.Encoding.Default;

            using (StreamReader streamReader = new StreamReader(filepath, encoding))
            {
                readWholeContent(streamReader);
            }

            using (StreamReader streamReader = new StreamReader(filepath, encoding))
            {
                parseTplFile(streamReader);
            }
        }

        /// <summary>
        /// 用於所有非WebApplication的建構子(security issue)
        /// </summary>
        private TemplateFile(string fileName, Stream stream)
        {
            Debug.Assert(isWebApp == false);

            this.fileName = fileName;

            // **************************************************
            // 對於Window程式(WinForm or WPF)，stream 已經由 ResourceManager統一管理
            // 因此請勿使用 using (StreamReader sr = new StreamReader(stream))
            // 來管理StreamReader
            // **************************************************
            if (stream == null)
            {
                string error = string.Format("stream can not be null, please make sure {0} exists and it is belong to embedded resource.", fileName);
                throw new SqlFileException(error);
            }

            stream.Seek(0, SeekOrigin.Begin);
            readWholeContent(new StreamReader(stream));

            stream.Seek(0, SeekOrigin.Begin);
            parseTplFile(new StreamReader(stream));
        }

        #endregion

        #region Fields

        private string wholeContent;
        private Hashtable blocks = new Hashtable();
        private string fileName;

        private const string tplFileVirtualDirectory = @"~/App_Data/Template";
        private static string tplFileDirectory;
        private static bool isWebApp;

        #endregion

        #region PrivateMethods

        #region readWholeContent
        private void readWholeContent(StreamReader streamReader)
        {
            wholeContent = streamReader.ReadToEnd();
        }
        #endregion

        #region parseTplFile
        private void parseTplFile(StreamReader streamReader)
        {
            blocks.Clear();

            string line = string.Empty;
            string section = string.Empty;
            string block = string.Empty;

            while ((line = streamReader.ReadLine()) != null)
            {
                if (isSection(line))
                {
                    if (!string.IsNullOrEmpty(section))
                    {
                        // push previous (section, block) pair into blocks
                        try
                        {
                            blocks.Add(section, block);
                        }
                        catch (Exception ex)
                        {
                            // catch exception that raised from duplicated section name
                            string error = string.Format("Maybe I had defined duplicated sections.({0})", ex.Message);
                            Log.E(new Exception(error));
                            throw;
                        }
                    }

                    // keep current section, and clear previous block content
                    section = trimSection(line);
                    block = string.Empty;
                    continue;
                }

                // if there is still no section defined yet, 
                // ignore current line
                if (string.IsNullOrEmpty(section))
                    continue;

                if (isComment(line))
                    continue;

                block += string.Format("{0}\n", line);
            }

            // handle EOF case
            if (!string.IsNullOrEmpty(section))
            {
                try
                {
                    blocks.Add(section, block);
                }
                catch (Exception ex)
                {
                    // catch exception that raised from duplicated section name
                    string error = string.Format("Maybe I had defined duplicated sections.({0})", ex.Message);
                    Log.E(new Exception(error));
                    throw;
                }
            }
        }
        #endregion

        #region isSection
        private bool isSection(string line)
        {
            // sql 是否包涵中括號
            // e.g. select * from [Plan] <-- 都是SqlServer惹得禍
            //
            // 如果 bSqlHasParenthesis = true，必須確保每一個Section的
            // 左括號都在每一行的第一個字元，否則會判斷錯誤
            //
            // 如果 bSqlHasParenthesis = false，就沒有這個問題
            //
            bool bSqlHasParenthesis = true;

            line = trimComment(line);

            line = bSqlHasParenthesis ? line.TrimEnd() : line.Trim();

            bool bSection = line.StartsWith("[") && line.EndsWith("]");

            if (!bSection
               && bSqlHasParenthesis
               && line.Contains("[")
               && line.Contains("]"))
            {
                Log.W("字串[ {0} ]判斷並非Section，若該字串非Section，請忽略此訊息; 若該字串為Section，請確認此字串的左括號是在該行的第一個字元，可能因為此原因誤判。", line);
            }

            return bSection;
        }
        #endregion

        #region isComment
        private bool isComment(string line)
        {
            int pos = line.IndexOf("--");

            return pos == 0;
        }
        #endregion

        #region trimComment
        private string trimComment(string line)
        {
            int pos = line.IndexOf("--");
            if (pos != -1)
            {
                line = line.Substring(0, pos);
            }
            return line;
        }
        #endregion

        #region trimSection
        private string trimSection(string section)
        {
            section = trimComment(section);

            section = section.TrimEnd();

            section = section.Replace("[", "").Replace("]", "");

            return section;
        }
        #endregion

        #endregion

        #region GetTemplate

        /// <summary>
        /// 取得Template檔對應的block內容
        /// </summary>
        private string getTemplate(string section)
        {
            object block = blocks[section];

            if (block == null)
            {
                string error = string.Format("cannot find block named {0} in file : {1}.", section, fileName);

                throw new TemplateFileException(error);
            }

            return block.ToString();
        }

        /// <summary>
        /// 取得整個Template檔內容
        /// </summary>
        private string getTemplate()
        { 
            return wholeContent; 
        }
        #endregion


    } // end of TemplateFile
}

class TemplateFileException : Exception
{
    public TemplateFileException(string message)
        : base(message)
    {
    }
}