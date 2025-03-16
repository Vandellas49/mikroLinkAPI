
namespace mikroLinkAPI.Application.Services
{
    public interface IUserSessionService
    {
        void UserConnected(string userId);
        void UserDisconnected(string userId);
        void WriteDailyDataToDatabase();
    }
}
