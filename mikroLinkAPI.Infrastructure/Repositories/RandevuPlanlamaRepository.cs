using GenericRepository;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Infrastructure.Context;

namespace mikroLinkAPI.Infrastructure.Repositories
{
    internal sealed class RandevuPlanlamaRepository(ApplicationDbContext context, IMemoryCache memoryCache) : Repository<RandevuPlanlanma, ApplicationDbContext>(context, memoryCache), IRandevuPlanlamaRepository;

}
