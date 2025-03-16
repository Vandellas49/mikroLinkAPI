using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.CompanyToCompanyAuthorization
{
    internal sealed class GetCompanyToCompanyHandler(ICompanyToCompanyRepository companyToCompanyRepository, IMapper mapper) : IRequestHandler<GetCompanyToCompanyQueryCommand, Result<List<CompanyVM>>>
    {
        public async Task<Result<List<CompanyVM>>> Handle(GetCompanyToCompanyQueryCommand request, CancellationToken cancellationToken)
        {
            var result =await companyToCompanyRepository.Where(p => p.ParentCompanyId == request.CompanyId).
                                                    Include(p => p.Company).
                                                    ThenInclude(p=>p.Il).
                                                    Select(p => p.Company).
                                                    ToListAsync(cancellationToken);
            return mapper.Map<List<CompanyVM>>(result);
        }
    }

}
