using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.MetarialExport
{
    internal sealed class MetarialExportHandler(IMetarialRepository metarialRepository, IMapper mapper,IExcelConvert convert) : IRequestHandler<MetarialExportCommand, Result<byte[]>>
    {
        public async Task<Result<byte[]>> Handle(MetarialExportCommand request, CancellationToken cancellationToken)
        {

            IQueryable<ComponentSerial> query = metarialRepository.Where(p => true).Include(p => p.Component);
            if (request.Filters.SeriNo != null)
            {
                query = query.FilterBy(p => p.SeriNo, request.Filters.SeriNo.matchMode, request.Filters.SeriNo.value);
            }
            if (request.Filters.EquipmentDescription != null)
            {
                query = query.FilterBy(p => p.Component.EquipmentDescription, request.Filters.EquipmentDescription.matchMode, request.Filters.EquipmentDescription.value);
            }
            if (request.Filters.ComponentId != null)
            {
                query = query.FilterBy(p => p.ComponentId, request.Filters.ComponentId.matchMode, request.Filters.ComponentId.value);
            }
            if (request.Filters.GIrsaliyeNo != null)
            {
                query = query.FilterBy(p => p.GIrsaliyeNo, request.Filters.GIrsaliyeNo.matchMode, request.Filters.GIrsaliyeNo.value);
            }
            if (request.Filters.Sturdy != null)
            {
                query = query.FilterBy(p => p.Sturdy, request.Filters.Sturdy.matchMode, request.Filters.Sturdy.value);
            }
            if (request.Filters.Defective != null)
            {
                query = query.FilterBy(p => p.Defective, request.Filters.Defective.matchMode, request.Filters.Defective.value);
            }
            if (request.Filters.Scrap != null)
            {
                query = query.FilterBy(p => p.Scrap, request.Filters.Scrap.matchMode, request.Filters.Scrap.value);
            }
            if (request.Filters.Shelf != null)
            {
                query = query.FilterBy(p => p.Shelf, request.Filters.Shelf.matchMode, request.Filters.Shelf.value);
            }
            if (request.Filters.MaterialType != null)
            {
                query = query.FilterBy(p => p.MaterialType, request.Filters.MaterialType.matchMode, request.Filters.MaterialType.value);
            }
            var items = mapper.Map<List<ComponentSerialVM>>(await query.ToListAsync(cancellationToken));
            return convert.ModelToExcel(items);
        }
    }

}
