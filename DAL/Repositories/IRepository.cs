using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IRepository<T> where T : Entity
    {
        T Create(T t);
        T Read(int i);
        List<T> ReadAll();
        List<T> ReadAllFromServer(int serverId);
        T Update(T t);
        bool Delete(int id);


    }
}
