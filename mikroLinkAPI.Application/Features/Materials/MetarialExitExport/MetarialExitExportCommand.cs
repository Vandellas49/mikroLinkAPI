using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Features.Materials.MetarialExit;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.MetarialExitExport
{
    public sealed record MetarialExitExportCommand(
        FilterByMetarialExit Filters
    ) : IRequest<Result<byte[]>>;
    internal sealed class MetarialExitExportHandler(
        IMaterialExitRepository materialExitRepository,
        ICurrentUserService currentUser,
        IMapper mapper,
        IExcelConvert convert
        ) : IRequestHandler<MetarialExitExportCommand, Result<byte[]>>
    {
        public async Task<Result<byte[]>> Handle(MetarialExitExportCommand request, CancellationToken cancellationToken)
        {
            int CompanyId = currentUser.UserCompanyId;
            var query = materialExitRepository.Where(p => p.CompanyId == CompanyId).
                Include(p => p.Cserial).
                ThenInclude(p=>p.Component).
                Include(p=>p.Company).
                Include(p=>p.CompanyIdExitNavigation).
                Include(p=>p.SiteIdExitNavigation).
                Include(p=>p.TeamLeaderIdExitNavigation).
                Include(p=>p.WhoDone).
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
            if (request.Filters.ExitCompanyId != null)
            {
                query = query.FilterBy(p => p.CompanyIdExit, request.Filters.ExitCompanyId.matchMode, request.Filters.ExitCompanyId.value);
            }
            if (request.Filters.ExitSiteId != null)
            {
                query = query.FilterBy(p => p.SiteIdExit, request.Filters.ExitSiteId.matchMode, request.Filters.ExitSiteId.value);
            }
            if (request.Filters.ExitTeamLeaderId != null)
            {
                query = query.FilterBy(p => p.TeamLeaderIdExit, request.Filters.ExitTeamLeaderId.matchMode, request.Filters.ExitTeamLeaderId.value);
            }
            if (request.Filters.CikisTipi != null)
            {
                query = query.FilterBy(p => p.ExitType, request.Filters.CikisTipi.matchMode, request.Filters.CikisTipi.value);
            }
            if (request.Filters.GIrsaliyeNo != null)
            {
                query = query.FilterBy(p => p.Cserial.GIrsaliyeNo, request.Filters.GIrsaliyeNo.matchMode, request.Filters.GIrsaliyeNo.value);
            }
            var items = mapper.Map<List<MaterialExitVM>>(await query.ToListAsync(cancellationToken));
            return convert.ModelToExcel(items);
        }
    }
}
