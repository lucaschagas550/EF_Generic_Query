using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using EF.Generic_Query.API.Models;

namespace EF.Generic_Query.API.Data.Repositories.QueryBuilder
{
    public class IncludeBuilder<T> where T : Entity
    {
        private IQueryable<T> queryable;

        public IncludeBuilder(IQueryable<T> queryable)
        {
            this.queryable = queryable;
        }

        public IncludeBuilder<T> Include<E>(Expression<Func<T, E>> include)
        {
            queryable = queryable.Include(include);
            return this;
        }

        public IncludeBuilder<T> ThenInclude<E, S>(Expression<Func<E, S>> include)
        {
            var includable = (IIncludableQueryable<T, E>)queryable;
            queryable = includable.ThenInclude(include);
            return this;
        }

        public IQueryable<T> build()
        {
            return queryable;
        }
    }
}