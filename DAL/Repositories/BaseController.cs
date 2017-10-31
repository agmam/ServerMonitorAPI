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
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public T Read(int i)
        {
            throw new NotImplementedException();
        }

        public List<T> ReadAll()
        {
            throw new NotImplementedException();
        }

        public List<T> ReadAllFromServer(int serverId)
        {
            throw new NotImplementedException();
        }

        public T Update(T t)
        {
            throw new NotImplementedException();
        }
    }
}
