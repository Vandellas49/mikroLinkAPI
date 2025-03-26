using mikroLinkAPI.Domain.Attributes;

namespace mikroLinkAPI.Domain.Interfaces
{
    [GetJobInfo("Günlük Kullanıcı Giriş Süresi")]
    public interface IDailyResetService
    {
        Task ResetDailySessions();
        Task CheckUserSessions();
    }
}
