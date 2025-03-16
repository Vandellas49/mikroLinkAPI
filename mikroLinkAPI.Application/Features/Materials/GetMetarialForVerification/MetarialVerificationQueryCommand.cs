using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.GetMetarialForVerification
{
    public sealed record MetarialVerificationQueryCommand(string SearchValue, int Id, OperationType OperationType) : IRequest<Result<ComponentSerialVM>>;
    internal sealed class MetarialVerificationQueryHandler(
        IComponentRepository componentRepository,
        IMetarialRepository metarialRepository,
        IMapper mapper) : IRequestHandler<MetarialVerificationQueryCommand, Result<ComponentSerialVM>>
    {
        public async Task<Result<ComponentSerialVM>> Handle(MetarialVerificationQueryCommand request, CancellationToken cancellationToken)
        {
            var component = await componentRepository.FirstOrDefaultAsync(p => p.Id == request.SearchValue, cancellationToken);
            ComponentSerial metarial = null;
            if (component == null || component.MalzemeTuru == (int)MalzemeTuru.Seri)
                metarial = await metarialRepository.Includes(p => p.Site, p => p.Company, p => p.TeamLeader, p => p.Component).
                                       FirstOrDefaultAsync(p => p.SeriNo == request.SearchValue, cancellationToken);
            else if (component.MalzemeTuru == (int)MalzemeTuru.Sarf)
            {
                var result = await metarialRepository
                      .Where(p => p.ComponentId == request.SearchValue &&
                                   request.OperationType == OperationType.Site ? p.SiteId == request.Id :
                                   request.OperationType == OperationType.TeamLeader ? p.TeamLeaderId == request.Id :
                                   p.CompanyId == request.Id)
                      .ToListAsync(cancellationToken);
                if (result.Count > 0)
                {
                    metarial = result.FirstOrDefault();
                    metarial.Sturdy = result.Sum(p => p.Sturdy);
                    metarial.Scrap = result.Sum(p => p.Scrap);
                    metarial.Defective = result.Sum(p => p.Defective);
                }
            }
            if (metarial != null)
                return mapper.Map<ComponentSerialVM>(metarial);
            return Result<ComponentSerialVM>.Failure("Malzeme bulunamadı");
        }

    }
}
