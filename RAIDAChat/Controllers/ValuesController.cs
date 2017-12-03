using RAIDAChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RAIDAChat.Controllers
{
   
    public class ValuesController : ApiController
    {

        [HttpGet]
        [Route("api/openConnect")]
        public void openSocketConnect()
        {
            new newSocket().OpenSocket();
        }

        [HttpGet]
        [Route("api/cleanDB")]
        public void cleanDB()
        {
            using (var db = new CloudChatEntities()) {
                var rows = from o in db.members
                           select o;
                foreach (var row in rows)
                {
                    db.members.Remove(row);
                }

                var rows1 = from o in db.groups
                           select o;
                foreach (var row in rows1)
                {
                    db.groups.Remove(row);
                }


                var rows2 = from o in db.group_members
                           select o;
                foreach (var row in rows2)
                {
                    db.group_members.Remove(row);
                }


                var rows3 = from o in db.shares
                            select o;
                foreach (var row in rows3)
                {
                    db.shares.Remove(row);
                }

                var rows4 = from o in db.content_over_8000
                            select o;
                foreach (var row in rows4)
                {
                    db.content_over_8000.Remove(row);
                }

                db.SaveChanges();
            }
        }
    }
}
