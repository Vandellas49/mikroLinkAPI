using GenericRepository;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Requests.MetarialRequestTeamLeader
{
    internal sealed class MetarialRequestTeamLeaderHandler(
        IRequestRepository requestRepository,
        IMetarialRepository metarialRepository,
        IRequestSiteCompanySerialRepository requestSiteCompanySerial,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork) : IRequestHandler<MetarialRequestTeamLeaderCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialRequestTeamLeaderCommand request, CancellationToken cancellationToken)
        {
            var req = await requestRepository.FirstOrDefaultAsync(p => p.Id == request.RequestId, cancellationToken);
            if (req == null)
                return Result<string>.Failure("Talep bulunamadı");
            foreach (var item in request.RequestMaterial)
            {
                var malzeme = await metarialRepository.FirstOrDefaultAsync(p => p.Id == item.Id, cancellationToken);
                malzeme.TeamLeaderId = req.TeamLeaderId;
                malzeme.SiteId = null;
                malzeme.CompanyId = null;
                malzeme.CreatedDate = DateTime.Now;
                malzeme.CreatedBy = currentUser.UserId;
                metarialRepository.Update(malzeme);
                RequestSiteCompanySerial rs = new()
                {
                    CserialId = item.Id,
                    RequestId = request.RequestId
                };
                requestSiteCompanySerial.Add(rs);
                req.RequestStatu = (int)RequestStatu.TeamLeader;
                requestRepository.Update(req);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result<string>($"Başarılı şekilde talep yapıldı");
        }
    }
}
