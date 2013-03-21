using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;

namespace JUtil.LicenseProxy
{
    public class LicenseClientException : Exception
    {
        public LicenseClientException(string message)
            : base(message) { }
    } 

    public class LicenseClient
    {
        public const string ServerIP = "skt90u.ath.cx";
        //public const string ServerIP = "192.168.0.100";
        public const int ServerPort = 8000;

        private IPAddress _server;
        private int _port;

        private Socket _clientSD;	// 宣告SocketID
        private const int SocketTimeOut = 6000; //6秒
        //private const int SocketTimeOut = 60000; //60秒
        private const int RecvBufferSize = 4096;

        private string _retMessage = string.Empty;

        public static bool CanConnect
        {
            get
            {
                // 有問題，別用
                return JUtil.Net.CheckPort(ServerIP, ServerPort, 2000);
            }
        }

        public void StartCon()
        {
            StartCon(ServerIP, ServerPort);
        }

        public void StartCon(string server, int port)
        {
            IPAddress[] IPs;
            try
            {
                IPs = Dns.GetHostAddresses(server);
            }
            catch (Exception ex)
            {
                string errorMessage = string.Format("Failed to Dns.GetHostAddresses({0}), becase {1}", server, ex.Message);
                throw new Exception(errorMessage);
            }
            _server = IPs[0];

            _port = port;

            _clientSD = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            _clientSD.SendTimeout = SocketTimeOut;
            _clientSD.ReceiveTimeout = SocketTimeOut;
            _clientSD.Connect(_server, _port);
        }

        public void EndCon()
        {
            if (_clientSD != null)
            {
                if (_clientSD.Connected)
                    _clientSD.Shutdown(SocketShutdown.Both);

                _clientSD.Close();
                _clientSD = null;
            }
        }

        private int Send(SendMsg sendMsg)
        {
            int total_send = 0;

            byte[] typeBuffer = new byte[4];
            typeBuffer = BitConverter.GetBytes((int)sendMsg.msgType);
            total_send += _clientSD.Send(typeBuffer, 0, 4, SocketFlags.None);

            byte[] contentLenBuffer = new byte[4];
            contentLenBuffer = BitConverter.GetBytes(sendMsg.msg_content_len);
            total_send += _clientSD.Send(contentLenBuffer, 0, 4, SocketFlags.None);

            if (sendMsg.msg_content.Length != 0)
                total_send += _clientSD.Send(sendMsg.msg_content, 0, sendMsg.msg_content.Length, SocketFlags.None);

            return total_send;
        }

        /// <summary>
        /// 接收資料
        /// </summary>
        private RetMsg Receive()
        {
            byte[] codeBuffer = new byte[4];
            int readBytes = _clientSD.Receive(codeBuffer, 0, 4, SocketFlags.None);
            RetCode retCode = (RetCode)(BitConverter.ToInt32(codeBuffer, 0));

            byte[] contentLenBuffer = new byte[4];
            readBytes = _clientSD.Receive(contentLenBuffer, 0, 4, SocketFlags.None);
            int contentLen = BitConverter.ToInt32(contentLenBuffer, 0);

            string ret_content = string.Empty;
            if (contentLen != 0)
            {
                byte[] contentBuffer = new byte[contentLen];
                readBytes = _clientSD.Receive(contentBuffer, 0, contentLen, SocketFlags.None);
                ret_content = System.Text.Encoding.ASCII.GetString(contentBuffer);
            }

            RetMsg retMsg = new RetMsg(retCode, ret_content);

            return retMsg;
        }

        public string GetActivationCode(string softwareName, string encodingStr)
        {
            SendMsg sendMsg = new SendMsg(MsgType.GetActivationCode, softwareName, encodingStr);

            Send(sendMsg);

            RetMsg retMsg = Receive();

            string retVal = System.Text.Encoding.ASCII.GetString(retMsg.ret_content);

            if (retMsg.retCode == RetCode.Successful)
                return retVal;
            else
                throw new LicenseClientException(retVal);
        }
    }
}
