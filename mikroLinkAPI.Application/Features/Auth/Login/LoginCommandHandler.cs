using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using TS.Result;

namespace mikroLinkAPI.Application.Features.Auth.Login
{
    internal sealed class LoginCommandHandler(
        ILoginRepository userManager,
        IJwtProvider jwtProvider) : IRequestHandler<LoginCommand, Result<LoginCommandResponse>>
    {
        public async Task<Result<LoginCommandResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            AccountSsom login = request;
            AccountSsom user = await userManager.Includes(c=>c.Company)
                .FirstOrDefaultAsync(p =>
                p.UserName == login.UserName&&p.Password== login.Password,
                cancellationToken);

            if (user is null)
            {
                return (500, "Kullanıcı adı şifre hatalı");
            }

            var loginResponse =  jwtProvider.CreateToken(user);


            return loginResponse;
        }
    }
}
