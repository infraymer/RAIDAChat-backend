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

            using (var db =  new CloudChatEntities())
            {
                db.members.ToList().ForEach(it => users.Add(it.public_id, it.an));
                db.groups.ToList().ForEach(it => groups.Add(it.group_id, it.group_name_part));
            }

            ViewBag.users = users;
            ViewBag.groups = groups;

            return View();
        }
    }
}
