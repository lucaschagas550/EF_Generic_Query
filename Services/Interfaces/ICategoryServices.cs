using EF.Generic_Query.API.Data.Repositories.Pagination;
using EF.Generic_Query.API.Models;

namespace EF.Generic_Query.API.Services.Interfaces
{
    public interface ICategoryServices
    {
        Task<Page<Category>> Get(Pageable pagination);
        Task<Category> GetById(Guid id);
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<Category> Delete(Guid id);
    }
}
