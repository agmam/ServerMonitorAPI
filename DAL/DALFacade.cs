using DAL.Repositories;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repositories.IRepositories;

namespace DAL
{
    public class DALFacade
    {
        //Server
        public IRepository<Server> GetServerRepository()
        {
            return new ServerRepository();
        }
     
        //ServerDetail
        public IServerDetailRepository GetServerDetailRepository()
        {
            return new ServerDetailRepository();
        }

        public IServerDetailAverageRepository GetServerDetailAverageRepository()
        {
            return new ServerDetailAverageRepository();
        }

        public IEventRepository GetEventRepository()
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
