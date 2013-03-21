using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using JUtil;

namespace JUtil.LicenseProxy
{
    public class LicenseServer
    {
        public List<ClientManager> clients = new List<ClientManager>();
        public BackgroundWorker bwListener;
        public Socket listenerSocket;
        public IPAddress serverIP;
        public int serverPort;

        public LicenseServer()
        {
            serverPort = 8000;
            serverIP = IPAddress.Any;
        }

        public LicenseServer(string ip)
        {
            serverPort = 8000;
            serverIP = IPAddress.Parse(ip);
        }

        public LicenseServer(string ip, int port)
        {
            serverPort = port;
            serverIP = IPAddress.Parse(ip);
        }

        public void Start()
        {
            try
            {
                clients = new List<ClientManager>();
                bwListener = new BackgroundWorker();
                bwListener.WorkerSupportsCancellation = true;
                bwListener.DoWork += new DoWorkEventHandler(StartToListen);
                bwListener.RunWorkerAsync();
            }
            catch (Exception e)
            {
                Log.E(e);
            }
        }

        public void Stop()
        {
            try
            {
                if (clients != null)
                {
                    foreach (ClientManager mngr in clients)
                        mngr.Disconnect();

                    bwListener.CancelAsync();
                    bwListener.Dispose();
                    listenerSocket.Close();
                    GC.Collect();
                }
            }
            catch (Exception e)
            {
                Log.E(e);
            }
        }

        private void StartToListen(object sender, DoWorkEventArgs e)
        {
            listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            listenerSocket.Bind(new IPEndPoint(serverIP, serverPort));
            listenerSocket.Listen(200);
            while (true)
            {
                CreateNewClientManager(listenerSocket.Accept());
            }
        }

        private void CreateNewClientManager(Socket socket)
        {
            try
            {
                ClientManager newClientManager = new ClientManager(socket);
                newClientManager.CommandReceived += new CommandReceivedEventHandler(CommandReceived);
                newClientManager.Disconnected += new DisconnectedEventHandler(ClientDisconnected);
                newClientManager.StartReceive();
                CheckForAbnormalDC(newClientManager);
                clients.Add(newClientManager);
            }
            catch (Exception e)
            {
                Log.E(e);
            }
        }

        private void CheckForAbnormalDC(ClientManager mngr)
        {
            if (RemoveClientManager(mngr.IP))UpdateConsole("Disconnected.", mngr.IP, mngr.Port);
        }

        private void UpdateConsole(string status, IPAddress IP, int port)
        {
            Log.I("Client {0}{1}{2} has been {3} ( {4}|{5} )", IP.ToString(), ":", port.ToString(), status, DateTime.Now.ToShortDateString(), DateTime.Now.ToLongTimeString());
        }

        private bool RemoveClientManager(IPAddress ip)
        {
            lock (this)
            {
                int index = IndexOfClient(ip);
                if (index != -1)
                {
                    clients.RemoveAt(index);
                    return true;
                }
                return false;
            }
        }

        private int IndexOfClient(IPAddress ip)
        {
            int index = -1;
            foreach (ClientManager cMngr in clients)
            {
                index++;
                if (cMngr.IP.Equals(ip))
                    return index;
            }
            return -1;
        }

        private void CommandReceived(object sender, CommandEventArgs e)
        {
            try
            {
                int idx = IndexOfClient(e.Command.IP);
                
                if (idx != -1)
                {
                    SendMsg sendMsg = e.Command.SendMsg;

                    ClientManager mngr = clients[idx];

                    switch(sendMsg.msgType)
                    {
                        case MsgType.GetActivationCode:
                            {
                                CmdGetActivationCode cmd = new CmdGetActivationCode(mngr, sendMsg);
                                cmd.Execute();
                            }
                            break;

                        default:
                            {
                                RetMsg retMsg = new RetMsg(RetCode.Failure, "unhandle MsgType: " + sendMsg.msgType.ToString());
                                mngr.SendCommand(retMsg);
                            }
                            break;
                    }
                }
            }
            catch (Exception err)
            {
                Log.E(err);
            }
        }

        void ClientDisconnected(object sender, ClientEventArgs e)
        {
            try
            {
                if (RemoveClientManager(e.IP))
                    UpdateConsole("Disconnected.", e.IP, e.Port);
            }
            catch (Exception ee)
            {
                Log.E(ee);
            }
        }
    }
}
