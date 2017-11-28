using RAIDAChat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RAIDAChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            Dictionary<Guid, Guid> users = new Dictionary<Guid, Guid>();

            Dictionary<Guid, String> groups = new Dictionary<Guid, String>();

            using (var db = new CloudChatEntities())
            {
                db.members.ToList().ForEach(it => users.Add(it.public_id, it.an));
                db.groups.ToList().ForEach(it => groups.Add(it.group_id, it.group_name_part));
            }

            ViewBag.users = users;
            ViewBag.groups = groups;

            return View();
        }

        //public ActionResult Chat()
        //{
        //    ViewBag.SocketList = new Dictionary<String, String>() { {"localhost", "ws://192.168.0.102:49011/" }
        //   // ViewBag.SocketList = new Dictionary<String, String>() { {"localhost", "ws://192.168.0.102:49011/" }, { "rcn1", "ws://5.141.98.216:49011/" }, { "rcn2", "ws://5.141.98.216:49012/" }
        //    //, { "rcn21", "ws://5.141.98.216:49012" }, { "rcn22", "ws://5.141.98.216:49012" }, { "rcn23", "ws://5.141.98.216:49012" }, { "rcn24", "ws://5.141.98.216:49012" }
        //    //, { "rcn72", "ws://5.141.98.216:49012" }, { "rcn26", "ws://5.141.98.216:49012" }, { "rcn25", "ws://5.141.98.216:49012" }, { "rcn225", "ws://5.141.98.216:49012" }, { "rcn251", "ws://5.141.98.216:49012" }, { "rcn253", "ws://5.141.98.216:49012" }
        //    };

        //    return View();
        //}

    }
}
