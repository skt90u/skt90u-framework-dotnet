using System;
using System.Collections.Generic;
using JUtil.Extensions;

namespace JUtil.CodeGenerator
{
    /// <summary>
    /// DalGenerator
    /// </summary>
    public class DalGenerator
    {
        /// <summary>
        /// DalGenerator
        /// </summary>
        public DalGenerator(IDatabase database,
                            string sNamespace,
                            string sClassName,
                            string selectSQL)
        {
            this.sNamespace = sNamespace;
            this.sClassName = sClassName;

            TableSchema ts = TableSchema.GetBySql(database, selectSQL);

            lstFieldSchema = new List<FieldSchema>();

            foreach (FieldSchema fs in ts.Fields)
            {
                lstFieldSchema.Add(fs);
            }
        }

        List<FieldSchema> lstFieldSchema;

        string indent = "\t";

        string sNamespace;
        string sClassName;

        /// <summary>
        /// Export
        /// </summary>
        public void Export(string path)
        {
            string sClass = createClass(lstFieldSchema);

            sClass.SaveAs(path);
        }

        string createProperty(FieldSchema fs)
        {
            string sProperty = string.Empty;

            sProperty += indent;
            sProperty += indent;

            string sType = fs.DataType.ToCSharpType();

            if (fs.DataType != typeof(string) && fs.AllowDBNull == true)
                sType += "?";

            //AllowDBNull

            sProperty += string.Format("public {0} {1} {{ get; set; }}", sType, fs.ColumnName);

            sProperty += "\n";

            return sProperty;
        }

        string createClass(List<FieldSchema> lstFieldSchema)
        {
            string sClass = string.Empty;

            sClass += string.Format("using System;\n");
            sClass += string.Format("\n");

            sClass += string.Format("namespace {0}\n", sNamespace);
            sClass += string.Format("{{\n");

            sClass += indent;
            sClass += string.Format("public class {0}\n", sClassName);

            sClass += indent;
            sClass += string.Format("{{\n");

            foreach (FieldSchema fs in lstFieldSchema)
            {
                try
                {
                    string sProperty = createProperty(fs);

                    sClass += sProperty;
                }
                catch (Exception ex)
                {
                    Log.ReportError(ex);
                }
            }

            sClass += indent;
            sClass += string.Format("}}\n");

            sClass += string.Format("}}\n");

            return sClass;
        }
    }
}
