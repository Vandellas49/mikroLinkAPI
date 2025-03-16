using MediatR;
using mikroLinkAPI.Application.Features.Sites.GetSite;
using mikroLinkAPI.Domain.ViewModel;
namespace mikroLinkAPI.Application.Features.Sites.SiteExport
{
    public sealed record SiteExportCommand(FilterBySite Filters) : IRequest<Result<byte[]>>;
}
