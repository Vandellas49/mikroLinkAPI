using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Users.TeamLeader
{
    public sealed record TeamLeaderByCompanyIdCommand(int? CompanyId = null) : IRequest<Result<List<CompanyTeamLeader>>>;

    internal sealed class SiteIdOrSiteNameQueryHandler(
        IUserRepository userRepository,
        ICompanyRepository companyRepository,
        ICurrentUserService currentUserService
        ) : IRequestHandler<TeamLeaderByCompanyIdCommand, Result<List<CompanyTeamLeader>>>
    {
        public async Task<Result<List<CompanyTeamLeader>>> Handle(TeamLeaderByCompanyIdCommand request, CancellationToken cancellationToken)
        {
            int userCompanyId = currentUserService.UserCompanyId;
            var companies = await companyRepository.GetAllFromCacheAsync(p => request.CompanyId != null && p.Id == request.CompanyId);
            var usercompany = await companyRepository.GetAllFromCacheAsync(p => p.Id == userCompanyId);
            var company = usercompany.Union(companies);
            var result = await userRepository.Where(s =>
            s.AccountAuthority.Any(f=>f.Authority.YetkiKodu== "TeamLeader") &&
            ((request.CompanyId != null && s.CompanyId == request.CompanyId) || s.CompanyId == userCompanyId)).
            Include(p=>p.AccountAuthority).
            ThenInclude(f=>f.Authority).ToListAsync(cancellationToken);
            return result.GroupBy(p => p.CompanyId).Select(p => new CompanyTeamLeader
            {
                Company = company.FirstOrDefault(c => c.Id == p.Key).Name,
                TeamLeaders = p.Select(s => new TeamLeaderVM
                {
                    Id = s.Id,
                    UserName = s.UserName
                }).ToList()
            }).ToList();
        }
    }
}
