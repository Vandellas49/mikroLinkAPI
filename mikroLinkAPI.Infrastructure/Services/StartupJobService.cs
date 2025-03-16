using Hangfire;
using Microsoft.Extensions.Hosting;
using mikroLinkAPI.Infrastructure.Jobs;

namespace mikroLinkAPI.Infrastructure.Services
{
    public class StartupJobService : IHostedService
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
