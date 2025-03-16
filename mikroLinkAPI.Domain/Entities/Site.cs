using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class Site : Entity
    {
        public Site()
        {
            CompanySiteAuthorization = new HashSet<CompanySiteAuthorization>();
            ComponentSerial = new HashSet<ComponentSerial>();
            MaterialTracking = new HashSet<MaterialTracking>();
            RequestReceiverSite = new HashSet<Request>();
            RequestSite = new HashSet<Request>();
            StoreExitSite = new HashSet<StoreExit>();
            StoreExitSiteIdExitNavigation = new HashSet<StoreExit>();
            StoreInFromSite = new HashSet<StoreIn>();
            StoreInSite = new HashSet<StoreIn>();
        }

        public int IlId { get; set; }
        public string PlanId { get; set; }
        public string SiteId { get; set; }
        public string SiteName { get; set; }
        public string SiteTip { get; set; }
        public string KordinatN { get; set; }
        public string KordinatE { get; set; }
        public string Region { get; set; }

        public Iller Il { get; set; }
        public ICollection<CompanySiteAuthorization> CompanySiteAuthorization { get; set; }
        public ICollection<ComponentSerial> ComponentSerial { get; set; }
        public ICollection<MaterialTracking> MaterialTracking { get; set; }
        public ICollection<Request> RequestReceiverSite { get; set; }
        public ICollection<Request> RequestSite { get; set; }
        public ICollection<StoreExit> StoreExitSite { get; set; }
        public ICollection<StoreExit> StoreExitSiteIdExitNavigation { get; set; }
        public ICollection<StoreIn> StoreInFromSite { get; set; }
        public ICollection<StoreIn> StoreInSite { get; set; }
    }
}
