using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Features.Requests.MetarialRequest;
using mikroLinkAPI.Application.Features.Requests.MetarialRequestTeamLeader;
using mikroLinkAPI.Application.Features.Requests.MetarialSiteRequest;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Requests.MetarialRequestComplate
{
    internal sealed class MetarialRequestComplateHandler(
        IRequestRepository requestRepository,
        IMetarialRepository metarialRepository,
        ICurrentUserService currentUser,
        IRequestHandler<MetarialRequestCommand, Result<string>> requestHandler,
         IRequestHandler<MetarialSiteRequestCommand, Result<string>> siterequestHandler,
         IRequestHandler<MetarialRequestTeamLeaderCommand, Result<string>> requestHandlerTeamLeader,
        IUnitOfWork unitOfWork) : IRequestHandler<MetarialRequestComplateCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialRequestComplateCommand request, CancellationToken cancellationToken)
        {
            var req = await requestRepository.WhereWithTracking(p => p.Id == request.RequestId)
                                             .Include(c => c.RequestSiteCompanySerial)
                                             .ThenInclude(t => t.Cserial)
                                             .FirstOrDefaultAsync(cancellationToken);
            if (req == null)
                return Result<string>.Failure("Talep bulunamadı");
            if (!request.RequestMaterial.All(x => req.RequestSiteCompanySerial.Any(c => c.CserialId == x.Id)))
                return Result<string>.Failure("Talep dışı malzeme bulundu");
            foreach (var item in request.RequestMaterial)
            {
                var malzeme = await metarialRepository.FirstOrDefaultAsync(p => p.Id == item.Id, cancellationToken);
                malzeme.TeamLeaderId = null;
                malzeme.SiteId = req.ReceiverSiteId;
                malzeme.CompanyId = req.ReceiverCompanyId;
                malzeme.CreatedDate = DateTime.Now;
                malzeme.CreatedBy = currentUser.UserId;
                metarialRepository.Update(malzeme);
            }
            req.RequestStatu = (int)RequestStatu.Complate;
            requestRepository.Update(req);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            var missingItems = req.RequestSiteCompanySerial
             .Where(c => !request.RequestMaterial.Any(x => x.Id == c.CserialId))
             .ToList();
            if (missingItems.Count != 0)
            {
                if (req.RequestType == 1)
                {
                    var refrequest=  await siterequestHandler.Handle(new MetarialSiteRequestCommand(req.ReceiverSiteId, req.WorkOrderNo, "Sahadan iade", missingItems.Select(p => new RequestMaterialVM { Id = p.CserialId }).ToList()),cancellationToken);
                    return new Result<string>($"Başarılı şekilde talep sonlandırıldı. Ve iade talebi oluşturuldu. Talep NO:{refrequest.IslemId}");
                }
                else
                {
                    var refrequest = await requestHandler.Handle(CreateRefundRequest(req, missingItems), cancellationToken);
                    await requestHandlerTeamLeader.Handle(new MetarialRequestTeamLeaderCommand(refrequest.IslemId,
                        missingItems.Select(c => new RequestMaterialVM { Id = c.CserialId }).ToList()), cancellationToken);
                    return new Result<string>($"Başarılı şekilde talep sonlandırıldı. Ve iade talebi oluşturuldu. Talep NO:{refrequest.IslemId}");
                }

            }
            return new Result<string>("Başarılı şekilde talep sonlandırıldı.");

        }
        private static MetarialRequestCommand CreateRefundRequest(Request req, List<RequestSiteCompanySerial> missingItems)
        {
            return new MetarialRequestCommand(
                req.RequestType,
                req.SiteId,
                req.CompanyId,
                req.TeamLeaderId,
                req.WorkOrderNo,
                req.RequestMessage,
                 missingItems.Select(item => req.RequestSiteCompanySerial.FirstOrDefault(c => c.CserialId == item.CserialId)?.Cserial).Where(t => t != null).Select(t => new MaterialsRequestVM
                 {
                     ComponentId = t.ComponentId,
                     Defective = t.Defective,
                     MaterialType = t.MaterialType,
                     Scrap = t.Scrap,
                     Sturdy = t.Sturdy
                 }).ToList(),
                req.ReceiverCompanyId
            );
        }

    }
}
