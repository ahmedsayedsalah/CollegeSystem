using Framework.Core.comman;
using Framework.Core.IRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.DataAccess.Repo
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private DbContext dbContext;
        private DbSet<TEntity> table;

        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            table = dbContext.Set<TEntity>();
        }
        public async Task<IList<TEntity>> GetAll()
        {
            return await table.AsNoTracking().ToListAsync();
        }

        public async Task<IList<TEntity>> GetAllWithIncludes(IEnumerable<string> includes = null)
        {
            var query = table.AsNoTracking().AsQueryable();
            if (includes is not null)
            {
                foreach (var include in includes)
                    query.Include(include);
            }
            return await query.ToListAsync();
        }

        public async Task<TEntity> GetById(object id)
        {
            return await table.FindAsync(id);
        }
        public async Task<IList<TEntity>> GetWithCondition(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
        {
            var query = table.AsQueryable();
            if (includes is not null)
            {
                foreach (var include in includes)
                    query.Include(include);
            }
            return await table.Where(predicate).ToListAsync();
        }
        public async Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, string[] includes = null)
        {
            var query = table.AsQueryable();
            if (includes is not null)
            {
                foreach (var include in includes)
                    query.Include(include);
            }
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<IList<TEntity>> Sort(Func<TEntity, object> order, bool IsAscending = true)
        {
            var query= table;
            if(IsAscending is true)
                table.OrderBy(order);
            else
                table.OrderByDescending(order);
            return await table.ToListAsync();
        }

        public async Task<IList<TEntity>> SortWithCondition(Expression<Func<TEntity, bool>> condition,
            Func<TEntity, object> order, bool IsAscending = true)
        {
            var query = table.Where(condition);

            if (IsAscending == true)
                query.OrderBy(order);
            else
                query.OrderByDescending(order);

            return await query.ToListAsync();
        }

        public async Task<IList<TEntity>> SortWithConditionWithIncludes(Expression<Func<TEntity, bool>> condition,
            Func<TEntity, object> order, bool IsAscending = true, string[] includes = null)
        {
            var query= table.Where(condition).AsQueryable();

            if(includes is not null)
            {
                foreach(var include in includes)
                    query.Include(include);
            }

            if(IsAscending is true)
                query.OrderBy(order);
            else
                query.OrderByDescending(order);

            return await table.ToListAsync();
        }
        public async Task<long> GetCount()
        {
            return await table.LongCountAsync();
        }

        public async Task<long> GetCountWithCondition(Expression<Func<TEntity, bool>> condition)
        {
            return await table.LongCountAsync(condition);
        }

        public async Task<IList<TEntity>> Pagination(int PageIndex, int SizeOfPage)
        {
            return await table
                .Skip((PageIndex-1) *SizeOfPage)
                .Take(SizeOfPage)
                .ToListAsync();
        }
        public async Task AddEntity(TEntity entity)
        {
            if(entity is ICreationTimeSignature obj)
            {
                obj.CreateTime = DateTime.Now;
            }
            await table.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity is ICreationTimeSignature obj)
                {
                    obj.CreateTime = DateTime.Now;
                }
            }
            await table.AddRangeAsync(entities);
        }

        public async Task DeleteEntity(TEntity entity)
        {
            await Task.Run(()=> table.Remove(entity));
        }

        public async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            await Task.Run(async() =>
            {
                foreach (var entity in entities)
                    await DeleteEntity(entity);
            }
            );
        }
        public async Task UpdateEntity(TEntity entity)
        {
           await Task.Run(()=> table.Update(entity));   
        }

        public async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            await Task.Run(async () =>
            {
                foreach (var entity in entities)
                    await UpdateEntity(entity);
            }
            );
        }

        public async ValueTask DisposeAsync()
        {
            await dbContext.DisposeAsync();
        }
    }
}
