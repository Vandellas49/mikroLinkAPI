using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using System.Globalization;

namespace mikroLinkAPI.Application.Features.SiteMetarial.GetMetarials
{
    internal sealed class SiteMetarialQueryHandler(IMetarialRepository metarialRepository,IMapper mapper) : IRequestHandler<SiteMetarialQueryCommand, Result<Inventory<ComponentSerialVM>>>
    {
        public async Task<Result<Inventory<ComponentSerialVM>>> Handle(SiteMetarialQueryCommand request, CancellationToken cancellationToken)
        {
            IQueryable<ComponentSerial> query = metarialRepository.Where(p => p.SiteId!=null).Include(p=>p.Component).Include(p=>p.Site);
            if (request.filters.SiteId != null)
            {
                query.FilterBy(p => p.SiteId, request.filters.SiteId.matchMode, request.filters.SiteId.value);
            }
            if (request.filters.SeriNo != null)
            {
                query = query.FilterBy(p=>p.SeriNo, request.filters.SeriNo.matchMode, request.filters.SeriNo.value);
            }
            if (request.filters.EquipmentDescription != null)
            {
                query = query.FilterBy(p => p.Component.EquipmentDescription, request.filters.EquipmentDescription.matchMode, request.filters.EquipmentDescription.value);
            }
            if (request.filters.ComponentId != null)
            {
                query = query.FilterBy(p=>p.ComponentId, request.filters.ComponentId.matchMode, request.filters.ComponentId.value);
            }
            if (request.filters.GIrsaliyeNo != null)
            {
                query = query.FilterBy(p=>p.GIrsaliyeNo, request.filters.GIrsaliyeNo.matchMode, request.filters.GIrsaliyeNo.value);
            }       
            if (request.filters.Sturdy != null)
            {
                query = query.FilterBy(p=>p.Sturdy, request.filters.Sturdy.matchMode, request.filters.Sturdy.value);
            } 
            if (request.filters.Defective != null)
            {
                query = query.FilterBy(p=>p.Defective, request.filters.Defective.matchMode, request.filters.Defective.value);
            }  
            if (request.filters.Scrap != null)
            {
                query = query.FilterBy(p => p.Scrap, request.filters.Scrap.matchMode, request.filters.Scrap.value);
            }
            if (request.filters.Shelf != null)
            {
                query = query.FilterBy(p => p.Shelf, request.filters.Shelf.matchMode, request.filters.Shelf.value);
            }
            if (request.filters.MaterialType != null)
            {
                query = query.FilterBy(p => p.MaterialType, request.filters.MaterialType.matchMode, request.filters.MaterialType.value);
            }
            if (request.Page.sorted != null && !string.IsNullOrEmpty(request.Page.sorted.attributeName))
            {
                TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
                query = query.OrderByName(textInfo.ToTitleCase(request.Page.sorted.attributeName), request.Page.sorted.order);
            }
            else
            {
                query = query.OrderBy(p => p.Id);
            }
            var items = mapper.Map<List<ComponentSerialVM>>(await query.Skip(request.Page.page * request.Page.pageSize).Take(request.Page.pageSize).ToListAsync(cancellationToken));
            var totalCount = await query.CountAsync();
            return new Inventory<ComponentSerialVM>(items, totalCount);
        }
    }
}
