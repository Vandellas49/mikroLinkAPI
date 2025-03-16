using Hangfire.Storage;
using Hangfire.Storage.Monitoring;
using MediatR;
using mikroLinkAPI.Application.Services;
using mikroLinkAPI.Domain.Attributes;
using mikroLinkAPI.Domain.Interfaces;
using mikroLinkAPI.Domain.ViewModel;
using System.Reflection;


namespace mikroLinkAPI.Application.Features.HangFire
{
    internal sealed class GetIJobInfoCommandHandler(IMonitoringApi _monitoringApi, ICurrentUserService currentUserService) : IRequestHandler<GetJobInfoCommand, Result<Inventory<GetJobInfoResponse>>>
    {
        public async Task<Result<Inventory<GetJobInfoResponse>>> Handle(GetJobInfoCommand request, CancellationToken cancellationToken)
        {
            int startIndex = (request.Page.page) * request.Page.pageSize;
            var succeededJobs = _monitoringApi.SucceededJobs(startIndex, request.Page.pageSize);
            var processingJobs = _monitoringApi.ProcessingJobs(startIndex, request.Page.pageSize);
            var scheduledJobs = _monitoringApi.ScheduledJobs(startIndex, request.Page.pageSize);
            var failedJobs = _monitoringApi.FailedJobs(startIndex, request.Page.pageSize);
            long totalSucceededCount = _monitoringApi.SucceededJobs(0, int.MaxValue)
                                        .Count(j => j.Value.InvocationData.Arguments.ToString().Contains(currentUserService.UserId.ToString()));

            long totalProcessingCount = _monitoringApi.ProcessingJobs(0, int.MaxValue)
                                         .Count(j => j.Value.InvocationData.Arguments.ToString().Contains(currentUserService.UserId.ToString()));

            long totalScheduledCount = _monitoringApi.ScheduledJobs(0, int.MaxValue)
                                      .Count(j => j.Value.InvocationData.Arguments.ToString().Contains(currentUserService.UserId.ToString()));

            long totalFailedCount = _monitoringApi.FailedJobs(0, int.MaxValue)
                                       .Count(j => j.Value.InvocationData.Arguments.ToString().Contains(currentUserService.UserId.ToString()));

            long totalCount = totalSucceededCount + totalProcessingCount + totalScheduledCount + totalFailedCount;

            var allJobs = new List<GetJobInfoResponse>();

            allJobs.AddRange(GetSucceededJobs(succeededJobs));
            allJobs.AddRange(GetProcessingJobs(processingJobs));
            allJobs.AddRange(GetScheduledJobs(scheduledJobs));
            allJobs.AddRange(GetFailedJobs(failedJobs));

            await Task.CompletedTask;
            return new Inventory<GetJobInfoResponse>(allJobs, (int)totalCount);
        }
        private List<GetJobInfoResponse> GetSucceededJobs(JobList<SucceededJobDto> jobs)
        {
            return jobs
                .Where(j => j.Value.InvocationData.Arguments.ToString().Contains(currentUserService.UserId.ToString()))
                .Select(item =>
                {
                    var jobDetails = _monitoringApi.JobDetails(item.Key);
                    return new GetJobInfoResponse
                    {
                        CreatedDate = jobDetails.CreatedAt,
                        HataExcelId = jobDetails.Properties.FirstOrDefault(c => c.Key == "ErrorReportFileId").Value,
                        ProcessDate = jobDetails.ExpireAt,
                        Status = jobDetails.Properties.FirstOrDefault(c => c.Key == "ErrorReportFileId").Value == null ? "Başarılı" : "Hatalı",
                        TalepExcelId = item.Value.Job.Args.FirstOrDefault()?.ToString(),
                        TotalCount = jobDetails.Properties.FirstOrDefault(c => c.Key == "TotalCount").Value,
                        JobName = item.Value.Job.Type.GetCustomAttribute<GetJobInfoAttribute>().Value
                    };
                })
                .ToList();
        }

        // İşlenmekte olan (processing) jobları işler
        private List<GetJobInfoResponse> GetProcessingJobs(JobList<ProcessingJobDto> jobs)
        {
            return jobs.Where(j => j.Value.InvocationData.Arguments.ToString().Contains(currentUserService.UserId.ToString()))
                .Select(item =>
                {
                    var jobDetails = _monitoringApi.JobDetails(item.Key);
                    return new GetJobInfoResponse
                    {
                        CreatedDate = jobDetails.CreatedAt,
                        HataExcelId = null,
                        ProcessDate = jobDetails.ExpireAt,
                        Status = "Beklemede",
                        TalepExcelId = item.Value.Job.Args.FirstOrDefault()?.ToString(),
                        TotalCount = null,
                        JobName = item.Value.Job.Type.GetCustomAttribute<GetJobInfoAttribute>().Value
                    };
                })
                .ToList();
        }

        // Zamanlanmış (scheduled) jobları işler
        private List<GetJobInfoResponse> GetScheduledJobs(JobList<ScheduledJobDto> jobs)
        {
            return jobs
                .Where(j => j.Value.InvocationData.Arguments.ToString().Contains(currentUserService.UserId.ToString()))
                .Select(item =>
                {
                    var jobDetails = _monitoringApi.JobDetails(item.Key);
                    return new GetJobInfoResponse
                    {
                        CreatedDate = jobDetails.CreatedAt,
                        HataExcelId = null,
                        ProcessDate = jobDetails.ExpireAt,
                        Status = "Zamanlanmış",
                        TalepExcelId = item.Value.Job.Args.FirstOrDefault()?.ToString(),
                        TotalCount = null,
                        JobName = item.Value.Job.Type.GetCustomAttribute<GetJobInfoAttribute>().Value

                    };
                })
                .ToList();
        }

        // **Hatalı (failed) jobları işler**
        private List<GetJobInfoResponse> GetFailedJobs(JobList<FailedJobDto> jobs)
        {
            return jobs
                .Where(j => j.Value.InvocationData.Arguments.ToString().Contains(currentUserService.UserId.ToString()))
                .Select(item =>
                {
                    var jobDetails = _monitoringApi.JobDetails(item.Key);
                    return new GetJobInfoResponse
                    {
                        CreatedDate = jobDetails.CreatedAt,
                        HataExcelId = null,
                        ProcessDate = jobDetails.ExpireAt,
                        Status = "Hatalı",
                        TalepExcelId = item.Value.Job.Args.FirstOrDefault()?.ToString(),
                        TotalCount = jobDetails.LoadException?.Message,
                        JobName = item.Value.Job.Type.GetCustomAttribute<GetJobInfoAttribute>().Value
                    };
                })
                .ToList();
        }

    }

}
