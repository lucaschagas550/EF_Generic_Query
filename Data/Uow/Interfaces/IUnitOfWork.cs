namespace EF.Generic_Query.API.Data.Uow.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        Task<bool> Commit();
    }
}