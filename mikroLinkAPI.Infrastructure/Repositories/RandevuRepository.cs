using GenericRepository;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Infrastructure.Context;

namespace mikroLinkAPI.Infrastructure.Repositories
{
    internal sealed class RandevuRepository(ApplicationDbContext context, IMemoryCache memoryCache) : Repository<Randevu, ApplicationDbContext>(context, memoryCache), IRandevuRepository;

}
