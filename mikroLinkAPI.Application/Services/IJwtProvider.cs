using mikroLinkAPI.Application.Features.Auth.Login;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.Application.Services
{
    public interface IJwtProvider
    {
        LoginCommandResponse CreateToken(AccountSsom user);
    }
}
