using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.CompanySiteAuthorization
{
    internal sealed class GetCompanySiteHandler(ICompanySiteRepository companySiteRepository, IMapper mapper) : IRequestHandler<GetCompanySiteQueryCommand, Result<List<SiteVM>>>
    {
        public async Task<Result<List<SiteVM>>> Handle(GetCompanySiteQueryCommand request, CancellationToken cancellationToken)
        {
            var result =await companySiteRepository.Where(p => p.CompanyId == request.CompanyId).
                                                    Include(p => p.Site).
                                                    Select(p => p.Site).
                                                    ToListAsync(cancellationToken);
            return mapper.Map<List<SiteVM>>(result);
        }
    }

}
