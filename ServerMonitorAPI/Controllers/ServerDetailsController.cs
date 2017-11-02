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
using Entities.Entities;

namespace ServerMonitorAPI.Controllers
{
    public class ServerDetailsController : ApiController
    {
        private IRepository<ServerDetail> dbCrud = new DALFacade().GetCRUDServerDetailRepository();

        // GET: api/ServerDetails
        public List<ServerDetail> GetServerDetails()
        {
            return dbCrud.ReadAll();
        }

        // GET: api/ServerDetails/5
        [ResponseType(typeof(ServerDetail))]
        public IHttpActionResult GetServerDetail(int id)
        {
            ServerDetail serverDetail = dbCrud.Read(id);
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
            dbCrud.Update(serverDetail);


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

            Server server = new DALFacade().GetServerRepository().GetServerByName(serverDetail.Server.ServerName);
            if (server == null)
            {
               server = new Server() { ServerName = serverDetail.Server.ServerName };
                server = new DALFacade().GetCRUDServerRepository().Create(server);
            }
             serverDetail.Server = server;
             serverDetail.ServerId = server.Id;
             dbCrud.Create(serverDetail);


            
            
            return StatusCode(HttpStatusCode.OK);
        }

        // DELETE: api/ServerDetails/5
        [ResponseType(typeof(ServerDetail))]
        public IHttpActionResult DeleteServerDetail(int id)
        {
            ServerDetail serverDetail = dbCrud.Read(id);
            if (serverDetail == null)
            {
                return NotFound();
            }

            dbCrud.Delete(id);
            return Ok(serverDetail);
        }

        public List<ServerDetail> GetServerDetails(int id)
        {
            var a = dbCrud.ReadAllFromServer(id);

            return a;
        }
    }
}