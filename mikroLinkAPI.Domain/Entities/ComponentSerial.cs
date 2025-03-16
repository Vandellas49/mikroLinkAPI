using Microsoft.EntityFrameworkCore;
using mikroLinkAPI.Domain.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace mikroLinkAPI.Domain.Entities
{
    public class ComponentSerial : Entity
    {
        public ComponentSerial()
        {
            MaterialTracking = new HashSet<MaterialTracking>();
            RequestSiteCompanySerial = new HashSet<RequestSiteCompanySerial>();
            StoreExit = new HashSet<StoreExit>();
            StoreIn = new HashSet<StoreIn>();
        }
        [NotMapped]
        public EntityState? PreviousState { get; set; }
        public string SeriNo { get; set; }
        public string ComponentId { get; set; }
        public string GIrsaliyeNo { get; set; }
        public int Sturdy { get; set; }
        public int Defective { get; set; }
        public int Scrap { get; set; }
        public string Shelf { get; set; }
        public int MaterialType { get; set; }
        public int? CompanyId { get; set; }
        public int? SiteId { get; set; }
        public int? TeamLeaderId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int State { get; set; }
        public Company Company { get; set; }
        public Component Component { get; set; }
        public Site Site { get; set; }
        public AccountSsom TeamLeader { get; set; }
        public AccountSsom WhoDone { get; set; }
        public ICollection<MaterialTracking> MaterialTracking { get; set; }
        public ICollection<RequestSiteCompanySerial> RequestSiteCompanySerial { get; set; }
        public ICollection<StoreExit> StoreExit { get; set; }
        public ICollection<StoreIn> StoreIn { get; set; }

    }
}
