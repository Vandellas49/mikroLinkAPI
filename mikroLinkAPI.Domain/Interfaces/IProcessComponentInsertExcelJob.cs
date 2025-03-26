using Hangfire;
using Hangfire.Server;
using mikroLinkAPI.Domain.Attributes;

namespace mikroLinkAPI.Domain.Interfaces
{
    [GetJobInfo("Parça Girişi")]
    public interface IProcessComponentInsertExcelJob
    {
        [AutomaticRetry(Attempts = 0)]
        Task RunAsync(int fileId, int UserId,  PerformContext context);
    }
}
