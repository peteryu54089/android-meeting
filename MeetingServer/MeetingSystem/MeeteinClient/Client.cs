using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace MeetingSystem.Client
{
    public enum ClientState { Create, Init, Running };
    public abstract class Client : IClient
    {
        public abstract void startListen();
        public abstract void send(ClientMessage clientMessage, Object data = null);
        public abstract void close();
        protected TcpClient tcpClient = null;
        protected BinaryWriter writer = null;
        protected BinaryReader reader = null;
        private ClientState state = ClientState.Create;
        protected ClientState State
        {
            get { return state;} 
            set
            {
                try
                {
                    state = value;
                    notifyClientStateChanged((MeetingClient)this, state);
                }
                catch
                {
                    throw new Exception("Cannot convert " + this.GetType() + "  to " + typeof(MeetingClient));
                }
            }
        }

        private IPAddress ipAddr = null;
        public IPAddress IpAddr
        {
            get {
                if (ipAddr == null)
                    ipAddr = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address;
                return ipAddr; 
            }
        }
        
        public delegate void ClientException_Handler(MeetingSystem.Client.Client client,Exception e);
        public event ClientException_Handler clientExceptionListener = null;
        public void notifyClientExceptionOcurred(MeetingSystem.Client.Client client,Exception e)
        {
            if (clientExceptionListener != null)
            {
                clientExceptionListener(client,e);
            }
        }

        public delegate void ClientStateChanged_Handler(MeetingClient client,ClientState state);
        public event ClientStateChanged_Handler clientStateChangeddListener = null;
        public void notifyClientStateChanged(MeetingClient client, ClientState state)
        {
            if (clientStateChangeddListener != null)
            {
                clientStateChangeddListener(client, state);
            }
        }
    }
}
