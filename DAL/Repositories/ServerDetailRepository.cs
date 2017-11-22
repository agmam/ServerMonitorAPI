using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.DB;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class ServerDetailRepository : AbstractRepository<ServerDetail>, IServerDetailRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        internal override ServerDetail CreateEntity(ServerMonitorContext ctx, ServerDetail s)
        {
            s.Created = DateTime.Now;
            ctx.Entry(s.Server).State = EntityState.Unchanged;
            var entity = ctx.ServerDetails.Add(s);
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


        public bool DeleteOldServerDetail(int minutes, int serverId)
        {
            try
            {
                using (var ctx = new ServerMonitorContext())
                {

                    var cutoff = DateTime.Now.Subtract(new TimeSpan(0, minutes, 0));
                    var oldDetails = ctx.ServerDetails.Where(a => a.Created < cutoff);
                    if (oldDetails.ToList().Count > 0)
                    {
                        ctx.ServerDetails.RemoveRange(oldDetails);  
                        ctx.SaveChanges();
                   
                    }
                     return true;
                }
            }
            catch (Exception e)
            {
                return false;
                log.Error("Database error - GetLatestServerDetailAverage:", e);
                
            }
            return false;
        }
    }
}