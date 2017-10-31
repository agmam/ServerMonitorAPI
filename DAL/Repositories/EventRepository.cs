using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DB;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class EventRepository : AbstractRepository<Event>
    {
        internal override Event CreateEntity(ServerMonitorContext ctx, Event t)
        {
            t.Created = DateTime.Now;
            var entity = ctx.Events.Add(t);
            ctx.SaveChanges();
            return entity;
        }

        internal override bool DeleteEntity(ServerMonitorContext ctx, int id)
        {
            var entity = ctx.EventTypes.FirstOrDefault(x => x.Id == id);
            if (entity == null) return false;
            ctx.EventTypes.Attach(entity);
            ctx.EventTypes.Remove(entity);
            ctx.SaveChanges();
            return true;
        }

        internal override List<Event> ReadAllEntity(ServerMonitorContext ctx)
        {
            return ctx.Events.ToList();
        }

        internal override List<Event> ReadAllFromServerEntity(ServerMonitorContext ctx, int serverId)
        {
            return null;
        }

        internal override Event ReadEntity(ServerMonitorContext ctx, int id)
        {
            return ctx.Events.FirstOrDefault(x => x.Id == id);
        }

        internal override Event UpdateEntity(ServerMonitorContext ctx, Event t)
        {
            var entity = ctx.Events.AsNoTracking().FirstOrDefault(x => x.Id == t.Id);
            if (entity == null) return null;
            ctx.Events.Attach(t);
            ctx.Entry(t).State = EntityState.Modified;
            ctx.SaveChanges();
            return t;
        }
    }
}