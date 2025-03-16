using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Dashboard
{
    public sealed record DashboardQueryCommand :IRequest<Result<DashBoardResponse>>;
    internal sealed class DashboardQueryHandler(
        IMetarialRepository metarialRepository,
        IMaterialExitRepository materialExitRepository,
        IRequestRepository requestRepository,
        ICurrentUserService currentUserService,
        IAccountAuthorityRepository accountAuthorityRepository) : IRequestHandler<DashboardQueryCommand, Result<DashBoardResponse>>
    {
        public async Task<Result<DashBoardResponse>> Handle(DashboardQueryCommand request, CancellationToken cancellationToken)
        {
            var malzemeSayisi = await metarialRepository.Where(c => c.CompanyId == currentUserService.UserCompanyId).CountAsync(cancellationToken);   
            var talepSayisi = await requestRepository.Where(c => c.CompanyId == currentUserService.UserCompanyId).CountAsync(cancellationToken);   
            var ekipSayisi = await accountAuthorityRepository.Where(c => c.Account.CompanyId == currentUserService.UserCompanyId&&c.Authority.YetkiKodu== "TeamLeader").CountAsync(cancellationToken);  
            var cikisSayisi=await materialExitRepository.Where(c=>c.CompanyId==currentUserService.UserCompanyId).CountAsync(cancellationToken);
            return new DashBoardResponse(malzemeSayisi,talepSayisi,cikisSayisi,ekipSayisi);
        
        }
    }
}
