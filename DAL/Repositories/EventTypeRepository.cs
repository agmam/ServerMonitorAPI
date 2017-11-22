using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DB;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class EventTypeRepository : AbstractRepository<EventType>
    {
        internal override EventType CreateEntity(ServerMonitorContext ctx, EventType s)
        {
            s.Created = DateTime.Now;
            var entity = ctx.EventTypes.Add(s);
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

        internal override List<EventType> ReadAllEntity(ServerMonitorContext ctx)
        {
            return ctx.EventTypes.ToList();
        }

        internal override List<EventType> ReadAllFromServerEntity(ServerMonitorContext ctx, int serverId)
        {
            return null;
        }

        internal override EventType ReadEntity(ServerMonitorContext ctx, int id)
        {
            return ctx.EventTypes.FirstOrDefault(x => x.Id == id);
        }

        internal override EventType UpdateEntity(ServerMonitorContext ctx, EventType t)
        {
            var entity = ctx.EventTypes.AsNoTracking().FirstOrDefault(x => x.Id == t.Id);
            if (entity == null) return null;
            t.Created = entity.Created;
            ctx.EventTypes.Attach(t);
            ctx.Entry(t).State = EntityState.Modified;
            ctx.SaveChanges();
            return t;
        }
    }
}