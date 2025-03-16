using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class Request : Entity
    {
        public Request()
        {
            RequestSiteCompanySerial = new HashSet<RequestSiteCompanySerial>();
            RequestedMaterial = new HashSet<RequestedMaterial>();
            StoreExit = new HashSet<StoreExit>();
            StoreIn = new HashSet<StoreIn>();
        }
        public int RequestType { get; set; }
        public int WhoDoneId { get; set; }
        public int TeamLeaderId { get; set; }
        public int? CompanyId { get; set; }
        public int? SiteId { get; set; }
        public int RequestStatu { get; set; }
        public DateTime RequestDate { get; set; }
        public string WorkOrderNo { get; set; }
        public int? ReceiverSiteId { get; set; }
        public string RequestMessage { get; set; }
        public int? ReceiverCompanyId { get; set; }
        public int? WhoCanceledId { get; set; }
        public int CanUpdate { get; set; }


        public Company Company { get; set; }
        public Company ReceiverCompany { get; set; }
        public Site ReceiverSite { get; set; }
        public Site Site { get; set; }
        public AccountSsom TeamLeader { get; set; }
        public AccountSsom WhoCanceled { get; set; }
        public AccountSsom WhoDone { get; set; }
        public ICollection<RequestSiteCompanySerial> RequestSiteCompanySerial { get; set; }
        public ICollection<RequestedMaterial> RequestedMaterial { get; set; }
        public ICollection<StoreExit> StoreExit { get; set; }
        public ICollection<StoreIn> StoreIn { get; set; }
    }
}
