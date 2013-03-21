using System;

namespace JUtil.LicenseProxy
{
    public class CmdGetActivationCode
    {
        public CmdGetActivationCode(ClientManager mngr, SendMsg sendMsg)
        {
            this.mngr = mngr;

            string msg_content = System.Text.Encoding.ASCII.GetString(sendMsg.msg_content);

            string[] args = msg_content.Split(new char[] { ',' });
            for (int i = 0; i < args.Length; i++)
                args[i] = args[i].Trim();

            if (args.Length > 0) softwareName = args[0];
            if (args.Length > 1) encodingStr = args[1];
        }

        public void Execute()
        {
            RetCode retCode = RetCode.Failure;
            string ret_content = string.Empty;

            try
            {
                // 檢驗 SerialNumber 是否已經被用在別台電腦
                CertificationCode.Check(softwareName, encodingStr);

                string actCode = ActivationCode.Generate(softwareName, encodingStr);

                retCode = RetCode.Successful;
                ret_content = actCode;
            }
            catch (Exception e)
            {
                ret_content = e.Message;
            }
            finally
            {
                RetMsg retMsg = new RetMsg(retCode, ret_content);
                mngr.SendCommand(retMsg);
            }

        }

        private ClientManager mngr;
        private string softwareName;
        private string encodingStr;
    }
}
