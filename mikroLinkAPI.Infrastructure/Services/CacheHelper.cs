using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;


namespace mikroLinkAPI.Infrastructure.Services
{
    public class CacheHelper(IMemoryCache _cache, IUserRepository userRepository) : ICacheHelper
    {
        public void Set(string key, Tuple<DateTime, double, bool> value, TimeSpan? expiration)
        {
            if (expiration != null)
                _cache.Set(key, value, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                });
            else
                _cache.Set(key, value);

        }
        public bool TryGet(string key, out Tuple<DateTime, double, bool> value)
        {
            return _cache.TryGetValue(key, out value);
        }
        public void Remove(string key)
        {
            _cache.Remove(key);


        }
        private async Task<List<string>> GetKeysAsync()
        {
            return await userRepository.Where(c=>c.Status).Select(c=>c.UserName).ToListAsync();
        }
        public async Task<Dictionary<string, Tuple<DateTime, double, bool>>> GetUserSessionsAsync()
        {
            var userSessions = new Dictionary<string, Tuple<DateTime, double, bool>>();

            foreach (var key in await GetKeysAsync())
            {
                if (_cache.TryGetValue(key, out Tuple<DateTime, double, bool> value))
                {
                    userSessions[key] = value;
                }
            }

            return userSessions;
        }

    }
}
