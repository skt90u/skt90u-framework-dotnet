using System;
using System.Collections;
using System.Text;
using JUtil.Extensions;
using JUtil.ResourceManagement;

namespace JUtil.CodeGenerator
{
    public abstract class CodeGenBase
    {
        #region MustOverride
        
        protected abstract void AssignExportTemplate(Hashtable Variables);

        protected abstract string GetExportTemplate();

        #endregion

        #region PublicMethods
        #region Export
        public void Export(string filepath)
        {
            JUtil.Path.File.Delete(filepath);

            string output = Export();

            output.SaveAs(filepath);
        }

        public string Export()
        {
            Hashtable ExportTemplateVariables = new Hashtable();

            AssignExportTemplate(ExportTemplateVariables);

            string block = GetExportTemplate();

            string output = ReplaceBlock(block, ExportTemplateVariables);

            return output;
        }
        #endregion
        #endregion

        #region ProtectedMethods
        #region GetTemplate

        protected string GetTemplate()
        {
            Type reflectedType = GetType();

            return TemplateFile.GetTemplate(reflectedType);
        }

        protected string GetTemplate(string section)
        {
            Type reflectedType = GetType();

            return TemplateFile.GetTemplate(reflectedType, section);
        }

        #endregion

        #region ReplaceBlock
        protected string ReplaceBlock(string block, Hashtable variables)
        {
            string[] lines = SplitString(block);

            StringBuilder outcome = new StringBuilder();

            foreach (string line in lines)
            {
                ReplaceBlockLine(outcome, line, variables);
            }

            return outcome.ToString();
        }
        #endregion
        #endregion

        #region PrivateMethods

        #region SplitString
        private string[] SplitString(string str)
        {
            string[] result = str.Split(new char[] { '\n' }, StringSplitOptions.None);

            if (str.LastIndexOf('\n') == str.Length - 1)
            {
                string[] newResult = new string[result.Length - 1];

                for (int i = 0; i < result.Length - 1; i++)
                {
                    newResult[i] = result[i];
                }
                return newResult;
            }
            else
            {
                return result;
            }
        }
        #endregion

        #region ReplaceBlockLine
        private void ReplaceBlockLine(StringBuilder outcome, string line, Hashtable variables)
        {
            string indent = GetIndent(line);

            string result = line;

            foreach (object objKey in variables.Keys)
            {
                string key = string.Format("${0}$", objKey.ToString());

                if (!result.Contains(key))
                    continue;

                string variable = Convert.ToString(variables[objKey]);

                variable = AppendIndent(indent, variable);

                result = result.Replace(key, variable);
            }

            outcome.AppendFormat("{0}\n", result);
        }
        #endregion

        #region AppendIndent
        private string AppendIndent(string indent, string variable)
        {
            StringBuilder outputBuilder = new StringBuilder();

            string[] lines = SplitString(variable);

            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];

                string currentline = string.Empty;

                currentline = i==0 ? line : indent + line;

                string format = (i != lines.Length - 1) ? "{0}\n" : "{0}";

                outputBuilder.AppendFormat(format, currentline);
            }

            return outputBuilder.ToString();
        }
        #endregion

        #region GetIndent
        private string GetIndent(string line)
        {
            string indent = string.Empty;

            foreach (char c in line)
            {
                if (IsMatchIndentPattern(c))
                    indent += c;
                else
                    break;
            }

            return indent;
        }
        #endregion

        #region IsMatchIndentPattern
        private bool IsMatchIndentPattern(char c)
        {
            char[] patterns = 
            {
                ' ',
                '\t',
            };

            foreach (char pattern in patterns)
            {
                if (pattern == c)
                    return true;
            }

            return false;
        }
        #endregion

        #endregion


    } // end of CodeGenerator
}
