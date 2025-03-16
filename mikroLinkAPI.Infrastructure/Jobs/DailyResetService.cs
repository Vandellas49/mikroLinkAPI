using GenericRepository;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Interfaces;
using mikroLinkAPI.Domain.Repositories;

namespace mikroLinkAPI.Infrastructure.Jobs
{
    public class DailyResetService(IUserSessionRepository userSessionRepository, ICacheHelper cache, IUnitOfWork unitOfWork) : IDailyResetService
    {
        public async Task ResetDailySessions()
        {
            var cacheEntries = await cache.GetUserSessionsAsync();
            foreach (var entry in cacheEntries)
            {
                var user = await userSessionRepository.FirstOrDefaultAsync(c => c.UserName == entry.Key && c.Date.Date == DateTime.Now.Date);
                if (user != null)
                {
                    user.TotalDuration = !entry.Value.Item3 ? entry.Value.Item2 : (DateTime.Now - entry.Value.Item1).TotalSeconds + (user.Date == entry.Value.Item1 ? 0 : entry.Value.Item2);
                    userSessionRepository.Update(user);
                }
                else
                {
                    var session = new UserSession
                    {
                        UserName = entry.Key.ToString(),
                        Date = DateTime.Now,
                        TotalDuration = !entry.Value.Item3 ? entry.Value.Item2 : (DateTime.Now - entry.Value.Item1).TotalSeconds + entry.Value.Item2,
                    };
                    await userSessionRepository.AddAsync(session);
                }
            }
            await unitOfWork.SaveChangesAsync();
        }

    }

}


