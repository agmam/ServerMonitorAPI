using DAL.DB;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public abstract class AbstractRepository<T> : IRepository<T> where T : Entity
    {
        public T Create(T t)
        {
            try
            {
                using (var ctx = new ServerMonitorContext())
                {
                    t = CreateEntity(ctx, t);
                }
                return t;
            }
            catch (Exception e)
            {
                return null;
            }
           
        }
        internal abstract T CreateEntity(ServerMonitorContext ctx, T t);

        public bool Delete(int id)
        {
            try
            {
                bool deleted;
                using (var ctx = new ServerMonitorContext())
                {
                    deleted = DeleteEntity(ctx, id);
                }
                return deleted;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        internal abstract bool DeleteEntity(ServerMonitorContext ctx, int id);

        public T Read(int id)
        {
            try
            {
                T t;
                using (var ctx = new ServerMonitorContext())
                {
                    t = ReadEntity(ctx, id);
                }
                return t;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal abstract T ReadEntity(ServerMonitorContext ctx, int id);

        public List<T> ReadAll()
        {
            try
            {
                List<T> t;
                using (var ctx = new ServerMonitorContext())
                {
                    t = ReadAllEntity(ctx);
                }
                return t;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal abstract List<T> ReadAllEntity(ServerMonitorContext ctx);

        public List<T> ReadAllFromServer(int serverId)
        {
            try
            {
                List<T> t;
                using (var ctx = new ServerMonitorContext())
                {
                    t = ReadAllFromServerEntity(ctx, serverId);
                }
                return t;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal abstract List<T> ReadAllFromServerEntity(ServerMonitorContext ctx, int serverId);

        public T Update(T t)
        {
            try
            {
                T entity;
                using (var ctx = new ServerMonitorContext())
                {
                    entity = UpdateEntity(ctx, t);
                }
                return entity;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        internal abstract T UpdateEntity(ServerMonitorContext ctx, T t);
    }
}
