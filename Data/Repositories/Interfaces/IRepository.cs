using EF.Generic_Query.API.Data.Repositories.Pagination;
using EF.Generic_Query.API.Data.Repositories.QueryBuilder;
using EF.Generic_Query.API.Models;
using System.Linq.Expressions;

namespace EF.Generic_Query.API.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> FindOne(
            Expression<Func<TEntity, bool>> where,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null);


        Task<TView> FindOne<TView>(
            Expression<Func<TEntity, TView>> selector,
            Expression<Func<TEntity, bool>> where,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null);

        Task<List<TEntity>> Find(
            Expression<Func<TEntity, bool>> where = null,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null,
            Func<OrderBuilder<TEntity>, OrderBuilder<TEntity>> order = null);

        Task<List<TView>> Find<TView>(
            Expression<Func<TEntity, TView>> selector,
            Expression<Func<TEntity, bool>> where = null,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null,
            Func<OrderBuilder<TEntity>, OrderBuilder<TEntity>> order = null);

        Task<Page<TEntity>> Find(
             Pageable pageable,
             Expression<Func<TEntity, bool>> where = null,
             Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null,
             Func<OrderBuilder<TEntity>, OrderBuilder<TEntity>> order = null);

        Task<Page<TView>> Find<TView>(
            Expression<Func<TEntity, TView>> selector,
            Pageable pageable,
            Expression<Func<TEntity, bool>> where = null,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null,
            Func<OrderBuilder<TEntity>, OrderBuilder<TEntity>> order = null);

        Task<TEntity> Create(TEntity entity);

        Task<TEntity> Update(TEntity entity);

        Task Delete(Expression<Func<TEntity, bool>> where);

        IQueryable<TEntity> AsQueryable();
    }
}
