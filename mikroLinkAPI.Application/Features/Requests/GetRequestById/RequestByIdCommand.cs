using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Requests.GetRequestById
{
    public sealed record RequestByIdCommand(int Id) : IRequest<Result<MetarialRequestResponse>>;

    internal sealed class RequestsReceivedQueryHandler(
        IRequestRepository requestRepository,
        IRequestMaterialRepository requestMaterialRepository,
        IMetarialRepository metarialRepository,
        ICurrentUserService currentUser,
        IMapper mapper) : IRequestHandler<RequestByIdCommand, Result<MetarialRequestResponse>>
    {
        public async Task<Result<MetarialRequestResponse>> Handle(RequestByIdCommand request, CancellationToken cancellationToken)
        {
            var talepler = await requestRepository.Where(p => p.Id == request.Id).
                                      Include(p => p.TeamLeader).
                                      Include(p => p.ReceiverCompany).
                                      Include(p => p.RequestSiteCompanySerial).
                                      ThenInclude(p => p.Cserial).
                                      ThenInclude(p => p.Component).
                                      Include(p => p.ReceiverSite).
                                      Include(p => p.RequestedMaterial).
                                      ThenInclude(p => p.Component).
                                      ThenInclude(c => c.ComponentSerial.Where(c => c.CompanyId == currentUser.UserCompanyId)).
                                      FirstOrDefaultAsync(cancellationToken);
            if (talepler == null)
                return Result<MetarialRequestResponse>.Failure("Talep bulunamadı");
            var result = mapper.Map<MetarialRequestResponse>(talepler);
            await GetMaxValuePerRequest(result.Material, request.Id, cancellationToken);
            return result;

        }
        private async Task GetMaxValuePerRequest(List<MaterialsRequestVM> Materials, int RequestId, CancellationToken cancellationToken)
        {
            foreach (var p in Materials)
            {
                var material = await metarialRepository.
                                    Where(c => c.ComponentId == p.ComponentId &&
                                               c.CompanyId == currentUser.UserCompanyId &&
                                               c.MaterialType == p.MaterialType).
                                    Select(x => new { x.Sturdy, x.Defective, x.Scrap }).
                                    ToListAsync(cancellationToken);
                var requestmaterial = await requestMaterialRepository.Includes(p => p.Request).Where(c => c.ComponentId == p.ComponentId &&
                                                                           c.Request.RequestStatu == (int)RequestStatu.StockMan &&
                                                                           c.MaterialType == p.MaterialType &&
                                                                           c.RequestId != RequestId &&
                                                                           c.Request.CompanyId == currentUser.UserCompanyId).
                                                                Select(c => new { c.Sturdy, c.Scrap, c.Defective }).
                                                                ToListAsync(cancellationToken);
                p.MaxSturdy = material.Sum(x => x.Sturdy) - requestmaterial.Sum(c => c.Sturdy);
                p.MaxDefective = material.Sum(x => x.Defective) - requestmaterial.Sum(c => c.Defective);
                p.MaxScrap = material.Sum(x => x.Scrap) - requestmaterial.Sum(c => c.Scrap);
            }
        }
    }
}
