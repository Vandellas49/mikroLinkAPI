using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Enums;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Materials.GetMetarialForRequest
{
    public sealed record MetarialRequestQueryCommand(string searchValue, int? MetarialType) : IRequest<Result<List<MaterialsRequestVM>>>;
    internal sealed class MetarialRequestQueryHandler(
        IMetarialRepository metarialRepository,
        ICurrentUserService currentUser) : IRequestHandler<MetarialRequestQueryCommand, Result<List<MaterialsRequestVM>>>
    {
        public async Task<Result<List<MaterialsRequestVM>>> Handle(MetarialRequestQueryCommand request, CancellationToken cancellationToken)
        {
            var result = await metarialRepository.Where(p =>
            p.CompanyId == currentUser.UserCompanyId &&
            p.MaterialType == request.MetarialType &&
            p.ComponentId == request.searchValue).
            Include(p => p.Component).
            ThenInclude(p => p.RequestedMaterial.Where(p => p.Request.RequestStatu == (int)RequestStatu.StockMan && p.Request.CompanyId == currentUser.UserCompanyId)).
            ThenInclude(p => p.Request).
            GroupBy(p => new { p.ComponentId, p.MaterialType }).
            ToListAsync(cancellationToken);
            if (result.Count > 0)
            {
                return result.Select(p => new MaterialsRequestVM
                {
                    ComponentId = p.Key.ComponentId,
                    MaterialType = p.Key.MaterialType,
                    EquipmentDescription = p.FirstOrDefault().Component.EquipmentDescription,
                    MaxDefective = p.Sum(p => p.Defective) - p.FirstOrDefault().Component.RequestedMaterial.Sum(c => c.Defective),
                    MaxSturdy = p.Sum(p => p.Sturdy) - p.FirstOrDefault().Component.RequestedMaterial.Sum(c => c.Sturdy),
                    MaxScrap = p.Sum(p => p.Scrap) - p.FirstOrDefault().Component.RequestedMaterial.Sum(c => c.Scrap),
                }).Where(c => c.MaxScrap + c.MaxSturdy + c.MaxDefective > 0).ToList();
            }
            return Result<List<MaterialsRequestVM>>.Failure("Malzeme bulunamadı");
        }
    }
}
