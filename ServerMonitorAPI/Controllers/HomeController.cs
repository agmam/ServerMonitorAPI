using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.DB;
using Entities.Entities;

namespace ServerMonitorAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Server s;
            using (var ctx = new ServerMonitorContext())
            {
               var res = ctx.Servers.Add(new Server { ServerName = "stor server", Created = DateTime.Now });
                ctx.SaveChanges();
                
            }
         
            using (var ctx = new ServerMonitorContext())
            {
               s = ctx.Servers.FirstOrDefault(x => x.ServerName == "stor server");

            }
            string ss = "ingen server";
            if (s != null)
            {
                ss = s.ServerName;
            }
            
            ViewBag.Title = ss;

            return View();
        }
    }
}
