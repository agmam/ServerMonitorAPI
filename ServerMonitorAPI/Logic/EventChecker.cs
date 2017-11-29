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
        public List<Event> CheckForEvent(ServerDetailAverage serverDetailAverage)
        {
            var eventList = new List<Event>();
            var et = new EventType();
            var eventTypes = eventTypeRepo.ReadAll();
            foreach (var eventType in eventTypes)
            {


                if (eventType.Name.Equals(et.setName(EventType.Type.LowMemory)))
                {
                    if ((((serverDetailAverage.RAMTotal - serverDetailAverage.RAMAvailable) /
                          serverDetailAverage.RAMTotal) * 100) > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }
                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.HighCpuTemperature)))
                {
                    if (serverDetailAverage.Temperature > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }

                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.HighNetworkUtilization)))
                {
                    if (serverDetailAverage.NetworkUtilization > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }
                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.Highcpu)))
                {
                    if (serverDetailAverage.CPUUtilization > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }
                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.LowDiskSpace)))
                {

                }

            }

            return eventList;

        }


        private Event MakeEvent(ServerDetailAverage serverDetailAverage, EventType eventType)
        {
            var e = new Event();
            EventType et = eventType;
            e.ServerId = serverDetailAverage.ServerId;
            e.Created = serverDetailAverage.Created;
            e.EventType = et;
            e.EventTypeId = et.Id;
            e.ServerDetailAverageId = serverDetailAverage.Id;
            Event @event = eventRepo.Create(e);
            return @event;
        }
    }
}