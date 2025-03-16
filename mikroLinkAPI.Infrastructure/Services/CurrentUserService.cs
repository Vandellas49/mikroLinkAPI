using Microsoft.AspNetCore.Http;
using mikroLinkAPI.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace mikroLinkAPI.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public int UserId =>Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue("Id"));

        public string UserEmail => _httpContextAccessor.HttpContext?.User.FindFirstValue("Email");

        public string UserName => _httpContextAccessor.HttpContext?.User.FindFirstValue("UserName");

        public int UserCompanyId =>Convert.ToInt32(_httpContextAccessor.HttpContext?.User.FindFirstValue("Company"));

        public bool IsAuthenticated => _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;

    }
}
