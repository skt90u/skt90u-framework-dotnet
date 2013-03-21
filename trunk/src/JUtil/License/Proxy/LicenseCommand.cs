using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace JUtil.LicenseProxy
{
    public class LicenseCommand
    {
        public LicenseCommand(IPAddress ip, int port, SendMsg sendMsg)
        {
            this.ip = ip;
            this.port = port;
            this.sendMsg = sendMsg;
        }

        private IPAddress ip;
        /// <summary>
        /// [Gets/Sets] The IP address of command sender.
        /// </summary>
        public IPAddress IP
        {
            get { return ip; }
            set { ip = value; }
        }

        private int port;
        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private SendMsg sendMsg;
        /// <summary>
        /// The body of the command.This string is different in various commands.
        /// <para>Message : The text of the message.</para>
        /// <para>ClientLoginInform,SendClientList : "RemoteClientIP:RemoteClientName".</para>
        /// <para>***WithTimer : The interval of timer in miliseconds..The default value is 60000 equal to 1 min.</para>
        /// <para>IsNameExists : 'True' or 'False'</para>
        /// <para>Otherwise pass the "" or null.</para>
        /// </summary>
        public SendMsg SendMsg
        {
            get { return sendMsg; }
            set { sendMsg = value; }
        }
    }
}
