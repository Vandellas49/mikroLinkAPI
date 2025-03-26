using mikroLinkAPI.Application.Services;


namespace mikroLinkAPI.Infrastructure.Services
{
    public class UserSessionService(ICacheHelper cache) : IUserSessionService
    {
         readonly TimeSpan cacheExpiration = TimeSpan.FromHours(24);
        public void UserConnected(string userId)
        {
            var now = DateTime.Now;
            if (cache.TryGet(userId, out Tuple<DateTime, double, bool> existingData))
                cache.Set(userId, GetDuration(existingData, true), cacheExpiration);
            else
                cache.Set(userId, new Tuple<DateTime, double, bool>(now, 0,true), cacheExpiration); 
        }

        public void UserDisconnected(string userId)
        {
            if (cache.TryGet(userId, out Tuple<DateTime, double, bool> existingData))
            {
                cache.Set(userId, GetDuration(existingData,false), cacheExpiration);
            }
        }
        private Tuple<DateTime, double, bool> GetDuration(Tuple<DateTime, double, bool> data,bool IsConnected)
        {
            return new Tuple<DateTime, double,bool>(DateTime.Now,(DateTime.Now - data.Item1).TotalSeconds+data.Item2, IsConnected);
        }
        public void WriteDailyDataToDatabase()
        {
            throw new NotImplementedException();
        }
    }
}
