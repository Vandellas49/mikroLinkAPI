using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.MetarialInExport
{
    public sealed record MetarialInExportCommand(
        FilterByMetarialInExport Filters
    ) : IRequest<Result<byte[]>>;
    internal sealed class MetarialInExportHandler(
        IMaterialInRepository materialInRepository,
        ICurrentUserService currentUser,
        IMapper mapper,
        IExcelConvert convert
        ) : IRequestHandler<MetarialInExportCommand, Result<byte[]>>
    {
        public async Task<Result<byte[]>> Handle(MetarialInExportCommand request, CancellationToken cancellationToken)
        {
            int CompanyId = currentUser.UserCompanyId;
            var query = materialInRepository.Where(p => p.CompanyId == CompanyId).
                     Include(p => p.Cserial).
                     ThenInclude(p => p.Component).
                     Include(p => p.Company).
                     Include(p => p.FromCompany).
                     Include(p => p.FromSite).
                     Include(p => p.FromTeamLeader).
                     Include(p => p.WhoDone).
                     AsQueryable();
            if (request.Filters.SeriNo != null)
            {
                query = query.FilterBy(p => p.Cserial.SeriNo, request.Filters.SeriNo.matchMode, request.Filters.SeriNo.value);
            }
            if (request.Filters.ComponentId != null)
            {
                query = query.FilterBy(p => p.Cserial.ComponentId, request.Filters.ComponentId.matchMode, request.Filters.ComponentId.value);
            }
            if (request.Filters.MaterialType != null)
            {
                query = query.FilterBy(p => p.Cserial.MaterialType, request.Filters.MaterialType.matchMode, request.Filters.MaterialType.value);
            }
            if (request.Filters.FStoreDate != null)
            {
                query = query.FilterBy(p => p.CreatedDate, request.Filters.FStoreDate.matchMode, request.Filters.FStoreDate.value);
            }
            if (request.Filters.SStoreDate != null)
            {
                query = query.FilterBy(p => p.CreatedDate, request.Filters.SStoreDate.matchMode, request.Filters.SStoreDate.value);
            }
            if (request.Filters.FromCompanyId != null)
            {
                query = query.FilterBy(p => p.FromCompanyId, request.Filters.FromCompanyId.matchMode, request.Filters.FromCompanyId.value);
            }
            if (request.Filters.FromSiteId != null)
            {
                query = query.FilterBy(p => p.FromSiteId, request.Filters.FromSiteId.matchMode, request.Filters.FromSiteId.value);
            }
            if (request.Filters.FromTeamLeaderId != null)
            {
                query = query.FilterBy(p => p.FromTeamLeaderId, request.Filters.FromTeamLeaderId.matchMode, request.Filters.FromTeamLeaderId.value);
            }
            if (request.Filters.GirisTipi != null)
            {
                query = query.FilterBy(p => p.EnterType, request.Filters.GirisTipi.matchMode, request.Filters.GirisTipi.value);
            }
            if (request.Filters.GIrsaliyeNo != null)
            {
                query = query.FilterBy(p => p.Cserial.GIrsaliyeNo, request.Filters.GIrsaliyeNo.matchMode, request.Filters.GIrsaliyeNo.value);
            }
            var items = mapper.Map<List<MaterialInVM>>(await query.ToListAsync(cancellationToken));
            return convert.ModelToExcel(items);
        }
    }
}
