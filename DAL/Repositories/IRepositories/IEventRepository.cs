using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace DAL.Repositories.IRepositories
{
    public interface IEventRepository: IRepository<Event>
    {
        List<Event> GetAllEventsByRange(DateTime from, DateTime to, int serverId);
    }
}
