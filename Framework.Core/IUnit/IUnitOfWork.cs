using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.IUnit
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task BeginTransaction();
        Task RollbackTransaction();
        Task CommitTransaction();
        Task SaveChanges();
    }
}
