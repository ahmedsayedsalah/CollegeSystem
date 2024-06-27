using Framework.Core.IRepo;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Core.IRepo
{
    public interface IUserRepository : IGenericRepository<User>
    {
    }
}
