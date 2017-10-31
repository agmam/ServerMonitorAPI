using System.Collections.Generic;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class EventTypeRepository : IRepository<EventType>
    {
        public EventType Create(EventType t)
        {
            throw new System.NotImplementedException();
        }

        public EventType Read(int i)
        {
            throw new System.NotImplementedException();
        }

        public List<EventType> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public List<EventType> ReadAllFromServer(int serverId)
        {
            throw new System.NotImplementedException();
        }

        public EventType Update(EventType t)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}