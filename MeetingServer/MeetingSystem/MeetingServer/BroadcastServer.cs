using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;
using System.Web.Script.Serialization;
using MeetingSystem.Utils;

namespace MeetingSystem.Server
{
    public class BroadcastServer : Server
    {
        private Thread thread = null;
        private Socket socket = null;
        private int interval = 2000;
        private IPAddress broadcastAddress = null;
        private static BroadcastServer instance = null;

        private BroadcastServer()
        {
            Byte[] ipBytes = IPAddress.Parse(IP).GetAddressBytes();
            ipBytes[3] = 0xff;
            broadcastAddress = new IPAddress(ipBytes);
        }

        public override void start()
        {
            State = ServerState.Init;
            if (socket == null)
            {
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.EnableBroadcast = true;
                socket.Ttl = 50;
            }
            if (thread == null)
            {
                thread = new Thread(new ThreadStart(broadcast));
                thread.Start();
            }
            State = ServerState.Running;
        }

        public override void stop()
        {
            if (State == ServerState.Stoped)
                return;
            if (thread != null)
            {
                thread.Interrupt();
                thread = null;
            }
            if (socket != null)
            {
                socket.Close();
                socket = null;
            }
            State = ServerState.Stoped;
        }

        public override void release()
        {
            if (instance != null)
                instance.stop();
            instance = null;
            State = ServerState.Released;
        }

        private void broadcast()
        {
            while (socket != null &&　thread != null && thread.ThreadState == ThreadState.Running)
            {
                String jsonStr = JsonTool.getInstance().Serialize(Info);
                //Console.WriteLine("Json String : " + jsonStr);
                byte[] data = UTF8Encoding.UTF8.GetBytes(jsonStr);

                IPEndPoint ipep = new IPEndPoint(broadcastAddress, BroadcastPort);
                socket.SendTo(data, ipep);
                try
                {
                    Thread.Sleep(interval);
                }
                catch
                { }
            }
        }

        public static BroadcastServer getInstance()
        {
            if (instance == null)
                instance = new BroadcastServer();
            return instance;
        }
    }
}
