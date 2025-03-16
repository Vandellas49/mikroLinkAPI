using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Requests.MetarialRequestTeamLeader
{
    public sealed record class MetarialRequestTeamLeaderCommand(int RequestId, List<RequestMaterialVM> RequestMaterial) : IRequest<Result<string>>;
}
