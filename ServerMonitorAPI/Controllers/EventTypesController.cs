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
    public class EventTypesController : ApiController
    {
        private IRepository<EventType> db = new DALFacade().GetCRUDEventTypeRepository();

        // GET: api/EventTypes
        public List<EventType> GetEventTypes()
        {
            return db.ReadAll();
        }

        // GET: api/EventTypes/5
        [ResponseType(typeof(EventType))]
        public IHttpActionResult GetEventType(int id)
        {
            EventType eventType = db.Read(id);
            if (eventType == null)
            {
                return NotFound();
            }

            return Ok(eventType);
        }

        // PUT: api/EventTypes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEventType(int id, EventType eventType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != eventType.Id)
            {
                return BadRequest();
            }
            db.Update(eventType);


            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/EventTypes
        [ResponseType(typeof(EventType))]
        public IHttpActionResult PostEventType(EventType eventType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Create(eventType);
            return CreatedAtRoute("DefaultApi", new { id = eventType.Id }, eventType);
        }

        // DELETE: api/EventTypes/5
        [ResponseType(typeof(EventType))]
        public IHttpActionResult DeleteEventType(int id)
        {
            EventType eventType = db.Read(id);
            if (eventType == null)
            {
                return NotFound();
            }

            db.Delete(id);
            return Ok(eventType);
        }

        public List<EventType> GetEventTypes(int id)
        {
            var a = db.ReadAllFromServer(id);

            return a;
        }
    }
}