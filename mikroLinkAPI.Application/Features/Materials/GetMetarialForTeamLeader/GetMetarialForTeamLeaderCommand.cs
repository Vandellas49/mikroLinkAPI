using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.GetMetarialForTeamLeader
{
    public sealed record GetMetarialForTeamLeaderCommand(string SearchValue) : IRequest<Result<ComponentSerialVM>>;
    internal sealed class GetMetarialForTeamLeaderHandler(
        IMetarialRepository metarialRepository,
        ICurrentUserService currentUserService,
        IMapper mapper) : IRequestHandler<GetMetarialForTeamLeaderCommand, Result<ComponentSerialVM>>
    {
        public async Task<Result<ComponentSerialVM>> Handle(GetMetarialForTeamLeaderCommand request, CancellationToken cancellationToken)
        {
            var metarial = await metarialRepository
                  .Where(p => p.SeriNo == request.SearchValue &&
                               p.CompanyId == currentUserService.UserCompanyId).Include(p => p.RequestSiteCompanySerial).ThenInclude(c => c.Request)
                  .FirstOrDefaultAsync(c => c.RequestSiteCompanySerial == null || !c.RequestSiteCompanySerial.Any(c => c.Request.RequestStatu == (int)RequestStatu.TeamLeader), cancellationToken);
            if (metarial != null)
                return mapper.Map<ComponentSerialVM>(metarial);
            return Result<ComponentSerialVM>.Failure("Malzeme bulunamadı");
        }

    }
}
