using Entities.Entities;

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DAL.DB.ServerMonitorContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "DAL.DB.ServerMonitorContext";
        }

        protected override void Seed(DAL.DB.ServerMonitorContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            if (context.EventTypes.ToList().Count == 0)
            {
                EventType lowMemoryEventType = new EventType()
                {
                    Id = 1,
                    ShouldNotify = true,
                    Created = DateTime.Now,
                    PeakValue = 60,
                    RiskEstimate = 1,
                };
                lowMemoryEventType.setName(EventType.Type.LowMemory);
                context.EventTypes.AddOrUpdate(x => x.Name, lowMemoryEventType);

                EventType highCpuEventType = new EventType()
                {
                    Id = 2,
                    ShouldNotify = true,
                    Created = DateTime.Now,
                    PeakValue = 75,
                    RiskEstimate = 2,
                };
                highCpuEventType.setName(EventType.Type.Highcpu);
                context.EventTypes.AddOrUpdate(x => x.Name, highCpuEventType);

                EventType highCpuTemperatureEventType = new EventType()
                {
                    Id = 3,
                    ShouldNotify = true,
                    Created = DateTime.Now,
                    PeakValue = 70,
                    RiskEstimate = 1,
                };
                highCpuTemperatureEventType.setName(EventType.Type.HighCpuTemperature);
                context.EventTypes.AddOrUpdate(x => x.Name, highCpuTemperatureEventType);

                EventType networkUtilizationHighEventType = new EventType()
                {
                    Id = 4,
                    ShouldNotify = true,
                    Created = DateTime.Now,
                    PeakValue = 60,
                    RiskEstimate = 1,
                };
                networkUtilizationHighEventType.setName(EventType.Type.HighNetworkUtilization);
                context.EventTypes.AddOrUpdate(x => x.Name, networkUtilizationHighEventType);

                EventType serverDownEventType = new EventType()
                {
                    Id = 5,
                    ShouldNotify = false,
                    Created = DateTime.Now,
                    PeakValue = 60,
                    RiskEstimate = 1,
                };
                serverDownEventType.setName(EventType.Type.ServerDown);
                context.EventTypes.AddOrUpdate(x => x.Name, serverDownEventType);

                EventType lowDiskSpaceEventType = new EventType()
                {
                    Id = 6,
                    ShouldNotify = true,
                    Created = DateTime.Now,
                    PeakValue = 60,
                    RiskEstimate = 3,
                };
                lowDiskSpaceEventType.setName(EventType.Type.LowDiskSpace);
                context.EventTypes.AddOrUpdate(x => x.Name, lowDiskSpaceEventType);



            }
            if (context.EmailRecipients.ToList().Count == 0)
            {
                EmailRecipient er = new EmailRecipient()
                {
                    Created = DateTime.Now,
                    Email = "ewfewf@gmail.com"
                };
                context.EmailRecipients.Add(er);
            }
            context.SaveChanges();

        }
    }
}
