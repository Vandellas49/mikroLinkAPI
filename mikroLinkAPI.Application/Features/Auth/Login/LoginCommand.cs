using MediatR;
using mikroLinkAPI.Domain.Entities;
using System.Text;
using TS.Result;

namespace mikroLinkAPI.Application.Features.Auth.Login
{
    public sealed record LoginCommand(
        string Username,
        string Password) : IRequest<Result<LoginCommandResponse>>
    {
        public static implicit operator AccountSsom(LoginCommand userDetail)
        {
            return new AccountSsom
            {
                UserName = userDetail.Username,
                Password = !string.IsNullOrEmpty(userDetail.Password) ? Encoding.ASCII.GetBytes(userDetail.Password) : null,
            };
        }
    }
}
