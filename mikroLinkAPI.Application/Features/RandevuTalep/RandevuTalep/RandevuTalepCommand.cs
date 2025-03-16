using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.RandevuTalep.RandevuTalep
{
    public sealed record RandevuTalepCommand(int Id) : IRequest<Result<string>>;
}
