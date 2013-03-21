using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUtil.LicenseProxy
{
    public class RetMsg
    {
        public RetMsg(RetCode retCode, params string[] args)
        {
            this.retCode = retCode;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                sb.Append(args[i]);

                if (i != args.Length - 1)
                    sb.Append(", ");
            }

            this.ret_content = Encoding.ASCII.GetBytes(sb.ToString().Trim());
            this.ret_content_len = this.ret_content.Length;
        }

        public RetCode retCode;
        public int ret_content_len;
        public byte[] ret_content;
    }
}
