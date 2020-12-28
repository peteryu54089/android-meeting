using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Client;
using System.IO;

namespace MeetingSystem.Message
{
    class CloseMessageHandler:Message
    {
        public static void init()
        {
            MessagePool.registerMessage(ClientMessage.CloseMessage, new CloseMessageHandler());
        }
        public override void sendMessage(Client.MeetingClient client, System.IO.BinaryWriter writer, object obj)
        {
            //writer.Write((int)ClientMessage.CloseMessage);
            String msg = "cmd:4" + "msg:" + "\n";
            writer.Write(msg);
            //Console.WriteLine(msg);
        }
        public override void handlerMessage(MeetingClient client, BinaryWriter writer, Object obj)
        {
        }
        public override Object receivedMessage(MeetingClient client, BinaryReader reader)
        {
            return null;
        }
    }
}
