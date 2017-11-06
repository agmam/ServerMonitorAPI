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
    internal class ServerDetailAverageRepository : AbstractRepository<ServerDetailAverage>, IServerDetailAverageRepository
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public bool GetLatestServerDetailAverage(int interval, int serverId)
        {
            try
            {
                using (var ctx = new ServerMonitorContext())
                {
                    var date = DateTime.Now.AddMinutes(-interval) ;
                    return ctx.ServerDetailAverages.Count(x => x.Created < DateTime.Now
                                                                     && x.Created > date && x.ServerId == serverId) > 0;
                }
            }
            catch (Exception e)
            {
                log.Error("Database error - GetLatestServerDetailAverage:", e);
                throw;
            }
        }

        public List<ServerDetailAverage> GetAllServerDetailAveragesForPeriod(int period, int serverId)
        {
                var list = new List<ServerDetailAverage>();
            try
            {
                using (var ctx = new ServerMonitorContext())
                {
                    var date = DateTime.Now.AddHours(-period);
                    list =  ctx.ServerDetailAverages.Where(x => x.Created < DateTime.Now
                                                                     && x.Created > date && x.ServerId == serverId).ToList();
                    return list;
                }
            }
            catch (Exception e)
            {
                log.Error("Database error - GetLatestServerDetailAverage:", e);
                throw;
            }

        }

        internal override ServerDetailAverage CreateEntity(ServerMonitorContext ctx, ServerDetailAverage s)
        {
            
            var entity = ctx.ServerDetailAverages.Add(s);
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