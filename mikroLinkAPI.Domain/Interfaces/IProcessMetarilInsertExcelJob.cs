using Hangfire;
using Hangfire.Server;
using mikroLinkAPI.Domain.Attributes;

namespace mikroLinkAPI.Domain.Interfaces
{
    [GetJobInfo("Depo Malzeme Girişi")]
    public interface IProcessMetarilInsertExcelJob
    {
        [AutomaticRetry(Attempts = 0)]

        Task RunAsync(int fileId, int UserId, int CompanyId, PerformContext context);
    }
}
