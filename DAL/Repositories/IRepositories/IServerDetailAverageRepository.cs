using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace DAL.Repositories.IRepositories
{
    public interface IServerDetailAverageRepository : IRepository<ServerDetailAverage>
    {
        bool GetLatestServerDetailAverage(int inteval, int serverId);
        List<ServerDetailAverage> GetAllServerDetailAveragesForPeriod(int period, int serverId);
    }
}
