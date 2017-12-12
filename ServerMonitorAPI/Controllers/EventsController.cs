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

namespace ServerMonitorAPI.Controllers
{
    public class EventsController : ApiController
    {
        private readonly IEventRepository EventRepo = new DALFacade().GetCRUDEventRepository();

        // GET: api/Events
        public List<Event> GetEvents()
        {
            return EventRepo.ReadAll();
        }
        public List<Event> GetAllEventsByRange(long from, long to, int serverId)
        {
            var fromdate = DateTime.FromBinary(from);
            var todate = DateTime.FromBinary(to);


            var e = EventRepo.GetAllEventsByRange(fromdate, todate, serverId);
            return e;
        }
        
        public List<Event> GetEventsFromServer(int id)
        { var e = EventRepo.ReadAllFromServer(id);
            return e;
        }
        // GET: api/Events/5
        [ResponseType(typeof(Event))]
        public IHttpActionResult GetEvent(int id)
        {
            Event @event = EventRepo.Read(id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/Events/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEvent(int id, Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @event.Id)
            {
                return BadRequest();
            }

           
            try
            {
                EventRepo.Update(@event);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Events
        [ResponseType(typeof(Event))]
        public IHttpActionResult PostEvent(Event @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            EventRepo.Create(@event);

            return CreatedAtRoute("DefaultApi", new { id = @event.Id }, @event);
        }

        // DELETE: api/Events/5
        [ResponseType(typeof(Event))]
        public IHttpActionResult DeleteEvent(int id)
        {
            Event @event = EventRepo.Read(id);
            if (@event == null)
            {
                return NotFound();
            }

            EventRepo.Delete(id);

            return Ok(@event);
        }

   

        private bool EventExists(int id)
        {
            return EventRepo.Read(id) != null;
        }
    }
}