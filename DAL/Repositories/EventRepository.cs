using System.Collections.Generic;
using DAL.Repositories;
using Entities.Entities;

namespace DAL
{
    internal class EventRepository : IRepository<Event>
    {
        public Event Create(Event t)
        {
            throw new System.NotImplementedException();
        }

        public Event Read(int i)
        {
            throw new System.NotImplementedException();
        }

        public List<Event> ReadAll()
        {
            throw new System.NotImplementedException();
        }

        public List<Event> ReadAllFromServer(int serverId)
        {
            throw new System.NotImplementedException();
        }

        public Event Update(Event t)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }
    }
}