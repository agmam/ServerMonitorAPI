using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DB;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class ServerDetailAverageRepository : AbstractRepository<ServerDetailAverage>
    {
        internal override ServerDetailAverage CreateEntity(ServerMonitorContext ctx, ServerDetailAverage t)
        {
            t.Created = DateTime.Now;
            var entity = ctx.ServerDetailAverages.Add(t);
            ctx.SaveChanges();
            return entity;
        }

        internal override bool DeleteEntity(ServerMonitorContext ctx, int id)
        {
            var entity = ctx.ServerDetailAverages.FirstOrDefault(x => x.Id == id);
            if (entity == null) return false;
            ctx.ServerDetailAverages.Attach(entity);
            ctx.ServerDetailAverages.Remove(entity);
            ctx.SaveChanges();
            return true;
        }

        internal override List<ServerDetailAverage> ReadAllEntity(ServerMonitorContext ctx)
        {
            return ctx.ServerDetailAverages.ToList();
        }

        internal override List<ServerDetailAverage> ReadAllFromServerEntity(ServerMonitorContext ctx, int serverId)
        {
            return ctx.ServerDetailAverages.Where(x => x.ServerId == serverId).ToList();

        }

        internal override ServerDetailAverage ReadEntity(ServerMonitorContext ctx, int id)
        {
            return ctx.ServerDetailAverages.FirstOrDefault(x => x.Id == id);
        }

        internal override ServerDetailAverage UpdateEntity(ServerMonitorContext ctx, ServerDetailAverage t)
        {
            var entity = ctx.ServerDetailAverages.AsNoTracking().FirstOrDefault(x => x.Id == t.Id);
            if (entity == null) return null;
            ctx.ServerDetailAverages.Attach(t);
            ctx.Entry(t).State = EntityState.Modified;
            ctx.SaveChanges();
            return t;
        }
    }
}