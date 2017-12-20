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

    [Authorize]
public class ServersController : ApiController
{
    private IRepository<Server> dbCrud = new DALFacade().GetServerRepository();

    // GET: api/Servers
    public List<Server> GetServers()
    {
        return dbCrud.ReadAll();
    }

    // GET: api/Servers/5
    [ResponseType(typeof(Server))]
    public IHttpActionResult GetServer(int id)
    {
        Server server = dbCrud.Read(id);
        if (server == null)
        {
            return NotFound();
        }

        return Ok(server);
    }

    // PUT: api/Servers/5
    [ResponseType(typeof(void))]
    public IHttpActionResult PutServer(int id, Server server)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != server.Id)
        {
            return BadRequest();
        }
        dbCrud.Update(server);


        return StatusCode(HttpStatusCode.NoContent);
    }

    // POST: api/Servers
    [ResponseType(typeof(Server))]
    public IHttpActionResult PostServer(Server server)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        dbCrud.Create(server);
        return CreatedAtRoute("DefaultApi", new { id = server.Id }, server);
    }

    // DELETE: api/Servers/5
    [ResponseType(typeof(Server))]
    public IHttpActionResult DeleteServer(int id)
    {
        Server server = dbCrud.Read(id);
        if (server == null)
        {
            return NotFound();
        }

        dbCrud.Delete(id);
        return Ok(server);
    }
}
}