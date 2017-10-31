using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.DB;
using DAL.Repositories;
using Entities.Entities;

namespace ServerMonitorAPI.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ActionResult Index()
        {
            var deleted = new DALFacade().GetServerRepository().ReadAll();
            log.Info("Hello");
            Server s = new Server(){
                ServerName = "lille server"
            };
            s = new DALFacade().GetServerRepository().Create(s);
            var ss = "ingen server";
            if (s != null)
            {
                s.ServerName = "Updated";
                s = new DALFacade().GetServerRepository().Update(s);
                ss = s.ServerName;
            }
            
            ViewBag.Title = ss;

            return View();
        }
    }
}
