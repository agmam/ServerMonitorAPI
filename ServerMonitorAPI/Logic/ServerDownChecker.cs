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

        private IRepository<ServerDetail> sd = new DALFacade().GetCRUDServerDetailRepository();
        private IRepository<Server> serverRepo = new DALFacade().GetCRUDServerRepository();
        private IRepository<EventType> eventTypeRepo = new DALFacade().GetCRUDEventTypeRepository();
        private IRepository<Event> eventRepo = new DALFacade().GetCRUDEventRepository();
        private IRepository<ServerDetailAverage> serverDetailAverageRepo = new DALFacade().GetCRUDServerDetailAverageRepository();
        public void ServerDown()
        {
            var servers = serverRepo.ReadAll();
            var eventTypes = eventTypeRepo.ReadAll();
            var serverdown = DateTime.Now.Subtract(new TimeSpan(0, 0, 0, 10));

            var et = new EventType();
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
                var serverInfo = sd.ReadAllFromServer(server.Id);
                if (serverInfo != null && serverInfo.Count > 0)
                {
                    serverInfo = serverInfo.OrderByDescending(x => x.Created).ToList();
                    serverDetail = serverInfo[0];
                }
                if (serverDetail != null && serverDetail.Created < serverdown)
                {
                    var lastEvent = eventRepo.ReadAllFromServer(server.Id).LastOrDefault();
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
                            eventRepo.Create(@event);
                            var email = new EmailSender();
                            email.SendEmail(new List<Event>()
                            {
                                @event
                            });
                        }
                    }

                }
            }


        }


    }
}