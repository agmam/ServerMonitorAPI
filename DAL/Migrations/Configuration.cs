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
            EventType lowMemoryEventType = new EventType()
            {
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 60,
                RiskEstimate = 1,
            };
            lowMemoryEventType.setName(EventType.Type.LowMemory);
            context.EventTypes.AddOrUpdate(lowMemoryEventType);

            EventType highCpuEventType = new EventType()
            {
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 70,
                RiskEstimate = 1,
            };
            highCpuEventType.setName(EventType.Type.Highcpu);
            context.EventTypes.AddOrUpdate(highCpuEventType);

            EventType highCpuTemperatureEventType = new EventType()
            {
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 70,
                RiskEstimate = 1,
            };
            highCpuTemperatureEventType.setName(EventType.Type.HighCpuTemperature);
            context.EventTypes.AddOrUpdate(highCpuTemperatureEventType);

            EventType networkUtilizationHighEventType = new EventType()
            {
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 60,
                RiskEstimate = 1,
            };
            networkUtilizationHighEventType.setName(EventType.Type.HighNetworkUtilization);
            context.EventTypes.AddOrUpdate(networkUtilizationHighEventType);

            EventType serverDownEventType = new EventType()
            {
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 60,
                RiskEstimate = 1,
            };
            serverDownEventType.setName(EventType.Type.ServerDown);
            context.EventTypes.AddOrUpdate(serverDownEventType);

            EventType lowDiskSpaceEventType = new EventType()
            {
                ShouldNotify = true,
                Created = DateTime.Now,
                PeakValue = 60,
                RiskEstimate = 1,
            };
            lowDiskSpaceEventType.setName(EventType.Type.LowDiskSpace);
            context.EventTypes.AddOrUpdate(lowDiskSpaceEventType);


            context.SaveChanges();
        }
    }
}
