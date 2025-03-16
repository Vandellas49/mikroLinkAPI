using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using System.Globalization;

namespace mikroLinkAPI.Application.Features.Sites.GetSite
{
    internal sealed class SiteQueryHandler(ISiteRepository siteRepository,IMapper mapper) : IRequestHandler<SiteQueryCommand, Result<Inventory<SiteVM>>>
    {
        public async Task<Result<Inventory<SiteVM>>> Handle(SiteQueryCommand request, CancellationToken cancellationToken)
        {
            List<SiteVM> items;
            IQueryable<Site> query = siteRepository.Where(p => true).Include(p => p.Il).Include(p => p.ComponentSerial).Include(p => p.RequestSite).Include(p => p.StoreExitSite);
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
            if (request.Page.sorted != null && !string.IsNullOrEmpty(request.Page.sorted.attributeName))
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                query = query.OrderByName(textInfo.ToTitleCase(request.Page.sorted.attributeName), request.Page.sorted.order);
            }
            else
            {
                query = query.OrderBy(p => p.SiteName);
            }
            if (!request.Page.All)
                 items = mapper.Map<List<SiteVM>>(await query.Skip(request.Page.page * request.Page.pageSize).Take(request.Page.pageSize).ToListAsync(cancellationToken));
            else
                items = mapper.Map<List<SiteVM>>(await query.ToListAsync(cancellationToken));
            var totalCount = await query.CountAsync(cancellationToken);
            return new Inventory<SiteVM>(items, totalCount);

        }
    }

}
