using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class CompanyToCompanyAuthorization:Entity
    {
        public int ParentCompanyId { get; set; }
        public int CompanyId { get; set; }
        public  Company Company { get; set; }
        public  Company ParentCompany { get; set; }
    }
}
