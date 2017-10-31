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
                using (var ctx = new ServerMonitorContext())
                {
                    var t = ReadEntity(ctx, id);
                }
                return t;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<T> ReadAll()
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

        public List<T> ReadAllFromServer(int serverId)
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

        public T Update(T t)
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
    }
}
