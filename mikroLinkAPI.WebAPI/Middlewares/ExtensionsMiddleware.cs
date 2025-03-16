using Microsoft.AspNetCore.Identity;
using mikroLinkAPI.Domain.Entities;

namespace mikroLinkAPI.WebAPI.Middlewares
{
    public static class ExtensionsMiddleware
    {
        public static void CreateFirstUser(WebApplication app)
        {
            using (var scoped = app.Services.CreateScope())
            {
                //var userManager = scoped.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                //if (!userManager.Users.Any(p => p.UserName == "Vandellas"))
                //{
                //    AccountSsom user = new()
                //    {
                //        UserName = "Vandellas",
                //        Email = "mhmtglr49@gmail.com",
                //        Name = "Mehmet",
                //        Surname = "Güler"
                //    };

                //   // userManager.CreateAsync(user, "12345678").Wait();
                //}
            }
        }
    }
}
