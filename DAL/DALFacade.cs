using DAL.Repositories;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DALFacade
    {
        public IRepository<Server> GetServerRepository()
        {
            return new ServerRepository();
        }
        public IRepository<ServerDetail> GetServerDetailRepository()
        {
            return new ServerDetailRepository();
        }
        public IRepository<ServerDetailAverage> ServerDetailAverageRepository()
        {
            return new ServerDetailAverageRepository();
        }
        public IRepository<Event> GetEventRepository()
        {
            return new EventRepository();
        }
        public IRepository<EventType> GetEventTypeRepository()
        {
            return new EventTypeRepository();
        }
        public IRepository<EmailRecipient> GetEmailRecipientRepository()
        {
            return new EmailRecipientRepository();
        }
    }
}
