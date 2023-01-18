using EF.Generic_Query.API.Data.Repositories;
using EF.Generic_Query.API.Data.Repositories.Interfaces;
using EF.Generic_Query.API.Data.Repositories.Pagination;
using EF.Generic_Query.API.Data.Repositories.QueryBuilder;
using EF.Generic_Query.API.Models;
using EF.Generic_Query.API.Services.Interfaces;

namespace EF.Generic_Query.API.Services
{
    public class CategoryService : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository) 
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> Create(Category category)
        {
            return await _categoryRepository.Create(category).ConfigureAwait(false);
        }

        public async Task<Page<Category>> Get(Pageable pagination)
        {
            var orderByQuery = new OrderBuilder<Category>(_categoryRepository.AsQueryable());

            return await _categoryRepository.Find(pagination, order: order => orderByQuery.OrderBy( c => c.Name, Direction.ASC)).ConfigureAwait(false);
        }

        public async Task<Category> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> Update(Category category)
        {
            throw new NotImplementedException();
        }

        public async Task<Category> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
