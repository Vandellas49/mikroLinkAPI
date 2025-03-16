using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class Randevu:Entity
    {
        public int CompanyId { get; set; }
        public int TeamLeaderId { get; set; }
        public int RadevuPlanId { get; set; }
        public int Durum { get; set; }
        public RandevuPlanlanma RandevuPlanlanma { get; set; }
        public  AccountSsom TeamLeader { get; set; }

    }
}
