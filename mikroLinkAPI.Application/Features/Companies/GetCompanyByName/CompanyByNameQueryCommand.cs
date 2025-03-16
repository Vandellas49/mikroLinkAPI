using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Companies.GetCompanyByName
{
    public sealed record CompanyByNameQueryCommand(string CompanyName) : IRequest<Result<List<CompanyVM>>>;

    internal sealed class GetCompanyByNameQueryHandler(ICompanyRepository companyRepository,ICurrentUserService currentUser, IMapper mapper) : IRequestHandler<CompanyByNameQueryCommand, Result<List<CompanyVM>>>
    {
        public async Task<Result<List<CompanyVM>>> Handle(CompanyByNameQueryCommand request, CancellationToken cancellationToken)
        {
            var query = companyRepository.Where(p => p.Id!=currentUser.UserCompanyId);
            if (!string.IsNullOrEmpty(request.CompanyName))
            {
                query = query.Where(p => p.Name.Contains(request.CompanyName));
            }
            return mapper.Map<List<CompanyVM>>(await query.ToListAsync(cancellationToken));
        }
    }
}
