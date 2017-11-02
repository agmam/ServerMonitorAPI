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
           
            ViewBag.Title = "yolo";

            return View();
        }
    }
}
