using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using DAL;
using DAL.Repositories;
using Entities.Entities;
using ServerMonitorAPI.Controllers;

namespace ServerMonitorAPI.Logic
{
    public class ServerDownChecker
    {

        private IRepository<ServerDetail> sd = new DALFacade().GetServerDetailRepository();
        private IRepository<Server> serverRepo = new DALFacade().GetServerRepository();
        private IRepository<EventType> eventTypeRepo = new DALFacade().GetEventTypeRepository();
        private IRepository<Event> eventRepo = new DALFacade().GetEventRepository();
        private IRepository<ServerDetailAverage> serverDetailAverageRepo = new DALFacade().GetServerDetailAverageRepository();
        public void ServerDown()
        {
            var servers = serverRepo.ReadAll();
            var eventTypes = eventTypeRepo.ReadAll();
            //This is the time we use, to see if the serverdetail we have
            //is older than time now minus 10 seconds
            var serverdown = DateTime.Now.Subtract(new TimeSpan(0, 0, 0, 10));

            var et = new EventType();
            //Then we set the event type
            foreach (var eventType in eventTypes)
            {
                if (eventType.Name.Equals(et.setName(EventType.Type.ServerDown)))
                {
                    et = eventType;
                    break;
                }
            }
            foreach (var server in servers)
            {
                ServerDetail serverDetail = null;
                //Reads the serverdetail with the server id to get the server
                var serverInfo = sd.ReadAllFromServer(server.Id);
                //So if we have some serverdetail
                if (serverInfo != null && serverInfo.Count > 0)
                {
                    //We order by decending to get the last datetime
                    serverInfo = serverInfo.OrderByDescending(x => x.Created).ToList();
                    //And gets the first in the list, which is the newest date
                    serverDetail = serverInfo[0];
                }
                //If we have a serverDetail and it's created is 10 seconds older than the serverDown 
                if (serverDetail != null && serverDetail.Created < serverdown)
                {
                    //Then we read the last event
                    var lastEvent = eventRepo.ReadAllFromServer(server.Id).LastOrDefault(x => x.EventType.Name == et.Name);
                    //If the event type for the server has been created within an hour
                    //the following will not be executed
                    //But if the event is more than an hour old a new event will be triggered
                    if (lastEvent?.Created  < DateTime.Now.Subtract(new TimeSpan(0,1,0,0)) || lastEvent == null)
                    {
                        var serverDetailAverage =
                       serverDetailAverageRepo.ReadAllFromServer(server.Id).LastOrDefault();
                        if (serverDetailAverage != null)
                        {
                            var @event = new Event()
                            {
                                EventType = et,
                                Created = DateTime.Now,
                                Server = serverDetail.Server,
                                EventTypeId = et.Id,
                                ServerId = serverDetail.ServerId,
                                ServerDetailAverage = serverDetailAverage,
                                ServerDetailAverageId = serverDetailAverage.Id,

                            };
                            //Lastly we create the event 
                            //And sends an email
                            eventRepo.Create(@event);
                            //email.SendEmail(new List<Event>()
                            //{
                            //    @event
                            //});
                        }
                    }

                }
            }


        }


    }
}