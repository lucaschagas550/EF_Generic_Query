using EF.Generic_Query.API.Data.Repositories.Interfaces;
using EF.Generic_Query.API.Data.Uow.Interfaces;
using EF.Generic_Query.API.Models;

namespace EF.Generic_Query.API.Data.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(IUnitOfWork uow, AppDbContext context) : base(uow, context)
        {
        }
    }
}
