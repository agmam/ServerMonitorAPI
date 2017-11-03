using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Entities;

namespace DAL.Repositories
{
    public interface ICustomRepository<T>: IRepository<T>
    {
        T GetServerByName(string name);
    }
}
