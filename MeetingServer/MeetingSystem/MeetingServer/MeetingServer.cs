using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;
using System.Collections;
using MeetingSystem.Client;
using System.Windows.Forms;
using MeetingSystem.Sync;
using rtaNetworking.Streaming;
using System.IO;

namespace MeetingSystem.Server
{
    public class MeetingServer : Server
    {

        private TcpListener tcpListener = null;
        private Thread listenThread = null;
        private static MeetingServer instance = null;
        private Hashtable clientHashTable = null;
        private List<IServer> subServers = null;
        private int[] radomCode=new int[900];
        private static int connectTimes = 0;
        private MeetingServer()
        {
            const int broadcastPort = 3389;
            const int screenServicePort = 8080;
            const int servicePort = 8081;
            clientHashTable = new Hashtable();
            subServers = new List<IServer>();
            IP = getServerIP();
            BroadcastPort = broadcastPort;
            ServicePort = servicePort;
            ScreenServicePort = screenServicePort;
            serverExceptionListener += onServerExceptionOcurred;
            serverStateChangedListener += onServerStateChanged;
            clientConnectedListener += onClientConnected;
            clientDisconnecteddListener += onClientDisconnected;
            subServers.Add(BroadcastServer.getInstance());
            subServers.Add(ImageStreamingServer.getInstance());
        }
        public static MeetingServer getInstance()
        {
            if (instance == null)
                instance = new MeetingServer();
            return instance;
        }

        private void startSubServers()
        {
            foreach (IServer server in subServers)
            {
                server.start();
            }
        }

        private void stopSubServers()
        {
            foreach (IServer server in subServers)
            {
                server.stop();
            }
        }

        private void releaseSubServers()
        {
            foreach (IServer server in subServers)
            {
                server.release();
            }
            subServers.Clear();
        }

        public override void send(ClientMessage clientMessage, Object obj = null)
        {
            lock (clientHashTable)
            {
                EventLog.Write(clientMessage.ToString());
                foreach (object key in clientHashTable.Keys)
                {
                    MeetingSystem.Client.Client client = (MeetingSystem.Client.Client)clientHashTable[key];
                    client.send(clientMessage, obj);
                }
            }

        }

        public override void start()
        {
            int retryCount = 0;
            const int retryLimit = 100;
            try
            {
             
                State = ServerState.Init;
                startSubServers();
                while (tcpListener == null && retryCount < retryLimit)
                {
                    try
                    {
                        tcpListener = new TcpListener(IPAddress.Any, ServicePort);
                        tcpListener.Start();
                    }
                    catch (Exception e)
                    {
                        tcpListener = null;
                        notifyClientExceptionOcurred(e);
                    }
                    retryCount++;
                }
                if (listenThread == null)
                {
                    listenThread = new Thread(new ThreadStart(clientListen));
                    listenThread.Start();
                }
                State = ServerState.Running;
            }
            catch (Exception e)
            {
                notifyClientExceptionOcurred(e);
                EventLog.Write("release "+e.ToString());
            }
        }

        public override void stop()
        {
            if (State == ServerState.Stoped)
                return;
            try
            {
                stopSubServers();
                removeAllClients();
                if (listenThread != null)
                {
                    listenThread.Interrupt();
                    listenThread = null;
                }
                if (tcpListener != null)
                {
                    tcpListener.Stop();
                    tcpListener = null;
                }
                State = ServerState.Stoped;
            }
            catch (Exception e)
            {
                notifyClientExceptionOcurred(e);
                EventLog.Write("stop " + e.ToString());
            }
        }

        public override void release()
        {
            releaseSubServers();
            if (instance != null)
                instance.stop();
            instance = null;
            State = ServerState.Released;
        }

        private void removeAllClients()
        {
            foreach (object key in clientHashTable.Keys)
            {
                MeetingClient client = (MeetingClient)clientHashTable[key];
                client.close();
            }
            clientHashTable.Clear();
        }

        private String getServerIP()
        {
            // 取得本機名稱
            String hostName = Dns.GetHostName();
            // 取得本機的 IpHostEntry 類別實體
            IPHostEntry iphostentry = Dns.GetHostByName(hostName);
            return iphostentry.AddressList[0].ToString();
        }
       
        private void clientListen()
        {
            try
            {
                EventLog.Write("Server listen started.");
                Console.WriteLine("Server listen started.");
                connectTimes = 0;
                while (listenThread != null && listenThread.ThreadState == ThreadState.Running)
                {
                    TcpClient tcpClient = tcpListener.AcceptTcpClient();
                    MeetingSystem.Client.Client client = new MeetingClient(tcpClient);
                    connectTimes++;
                    registClientEvent(client);
                    client.startListen();
                    notifyClientConnected(client);
                }
                Console.WriteLine("Server listen stoped.");
                EventLog.Write("Server listen stoped.");
            }
            catch (Exception e)
            {
                notifyClientExceptionOcurred(e);
                EventLog.Write("clientListen " + e.ToString());
            }
        }

        private void registClientEvent(MeetingSystem.Client.Client client)
        {
            client.clientExceptionListener += onClientExceptionOcurred;
        }

        private void onClientExceptionOcurred(MeetingSystem.Client.Client client, Exception e)
        {
            notifyClientDisconnected(client);
        }

        private void onServerExceptionOcurred(Exception e)
        {
            new Thread(new ThreadStart(() =>
            {
                Adapter.Invoke(new SendOrPostCallback(o =>
                {
                    //clientHashTable.Remove();
                    MessageBox.Show(e.Message);
                }), null);
            })) { IsBackground = true }.Start();
        }

        private void onServerStateChanged(Server server, ServerState state)
        {
            new Thread(new ThreadStart(() =>
            {
                Adapter.Invoke(new SendOrPostCallback(o =>
                {
                    Console.WriteLine(server.Name + " : " + Enum.GetName(typeof(ServerState), state));
                    EventLog.Write(server.Name + " : " + Enum.GetName(typeof(ServerState), state));
                }), null);
            })) { IsBackground = true }.Start();
        }

        private void onClientDisconnected(MeetingSystem.Client.Client client)
        {
            lock (clientHashTable)
            {
                Console.WriteLine("Client : " + client.IpAddr.ToString() + " Disconnected.");
                EventLog.Write("Client : " + client.IpAddr.ToString() + " Disconnected.");
                clientHashTable.Remove(client.IpAddr);
                client.close();
            }
        }

        private void onClientConnected(MeetingSystem.Client.Client client)
        {
            lock (clientHashTable)
            {
                if (clientHashTable.ContainsKey(client.IpAddr))//防止client端因為wifi斷線，重覆註冊的問題~~~~~~
                {
                    MeetingSystem.Client.Client redundancyClient=(MeetingSystem.Client.Client)clientHashTable[client.IpAddr];
                    redundancyClient.close();
                    
                    clientHashTable.Remove(client.IpAddr);
                }
                    
                 clientHashTable.Add(client.IpAddr, client);
                 Console.WriteLine("Client : " + client.IpAddr.ToString() + " Connected.");
                 EventLog.Write("Client : " + client.IpAddr.ToString() + " Connected.");
                
            }
        }
        public void cleanLocalFile(string WEB_PATH)
        {
            string[] webFilePaths = Directory.GetFiles(WEB_PATH);
            foreach (string filePath in webFilePaths)
            {
                File.Delete(filePath);
            }
            if (!File.Exists(WEB_PATH + "file\\"))
                return;
            string[] filePaths = Directory.GetFiles(WEB_PATH + "file\\");
            foreach (string filePath in filePaths)
            {
                File.Delete(filePath);
            }
        }
    }
}
