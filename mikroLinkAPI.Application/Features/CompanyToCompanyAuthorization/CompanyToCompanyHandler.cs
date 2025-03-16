using AutoMapper;
using GenericRepository;
using MediatR;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.CompanyToCompanyAuthorization
{
    internal sealed class CompanyToCompanyHandler   (ICompanyToCompanyRepository companyToCompanyRepository, IUnitOfWork unitOfWork) : IRequestHandler<CompanyToCompanyQueryCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(CompanyToCompanyQueryCommand request, CancellationToken cancellationToken)
        {
            await companyToCompanyRepository.DeleteByExpressionAsync(f => f.ParentCompanyId == request.CompanyId, cancellationToken);
            await companyToCompanyRepository.AddRangeAsync(request.Companies.Select(p => new Domain.Entities.CompanyToCompanyAuthorization
            {
                ParentCompanyId = request.CompanyId,
                CompanyId = p.Id
            }).ToList(), cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Başarışı şekilde kaydedildi";
        }
    }

}
