using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.GetComponentByCode
{
    public sealed record ComponentByCodeQuery(string parcaKodu) : IRequest<Result<List<ComponentVM>>>;

}
