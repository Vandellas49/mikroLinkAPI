using MediatR;
using mikroLinkAPI.Domain.Attributes;
using mikroLinkAPI.Domain.Repositories;
namespace mikroLinkAPI.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse>(ITransactionUnitOfWork transactionUnitOfWork) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ITransactionUnitOfWork _transactionUnitOfWork = transactionUnitOfWork;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (typeof(TRequest).GetCustomAttributes(typeof(TransactionAttribute), false).Length == 0)
                return await next();
            using var transaction = await _transactionUnitOfWork.BeginTransactionAsync();
            try
            {
                var response = await next(); // Handler'ı çalıştır
                await _transactionUnitOfWork.CommitAsync(); // Başarılıysa commit
                return response;
            }
            catch (Exception)
            {
                await _transactionUnitOfWork.RollbackAsync(); // Hata olursa rollback
                throw;
            }
        }
    }

}
