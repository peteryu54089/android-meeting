using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Client;
using System.Collections;

namespace MeetingSystem.Message
{
    public class MessagePool
    {
        private static Hashtable messages = new Hashtable();

        public static void registerMessage(ClientMessage command,IMessage message)
        {
            messages.Add(command,message);
        }

        public static void unregisterMessage(ClientMessage command, IMessage message)
        {
            messages.Remove(command);
        }

        public static IMessage getMessage(ClientMessage command)
        { 
            return (IMessage)messages[command];
        }
    }
}
