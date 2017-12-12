using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ServerMonitorAPI.Logic;
using ServerMonitorAPI.Models;

[assembly: OwinStartup(typeof(ServerMonitorAPI.Startup))]

namespace ServerMonitorAPI
{
    public partial class Startup
    {
        private Thread serverdownThread;
        public void Configuration(IAppBuilder app)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var name = "server@monitor.dk";
            var user = new ApplicationUser();
            user.UserName = name;
            user.Email = name;
            string userPWD = "server123";

            var result = userManager.Create(user, userPWD);
            serverdownThread = new Thread(new ThreadStart(RunServerdownThread));
            serverdownThread.Start();

            ConfigureAuth(app);
        }

        private void RunServerdownThread()
        {
            while (true)
            {
                ServerDownChecker sdc = new ServerDownChecker();
                sdc.ServerDown();
                Thread.Sleep(5000);
            }
        }
    }
}
