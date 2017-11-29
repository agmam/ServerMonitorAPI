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
        //This checks for the different event types
        public List<Event> CheckForEvent(ServerDetailAverage serverDetailAverage)
        {
            //Creates a list for the events
            var eventList = new List<Event>();

            var et = new EventType();

            //We read the eventtypes
            var eventTypes = eventTypeRepo.ReadAll();
            foreach (var eventType in eventTypes)
            {

                //If the eventType is low memory
                if (eventType.Name.Equals(et.setName(EventType.Type.LowMemory)))
                {
                    //We calculate the RAM used in %
                    //If it is greater than the peak value we add it to the list of events
                    if ((((serverDetailAverage.RAMTotal - serverDetailAverage.RAMAvailable) /
                          serverDetailAverage.RAMTotal) * 100) > eventType.PeakValue)
                    {
                        //Creates the event
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        //We add it to the list
                        eventList.Add(@event);
                    }
                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.HighCpuTemperature)))
                {
                    //If the CPU temperature is greater than the peak value we add it to the list
                    if (serverDetailAverage.Temperature > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }

                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.HighNetworkUtilization)))
                {
                    //The same applies to network as with the cpu temperature
                    if (serverDetailAverage.NetworkUtilization > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }
                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.Highcpu)))
                {
                    //It applies here too
                    if (serverDetailAverage.CPUUtilization > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }
                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.LowDiskSpace)))
                {
                    if ((serverDetailAverage.HarddiskTotalSpace - serverDetailAverage.HarddiskUsedSpace) < eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }
                }

            }
            //We then return the list
            return eventList;

        }



        private Event MakeEvent(ServerDetailAverage serverDetailAverage, EventType eventType)
        {
            //Here we set the event
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