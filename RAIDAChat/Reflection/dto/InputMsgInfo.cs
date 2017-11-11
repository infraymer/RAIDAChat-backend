using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat.Reflection.dto
{
    [Serializable]
    public class InputMsgInfo: AuthInfo
    {
        public Guid recipientId { get; set; }
        public bool toGroup { get; set; }
        public string textMsg { get; set; }

        [JsonProperty(PropertyName = "guidMsg")]
        public Guid msgId { get; set; }
    }
}