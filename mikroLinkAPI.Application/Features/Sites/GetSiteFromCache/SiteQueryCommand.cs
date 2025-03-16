using MediatR;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Sites.GetSiteFromCache
{
    public sealed record SiteFromCacheQueryCommand() :IRequest<Result<List<SiteVM>>>;

}
