using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Materials.GetMetarialForRequest.GetMatreialByRequestAndSeriNo
{
    public sealed record GetMatreialByRequestAndSeriNoCommand(string SearchValue, int? RequestId) : IRequest<Result<ComponentSerialVM>>;
    internal sealed class MatreialByRequestAndSeriNoHandler(
        IMetarialRepository metarialRepository,
        IMapper mapper,
        ICurrentUserService currentUser) : IRequestHandler<GetMatreialByRequestAndSeriNoCommand, Result<ComponentSerialVM>>
    {
        public async Task<Result<ComponentSerialVM>> Handle(GetMatreialByRequestAndSeriNoCommand request, CancellationToken cancellationToken)
        {
            var result = await metarialRepository.
            Where(p => p.SeriNo == request.SearchValue && p.CompanyId == currentUser.UserCompanyId&&p.Component.RequestedMaterial.Any(c=>c.RequestId==request.RequestId)).
            Include(p => p.Component).
            ThenInclude(p=>p.RequestedMaterial).
            FirstOrDefaultAsync(cancellationToken);
            if (result == null)
                return Result<ComponentSerialVM>.Failure("Malzeme bulunamadı");
            return mapper.Map<ComponentSerialVM>(result);
        }
    }
}
