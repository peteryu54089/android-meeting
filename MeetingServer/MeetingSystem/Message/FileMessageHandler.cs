using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Client;
using System.ComponentModel;
using System.IO;
using MeetingSystem.Utils;

namespace MeetingSystem.Message
{
    class FileMessageHandler: Message
    {
        private FileMessageHandler()
        {
            parm = new BindingList<FileDataItem>();
        }

        public static void init()
        {
            MessagePool.registerMessage(ClientMessage.FileMessage, new FileMessageHandler());
        }

        public override void sendMessage(MeetingClient client, BinaryWriter writer, Object obj)
        {
            String jsonStr;
            if(obj==null)
                jsonStr = JsonTool.getInstance().Serialize(parm);
            else
                jsonStr = JsonTool.getInstance().Serialize(obj);
            String msg = "cmd:2" + "msg:" + jsonStr + "\n";
            writer.Write(msg);
            Console.WriteLine(msg);
            //writer.Write((int)ClientMessage.FileMessage);
            Console.WriteLine(client.IpAddr+ " " + jsonStr);
            EventLog.Write(client.IpAddr + " " + jsonStr);
            //writer.Write(jsonStr);
        }

        public override void handlerMessage(MeetingClient client, BinaryWriter writer, Object obj)
        {

        }

        public override Object receivedMessage(MeetingClient client, BinaryReader reader)
        {
            //String jsonStr = reader.ReadString();
            //Console.WriteLine(jsonStr);
            return null;
        }

        public BindingList<FileDataItem> FileLists
        {
            get { return (BindingList<FileDataItem>)parm; }
            set { parm = value; }
        }
    }
}
