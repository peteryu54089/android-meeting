using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Structure;
using MeetingClient = MeetingSystem.Client.MeetingClient;
using MeetingSystem.Client;

namespace MeetingSystem.Server
{
    public enum ServerState { Create, Init, Running, Stoped, Released };
    public enum Command { SendScoreResult, GetScoreTable };
    public abstract class Server : IServer
    {
        public virtual void send(ClientMessage clientMessage, Object obj) { }
        public abstract void start();
        public abstract void stop();
        public abstract void release();

        private ServerInfo info = new ServerInfo();
        internal ServerInfo Info
        {
            get { return info; }
            set { info = value; }
        }

        public int ServicePort
        {
            get { return info.ServicePort; }
            set { info.ServicePort = value; }
        }

        public int ScreenServicePort
        {
            get { return info.ScreenServicePort; }
            set { info.ScreenServicePort = value; }
        }

        public int BroadcastPort
        {
            get { return info.BroadcastPort; }
            set { info.BroadcastPort = value; }
        }

        public String IP
        {
            get { return info.Ip; }
            set { info.Ip = value; }
        }

        public String Name
        {
            get { return info.Name; }
            set { info.Name = value; }
        }

        private ServerState state = ServerState.Create;
        public ServerState State
        {
            get { return state; }
            set
            {
                state = value;
                notifyServerStateChanged(this, state);
            }
        }

        public delegate void ClientConnected_Handler(MeetingSystem.Client.Client client);
        public event ClientConnected_Handler clientConnectedListener = null;
        public void notifyClientConnected(MeetingSystem.Client.Client client)
        {
            if (clientConnectedListener != null)
            {
                clientConnectedListener(client);
            }
        }


        public delegate void ClientDisconnected_Handler(MeetingSystem.Client.Client client);
        public event ClientDisconnected_Handler clientDisconnecteddListener = null;
        public void notifyClientDisconnected(MeetingSystem.Client.Client client)
        {
            if (clientDisconnecteddListener != null)
            {
                clientDisconnecteddListener(client);
            }
        }

        public delegate void ServerException_Handler(Exception e);
        public event ServerException_Handler serverExceptionListener = null;
        public void notifyClientExceptionOcurred(Exception e)
        {
            if (serverExceptionListener != null)
            {
                serverExceptionListener(e);
            }
        }

        public delegate void ServerStateChanged_Handler(Server server, ServerState state);
        public event ServerStateChanged_Handler serverStateChangedListener = null;
        public void notifyServerStateChanged(Server server, ServerState state)
        {
            if (serverStateChangedListener != null)
            {
                serverStateChangedListener(server, state);
            }
        }
    }
}
