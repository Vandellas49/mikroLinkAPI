
using mikroLinkAPI.Domain.ViewModel;
using System.Collections.Concurrent;

namespace mikroLinkAPI.Application.Services
{
    public interface ICacheHelper
    {
        void Set<T>(string key, T value, TimeSpan? duration=null);
        bool TryGet<T>(string key, out T value);
        void Remove(string key);
        Task<Dictionary<string, Tuple<DateTime, double,bool>>> GetUserSessionsAsync();
        Task<Dictionary<string, ConcurrentDictionary<string, ConcurrentBag<UserSignalR>>>> GetUserOnlineAsync();
    }
}
