using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MeetingSystem.Client;

namespace MeetingSystem.Message
{
    public abstract class Message : IMessage
    {
        protected Object parm = null;
        public delegate void StatusHandler(string msg);
        public event StatusHandler StatusListener;
        protected void NotifyStatus(string msg)
        {
            if (StatusListener != null)
                StatusListener(msg);
        }
        public abstract void sendMessage(MeetingClient client, BinaryWriter writer, Object obj);
        public abstract void handlerMessage(MeetingClient client, BinaryWriter writer, Object obj);
        public abstract Object receivedMessage(MeetingClient client, BinaryReader reader);
        public void setParm(Object obj)
        {
            parm = obj;
        }
        public Object getParm()
        {
            return parm;
        }
        protected void createDirectory(String path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        } 
    }
}
