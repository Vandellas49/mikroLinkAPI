using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.CompanySiteAuthorization
{
    internal sealed class CompanySiteHandler(ICompanySiteRepository companySiteRepository, IUnitOfWork unitOfWork) : IRequestHandler<CompanySiteQueryCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CompanySiteQueryCommand request, CancellationToken cancellationToken)
        {
            await companySiteRepository.DeleteByExpressionAsync(f => f.CompanyId == request.CompanyId, cancellationToken);
            await companySiteRepository.AddRangeAsync(request.sites.Select(p => new Domain.Entities.CompanySiteAuthorization
            {
                CompanyId = request.CompanyId,
                SiteId = p.Id
            }).ToList(), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Başarışı şekilde kaydedildi";
        }
    }

}
