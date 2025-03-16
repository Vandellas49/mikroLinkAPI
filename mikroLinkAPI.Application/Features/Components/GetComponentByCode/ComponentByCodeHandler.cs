using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.GetComponentByCode
{
    internal sealed class ComponentByCodeHandler(IComponentRepository componentRepository,IMapper mapper) : IRequestHandler<ComponentByCodeQuery, Result<List<ComponentVM>>>
    {
        public async Task<Result<List<ComponentVM>>> Handle(ComponentByCodeQuery request, CancellationToken cancellationToken)
        {
            return mapper.Map<List<ComponentVM>>(await componentRepository.Where(p => p.Id.Contains(request.parcaKodu)).ToListAsync(cancellationToken));
        }
    }

}
