using MediatR;
using mikroLinkAPI.Domain.Attributes;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.RandevuPlanlama.RandevuPlanlamaAdd
{
    public sealed record RandevuPlanlamaAddCommand(List<RandevuPlanlamaVM> planlama) : IRequest<Result<string>>;
}
