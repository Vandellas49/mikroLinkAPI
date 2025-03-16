using GenericRepository;
using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using mikroLinkAPI.Domain;
using mikroLinkAPI.Domain.Interfaces;
using mikroLinkAPI.Domain.Interfaces.Kafka;
using mikroLinkAPI.Domain.Repositories;
using mikroLinkAPI.Infrastructure.Context;
using mikroLinkAPI.Infrastructure.Interceptors;
using mikroLinkAPI.Infrastructure.Jobs;
using mikroLinkAPI.Infrastructure.KafkaMessaging;
using mikroLinkAPI.Infrastructure.Options;
using mikroLinkAPI.Infrastructure.Repositories;
using mikroLinkAPI.Infrastructure.Services;
using Scrutor;
using System.Reflection;

namespace mikroLinkAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITransactionUnitOfWork, TransactionUnitOfWork>();
            services.AddScoped<InventoryInterceptor>();
            services.AddScoped<ComponentSerialSaveChangesInterceptor>();
            services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")).
                AddInterceptors(serviceProvider.GetRequiredService<InventoryInterceptor>(), serviceProvider.GetRequiredService<ComponentSerialSaveChangesInterceptor>());
            });
            //RecurringJob.AddOrUpdate<IDailyResetService>("ResetUserSessions", x => x.ResetDailySessions(), "*/2 * * * *");

            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection"));
            });
            services.AddHangfireServer();
            services.AddSingleton<IMonitoringApi>(provider =>
            {
                var storage = provider.GetRequiredService<JobStorage>();
                return storage.GetMonitoringApi();
            });
            //services.AddHostedService<StartupJobService>();
            //RecurringJob.AddOrUpdate<IDailyResetService>("ResetUserSessions", x => x.ResetDailySessions(), "0 0 * * *"); 



            services.AddSingleton<IKafkaConfig, KafkaConfig>();
            services.AddSingleton<IKafkaProducerService, KafkaProducerService>();
            services.AddSingleton<IKafkaConsumerService, KafkaConsumerService>();
            services.AddHostedService<KafkaBackgroundService>();
            services.AddSingleton<IKafkaConfigService, KafkaConfigService>();
            services.AddSingleton<IManualKafkaConsumerService, ManualKafkaConsumerService>();

       

            services.AddScoped<IUnitOfWork>(srv => srv.GetRequiredService<ApplicationDbContext>());
            services.Configure<JwtOptions>(configuration.GetSection("Jwt"));
            services.ConfigureOptions<JwtTokenOptionsSetup>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer();
            services.AddAuthorization();

            services.Scan(action =>
            {
                action
                .FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(publicOnly: false)
                .UsingRegistrationStrategy(RegistrationStrategy.Skip)
                .AsMatchingInterface()
                .AsImplementedInterfaces()
                .WithScopedLifetime();
               
            });
            services.AddHealthChecks()
            .AddCheck("health-check", () => HealthCheckResult.Healthy())
            .AddDbContextCheck<ApplicationDbContext>()
            ;
            return services;
        }
    }
}
