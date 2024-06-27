using Framework.Core.comman;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Interceptors
{
        public class SoftDeleteInterceptor : SaveChangesInterceptor
        {
            public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
                InterceptionResult<int> result, CancellationToken cancellationToken = default)
            {

                foreach (var entry in eventData.Context.ChangeTracker.Entries())
                {
                    if (entry.State is EntityState.Deleted && entry.Entity is ISoftDelete softDeletable)
                    {
                        entry.State = EntityState.Modified;
                        await softDeletable.DeleteAsync();
                    }
                }
                return await base.SavingChangesAsync(eventData, result, cancellationToken);
            }
        }
}
