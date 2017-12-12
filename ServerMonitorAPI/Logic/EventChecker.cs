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

       
        //This checks for the different event types
        public List<Event> CheckForEvent(ServerDetailAverage serverDetailAverage , List<EventType> eventTypes )
        {
            if (serverDetailAverage == null || eventTypes == null)
            {
                return new List<Event>();
            }
            //Creates a list for the events
            var eventList = new List<Event>();

            var et = new EventType();

            //We read the eventtypes
            foreach (var eventType in eventTypes)
            {

                //If the eventType is low memory
                if (eventType.Name.Equals(et.setName(EventType.Type.LowMemory))) // RAM
                {
                    //We calculate the RAM used in %
                    //If it is greater than the peak value we add it to the list of events
                    if(serverDetailAverage.RAMTotal > 0) { 
                    if ((((serverDetailAverage.RAMTotal - serverDetailAverage.RAMAvailable) /
                          serverDetailAverage.RAMTotal) * 100) > eventType.PeakValue)
                    {
                        //Creates the event
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        //We add it to the list
                        eventList.Add(@event);
                    }
                    }
                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.HighCpuTemperature))) // TEMP
                {
                    //If the CPU temperature is greater than the peak value we add it to the list
                    if (serverDetailAverage.Temperature > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }

                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.HighNetworkUtilization))) // Network
                {
                    //The same applies to network as with the cpu temperature
                    if (serverDetailAverage.NetworkUtilization > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }
                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.Highcpu))) // CPU
                {
                    //It applies here too
                    if (serverDetailAverage.CPUUtilization > eventType.PeakValue)
                    {
                        var @event = MakeEvent(serverDetailAverage, eventType);
                        eventList.Add(@event);
                    }
                }
                else if (eventType.Name.Equals(et.setName(EventType.Type.LowDiskSpace)))// DISK
                {
                    if (serverDetailAverage.HarddiskTotalSpace > 0)
                    {
                        if ((serverDetailAverage.HarddiskTotalSpace - serverDetailAverage.HarddiskUsedSpace) <
                            eventType.PeakValue)
                        {
                            var @event = MakeEvent(serverDetailAverage, eventType);
                            eventList.Add(@event);
                        }
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
             
            return e;
        }
    }
}