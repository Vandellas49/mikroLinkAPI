using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.ComponentDelete
{
    public sealed record ComponentDeleteCommand(string Id): IRequest<Result<string>>;
}
