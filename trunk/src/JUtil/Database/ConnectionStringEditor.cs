using System;
using System.Collections;
using System.ComponentModel;
using System.Data;


namespace JUtil
{
    /// <summary>
    /// The ConnectionString Editor
    /// </summary>
    /// <remarks>
    /// reference : http://www.codeproject.com/KB/database/DataLinks.aspx
    /// </remarks>
    public class ConnectionStringEditor
    {
        /// <summary>
        /// Get ConnectionString
        /// </summary>
        public string GetConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                return GetConnectionString();

            /* 
              Reference DataLinks
               NOTE: Reference 
                    C:\Program Files\Common Files\System\Ole DB\OLEDB32.DLL
                    (Was MSDASC.dll) 
               SEE:
                    http://support.microsoft.com:80/support/kb/articles/Q225/1/32.asp
             */
            MSDASC.DataLinks dataLinks = new MSDASC.DataLinksClass();
            //note that a reference to: 
            //  c:\Program Files\Microsoft.NET\Primary Interop Assemblies\adodb.dll
            //is also required to read the ADODB._Connection result
            ADODB._Connection connection;
            //are we editing an existing connect string or getting a new one?
            // edit connection string
            connection = new ADODB.ConnectionClass();
            connection.ConnectionString = connectionString;
            //set local COM compatible data type
            object oConnection = connection;
            //prompt user to edit the given connect string
            if ((bool)dataLinks.PromptEdit(ref oConnection))
            {
                return connection.ConnectionString;
            }
            return string.Empty;
        }

        /// <summary>
        /// Get ConnectionString
        /// </summary>
        public string GetConnectionString()
        {
            /* 
              Reference DataLinks
               NOTE: Reference 
                    C:\Program Files\Common Files\System\Ole DB\OLEDB32.DLL
                    (Was MSDASC.dll) 
               SEE:
                    http://support.microsoft.com:80/support/kb/articles/Q225/1/32.asp
             */
            MSDASC.DataLinks dataLinks = new MSDASC.DataLinksClass();
            //note that a reference to: 
            //  c:\Program Files\Microsoft.NET\Primary Interop Assemblies\adodb.dll
            //is also required to read the ADODB._Connection result
            ADODB._Connection connection;

            // get a new connection string
            //Prompt user for new connect string
            connection = (ADODB._Connection)dataLinks.PromptNew();
            //read result
            return connection.ConnectionString.ToString();
        }
    }
}
