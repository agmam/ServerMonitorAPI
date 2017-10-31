using System.Collections.Generic;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class ServerDetailRepository : IRepository<ServerDetail>
    {
        public ServerDetail Create(ServerDetail t)
        {
            throw new System.NotImplementedException();
        }

        public ServerDetail Read(int i)
        {
            throw new System.NotImplementedException();
        }

        public List<ServerDetail> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public List<ServerDetail> ReadAllFromServer(int serverId)
        {
            throw new System.NotImplementedException();
        }

        public ServerDetail Update(ServerDetail t)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}