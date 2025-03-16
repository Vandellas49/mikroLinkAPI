using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.GetMetarialForSite
{
    public sealed record MetarialBySiteQueryCommand(string SearchValue, int SiteId) : IRequest<Result<ComponentSerialVM>>;
    internal sealed class MetarialBySiteQueryHandler(
        IMetarialRepository metarialRepository,
        IMapper mapper) : IRequestHandler<MetarialBySiteQueryCommand, Result<ComponentSerialVM>>
    {
        public async Task<Result<ComponentSerialVM>> Handle(MetarialBySiteQueryCommand request, CancellationToken cancellationToken)
        {
            var metarial = await metarialRepository.Where(x => true).
                  Include(p => p.Component).
                  FirstOrDefaultAsync(p => p.SeriNo == request.SearchValue && p.SiteId == request.SiteId, cancellationToken);
            if (metarial != null)
            {
                return mapper.Map<ComponentSerialVM>(metarial);
            }
            return Result<ComponentSerialVM>.Failure("Malzeme bulunamadı");
        }
    }
}
