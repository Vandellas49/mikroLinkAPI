using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.RandevuPlanlama.GetRandevuPlanlama
{
    public sealed record GetRandevuPlanlamaCommand : IRequest<Result<List<RandevuPlanlamaVM>>>;
}
