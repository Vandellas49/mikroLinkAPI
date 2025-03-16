using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.SiteMetarial.GetMetarials
{
    public sealed record SiteMetarialQueryCommand(FilterBySiteMetarial filters, PageSettings Page) :IRequest<Result<Inventory<ComponentSerialVM>>>;
}
