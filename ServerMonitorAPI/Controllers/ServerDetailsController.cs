using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DAL;
using DAL.DB;
using DAL.Repositories;
using DAL.Repositories.IRepositories;
using Entities.Entities;
using ServerMonitorAPI.Logic;

namespace ServerMonitorAPI.Controllers
{
    public class ServerDetailsController : ApiController
    {
        private IServerDetailRepository serverDetailDB = new DALFacade().GetCRUDServerDetailRepository();
        private IServerDetailAverageRepository serverDetailAverageDB = new DALFacade().GetCRUDServerDetailAverageRepository();

        private static int INTERVAL = 5;

        // GET: api/ServerDetails
        public List<ServerDetail> GetServerDetails()
        {
            return serverDetailDB.ReadAll();

        }

        // GET: api/ServerDetails/5
        [ResponseType(typeof(ServerDetail))]
        public IHttpActionResult GetServerDetail(int id)
        {
            ServerDetail serverDetail = serverDetailDB.Read(id);
            if (serverDetail == null)
            {
                return NotFound();
            }

            return Ok(serverDetail);
        }

        // PUT: api/ServerDetails/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServerDetail(int id, ServerDetail serverDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serverDetail.Id)
            {
                return BadRequest();
            }
            serverDetailDB.Update(serverDetail);


            return StatusCode(HttpStatusCode.NoContent);
        }

        [Authorize]
        // POST: api/ServerDetails
        [ResponseType(typeof(ServerDetail))]
        public IHttpActionResult PostServerDetail(ServerDetail serverDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Server server = new DALFacade().GetCRUDServerRepository().Read(serverDetail.ServerId);
            if (server == null)
            {
                return BadRequest("no server with id:" + serverDetail.ServerId);
            }
            serverDetail.Server = server;
            serverDetailDB.Create(serverDetail);

            serverDetailDB.DeleteOldServerDetail(INTERVAL, server.Id);

            bool isCreated = serverDetailAverageDB.GetLatestServerDetailAverage(INTERVAL, server.Id);

            if (!isCreated)
            {
                List<ServerDetail> serverDetails = serverDetailDB.ReadAll() ?? new List<ServerDetail>();
                if (serverDetails.Count > 0)
                {
                    var serverDetailAverage = new ServerDetailAverage();
                    serverDetailAverage.ServerId = server.Id;
                    serverDetailAverage.Created = GetStartOfInterval(DateTime.Now);
                    serverDetailAverage.CPUUtilization = serverDetails.Average(x => x.CPUUtilization);
                    serverDetailAverage.BytesReceived = serverDetails.Sum(x => x.BytesReceived);
                    serverDetailAverage.NetworkUtilization = serverDetails.Average(x => x.NetworkUtilization);
                    serverDetailAverage.HarddiskTotalSpace = serverDetails.Average(x => x.HarddiskTotalSpace);
                    serverDetailAverage.HarddiskUsedSpace = serverDetails.Average(x => x.HarddiskUsedSpace);
                    serverDetailAverage.BytesSent = serverDetails.Sum(x => x.BytesSent);
                    serverDetailAverage.Handles = serverDetails.Average(x => x.Handles);
                    serverDetailAverage.Processes = serverDetails.Average(x => x.Processes);
                    serverDetailAverage.RAMAvailable = serverDetails.Average(x => x.RAMAvailable);
                    serverDetailAverage.RAMTotal = serverDetails.Average(x => x.RAMTotal);
                    serverDetailAverage.UpTime = serverDetails.LastOrDefault().UpTime;
                    serverDetailAverage.Temperature = serverDetails.Average(x => x.Temperature);
                   var serverdetailavarage = serverDetailAverageDB.Create(serverDetailAverage);
                    
                    EventChecker checker = new EventChecker();
                    var ev = checker.CheckForEvent(serverdetailavarage);
                   
                    if (ev !=null)
                    {
                      EmailSender es = new EmailSender();
                        es.SendEmail(ev);
                    }


                }
            }


            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/ServerDetails/5
        [ResponseType(typeof(ServerDetail))]
        public IHttpActionResult DeleteServerDetail(int id)
        {
            ServerDetail serverDetail = serverDetailDB.Read(id);
            if (serverDetail == null)
            {
                return NotFound();
            }

            serverDetailDB.Delete(id);
            return Ok(serverDetail);
        }

        public List<ServerDetail> GetServerDetailsFromServer(int id)
        {
            var a = serverDetailDB.ReadAllFromServer(id);

            return a;
        }

        /// <summary>
        /// new class logic 
        /// </summary>
        /// <param name="minutes"></param>
        /// <returns></returns>
        public DateTime GetStartOfInterval(DateTime now)
        {
            var mins = now.Minute / INTERVAL;
            mins *= INTERVAL;
            DateTime time = new DateTime(now.Year, now.Month, now.Day, now.Hour, mins, 0);
            return time;
        }
    }
}