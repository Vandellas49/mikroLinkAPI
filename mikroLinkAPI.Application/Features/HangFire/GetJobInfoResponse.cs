
namespace mikroLinkAPI.Application.Features.HangFire
{
    public class GetJobInfoResponse
    {
        public DateTime? CreatedDate { get; set; }
        public DateTime? ProcessDate { get; set; }
        public string Status { get; set; }
        public string TotalCount { get; set; }
        public string HataExcelId { get; set; }
        public string TalepExcelId { get; set; }
        public string JobName { get; set; }
        public string ErrorMessage { get; set; }

    }
}
