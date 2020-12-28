using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MeetingSystem.Client
{
    interface IClient
    {
        void startListen();
        void send(ClientMessage clientMessage, Object data);
        void close();
    }
}
