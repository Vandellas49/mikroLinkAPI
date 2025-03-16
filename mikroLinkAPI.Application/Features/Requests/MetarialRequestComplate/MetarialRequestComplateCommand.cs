using MediatR;
using mikroLinkAPI.Domain.Attributes;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Requests.MetarialRequestComplate
{
    [Transaction]
    public sealed record class MetarialRequestComplateCommand(int RequestId, List<RequestMaterialVM> RequestMaterial) : IRequest<Result<string>>;
}
