using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat.Reflection.dto
{
    [Serializable]
    public class GroupInfo
    {
        [JsonProperty(PropertyName = "groupName")]
        public string name { get; set; }
        public Guid public_id { get; set; }
    }
}