using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Structure
{
    class ServerInfo
    {
        private static String name;
        private static String ip;
        private static int broadcastPort;
        private static int servicePort;
        private static int screenServicePort;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        public int BroadcastPort
        {
            get { return broadcastPort; }
            set { broadcastPort = value; }
        }

        public int ServicePort
        {
            get { return servicePort; }
            set { servicePort = value; }
        }

        public int ScreenServicePort
        {
            get { return screenServicePort; }
            set { screenServicePort = value; }
        }
    }
}
