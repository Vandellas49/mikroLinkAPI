using Hangfire;
using Hangfire.Server;
using mikroLinkAPI.Domain.Attributes;

namespace mikroLinkAPI.Domain.Interfaces
{
    [GetJobInfo("Saha Malzeme Girişi")]

    public interface IProcessSiteMetarilInsertExcelJob
    {
        [AutomaticRetry(Attempts = 0)]

        Task RunAsync(int fileId, int UserId, PerformContext context);
    }
}
