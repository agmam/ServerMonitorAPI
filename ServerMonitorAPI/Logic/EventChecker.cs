using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Web;
using DAL;
using DAL.Repositories;
using Entities.Entities;

namespace ServerMonitorAPI.Logic
{
    public class EventChecker
    {
        private IRepository<EventType> eventTypeRepo = new DALFacade().GetCRUDEventTypeRepository();
        private IRepository<Event> eventRepo = new DALFacade().GetCRUDEventRepository();
        private IRepository<Server> serverRepo = new DALFacade().GetCRUDServerRepository();
        private int RamMax { get; set; }

        public EventChecker()
        {
            RamMax = 50;
        }
        public Event CheckForEvent(ServerDetailAverage serverDetailAverage)
        {
            Event e = new Event();
            if ((((serverDetailAverage.RAMTotal - serverDetailAverage.RAMAvailable) / serverDetailAverage.RAMTotal) * 100) > RamMax)
            {
                var eventTypes = eventTypeRepo.ReadAll();
                var et = new EventType();
                et.setName(EventType.Type.LowMemory);

                foreach (var eventType in eventTypes)
                {
                    if (eventType.Name.Equals(et.Name))
                    {
                        et = eventType;
                        break;
                    }
                }
                e.ServerId = serverDetailAverage.ServerId;
                e.Created = serverDetailAverage.Created;
                e.EventType = et;
                e.EventTypeId = et.Id;
                e.Server = serverRepo.Read(serverDetailAverage.ServerId);
                e.ServerDetailAverage = serverDetailAverage;
                e.ServerDetailAverageId = serverDetailAverage.Id;
                eventRepo.Create(e);
                return e;
            }
            return null;

        }
    }
}