using System.Data;
namespace mikroLinkAPI.Domain.Repositories
{
    public interface ITransactionUnitOfWork
    {
        Task<IDbTransaction> BeginTransactionAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
