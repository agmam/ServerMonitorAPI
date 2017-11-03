using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DB;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class ServerRepository : AbstractRepository<Server>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Server GetServerByName(string name)
        {
            try
            {
                Server s;
                using (var ctx = new ServerMonitorContext())
                {
                    s = ctx.Servers.FirstOrDefault(x => x.ServerName == name);
                }
                return s;
            }
            catch (Exception e)
            {
                log.Error("AbstractRepository Read: " + e.Message);
                return null;
            }
        }

        internal override Server CreateEntity(ServerMonitorContext ctx, Server s)
        {
            s.Created = DateTime.Now;
            var entity = ctx.Servers.Add(s);
            ctx.SaveChanges();
            return entity;
        }

        internal override bool DeleteEntity(ServerMonitorContext ctx, int id)
        {
            var entity = ctx.Servers.FirstOrDefault(x => x.Id == id);
            if (entity == null) return false;
            ctx.Servers.Attach(entity);
            ctx.Servers.Remove(entity);
            ctx.SaveChanges();
            return true;
        }

        internal override List<Server> ReadAllEntity(ServerMonitorContext ctx)
        {
            return ctx.Servers.ToList();
        }

        internal override List<Server> ReadAllFromServerEntity(ServerMonitorContext ctx, int serverId)
        {
            return null;
        }

        internal override Server ReadEntity(ServerMonitorContext ctx, int id)
        {
            return ctx.Servers.FirstOrDefault(x => x.Id == id);
        }

        internal override Server UpdateEntity(ServerMonitorContext ctx, Server t)
        {
            var entity = ctx.Servers.AsNoTracking().FirstOrDefault(x => x.Id == t.Id);
            if (entity == null) return null;
            ctx.Servers.Attach(t);
            ctx.Entry(t).State = EntityState.Modified;
            ctx.SaveChanges();
            return t;
        }
    }
}