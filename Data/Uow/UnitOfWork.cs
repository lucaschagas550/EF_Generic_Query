using EF.Generic_Query.API.Data.Uow.Interfaces;

namespace EF.Generic_Query.API.Data.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private const int EMPTY = 0;
        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context) =>
            _context = context;

        public async Task<bool> Commit() =>
            await _context.SaveChangesAsync().ConfigureAwait(false) > EMPTY;

        public void Dispose() =>
            _context?.Dispose();
    }
}