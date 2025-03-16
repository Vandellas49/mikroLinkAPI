

using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class MaterialTracking:Entity
    {
        public int CserialId { get; set; }
        public int? CompanyId { get; set; }
        public int? SiteId { get; set; }
        public int? TeamLeaderId { get; set; }
        public int Sturdy { get; set; }
        public int Defective { get; set; }
        public int Scrap { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int State { get; set; }
        public Company Company { get; set; }
        public  ComponentSerial Cserial { get; set; }
        public  Site Site { get; set; }
        public  AccountSsom TeamLeader { get; set; }

    }
}
