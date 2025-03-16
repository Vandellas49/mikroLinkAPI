using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.ComponentExport
{
    internal sealed class MetarialExportHandler(IComponentRepository componentRepository, IMapper mapper, IExcelConvert convert) : IRequestHandler<ComponentExportCommand, Result<byte[]>>
    {
        public async Task<Result<byte[]>> Handle(ComponentExportCommand request, CancellationToken cancellationToken)
        {
            IQueryable<Component> query = componentRepository.Where(p => true);
            if (request.filters.MalzemeTuru != null)
            {
                query = query.FilterBy(nameof(request.filters.MalzemeTuru), request.filters.MalzemeTuru.matchMode, request.filters.MalzemeTuru.value);
            }
            if (request.filters.EquipmentDescription != null)
            {
                query = query.FilterBy(p=>p.EquipmentDescription, request.filters.EquipmentDescription.matchMode, request.filters.EquipmentDescription.value);
            }
            if (request.filters.Id != null)
            {
                query = query.FilterBy(p=>p.Id, request.filters.Id.matchMode, request.filters.Id.value);
            }
            var items = mapper.Map<List<ComponentVM>>(await query.ToListAsync(cancellationToken));
            return convert.ModelToExcel(items);
        }
    }

}
