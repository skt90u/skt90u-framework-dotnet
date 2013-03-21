using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JUtil.LicenseProxy
{
    public delegate void CommandReceivedEventHandler(object sender, CommandEventArgs e);
    public delegate void DisconnectedEventHandler(object sender, ClientEventArgs e);
    public delegate void CommandSendingFailedEventHandler(object sender, EventArgs e);
    public delegate void CommandSentEventHandler(object sender, EventArgs e);

    public delegate void NetworkDeadEventHandler(object sender, EventArgs e);
    public delegate void ClientDisconnectedEventHandler(object sender, EventArgs e);
    public delegate void NetworkAlivedEventHandler(object sender, EventArgs e);
}
