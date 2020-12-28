using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace MeetingSystem.Utils
{
    public class JsonTool
    {
        private static JavaScriptSerializer instance = null;
        
        public static JavaScriptSerializer getInstance()
        {
            if (instance == null)
            {
                instance = new JavaScriptSerializer();
            }
            return instance;
        }
    }
}
