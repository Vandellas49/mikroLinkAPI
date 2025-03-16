using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Sites.SiteExport
{
    internal sealed class SiteExportHandler(ISiteRepository siteRepository, IMapper mapper,IExcelConvert convert) : IRequestHandler<SiteExportCommand, Result<byte[]>>
    {
        public async Task<Result<byte[]>> Handle(SiteExportCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Site> query = siteRepository.Where(p => true).Include(p => p.Il);
            if (request.Filters.PlanId != null)
            {
                query = query.FilterBy(p=>p.PlanId, request.Filters.PlanId.matchMode, request.Filters.PlanId.value);
            }
            if (request.Filters.SiteId != null)
            {
                query = query.FilterBy(p=>p.SiteId, request.Filters.SiteId.matchMode, request.Filters.SiteId.value);
            }
            if (request.Filters.Region != null)
            {
                query = query.FilterBy(p=>p.Region, request.Filters.Region.matchMode, request.Filters.Region.value);
            }
            if (request.Filters.SiteName != null)
            {
                query = query.FilterBy(p=>p.SiteName, request.Filters.SiteName.matchMode, request.Filters.SiteName.value);
            }
            if (request.Filters.SiteTip != null)
            {
                query = query.FilterBy(p=>p.SiteTip, request.Filters.SiteTip.matchMode, request.Filters.SiteTip.value);
            }
            if (request.Filters.IlId != null)
            {
                query = query.FilterBy(p=>p.IlId, request.Filters.IlId.matchMode, request.Filters.IlId.value);
            }
            var items = mapper.Map<List<SiteVM>>(await query.ToListAsync(cancellationToken));
            return convert.ModelToExcel(items);
        }
    }
}
