using GenericRepository;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Infrastructure.Context;

namespace mikroLinkAPI.Infrastructure.Repositories
{
    internal class LoginRepository(ApplicationDbContext context, IMemoryCache memoryCache) : Repository<AccountSsom, ApplicationDbContext>(context,memoryCache), ILoginRepository;
}
