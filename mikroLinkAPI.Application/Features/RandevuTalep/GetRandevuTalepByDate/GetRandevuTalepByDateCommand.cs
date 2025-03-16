using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.RandevuTalep.GetRandevuTalepByDate
{
    public sealed record GetRandevuTalepByDateCommand(string Date) : IRequest<Result<List<RandevuTalepByDateVM>>>;
}
