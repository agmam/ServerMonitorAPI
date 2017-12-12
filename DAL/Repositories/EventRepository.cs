using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DB;
using DAL.Repositories;
using DAL.Repositories.IRepositories;
using Entities.Entities;

namespace DAL
{
    internal class EventRepository : AbstractRepository<Event>, IEventRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal override Event CreateEntity(ServerMonitorContext ctx, Event s)
        {
           
            ctx.Entry(s.EventType).State = EntityState.Unchanged;

            var entity = ctx.Events.Add(s);
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
            var events = ctx.Events.Where(x => x.ServerId == serverId).OrderByDescending(x => x.Created).Take(5).Include(x => x.EventType).ToList();
            return events;
        }

        internal override Event ReadEntity(ServerMonitorContext ctx, int id)
        {
            return ctx.Events.Include(x => x.ServerDetailAverage).Include(x => x.EventType).Include(x => x.Server).FirstOrDefault(x => x.Id == id);
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


        public List<Event> GetAllEventsByRange(DateTime @from, DateTime to, int serverId)
        {
            var list = new List<Event>();
            try
            {
                using (var ctx = new ServerMonitorContext())
                {
                    list = ctx.Events.Where(x => x.Created < to
                                                               && x.Created > from && x.ServerId == serverId).Include(x=>x.EventType).Include(x=>x.ServerDetailAverage).ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                log.Error("Database error - GetAllEventsByRange:", e);
                throw;
            }
        }
    }
}