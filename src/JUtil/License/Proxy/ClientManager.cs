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
    public class ClientManager
    {
        private Socket socket;
        NetworkStream networkStream;
        private BackgroundWorker bwReceiver;

        /// <summary>
        /// Gets the IP address of connected remote client.This is 'IPAddress.None' if the client is not connected.
        /// </summary>
        public IPAddress IP
        {
            get
            {
                if (socket != null)
                    return ((IPEndPoint)socket.RemoteEndPoint).Address;
                else
                    return IPAddress.None;
            }
        }
        /// <summary>
        /// Gets the port number of connected remote client.This is -1 if the client is not connected.
        /// </summary>
        public int Port
        {
            get
            {
                if (socket != null)
                    return ((IPEndPoint)socket.RemoteEndPoint).Port;
                else
                    return -1;
            }
        }
        /// <summary>
        /// [Gets] The value that specifies the remote client is connected to this server or not.
        /// </summary>
        public bool Connected
        {
            get
            {
                if (socket != null)
                    return socket.Connected;
                else
                    return false;
            }
        }

        public ClientManager(Socket clientSocket)
        {
            socket = clientSocket;
            networkStream = new NetworkStream(socket);
        }

        public void StartReceive()
        {
            bwReceiver = new BackgroundWorker();
            bwReceiver.DoWork += new DoWorkEventHandler(StartReceive);
            bwReceiver.RunWorkerAsync();
        }

        public bool Disconnect()
        {
            if (socket != null && socket.Connected)
            {
                try
                {
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            else
                return true;
        }

        private void StartReceive(object sender, DoWorkEventArgs e)
        {
            while (socket.Connected)
            {
                try
                {
                    byte[] typeBuffer = new byte[4];
                    int readBytes = networkStream.Read(typeBuffer, 0, 4);
                    if (readBytes == 0)
                        break;
                    MsgType msgType = (MsgType)(BitConverter.ToInt32(typeBuffer, 0));

                    byte[] contentLenBuffer = new byte[4];
                    readBytes = networkStream.Read(contentLenBuffer, 0, 4);
                    if (readBytes == 0)
                        break;
                    int contentLen = BitConverter.ToInt32(contentLenBuffer, 0);

                    string msg_content = string.Empty;
                    if(contentLen !=0)
                    {
                        byte[] contentBuffer = new byte[contentLen];
                        readBytes = networkStream.Read(contentBuffer, 0, contentLen);
                        if (readBytes == 0)
                            break;
                        msg_content = System.Text.Encoding.ASCII.GetString(contentBuffer);    
                    }

                    SendMsg sendMsg = new SendMsg(msgType, msg_content);

                    LicenseCommand cmd = new LicenseCommand(IP, Port, sendMsg);

                    OnCommandReceived(new CommandEventArgs(cmd));
                }
                catch (Exception)
                {
                    break;
                }
            }
            OnDisconnected(new ClientEventArgs(socket));
            Disconnect();
        }

        public event CommandReceivedEventHandler CommandReceived;
        protected virtual void OnCommandReceived(CommandEventArgs e)
        {
            if (CommandReceived != null)
                CommandReceived(this, e);
        }

        public event DisconnectedEventHandler Disconnected;
        protected virtual void OnDisconnected(ClientEventArgs e)
        {
            if (Disconnected != null)
                Disconnected(this, e);
        }

        public event CommandSendingFailedEventHandler CommandFailed;
        protected virtual void OnCommandFailed(EventArgs e)
        {
            if (CommandFailed != null)
                CommandFailed(this, e);
        }

        public event CommandSentEventHandler CommandSent;
        /// <summary>
        /// Occurs when a command had been sent to the remote client successfully.
        /// </summary>
        /// <param name="e">The sent command.</param>
        protected virtual void OnCommandSent(EventArgs e)
        {
            if (CommandSent != null)
                CommandSent(this, e);
        }

        public void SendCommand(RetMsg retMsg)
        {
            if (socket != null && socket.Connected)
            {
                BackgroundWorker bwSender = new BackgroundWorker();
                bwSender.DoWork += new DoWorkEventHandler(bwSender_DoWork);
                bwSender.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bwSender_RunWorkerCompleted);
                bwSender.RunWorkerAsync(retMsg);
            }
            else
                OnCommandFailed(new EventArgs());
        }

        private void bwSender_DoWork(object sender, DoWorkEventArgs e)
        {
            RetMsg retMsg = (RetMsg)e.Argument;
            e.Result = SendCommandToClient(retMsg);
        }

        private void bwSender_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null && ((bool)e.Result))
                OnCommandSent(new EventArgs());
            else
                OnCommandFailed(new EventArgs());

            ((BackgroundWorker)sender).Dispose();
            GC.Collect();
        }

        //This Semaphor is to protect the critical section from concurrent access of sender threads.
        System.Threading.Semaphore semaphor = new System.Threading.Semaphore(1, 1);
        private bool SendCommandToClient(RetMsg retMsg)
        {
            try
            {
                semaphor.WaitOne();

                byte[] codeBuffer = new byte[4];
                codeBuffer = BitConverter.GetBytes((int)retMsg.retCode);
                this.networkStream.Write(codeBuffer, 0, 4);
                this.networkStream.Flush();

                byte[] contentLenBuffer = new byte[4];
                contentLenBuffer = BitConverter.GetBytes(retMsg.ret_content_len);
                this.networkStream.Write(contentLenBuffer, 0, 4);
                this.networkStream.Flush();

                if (retMsg.ret_content_len != 0)
                {
                    networkStream.Write(retMsg.ret_content, 0, retMsg.ret_content.Length);
                    networkStream.Flush();    
                }

                semaphor.Release();
                return true;
            }
            catch
            {
                semaphor.Release();
                return false;
            }
        }


    }
}
