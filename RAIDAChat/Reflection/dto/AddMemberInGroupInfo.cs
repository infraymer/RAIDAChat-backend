using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAIDAChat.Reflection.dto
{
    public class AddMemberInGroupInfo
    {
        public Guid memberId { get; set; }
        public Guid groupId { get; set; }
    }
}