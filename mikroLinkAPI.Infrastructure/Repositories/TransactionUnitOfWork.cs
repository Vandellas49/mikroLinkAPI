using Microsoft.EntityFrameworkCore.Storage;
using mikroLinkAPI.Domain.Repositories;
using System.Data;
using mikroLinkAPI.Infrastructure.Context;

namespace mikroLinkAPI.Infrastructure.Repositories;
internal class TransactionUnitOfWork(ApplicationDbContext dbContext) : ITransactionUnitOfWork
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private IDbContextTransaction _transaction;

    public async Task<IDbTransaction> BeginTransactionAsync()
    {
        _transaction = await _dbContext.Database.BeginTransactionAsync();
        return _transaction.GetDbTransaction();
    }
    public async Task CommitAsync()
    {
        await _transaction.CommitAsync();
    }
    public async Task RollbackAsync()
    {
        await _transaction.RollbackAsync();
    }
}
