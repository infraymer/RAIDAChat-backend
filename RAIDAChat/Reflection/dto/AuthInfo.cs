//using Fleck;
using SuperSocket.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat.Reflection.dto
{
    public class AuthInfo
    {
        public String login { get; set; }
        public Guid an { get; set; }
    }

    public class AuthInfoWithSocket: AuthInfo
    {
        public bool auth { get; set; }
        public Guid publicId { get; set; }
        public WebSocketSession socket { get; set; }
    }

    public class RegistrationInfo: AuthInfo
    {
        public Guid publicId { get; set; }
    }
}