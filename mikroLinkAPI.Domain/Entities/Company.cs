using mikroLinkAPI.Domain.Abstractions;

namespace mikroLinkAPI.Domain.Entities
{
    public sealed class Company : Entity
    {
        public Company()
        {
            AccountSsom = new HashSet<AccountSsom>();
            CompanySiteAuthorization = new HashSet<CompanySiteAuthorization>();
            CompanyToCompanyAuthorizationCompany = new HashSet<CompanyToCompanyAuthorization>();
            CompanyToCompanyAuthorizationParentCompany = new HashSet<CompanyToCompanyAuthorization>();
            ComponentSerial = new HashSet<ComponentSerial>();
            MaterialTracking = new HashSet<MaterialTracking>();
            RequestCompany = new HashSet<Request>();
            RequestReceiverCompany = new HashSet<Request>();
            StoreExitCompany = new HashSet<StoreExit>();
            StoreExitCompanyIdExitNavigation = new HashSet<StoreExit>();
            StoreInCompany = new HashSet<StoreIn>();
            StoreInFromCompany = new HashSet<StoreIn>();
            Teams = new HashSet<Teams>();
            RadevuPlanlama = new HashSet<RandevuPlanlanma>();
        }

        public string Name { get; set; }
        public int IlId { get; set; }
        public string Email { get; set; }

        public Iller Il { get; set; }
        public ICollection<AccountSsom> AccountSsom { get; set; }
        public ICollection<RandevuPlanlanma> RadevuPlanlama { get; set; }
        public ICollection<CompanySiteAuthorization> CompanySiteAuthorization { get; set; }
        public ICollection<CompanyToCompanyAuthorization> CompanyToCompanyAuthorizationCompany { get; set; }
        public ICollection<CompanyToCompanyAuthorization> CompanyToCompanyAuthorizationParentCompany { get; set; }
        public ICollection<ComponentSerial> ComponentSerial { get; set; }
        public ICollection<MaterialTracking> MaterialTracking { get; set; }
        public ICollection<Request> RequestCompany { get; set; }
        public ICollection<Request> RequestReceiverCompany { get; set; }
        public ICollection<StoreExit> StoreExitCompany { get; set; }
        public ICollection<StoreExit> StoreExitCompanyIdExitNavigation { get; set; }
        public ICollection<StoreIn> StoreInCompany { get; set; }
        public ICollection<StoreIn> StoreInFromCompany { get; set; }
        public ICollection<Teams> Teams { get; set; }
    }
}
