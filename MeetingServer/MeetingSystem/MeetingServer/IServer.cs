using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using MeetingSystem.Client;

namespace MeetingSystem.Server
{
    interface IServer
    {
        void send(ClientMessage clientMessage, Object obj);
        void start();
        void stop();
        void release();
    }
}
