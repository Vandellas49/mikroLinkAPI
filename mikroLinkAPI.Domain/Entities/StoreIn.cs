

using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class StoreIn:Entity
    {
        public int CserialId { get; set; }
        public int? CompanyId { get; set; }
        public int? SiteId { get; set; }
        public int? TeamLeaderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Sturdy { get; set; }
        public int Defective { get; set; }
        public int Scrap { get; set; }
        public int? FromCompanyId { get; set; }
        public int? FromSiteId { get; set; }
        public int? FromTeamLeaderId { get; set; }
        public int? RequestId { get; set; }
        public int EnterType { get; set; }
        public int CreatedBy { get; set; }


        public  Company Company { get; set; }
        public  ComponentSerial Cserial { get; set; }
        public  Company FromCompany { get; set; }
        public  Site FromSite { get; set; }
        public  AccountSsom FromTeamLeader { get; set; }
        public  Request Request { get; set; }
        public  Site Site { get; set; }
        public  AccountSsom TeamLeader { get; set; }
        public  AccountSsom WhoDone { get; set; }
    }
}
