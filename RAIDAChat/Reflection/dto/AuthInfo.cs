﻿using Fleck;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat.Reflection.dto
{
    public class AuthInfo
    {
        public Guid login { get; set; }
        public Guid an { get; set; }
    }

    public class AuthInfoWithSocket: AuthInfo
    {
        public bool auth { get; set; }
        public IWebSocketConnection socket { get; set; }

    }
}