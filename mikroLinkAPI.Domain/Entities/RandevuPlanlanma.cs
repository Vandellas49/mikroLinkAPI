using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class RandevuPlanlanma:Entity
    {
        public int CompanyId { get; set; }
        public DateTime RandevuTarihi { get; set; }
        public string RandevuBaslangic { get; set; }
        public string RandevuBitis { get; set; }
        public Company Company { get; set; }
        public Randevu Randevu { get; set; }
    }
}
