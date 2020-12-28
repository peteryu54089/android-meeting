using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeetingSystem.Utils;
using MeetingSystem.Message;

namespace MeetingSystem.Client
{
    public enum ClientMessage { ScoreMessage, VoteMessage, FileMessage, RankMessage, CloseMessage };
    public partial class MeetingClient
    {
        public static void initMessage()
        {
            VoteMessageHandler.init();
            ScoreMessageHandler.init();
            FileMessageHandler.init();
            RankMessageHandler.init();
            CloseMessageHandler.init();
        }
    }
}
