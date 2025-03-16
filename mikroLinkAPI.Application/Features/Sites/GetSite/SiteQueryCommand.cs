using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Sites.GetSite
{
    public sealed record SiteQueryCommand(PageSettings Page, FilterBySite Filters ) :IRequest<Result<Inventory<SiteVM>>>;

}
