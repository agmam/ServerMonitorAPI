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
        //Server
        public IRepository<Server> GetCRUDServerRepository()
        {
            return new ServerRepository();
        }
     
        //ServerDetail
        public ICustomRepository<ServerDetail> GetCRUDServerDetailRepository()
        {
            return new ServerDetailRepository();
        }


        public IRepository<ServerDetailAverage> GetCRUDServerDetailAverageRepository()
        {
            return new ServerDetailAverageRepository();
        }
        public IRepository<Event> GetCRUDEventRepository()
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
