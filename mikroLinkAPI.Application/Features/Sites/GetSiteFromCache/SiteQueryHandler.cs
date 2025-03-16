using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Filters;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;

namespace mikroLinkAPI.Application.Features.Sites.GetSiteFromCache
{
    internal sealed class SiteFromCacheQueryHandler(ISiteRepository siteRepository,IMapper mapper) : IRequestHandler<SiteFromCacheQueryCommand, Result<List<SiteVM>>>
    {
        public async Task<Result<List<SiteVM>>> Handle(SiteFromCacheQueryCommand request, CancellationToken cancellationToken)
        {
            List<Site> result = [.. (await siteRepository.GetAllFromCacheAsync()).Take(10).OrderBy(p => p.SiteName)];
            return mapper.Map<List<SiteVM>>(result);
        }
    }

}
