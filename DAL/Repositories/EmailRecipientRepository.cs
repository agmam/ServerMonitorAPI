using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Repositories;
using Entities.Entities;
using DAL.DB;

namespace DAL
{
    internal class EmailRecipientRepository : AbstractRepository<EmailRecipient>
    {
        internal override EmailRecipient CreateEntity(ServerMonitorContext ctx, EmailRecipient t)
        {
            t.Created = DateTime.Now;
            var entity = ctx.EmailRecipients.Add(t);
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

        internal override List<EmailRecipient> ReadAllEntity(ServerMonitorContext ctx)
        {
            return ctx.EmailRecipients.ToList();
        }

        internal override List<EmailRecipient> ReadAllFromServerEntity(ServerMonitorContext ctx, int serverId)
        {
            return null;
        }

        internal override EmailRecipient ReadEntity(ServerMonitorContext ctx, int id)
        {
            return ctx.EmailRecipients.FirstOrDefault(x => x.Id == id);
        }

        internal override EmailRecipient UpdateEntity(ServerMonitorContext ctx, EmailRecipient t)
        {
            var entity = ctx.EmailRecipients.AsNoTracking().FirstOrDefault(x => x.Id == t.Id);
            if (entity == null) return null;
            ctx.EmailRecipients.Attach(t);
            ctx.Entry(t).State = EntityState.Modified;
            ctx.SaveChanges();
            return t;
        }
    }
}