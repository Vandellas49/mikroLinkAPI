using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Components.GetComponent
{
    public sealed record ComponentQueryCommand(PageSettings Page, FilterByComponent Filters) :IRequest<Result<Inventory<ComponentVM>>>;
}
