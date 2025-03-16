using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.RandevuTalep.GetRandevuTalep
{
    public sealed record GetRandevuTalepCommand() : IRequest<Result<List<RandevuTalepVM>>>;
}
