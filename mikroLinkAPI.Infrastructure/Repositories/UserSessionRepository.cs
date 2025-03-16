using GenericRepository;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Infrastructure.Context;

namespace mikroLinkAPI.Infrastructure.Repositories;
internal sealed class UserSessionRepository(ApplicationDbContext context, IMemoryCache memoryCache) : Repository<UserSession, ApplicationDbContext>(context, memoryCache), IUserSessionRepository;
