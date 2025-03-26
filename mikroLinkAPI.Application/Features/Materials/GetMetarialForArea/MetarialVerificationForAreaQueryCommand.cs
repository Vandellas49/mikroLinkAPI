using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.GetMetarialForArea
{
    public sealed record MetarialVerificationForAreaQueryCommand(string SearchValue, int IlId) : IRequest<Result<ComponentSerialVM>>;
    internal sealed class MetarialVerificationQueryHandler(
        ICompanySiteRepository companySiteRepository,
        ICurrentUserService currentUserService,
        IMapper mapper) : IRequestHandler<MetarialVerificationForAreaQueryCommand, Result<ComponentSerialVM>>
    {
        public async Task<Result<ComponentSerialVM>> Handle(MetarialVerificationForAreaQueryCommand request, CancellationToken cancellationToken)
        {
            //.Includes(p => p.Site.ComponentSerial,p=>p.Site.Il)
            var res = await companySiteRepository.
                       Where(c => c.CompanyId == currentUserService.UserCompanyId &&
                                  c.Site.ComponentSerial.Any(c => c.SeriNo == request.SearchValue && c.Site.IlId == request.IlId)).
                                  Include(c => c.Site).
                                  ThenInclude(c => c.ComponentSerial).
                                  Include(c => c.Site.Il).
                       FirstOrDefaultAsync(cancellationToken);
            if (res != null && res.Site != null && res.Site.ComponentSerial.Count > 0)
                return mapper.Map<ComponentSerialVM>(res.Site.ComponentSerial.FirstOrDefault());
            return Result<ComponentSerialVM>.Failure("Malzeme bulunamadı");
        }

    }
}
