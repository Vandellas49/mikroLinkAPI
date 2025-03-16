using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Requests.RequestDelete
{
    public sealed record RequestDeleteQueryCommand(int Id) : IRequest<Result<string>>;
    internal sealed class RequestDeleteQueryHandler(IRequestRepository requestRepository, IRequestMaterialRepository requestMaterialRepository, IUnitOfWork unitOfWork) : IRequestHandler<RequestDeleteQueryCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(RequestDeleteQueryCommand request, CancellationToken cancellationToken)
        {
            var talep = await requestRepository.Where(p => p.Id == request.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            requestRepository.Delete(talep);
            await requestMaterialRepository.DeleteByExpressionAsync(c => c.RequestId == request.Id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Talep başarılı şekilde silindi";
        }
    }
}
