using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using mikroLinkAPI.Application.Features.Auth.Login;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Entities;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Infrastructure.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace mikroLinkAPI.Infrastructure.Services
{
    internal class JwtProvider(
        IOptions<JwtOptions> jwtOptions) : IJwtProvider
    {
        public  LoginCommandResponse CreateToken(AccountSsom user)
        {
            List<Claim> claims =
            [
                new Claim("Id", user.Id.ToString()),
                new Claim("Name", user.Name),
                new Claim("Email", user.Email),
                new Claim("UserName", user.UserName),
                new Claim("CompanyName", user.Company.Name),
                new Claim("Company", user.CompanyId.ToString())
            ];

            DateTime expires = DateTime.UtcNow.AddMonths(1);


            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Value.SecretKey));

            JwtSecurityToken jwtSecurityToken = new(
                issuer: jwtOptions.Value.Issuer,
                audience: jwtOptions.Value.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));

            JwtSecurityTokenHandler handler = new();

            string token = handler.WriteToken(jwtSecurityToken);

            string refreshToken = Guid.NewGuid().ToString();
            DateTime refreshTokenExpires = expires.AddHours(1);

            //user.RefreshToken = refreshToken;
            //user.RefreshTokenExpires = refreshTokenExpires;

            // userManager.Update(user);

            return new(token, refreshToken, refreshTokenExpires);
        }
    }
}
