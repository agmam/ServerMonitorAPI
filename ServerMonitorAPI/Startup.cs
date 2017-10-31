using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ServerMonitorAPI.Models;

[assembly: OwinStartup(typeof(ServerMonitorAPI.Startup))]

namespace ServerMonitorAPI
{
    public partial class Startup
    {
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

            ConfigureAuth(app);
        }
    }
}
