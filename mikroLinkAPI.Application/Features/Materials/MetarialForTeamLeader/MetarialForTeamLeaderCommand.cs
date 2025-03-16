using GenericRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.MetarialForTeamLeader
{
    public sealed record MetarialForTeamLeaderCommand(int TeamLeaderId, List<int> CsIds, int RandevuId) : IRequest<Result<string>>;
    internal sealed class MetarialForTeamLeaderHandler(
        IMetarialRepository metarialRepository,
        IRandevuRepository randevuRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<MetarialForTeamLeaderCommand, Result<string>>
    {
        public async Task<Result<string>> Handle(MetarialForTeamLeaderCommand request, CancellationToken cancellationToken)
        {
            if (request.CsIds.Count == 0)
                return Result<string>.Failure("Lütfen malzeme giriniz");
            foreach (var item in request.CsIds)
            {
                var metarial = await metarialRepository.WhereWithTracking(c => c.Id == item).FirstOrDefaultAsync(cancellationToken);
                if (metarial != null)
                {
                    metarial.TeamLeaderId = request.TeamLeaderId;
                    metarial.CompanyId = null;
                    metarial.State = (int)State.Randevu;
                    metarialRepository.Update(metarial);
                }
            }
            var randevu = await randevuRepository.FirstOrDefaultAsync(c => c.Id == request.RandevuId, cancellationToken);
            randevu.Durum = 1;
            randevuRepository.Update(randevu);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return "Başarılı şekilde malzeme takım liderine aktarıldı";
        }

    }
}
