using Framework.DataAccess.Repo;
using Microsoft.EntityFrameworkCore;
using Service.Core.IRepo;
using Service.DataAccess.Context;
using Service.Entities.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DataAccess.Repo
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
