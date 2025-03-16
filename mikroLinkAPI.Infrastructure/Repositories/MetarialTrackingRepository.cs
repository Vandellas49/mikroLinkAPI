using GenericRepository;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using mikroLinkAPI.Infrastructure.Context;

namespace mikroLinkAPI.Infrastructure.Repositories;
internal sealed class MetarialTrackingRepository(ApplicationDbContext context, IMemoryCache memoryCache) : Repository<MaterialTracking, ApplicationDbContext>(context, memoryCache), IMaterialTrackingRepository;

