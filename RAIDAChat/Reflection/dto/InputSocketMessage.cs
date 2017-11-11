using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat.Reflection.dto
{
    public class InputSocketMessage
    {
        public string execFun { get; set; }
        public Object data { get; set; }
    }
}