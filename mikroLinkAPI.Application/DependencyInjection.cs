using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using mikroLinkAPI.Application.Behaviors;

namespace mikroLinkAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);

            services.AddMediatR(conf =>
            {
                conf.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly);
                conf.AddOpenBehavior(typeof(ValidationBehavior<,>));
                conf.AddOpenBehavior(typeof(TransactionBehavior<,>));
            });

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }
    }
}
