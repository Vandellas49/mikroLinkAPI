
namespace mikroLinkAPI.Application.Services
{
    public interface ICurrentUserService
    {
        int UserId { get; }
        string UserEmail { get; }
        string UserName { get; }
        int UserCompanyId { get; }
        bool IsAuthenticated { get; }
    }
}
