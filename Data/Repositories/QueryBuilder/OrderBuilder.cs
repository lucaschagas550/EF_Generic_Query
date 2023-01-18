using EF.Generic_Query.API.Models;
using System.Linq.Expressions;

namespace EF.Generic_Query.API.Data.Repositories.QueryBuilder
{
    public class OrderBuilder<T> where T : class
    {
        private IQueryable<T> queryable;

        public OrderBuilder(IQueryable<T> queryable)
        {
            this.queryable = queryable;
        }

        public OrderBuilder<T> OrderBy<O>(Expression<Func<T, O>> order, Direction direction)
        {
            if (direction.Equals(Direction.DESC))
            {
                queryable = queryable.OrderByDescending(order);
            }
            else
            {
                queryable = queryable.OrderBy(order);
            }

            return this;
        }

        public IQueryable<T> build()
        {
            return queryable;
        }
    }
}

public enum Direction
{
    ASC,
    DESC
}
