using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.IRepo
{
    public interface IGenericRepository<TEntity> : IAsyncDisposable where TEntity : class
    {
        Task<IList<TEntity>> GetAll();
        Task<IList<TEntity>> GetAllWithIncludes(IEnumerable<string> includes = null);
        Task<TEntity> GetById(object id);
        Task<TEntity> FirstOrDefault(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        Task<IList<TEntity>> GetWithCondition(Expression<Func<TEntity, bool>> predicate, string[] includes = null);
        Task<IList<TEntity>> Sort(Func<TEntity, object> order, bool IsAscending = true);
        Task<IList<TEntity>> SortWithCondition(Expression<Func<TEntity, bool>> condition, Func<TEntity, object> order, bool IsAscending = true);
        Task<IList<TEntity>> SortWithConditionWithIncludes(Expression<Func<TEntity, bool>> condition, Func<TEntity, object> order,
            bool IsAscending = true, string[] includes = null);
        Task<IList<TEntity>> Pagination(int PageIndex, int SizeOfPage);
        Task<long> GetCount();
        Task<long> GetCountWithCondition(Expression<Func<TEntity, bool>> condition);
        Task AddEntity(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task UpdateEntity(TEntity entity);
        Task UpdateRange(IEnumerable<TEntity> entities);
        Task DeleteEntity(TEntity entity);
        Task DeleteRange(IEnumerable<TEntity> entities);
    }
}
