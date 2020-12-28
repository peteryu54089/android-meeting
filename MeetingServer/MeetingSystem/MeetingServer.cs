using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading;
using System.Net.Sockets;

namespace MeetingSystem
{
    public class MeetingServer
    {
        private const int broadcastPort = 3389;
        private const int screenServicePort = 8080;
        private const int servicePort = 8081;
        private TcpListener serviceListener = null;
        private Thread listenThread = null;
        private static MeetingServer instance = null;

        private MeetingServer()
        {
        }

        public MeetingServer getInstance()
        {
            if (instance == null)
                instance = new MeetingServer();
            return instance;
        }

        public void start()
        {
            if (listenThread == null)
            {
                listenThread = new Thread(new ThreadStart(listenClientHandler));
                listenThread.Start();
            }
        }

        public void stop()
        {

        }

        public void release()
        {

        }

        private void listenClientHandler()
        {
            serviceListener = new TcpListener(servicePort);
            serviceListener.Start();
            while(listenThread != null && listenThread.ThreadState == ThreadState.Running)
            {
                TcpClient tcpClient = serviceListener.AcceptTcpClient();
            }
        }
    }
}
