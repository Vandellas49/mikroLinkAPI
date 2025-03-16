using Hangfire.Server;
using mikroLinkAPI.Domain.Attributes;

namespace mikroLinkAPI.Domain.Interfaces
{
    [GetJobInfo("Saha Malzeme Girişi")]

    public interface IProcessSiteMetarilInsertExcelJob
    {
        Task RunAsync(int fileId, int UserId, PerformContext context);
    }
}
