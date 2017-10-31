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
        private IRepository<ServerDetail> db = new DALFacade().GetServerDetailRepository();

        // GET: api/ServerDetails
        public List<ServerDetail> GetServerDetails()
        {
            return db.ReadAll();
        }

        // GET: api/ServerDetails/5
        [ResponseType(typeof(ServerDetail))]
        public IHttpActionResult GetServerDetail(int id)
        {
            ServerDetail serverDetail = db.Read(id);
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
            db.Update(serverDetail);


            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ServerDetails
        [ResponseType(typeof(ServerDetail))]
        public IHttpActionResult PostServerDetail(ServerDetail serverDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Create(serverDetail);
            return CreatedAtRoute("DefaultApi", new { id = serverDetail.Id }, serverDetail);
        }

        // DELETE: api/ServerDetails/5
        [ResponseType(typeof(ServerDetail))]
        public IHttpActionResult DeleteServerDetail(int id)
        {
            ServerDetail serverDetail = db.Read(id);
            if (serverDetail == null)
            {
                return NotFound();
            }

            db.Delete(id);
            return Ok(serverDetail);
        }

        public List<ServerDetail> GetServerDetails(int id)
        {
            var a = db.ReadAllFromServer(id);

            return a;
        }
    }
}