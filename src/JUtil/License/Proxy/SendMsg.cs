using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUtil.LicenseProxy
{
    /// <summary>
    /// LicenseClient 傳送至 LicenseServer的資料格式
    /// </summary>
    public class SendMsg
    {
        public SendMsg(MsgType msgType, params string[] args)
        {
            this.msgType = msgType;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                sb.Append(args[i]);

                if (i != args.Length - 1)
                    sb.Append(",");
            }

            this.msg_content = Encoding.ASCII.GetBytes(sb.ToString().Trim());
            this.msg_content_len = this.msg_content.Length;
        }

        public MsgType msgType;
        public int msg_content_len;
        public byte[] msg_content;
    }
}
