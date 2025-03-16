using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using System.Globalization;

namespace mikroLinkAPI.Application.Features.Components.GetComponent
{
    internal sealed class ComponentQueryHandler(IComponentRepository componentRepository,IMapper mapper) : IRequestHandler<ComponentQueryCommand, Result<Inventory<ComponentVM>>>
    {
        public async Task<Result<Inventory<ComponentVM>>> Handle(ComponentQueryCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Component> query = componentRepository.Where(p => true).Include(p=>p.RequestedMaterial).Include(p=>p.ComponentSerial);
            if (request.Filters.MalzemeTuru != null)
            {
                query = query.FilterBy(nameof(request.Filters.MalzemeTuru), request.Filters.MalzemeTuru.matchMode, request.Filters.MalzemeTuru.value);
            }
            if (request.Filters.EquipmentDescription != null)
            {
                query = query.FilterBy(p=>p.EquipmentDescription, request.Filters.EquipmentDescription.matchMode, request.Filters.EquipmentDescription.value);
            }
            if (request.Filters.Id != null)
            {
                query = query.FilterBy(p=>p.Id, request.Filters.Id.matchMode, request.Filters.Id.value);
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
            var items = mapper.Map<List<ComponentVM>>(await query.Skip(request.Page.page * request.Page.pageSize).Take(request.Page.pageSize).ToListAsync(cancellationToken));
            var totalCount = await query.CountAsync(cancellationToken);
            return new Inventory<ComponentVM>(items, totalCount);

        }
    }
}
