using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DB;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class ServerDetailRepository : AbstractRepository<ServerDetail>
    {
        internal override ServerDetail CreateEntity(ServerMonitorContext ctx, ServerDetail t)
        {
            t.Created = DateTime.Now;
            var entity = ctx.ServerDetails.Add(t);
            ctx.SaveChanges();
            return entity;
        }

        internal override bool DeleteEntity(ServerMonitorContext ctx, int id)
        {
            var entity = ctx.ServerDetails.FirstOrDefault(x => x.Id == id);
            if (entity == null) return false;
            ctx.ServerDetails.Attach(entity);
            ctx.ServerDetails.Remove(entity);
            ctx.SaveChanges();
            return true;
        }

        internal override List<ServerDetail> ReadAllEntity(ServerMonitorContext ctx)
        {
            return ctx.ServerDetails.ToList();
        }

        internal override List<ServerDetail> ReadAllFromServerEntity(ServerMonitorContext ctx, int serverId)
        {
            return ctx.ServerDetails.Where(x=>x.ServerId == serverId).ToList();
        }

        internal override ServerDetail ReadEntity(ServerMonitorContext ctx, int id)
        {
            return ctx.ServerDetails.FirstOrDefault(x => x.Id == id);
        }

        internal override ServerDetail UpdateEntity(ServerMonitorContext ctx, ServerDetail t)
        {
            var entity = ctx.ServerDetails.AsNoTracking().FirstOrDefault(x => x.Id == t.Id);
            if (entity == null) return null;
            ctx.ServerDetails.Attach(t);
            ctx.Entry(t).State = EntityState.Modified;
            ctx.SaveChanges();
            return t;
        }
    }
}