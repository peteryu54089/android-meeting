using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MeetingSystem.Client;

namespace MeetingSystem
{
    public interface IMessage
    {
        void sendMessage(MeetingClient client, BinaryWriter writer, Object obj);
        void handlerMessage(MeetingClient client, BinaryWriter writer, Object obj);
        Object receivedMessage(MeetingClient client, BinaryReader reader);
        void setParm(Object obj);
        Object getParm();

    }
}
