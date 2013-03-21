using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Web.Hosting;
using JUtil;
using JUtil.ResourceManagement;
using System.Security.Permissions;

namespace JUtil.ResourceManagement
{
    /// <summary>
    /// 提供方便存取Sql檔案的方式
    /// </summary>
    public class SqlFile
    {
        #region for LookupBase
        /// <summary>
        /// GetSQL
        /// </summary>
        public static string GetSQL(Type reflectedType, string section, params object[] args)
        {
            SqlFile sqlFile = GetSqlFile(reflectedType);

            return sqlFile.getSQL(section, args);
        }
        #endregion

        #region GetSQL

        /// <summary>
        /// GetSQL
        /// </summary>
        public static string GetSQL(string section, params object[] args)
        {
            Type reflectedType = new StackTrace().GetFrame(1).GetMethod().ReflectedType;

            SqlFile sqlFile = GetSqlFile(reflectedType);

            return sqlFile.getSQL(section, args);
        }

        #endregion

        #region Fields

        private Hashtable blocks = new Hashtable();
        private string fileName;

        private const string sqlFileVirtualDirectory = @"~/App_Data/SQL";
        private static string sqlFileDirectory;
        private static bool isWebApp;

        #endregion

        #region Constructors

        static SqlFile()
        {
            isWebApp = JUtil.Environment.ApplicationType == ApplicationType.WebApplication;

            if (isWebApp)
            {
                sqlFileDirectory = HostingEnvironment.MapPath(sqlFileVirtualDirectory);
            }
        }

        /// <summary>
        /// 目前用於WebApplication
        /// </summary>
        /// 
        private SqlFile(string filepath)
        {
            fileName = System.IO.Path.GetFileName(filepath);

            using (StreamReader streamReader = new StreamReader(filepath, System.Text.Encoding.Default))
            {
                parseSqlFile(streamReader);
            }
        }

        /// <summary>
        /// 所有非WebApplication，都使用此建構子(security issue)
        /// </summary>
        private SqlFile(string fileName, Stream stream)
        {
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

            parseSqlFile(new StreamReader(stream));
        }

        #endregion

        #region PrivateMethods

        #region parseSqlFile
        private void parseSqlFile(StreamReader streamReader)
        {
            if (streamReader == null)
            {
                string error = string.Format("stream can not be null, please make sure {0} exists and it is belong to embedded resource.", fileName);
                throw new SqlFileException(error);
            }

            blocks.Clear();

            string line = string.Empty;
            string section = string.Empty;
            string block = string.Empty;

            while ((line = streamReader.ReadLine()) != null)
            {
                if (isUseless(line)) continue; // ignore empty string

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

                block += trimSQL(line);
                block += " ";
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

        #region isUseless
        private bool isUseless(string line)
        {
            line = trimComment(line);

            line = line.Trim();

            return line.Length == 0;
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

        #region trimSQL
        private string trimSQL(string line)
        {
            line = trimComment(line);

            line = line.Trim();

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

        #region getSQL

        private string getSQL(string section)
        {
            object block = blocks[section];
            if (block == null)
            {
                string error = string.Format("Cannot find [{0}] in {1}", section, fileName);
                throw new SqlFileException(error);
            }
            return block.ToString();
        }

        /// <summary>
        /// getSQL
        /// </summary>
        public string getSQL(string section, params object[] args)
        {
            string sql = string.Empty;

            try
            {
                sql = getSQL(section);

                sql = (args == null || args.Length == 0) ? sql : string.Format(sql, args);
            }
            catch (FormatException ex)
            {
                string error = string.Format("已取得你要得SQL，但發生FormatException，此段SQL[ {0} ] 可能需要輸入對應的SQL參數。[ Exception : {1} ]", sql, ex.Message);
                throw new Exception(error);
            }

            return sql;
        }

        #endregion

        #region GetSqlFile
        private static SqlFile GetSqlFile(Type reflectedType)
        {
            SqlFile sqlFile = null;

            string callerName = reflectedType.Name;

            // 解決泛用型別的問題
            // NNN_DataGenerator`1 --> NNN_DataGenerator
            int pos = callerName.IndexOf('`');
            if(pos !=-1)
            {
                callerName = callerName.Substring(0, pos);
            }
            

            string fileName = string.Format("{0}.sql", callerName);

            if (isWebApp)
            {
                // 使用絕對路徑取得對應SQL檔案
                //
                // 必須設定以下步驟
                // 假設呼叫SqlFile.GetSQL的類別為MyClass
                // 將MyClass.sql加入sqlFileDirectory所在資料夾(App_Data/SQL/)
                //
                string filepath = JUtil.Path.File.GetAbsolutePath(sqlFileDirectory, fileName);

                if (!JUtil.Path.File.Exists(filepath))
                {
                    string error = string.Format("無法取得檔案({0})。", filepath);

                    throw new SqlFileException(error);
                }

                sqlFile = new SqlFile(filepath);
            }
            else
            {
                // 使用內嵌資源中定義的對應SQL檔案(security issue)
                //
                // 必須設定以下步驟
                // 假設呼叫SqlFile.GetSQL的類別為MyClass
                // (1) 將MyClass.sql加入專案
                // (2) 將MyClass.sql設定為內嵌資源
                //
                string assemblyPath = reflectedType.Assembly.Location;
                Stream stream = ResourceManager.GetResource(assemblyPath, fileName);
                if (stream == null)
                {
                    string error = string.Format("無法取得你要得內嵌資源({0})，若以包涵此檔案，請將此檔案的建置動作設定成內嵌資源。", fileName);

                    throw new SqlFileException(error);
                }

                sqlFile = new SqlFile(fileName, stream);
            }

            return sqlFile;
        }
        #endregion

        #endregion


    } // end of SqlFile

    class SqlFileException : Exception
    {
        public SqlFileException(string message)
            : base(message)
        {
        }
    }
}

