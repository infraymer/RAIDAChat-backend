using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat.Reflection.dto
{
    public class AddMemberInGroupInfo
    {
        public String memberLogin { get; set; }
        public Guid groupId { get; set; }
    }
}