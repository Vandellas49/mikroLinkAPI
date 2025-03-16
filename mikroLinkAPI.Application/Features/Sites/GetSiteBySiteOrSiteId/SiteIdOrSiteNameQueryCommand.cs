using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Sites.GetSiteBySiteOrSiteId
{
    public sealed record SiteIdOrSiteNameQueryCommand(string siteName) : IRequest<Result<List<SiteVM>>>;

    internal sealed class SiteIdOrSiteNameQueryHandler(ISiteRepository siteRepository, IMapper mapper) : IRequestHandler<SiteIdOrSiteNameQueryCommand, Result<List<SiteVM>>>
    {
        public async Task<Result<List<SiteVM>>> Handle(SiteIdOrSiteNameQueryCommand request, CancellationToken cancellationToken)
        {
            var query = siteRepository.Where(p => true);
            if (!string.IsNullOrEmpty(request.siteName))
            {
                query = siteRepository.Where(p => p.SiteName.Contains(request.siteName) || p.SiteId.Contains(request.siteName));
            }
            return mapper.Map<List<SiteVM>>(await query.ToListAsync(cancellationToken));
        }
    }
}
