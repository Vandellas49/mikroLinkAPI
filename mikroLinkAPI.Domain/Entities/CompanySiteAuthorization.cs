using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class CompanySiteAuthorization:Entity
    {
        public int CompanyId { get; set; }
        public int SiteId { get; set; }

        public  Company Company { get; set; }
        public  Site Site { get; set; }
    }
}
