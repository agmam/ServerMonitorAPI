﻿using System;
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
    public class ServerDetailAveragesController : ApiController
    {
        private IRepository<ServerDetailAverage> db = new DALFacade().ServerDetailAverageRepository();

        // GET: api/ServerDetailAverages
        public List<ServerDetailAverage> GetServerDetailAverages()
        {
            return db.ReadAll();
        }

        // GET: api/ServerDetailAverages/5
        [ResponseType(typeof(ServerDetailAverage))]
        public IHttpActionResult GetServerDetailAverage(int id)
        {
            ServerDetailAverage serverDetailAverage = db.Read(id);
            if (serverDetailAverage == null)
            {
                return NotFound();
            }

            return Ok(serverDetailAverage);
        }

        // PUT: api/ServerDetailAverages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServerDetailAverage(int id, ServerDetailAverage serverDetailAverage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != serverDetailAverage.Id)
            {
                return BadRequest();
            }
            db.Update(serverDetailAverage);


            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ServerDetailAverages
        [ResponseType(typeof(ServerDetailAverage))]
        public IHttpActionResult PostServerDetailAverage(ServerDetailAverage serverDetailAverage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Create(serverDetailAverage);
           return CreatedAtRoute("DefaultApi", new { id = serverDetailAverage.Id }, serverDetailAverage);
        }

        // DELETE: api/ServerDetailAverages/5
        [ResponseType(typeof(ServerDetailAverage))]
        public IHttpActionResult DeleteServerDetailAverage(int id)
        {
            ServerDetailAverage serverDetailAverage = db.Read(id);
            if (serverDetailAverage == null)
            {
                return NotFound();
            }

            db.Delete(id);
            return Ok(serverDetailAverage);
        }
        public List<ServerDetailAverage> GetServerDetailAverages(int id)
        {
            var a = db.ReadAllFromServer(id);

            return a;
        }

    }
}