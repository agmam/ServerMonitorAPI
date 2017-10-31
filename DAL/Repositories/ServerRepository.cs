using System.Collections.Generic;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class ServerRepository : IRepository<Server>
    {
        public Server Create(Server t)
        {
            throw new System.NotImplementedException();
        }

        public Server Read(int i)
        {
            throw new System.NotImplementedException();
        }

        public List<Server> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public List<Server> ReadAllFromServer(int serverId)
        {
            throw new System.NotImplementedException();
        }

        public Server Update(Server t)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}