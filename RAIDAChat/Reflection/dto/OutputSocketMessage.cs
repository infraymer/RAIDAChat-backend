using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat.Reflection.dto
{
    public class OutputSocketMessage
    {
        public string callFunction { get; set; }
        public bool success { get; set; }
        public string msgError { get; set; }
        public Object data { get; set; }

        public OutputSocketMessage(string callFunction, bool success, string msgError, object data)
        {
            this.callFunction = callFunction;
            this.success = success;
            this.msgError = msgError;
            this.data = data;
        }
    }

    public class OutputSocketMessageWithUsers
    {
        public OutputSocketMessage msg { get; set; }
        public List<Guid> usersId { get; set; }
        public OutputSocketMessageWithUsers()
        {
            this.usersId = new List<Guid>();
        }
    }
}