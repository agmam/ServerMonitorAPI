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
        public IRepository<Server> GetCRUDServerRepository()
        {
            return new ServerRepository();
        }
     
        //ServerDetail
        public IServerDetailRepository GetCRUDServerDetailRepository()
        {
            return new ServerDetailRepository();
        }

        public IServerDetailAverageRepository GetCRUDServerDetailAverageRepository()
        {
            return new ServerDetailAverageRepository();
        }

        public IEventRepository GetCRUDEventRepository()
        {
            return new EventRepository();
        }
        public IRepository<EventType> GetCRUDEventTypeRepository()
        {
            return new EventTypeRepository();
        }
        public IRepository<EmailRecipient> GetCRUDEmailRecipientRepository()
        {
            return new EmailRecipientRepository();
        }
    }
}
