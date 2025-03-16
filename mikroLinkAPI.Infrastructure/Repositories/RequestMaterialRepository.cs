using GenericRepository;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Infrastructure.Context;

namespace mikroLinkAPI.Infrastructure.Repositories
{
    internal class RequestMaterialRepository(ApplicationDbContext context, IMemoryCache memoryCache) : Repository<RequestedMaterial,ApplicationDbContext>(context,memoryCache), IRequestMaterialRepository;

}
