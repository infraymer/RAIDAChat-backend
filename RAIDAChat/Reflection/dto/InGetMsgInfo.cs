using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat.Reflection.dto
{
    public class InGetMsgInfo: AuthInfo
    {
        public bool getAll { get; set; }
        public bool onGroup { get; set; }
        public Guid onlyId { get; set; }

    }

    public class OutGetMsgInfo
    {
        public Guid guidMsg { get; set; }
        public string textMsg { get; set; }
        public Guid sender { get; set; }
        public string group { get; set; }

        public OutGetMsgInfo()
        {

        }

        public OutGetMsgInfo(Guid guidMsg, string textMsg, Guid sender, string group)
        {
            this.guidMsg = guidMsg;
            this.textMsg = textMsg;
            this.sender = sender;
            this.group = group;
        }
    }
}