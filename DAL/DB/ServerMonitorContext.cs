using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;
using DAL.Migrations;

namespace DAL.DB
{
    public class ServerMonitorContext: DbContext
    {
       public ServerMonitorContext(): base("ServerMonitorDB")
        {
            Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ServerMonitorContext, Configuration>());
        }

        public DbSet<Server> Servers { get; set; }
        public DbSet<ServerDetail> ServerDetails { get; set; }
        public DbSet<Event> Events{ get; set; }
        public DbSet<EmailRecipient> EmailRecipients { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<ServerDetailAverage> ServerDetailAverages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // Database.SetInitializer<ServerMonitorContext>(null);

            modelBuilder.Entity<Event>().HasRequired(x => x.EventType).WithMany(x => x.Events);
            modelBuilder.Entity<Event>().HasRequired(x => x.ServerDetail).WithMany(x => x.Events).WillCascadeOnDelete(false);

            modelBuilder.Entity<Server>().HasMany(x => x.Events).WithRequired(x => x.Server);
            modelBuilder.Entity<Server>().HasMany(x => x.ServerDetails).WithRequired(x => x.Server);
            modelBuilder.Entity<Server>().HasMany(x => x.ServerDetailAverages).WithRequired(x => x.Server);

            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

       
    }
}
