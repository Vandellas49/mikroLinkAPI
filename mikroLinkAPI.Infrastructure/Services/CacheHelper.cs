using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Domain.ViewModel;
using System.Collections.Concurrent;


namespace mikroLinkAPI.Infrastructure.Services
{
    public class CacheHelper(IMemoryCache _cache, IUserRepository userRepository, ICompanyRepository companyRepository) : ICacheHelper
    {
        public void Set<T>(string key, T value, TimeSpan? expiration)
        {
            if (expiration != null)
                _cache.Set(key, value, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiration
                });
            else
                _cache.Set(key, value);

        }
        public bool TryGet<T>(string key, out T value)
        {
            return _cache.TryGetValue(key, out value);
        }
        public void Remove(string key)
        {
            _cache.Remove(key);


        }
        private async Task<List<string>> GetKeysAsync()
        {
            return await userRepository.Where(c => c.Status).Select(c => c.UserName).ToListAsync();
        }
        public async Task<Dictionary<string, ConcurrentDictionary<string, ConcurrentBag<UserSignalR>>>> GetUserOnlineAsync()
        {
            var companies = await companyRepository.GetAll().ToListAsync();
            var allusers = new Dictionary<string, ConcurrentDictionary<string, ConcurrentBag<UserSignalR>>>();
            foreach (var item in companies)
                if (_cache.TryGetValue($"Firm_{item.Id}", out ConcurrentDictionary<string, ConcurrentBag<UserSignalR>> users))
                    allusers.Add($"Firm_{item.Id}", users);
            return allusers;
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
