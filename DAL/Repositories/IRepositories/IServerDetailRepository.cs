using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace DAL.Repositories
{
    public interface IServerDetailRepository : IRepository<ServerDetail>
    {
        bool DeleteOldServerDetail(int minutes, int serverId);
        
    }
}
