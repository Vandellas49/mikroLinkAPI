
namespace mikroLinkAPI.Application.Services
{
    public interface ICacheHelper
    {
        void Set(string key, Tuple<DateTime, double,bool> value, TimeSpan? duration=null);
        bool TryGet(string key, out Tuple<DateTime, double,bool> value);
        void Remove(string key);
        Task<Dictionary<string, Tuple<DateTime, double,bool>>> GetUserSessionsAsync();
    }
}
