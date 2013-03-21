using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using JUtil;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.IO;

namespace JUtil.Extensions
{
    class ExtWindowsIdentity
    {
        /// <summary>
        /// 模擬特定使用者帳號
        /// 
        /// reference : http://www.dotblogs.com.tw/puma/archive/2009/02/24/7281.aspx
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns>表示模擬作業前的 Windows 使用者</returns>
        /// <remarks>
        /// Response.Write("CurrentUserName: " + WindowsIdentity.GetCurrent().Name);
        /// 
        /// 模擬使用者
        /// WindowsImpersonationContext wic = Impersonate("localhost", "Administrator", "AccountPass");
        /// Response.Write("CurrentUserName: " + WindowsIdentity.GetCurrent().Name);
        /// 
        /// 取消模擬
        /// wic.Undo();
        /// Response.Write("CurrentUserName: " + WindowsIdentity.GetCurrent().Name);
        /// </remarks>
        public WindowsImpersonationContext Impersonate(string domain, string userName, string password)
        {
            WindowsImpersonationContext impersonationContext = null;
            IntPtr token = IntPtr.Zero;
            IntPtr tokenDuplicate = IntPtr.Zero;

            try
            {
                if (RevertToSelf())
                {
                    if (LogonUserA(userName, domain, password, LOGON32_LOGON_INTERACTIVE,
                        LOGON32_PROVIDER_DEFAULT, ref token) != 0)
                    {
                        if (DuplicateToken(token, 2, ref tokenDuplicate) != 0)
                        {
                            WindowsIdentity tempWindowsIdentity = new WindowsIdentity(tokenDuplicate);
                            impersonationContext = tempWindowsIdentity.Impersonate();
                        }
                    }
                }
            }
            catch
            {
                ;
            }
            finally
            {
                if (token != IntPtr.Zero)
                    CloseHandle(token);
                if (tokenDuplicate != IntPtr.Zero)
                    CloseHandle(tokenDuplicate);
            }

            return impersonationContext;
        }

        private const int LOGON32_LOGON_INTERACTIVE = 2;
        private const int LOGON32_PROVIDER_DEFAULT = 0;

        [DllImport("advapi32.dll")]
        private static extern int LogonUserA(
            String lpszUserName,
            String lpszDomain,
            String lpszPassword,
            int dwLogonType,
            int dwLogonProvider,
            ref IntPtr phToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DuplicateToken(
            IntPtr hToken,
            int impersonationLevel,
            ref IntPtr hNewToken);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool RevertToSelf();

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern bool CloseHandle(IntPtr handle);

    }
}
