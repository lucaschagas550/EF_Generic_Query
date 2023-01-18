using EF.Generic_Query.API.Data.Repositories.Interfaces;
using EF.Generic_Query.API.Data.Repositories.Pagination;
using EF.Generic_Query.API.Data.Repositories.QueryBuilder;
using EF.Generic_Query.API.Data.Uow.Interfaces;
using EF.Generic_Query.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EF.Generic_Query.API.Data.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        private readonly IUnitOfWork _uow;
        protected readonly AppDbContext _context;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(IUnitOfWork uow, AppDbContext context)
        {
            _uow = uow;
            _context = context;
            dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> FindOne(
          Expression<Func<TEntity, bool>> where,
          Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null)
        {
            var query = Specify(where, includes, null);
            return await query.FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<TView> FindOne<TView>(
            Expression<Func<TEntity, TView>> selector,
            Expression<Func<TEntity, bool>> where,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null)
        {
            var query = Specify(where, includes, null);
            return await query.Select(selector).FirstOrDefaultAsync().ConfigureAwait(false);
        }

        public async Task<List<TEntity>> Find(
            Expression<Func<TEntity, bool>> where = null,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null,
            Func<OrderBuilder<TEntity>, OrderBuilder<TEntity>> order = null)
        {
            var query = Specify(where, includes, order);

            return await query.ToListAsync().ConfigureAwait(false);
        }

        public async Task<List<TView>> Find<TView>(
            Expression<Func<TEntity, TView>> selector,
            Expression<Func<TEntity, bool>> where = null,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null,
            Func<OrderBuilder<TEntity>, OrderBuilder<TEntity>> order = null)
        {
            var query = Specify(where, includes, order);

            return await query.Select(selector).ToListAsync().ConfigureAwait(false);
        }

        public async Task<Page<TEntity>> Find(
            Pageable pageable,
            Expression<Func<TEntity, bool>> where = null,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null,
            Func<OrderBuilder<TEntity>, OrderBuilder<TEntity>> order = null)
        {
            var query = Specify(where, includes, order);

            var content = Paginate(query, pageable).ToList();

            var total = query.Count();

            return new Page<TEntity>(total, content, pageable);
        }

        public async Task<Page<TView>> Find<TView>(
            Expression<Func<TEntity, TView>> selector,
            Pageable pageable,
            Expression<Func<TEntity, bool>> where = null,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null,
            Func<OrderBuilder<TEntity>, OrderBuilder<TEntity>> order = null)
        {
            var query = Specify(where, includes, order);

            var content = await Paginate(query, pageable).Select(selector).ToListAsync().ConfigureAwait(false);

            var total = query.Count();

            return new Page<TView>(total, content, pageable);
        }

        public async Task<TEntity> Create(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Added;

                var result = dbSet.Add(entity);

                await _uow.Commit().ConfigureAwait(false);

                _context.Entry(entity).State = EntityState.Detached;

                return result.Entity;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<TEntity> Update(TEntity entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;

                var result = dbSet.Update(entity);

                await _uow.Commit().ConfigureAwait(false);

                _context.Entry(entity).State = EntityState.Detached;

                return result.Entity;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var entity = dbSet.Where(where).FirstOrDefault();

                if (entity != null)
                {
                    var result = dbSet.Remove(entity);

                    await _uow.Commit().ConfigureAwait(false);

                    _context.Entry(entity).State = EntityState.Deleted;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        private IQueryable<TEntity> Specify(
            Expression<Func<TEntity, bool>> where = null,
            Func<IncludeBuilder<TEntity>, IncludeBuilder<TEntity>> includes = null,
            Func<OrderBuilder<TEntity>, OrderBuilder<TEntity>> order = null)
        {

            var query = AsQueryable();

            var includer = new IncludeBuilder<TEntity>(query);
            var ordenator = new OrderBuilder<TEntity>(query);

            if (includes != null)
            {
                query = includes(includer).build();
            }

            if (where != null)
            {
                query = query.Where(where);
            }

            if (order != null)
            {
                query = order(ordenator).build();
            }

            return query;
        }

        public IQueryable<TEntity> AsQueryable()
        {
            return dbSet
                .AsQueryable()
                .AsNoTracking();
        }

        private IQueryable<TEntity> Paginate(IQueryable<TEntity> query, Pageable pageable)
        {
            return query
                .Skip((pageable.Page - 1) * pageable.Size)
                     .Take(pageable.Size);
        }
    }
}
